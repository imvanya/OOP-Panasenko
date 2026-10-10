using System;
using System.Collections.Generic;

namespace Lab5v18
{
    public class Inventory
    {
        private Dictionary<string, int> _items = new Dictionary<string, int>();

        public int Count
        {
            get { return _items.Count; }
        }

        public int this[string itemName]
        {
            get
            {
                if (itemName == null)
                {
                    throw new ArgumentNullException("itemName");
                }
                if (_items.ContainsKey(itemName))
                {
                    return _items[itemName];
                }
                return 0;
            }
            set
            {
                if (itemName == null)
                {
                    throw new ArgumentNullException("itemName");
                }
                if (value < 0)
                {
                    throw new ArgumentException("Кiлькiсть не може бути вiд'ємною");
                }
                if (value == 0)
                {
                    _items.Remove(itemName);
                }
                else
                {
                    _items[itemName] = value;
                }
            }
        }

        public void AddItem(string item, int quantity)
        {
            if (quantity <= 0)
            {
                throw new ArgumentException("Кiлькiсть має бути бiльшою за нуль");
            }
            this[item] = this[item] + quantity;
        }

        public static Inventory operator +(Inventory a, Inventory b)
        {
            Inventory result = new Inventory();
            foreach (KeyValuePair<string, int> pair in a._items)
            {
                result._items[pair.Key] = pair.Value;
            }
            foreach (KeyValuePair<string, int> pair in b._items)
            {
                result.AddItem(pair.Key, pair.Value);
            }
            return result;
        }

        public static Inventory operator -(Inventory a, Inventory b)
        {
            Inventory result = new Inventory();
            foreach (KeyValuePair<string, int> pair in a._items)
            {
                int left = pair.Value - b[pair.Key];
                if (left > 0)
                {
                    result._items[pair.Key] = left;
                }
            }
            return result;
        }

        public static bool operator ==(Inventory a, Inventory b)
        {
            if (ReferenceEquals(a, b))
            {
                return true;
            }
            if (a is null || b is null)
            {
                return false;
            }
            if (a.Count != b.Count)
            {
                return false;
            }
            foreach (KeyValuePair<string, int> pair in a._items)
            {
                if (b[pair.Key] != pair.Value)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool operator !=(Inventory a, Inventory b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            Inventory other = obj as Inventory;
            if (other is null)
            {
                return false;
            }
            return this == other;
        }

        public override int GetHashCode()
        {
            int hash = 0;
            foreach (KeyValuePair<string, int> pair in _items)
            {
                hash ^= pair.Key.GetHashCode() * 31 + pair.Value;
            }
            return hash;
        }

        public override string ToString()
        {
            if (_items.Count == 0)
            {
                return "{порожньо}";
            }
            List<string> parts = new List<string>();
            foreach (KeyValuePair<string, int> pair in _items)
            {
                parts.Add(pair.Key + " x" + pair.Value);
            }
            return "{" + string.Join(", ", parts) + "}";
        }
    }
}