using Edelstein.Protocol.Database;
using Edelstein.Protocol.Interop.V1;

namespace Edelstein.Common.Interop
{
    public class SessionServiceImpl : SessionService.SessionServiceBase
    {
        private readonly IDataStore _store;

        public SessionServiceImpl(IDataStore store)
        {
            _store = store;
        }
    }
}
