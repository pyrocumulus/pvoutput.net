using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using PVOutput.Net.Objects.Factories;

namespace PVOutput.Net.Objects.Core
{
    internal class LineDelimitedArrayStringReader<TObjectType> : BaseArrayStringReader<TObjectType>
    {
        public LineDelimitedArrayStringReader()
        {
        }

        public override async Task<IEnumerable<TObjectType>> ReadArrayAsync(TextReader reader, CancellationToken cancellationToken = default)
        {
            if (reader == null)
            {
                return await Task.FromResult(default(IEnumerable<TObjectType>)).ConfigureAwait(false);
            }

            IObjectStringReader<TObjectType> objectReader = StringFactoryContainer.CreateObjectReader<TObjectType>();
            var results = new List<TObjectType>();

            while (reader.Peek() >= 0)
            {
                var line = await reader.ReadLineAsync().ConfigureAwait(false);
                await ReadAndAddObjectAsync(objectReader, results, line, cancellationToken).ConfigureAwait(false);
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
