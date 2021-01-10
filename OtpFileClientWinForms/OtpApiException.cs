using System;

namespace OtpFileClientWinForms
{
    public class OtpApiException : Exception
    {
        public int StatusCode { get; set; }

        public OtpApiException(string message) : base(message) { }

        public OtpApiException(string message, int statusCode) : base(message)
        {
            StatusCode = statusCode;
        }

        public OtpApiException(string message, Exception innerException) : base(message, innerException) { }
    }

}
