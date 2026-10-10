using System;
using System.Collections.Generic;

namespace Lab6v18
{
    public class Container
    {
        private int _capacity;
        private string _material;
        private List<string> _items = new List<string>();

        public int Capacity
        {
            get { return _capacity; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Мiсткiсть має бути бiльшою за нуль");
                }
                _capacity = value;
            }
        }

        public string Material
        {
            get { return _material; }
            set { _material = value; }
        }

        public int Count
        {
            get { return _items.Count; }
        }

        public Container(int capacity, string material)
        {
            Capacity = capacity;
            Material = material;
        }

        public virtual void StoreItem(string item)
        {
            if (_items.Count >= _capacity)
            {
                Console.WriteLine("[Контейнер] Немає мiсця для: " + item);
                return;
            }
            _items.Add(item);
            Console.WriteLine("[Контейнер] Збережено: " + item + " (" + _items.Count + "/" + _capacity + ")");
        }

        public string GetContainerType()
        {
            return "Контейнер";
        }
    }
}