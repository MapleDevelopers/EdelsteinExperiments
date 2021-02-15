using System.Threading.Tasks;

namespace Edelstein.Protocol.Network.Transport
{
    public interface IDeliveryUnicast
    {
        Task SendPacket(IPacket packet);
    }
}
