using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Aoc2019.Tests.Util
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<TSource> Flatten<TSource>(this IEnumerable<TSource> source, Func<TSource, IEnumerable<TSource>> children)
        {
            foreach (var item in source)
            {
                yield return item;
                foreach (var child in children(item).Flatten(children))
                {
                    yield return child;
                }
            }
        }

        public static IEnumerable<TSource> Pad<TSource>(this IEnumerable<TSource> source, int paddedLength, TSource padding)
        {
            var i = 0;
            foreach (var item in source)
            {
                i++;
                yield return item;
            }
            for(; i < paddedLength; i++)
            {
                yield return padding;
            }
        }

        public static int ToInt(this IEnumerable<int> digits) 
        {
            return digits.Reverse().Select((digit, i) => digit * 10.Pow(i)).Sum();
        }

        public static IEnumerable<TSource> ForEach<TSource>(this IEnumerable<TSource> source, Action<TSource> action)
        {
            var items = new List<TSource>();
            foreach (var item in source)
            {
                action(item);
                items.Add(item);
            }
            return items;
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
