using System;
using System.Collections.Generic;

namespace Lab10v18
{
    public class WebServiceDataSource : IDataSource<int>
    {
        public string Url { get; set; } = "https://example.com/api/numbers";

        public IEnumerable<int> GetData()
        {
            Console.WriteLine("[Web] GET " + Url);
            return new List<int> { 5, 15, 25 };
        }
    }
}