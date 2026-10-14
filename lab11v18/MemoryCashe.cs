using System;
using System.Collections.Generic;

namespace Lab11v18
{
    public class MemoryCache : CacheManager
    {
        private Dictionary<string, string> _storage = new Dictionary<string, string>();

        public int StoredCount
        {
            get { return _storage.Count; }
        }

        protected override void GenerateKey()
        {
            Key = "mem_" + Name.ToLower();
            Console.WriteLine("Ключ: " + Key);
        }

        protected override void CheckExpiration()
        {
            LifetimeMinutes = 5;
            Console.WriteLine("Кеш у пам'ятi, термiн дiї скорочено до " + LifetimeMinutes + " хв");
        }

        protected override void Serialize()
        {
            SerializedData = Data;
            Console.WriteLine("Серiалiзацiя не потрiбна, зберiгаємо рядок як є");
        }

        protected override void Store()
        {
            _storage[Key] = SerializedData;
            Console.WriteLine("Збережено у словник, записiв у пам'ятi: " + StoredCount);
        }
    }
}