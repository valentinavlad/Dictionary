using System;
using Xunit;

namespace Dictionary
{
    public class DictionaryTests
    {
        [Fact]
        public void CheckIfAddValueWithExistingKeyThrowsError()
        {
            var dictionary = new Dictionary<int, string>(5);
            dictionary.Add(1, "a");
            dictionary.Add(2, "b");
            dictionary.Add(10, "c");
            dictionary.Add(7, "d");
            dictionary.Add(12, "e");

            var ex = Assert.Throws<ArgumentException>(() => dictionary.Add(7, "etys"));
            Assert.Equal("An element with the same key already exists", ex.Message);
        }

        [Fact]
        public void CheckIfContainsKeysReturnProperOutput()
        {
            var dictionary = new Dictionary<int, string>(5);
            dictionary.Add(1, "a");
            dictionary.Add(2, "b");
            dictionary.Add(10, "c");
            dictionary.Add(7, "d");
            dictionary.Add(12, "e");
   
            Assert.True(dictionary.ContainsKey(7));
            Assert.False(dictionary.ContainsKey(22));
            Assert.Equal(5, dictionary.Count);
        }
        [Fact]
        public void CheckIfGetPropReturnProperOutput()
        {
            var dictionary = new Dictionary<int, string>(5);
            dictionary.Add(1, "a");
            dictionary.Add(2, "b");
            dictionary.Add(10, "c");
            dictionary.Add(7, "d");
            dictionary.Add(12, "e");
            dictionary[7] = "test";
            dictionary[12] = "cheie12";
            Assert.Equal("test", dictionary[7]);
            Assert.Equal("cheie12", dictionary[12]);
        }

        [Fact]
        public void CheckIfRemove2ReturnProperOutput()
        {
            var dictionary = new Dictionary<int, string>(5);
            dictionary.Add(1, "a");
            dictionary.Add(2, "b");
            dictionary.Add(10, "c");
            dictionary.Add(7, "d");
            dictionary.Add(12, "e");

            Assert.True(dictionary.Remove(1));

        }

        [Fact]
        public void CheckIfRemove3ReturnProperOutput()
        {
            var dictionary = new Dictionary<int, string>(5);
            dictionary.Add(1, "a");
            dictionary.Add(2, "b");
            dictionary.Add(10, "c");
            dictionary.Add(7, "d");
            dictionary.Add(12, "e");

            Assert.True(dictionary.Remove(7));
            Assert.True(dictionary.Remove(1));
        }

        [Fact]
        public void CheckIfRemove4ReturnProperOutput()
        {
            var dictionary = new Dictionary<int, string>(5);
            dictionary.Add(1, "a");
            dictionary.Add(2, "b");
            dictionary.Add(10, "c");
            dictionary.Add(7, "d");
            dictionary.Add(12, "e");

            Assert.True(dictionary.Remove(1));

        }

        [Fact]
        public void CheckIfRemove5ReturnProperOutput()
        {
            var dictionary = new Dictionary<int, string>(5);
            dictionary.Add(1, "a");
            dictionary.Add(2, "b");
            dictionary.Add(10, "c");
            dictionary.Add(7, "d");
            dictionary.Add(12, "e");
            dictionary.Remove(7);
            dictionary.Remove(1);

            dictionary.Add(17, "f");
            dictionary.Add(3, "g");
            Assert.Equal("f", dictionary[17]);
            Assert.Equal(5, dictionary.Count);
        }

        [Fact]
        public void CheckIfRemoveReturnProperOutput()
        {
            var dictionary = new Dictionary<int, string>(5);
            dictionary.Add(1, "a");
            dictionary.Add(2, "b");
            dictionary.Add(10, "c");
            dictionary.Add(7, "d");
            dictionary.Add(12, "e");

            Assert.True(dictionary.Remove(12));       
        }

        [Fact]
        public void CheckIfRemoveThrowsError()
        {
            var dictionary = new Dictionary<int, string>(5);
            dictionary.Add(1, "a");
            dictionary.Add(2, "b");
            dictionary.Add(10, "c");
            dictionary.Add(7, "d");
            dictionary.Add(12, "e");
            dictionary.Remove(12);

            var ex = Assert.Throws<IndexOutOfRangeException>(() => dictionary[12] = "test");
            Assert.Equal("index doesn't exists!", ex.Message);

        }

        [Fact]
        public void GetKeys()
        {
            var dictionary = new Dictionary<int, string>(5);
            dictionary.Add(1, "a");
            dictionary.Add(2, "b");
            dictionary.Add(10, "c");
            dictionary.Add(7, "d");
            dictionary.Add(12, "e");
            var keys = dictionary.Keys;
            //Assert.Equal()
        }
    }
}
