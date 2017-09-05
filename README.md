# Geoffles.Collections

This is a collections library with specific collections not available the standard framework.

## CircularBuffer<T>

The circular buffer is a collection holding a fixed number of elements. The youngest element overwrites the oldest element when the buffer is full. The Collection exposes the `IEnumerable<T>` interface and behaves identically to an array enumerator.

### Usage

```
var buffer = new CircularBuffer<int>(4, i => 0); //Force initialize all to zero

Console.WriteLine(buffer[1]); // 0

foreach(int e in buffer)
{
    Console.WriteLine(e); // 0 \ 0 \ 0 \ 0
}

buffer.Push(2);
buffer.Push(2);

//using System.Linq
int ave = buffer.Average(); // == 1

buffer.Push(4);
buffer.Push(4);

ave = buffer.Average(); // == 3

buffer.Push(8);
buffer.Push(8));

ave = buffer.Sum(); // == 24
```

### Applications

This collection is useful for windowing functions.

### Remarks

The collection is back by an array for fast access and a fixed memory footprint. The array is allocated upfront.

The collection implements an indexed `Last()` implementation for fast access in LinQ like queries.

# License

This software is licensed with the BSD license. Please read `LICENSE` for more information.