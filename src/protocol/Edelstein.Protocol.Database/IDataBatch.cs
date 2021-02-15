using System;
using System.Threading.Tasks;

namespace Edelstein.Protocol.Database
{
    public interface IDataBatch : IDataAction, IDisposable
    {
        void SaveChanges();
        Task SaveChangesAsync();
    }
}