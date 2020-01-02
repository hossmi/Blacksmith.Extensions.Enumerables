using System;
using System.Collections.Generic;
using System.Linq;

namespace Blacksmith.Extensions.Enumerables.Strings
{
    public static class StringEnumerableExtensions
    {
        public const string DEFAULT_STRINGIFY_SEPARATOR = ", ";

        public static string stringify<T>(this IEnumerable<T> items
            , Func<T, string> selectorFunction = null
            , string separator = DEFAULT_STRINGIFY_SEPARATOR
            , bool skipEmptyItems = true)
        {
            selectorFunction = selectorFunction ?? prv_toString<T>;

            return prv_stringify(items, selectorFunction, separator, skipEmptyItems);
        }

        private static string prv_stringify<T>(IEnumerable<T> items, Func<T, string> selectorFunction, string separator, bool skipEmptyItems)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));
            if (selectorFunction is null)
                throw new ArgumentNullException(nameof(selectorFunction));
            if (separator == null)
                throw new ArgumentNullException(nameof(separator));

            return items
                .Select(selectorFunction)
                .whereIf(skipEmptyItems, prv_stringHasContent)
                .DefaultIfEmpty(string.Empty)
                .Aggregate((acum, item) => $"{acum}{separator}{item}");
        }

        private static bool prv_stringHasContent(string item)
        {
            return string.IsNullOrWhiteSpace(item) == false;
        }

        private static string prv_toString<T>(T item)
        {
            return item?.ToString();
        }
    }
}
