using Study.LabWork1.Shared.Services;
using Study.LabWork1.Features.Task3;

namespace Study.LabWork1;

public static class Program
{
    public static void Main()
    {
        var service = new RunService();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("\nДоступные задания:");
            Console.WriteLine("1 - Задание 1");
            Console.WriteLine("2 - Задание 2");
            Console.WriteLine("3 - Задание 3");
            Console.WriteLine("0 - Выход");

            Console.Write("\nВыберите номер задания: ");
            string input = Console.ReadLine();

            if (input == "0")
            {
                Console.WriteLine("\nПрограмма завершена");
                break;
            }

            if (!int.TryParse(input, out int taskNumber) || taskNumber < 1 || taskNumber > 3)
            {
                Console.WriteLine("\nОшибка: Введите число от 1 до 3");
                Console.WriteLine("Нажмите любую клавишу для продолжения");
                Console.ReadKey();
                continue;
            }

            Console.Clear();

            try
            {
                switch (taskNumber)
                {
                    case 1:
                        Console.WriteLine("Задание 1\n");
                        service.RunTask1();
                        break;
                    case 2:
                        Console.WriteLine("Задание 2\n");
                        service.RunTask2();
                        break;
                    case 3:
                        Console.WriteLine("Задание 3\n");

                        var root = new TreeNode("A");
                        var b = new TreeNode("B");
                        var c = new TreeNode("C");
                        var d = new TreeNode("D");
                        var e = new TreeNode("E");

                        root.AddChild(b);
                        root.AddChild(c);
                        b.AddChild(d);
                        b.AddChild(e);

                        service.SetTree(root);
                        service.RunTask3();
                        break;
                }
            }
            catch (NotImplementedException)
            {
                Console.WriteLine($"\nЗадание {taskNumber} ещё не реализовано!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nОшибка при выполнении задания: {ex.Message}");
            }

            Console.WriteLine("\nНажмите любую клавишу для возврата в меню...");
            Console.ReadKey();
        }
    }
}
