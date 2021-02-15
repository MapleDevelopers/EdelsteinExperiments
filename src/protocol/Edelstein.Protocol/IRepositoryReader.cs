using System.Collections.Generic;
using System.Threading.Tasks;

namespace Edelstein.Protocol
{
    public interface IRepositoryReader<
        TKey,
        TEntry
    > where TEntry : class, IRepositoryEntry<TKey>
    {
        Task<TEntry> Retrieve(TKey key);
        Task<IEnumerable<TEntry>> RetrieveAll(IEnumerable<TKey> keys);
        Task<IEnumerable<TEntry>> RetrieveAll();
    }
}
