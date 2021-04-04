using System;
using System.Collections;
using System.Collections.Generic;

namespace Lab_1
{
    public class LinkedList<T> : IEnumerable<T>
    {
        private class Node<T>
        {
            public Node(T data)
            {
                Data = data;
            }

            public T Data { get; set; }
            public Node<T> Next { get; set; }
        }
        
        private Node<T> Head;
        private Node<T> Tail;

        private int Count;

        public void Add(T data)
        {
            Node<T> node = new Node<T>(data);
            if (Head == null)
                Head = node;
            else
                Tail.Next = node;
            Tail = node;
            ++Count;
        }

        public bool Remove(T data)
        {
            Node<T> current = Head;
            Node<T> prev = null;

            while (current != null)
            {
                if (current.Data.Equals(data))
                    // removing
                {
                    if (prev == null)
                    {
                        Head = current.Next;
                        if (Head == null)
                            Tail = null;
                    }
                    else
                    {
                        prev.Next = current.Next;

                        if (current.Next == null)
                            Tail = prev;
                    }

                    --Count;
                    return true;
                }

                prev = current;
                current = current.Next;
            }

            return false;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<T>) this).GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            Node<T> current = Head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }

        public void Reverse()
        {
            if (Head == null || Head == Tail)
                return;
            
            Node<T> current = Head;
            Node<T> prev = null;

            Tail = Head;
            
            while (current != null)
            {
                Node<T> next = current.Next;
                current.Next = prev;

                prev = current;
                current = next;

            }

            Head = prev;
        }
    }
}