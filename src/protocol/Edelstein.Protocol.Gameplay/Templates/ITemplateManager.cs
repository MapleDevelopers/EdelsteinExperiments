using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Edelstein.Protocol.Parsing.Templates
{
    public interface ITemplateManager
    {
        Task Register<T>(ITemplateRepository repository) where T : ITemplate;
        Task Deregister<T>() where T : ITemplate;

        Task Register(Type type, ITemplateRepository repository);
        Task Deregister(Type type);

        T Retrieve<T>(int id) where T : ITemplate;
        IEnumerable<T> RetrieveAll<T>() where T : ITemplate;

        Task<T> RetrieveAsync<T>(int id) where T : ITemplate;
        Task<IEnumerable<T>> RetrieveAllAsync<T>() where T : ITemplate;
    }
}
