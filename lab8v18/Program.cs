using System;
using System.Collections.Generic;

namespace Lab8v18
{
    public class WorkItem
    {
        public string Title { get; set; }

        public WorkItem(string title)
        {
            Title = title;
        }

        public virtual void Execute()
        {
            Console.WriteLine($"Виконується завдання: {Title}");
        }
    }

    public class DevelopmentTask : WorkItem
    {
        public string FeatureName { get; set; }

        public DevelopmentTask(string title, string featureName) : base(title)
        {
            FeatureName = featureName;
        }

        public override void Execute()
        {
            Console.WriteLine($"[Розробка] {Title}: реалізація функції \"{FeatureName}\"");
        }
    }

    public class TestingTask : WorkItem
    {
        public int TestPlanId { get; set; }

        public TestingTask(string title, int testPlanId) : base(title)
        {
            TestPlanId = testPlanId;
        }

        public override void Execute()
        {
            Console.WriteLine($"[Тестування] {Title}: запуск тест-плану №{TestPlanId}");
        }
    }

    public class DocumentationTask : WorkItem
    {
        public string DocVersion { get; set; }

        public DocumentationTask(string title, string docVersion) : base(title)
        {
            DocVersion = docVersion;
        }

        public override void Execute()
        {
            Console.WriteLine($"[Документація] {Title}: оновлення документації до версії {DocVersion}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            List<WorkItem> tasks = new List<WorkItem>
            {
                new DevelopmentTask("Завдання 1", "Функція A"),
                new TestingTask("Завдання 2", 101),
                new DocumentationTask("Завдання 3", "1.0")
            };

            List<string> executed = new List<string>();

            Console.WriteLine("Виконання завдань");
            foreach (WorkItem task in tasks)
            {
                task.Execute();            
                executed.Add(task.Title);  
            }

            Console.WriteLine("\nВиконані завдання");
            foreach (string title in executed)
            {
                Console.WriteLine(title);
            }
        }
    }
}