namespace Edelstein.Protocol
{
    public interface IRepositoryEntry<TKey>
    {
        TKey ID { get; }
    }
}
