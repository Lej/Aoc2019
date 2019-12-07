using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Aoc2019.Utils
{
    public class LambdaComparer<T> : IEqualityComparer<T>
    {
        private readonly Func<T, T, bool> _equals;
        private readonly Func<T, dynamic> _hashCode;

        private LambdaComparer(Func<T, T, bool> equals, Func<T, dynamic> toHash = null)
        {
            _equals = equals;
            _hashCode = toHash;
        }

        public static IEqualityComparer<T> Create(Func<T, T, bool> equals, Func<T, dynamic> toHash = null)
        {
            return new LambdaComparer<T>(equals, toHash);
        }

        public bool Equals([AllowNull] T x, [AllowNull] T y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (x == null && y == null) return true;
            if (x == null || y == null) return false;
            return _equals(x, y);
        }

        public int GetHashCode([DisallowNull] T obj)
        {
            if (_hashCode == null) return 0;
            return _hashCode(obj).GetHashCode();
        }
    }
}
