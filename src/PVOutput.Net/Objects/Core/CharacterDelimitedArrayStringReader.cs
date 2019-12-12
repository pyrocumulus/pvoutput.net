using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using PVOutput.Net.Objects.Factories;

namespace PVOutput.Net.Objects.Core
{
    internal class CharacterDelimitedArrayStringReader<T> : BaseArrayStringReader<T>
    {
        public char[] Delimiter { get; }

        public CharacterDelimitedArrayStringReader(char delimiter = ';')
        {
            Delimiter = new char[] { delimiter };
        }

        public override async Task<IEnumerable<T>> ReadArrayAsync(TextReader reader, CancellationToken cancellationToken = default)
        {
            if (reader == null)
            {
                return await Task.FromResult(default(IEnumerable<T>));
            }

            var content = await reader.ReadToEndAsync();

            if (!string.IsNullOrEmpty(content))
            {
                var results = content.Split(Delimiter, StringSplitOptions.RemoveEmptyEntries);

                var objectReader = StringFactoryContainer.CreateObjectReader<T>();
                var objects = new List<T>();

                foreach (string outputString in results)
                {
                    T output = await objectReader.ReadObjectAsync(new StringReader(outputString), cancellationToken);
                    objects.Add(output);
                }

                return objects;
            }

            return await Task.FromResult(default(IEnumerable<T>));
        }
    }
}
