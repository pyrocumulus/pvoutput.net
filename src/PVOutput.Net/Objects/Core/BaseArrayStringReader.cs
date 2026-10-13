using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace PVOutput.Net.Objects.Core
{
    internal abstract class BaseArrayStringReader<TReturnType> : IArrayStringReader<TReturnType>
    {
        public async Task<IEnumerable<TReturnType>> ReadArrayAsync(Stream stream, CancellationToken cancellationToken = default)
        {
            if (stream == null)
            {
                return new List<TReturnType>();
            }

            using (TextReader textReader = new StreamReader(stream))
            {
                return await ReadArrayAsync(textReader, cancellationToken).ConfigureAwait(false);
            }
        }

        public abstract Task<IEnumerable<TReturnType>> ReadArrayAsync(TextReader reader, CancellationToken cancellationToken = default);
    }
}
