using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Dictionary
{
    public class Dictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private int[] bucketList;
        private DictionaryElement<TKey, TValue>[] elements;
        private int removedIndex = -1;

        public Dictionary(int size = 5)
        {
            this.elements = new DictionaryElement<TKey, TValue>[size];
            this.bucketList = new int[size];
            Array.Fill(bucketList, -1);
        }

        public TValue this[TKey key]
        {
            get
            {
                if (key == null)
                    throw new ArgumentNullException();
                int index = IndexOf(key);
                if (index == -1)
                    throw new KeyNotFoundException();
                return elements[index].Value;
            }
            set
            {
                if (key == null)
                    throw new ArgumentNullException();
                int index = IndexOf(key);
                if (index == -1)
                    Add(key, value);
                else
                    elements[index].Value = value;
            }
        }

        public ICollection<TKey> Keys
        {
            get => GetKeys(elements);
        }

        public ICollection<TValue> Values
        {
            get => GetValues(elements);
        }

        public int Count { get; set; } = 0;

        public bool IsReadOnly => false;

        public void Add(TKey key, TValue value)
        {
            if (key == null)
                throw new ArgumentNullException("The key of a Dictionary element cannot be null.");
            int bucketIndex = GetBucketIndex(key);
            int nextIndex = bucketList[bucketIndex];
            int index = (removedIndex == -1) ? Count : removedIndex;

            if (nextIndex != -1 && ContainsKey(key))
                throw new ArgumentException("An element with the same key cannot be added in the Dictionary.");

            elements[index] = new DictionaryElement<TKey, TValue>(key, value, nextIndex);
            bucketList[bucketIndex] = index;
            if (removedIndex != -1) removedIndex = elements[removedIndex].Next;
            Count++;
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        public void Clear()
        {
            elements = new DictionaryElement<TKey, TValue>[bucketList.Length];
            Array.Fill<int>(bucketList, -1, 0, bucketList.Length);
            Count = 0;
        }

        public bool Contains(KeyValuePair<TKey, TValue> element)
        {
            if (element.Key == null)
                throw new ArgumentNullException();
            int index = IndexOf(element.Key);
            return index != -1 && elements[index].Value.Equals(element.Value);
        }

        public bool ContainsKey(TKey key)
        {
            if (key == null)
                throw new ArgumentNullException();
            return IndexOf(key) != -1;
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            if (arrayIndex + Count > array.Length)
                throw new ArgumentException("The number of elements needed to be copied is greater than " +
                                            " the availbale space from index to the end of the array");

            for (int i = 0; i < Count; i++, arrayIndex++)
                array[arrayIndex] = new KeyValuePair<TKey, TValue>(elements[i].Key, elements[i].Value);
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
            {
                if (ItemIsRemoved(elements[i].Key))
                { i--; continue; }
                yield return new KeyValuePair<TKey, TValue>(elements[i].Key, elements[i].Value);

            }
        }

        public bool Remove(TKey key)
        {
            if (key == null)
                throw new ArgumentNullException();
            if (!ContainsKey(key))
                return false;

            int bucketIndex = GetBucketIndex(key);
            int indexOfKey = IndexOf(key);
            int bucketFirst = bucketList[bucketIndex];
            var currentElement = elements[bucketFirst];

            if (currentElement.Key.Equals(key))
            {
                bucketList[bucketIndex] = currentElement.Next;
                elements[indexOfKey].Next = removedIndex;
                removedIndex = indexOfKey;
                Count--;
                return true;
            }

            while (currentElement.Next != indexOfKey)
                currentElement = elements[currentElement.Next];
            currentElement.Next = elements[indexOfKey].Next;
            elements[indexOfKey].Next = removedIndex;
            removedIndex = indexOfKey;
            Count--;
            return true;
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            int index = IndexOf(item.Key);
            return index == -1 || !elements[index].Value.Equals(item.Value) ? false : Remove(item.Key);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            if (key == null)
                throw new ArgumentException();
            var keyExists = IndexOf(key) != -1;
            value = default(TValue);
            if (keyExists)
                value = elements[IndexOf(key)].Value;
            return keyExists;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private int GetBucketIndex(TKey key)
        {
            return Math.Abs(key.GetHashCode() % bucketList.Length);
        }

        private int IndexOf(TKey key)
        {
            int bucketIndex = GetBucketIndex(key);
            if (bucketList[bucketIndex] == -1)
                return -1;
            var current = elements[bucketList[bucketIndex]];
            if (current.Key.Equals(key))
                return bucketList[bucketIndex];
            while (current.Next > -1)
            {
                if (elements[current.Next].Key.Equals(key))
                    return current.Next;
                current = elements[current.Next];
            }
            return -1;
        }

        private List<TKey> GetKeys(DictionaryElement<TKey, TValue>[] elements)
        {
            var list = new List<TKey>();
            for (int i = 0; i < Count; i++)
            {
                if (!ItemIsRemoved(elements[i].Key))
                { i--; continue; }
                list.Add(elements[i].Key);
            }
            return list;
        }

        private List<TValue> GetValues(DictionaryElement<TKey, TValue>[] elements)
        {
            var list = new List<TValue>();
            for (int i = 0; i < Count; i++)
            {
                if (ItemIsRemoved(elements[i].Key))
                { i--; continue; }
                list.Add(elements[i].Value);
            }
            return list;
        }

        private bool ItemIsRemoved(TKey key)
        {
            if (removedIndex == -1)
                return false;
            for (var current = removedIndex; current > -1; current = elements[current].Next)
            {
                if (elements[current].Key.Equals(key))
                    return true;
            }
            return false;
        }

        private void AddToRemovedAndDecrementCount()
        {

        }

    }
}
