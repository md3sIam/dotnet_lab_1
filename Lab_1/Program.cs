using System;
using System.Collections;

namespace Lab_1
{
    class Program
    {
        static int Main(string[] args)
        {
            TestList();
            TestTree();
            TestSort();
            return 0;
        }

        static T[] InsertionSort<T>(T[] array) where T : IComparable
        {
            for (int i = 1; i < array.Length; ++i)
            {
                T item = array[i];
                int j = i;
                while (j > 0 && array[j - 1].CompareTo(item) > 0)
                {
                    array[j] = array[j - 1];
                    --j;
                }

                array[j] = item;
            }

            return array;
        }

        static void TestSort()
        {
            Console.WriteLine("Testing sort -------------------------------------");
            int[] array = new int[8] { 6, 5, 3, 1, 8, 7, 2, 4};
            
            Console.WriteLine("Not sorted: {0}\r\nSorted    : {1}", 
                string.Join(", ", array), string.Join(", ", InsertionSort(array)));
        }

        static void TestTree()
        {
            Console.WriteLine("Testing tree -------------------------------------");
            var tree = new BinaryTree<int>();
        
            tree.Add(8);
            tree.Add(3);
            tree.Add(10);
            tree.Add(1);
            tree.Add(6);
            tree.Add(4);
            tree.Add(7);
            tree.Add(14);
            tree.Add(16);

            Console.WriteLine("Tree filled:");
            tree.PrintTree();
            
            Console.WriteLine("Removing 3:");
            tree.Remove(3);
            tree.PrintTree();

            Console.WriteLine("removing 8:");
            tree.Remove(8);
            tree.PrintTree();
        }

        static void TestList()
        {
            Console.WriteLine("Testing list -------------------------------------");
            LinkedList<int> list = new LinkedList<int>();
            for (int i = 0; i < 10; ++i)
                list.Add(i);
            
            Print(list, "List filled:");

            list.Remove(6);
            Print(list, "Removing 6:");

            list.Remove(0);
            Print(list, "Removing 0:");

            list.Remove(9);
            Print(list, "Removing 9:");

            list.Reverse();
            Print(list, "Reversed:");
            
            list.Reverse();
            Print(list, "Reversed again: ");
        }

        static void Print<T>(T int_list, String comment) where T : IEnumerable
        {
            Console.WriteLine(comment);
            foreach(int i in int_list)
                Console.Write("{0:D} ", i);
            Console.WriteLine();
        }
    }
}