using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Serilog;
using System;
using System.Linq;

namespace OtpFileServerWebApi.Test.Controllers
{
    [TestClass]
    public class DokumentumokControllerTest
    {
        private MockRepository mockRepository;

        public DokumentumokControllerTest()
        {
            mockRepository = new MockRepository(MockBehavior.Default);
        }

        [TestMethod]
        public void Get()
        {
            bool logInfoLefutott = false;
            var loggerMock = CreateLoggerMock(() =>
            {
                logInfoLefutott = true;
            });

            var fileManagerMock = CreateFileManagerMock();

            var controller = new DokumentumokController(loggerMock.Object, fileManagerMock.Object);

            var result = controller.Get();

            fileManagerMock.Verify(m => m.GetFolderFiles(It.IsAny<string>()), Times.Once());

            Assert.IsNotNull(result);
            Assert.IsTrue(logInfoLefutott);
            Assert.AreEqual(result.Count(), 3);
        }

        [TestMethod]
        public void GetById()
        {
            bool logInfoLefutott = false;

            var loggerMock = CreateLoggerMock(() =>
            {
                logInfoLefutott = true;
            });

            var fileManagerMock = CreateFileManagerMock();

            var controller = new DokumentumokController(loggerMock.Object, fileManagerMock.Object);

            var result = controller.Get("FileServerRoot");

            fileManagerMock.Verify(m => m.Download(It.IsAny<string>(), It.IsAny<string>()), Times.Once());

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
            Assert.IsNotNull(result.FileName);
            Assert.IsTrue(logInfoLefutott);
        }

        [TestMethod]
        public void Post()
        {
            bool logInfoLefutott = false;
            var loggerMock = CreateLoggerMock(() =>
            {
                logInfoLefutott = true;
            });

            var fileManagerMock = CreateFileManagerMock();

            var controller = new DokumentumokController(loggerMock.Object, fileManagerMock.Object);

            controller.Post("elso.txt", new FileUpload { MimeType = "text", Content = "ZWxzbw==" });

            fileManagerMock.Verify(m => m.Upload(It.IsAny<string>(), It.IsAny<FileMetadata>()), Times.Once());
            Assert.IsTrue(logInfoLefutott);
        }

        private Mock<ILogger> CreateLoggerMock(Action callbackAction)
        {
            var loggerMock = mockRepository.Create<ILogger>();

            loggerMock.Setup(l => l.Information(It.IsAny<string>())).Callback(callbackAction);

            return loggerMock;
        }

        private Mock<IFileManager> CreateFileManagerMock()
        {
            var fileManagerMock = mockRepository.Create<IFileManager>();

            fileManagerMock.Setup(fm => fm.GetFolderFiles(It.IsAny<string>())).Returns(new string[] {
                "elso.txt", "masodik.txt", "harmadik.txt"
            });

            fileManagerMock.Setup(fm => fm.Download(It.IsAny<string>(), It.IsAny<string>())).Returns(new FileMetadata
            {
                FileName = "elso.txt",
                Content = "ZWxzbw=="
            });

            fileManagerMock.Setup(fm => fm.Upload(It.IsAny<string>(), It.IsAny<FileMetadata>()));

            return fileManagerMock;
        }
    }
}
