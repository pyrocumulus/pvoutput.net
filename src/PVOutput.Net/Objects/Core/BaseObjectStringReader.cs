using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("PVOutput.Net.Tests")]
namespace PVOutput.Net.Objects.Core
{
    internal abstract class BaseObjectStringReader<TReturnType> : IObjectStringReader<TReturnType>
    {
        protected const char DefaultItemDelimiter = ',';
        protected const char DefaultGroupDelimiter = ';';

        protected virtual char ItemDelimiter => DefaultItemDelimiter;
        protected virtual char GroupDelimiter => DefaultGroupDelimiter;

        public abstract TReturnType CreateObjectInstance();

        protected List<Action<TReturnType, TextReader>> _parsers;

        public BaseObjectStringReader()
        {
            _parsers = new List<Action<TReturnType, TextReader>>();
        }

        public async Task<TReturnType> ReadObjectAsync(Stream stream, CancellationToken cancellationToken = default)
        {
            if (stream == null)
            {
                return await Task.FromResult(default(TReturnType)).ConfigureAwait(false);
            }

            using (TextReader textReader = new StreamReader(stream))
            {
                return await ReadObjectAsync(textReader, cancellationToken).ConfigureAwait(false);
            }
        }

        public async Task<TReturnType> ReadObjectAsync(TextReader reader, CancellationToken cancellationToken = default)
        {
            if (reader != null && reader.Peek() >= 0)
            {
                TReturnType output = CreateObjectInstance();
                ParseProperties(output, reader, cancellationToken);
                return output;
            }

            return await Task.FromResult(GetDefaultResult()).ConfigureAwait(false);
        }

        protected virtual TReturnType GetDefaultResult()
        {
            return default;
        }

        private void ParseProperties(TReturnType target, TextReader reader, CancellationToken cancellationToken = default)
        {
            foreach (Action<TReturnType, TextReader> parser in _parsers)
            {
                cancellationToken.ThrowIfCancellationRequested();

                parser(target, reader);
            }
        }

        protected void ParsePropertyArray(TReturnType target, TextReader reader, Action<TReturnType, string>[] properties)
        {
            for (var i = 0; i < properties.Length; i++)
            {
                properties[i](target, ReadProperty(reader));
            }
        }

        protected IList<string> ReadPropertiesForGroup(TextReader reader)
        {
            var result = new List<string>();
            var characters = new List<char>();
            while (reader.Peek() >= 0)
            {
                var c = (char)reader.Read();

                if (c == ItemDelimiter || c == GroupDelimiter)
                {
                    result.Add(new string(characters.ToArray()));

                    if (c == GroupDelimiter)
                    {
                        return result;
                    }

                    characters.Clear();
                    continue;
                }

                characters.Add(c);
            }

            result.Add(new string(characters.ToArray()));
            return result;
        }

        protected string ReadProperty(TextReader reader)
        {
            var characters = new List<char>();
            while (reader.Peek() >= 0)
            {
                var c = (char)reader.Read();

                if (c == ItemDelimiter || c == GroupDelimiter)
                {
                    return new string(characters.ToArray());
                }

                characters.Add(c);
            }

            return new string(characters.ToArray());
        }
    }
}
