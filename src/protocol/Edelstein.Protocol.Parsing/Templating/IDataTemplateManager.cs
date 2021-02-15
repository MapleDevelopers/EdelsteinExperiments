using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Edelstein.Protocol.Parsing.Templating
{
    public interface IDataTemplateManager
    {
        Task Register<T>(IDataTemplateRepository repository) where T : IDataTemplate;
        Task Deregister<T>() where T : IDataTemplate;

        Task Register(Type type, IDataTemplateRepository repository);
        Task Deregister(Type type);

        T Retrieve<T>(int id) where T : IDataTemplate;
        IEnumerable<T> RetrieveAll<T>() where T : IDataTemplate;

        Task<T> RetrieveAsync<T>(int id) where T : IDataTemplate;
        Task<IEnumerable<T>> RetrieveAllAsync<T>() where T : IDataTemplate;
    }
}
