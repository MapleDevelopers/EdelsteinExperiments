using Edelstein.Protocol.Network.Transport;

namespace Tinystein
{
    public class TinysteinSocketHandlerFactory : ISocketHandlerFactory
    {
        public ISocketHandler Build(ISocket socket)
            => new TinysteinSocketHandler(socket);
    }
}
