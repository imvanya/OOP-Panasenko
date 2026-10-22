using System;
using System.Collections.Generic;

namespace Lab10v18
{
    public abstract class DataAggregator
    {
        protected List<int> Numbers = new List<int>();
        protected IDataSource<int> Source { get; private set; }

        public string Name { get; private set; }
        public double Result { get; protected set; }

        protected DataAggregator(string name, IDataSource<int> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source", "Джерело даних не задане");
            }
            Name = name;
            Source = source;
        }

        public abstract void FetchData();
        public abstract void Aggregate();

        public void DisplayAggregatedData()
        {
            Console.WriteLine(Name + " [" + Source.GetType().Name + "]: "
                + string.Join(", ", Numbers) + " => " + Result);
        }
    }
}