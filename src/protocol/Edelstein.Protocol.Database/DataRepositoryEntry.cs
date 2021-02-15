namespace Edelstein.Protocol.Database
{
    public record DataRepositoryEntry : IDataEntity, IRepositoryEntry<int>
    {
        public int ID { get; set; }
    }
}
