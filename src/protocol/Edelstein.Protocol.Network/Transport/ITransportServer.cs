using System.Collections.Generic;

namespace Edelstein.Protocol.Network.Transport
{
    public interface ITransportServer : ITransport, IDeliveryBroadcast
    {
        ICollection<ISocket> Sockets { get; }
        ISocketHandlerFactory SocketHandlerFactory { get; }
    }
}
