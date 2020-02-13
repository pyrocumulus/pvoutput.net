using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace PVOutput.Net.Objects.Core
{
    internal interface IArrayStringReader<TReturnType>
    {
        Task<IEnumerable<TReturnType>> ReadArrayAsync(Stream stream, CancellationToken cancellationToken = default);

        Task<IEnumerable<TReturnType>> ReadArrayAsync(TextReader reader, CancellationToken cancellationToken = default);
    }
}
