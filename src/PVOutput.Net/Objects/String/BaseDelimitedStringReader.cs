using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PVOutput.Net.Objects.String
{
	internal abstract class BaseDelimitedStringReader
	{
		protected const char ItemDelimiter = ',';
		protected const char GroupDelimiter = ';';

		protected IEnumerable<string> ReadPropertiesForGroup(TextReader reader)
		{
			var characters = new List<char>();
			while (reader.Peek() >= 0)
			{
				var c = (char)reader.Read();

				if (c == ItemDelimiter || c == GroupDelimiter)
				{
					yield return new string(characters.ToArray());

					if (c == GroupDelimiter)
						yield break;

					characters.Clear();
					continue;
				}

				characters.Add(c);
			}

			yield return new string(characters.ToArray());
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
