using System;

namespace DisposableExample
{
    public class MyMemoryStream : IDisposable
    {
        private byte[] _buffer;
        private bool _isOpen;
        private bool _disposed = false;

        public bool IsOpen => _isOpen;
        public int Capacity => _buffer?.Length ?? 0;

        public MyMemoryStream(int size = 1024)
        {
            _buffer = new byte[size];
            _isOpen = true; 
            Console.WriteLine($"[Конструктор] Видiлено ресурс: буфер на {size} байт. Потiк вiдкрито.");
        }

        public void Write(byte[] data)
        {
            if (_disposed || !_isOpen)
                throw new ObjectDisposedException(nameof(MyMemoryStream), "Не неможливо записувати у закритий потiк.");
            
            Console.WriteLine($"[Write] Записано {data.Length} байт у потiк пам'ятi.");
        }

        public byte[] Read()
        {
            if (_disposed || !_isOpen)
                throw new ObjectDisposedException(nameof(MyMemoryStream), "Неможливо читати iз закритого потоку.");
            
            Console.WriteLine("[Read] Читання даних з потоку пам'ятi.");
            return new byte[0]; 
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Console.WriteLine("[Dispose] Звiльнення керованих ресурсiв.");
                }

                if (_isOpen)
                {
                    Console.WriteLine("[Dispose] Звiльнення некерованих ресурсiв: буфер очищено, потiк закрито.");
                    _buffer = null;
                    _isOpen = false;
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this); 
        }

        ~MyMemoryStream()
        {
            Console.WriteLine("[Деструктор] Викликано фiналiзатор збирачем смiття (GC).");
            Dispose(false);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("1. Створення об’єкта з використанням using");
            using (var stream1 = new MyMemoryStream(512))
            {
                stream1.Write(new byte[] { 1, 2, 3, 4, 5 });
            } 
            
            Console.WriteLine("\n2. Створення об’єкта без using та явний виклик Dispose()");
            var stream2 = new MyMemoryStream(256);
            stream2.Read();
            stream2.Dispose(); 
            
            Console.WriteLine("\n3. Робота деструктора через GC.Collect()");
            CreateGarbageStream();
            
            Console.WriteLine("-> Виклик GC.Collect()...");
            GC.Collect();
            GC.WaitForPendingFinalizers(); 
            
            Console.WriteLine("\nПрограма успiшно завершила роботу.");
        }

        static void CreateGarbageStream()
        {
            var stream3 = new MyMemoryStream(2048);
            stream3.Write(new byte[] { 10, 20 });
        }
    }
}