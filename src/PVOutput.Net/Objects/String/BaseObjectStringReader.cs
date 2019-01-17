using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PVOutput.Net.Objects.String
{
    internal abstract class BaseObjectStringReader<TReturnType> : IObjectStringReader<TReturnType>
    {
        protected const char Separator = ',';

        protected abstract Action<TReturnType, string>[] ObjectProperties { get; }

        private void ApplyPropertiesByIndex(TReturnType target, string[] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                ObjectProperties[i](target, data[i]);
            }
        }

        public virtual async Task<TReturnType> ReadObjectAsync(Stream stream, CancellationToken cancellationToken = default)
        {
            if (stream == null)
                return await Task.FromResult(default(TReturnType));

            using (TextReader textReader = new StreamReader(stream))
            {
                var content = await textReader.ReadToEndAsync();

                return await ReadObjectAsync(content, cancellationToken);
            }
        }

        public abstract TReturnType CreateObjectInstance();

        public async Task<TReturnType> ReadObjectAsync(string content, CancellationToken cancellationToken = default)
        {
            if (!string.IsNullOrEmpty(content))
            {
                TReturnType output = CreateObjectInstance();

                ApplyPropertiesByIndex(output, content.Split(Separator));

                return output;
            }

            return await Task.FromResult(default(TReturnType));
        }
    }
}
