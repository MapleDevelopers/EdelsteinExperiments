using System.Threading.Tasks;

namespace Edelstein.Protocol.Network.Transport
{
    public interface IDeliveryBroadcast
    {
        Task BroadcastPacket(IPacket packet);
    }
}
