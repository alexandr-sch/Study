using System;
using System.Collections.Generic;
using System.Text;
using Study.LabWork1.Features.Task3;

namespace Study.LabWork1.UnitTests.Features.Task3
{
    [TestFixture]
    public class TreeNodeTests
    {
        [Test]
        public void Constructor_CreatesNodeWithValue()
        {
            var node = new TreeNode("Test");

            Assert.That(node.Value, Is.EqualTo("Test"));
            Assert.That(node.Children, Is.Not.Null);
            Assert.That(node.Children.Count, Is.EqualTo(0));
        }

        [Test]
        public void AddChild_AddsNodeToChildren()
        {
            var root = new TreeNode("Root");
            var child = new TreeNode("Child");

            root.AddChild(child);

            Assert.That(root.Children.Count, Is.EqualTo(1));
            Assert.That(root.Children[0], Is.SameAs(child));
            Assert.That(root.Children[0].Value, Is.EqualTo("Child"));
        }

        [Test]
        public void AddChild_MultipleChildren_AllAdded()
        {
            var root = new TreeNode("Root");
            var child1 = new TreeNode("Child1");
            var child2 = new TreeNode("Child2");
            var child3 = new TreeNode("Child3");

            root.AddChild(child1);
            root.AddChild(child2);
            root.AddChild(child3);

            Assert.That(root.Children.Count, Is.EqualTo(3));
            Assert.That(root.Children[0].Value, Is.EqualTo("Child1"));
            Assert.That(root.Children[1].Value, Is.EqualTo("Child2"));
            Assert.That(root.Children[2].Value, Is.EqualTo("Child3"));
        }

        [Test]
        public void Print_OutputsTreeStructure()
        {
            var root = new TreeNode("A");
            var b = new TreeNode("B");
            var c = new TreeNode("C");
            var d = new TreeNode("D");

            root.AddChild(b);
            root.AddChild(c);
            b.AddChild(d);

            using var sw = new StringWriter();
            Console.SetOut(sw);

            root.Print();

            var output = sw.ToString();

            Assert.That(output, Does.Contain("A"));
            Assert.That(output, Does.Contain("  B"));
            Assert.That(output, Does.Contain("    D"));
            Assert.That(output, Does.Contain("  C"));
        }

        [Test]
        public void Print_EmptyTree_PrintsOnlyRoot()
        {
            var root = new TreeNode("Root");

            using var sw = new StringWriter();
            Console.SetOut(sw);

            root.Print();

            var output = sw.ToString().Trim();

            Assert.That(output, Is.EqualTo("Root"));
        }

        [Test]
        public void TreeStructure_MatchesConfiguration()
        {
            var root = new TreeNode("A");
            var b = new TreeNode("B");
            var c = new TreeNode("C");
            var d = new TreeNode("D");
            var e = new TreeNode("E");

            root.AddChild(b);
            root.AddChild(c);
            b.AddChild(d);
            b.AddChild(e);

            Assert.That(root.Children.Count, Is.EqualTo(2));
            Assert.That(root.Children[0].Value, Is.EqualTo("B"));
            Assert.That(root.Children[1].Value, Is.EqualTo("C"));
            Assert.That(root.Children[0].Children.Count, Is.EqualTo(2));
            Assert.That(root.Children[0].Children[0].Value, Is.EqualTo("D"));
            Assert.That(root.Children[0].Children[1].Value, Is.EqualTo("E"));
            Assert.That(root.Children[1].Children.Count, Is.EqualTo(0));
        }
    }
}
