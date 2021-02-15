namespace Edelstein.Protocol.Database
{
    public interface IDataEntity : IRepositoryEntry<int>
    {
        new int ID { get; set; }
    }
}