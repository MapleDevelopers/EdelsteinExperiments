using System.Threading.Tasks;

namespace Edelstein.Protocol.Network.Transport
{
    public interface ITransport
    {
        short Version { get; }
        string Patch { get; }
        byte Locale { get; }

        Task Start(string host, int port);
        Task Close();
    }
}
