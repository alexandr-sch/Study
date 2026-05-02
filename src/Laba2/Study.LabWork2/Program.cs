using Study.LabWork2.Feature.Task1.SubTask2;

namespace Study.LabWork2;

public static class Program
{
    public static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        RunTask12();

        Console.WriteLine("\nНажмите любую клавишу для выхода...");
        Console.ReadKey();
    }

    private static void RunTask12()
    {
        Console.WriteLine("Лабораторная работа 2, Задание 1.2");

        const int maxThreads = 4; 

        var processor = new NumberSetProcessor(maxThreads);
        processor.Process();

        var result = processor.GetResult();

        Console.WriteLine("\nИТОГИ");

        Console.WriteLine("\nРезультаты по наборам:");
        foreach (var entry in result.Results.OrderBy(r => r.SetNumber))
        {
            Console.WriteLine(entry);
        }

        Console.WriteLine($"\nОбщий итог по всем наборам: {result.TotalSum:N0}");
        Console.WriteLine($"Время выполнения: {result.ExecutionTime.TotalMilliseconds:F2} мс");
        Console.WriteLine($"Обработано наборов: {result.ProcessedSetsCount}");
        Console.WriteLine($"Максимум одновременных потоков: {maxThreads}");
    }
}
