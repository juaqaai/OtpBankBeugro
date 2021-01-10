using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;

namespace OtpFileServerWebApi.Test
{
    /// <summary>
    /// FileManager műveleteket teszteli
    /// </summary>
    [TestClass]
    public class FileManagerTest
    {
        private const string FileStoreFolderName = @"FileServerRoot\Fajlok_01";

        private IFileManager fileManager;

        public FileManagerTest()
        {
            fileManager = new FileManager();
        }

        /// <summary>
        /// minden teszt után lefut, ha van feltöltött file, akkor azt törli
        /// </summary>
        [TestCleanup()]
        public void FileManagerTestCleanup()
        {
            var permanentFiles = new string[] { "elso.txt", "masodik.txt", "harmadik.txt" };

            var directoryInfo = new DirectoryInfo(GetFileStoreFolderName());

            foreach (var fileInfo in directoryInfo.GetFiles("*", SearchOption.TopDirectoryOnly))
            {
                if (!permanentFiles.Contains(fileInfo.Name))
                {
                    fileInfo.Delete();
                }
            }
        }

        [TestMethod]
        public void GetFolderFilesTest()
        {
            var fileNames = fileManager.GetFolderFiles(GetFileStoreFolderName());

            Assert.IsNotNull(fileNames);
            Assert.AreEqual(3, fileNames.Count());
        }

        [TestMethod]
        public void DownloadTest()
        {
            var fileName = "elso.txt";

            var file = fileManager.Download(GetFileStoreFolderName(), fileName);

            Assert.IsNotNull(file);
            Assert.IsNotNull(file.Content);
            Assert.IsNotNull(file.FileName);
            Assert.AreEqual(fileName, file.FileName);
        }

        [TestMethod]
        public void UploadTest()
        {
            var contentBytes = System.Text.Encoding.UTF8.GetBytes("negyedik");

            var fileName = "negyedik.txt";

            fileManager.Upload(GetFileStoreFolderName(), new FileMetadata
            {
                FileName = fileName,
                Content = Convert.ToBase64String(contentBytes)
            });

            var path = Path.Combine(GetFileStoreFolderName(), fileName);

            var exists = File.Exists(path);

            Assert.IsTrue(exists);
        }

        private string GetFileStoreFolderName()
        {
            var enviroment = Environment.CurrentDirectory;

            string projectDirectory = Directory.GetParent(enviroment).Parent.FullName;

            return Path.Combine(projectDirectory, FileStoreFolderName);
        }
    }
}
