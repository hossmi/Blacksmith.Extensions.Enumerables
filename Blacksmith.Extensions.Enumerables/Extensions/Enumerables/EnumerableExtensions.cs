using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Blacksmith
{
    public static class EnumerableExtensions
    {
        public static IOrderedEnumerable<TSource> orderBy<TSource, TKey>(
            this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, OrderDirection direction)
        {
            assertNotNull(source, nameof(source));
            assertNotNull(keySelector, nameof(keySelector));

            if (direction == OrderDirection.Ascendant)
                return source.OrderBy(keySelector);
            else if (direction == OrderDirection.Descendant)
                return source.OrderByDescending(keySelector);
            else
                throw new ArgumentException($"Wrong {nameof(OrderDirection)} enum value");
        }

        public static IOrderedEnumerable<TSource> thenBy<TSource, TKey>(
            this IOrderedEnumerable<TSource> source, Func<TSource, TKey> keySelector, OrderDirection direction)
        {
            assertNotNull(source, nameof(source));
            assertNotNull(keySelector, nameof(keySelector));

            if (direction == OrderDirection.Ascendant)
                return source.ThenBy(keySelector);
            else if (direction == OrderDirection.Descendant)
                return source.ThenByDescending(keySelector);
            else
                throw new ArgumentException($"Wrong {nameof(OrderDirection)} enum value");
        }

        public static IOrderedQueryable<TSource> orderBy<TSource, TKey>(
            this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, OrderDirection direction)
        {
            assertNotNull(source, nameof(source));
            assertNotNull(keySelector, nameof(keySelector));

            if (direction == OrderDirection.Ascendant)
                return source.OrderBy(keySelector);
            else if (direction == OrderDirection.Descendant)
                return source.OrderByDescending(keySelector);
            else
                throw new ArgumentException($"Wrong {nameof(OrderDirection)} enum value");
        }

        public static IOrderedQueryable<TSource> thenBy<TSource, TKey>(
            this IOrderedQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, OrderDirection direction)
        {
            assertNotNull(source, nameof(source));
            assertNotNull(keySelector, nameof(keySelector));

            if (direction == OrderDirection.Ascendant)
                return source.ThenBy(keySelector);
            else if (direction == OrderDirection.Descendant)
                return source.ThenByDescending(keySelector);
            else
                throw new ArgumentException($"Wrong {nameof(OrderDirection)} enum value");
        }

        public static void forEach<TSource>(this IEnumerable<TSource> items, Action<TSource> forEachDelegate)
        {
            assertNotNull(items, nameof(items));
            assertNotNull(items, nameof(forEachDelegate));

            foreach (TSource item in items)
                forEachDelegate(item);
        }

        public static IEnumerable<T> whereIf<T>(this IEnumerable<T> items, bool condition, Func<T, bool> predicate)
        {
            assertNotNull(items, nameof(items));
            assertNotNull(predicate, nameof(predicate));

            if (condition)
                return items.Where(predicate);
            else
                return items;
        }

        public static IQueryable<T> whereIf<T>(this IQueryable<T> items, bool condition, Expression<Func<T, bool>> predicate)
        {
            assertNotNull(items, nameof(items));
            assertNotNull(predicate, nameof(predicate));

            if (condition)
                return items.Where(predicate);
            else
                return items;
        }

        public static IEnumerable<T> whereIfStringIsFilled<T>(this IEnumerable<T> items, string text, Func<T, bool> predicate)
        {
            bool stringIsFilled;

            assertNotNull(items, nameof(items));
            assertNotNull(predicate, nameof(predicate));

            stringIsFilled = false == string.IsNullOrWhiteSpace(text);

            return whereIf<T>(items, stringIsFilled, predicate);
        }

        public static IQueryable<T> whereIfStringIsFilled<T>(this IQueryable<T> items, string text, Expression<Func<T, bool>> predicate)
        {
            bool stringIsFilled;

            assertNotNull(items, nameof(items));
            assertNotNull(predicate, nameof(predicate));

            stringIsFilled = false == string.IsNullOrWhiteSpace(text);

            return whereIf<T>(items, stringIsFilled, predicate);
        }

        public static IQueryable<T> paginate<T>(this IQueryable<T> source, int pageSize, int page)
        {
            assertNotNull(source, nameof(source));

            if (pageSize <= 0)
                throw new ArgumentException("PageSize must be greater than zero.", nameof(pageSize));
            if (page < 0)
                throw new ArgumentException("Page must be greater or equal than zero.", nameof(page));

            return source
                .Skip(page * pageSize)
                .Take(pageSize);
        }

        public static IEnumerable<T> paginate<T>(this IEnumerable<T> source, int pageSize, int page)
        {
            assertNotNull(source, nameof(source));

            if (pageSize <= 0)
                throw new ArgumentException("PageSize must be greater than zero.", nameof(pageSize));
            if (page < 0)
                throw new ArgumentException("Page must be greater or equal than zero.", nameof(page));

            return source
                .Skip(page * pageSize)
                .Take(pageSize);
        }

        public static ICollection<T> push<T>(this ICollection<T> source, T item)
        {
            assertNotNull(source, nameof(source));

            if (source.IsReadOnly)
                throw new ArgumentException("Source collection is read only.", nameof(source));

            source.Add(item);
            return source;
        }

        public static IEnumerable<TSource> notNull<TSource>(this IEnumerable<TSource> source)
        {
            assertNotNull(source, nameof(source));

            return source.Where(itemNotNull);
        }

        public static IEnumerable<string> notEmpty(this IEnumerable<string> source)
        {
            assertNotNull(source, nameof(source));

            return source.Where(stringIsNotEmpty);
        }

        public static IEnumerable<string> trim(this IEnumerable<string> source)
        {
            assertNotNull(source, nameof(source));

            return source.Select(trimString);
        }

        private static bool itemNotNull<T>(T item)
        {
            return item != null;
        }

        private static bool stringIsNotEmpty(string source)
        {
            return false == string.IsNullOrWhiteSpace(source);
        }

        private static string trimString(string source)
        {
            return source.Trim();
        }

        private static void assertNotNull(object item, string parameterName)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(parameterName));
        }

    }
}