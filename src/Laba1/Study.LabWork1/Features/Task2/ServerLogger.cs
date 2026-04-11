using System;
using System.Collections.Generic;
using System.Text;

namespace Study.LabWork1.Features.Task2
{
    public class ServerLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine($"[Server] Отправлено на сервер: {message}");
        }
    }
}
