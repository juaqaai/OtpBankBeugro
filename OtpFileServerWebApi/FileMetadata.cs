namespace OtpFileServerWebApi
{
    public class FileMetadata
    {
        /// <summary>
        /// fájl neve
        /// </summary>
        public string FileName { get; set; }

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