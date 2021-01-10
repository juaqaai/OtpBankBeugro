using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Http;

namespace OtpFileServerWebApi
{
    /// <summary>
    /// Dokuemtumok web api controller
    /// </summary>
    public class DokumentumokController : ApiController
    {
        private readonly ILogger logger;

        private readonly IFileManager fileManager;

        private readonly string FileStoreFolderName = WebConfigurationManager.AppSettings["FileStoreFolderName"];

        public DokumentumokController(ILogger logger, IFileManager fileManager)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.fileManager = fileManager ?? throw new ArgumentNullException(nameof(fileManager));
        }

        /// <summary>
        /// visszaadja egy mappában lévő fájlok listáját
        /// a mappa neve az elérési úttal a web.config-ban állítható be
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            logger.Information("URL: GET");
            return fileManager.GetFolderFiles(GetSelectedFileStorePath());
        }

        /// <summary>
        /// visszaadja a paraméterben megadott file-t
        /// </summary>
        [HttpGet]
        public FileMetadata Get(string id)
        {
            logger.Information($"URL: GET parameter {id}");
            return fileManager.Download(GetSelectedFileStorePath(), id);
        }

        /// <summary>
        /// fájl feltöltése
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fileUpload">file</param>
        [HttpPost]
        public void Post(string id, [FromBody] FileUpload fileUpload)
        {
            logger.Information($"URL: Post parameter {id}");
            fileManager.Upload(GetSelectedFileStorePath(), new FileMetadata { FileName = id, Content = fileUpload.Content, MimeType = fileUpload.MimeType });
        }

        /// <summary>
        /// visszaadja a választott mappa elérési útját
        /// </summary>
        /// <returns></returns>
        private string GetSelectedFileStorePath()
        {
            if (String.IsNullOrEmpty(HostingEnvironment.ApplicationPhysicalPath) || String.IsNullOrEmpty(FileStoreFolderName))
            {
                return string.Empty;
            }

            return Path.Combine(HostingEnvironment.ApplicationPhysicalPath, FileStoreFolderName);
        }
    }
}