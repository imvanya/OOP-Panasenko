using System;
using System.Net.Http;

namespace Lab17v18
{
    public class NetworkClient
    {
        private int _calls;

        public int FailuresBeforeSuccess { get; set; } = 2;

        public bool UploadCache(string url, string cacheData)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentException("URL не може бути порожнім");
            }
            if (cacheData == null)
            {
                throw new ArgumentNullException("cacheData");
            }

            _calls++;
            if (_calls <= FailuresBeforeSuccess)
            {
                throw new HttpRequestException("Сервер " + url + " недоступний (виклик " + _calls + ")");
            }

            Console.WriteLine("  Кеш відправлено на " + url);
            return true;
        }
    }
}