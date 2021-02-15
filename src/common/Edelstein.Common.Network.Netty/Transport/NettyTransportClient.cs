using System.Net;
using System.Threading.Tasks;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using Edelstein.Common.Network.Codecs;
using Edelstein.Common.Network.Netty.Logging;
using Edelstein.Protocol.Network;
using Edelstein.Protocol.Network.Ciphers;
using Edelstein.Protocol.Network.Transport;

namespace Edelstein.Common.Network.Netty.Transport
{
    public class NettyTransportClient : ITransportClient
    {
        private static readonly ILog Logger = LogProvider.GetCurrentClassLogger();

        public ISocketHandlerFactory SocketHandlerFactory { get; }
        public ISocketHandler? Handler { get; set; }
        public short Version { get; }
        public string Patch { get; }
        public byte Locale { get; }

        private IChannel? Channel { get; set; }
        private IEventLoopGroup? WorkerGroup { get; set; }

        public NettyTransportClient(
            ISocketHandlerFactory socketHandlerFactory,
            short version,
            string patch,
            byte locale
        )
        {
            SocketHandlerFactory = socketHandlerFactory;
            Version = version;
            Patch = patch;
            Locale = locale;
        }

        public async Task Start(string host, int port)
        {
            var aesCipher = new AESCipher();
            var igCipher = new IGCipher();

            WorkerGroup = new MultithreadEventLoopGroup();
            Channel = await new Bootstrap()
                .Group(WorkerGroup)
                .Channel<TcpSocketChannel>()
                .Option(ChannelOption.TcpNodelay, true)
                .Handler(new ActionChannelInitializer<IChannel>(ch =>
                {
                    ch.Pipeline.AddLast(
                        new NettyPacketDecoder(this, aesCipher, igCipher),
                        new NettyClientHandler(this),
                        new NettyPacketEncoder(this, aesCipher, igCipher)
                    );
                }))
                .ConnectAsync(IPAddress.Parse(host), port);

            Logger.Info($"Established connection to {host}:{port}");
        }

        public async Task SendPacket(IPacket packet) {
            if (Handler != null)
                await Handler.SendPacket(packet);
        }

        public async Task Close()
        {
            if (Handler != null) await Handler.Socket.Close();
            if (Channel != null) await Channel.CloseAsync();
            if (WorkerGroup != null) await WorkerGroup.ShutdownGracefullyAsync();
        }
    }
}
