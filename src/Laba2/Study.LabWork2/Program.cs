using System.Text.Json;
using Study.LabWork2.Abstractions.Feature.Task2.DtoModels;
using Study.LabWork2.Feature.Task2;

namespace Study.LabWork2;

public static class Program
{
    public static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        RunTask2();

        Console.WriteLine("\nНажмите любую клавишу для выхода...");
        Console.ReadKey();
    }

    private static void RunTask2()
    {
        Console.WriteLine("Лабораторная работа 2, Задание 2");
        Console.WriteLine("HTTP запросы к серверам (синхронно и асинхронно)\n");

        var servers = new ServerConfigDto[]
        {
            new()
            {
                Name = "JSONPlaceholder Users",
                Url = "https://jsonplaceholder.typicode.com/users/1",
                Method = "GET"
            },
            new()
            {
                Name = "JSONPlaceholder Posts",
                Url = "https://jsonplaceholder.typicode.com/posts/1",
                Method = "GET"
            },
            new()
            {
                Name = "JSONPlaceholder Todos",
                Url = "https://jsonplaceholder.typicode.com/todos/1",
                Method = "GET"
            }
        };

        var requestService = new RequestService();

        Console.WriteLine("\nСИНХРОННАЯ ВЕРСИЯ\n");
        var syncApp = new SynchronousServerRequestApp(requestService);
        var syncResult = syncApp.ExecuteRequests<JsonElement>(servers);
        PrintResult(syncResult);

        Console.WriteLine("\n" + new string('=', 60) + "\n");

        Console.WriteLine("АСИНХРОННАЯ ВЕРСИЯ\n");
        var asyncApp = new AsynchronousServerRequestApp(requestService);
        var asyncResult = asyncApp.ExecuteRequests<JsonElement>(servers);
        PrintResult(asyncResult);

        Console.WriteLine("\nСРАВНЕНИЕ");
        Console.WriteLine($"Синхронная версия: {syncResult.TotalExecutionTime.TotalMilliseconds:F2} мс");
        Console.WriteLine($"Асинхронная версия: {asyncResult.TotalExecutionTime.TotalMilliseconds:F2} мс");

        if (asyncResult.TotalExecutionTime < syncResult.TotalExecutionTime)
        {
            Console.WriteLine("Асинхронная версия быстрее!");
        }
        else
        {
            Console.WriteLine("Время сопоставимо");
        }
    }

    private static void PrintResult(ExecutionResultDto<JsonElement> result)
    {
        Console.WriteLine($"\nВерсия: {result.Version}");
        Console.WriteLine($"Успешных запросов: {result.SuccessfulRequests}");
        Console.WriteLine($"Ошибок: {result.FailedRequests}");
        Console.WriteLine($"Общее время выполнения: {result.TotalExecutionTime.TotalMilliseconds:F2} мс");
    }

}
