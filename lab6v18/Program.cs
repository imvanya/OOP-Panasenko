using System;

namespace Lab6v18
{
    class Program
    {
        static void Main(string[] args)
        {
            Container container = new Container(2, "пластик");
            Box box = new Box(3, "картон", false);
            Crate crate = new Crate(2, "дерево", true);

            Console.WriteLine("Полiморфiзм (virtual / override)");
            Container[] all = { container, box, crate };
            foreach (Container c in all)
            {
                c.StoreItem("книга");
            }

            Console.WriteLine();
            Console.WriteLine("Власнi методи");
            box.CloseBox();
            box.StoreItem("ручка");  
            crate.StackCrate();

            Console.WriteLine();
            Console.WriteLine("Переповнення (логiка базового класу)");
            container.StoreItem("зошит");
            container.StoreItem("лiнiйка");

            Console.WriteLine();
            Console.WriteLine("override проти new");
            Container boxAsContainer = box;  

            Console.WriteLine("box.GetContainerType(): " + box.GetContainerType());
            Console.WriteLine("boxAsContainer.GetContainerType(): " + boxAsContainer.GetContainerType());

            Box openBox = new Box(3, "картон", false);
            Container openBoxAsContainer = openBox;
            Console.WriteLine("Виклик StoreItem через посилання Container на об'єкт Box:");
            openBoxAsContainer.StoreItem("олiвець");
        }
    }
}