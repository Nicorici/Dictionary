using System;
using System.Collections.Generic;
using System.Text;

namespace Dictionary
{
    public class DictionaryElement<TKey, TValue>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }
        public int Next { get; set; } 

        public DictionaryElement(TKey key, TValue value,int next=-1)
        {
            Key = key;
            Value = value;
            Next = next;
        }
    }
}
