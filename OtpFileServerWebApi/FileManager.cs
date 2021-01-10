using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OtpFileServerWebApi
{
    public interface IFileManager
    {
        void Upload(string fileStoreFolderName, FileMetadata fileMetadata);

        FileMetadata Download(string fileStoreFolderName, string fileName);

        IEnumerable<string> GetFolderFiles(string fileStoreFolderName);
    }

    /// <summary>
    /// fájlműveleteket valósítja meg
    /// - feltöltés
    /// - letöltés
    /// - mappa tartalmának listázása
    /// </summary>
    public class FileManager : IFileManager
    {
        /// <summary>
        /// feltölt egy fájlt, melynek tartalma base64 kódolással érkezik
        /// </summary>
        /// <param name="fileStoreFolderName"></param>
        /// <param name="fileMetadata"></param>
        public void Upload(string fileStoreFolderName, FileMetadata fileMetadata)
        {
            ValidateFolder(fileStoreFolderName);

            ValidateFileMetadata(fileMetadata);

            var byteContent = Convert.FromBase64String(fileMetadata.Content);

            var filePath = GetSelectedFilePath(fileStoreFolderName, fileMetadata.FileName);

            File.WriteAllBytes(filePath, byteContent);
        }

        /// <summary>
        /// visszaadja a kért file tartalmát base64 kódolással
        /// </summary>
        /// <param name="fileStoreFolderName"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public FileMetadata Download(string fileStoreFolderName, string fileName)
        {
            ValidateFolder(fileStoreFolderName);

            ValidateFile(fileStoreFolderName, fileName);

            var selectedFilePath = GetSelectedFilePath(fileStoreFolderName, fileName);

            var byteContent = File.ReadAllBytes(selectedFilePath);

            var content = Convert.ToBase64String(byteContent);

            return new FileMetadata { FileName = fileName, Content = content };
        }

        /// <summary>
        /// visszaadja a web.config-ban beállított mappában elhelyezett fájlok listáját
        /// </summary>
        /// <param name="fileStoreFolderName"></param>
        /// <returns></returns>
        public IEnumerable<string> GetFolderFiles(string fileStoreFolderName)
        {
            ValidateFolder(fileStoreFolderName);

            var directoryInfo = new DirectoryInfo(fileStoreFolderName);

            var fileInfos = directoryInfo.GetFiles("*", SearchOption.AllDirectories);

            return fileInfos.Select(fileInfo => fileInfo.Name).OrderBy(n => n).ToList();
        }

        /// <summary>
        /// ellenőrzi, hogy elérhető-e a mappa
        /// </summary>
        /// <param name="fileStoreFolderName"></param>
        private void ValidateFolder(string fileStoreFolderName)
        {
            if (string.IsNullOrEmpty(fileStoreFolderName))
            {
                throw new ArgumentNullException(nameof(fileStoreFolderName));
            }

            if (!Directory.Exists(fileStoreFolderName))
            {
                throw new DirectoryNotFoundException($"A megadott mappa nem található! {fileStoreFolderName}");
            }
        }

        /// <summary>
        /// ellenőrzi, hogy elérhető-e a fájl
        /// </summary>
        /// <param name="fileStoreFolderName"></param>
        /// <param name="fileName"></param>
        private void ValidateFile(string fileStoreFolderName, string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            var selectedFilePath = Path.Combine(fileStoreFolderName, fileName);

            if (!File.Exists(selectedFilePath))
            {
                throw new FileNotFoundException($"A megadott file nem található! {selectedFilePath}");
            }
        }

        /// <summary>
        /// ellenőrzi, hogy a file meta fel van-e töltve
        /// </summary>
        /// <param name="fileMetadata"></param>
        private void ValidateFileMetadata(FileMetadata fileMetadata)
        {
            if (fileMetadata == null)
            {
                throw new ArgumentNullException(nameof(fileMetadata));
            }

            if (string.IsNullOrEmpty(fileMetadata.Content))
            {
                throw new ArgumentNullException(nameof(fileMetadata.Content));
            }

            if (string.IsNullOrEmpty(fileMetadata.FileName))
            {
                throw new ArgumentNullException(nameof(fileMetadata.FileName));
            }
        }

        /// <summary>
        /// visszaadja a választott fájl elérési útját
        /// </summary>
        /// <param name="fileStoreFolderName"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private string GetSelectedFilePath(string fileStoreFolderName, string fileName)
        {
            return Path.Combine(Path.GetFullPath(fileStoreFolderName), fileName);
        }
    }
}