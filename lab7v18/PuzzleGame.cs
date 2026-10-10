using System;

namespace Lab7v18
{
    public class PuzzleGame : Game
    {
        private int _levels;

        public int Levels
        {
            get { return _levels; }
            set { _levels = value; }
        }

        public PuzzleGame(string title, int levels)
            : base(title)
        {
            Levels = levels;
        }

        public new void Start()
        {
            Console.WriteLine("[PuzzleGame] Розкладаємо головоломку \"" + Title + "\", рiвнiв: " + Levels);
        }
    }
}