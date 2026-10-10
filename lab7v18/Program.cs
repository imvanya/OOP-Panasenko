using System;

namespace Lab7v18
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game("Базова гра");
            Game action = new ActionGame("Doom Run", 12);   
            Game puzzle = new PuzzleGame("Tetris Mix", 30); 

            Console.WriteLine("Виклик через посилання базового класу Game");
            game.Start();
            action.Start();   
            puzzle.Start();   

            Console.WriteLine();
            Console.WriteLine("Виклик через посилання похiдних класiв (з приведенням)");
            ((ActionGame)action).Start();   
            ((PuzzleGame)puzzle).Start();   

            Console.WriteLine();
            Console.WriteLine("Пояснення");
            Console.WriteLine("ActionGame (override): тип об'єкта визначає, який метод виконається.");
            Console.WriteLine("PuzzleGame (new): тип посилання визначає, який метод виконається.");
        }
    }
}