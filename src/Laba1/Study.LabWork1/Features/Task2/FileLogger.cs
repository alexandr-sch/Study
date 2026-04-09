using System;
using System.Collections.Generic;
using System.Text;

namespace Study.LabWork1.Features.Task2
{
    public class FileLogger : ILogger
    {
        private readonly string _filePath;

        public FileLogger(string filePath = "log.txt")
        {
            _filePath = filePath;
        }
        public void Log(string message)
        {
            File.AppendAllText(_filePath, message + "\n");
        }
    }
}
