using System.Threading.Tasks;
using DotNetty.Transport.Channels;
using Edelstein.Protocol.Network;
using Edelstein.Protocol.Network.Transport;

namespace Edelstein.Common.Network.Netty.Transport
{
    public class NettySocket : ISocket
    {
        public readonly IChannel _channel;

        public ITransport Transport { get; }

        public uint SeqSend { get; set; }
        public uint SeqRecv { get; set; }

        public bool EncryptData => true;

        public NettySocket(
            IChannel channel,
            ITransport transport,
            uint seqSend,
            uint seqRecv
        )
        {
            _channel = channel;

            Transport = transport;
            SeqSend = seqSend;
            SeqRecv = seqRecv;
        }

        public async Task SendPacket(IPacket packet)
        {
            if (_channel.Open)
                await _channel.WriteAndFlushAsync(packet);
        }

        public async Task Close()
        {
            await _channel.DisconnectAsync();
        }
    }
}
