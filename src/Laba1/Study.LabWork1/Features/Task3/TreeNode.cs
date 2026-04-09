using System;
using System.Collections.Generic;
using System.Text;

namespace Study.LabWork1.Features.Task3
{
    public class TreeNode
    {
        public string Value { get; }
        public List<TreeNode> Children { get; }

        public TreeNode(string value)
        {
            Value = value;
            Children = new List<TreeNode>();
        }

        public void AddChild(TreeNode child)
        {
            Children.Add(child);
        }
        public void Print(int level = 0)
        {
            Console.WriteLine(new string(' ', level * 2) + Value);

            foreach (var child in Children)
            {
                child.Print(level + 1);
            }

        }
    }
}
