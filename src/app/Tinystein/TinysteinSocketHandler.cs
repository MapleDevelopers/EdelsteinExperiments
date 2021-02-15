using System;
using System.Threading.Tasks;
using Edelstein.Protocol.Network;
using Edelstein.Protocol.Network.Transport;
using Tinystein.Logging;

namespace Tinystein
{
    public class TinysteinSocketHandler : ISocketHandler
    {
        private static readonly ILog Logger = LogProvider.GetCurrentClassLogger();

        public ISocket Socket { get; }

        public TinysteinSocketHandler(ISocket socket)
        {
            Socket = socket;
        }

        public Task OnPacket(IPacketReader packet)
        {
            var operation = packet.ReadShort();
            Logger.Info($"Received packet of operation 0x{operation:X} ({operation}) of length {packet.Buffer.Length}");
            return Task.CompletedTask;
        }

        public Task OnException(Exception exception)
        {
            Logger.WarnException("Socket caught exception", exception);
            return Task.CompletedTask;
        }

        public Task OnUpdate()
            => Task.CompletedTask;

        public Task OnDisconnect()
        {
            Logger.Info("Socket disconnected");
            return Task.CompletedTask;
        }

        public Task SendPacket(IPacket packet)
            => Socket.SendPacket(packet);
    }
}
