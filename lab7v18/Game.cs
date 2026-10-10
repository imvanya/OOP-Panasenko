using System;

namespace Lab7v18
{
    public class Game
    {
        private string _title;

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public Game(string title)
        {
            Title = title;
        }

        public virtual void Start()
        {
            Console.WriteLine("[Game] Запуск гри \"" + Title + "\"");
        }
    }
}