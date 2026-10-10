using System;

namespace Lab5v18
{
    class Program
    {
        static void Main(string[] args)
        {
            Inventory bag = new Inventory();
            bag.AddItem("меч", 1);
            bag.AddItem("зiлля", 5);
            bag.AddItem("стрiла", 20);

            Inventory chest = new Inventory();
            chest.AddItem("зiлля", 3);
            chest.AddItem("щит", 1);

            Inventory bagCopy = new Inventory();
            bagCopy.AddItem("стрiла", 20);
            bagCopy.AddItem("меч", 1);
            bagCopy.AddItem("зiлля", 5);

            Console.WriteLine("bag: " + bag);
            Console.WriteLine("chest: " + chest);

            Console.WriteLine("bag[\"зiлля\"] = " + bag["зiлля"]);
            Console.WriteLine("bag[\"лук\"] = " + bag["лук"] + " (такого предмета нема)");

            bag["зiлля"] = 8;
            bag["лук"] = 1;
            Console.WriteLine("Пiсля змiн через iндексатор: " + bag);

            try
            {
                bag["меч"] = -2;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Помилка: " + ex.Message);
            }

            Inventory sum = bag + chest;
            Console.WriteLine("bag + chest: " + sum);

            Inventory diff = sum - chest;
            Console.WriteLine("(bag + chest) - chest: " + diff);

            // == та !=
            bag["зiлля"] = 5;
            bag["лук"] = 0;
            Console.WriteLine("Пiсля повернення старих значень: " + bag);
            Console.WriteLine("bag == bagCopy: " + (bag == bagCopy));
            Console.WriteLine("bag != chest: " + (bag != chest));
            Console.WriteLine("Хешi рiвних iнвентарiв однаковi: " + (bag.GetHashCode() == bagCopy.GetHashCode()));
        }
    }
}