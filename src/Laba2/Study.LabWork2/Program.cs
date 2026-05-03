using Study.LabWork2.Abstractions.Feature.Task1.SubTask1;
using Study.LabWork2.Feature.Task1.SubTask1;

namespace Study.LabWork2;

public static class Program
{
    public static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("Лабораторная работа 2, Задание 1.1\n");

        const int start = 1;
        const int end = 10000;
        const int threadCount = 4;

        IPrimeCounter[] counters =
        {
            new MonitorService(),
            new MutexService(),
            new SemaphoreService()
        };

        foreach (var counter in counters)
        {
            Console.WriteLine($"\nЗапуск версии: {counter.GetVersionName()}\n");

            var result = counter.CountPrimes(start, end, threadCount);

            Console.WriteLine($"\n{result}");

        }

        Console.WriteLine("\nНажмите любую клавишу для выхода");
        Console.ReadKey();
    }
}
