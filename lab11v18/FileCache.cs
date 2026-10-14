using System;
using System.IO;

namespace Lab11v18
{
    public class FileCache : CacheManager
    {
        public string FilePath { get; private set; }

        protected override void GenerateKey()
        {
            Key = "file_" + Name.ToLower().Replace(' ', '_');
            FilePath = Path.Combine(Path.GetTempPath(), Key + ".cache");
            Console.WriteLine("Ключ: " + Key);
        }

        protected override void Serialize()
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(Data);
            SerializedData = Convert.ToBase64String(bytes);
            Console.WriteLine("Серiалiзовано у Base64: " + SerializedData);
        }

        protected override void Store()
        {
            File.WriteAllText(FilePath, SerializedData);
            Console.WriteLine("Записано у файл: " + FilePath);
        }
    }
}