using System;

namespace Lab7v18
{
    public class ActionGame : Game
    {
        private int _enemies;

        public int Enemies
        {
            get { return _enemies; }
            set { _enemies = value; }
        }

        public ActionGame(string title, int enemies)
            : base(title)
        {
            Enemies = enemies;
        }

        public override void Start()
        {
            Console.WriteLine("[ActionGame] Починається бiй у грi \"" + Title + "\", ворогiв: " + Enemies);
        }
    }
}