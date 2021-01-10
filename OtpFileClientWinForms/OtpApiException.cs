using System;

namespace OtpFileClientWinForms
{
    public class OtpApiException : Exception
    {
        public int StatusCode { get; set; }

        public string Content { get; set; }
    }

}
