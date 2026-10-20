using System;

namespace Lab9v18
{
    public abstract class Encoder
    {
        public string Name { get; private set; }

        protected Encoder(string name)
        {
            Name = name;
        }

        public abstract string Encode(string data);

        protected static void CheckData(string data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data", "Дані для кодування не задані (null)");
            }
        }
    }
}