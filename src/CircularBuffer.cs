using System;
using System.Collections;
using System.Collections.Generic;

namespace Geoffles.Collections
{
    /// <summary>
    /// A circular buffer backed by an array.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CircularBuffer<T> : IBuffer<T>, IEnumerable<T>
    {
        private class Enumerator : IEnumerator<T>
        {
            private readonly CircularBuffer<T> _buffer;
            private int i = -1;

            public Enumerator(CircularBuffer<T> buffer)
            {
                _buffer = buffer;
            }

            public T Current => Get();

            object IEnumerator.Current => Get();

            private T Get()
            {
                if (i == -1)
                {
                    throw new InvalidOperationException("Enumeration has not started. Call MoveNext.");
                }
                return _buffer[i];
            }

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                ++i;
                return i < _buffer._size;
            }

            public void Reset()
            {
                i = -1;
            }
        }

        private readonly T[] _buffer;
        private readonly int _size;

        private int _head;

        /// <summary>
        /// Initialize a new circular buffer with <c>size<c> elements.
        /// </summary>
        /// <param name="size">The number of elements to contain</param>
        /// <param name="initializer">(optional)Function to initialize the collection. Defaults null.</param>
        public CircularBuffer(int size, Func<int, T> initializer = null)
            : this(size)
        {
            int last = size - 1;
            for (int i = 0; i < _size; ++i)
            {
                Push(initializer(i));
            }
        }

        public CircularBuffer(int size)
        {
            _size = size;
            _head = 0;
            _buffer = new T[_size];
        }

        /// <summary>
        /// Insert a new element into the buffer. The oldest element will be overwritten
        /// </summary>
        /// <param name="item">The item to add</param>
        public void Push(T item)
        {
            _buffer[_head++] = item;
            if (_head >= _size)
            {
                _head = 0;
            }
        }

        /// <summary>
        /// Get the oldest element of the buffer
        /// </summary>
        public T Last()
        {
            return this[_size - 1];
        }

        public int Length
        {
            get { return _size; }
        }
        
        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator(this);
        }

        public T this[int i]
        {
            get
            {
                if (i >= _size || i < 0)
                {
                    throw new IndexOutOfRangeException();
                }
                int _i = (_head + i) % _size;
                return _buffer[_i];
            }
        }
    }
}
