using System.Threading.Tasks;

namespace Edelstein.Protocol.Network.Transport
{
    public interface ISocket : IDeliveryUnicast
    {
        ITransport Transport { get; }

        uint SeqSend { get; set; }
        uint SeqRecv { get; set; }
        bool EncryptData { get; }

        Task Close();
    }
}
