using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using PVOutput.Net.Objects.Factories;

namespace PVOutput.Net.Objects.Core
{
    internal class SemiColonSeparatedArrayStringReader<T> : BaseArrayStringReader<T>
    {
        private const char delimiter = ';';

        public override async Task<IEnumerable<T>> ReadArrayAsync(TextReader reader, CancellationToken cancellationToken = default)
        {
            if (reader == null)
            {
                return await Task.FromResult(default(IEnumerable<T>));
            }

            var content = await reader.ReadToEndAsync();

            if (!string.IsNullOrEmpty(content))
            {
                var results = content.Split(delimiter);

                var objectReader = StringFactoryContainer.CreateObjectReader<T>();
                var objects = new List<T>();

                foreach (string outputString in results)
                {
#warning this has to go away
                    T output = await objectReader.ReadObjectAsync(new StringReader(outputString), cancellationToken);
                    objects.Add(output);
                }

                return objects;
            }

            return await Task.FromResult(default(IEnumerable<T>));
        }
    }
}
