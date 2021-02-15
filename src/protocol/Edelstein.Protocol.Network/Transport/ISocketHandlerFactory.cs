namespace Edelstein.Protocol.Network.Transport
{
    public interface ISocketHandlerFactory
    {
        ISocketHandler Build(ISocket socket);
    }
}
