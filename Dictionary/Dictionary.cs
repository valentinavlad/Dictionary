using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Dictionary
{
    class Dictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private int[] buckets;

        private int capacity;

        private int count;

        private Element[] elements;

        private int freeIndex;

        public Dictionary(int capacity)
        {
            this.capacity = capacity;
            buckets = new int[capacity];
            Array.Fill(buckets, -1);
            elements = new Element[capacity];
            freeIndex = -1;
        }

        public int Count
        {
            get => count;
            private set => count = value;
        }

        public bool IsReadOnly => false;

        public ICollection<TKey> Keys
        {
            
            get
            {
                var keys = new List<TKey>();
                foreach (var kvp in this)
                {
                    keys.Add(kvp.Key);
                }

                return keys;
            }
        }

        public ICollection<TValue> Values
        {
            get
            {
                var values = new List<TValue>();
                foreach (var kvp in this)
                {
                    values.Add(kvp.Value);
                }

                return values;
            }
        }

        public TValue this[TKey Key]
        {
            get
            {
                int i = FindElementIndex(Key);
                if (i >= 0 && i <= Count)
                {
                    return elements[i].Value;
                }

                return default(TValue);
            }
            set
            {
                int i = FindElementIndex(Key);
                if (i >= 0 && i <= Count)
                {
                    elements[i].Value = value;
                }
                else
                {
                    throw new IndexOutOfRangeException("index doesn't exists!");
                }
            }
        }

        public void Add(TKey Key, TValue Value)
        {
            ValidateKey(Key);
            int bucketIndex = GetHashKey(Key);
            int indexElem = 0;

            if (freeIndex != -1)
            {
                var tempFreeIndex = elements[freeIndex].Next;
                indexElem = freeIndex;
                freeIndex = tempFreeIndex;
            }
            else
            {
                indexElem = Count;
            }

            elements[indexElem].Key = Key;
            elements[indexElem].Value = Value;
            elements[indexElem].Next = buckets[bucketIndex];
            buckets[bucketIndex] = indexElem;
            Count++;
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        public void Clear()
        {
            if (Count > 0)
            {
                Array.Fill(buckets, -1);
                Array.Clear(elements, 0, Count);
                freeIndex = -1;
                count = 0;
            }
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            int i = FindElementIndex(item.Key);
            return (i >= 0 && elements[i].Value.Equals(item.Value));

        }

        public bool ContainsKey(TKey Key)
        {
            return FindElementIndex(Key) != -1;
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array is null!");
            }

            if (arrayIndex < 0 || arrayIndex > array.Length)
            {
                throw new ArgumentOutOfRangeException("index out of range!");

            }
            for (int i = 0; i < buckets.Length; i++)
            {
                for (int j = buckets[i]; j != -1; j = elements[j].Next)
                {
                    array[arrayIndex++] = new KeyValuePair<TKey, TValue>(elements[j].Key, elements[j].Value);
                }
            }
        }

        public int FindElementIndex(TKey Key)
        {
            return FindElementIndex(Key, out int _);
        }

        public int FindElementIndex(TKey Key, out int previousIndex)
        {
            CheckIfKeyIsNull(Key);

            previousIndex = -1;
            int bucketIndex = GetHashKey(Key);

            for (int j = buckets[bucketIndex]; j != -1; j = elements[j].Next)
            {
                if (elements[j].Key.Equals(Key))
                {
                    return j;
                }
                previousIndex = j;
            }

            return -1;
        }
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            for (int i = 0; i < buckets.Length; i++)
            {
                for (int j = buckets[i]; j != -1; j = elements[j].Next)
                {
                    yield return new KeyValuePair<TKey, TValue>(elements[j].Key, elements[j].Value);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool Remove(TKey Key)
        {
            int indexElementToRemove = FindElementIndex(Key);
            if (indexElementToRemove == -1)
            {
                return false;
            }

            int bucketIndex = GetHashKey(Key);
            if (buckets[bucketIndex] == indexElementToRemove)
            {
                buckets[bucketIndex] = elements[indexElementToRemove].Next;
            }
            else
            {
                RemoveElementNotFirstInBucket(Key, indexElementToRemove, bucketIndex);
            }

            UpdateFreeList(indexElementToRemove);
            ResetElementValuesToDefault(indexElementToRemove);
            Count--;
            return true;
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return Remove(item.Key);
        }

        public bool TryGetValue(TKey Key, [MaybeNullWhen(false)] out TValue Value)
        {
            CheckIfKeyIsNull(Key);
            int i = FindElementIndex(Key);
            if (i >= 0 && i < Count)
            {
                Value = elements[i].Value;
                return true;
            }
            Value = default(TValue);
            return false;
        }

        private static void CheckIfKeyIsNull(TKey Key)
        {
            if (Key == null)
            {
                throw new ArgumentNullException("Key is null");
            }
        }

        private int GetHashKey(TKey Key)
        {
            var hash = Math.Abs(Key.GetHashCode()) % this.capacity;
            return hash;
        }

        private void RemoveElementNotFirstInBucket(TKey Key, int indexElementToRemove, int bucketIndex)
        {
            int temp = elements[indexElementToRemove].Next;
            int previous;
            FindElementIndex(Key, out previous);
            for (int j = buckets[bucketIndex]; j != elements[indexElementToRemove].Next; j = elements[j].Next)
            {
                if (elements[j].Key.Equals(Key))
                {
                    elements[previous].Next = temp;
                }
            }
        }

        private void ResetElementValuesToDefault(int KeyPosition)
        {
            elements[KeyPosition].Value = default(TValue);
            elements[KeyPosition].Key = default(TKey);
        }

        private void UpdateFreeList(int indexElementToRemove)
        {
            elements[indexElementToRemove].Next = freeIndex;
            freeIndex = indexElementToRemove;
        }

        private void ValidateKey(TKey Key)
        {
            CheckIfKeyIsNull(Key);

            if (ContainsKey(Key))
            {
                throw new ArgumentException("An element with the same key already exists");
            }
        }

        public struct Element
        {
            public TKey Key { get; set; }
            public int Next { get; set; }
            public TValue Value { get; set; }
        }
    }
}
