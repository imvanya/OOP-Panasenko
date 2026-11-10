namespace Lab15v18
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }

        public Client(int id, string name, string company)
        {
            Id = id;
            Name = name;
            Company = company;
        }
    }
}