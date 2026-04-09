using Study.LabWork1.Features.Task1;
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
    public void RunTask1()
    {
        Console.WriteLine("Задача 1");
        Console.Write("Введите числитель: ");
        int a = int.Parse(Console.ReadLine());

        Console.Write("Введите знаменатель: ");
        int b = int.Parse(Console.ReadLine());

        RationalNumber number = new RationalNumber(a, b);

        Console.WriteLine($"Число: {number}");
        
    }

    /// <summary>
    /// Задание 2
    /// </summary>
    public void RunTask2() => throw new NotImplementedException();

    /// <summary>
    /// Задание 3
    /// </summary>
    public void RunTask3() => throw new NotImplementedException();
}
