using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Edelstein.Core.Gameplay.Migrations.States;
using Edelstein.Core.Network.Packets;
using Edelstein.Core.Utils.Packets;
using Edelstein.Service.Login.Types;

namespace Edelstein.Service.Login.Handlers
{
    public class CheckUserLimitHandler : AbstractPacketHandler<LoginServiceAdapter>
    {
        protected override async Task Handle(
            LoginServiceAdapter adapter,
            RecvPacketOperations operation,
            IPacketDecoder packet
        )
        {
            var worldID = packet.DecodeByte();

            if (adapter.Account == null) return;

            var services = (await adapter.Service.GetPeers())
                .Select(n => n.State)
                .OfType<GameNodeState>()
                .Where(s => s.Worlds.Contains(worldID))
                .ToImmutableList();
            var tasks = services
                .Select(async s => await adapter.Service.SocketCountCache.GetAsync<int>(s.Name))
                .ToImmutableList();

            await Task.WhenAll(tasks);

            var userNo = tasks
                .Select(c => c.Result)
                .Select(r => r.HasValue ? r.Value : 0)
                .Sum();
            var userLimit = adapter.Service.State.Worlds
                .First(w => w.ID == worldID)
                .UserLimit;

            var capacity = (double) userNo / Math.Max(1, userLimit);
            capacity = Math.Min(1, capacity);
            capacity = Math.Max(0, capacity);

            using var p = new OutPacket(SendPacketOperations.CheckUserLimitResult);

            var capacityState = capacity switch
            {
                1 when capacity >= 1 => WorldCapacityState.Full,
                0.75 when capacity >= 0.75 => WorldCapacityState.OverPopulated,
                _ => WorldCapacityState.Normal
            };

            p.EncodeByte((byte)capacityState);

            var populationLevel = capacity switch
            {
                0.75 when capacity >= 0.75 => WorldPopulationLevel.OverPopulated,
                0.5 when capacity >= 0.5 => WorldPopulationLevel.HighlyPopulated,
                _ => WorldPopulationLevel.Normal
            };

            p.EncodeByte((byte)populationLevel);

            await adapter.SendPacket(p);
        }
    }
}