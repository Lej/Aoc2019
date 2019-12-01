using System;
using System.Collections.Generic;

namespace Aoc2019.Util
{
    public static class EnumerableExtensions
    {
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
