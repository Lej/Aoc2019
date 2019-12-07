using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc2019.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<IEnumerable<TSource>> Permutations<TSource>(this IEnumerable<TSource> source)
        {
            var items = source.ToList();
            var factorial = items.Count.Factorial();
            for (var i = 0; i < factorial; i++)
            {
                yield return items.Permutation(i);
            }
        }

        public static IEnumerable<TSource> Permutation<TSource>(this IEnumerable<TSource> source, int n)
        {
            var nodes = new LinkedList<TSource>(source);
            var factoradicDigits = n.ToFactoradicDigits().PadLeft(nodes.Count, 0);
            foreach (var factoradicDigit in factoradicDigits)
            {
                var node = nodes.First;
                for (var i = 0; i < factoradicDigit; i++)
                {
                    node = node.Next;
                }
                nodes.Remove(node);
                yield return node.Value;
            }
        }

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

        public static IEnumerable<TSource> PadRight<TSource>(this IEnumerable<TSource> source, int paddedLength, TSource padding)
        {
            var i = 0;
            foreach (var item in source)
            {
                i++;
                yield return item;
            }
            for (; i < paddedLength; i++)
            {
                yield return padding;
            }
        }

        public static IEnumerable<TSource> PadLeft<TSource>(this IEnumerable<TSource> source, int paddedLength, TSource padding)
        {
            var items = source.ToList();
            var missing = Math.Max(0, paddedLength - items.Count);
            for (var i = 0; i < missing; i++)
            {
                yield return padding;
            }
            foreach (var item in items)
            {
                yield return item;
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
