using System;
using System.Collections.Generic;
using System.Text;

namespace Study.LabWork1.Features.Task2
{
    public class LoggerFactory
    {
        public static ILogger CreateLogger(LoggerType type)
        {
            return type switch
            {
                LoggerType.Console => new ConsoleLogger(),
                LoggerType.File => new FileLogger(),
                LoggerType.Server => new ServerLogger(),
                _ => throw new ArgumentException("Неизвестный тип логгера")
            };
        }
    }
}
