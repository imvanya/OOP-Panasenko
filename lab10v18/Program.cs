using System;
using System.Collections.Generic;

namespace Lab10v18
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            List<IDataSource<int>> sources = new List<IDataSource<int>>();
            sources.Add(new DatabaseDataSource());
            sources.Add(new WebServiceDataSource());

            Console.WriteLine("Джерела даних (через інтерфейс)");
            foreach (IDataSource<int> source in sources)
            {
                IEnumerable<int> data = source.GetData();
                Console.WriteLine(source.GetType().Name + " повернув: " + string.Join(", ", data));
            }

            List<DataAggregator> aggregators = new List<DataAggregator>();
            aggregators.Add(new SumAggregator(sources[0]));
            aggregators.Add(new AverageAggregator(sources[1]));
            aggregators.Add(new SumAggregator(sources[1]));

            Console.WriteLine();
            Console.WriteLine("Агрегатори (через абстрактний клас)");
            foreach (DataAggregator aggregator in aggregators)
            {
                aggregator.FetchData();
                aggregator.Aggregate();
                aggregator.DisplayAggregatedData();
                Console.WriteLine();
            }
        }
    }
}