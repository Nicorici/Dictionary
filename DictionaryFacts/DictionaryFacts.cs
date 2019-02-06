using System;
using System.Collections.Generic;
using Xunit;

namespace Dictionary.Tests
{
    public class DictioanaryFacts
    {
        [Fact]
        public void AddAnElementInAnEmptyDictionary()
        {
            var dictionary = new Dictionary<int, string>
            {
                { 0, "firstElement" }
            };
            var obj = new KeyValuePair<int, string>(0, "firstElement");
            Assert.Contains(obj, dictionary);
        }

        [Fact]
        public void AddMultipleElementsInAnEmptyDictionary()
        {
            var dictionary = new Dictionary<int, string>
            {
                { 0, "firstElement" },
                { 1, "secondElement" },
                { 2, "thirdElement" },
                { 3, "forthElement" },
                { 4, "fifthElement" }
            };
            var element = new KeyValuePair<int, string>(2, "thirdElement");
            Assert.Contains(element, dictionary);
        }

        [Fact]
        public void AddElementsWithTheSameHashCode()
        {
            var dictionary = new Dictionary<int, string>
            {
                { 3, "firstElement" },
                { 13, "secondElement" }
            };
            Assert.True(dictionary.ContainsKey(3));
            Assert.True(dictionary.ContainsKey(13));
        }

        [Fact]
        public void AddElementBeyondTheCapacityOfTheArray()
        {
            var dictionary = new Dictionary<int, string>
            {
                { 0, "firstElement" },
                { 1, "secondElement" },
                { 2, "thirdElement" },
                { 3, "forthElement" },
                { 4, "fifthElement" }
            };
            Assert.Throws<IndexOutOfRangeException>(() => dictionary.Add(5, "sixthElemnt"));
        }

        [Fact]
        public void AddElementsWithTheSameHashCodeInASingleBucket()
        {
            var dictionary = new Dictionary<int, string>
            {
                { 3, "firstElement" },
                { 13, "secondElement" },
                { 23, "thirdElement" },
                { 33, "forthElement" },
                { 43, "fifthElement" }
            };
            Assert.True(dictionary.ContainsKey(3));
        }

        [Fact]
        public void AddElementsWithTheSameKeyInTheDictionary()
        {
            var dictionary = new Dictionary<int, string>
            {
                { 3, "firstElement" }
            };
            Assert.Throws<ArgumentException>(() => dictionary.Add(3, "secondElement"));
        }

        [Fact]
        public void AddAnElementWithANullKey()
        {
            var dictionary = new Dictionary<string, string>
            {
                { "0", "firstElement" }
            };
            Assert.Throws<ArgumentNullException>(() => dictionary.Add(null, "secondElement"));
        }

        [Fact]
        public void AddKeyValuePairsElements()
        {
            var dictionary = new Dictionary<int, int>
            {
                new KeyValuePair<int, int>(0, 0),
                new KeyValuePair<int, int>(1, 1)
            };
            Assert.True(dictionary.ContainsKey(0));
            Assert.True(dictionary.ContainsKey(1));
            Assert.False(dictionary.ContainsKey(2));
        }

        [Fact]
        public void GetTheDataCollectionValues()
        {
            var dictionary = new Dictionary<int, int>
            {
                { 1, 1 },
                { 2, 2 },
                { 3, 3 }
            };
            Assert.True(dictionary.ContainsKey(2));
        }

        [Fact]
        public void TryGettingAValuefromTheDictionaryWhenTheKeyIsNotFound()
        {
            var dictionary = new Dictionary<int, int>
            {
                { 1, 1 },
                { 2, 2 }
            };
            Assert.Throws<KeyNotFoundException>(() => dictionary[4]);
        }

        [Fact]
        public void TryGettingAValuefromTheDictionaryWhenTheKeyIsNull()
        {
            var dictionary = new Dictionary<object, int>
            {
                { 1, 1 },
                { 2, 2 }
            };
            Assert.Throws<ArgumentNullException>(() => dictionary[null]);
        }

        [Fact]
        public void TrySettingAValuefromTheDictionaryWhenTheKeyIsNull()
        {
            var dictionary = new Dictionary<object, int>
            {
                { 1, 1 },
                { 2, 2 }
            };
            Assert.Throws<ArgumentNullException>(() => dictionary[null]);
        }

