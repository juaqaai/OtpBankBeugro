namespace OtpFileServerWebApi
{
    public class FileUpload
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