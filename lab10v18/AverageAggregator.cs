using System;

namespace Lab10v18
{
    public class AverageAggregator : DataAggregator
    {
        public AverageAggregator(IDataSource<int> source)
            : base("Середнє", source)
        {
        }

        public override void FetchData()
        {
            Numbers.Clear();
            Numbers.AddRange(Source.GetData());
        }

        public override void Aggregate()
        {
            if (Numbers.Count == 0)
            {
                Console.WriteLine("Немає даних для обчислення середнього");
                Result = 0;
                return;
            }

            double total = 0;
            foreach (int n in Numbers)
            {
                total += n;
            }
            Result = total / Numbers.Count;
        }
    }
}