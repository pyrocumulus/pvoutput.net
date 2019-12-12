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
                return await Task.FromResult(default(IEnumerable<TObjectType>));
            }

            var objectReader = StringFactoryContainer.CreateObjectReader<TObjectType>();
            var results = new List<TObjectType>();
            var characters = new List<char>();

            while (reader.Peek() >= 0)
            {
                var c = (char)reader.Read();

                if (c == Delimiter)
                {
                    await ReadAndAddObjectAsync(objectReader, results, new string(characters.ToArray()), cancellationToken);

                    characters.Clear();
                    continue;
                }

                characters.Add(c);
            }

            if (characters.Count > 0)
            {
                await ReadAndAddObjectAsync(objectReader, results, new string(characters.ToArray()), cancellationToken);
            }

            return await Task.FromResult(results);
        }

        private static async Task ReadAndAddObjectAsync(IObjectStringReader<TObjectType> objectReader, List<TObjectType> objectList, string objectContent, CancellationToken cancellationToken)
        {
            TObjectType output = await objectReader.ReadObjectAsync(new StringReader(objectContent), cancellationToken);
            objectList.Add(output);
        }
    }
}
