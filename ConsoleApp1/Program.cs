using System;
using ConsoleApp1.BinaryTree;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine();
            LinkedList.List<int> list = new LinkedList.List<int>();
            list.PushFront(1);
            list.PushFront(2);
            list.PushLast(4);
            list.PushLast(3);
            list.InsertAfter(2, 5);
            list.Sort(true);
            list.Invert();
            foreach (var i in list)
                Console.WriteLine("i is " + Convert.ToString(i));


            Console.WriteLine();
            BinaryTree.Tree<int> tree = new Tree<int>();
            tree.Insert(2);
            tree.Insert(1);
            tree.Insert(4);
            tree.Insert(3);
            tree.Insert(6);
            tree.Insert(5);
            tree.DeleteValue(4);
            tree.Print();
        }
    }
}