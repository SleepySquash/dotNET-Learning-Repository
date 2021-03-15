using System;
using System.Collections;
using System.Collections.Generic;

namespace ConsoleApp1
{
    namespace LinkedList
    {
        internal class Node<T>
        {
            public T Data;
            public Node<T> Next;

            public Node(T data, Node<T> next = null)
            {
                this.Data = data;
                this.Next = next;
            }
        }

        internal class NodeEnumerator<T> : IEnumerator<T>
        {
            public T Current { get; private set; }

            private Node<T> _current;
            private readonly Node<T> _head;

            public NodeEnumerator(Node<T> head)
            {
                this._head = head;
                _current = head;
            }

            public bool MoveNext()
            {
                if (_current == null) return false;
                Current = _current.Data;
                _current = _current.Next;
                return true;
            }

            public void Reset()
            {
                _current = _head;
            }

            object IEnumerator.Current => Current;

            public void Dispose()
            {

            }
        }

        public class List<T> : IEnumerable<T> where T: IComparable
        {
            private Node<T> _head;
            private Node<T> _tail;
            
            public T First
            {
                get => _head != null ? _head.Data : default(T);
            }
            public T Last
            {
                get => _tail != null ? _tail.Data : default(T);
            }

            IEnumerator<T> IEnumerable<T>.GetEnumerator()
            {
                return new NodeEnumerator<T>(_head);
            }

            public IEnumerator GetEnumerator()
            {
                return new NodeEnumerator<T>(_head);
            }

            public void PushFront(T data)
            {
                Node<T> node = new Node<T>(data, _head);
                if (_head == null) _tail = node;
                _head = node;
            }

            public void PushLast(T data)
            {
                Node<T> node = new Node<T>(data);
                if (_tail != null) _tail.Next = node;
                _tail = node;
            }

            public void InsertAfter(int position, T data)
            {
                int pos = 0;
                Node<T> current = _head;
                while (pos < position && current.Next != null)
                {
                    pos++;
                    current = current.Next;
                }

                if (current != null)
                {
                    if (current == _tail) PushLast(data);
                    else if (current == _head) PushFront(data);
                    else
                    {
                        Node<T> node = new Node<T>(data);
                        Node<T> next = current.Next;
                        node.Next = next;
                        current.Next = node;
                    }
                }
                else PushFront(data);
            }

            public void RemoveValue(T data)
            {
                Node<T> curr = _head;
                Node<T> prev = null;
                while (curr != null)
                {
                    Node<T> next = curr.Next;
                    if (curr.Data.Equals(data))
                    {
                        if (prev != null)
                        {
                            prev.Next = next;
                            if (next == null) _tail = prev;
                        }
                        else _head = next;

                        curr.Next = null;
                    }
                    else prev = curr;

                    curr = next;
                }

                if (_head == null) _tail = null;
            }

            public void Invert()
            {
                Node<T> curr = _head;
                Node<T> prev = null;
                _head = _tail;
                _tail = curr;
                while (curr != null)
                {
                    Node<T> next = curr.Next;
                    curr.Next = prev;
                    prev = curr;
                    curr = next;
                }
            }

            public void Sort(bool ascending = true)
            {
                if (_head?.Next == null) return;
                
                Node<T> t1 = _head.Next;
                while (t1 != null)
                {
                    T data = t1.Data;
                    bool found = false;
                    
                    Node<T> t2 = _head;
                    while (t1 != t2)
                    {
                        if ((ascending ? t2.Data.CompareTo(t1.Data) > 0 : t2.Data.CompareTo(t1.Data) < 0) && !found)
                        {
                            data = t2.Data;
                            t2.Data = t1.Data;
                            found = true;
                            t2 = t2.Next;
                        }
                        else
                        {
                            if (found)
                            {
                                T temp = data;
                                data = t2.Data;
                                t2.Data = temp;
                            }

                            t2 = t2.Next;
                        }
                    }

                    t2.Data = data;
                    t1 = t1.Next;
                }
            }
        }
    }
}