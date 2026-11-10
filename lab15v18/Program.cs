using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Lab15v18
{
    class Program
    {
        static List<Client> clients = new List<Client>
        {
            new Client(1, "Oleh Koval", "Tech Solutions"),
            new Client(2, "Maria Shevchenko", "Tech Solutions"),
            new Client(3, "Andrii Bondar", "Tech Solutions"),
            new Client(4, "Sofia Melnyk", "Tech Solutions"),
            new Client(5, "Ihor Tkachuk", "Green Logistics"),
            new Client(6, "Natalia Kravets", "Green Logistics"),
            new Client(7, "Taras Lysenko", "Blue Bank"),
            new Client(8, "Olena Savchuk", "Blue Bank"),
            new Client(9, "Dmytro Rudenko", "Nova Print"),
            new Client(10, "Iryna Polishchuk", "Nova Print"),
            new Client(11, "Vasyl Moroz", "Tech Solutions"),
            new Client(12, "Yulia Hnatiuk", "Sky Travel")
        };

        static List<Contact> contacts = new List<Contact>
        {
            new Contact(1, "oleh@techsol.com", "+380501110001", 1),
            new Contact(2, "oleh.koval@gmail.com", "+380501110002", 1),
            new Contact(3, "o.koval@work.net", "+380501110003", 1),
            new Contact(4, "maria@techsol.com", "+380501110004", 2),
            new Contact(5, "andrii@techsol.com", "+380501110005", 3),
            new Contact(6, "bondar.a@gmail.com", "+380501110006", 3),
            new Contact(7, "ihor@greenlog.com", "+380501110007", 5),
            new Contact(8, "ihor.t@gmail.com", "+380501110008", 5),
            new Contact(9, "natalia@greenlog.com", "+380501110009", 6),
            new Contact(10, "taras@bluebank.com", "+380501110010", 7),
            new Contact(11, "taras.l@gmail.com", "+380501110011", 7),
            new Contact(12, "t.lysenko@work.net", "+380501110012", 7),
            new Contact(13, "lysenko@mail.com", "+380501110013", 7),
            new Contact(14, "olena@bluebank.com", "+380501110014", 8),
            new Contact(15, "dmytro@novaprint.com", "+380501110015", 9),
            new Contact(16, "d.rudenko@gmail.com", "+380501110016", 9),
            new Contact(17, "vasyl@techsol.com", "+380501110017", 11),
            new Contact(18, "moroz.v@gmail.com", "+380501110018", 11),
            new Contact(19, "yulia@skytravel.com", "+380501110019", 12)
        };

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Compare("Запит 1: клієнти Tech Solutions з більше ніж одним контактом",
                () => (from c in clients
                       join k in contacts on c.Id equals k.ClientId into cg
                       where c.Company == "Tech Solutions" && cg.Count() > 1
                       select c.Name + " (" + cg.Count() + ")").ToList(),
                () => clients
                    .Where(c => c.Company == "Tech Solutions")
                    .Select(c => new { c.Name, Count = contacts.Count(k => k.ClientId == c.Id) })
                    .Where(x => x.Count > 1)
                    .Select(x => x.Name + " (" + x.Count + ")")
                    .ToList(),
                10);

            Compare("Запит 2: клієнти та email їхніх контактів (join)",
                () => (from c in clients
                       join k in contacts on c.Id equals k.ClientId
                       select c.Name + " - " + k.Email).ToList(),
                () => clients
                    .Join(contacts, c => c.Id, k => k.ClientId, (c, k) => c.Name + " - " + k.Email)
                    .ToList(),
                5);

            Compare("Запит 3: кількість контактів для кожного клієнта",
                () => (from c in clients
                       join k in contacts on c.Id equals k.ClientId into cg
                       select c.Name + ": " + cg.Count()).ToList(),
                () => clients
                    .GroupJoin(contacts, c => c.Id, k => k.ClientId, (c, cg) => c.Name + ": " + cg.Count())
                    .ToList(),
                12);

            Compare("Запит 4: клієнт з найбільшою кількістю контактів",
                () => (from c in clients
                       let count = contacts.Count(k => k.ClientId == c.Id)
                       orderby count descending
                       select c.Name + " (" + count + ")").Take(1).ToList(),
                () =>
                {
                    Client best = clients.MaxBy(c => contacts.Count(k => k.ClientId == c.Id));
                    int count = contacts.Count(k => k.ClientId == best.Id);
                    return new List<string> { best.Name + " (" + count + ")" };
                },
                1);

            DeferredDemo();
            BigDataDemo();
        }

        static void Compare(string title, Func<List<string>> querySyntax, Func<List<string>> methodSyntax, int show)
        {
            Console.WriteLine(title);

            Stopwatch watch = Stopwatch.StartNew();
            List<string> result1 = querySyntax();
            watch.Stop();
            double time1 = watch.Elapsed.TotalMilliseconds;

            watch.Restart();
            List<string> result2 = methodSyntax();
            watch.Stop();
            double time2 = watch.Elapsed.TotalMilliseconds;

            foreach (string line in result1.Take(show))
            {
                Console.WriteLine("  " + line);
            }
            if (result1.Count > show)
            {
                Console.WriteLine("  ... (усього рядків: " + result1.Count + ")");
            }

            Console.WriteLine("Синтаксис запитів: " + time1.ToString("F3") + " мс");
            Console.WriteLine("Синтаксис методів: " + time2.ToString("F3") + " мс");
            Console.WriteLine("Результати збігаються: " + result1.SequenceEqual(result2));
            Console.WriteLine();
        }

        static void DeferredDemo()
        {
            Console.WriteLine("Відкладене та негайне виконання");

            Stopwatch watch = Stopwatch.StartNew();
            IEnumerable<Client> query = clients.Where(c => c.Company == "Tech Solutions");
            watch.Stop();
            Console.WriteLine("Створення запиту (без виконання): " + watch.Elapsed.TotalMilliseconds.ToString("F3") + " мс");

            watch.Restart();
            List<Client> list = query.ToList();
            watch.Stop();
            Console.WriteLine("ToList() (негайне виконання): " + watch.Elapsed.TotalMilliseconds.ToString("F3")
                + " мс, знайдено " + list.Count);
            Console.WriteLine();
        }

        static void BigDataDemo()
        {
            Console.WriteLine("Великі дані: GroupJoin проти Count у циклі");

            List<Client> bigClients = Enumerable.Range(1, 3000)
                .Select(i => new Client(i, "Client" + i, "Company" + i % 50)).ToList();
            List<Contact> bigContacts = Enumerable.Range(1, 12000)
                .Select(i => new Contact(i, "user" + i + "@mail.com", "+380" + i, i % 3000 + 1)).ToList();
            Console.WriteLine("Клієнтів: " + bigClients.Count + ", контактів: " + bigContacts.Count);

            Stopwatch watch = Stopwatch.StartNew();
            List<int> viaJoin = bigClients
                .GroupJoin(bigContacts, c => c.Id, k => k.ClientId, (c, cg) => cg.Count())
                .ToList();
            watch.Stop();
            Console.WriteLine("GroupJoin: " + watch.Elapsed.TotalMilliseconds.ToString("F3") + " мс");

            watch.Restart();
            List<int> viaCount = bigClients
                .Select(c => bigContacts.Count(k => k.ClientId == c.Id))
                .ToList();
            watch.Stop();
            Console.WriteLine("Count для кожного клієнта: " + watch.Elapsed.TotalMilliseconds.ToString("F3") + " мс");
            Console.WriteLine("Результати збігаються: " + viaJoin.SequenceEqual(viaCount));
        }
    }
}

