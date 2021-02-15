using System;
using DotNetty.Transport.Channels;
using Edelstein.Common.Network.Netty.Logging;
using Edelstein.Protocol.Network;
using Edelstein.Protocol.Network.Transport;

namespace Edelstein.Common.Network.Netty.Transport
{
    public class NettyClientHandler : ChannelHandlerAdapter
    {
        private static readonly ILog Logger = LogProvider.GetCurrentClassLogger();
        private readonly ITransportClient _client;

        public NettyClientHandler(ITransportClient client)
        {
            _client = client;
        }

        public override void ChannelRead(IChannelHandlerContext context, object message)
        {
            var handler = context.Channel.GetAttribute(NettySocketAttributes.HandlerKey).Get();
            var handshake = (IPacketReader)message;

            if (handler != null) handler.OnPacket(handshake);
            else
            {
                var version = handshake.ReadShort();
                var patch = handshake.ReadString();
                var seqSend = handshake.ReadUInt();
                var seqRecv = handshake.ReadUInt();
                var locale = handshake.ReadByte();

                if (version != _client.Version) return;
                if (patch != _client.Patch) return;
                if (locale != _client.Locale) return;

                var newSocket = new NettySocket(
                    context.Channel,
                    _client,
                    seqSend,
                    seqRecv
                );
                var newHandler = _client.SocketHandlerFactory.Build(newSocket);

                lock (_client)
                    _client.Handler = newHandler;

                context.Channel.GetAttribute(NettySocketAttributes.SocketKey).Set(newSocket);
                context.Channel.GetAttribute(NettySocketAttributes.HandlerKey).Set(newHandler);
                Logger.Debug($"Initialized connection to {context.Channel.RemoteAddress}");
            }
        }

        public override void ChannelInactive(IChannelHandlerContext context)
        {
            var handler = context.Channel.GetAttribute(NettySocketAttributes.HandlerKey).Get();

            handler?.OnDisconnect();
            base.ChannelInactive(context);

            Logger.Debug($"Released connection to {context.Channel.RemoteAddress}");
        }


        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception)
        {
            var handler = context.Channel.GetAttribute(NettySocketAttributes.HandlerKey).Get();
            handler?.OnException(exception);
        }
    }
}
