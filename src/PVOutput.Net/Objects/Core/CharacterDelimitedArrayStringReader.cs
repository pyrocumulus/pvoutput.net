using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using PVOutput.Net.Objects.Factories;

namespace PVOutput.Net.Objects.Core
{
    internal class CharacterDelimitedArrayStringReader<TObjectType> : BaseArrayStringReader<TObjectType>
    {
        public char Delimiter { get; }

        public CharacterDelimitedArrayStringReader(char delimiter = ';')
        {
            Delimiter = delimiter;
        }

        public override async Task<IEnumerable<TObjectType>> ReadArrayAsync(TextReader reader, CancellationToken cancellationToken = default)
        {
            if (reader == null)
            {
                return await Task.FromResult(default(IEnumerable<TObjectType>)).ConfigureAwait(false);
            }

            IObjectStringReader<TObjectType> objectReader = StringFactoryContainer.CreateObjectReader<TObjectType>();
            var results = new List<TObjectType>();
            var characters = new List<char>();

            while (reader.Peek() >= 0)
            {
                var c = (char)reader.Read();

                if (c == Delimiter)
                {
                    await ReadAndAddObjectAsync(objectReader, results, new string(characters.ToArray()), cancellationToken).ConfigureAwait(false);

                    characters.Clear();
                    continue;
                }

                characters.Add(c);
            }

            if (characters.Count > 0)
            {
                await ReadAndAddObjectAsync(objectReader, results, new string(characters.ToArray()), cancellationToken).ConfigureAwait(false);
            }

            return await Task.FromResult(results).ConfigureAwait(false);
        }

        private static async Task ReadAndAddObjectAsync(IObjectStringReader<TObjectType> objectReader, List<TObjectType> objectList, string objectContent, CancellationToken cancellationToken)
        {
            using (var reader = new StringReader(objectContent))
            {
                TObjectType output = await objectReader.ReadObjectAsync(reader, cancellationToken).ConfigureAwait(false);
                objectList.Add(output);
            }
        }
    }
}
