using System;
using System.IO;

namespace Lab17v18
{
    public class FileProcessor
    {
        private int _calls;

        public int FailuresBeforeSuccess { get; set; } = 4;

        public void SaveCache(string path, string cacheData)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("Шлях не може бути порожнім");
            }
            if (cacheData == null)
            {
                throw new ArgumentNullException("cacheData");
            }

            _calls++;
            if (_calls <= FailuresBeforeSuccess)
            {
                throw new IOException("Файл \"" + path + "\" зайнятий іншим процесом (виклик " + _calls + ")");
            }

            Console.WriteLine("  Кеш збережено у \"" + path + "\" (" + cacheData.Length + " символів)");
        }

        public void Reset()
        {
            _calls = 0;
        }
    }
}