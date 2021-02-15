using System.Threading.Tasks;

namespace Edelstein.Protocol.Parsing.Entities
{
    public interface IAccountRepository<TEntry> :
        IRepositoryWriter<int, TEntry>,
        IRepositoryReader<int, TEntry>
    where TEntry : class, IAccount
    {
        Task<TEntry> RetrieveByUsername(string username);
    }
}
