using System;

namespace Lab11v18
{
    public class RedisCache : CacheManager
    {
        public string Server { get; set; } = "localhost:6379";

        protected override void GenerateKey()
        {
            Key = "app:cache:" + Name.ToLower();
            Console.WriteLine("Ключ: " + Key);
        }

        protected override void Serialize()
        {
            SerializedData = "{\"key\":\"" + Key + "\",\"value\":\"" + Data + "\"}";
            Console.WriteLine("Серiалiзовано у JSON: " + SerializedData);
        }

        protected override void Store()
        {
            Console.WriteLine("SET " + Key + " на серверi " + Server);
        }

        protected override void LogOperation()
        {
            base.LogOperation();
            Console.WriteLine("Лог: вiдправлено команду на " + Server);
        }
    }
}