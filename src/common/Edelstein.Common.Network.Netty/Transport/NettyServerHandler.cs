using System;
using DotNetty.Handlers.Timeout;
using DotNetty.Transport.Channels;
using Edelstein.Common.Network.Netty.Logging;
using Edelstein.Protocol.Network;
using Edelstein.Protocol.Network.Transport;

namespace Edelstein.Common.Network.Netty.Transport
{
    public class NettyServerHandler : ChannelHandlerAdapter
    {
        private static readonly ILog Logger = LogProvider.GetCurrentClassLogger();
        private readonly ITransportServer _server;

        public NettyServerHandler(ITransportServer server)
        {
            _server = server;
        }

        public override void ChannelActive(IChannelHandlerContext context)
        {
            var random = new Random();
            var socket = new NettySocket(
                context.Channel,
                _server,
                (uint)random.Next(),
                (uint)random.Next()
            );
            var handler = _server.SocketHandlerFactory.Build(socket);
            var handshake = new RawPacketOutgoing();

            handshake.WriteShort(_server.Version);
            handshake.WriteString(_server.Patch);
            handshake.WriteInt((int)socket.SeqRecv);
            handshake.WriteInt((int)socket.SeqSend);
            handshake.WriteByte(_server.Locale);

            _ = socket.SendPacket(handshake);

            context.Channel.GetAttribute(NettySocketAttributes.SocketKey).Set(socket);
            context.Channel.GetAttribute(NettySocketAttributes.HandlerKey).Set(handler);
            Logger.Debug($"Accepted connection from {context.Channel.RemoteAddress}");

            lock (_server) _server.Sockets.Add(socket);
        }

        public override void ChannelInactive(IChannelHandlerContext context)
        {
            var socket = context.Channel.GetAttribute(NettySocketAttributes.SocketKey).Get();
            var handler = context.Channel.GetAttribute(NettySocketAttributes.HandlerKey).Get();

            handler?.OnDisconnect();
            base.ChannelInactive(context);
            Logger.Debug($"Released connection from {context.Channel.RemoteAddress}");

            lock (_server) _server.Sockets.Remove(socket);
        }

        public override void ChannelRead(IChannelHandlerContext context, object message)
        {
            var handler = context.Channel.GetAttribute(NettySocketAttributes.HandlerKey).Get();
            handler?.OnPacket((IPacketReader)message);
        }

        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception)
        {
            var ddr = context.Channel.GetAttribute(NettySocketAttributes.HandlerKey).Get();

            if (exception is ReadTimeoutException)
                Logger.Debug($"Closing connection from {context.Channel.RemoteAddress} due to idle activity");
            else ddr?.OnException(exception);
        }
    }
}
