using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetty.Handlers.Timeout;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using Edelstein.Protocol.Network.Netty.Logging;
using Edelstein.Protocol.Network.Ciphers;
using Edelstein.Protocol.Network.Codecs;

namespace Edelstein.Protocol.Network.Transport
{
    public class NettyTransportServer : ITransportServer
    {
        private static readonly ILog Logger = LogProvider.GetCurrentClassLogger();

        public ICollection<ISocket> Sockets { get; }
        public ISocketHandlerFactory SocketHandlerFactory { get; }
        public short Version { get; }
        public string Patch { get; }
        public byte Locale { get; }

        private IChannel? Channel { get; set; }
        private IEventLoopGroup? BossGroup { get; set; }
        private IEventLoopGroup? WorkerGroup { get; set; }

        public NettyTransportServer(
            ISocketHandlerFactory socketHandlerFactory,
            short version,
            string patch,
            byte locale
        )
        {
            Sockets = new List<ISocket>();
            SocketHandlerFactory = socketHandlerFactory;
            Version = version;
            Patch = patch;
            Locale = locale;
        }

        public async Task Start(string host, int port)
        {
            var aesCipher = new AESCipher();
            var igCipher = new IGCipher();

            BossGroup = new MultithreadEventLoopGroup();
            WorkerGroup = new MultithreadEventLoopGroup();
            Channel = await new ServerBootstrap()
                .Group(BossGroup, WorkerGroup)
                .Channel<TcpServerSocketChannel>()
                .Option(ChannelOption.SoBacklog, 1024)
                .ChildHandler(new ActionChannelInitializer<IChannel>(ch =>
                {
                    ch.Pipeline.AddLast(
                        new ReadTimeoutHandler(TimeSpan.FromMinutes(1)),
                        new NettyPacketDecoder(this, aesCipher, igCipher),
                        new NettyServerHandler(this),
                        new NettyPacketEncoder(this, aesCipher, igCipher)
                    );
                }))
                .BindAsync(port);

            Logger.Info($"Bounded server on {host}:{port}");
        }

        public Task BroadcastPacket(IPacket packet)
            => Task.WhenAll(Sockets.Select(s => s.SendPacket(packet)));

        public async Task Close()
        {
            await Task.WhenAll(Sockets.Select(s => s.Close()));
            if (Channel != null) await Channel.CloseAsync();
            if (BossGroup != null) await BossGroup.ShutdownGracefullyAsync();
            if (WorkerGroup != null) await WorkerGroup.ShutdownGracefullyAsync();
        }
    }
}
