using System;
using System.Net;

namespace Lab9v18
{
    public class HTMLEncoder : Encoder
    {
        public HTMLEncoder() : base("HTML")
        {
        }

        public override string Encode(string data)
        {
            CheckData(data);
            return WebUtility.HtmlEncode(data);
        }
    }
}