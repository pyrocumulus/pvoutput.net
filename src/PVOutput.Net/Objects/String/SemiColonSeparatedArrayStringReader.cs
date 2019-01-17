using PVOutput.Net.Objects.Factories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PVOutput.Net.Objects.String
{
    internal class SemiColonSeparatedArrayStringReader<T> : BaseArrayStringReader<T>
    {
        private const char delimiter = ';';

        public override async Task<IEnumerable<T>> ReadArrayAsync(TextReader reader, CancellationToken cancellationToken = default)
        {
            if (reader == null)
                return await Task.FromResult(default(IEnumerable<T>));

            var content = await reader.ReadToEndAsync();

            if (!string.IsNullOrEmpty(content))
            {
                var results = content.Split(delimiter);

                var objectReader = StringFactoryContainer.CreateObjectReader<T>();
                var objects = new List<T>();

                foreach (string outputString in results)
                {
                    T output = await objectReader.ReadObjectAsync(outputString, cancellationToken);
                    objects.Add(output);
                }

                return objects;
            }

            return await Task.FromResult(default(IEnumerable<T>));
        }
    }
}
