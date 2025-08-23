using System.Collections.Generic;

namespace PVOutput.Net.Objects.Core
{
    internal static class DictionaryExtensions
    {
        /// <summary>
        /// Adds the key and value to the dictionary only if the value is not null.
        /// </summary>
        internal static void AddIfNotNull<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue? value)
            where TKey : notnull
        {
            if (value is not null)
            {
                dictionary.Add(key, value);
            }
        }
    }
}
