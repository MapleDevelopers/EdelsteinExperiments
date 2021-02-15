using System.Threading.Tasks;

namespace Edelstein.Protocol.Database
{
    public interface IDataStore
    {
        Task Initialize();

        IDataSession StartSession();
        IDataBatch StartBatch();
    }
}