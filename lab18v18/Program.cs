using System;

namespace Lab18v18
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            EventManagementService service = new EventManagementService();
            service.CreateEvent("Workshop", 3);

            Console.WriteLine("RegisterParticipant (виняток)");
            string[] people = { "Oleh", "Maria", "Ihor", "Sofia" };
            foreach (string person in people)
            {
                try
                {
                    service.RegisterParticipant("Workshop", person);
                }
                catch (EventCapacityExceededException ex)
                {
                    Console.WriteLine("  Помилка: " + ex.Message);
                    Console.WriteLine("  Подія: " + ex.EventName + ", місць: " + ex.MaxCapacity);
                }
            }

            try
            {
                service.RegisterParticipant("Concert", "Oleh");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("  Помилка: " + ex.Message);
            }

            Console.WriteLine();
            Console.WriteLine("CancelRegistration (Result)");
            PrintResult("Скасування Maria", service.CancelRegistration("Workshop", "Maria"));
            PrintResult("Повторне скасування Maria", service.CancelRegistration("Workshop", "Maria"));
            PrintResult("Скасування на події Concert", service.CancelRegistration("Concert", "Oleh"));

            Console.WriteLine();
            Console.WriteLine("Після скасування місце звільнилось");
            try
            {
                service.RegisterParticipant("Workshop", "Sofia");
            }
            catch (EventCapacityExceededException ex)
            {
                Console.WriteLine("  Помилка: " + ex.Message);
            }
            Console.WriteLine("Учасників: " + service.GetParticipantsCount("Workshop"));
        }

        static void PrintResult(string title, Result<bool> result)
        {
            if (result.IsSuccess)
            {
                Console.WriteLine("  " + title + ": успіх (" + result.Value + ")");
            }
            else
            {
                Console.WriteLine("  " + title + ": невдача - " + result.Error);
            }
        }
    }
}