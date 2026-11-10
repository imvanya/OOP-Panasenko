using System;
using System.Collections.Generic;

namespace Lab14v18
{
    public class Monitor
    {
        private HashSet<string> _services = new HashSet<string>();

        private Queue<string> _checkQueue = new Queue<string>();

        private Stack<ServiceStatus> _history = new Stack<ServiceStatus>();

        private Random _random = new Random(1);

        public int ServicesCount
        {
            get { return _services.Count; }
        }

        public int QueueCount
        {
            get { return _checkQueue.Count; }
        }

        public int HistoryCount
        {
            get { return _history.Count; }
        }


        public bool AddService(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Назва сервісу порожня");
            }
            return _services.Add(name);
        }

        public bool RemoveService(string name)
        {
            return _services.Remove(name);
        }

        public bool IsMonitored(string name)
        {
            return _services.Contains(name);
        }

        public void PrintServices()
        {
            Console.WriteLine("Сервіси (" + _services.Count + "): " + string.Join(", ", _services));
        }


        public void ScheduleCheck(string name)
        {
            if (!_services.Contains(name))
            {
                Console.WriteLine("  Сервіс \"" + name + "\" не відстежується, перевірку не додано");
                return;
            }
            _checkQueue.Enqueue(name);
            Console.WriteLine("  У чергу додано: " + name);
        }

        public string PeekNextCheck()
        {
            if (_checkQueue.Count == 0)
            {
                return null;
            }
            return _checkQueue.Peek();
        }


        public ServiceStatus ProcessNextCheck()
        {
            if (_checkQueue.Count == 0)
            {
                Console.WriteLine("  Черга порожня");
                return null;
            }

            string name = _checkQueue.Dequeue();
            bool online = _random.Next(100) > 25;           
            int response = online ? _random.Next(20, 300) : 0;

            ServiceStatus status = new ServiceStatus(name, online, response);
            _history.Push(status);
            Console.WriteLine("  Перевірено: " + status);
            return status;
        }


        public ServiceStatus PeekLastStatus()
        {
            if (_history.Count == 0)
            {
                return null;
            }
            return _history.Peek();
        }

        public ServiceStatus UndoLastStatus()
        {
            if (_history.Count == 0)
            {
                Console.WriteLine("  Історія порожня");
                return null;
            }
            ServiceStatus last = _history.Pop();
            Console.WriteLine("  Видалено з історії: " + last);
            return last;
        }

        public void PrintHistory()
        {
            Console.WriteLine("Історія (від найновішого до найстарішого):");
            foreach (ServiceStatus status in _history)
            {
                Console.WriteLine("  " + status);
            }
        }
    }
}