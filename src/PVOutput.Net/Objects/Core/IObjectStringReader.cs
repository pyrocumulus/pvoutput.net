using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace PVOutput.Net.Objects.Core
{
    public interface IObjectStringReader<TReturnType>
    {
        Task<TReturnType> ReadObjectAsync(Stream stream, CancellationToken cancellationToken = default);

        Task<TReturnType> ReadObjectAsync(TextReader reader, CancellationToken cancellationToken = default);
    }
}
