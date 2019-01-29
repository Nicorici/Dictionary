using System;
using System.Collections.Generic;
using System.Text;

namespace Dictionary
{
    public class DictionaryElement<TKey, TValue>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }
        public int Next { get; set; } = -1;

        public DictionaryElement(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }
}
