using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edelstein.Protocol.Database
{
    public class DataRepository<TEntry> : IRepositoryReader<int, TEntry>, IRepositoryWriter<int, TEntry>
         where TEntry : DataRepositoryEntry
    {
        protected IDataStore Store { get; }

        protected DataRepository(IDataStore store)
        {
            Store = store;
        }

        public Task<TEntry> Retrieve(int key)
        {
            using var session = Store.StartSession();
            return session.RetrieveAsync<TEntry>(key);
        }

        public Task<IEnumerable<TEntry>> RetrieveAll(IEnumerable<int> keys)
        {
            using var session = Store.StartSession();
            return Task.FromResult<IEnumerable<TEntry>>(
                session.Query<TEntry>().Where(e => keys.Contains(e.ID)).ToList()
            );
        }

        public Task<IEnumerable<TEntry>> RetrieveAll()
        {
            using var session = Store.StartSession();
            return Task.FromResult<IEnumerable<TEntry>>(
                session.Query<TEntry>().Where(e => true).ToList()
            );
        }

        public async Task<TEntry> Insert(TEntry entry)
        {
            using var session = Store.StartSession();
            await session.InsertAsync(entry);
            return entry;
        }

        public async Task<IEnumerable<TEntry>> Insert(IEnumerable<TEntry> entries)
        {
            using var session = Store.StartSession();
            await Task.WhenAll(entries.Select(e => session.InsertAsync(e)));
            return entries;
        }

        public Task Update(TEntry entry)
        {
            using var session = Store.StartSession();
            return session.UpdateAsync(entry);
        }

        public async Task Delete(int key)
        {
            using var session = Store.StartSession();
            await session.DeleteAsync(await session.RetrieveAsync<TEntry>(key));
        }

        public Task Delete(TEntry entry)
        {
            using var session = Store.StartSession();
            return session.DeleteAsync(entry);
        }
    }
}
