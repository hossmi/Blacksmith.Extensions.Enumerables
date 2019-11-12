using System;
using System.Collections.Generic;
using System.Linq;

namespace Blacksmith
{
    public static class EnumerableExtensions
    {
        public static IOrderedEnumerable<TSource> sortBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, OrderDirection direction)
        {
            if (direction == OrderDirection.Ascendant)
                return source.OrderBy(keySelector);
            else if (direction == OrderDirection.Descendant)
                return source.OrderByDescending(keySelector);
            else
                throw new ArgumentException($"Wrong {nameof(OrderDirection)} enum value");
        }
    }
}