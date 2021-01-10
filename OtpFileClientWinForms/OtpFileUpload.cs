namespace OtpFileClientWinForms
{
    public class OtpFileUpload
    {
        /// <summary>
        /// fájl mime type
        /// </summary>
        public string MimeType { get; set; }

        /// <summary>
        /// fájl tartalom base64 formában
        /// </summary>
        public string Content { get; set; }
    }
}