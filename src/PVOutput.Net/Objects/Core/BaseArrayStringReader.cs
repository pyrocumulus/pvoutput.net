using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PVOutput.Net.Objects.Core
{
    internal abstract class BaseArrayStringReader<TReturnType> : IArrayStringReader<TReturnType>
    {
        public virtual Task<IEnumerable<TReturnType>> ReadArrayAsync(Stream stream, CancellationToken cancellationToken = default)
        {
            if (stream == null)
                return Task.FromResult(default(IEnumerable<TReturnType>));

            using (TextReader textReader = new StreamReader(stream))
            {
                return ReadArrayAsync(textReader, cancellationToken);
            }
        }

        public abstract Task<IEnumerable<TReturnType>> ReadArrayAsync(TextReader reader, CancellationToken cancellationToken = default);
    }
}
