namespace Edelstein.Protocol.Network.Transport
{
    public interface ITransportClient : ITransport, IDeliveryUnicast
    {
        ISocketHandlerFactory SocketHandlerFactory { get; }
        ISocketHandler Handler { get; set; }
    }
}
