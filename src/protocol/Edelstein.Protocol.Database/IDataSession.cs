using System;

namespace Edelstein.Protocol.Database
{
    public interface IDataSession : IDataAction, IDisposable
    {
        IDataQuery<T> Query<T>() where T : class, IDataEntity;
        IDataBatch Batch();
    }
}