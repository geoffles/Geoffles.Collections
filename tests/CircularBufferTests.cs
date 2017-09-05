using System;
using Xunit;
using Geoffles.Collections;

namespace tests
{
    public class CircularBufferTests
    {
        [Fact]
        public void CircularBuffer_Indexer_MustIndex()
        {
            var buffer = new CircularBuffer<int>(3, i => (i+1) * 2);

            Assert.Equal(2, buffer[0]);
            Assert.Equal(4, buffer[1]);
            Assert.Equal(6, buffer[2]);
        }

        [Fact]
        public void CircularBuffer_Enumerate_MustMatchArray()
        {
            var array = new int[] { 2, 4, 6 };
            var buffer = new CircularBuffer<int>(3, i => (i + 1) * 2);

            var ea = array.GetEnumerator();
            var eb = buffer.GetEnumerator();

            
            Assert.Throws<InvalidOperationException>(() => eb.Current);

            Assert.Equal(ea.MoveNext(), eb.MoveNext());
            Assert.Equal(2, eb.Current);

            Assert.Equal(ea.MoveNext(), eb.MoveNext());
            Assert.Equal(4, eb.Current);

            Assert.Equal(ea.MoveNext(), eb.MoveNext());
            Assert.Equal(6, eb.Current);

            Assert.Equal(ea.MoveNext(), eb.MoveNext());

            Assert.False(ea.MoveNext());
            Assert.False(eb.MoveNext());

            ea.Reset();
            eb.Reset();

            Assert.Throws<InvalidOperationException>(() => eb.Current);

            Assert.Equal(ea.MoveNext(), eb.MoveNext());
            Assert.Equal(2, eb.Current);
        }

        [Fact]
        public void CircularBuffer_Indexer_MustProtectRange()
        {
            var buffer = new CircularBuffer<int>(3);

            Assert.Throws<IndexOutOfRangeException>(() => buffer[3]);
            Assert.Throws<IndexOutOfRangeException>(() => buffer[-1]);
        }

        [Fact]
        public void CircularBuffer_Push_MustOverwriteOldest()
        {
            var buffer = new CircularBuffer<int>(3, i => (i + 1) * 2);

            buffer.Push(8);

            Assert.Equal(4, buffer[0]);
            Assert.Equal(6, buffer[1]);
            Assert.Equal(8, buffer[2]);
        }

        [Fact]
        public void CircularBuffer_Push_MustBeCircular()
        {
            var buffer = new CircularBuffer<int>(3, n => (n + 1) * 2);

            for (int i = 0; i < 10; i++)
            {
                var newVal = (i + 4) * 2;

                buffer.Push(newVal);

                Assert.Equal(newVal - 4, buffer[0]);
                Assert.Equal(newVal - 2, buffer[1]);
                Assert.Equal(newVal, buffer[2]);
            }
        }
    }
}
