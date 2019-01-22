using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PVOutput.Net.Objects.String
{
	internal abstract class BaseObjectStringReader<TReturnType> : BaseDelimitedStringReader, IObjectStringReader<TReturnType>
	{
		protected abstract Action<TReturnType, string>[] ObjectProperties { get; }

		public abstract TReturnType CreateObjectInstance();

		public virtual async Task<TReturnType> ReadObjectAsync(Stream stream, CancellationToken cancellationToken = default)
		{
			if (stream == null)
				return await Task.FromResult(default(TReturnType));

			using (TextReader textReader = new StreamReader(stream))
			{
				return await ReadObjectAsync(textReader, cancellationToken);
			}
		}

		public async Task<TReturnType> ReadObjectAsync(TextReader reader, CancellationToken cancellationToken = default)
		{
			if (reader == null)
				return await Task.FromResult(default(TReturnType));

			if (reader.Peek() >= 0)
			{
				TReturnType output = CreateObjectInstance();

				int i = 0;
				foreach (string property in ReadPropertiesForGroup(reader))
				{
					ObjectProperties[i](output, property);
					i++;
				}

				return output;
			}

			return await Task.FromResult(default(TReturnType));
		}
	}

	internal abstract class ComplexObjectStringReader<TReturnType> : BaseDelimitedStringReader, IObjectStringReader<TReturnType>
    {
		public abstract TReturnType CreateObjectInstance();

		protected List<Action<TReturnType, TextReader>> _parsers;

		public virtual async Task<TReturnType> ReadObjectAsync(Stream stream, CancellationToken cancellationToken = default)
        {
            if (stream == null)
                return await Task.FromResult(default(TReturnType));

            using (TextReader textReader = new StreamReader(stream))
            {
                return await ReadObjectAsync(textReader, cancellationToken);
            }
        }

        public async Task<TReturnType> ReadObjectAsync(TextReader reader, CancellationToken cancellationToken = default)
        {
            if (reader != null && reader.Peek() >= 0)
            {
                TReturnType output = CreateObjectInstance();

                ParseProperties(output, reader);

                return output;
            }

            return await Task.FromResult(default(TReturnType));
        }

		protected void ParseProperties(TReturnType target, TextReader reader, CancellationToken cancellationToken = default)
		{
			foreach (var parser in _parsers)
			{
				cancellationToken.ThrowIfCancellationRequested();

				parser(target, reader);
			}
		}

		protected void ParsePropertyArray(TReturnType target, TextReader reader, Action<TReturnType, string>[] properties)
		{
			for (int i = 0; i < properties.Length; i++)
			{
				properties[i](target, ReadProperty(reader));
			}
		}
	}
}
