using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Edelstein.Protocol.Parsing;
using reWZ.WZProperties;

namespace Edelstein.Common.Parsing.reWZ
{
    public class WZDataProperty : IDataProperty
    {
        private readonly WZObject _node;

        public string Name => _node.Name;
        public string Path => _node.Name;
        public IDataProperty Parent => new WZDataProperty(_node.Parent);
        public IEnumerable<IDataProperty> Children
        {
            get
            {
                var result = new HashSet<WZDataProperty>();

                foreach (var child in _node)
                    result.Add(new WZDataProperty(child));

                return result;
            }
        }

        public WZDataProperty(WZObject node)
        {
            _node = node;
        }

        public IDataProperty Resolve(string path = null)
        {
            var node = _node.ResolvePath(path);
            return node == null ? null : new WZDataProperty(node);
        }

        public IDataProperty ResolveAll()
            => new WZDataProperty(_node);

        public void ResolveAll(Action<IDataProperty> context)
            => context.Invoke(ResolveAll());

        public T? Resolve<T>(string path = null) where T : struct
            => _node.ResolvePath(path).ValueOrDie<T>();

        public T ResolveOrDefault<T>(string path = null) where T : class
            => _node.ResolvePath(path).ValueOrDefault<T>(null);

        public Task<IDataProperty> ResolveAsync(string path = null)
            => Task.Run(() => Resolve(path));

        public Task<T?> ResolveAsync<T>(string path = null) where T : struct
            => Task.Run(() => Resolve<T>(path));

        public Task<T> ResolveOrDefaultAsync<T>(string path = null) where T : class
            => Task.Run(() => ResolveOrDefaultAsync<T>(path));
    }
}
