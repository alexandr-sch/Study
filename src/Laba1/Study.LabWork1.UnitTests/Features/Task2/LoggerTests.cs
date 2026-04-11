using System;
using System.Collections.Generic;
using System.Text;
using Study.LabWork1.Features.Task2;

namespace Study.LabWork1.UnitTests.Features.Task2
{
    [TestFixture]
    public class LoggerTests
    {
        [Test]
        public void Factory_CreatesConsoleLogger()
        {
            var logger = LoggerFactory.CreateLogger(LoggerType.Console);

            Assert.That(logger, Is.TypeOf<ConsoleLogger>());
        }

        [Test]
        public void Factory_CreatesFileLogger()
        {
            var logger = LoggerFactory.CreateLogger(LoggerType.File);

            Assert.That(logger, Is.TypeOf<FileLogger>());
        }

        [Test]
        public void Factory_CreatesServerLogger()
        {
            var logger = LoggerFactory.CreateLogger(LoggerType.Server);

            Assert.That(logger, Is.TypeOf<ServerLogger>());
        }

        [Test]
        public void Factory_InvalidType_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() =>
                LoggerFactory.CreateLogger((LoggerType)999));
        }

        [Test]
        public void FileLogger_WritesToFile()
        {
            string path = "test_log.txt";

            if (File.Exists(path))
                File.Delete(path);

            var logger = new FileLogger(path);
            logger.Log("test message");

            Assert.That(File.Exists(path), Is.True);

            string content = File.ReadAllText(path);
            Assert.That(content, Does.Contain("test message"));
        }

        [Test]
        public void ConsoleLogger_DoesNotThrow()
        {
            var logger = new ConsoleLogger();

            Assert.DoesNotThrow(() => logger.Log("test"));
        }

        [Test]
        public void ServerLogger_DoesNotThrow()
        {
            var logger = new ServerLogger();

            Assert.DoesNotThrow(() => logger.Log("test"));
        }
    }
}
