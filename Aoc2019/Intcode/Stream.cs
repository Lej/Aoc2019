using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Aoc2019.Intcode
{
    public class Stream<T> : IEnumerable<T>
    {
        public int Count => _queue.Count;

        private BlockingCollection<T> _queue = new BlockingCollection<T>(new ConcurrentQueue<T>());

        public void Write(T item)
        {
            _queue.Add(item);
        }

        public T Read()
        {
            return _queue.Take();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)_queue).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<T>)_queue).GetEnumerator();
        }
    }
}
