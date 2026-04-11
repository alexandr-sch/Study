using System;
using System.Collections.Generic;
using System.Text;

namespace Study.LabWork1.Features.Task2
{
    public class ConsoleLogger : ILogger    
    {
        public void Log(string message)
        {
            Console.WriteLine($"[Console] {message}");
        }
    }
}
