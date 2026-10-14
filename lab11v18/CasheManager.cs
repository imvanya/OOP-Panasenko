using System;

namespace Lab11v18
{
    public abstract class CacheManager
    {
        public string Name { get; private set; }
        public string Data { get; private set; }
        public string Key { get; protected set; }
        public string SerializedData { get; protected set; }
        public int LifetimeMinutes { get; protected set; } = 10;
        public int OperationsCount { get; private set; }

        public void Cache(string name, string data)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Назва не може бути порожньою");
            }
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

            Name = name;
            Data = data;

            Console.WriteLine("=== " + GetType().Name + " ===");
            GenerateKey();
            CheckExpiration();
            Serialize();
            Store();
            OperationsCount++;
            LogOperation();
            Console.WriteLine();
        }

        protected abstract void GenerateKey();
        protected abstract void Serialize();
        protected abstract void Store();

        protected virtual void CheckExpiration()
        {
            Console.WriteLine("Термiн дiї запису: " + LifetimeMinutes + " хв");
        }

        protected virtual void LogOperation()
        {
            Console.WriteLine("Лог: запис \"" + Key + "\" збережено, операцiй: " + OperationsCount);
        }
    }
}