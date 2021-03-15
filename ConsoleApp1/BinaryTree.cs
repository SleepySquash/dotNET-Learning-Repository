using System;

namespace ConsoleApp1
{
    namespace BinaryTree
    {
        public class Node<T>
        {
            public T Data;
            public Node<T> Left;
            public Node<T> Right;

            public Node(T data, Node<T> left = null, Node<T> right = null)
            {
                Data = data;
                Left = left;
                Right = right;
            }
        }

        public class Tree<T> where T : IComparable
        {
            private Node<T> _head;

            public void Insert(T data)
            {
                _Insert(ref _head, data);
            }

            private void _Insert(ref Node<T> node, T data)
            {
                if (node == null) node = new Node<T>(data);
                else if (node.Data.CompareTo(data) > 0)
                    _Insert(ref node.Left, data);
                else if (node.Data.CompareTo(data) < 0)
                    _Insert(ref node.Right, data);
            }

            public void Print()
            {
                _Print(ref _head);
                Console.WriteLine();
            }

            private void _Print(ref Node<T> node)
            {
                if (node == null) return;
                if (node.Left == null && node.Right == null) Console.Write(node.Data);
                else if (node.Right == null)
                {
                    Console.Write(node.Data + " [");
                    _Print(ref node.Left);
                    Console.Write(";]");
                }
                else
                {
                    Console.Write(node.Data + " [");
                    _Print(ref node.Left);
                    Console.Write("; ");
                    _Print(ref node.Right);
                    Console.Write("]");
                }
            }

            public Node<T> Search(T data)
            {
                return _Search(ref _head, data);
            }

            private Node<T> _Search(ref Node<T> node, T data)
            {
                if (node == null) return null;
                if (node.Data.Equals(data)) return node;
                return node.Data.CompareTo(data) > 0 ? _Search(ref node.Left, data) : _Search(ref node.Right, data);
            }

            public Node<T> FindMinimum()
            {
                return _FindMinimum(ref _head);
            }
            
            private Node<T> _FindMinimum(ref Node<T> node)
            {
                if (node.Left == null) return node;
                return _FindMinimum(ref node.Left);
            }
            
            private Node<T> _DeleteMinimum(Node<T> node, Node<T> parent)
            {
                if (node.Left != null) return _DeleteMinimum(node.Left, node);
                _DeleteNode(node, parent);
                return node;
            }

            public void DeleteValue(T data)
            {
                _DeleteValue(_head, null, data);
            }

            private void _DeleteNode(Node<T> node, Node<T> parent)
            {
                // deleting ...
                if (node.Left == null && node.Right == null)
                {
                    if (parent == null) _head = null;
                    else if (parent.Left == node) parent.Left = null;
                    else if (parent.Right == node) parent.Right = null;
                }
                else if (node.Left == null)
                {
                    if (parent == null) _head = node.Right;
                    else if (parent.Left == node) parent.Left = node.Right;
                    else if (parent.Right == node) parent.Right = node.Right;
                }
                else if (node.Right == null)
                {
                    if (parent == null) _head = node.Left;
                    else if (parent.Left == node) parent.Left = node.Left;
                    else if (parent.Right == node) parent.Right = node.Left;
                }
                else
                {
                    Node<T> min = _DeleteMinimum(node.Right, node);
                    min.Left = node.Left;
                    min.Right = node.Right;
                    
                    if (parent == null) _head = min;
                    else if (parent.Left == node) parent.Left = min;
                    else if (parent.Right == node) parent.Right = min;
                    // 123
                }
            }
            
            private void _DeleteValue(Node<T> node, Node<T> parent, T data)
            {
                if (node == null) return;
                if (node.Data.Equals(data)) _DeleteNode(node, parent);
                else
                {
                    _DeleteValue(node.Left, node, data);
                    _DeleteValue(node.Right, node, data);
                }
            }
        }
    }
}