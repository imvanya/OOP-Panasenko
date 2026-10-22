using System;
using System.Collections.Generic;

namespace Lab10v18
{
    public class DatabaseDataSource : IDataSource<int>
    {
        private List<int> _rows = new List<int> { 10, 20, 30, 40 };

        public IEnumerable<int> GetData()
        {
            Console.WriteLine("[БД] Виконуємо запит SELECT, рядків: " + _rows.Count);
            return new List<int>(_rows);
        }
    }
}