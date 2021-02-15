using System.Collections.Generic;
using System.Threading.Tasks;
using Edelstein.Protocol.Parsing;
using reWZ;

namespace Edelstein.Common.Parsing.reWZ
{
    public class WZDataDirectory : IDataDirectory
    {
        private readonly WZFile _file;

        public string Name => _file.MainDirectory.Name;
        public string Path => _file.MainDirectory.Path;

        public IEnumerable<IDataProperty> Children
        {
            get
            {
                var result = new HashSet<WZDataProperty>();

                foreach (var child in _file.MainDirectory)
                    result.Add(new WZDataProperty(child));

                return result;
            }
        }

        public WZDataDirectory(WZFile file)
        {
            _file = file;
        }

        public IDataProperty Resolve(string path = null)
            => new WZDataProperty(_file.MainDirectory).Resolve(path);

        public Task<IDataProperty> ResolveAsync(string path = null)
            => Task.Run(() => Resolve(path));
    }
}
