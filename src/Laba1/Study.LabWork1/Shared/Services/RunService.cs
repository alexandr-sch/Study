using Study.LabWork1.Shared.Abstractions;

namespace Study.LabWork1.Shared.Services;
using Study.LabWork1.Features.Task3;

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
    public void RunTask2() => throw new NotImplementedException();


    /// <summary>
    /// Задание 3
    /// </summary>
    private TreeNode _root;
    public void SetTree(TreeNode root)
    {
        _root = root;
    }
    public void RunTask3()
    {

        Console.WriteLine("Задание 3\n");

        if (_root == null)
        {
            Console.WriteLine("Дерево не задано!");
            return;
        }

        _root.Print();
    }

}
