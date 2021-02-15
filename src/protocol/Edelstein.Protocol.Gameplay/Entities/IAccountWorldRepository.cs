using System.Threading.Tasks;

namespace Edelstein.Protocol.Parsing.Entities
{
    public interface IAccountWorldRepository<TEntry> :
        IRepositoryWriter<int, TEntry>,
        IRepositoryReader<int, TEntry>
    where TEntry : class, IAccountWorld
    {
        Task<TEntry> RetrieveByAccountAndWorld(int accountID, int worldID);
    }
}
