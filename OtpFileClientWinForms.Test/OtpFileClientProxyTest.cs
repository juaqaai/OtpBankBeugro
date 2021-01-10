using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace OtpFileClientWinForms.Test
{
    [TestClass]
    public class OtpFileClientProxyTest
    {
        [TestMethod]
        public void GetFolderFiles()
        {
            var proxy = new OtpFileClientProxy();

            var result = proxy.GetFolderFiles();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DownloadFile()
        {
            var proxy = new OtpFileClientProxy();

            var result = proxy.DownloadFile("elso.txt");

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void UploadFile()
        {
            var proxy = new OtpFileClientProxy();

            var contentBytes = System.Text.Encoding.UTF8.GetBytes("nyolcadik");

            var fileContent = Convert.ToBase64String(contentBytes);

            proxy.UploadFile("nyolcadik.txt", new OtpFileUpload { Content = fileContent, MimeType = "text" });
        }
    }
}
