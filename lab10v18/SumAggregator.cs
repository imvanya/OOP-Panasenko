using System;

namespace Lab10v18
{
    public class SumAggregator : DataAggregator
    {
        public SumAggregator(IDataSource<int> source)
            : base("Сума", source)
        {
        }

        public override void FetchData()
        {
            Numbers.Clear();
            Numbers.AddRange(Source.GetData());
        }

        public override void Aggregate()
        {
            long sum = 0;
            foreach (int n in Numbers)
            {
                sum += n;
            }
            Result = sum;
        }
    }
}