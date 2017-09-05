using System.Collections.Generic;

namespace Geoffles.Collections
{
    public interface IBuffer<T> : IEnumerable<T>
    {
        void Push(T item);
        int Length { get; }
        T this[int i] { get; }
        T Last();
    }
}