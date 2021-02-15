using System.Threading.Tasks;

namespace Edelstein.Protocol.Parsing.Entities.Characters
{
    public interface ICharacterRepository<TEntry> :
        IRepositoryWriter<int, TEntry>,
        IRepositoryReader<int, TEntry>
    where TEntry : class, ICharacter
    {
        Task<TEntry> RetrieveByName(string name);
    }
}
