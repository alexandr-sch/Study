using Study.LabWork1.Features.Task2;
using Study.LabWork1.Shared.Abstractions;

namespace Study.LabWork1.Shared.Services;

/// <summary>
/// Реализация заданий Л/Р
/// </summary>
public class RunService : IRunService
{
    /// <summary>
    /// Задание 1
    /// </summary>
    public void RunTask1() => throw new NotImplementedException();

    /// <summary>
    /// Задание 2
    /// </summary>
    public void RunTask2()
    {
        Console.WriteLine("Задание 2\n");
        while (true)
        {
            Console.WriteLine("\nВыберите тип логгера:");
            Console.WriteLine("1 - Console");
            Console.WriteLine("2 - File");
            Console.WriteLine("3 - Server");
            Console.WriteLine("0 - Выход");

            string choice = Console.ReadLine();

            if (choice == "0")
                return;

            ILogger logger = choice switch
            {
                "1" => LoggerFactory.CreateLogger(LoggerType.Console),
                "2" => LoggerFactory.CreateLogger(LoggerType.File),
                "3" => LoggerFactory.CreateLogger(LoggerType.Server),
                _ => null
            };

            if (logger == null)
            {
                Console.WriteLine("Неверный выбор");
                continue;
            }

            Console.Write("Введите сообщение: ");
            string message = Console.ReadLine();

            logger.Log(message);
        }
    }

    /// <summary>
    /// Задание 3
    /// </summary>
    public void RunTask3() => throw new NotImplementedException();
}
