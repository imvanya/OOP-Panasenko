using System;
using System.Collections.Generic;
using IndependentWork7;
using Xunit;

namespace IndependentWork7.Tests
{
    public class CacheTests
    {
        [Fact]
        public void Count_NewCache_IsZero()
        {
            var cache = new Cache<string, int>(3);

            Assert.Equal(0, cache.Count);
        }

        [Fact]
        public void Add_NewItems_IncreasesCount()
        {
            var cache = new Cache<string, int>(3);

            cache.Add("a", 1);
            cache.Add("b", 2);

            Assert.Equal(2, cache.Count);
        }

        [Fact]
        public void Get_ExistingKey_ReturnsValue()
        {
            var cache = new Cache<string, int>(3);
            cache.Add("a", 10);

            int value = cache.Get("a");

            Assert.Equal(10, value);
        }

        [Fact]
        public void Get_MissingKey_ThrowsKeyNotFoundException()
        {
            var cache = new Cache<string, int>(3);

            Assert.Throws<KeyNotFoundException>(() => cache.Get("missing"));
        }

        [Fact]
        public void ContainsKey_ExistingKey_ReturnsTrue()
        {
            var cache = new Cache<string, int>(3);
            cache.Add("a", 1);

            Assert.True(cache.ContainsKey("a"));
        }

        [Fact]
        public void ContainsKey_MissingKey_ReturnsFalse()
        {
            var cache = new Cache<string, int>(3);

            Assert.False(cache.ContainsKey("a"));
        }

        [Fact]
        public void Add_OverCapacity_RemovesOldestItem()
        {
            var cache = new Cache<string, int>(2);
            cache.Add("first", 1);
            cache.Add("second", 2);

            cache.Add("third", 3);

            Assert.False(cache.ContainsKey("first"));
            Assert.True(cache.ContainsKey("second"));
            Assert.True(cache.ContainsKey("third"));
        }

        [Fact]
        public void Add_OverCapacity_CountStaysEqualToCapacity()
        {
            var cache = new Cache<int, string>(3);

            for (int i = 1; i <= 10; i++)
            {
                cache.Add(i, "value" + i);
            }

            Assert.Equal(3, cache.Count);
            Assert.True(cache.ContainsKey(10));
            Assert.False(cache.ContainsKey(7));
        }

        [Fact]
        public void Add_ExistingKey_UpdatesValueWithoutChangingCount()
        {
            var cache = new Cache<string, int>(3);
            cache.Add("a", 1);

            cache.Add("a", 99);

            Assert.Equal(1, cache.Count);
            Assert.Equal(99, cache.Get("a"));
        }

        [Fact]
        public void Add_ExistingKey_DoesNotChangeFifoOrder()
        {
            var cache = new Cache<string, int>(2);
            cache.Add("a", 1);
            cache.Add("b", 2);

            cache.Add("a", 100);   
            cache.Add("c", 3);     

            Assert.False(cache.ContainsKey("a"));
            Assert.True(cache.ContainsKey("b"));
            Assert.True(cache.ContainsKey("c"));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        public void Constructor_NonPositiveCapacity_Throws(int capacity)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Cache<string, int>(capacity));
        }
    }
}