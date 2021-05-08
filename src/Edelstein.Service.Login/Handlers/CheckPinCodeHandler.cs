using System.Threading.Tasks;
using Edelstein.Core.Network.Packets;
using Edelstein.Core.Utils.Packets;

namespace Edelstein.Service.Login.Handlers
{
    public class CheckPinCodeHandler : AbstractPacketHandler<LoginServiceAdapter>
    {
        protected override async Task Handle(
            LoginServiceAdapter adapter,
            RecvPacketOperations operation,
            IPacketDecoder packet
        )
        {
            var c2 = packet.DecodeByte();
            var c3 = packet.DecodeByte();

            //Todo

            using var p = new OutPacket(SendPacketOperations.SetAccountResult);
            p.EncodeBool(true);
            await adapter.SendPacket(p);
        }
    }
}