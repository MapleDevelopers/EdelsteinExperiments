using Edelstein.Protocol.Database;
using Edelstein.Protocol.Interop.V1;

namespace Edelstein.Common.Interop
{
    public class ServerRegistryServiceImpl : ServerRegistryService.ServerRegistryServiceBase
    {
        private readonly IDataStore _store;

        public ServerRegistryServiceImpl(IDataStore store)
        {
            _store = store;
        }
    }
}
