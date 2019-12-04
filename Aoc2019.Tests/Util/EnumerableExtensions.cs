using System;
using System.Collections.Generic;

namespace Aoc2019.Tests.Util
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<TSource> ForEach<TSource>(this IEnumerable<TSource> source, Action<TSource> action)
        {
            foreach (var item in source)
            {
                action(item);
                yield return item;
            }
        }

        public static IEnumerable<TSource> AsEnumerable<TSource>(this TSource source)
        {
            yield return source;
        }

        public static IEnumerable<TSource> SelectWhile<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, TSource> selector,
            Func<TSource, bool> predicate)
        {
            foreach (var item in source)
            {
                var current = item;
                while (predicate(current))
                {
                    yield return current;
                    current = selector(current);
                }
            }
        }
    }
}
