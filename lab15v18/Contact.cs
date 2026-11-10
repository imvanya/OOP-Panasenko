namespace Lab15v18
{
    public class Contact
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int ClientId { get; set; }

        public Contact(int id, string email, string phone, int clientId)
        {
            Id = id;
            Email = email;
            Phone = phone;
            ClientId = clientId;
        }
    }
}