        [Fact]
        public void TrySettingAValueFromTheDictionaryWhenTheKeyIsNotFound()
        {
            var dictionary = new Dictionary<int, int>
            {
                { 1, 1 },
                { 2, 2 }
            };
            dictionary[3] = 11;
            var element = new KeyValuePair<int, int>(3, 11);
            Assert.Contains(element, dictionary);
        }

        [Fact]
        public void GetAllTheKeysInTheDictionary()
        {
            var dictionary = new Dictionary<int, string>
            {
                { 1,"one" },
                { 2,"two" },
                { 3,"three" }
            };
            Assert.Contains(1, dictionary.Keys);
            Assert.Contains(2, dictionary.Keys);
            Assert.Contains(3, dictionary.Keys);
        }

        [Fact]
        public void GetAllTheValuesInTheDictionary()
        {
            var dictionary = new Dictionary<int, string>
            {
                { 1,"one" },
                { 2,"two" },
                { 3,"three" }
            };
            Assert.Contains("one", dictionary.Values);
            Assert.Contains("two", dictionary.Values);
            Assert.Contains("three", dictionary.Values);
        }

        [Fact]
        public void RemoveTheSingleElementOfADictionary()
        {
            var dictionary = new Dictionary<int, int>
            {
                {2,3}
            };
            Assert.True(dictionary.Remove(2));
            Assert.DoesNotContain(2, dictionary);
        }

        [Fact]
        public void RemoveAnElementWhenItsKeyIsNull()
        {
            var dictionary = new Dictionary<object, int>
            {
                {2,3}
            };
            Assert.Throws<ArgumentNullException>(() => dictionary.Remove(null));
        }

        [Fact]
        public void RemoveAnElementWhenItIsTheFirstElementOfItsBucket()
        {
            var dictionary = new Dictionary<int, string>
            {
                { 2,"third" },
                { 3,"other" },
                { 12,"second" },
                { 13,"other"},
                { 22,"first"}

            };
            dictionary.Remove(12);
            Assert.Contains(2, dictionary);
            Assert.False(dictionary.ContainsKey(12));
        }

        [Fact]
        public void RemoveAnElementWhichIsNotFoundInTheDictinary()
        {
            var dictionary = new Dictionary<int, int>
            {
                {2,3}
            };
            Assert.False(dictionary.Remove(4));
        }

        [Fact]
        public void TryRemovingAnElementWhichWasPreviousRemoved()
        {
            var dictionary = new Dictionary<int, int>
            {
                { 1,4 },
                { 2,4 }
            };
            Assert.True(dictionary.Remove(1));
            dictionary.Remove(2);
            Assert.False(dictionary.Remove(1));
        }

        [Fact]
        public void RemoveTheLastElementOfABucketContaingMultipleElements()
        {
            var dictionary = new Dictionary<int, int>
            {
                { 1,4  },
                { 11,4 },
                { 21,4 },
                { 31,4 }
            };
            Assert.Contains(31, dictionary);
            dictionary.Remove(31);
            Assert.DoesNotContain(31, dictionary);

        }

        [Fact]
        public void RemoveMultipleElementsOfTheSameBucket()
        {
            var dictionary = new Dictionary<int, int>
            {
                { 1,4  },
                { 2,4 },
                { 11,4 },
                { 12,4 },
                { 21,4 },
            };
            dictionary.Remove(21);
            dictionary.Remove(11);
            Assert.False(dictionary.ContainsKey(11));
            Assert.False(dictionary.ContainsKey(21));
            Assert.True(dictionary.ContainsKey(1));
        }

        [Fact]
        public void AddMaxElementsToTheDictionaryThanRemoveAndAddAfter()
        {
            var dictionary = new Dictionary<int, int>(3)
            {
                { 1,4  },
                { 2,4 },
                { 11,4 },
            };
            dictionary.Remove(1);
            dictionary.Remove(11);
            dictionary.Add(5, 3);
            dictionary.Add(3, 3);
            Assert.True(dictionary.ContainsKey(5));
            Assert.True(dictionary.ContainsKey(3));

        }
    }
}
