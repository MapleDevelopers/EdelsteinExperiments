using Edelstein.Protocol.Database;

namespace Edelstein.Protocol.Parsing.Entities
{
    public interface IAccount : IDataEntity
    {
        string Username { get; set; }

        string Password { get; set; }
        string PIN { get; set; }
        string SPW { get; set; }

        byte? Gender { get; set; }

        byte? LatestConnectedWorld { get; set; }
    }
}
