using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Lab_1
{
    public enum Side
    {
        Left,
        Right
    }
    
    public class Node<T>
    {
        public T Data { get; set; }

        public Node(T data)
        {
            Data = data;
        }

        public Node<T> LeftChild { get; set; }
        public Node<T> RightChild { get; set; }
        public Node<T> Parent { get; set; }

        public Side? NodeSide => Parent == null
            ? (Side?) null
            : Parent.LeftChild == this
                ? Side.Left
                : Side.Right;
    }
    
    public class BinaryTree<T> where T : IComparable
    {
        private Node<T> Root;

        private Node<T> Add(Node<T> node, Node<T> currentNode = null)
        {
            if (Root == null)
            {
                node.Parent = null;
                Root = node;
                return Root;
            }

            currentNode = (currentNode != null ? currentNode : Root);
            node.Parent = currentNode;
            int result = node.Data.CompareTo(currentNode.Data);
            
            if (result == 0)
                return currentNode;
            
            if (result < 0)
                return currentNode.LeftChild == null ? currentNode.LeftChild = node : Add(node, currentNode.LeftChild);
            else // result > 0
                return currentNode.RightChild == null ? currentNode.RightChild = node : Add(node, currentNode.RightChild);
        }

        public Node<T> Add(T data)
        {
            return Add(new Node<T>(data));
        }

        public Node<T> Find(T data, Node<T> startingNode = null)
        {
            startingNode = startingNode ?? Root;
            int result = data.CompareTo(startingNode.Data);

            if (result == 0)
                return startingNode;
            if (result < 0)
                return startingNode.LeftChild == null ? null : Find(data, startingNode.LeftChild);
            else // result > 0
                return startingNode.RightChild == null ? null : Find(data, startingNode.RightChild);
        }

        public void Remove(Node<T> node)
        {
            if (node == null)
                return;

            Side? side = node.NodeSide;
            if (node.LeftChild == null && node.RightChild == null)
            {
                if (side == Side.Left)
                    node.Parent.LeftChild = null;
                else
                    node.Parent.RightChild = null;
            }
            else if (node.LeftChild == null)
            {
                if (side == Side.Left)
                    node.Parent.LeftChild = node.Parent.RightChild;
                else
                    node.Parent.RightChild = node.Parent.RightChild;

                node.RightChild.Parent = node.Parent;
            }
            else if (node.RightChild == null)
            {
                if (side == Side.Left)
                    node.Parent.LeftChild = node.LeftChild;
                else
                    node.Parent.RightChild = node.LeftChild;

                node.LeftChild.Parent = node.Parent;
            }
            else
                switch (side)
                {
                    case Side.Left:
                        node.Parent.LeftChild = node.RightChild;
                        node.RightChild.Parent = node.Parent;
                        Add(node.LeftChild, node.RightChild);
                        break;
                    case Side.Right:
                        node.Parent.RightChild = node.RightChild;
                        node.RightChild.Parent = node.Parent;
                        Add(node.LeftChild, node.RightChild);
                        break;
                    default:
                        var bufLeft = node.LeftChild;
                        var bufRightLeft = node.RightChild.LeftChild;
                        var bufRightRight = node.RightChild.RightChild;
                        node.Data = node.RightChild.Data;
                        node.RightChild = bufRightRight;
                        node.LeftChild = bufRightLeft;
                        Add(bufLeft, node);
                        break;
                }
        }
        
        public void Remove(T data)
        {
            var node = Find(data);
            Remove(node);
        }
        
        public void PrintTree()
        {
            PrintTree(Root);
        }
        
        private void PrintTree(Node<T> startNode, string indent = "", Side? side = null)
        {
            if (startNode != null)
            {
                var nodeSide = side == null ? "+" : side == Side.Left ? "L" : "R";
                Console.WriteLine($"{indent} [{nodeSide}]- {startNode.Data}");
                indent += new string(' ', 3);
                //рекурсивный вызов для левой и правой веток
                PrintTree(startNode.LeftChild, indent, Side.Left);
                PrintTree(startNode.RightChild, indent, Side.Right);
            }
        }
    }
}