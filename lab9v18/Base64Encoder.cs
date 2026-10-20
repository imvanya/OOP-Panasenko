using System;

namespace Lab9v18
{
    public class Base64Encoder : Encoder
    {
        public int MaxLength { get; set; } = 100;

        public Base64Encoder() : base("Base64")
        {
        }

        public override string Encode(string data)
        {
            CheckData(data);
            if (data.Length > MaxLength)
            {
                throw new EncodingException("Рядок задовгий для Base64: " + data.Length + " > " + MaxLength);
            }
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(data);
            return Convert.ToBase64String(bytes);
        }
    }
}