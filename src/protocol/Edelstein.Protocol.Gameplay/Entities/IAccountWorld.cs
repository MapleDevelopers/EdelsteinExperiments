using Edelstein.Protocol.Database;

namespace Edelstein.Protocol.Parsing.Entities
{
    public interface IAccountWorld : IDataEntity
    {
        int AccountID { get; set; }

        byte WorldID { get; set; }
        int SlotCount { get; set; }
    }
}
