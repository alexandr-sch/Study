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
        
        Console.WriteLine("Задание 1\n");

        while (true)
        {
            Console.WriteLine("\nВыберите действие:");
            Console.WriteLine("1 - Создать дробь");
            Console.WriteLine("2 - Сложить две дроби");
            Console.WriteLine("3 - Вычесть две дроби");
            Console.WriteLine("4 - Умножить две дроби");
            Console.WriteLine("5 - Разделить две дроби");
            Console.WriteLine("6 - Сравнить две дроби");
            Console.WriteLine("0 - Выход");
            Console.Write("\nВаш выбор: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreateFraction();
                    break;
                case "2":
                    PerformOperation("сложения", (a, b) => a + b, "+");
                    break;
                case "3":
                    PerformOperation("вычитания", (a, b) => a - b, "-");
                    break;
                case "4":
                    PerformOperation("умножения", (a, b) => a * b, "*");
                    break;
                case "5":
                    PerformOperation("деления", (a, b) => a / b, "/");
                    break;
                case "6":
                    CompareFractions();
                    break;
                case "0":
                    Console.WriteLine("\nВыход из задания 1.");
                    return;
                default:
                    Console.WriteLine("Неверный выбор. Попробуйте снова.");
                    break;
            }
        }
    }
    private void CreateFraction()
    {
        Console.WriteLine("\nСоздание дроби");
        var fraction = InputFraction("Введите дробь");
        if (fraction != null)
        {
            Console.WriteLine($"\nРезультат создания:");
            Console.WriteLine($"  В виде дроби: {fraction}");
            Console.WriteLine($"  Числитель: {fraction.Numerator}");
            Console.WriteLine($"  Знаменатель: {fraction.Denominator}");
        }
    }
    private void PerformOperation(string operationName, Func<RationalNumber, RationalNumber, RationalNumber> operation, string symbol)
    {
        Console.WriteLine($"\nОперация {operationName.ToUpper()}");

        var a = InputFraction("Введите первую дробь");
        if (a == null) return;

        var b = InputFraction("Введите вторую дробь");
        if (b == null) return;

        try
        {
            var result = operation(a, b);
            Console.WriteLine($"\nРезультат {operationName}:");
            Console.WriteLine($"  {a} {symbol} {b} = {result}");
            Console.WriteLine($"  В виде числитель/знаменатель: {result.Numerator}/{result.Denominator}");
        }
        catch (DivideByZeroException ex)
        {
            Console.WriteLine($"\nОшибка: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nНеизвестная ошибка: {ex.Message}");
        }
    }
    private void CompareFractions()
    {
        Console.WriteLine("\nСравнение дробей");

        var a = InputFraction("Введите первую дробь");
        if (a == null) return;

        var b = InputFraction("Введите вторую дробь");
        if (b == null) return;

        Console.WriteLine($"\nРезультаты сравнения {a} и {b}:");
        Console.WriteLine($"  {a} == {b} : {a == b}");
        Console.WriteLine($"  {a} != {b} : {a != b}");
        Console.WriteLine($"  {a} < {b}  : {a < b}");
        Console.WriteLine($"  {a} > {b}  : {a > b}");
        Console.WriteLine($"  {a} <= {b} : {a <= b}");
        Console.WriteLine($"  {a} >= {b} : {a >= b}");
    }
    private RationalNumber InputFraction(string prompt)
    {
        while (true)
        {
            Console.Write($"{prompt} (формат: числитель/знаменатель или 'отмена'): ");
            string input = Console.ReadLine();

            if (input.ToLower() == "отмена")
                return null;

            try
            {
                // Парсим ввод в формате "числитель/знаменатель"
                var parts = input.Split('/');

                if (parts.Length != 2)
                {
                    Console.WriteLine("  Ошибка: Неверный формат. Используйте формат 'числитель/знаменатель'.");
                    continue;
                }

                if (!int.TryParse(parts[0].Trim(), out int numerator))
                {
                    Console.WriteLine("  Ошибка: Числитель должен быть целым числом.");
                    continue;
                }

                if (!int.TryParse(parts[1].Trim(), out int denominator))
                {
                    Console.WriteLine("  Ошибка: Знаменатель должен быть целым числом.");
                    continue;
                }

                return new RationalNumber(numerator, denominator);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"  Ошибка создания дроби: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  Неизвестная ошибка: {ex.Message}");
            }

        }
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
