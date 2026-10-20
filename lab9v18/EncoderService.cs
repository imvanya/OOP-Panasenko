using System;
using System.Collections.Generic;

namespace Lab9v18
{
    public class EncoderService
    {
        public int SuccessCount { get; private set; }
        public int ErrorCount { get; private set; }

        public void EncodeAll(List<Encoder> encoders, string data)
        {
            foreach (Encoder encoder in encoders)
            {
                try
                {
                    string result = encoder.Encode(data);
                    Console.WriteLine(encoder.Name + ": " + result);
                    SuccessCount++;
                }
                catch (ArgumentNullException ex)
                {
                    Console.WriteLine(encoder.Name + " - помилка даних: " + ex.Message);
                    ErrorCount++;
                }
                catch (EncodingException ex)
                {
                    Console.WriteLine(encoder.Name + " - помилка кодування: " + ex.Message);
                    ErrorCount++;
                }
            }
        }
    }
}