using System.Threading.Tasks;
using Edelstein.Protocol.Database;
using LiteDB;

namespace Edelstein.Protocol.Database.LiteDB
{
    public class LiteDBDataStore : IDataStore
    {
        private readonly LiteRepository _repository;

        public LiteDBDataStore(LiteRepository repository)
            => _repository = repository;

        public Task Initialize()
            => Task.CompletedTask;

        public IDataSession StartSession()
            => new LiteDBDataSession(_repository);

        public IDataBatch StartBatch()
            => StartSession().Batch();
    }
}