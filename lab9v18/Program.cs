using System;
using System.Collections.Generic;

namespace Lab9v18
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            List<Encoder> encoders = new List<Encoder>();
            encoders.Add(new Base64Encoder());
            encoders.Add(new URLEncoder());
            encoders.Add(new HTMLEncoder());

            EncoderService service = new EncoderService();

            Console.WriteLine("Звичайний запуск");
            service.EncodeAll(encoders, "a&b=c <b>test</b>");

            Console.WriteLine();
            Console.WriteLine("Навмисна помилка: null");
            service.EncodeAll(encoders, null);

            Console.WriteLine();
            Console.WriteLine("Навмисна помилка: задовгий рядок для Base64");
            service.EncodeAll(encoders, new string('x', 150));

            Console.WriteLine();
            Console.WriteLine("Успiшно: " + service.SuccessCount + ", помилок: " + service.ErrorCount);
        }
    }
}