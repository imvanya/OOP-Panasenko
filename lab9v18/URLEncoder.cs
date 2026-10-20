using System;

namespace Lab9v18
{
    public class URLEncoder : Encoder
    {
        public URLEncoder() : base("URL")
        {
        }

        public override string Encode(string data)
        {
            CheckData(data);
            return Uri.EscapeDataString(data);
        }
    }
}