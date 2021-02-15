using System;
using System.Threading.Tasks;

namespace Edelstein.Protocol.Network.Transport
{
    public interface ISocketHandler : IDeliveryUnicast
    {
        ISocket Socket { get; }

        Task OnPacket(IPacketReader packet);
        Task OnException(Exception exception);
        Task OnUpdate();
        Task OnDisconnect();
    }
}
