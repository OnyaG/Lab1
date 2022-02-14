using System;
using System.Linq;
using System.Collections.Generic;


namespace FirstLab
{
    public class Tree
    {
        public class Node
        {
            public int key;
            public Node parent, left, right;

            public Node(int newKey)
            {
                key = newKey;
                parent = left = right = null;
            }
        }

        public Node root = null;

        public static void Insert(ref Tree T, int newKey)
        {
            Node parentNode = null;
            Node currentNode = T.root;
            Node newNode = new Node(newKey);
            while (currentNode != null)
            {
                parentNode = currentNode;
                if (newNode.key < currentNode.key)
                {
                    currentNode = currentNode.left;
                }
                else
                {
                    currentNode = currentNode.right;
                }
            }
            newNode.parent = parentNode;
            if (parentNode == null)
            {
                T.root = newNode;
            }
            else if(newNode.key<parentNode.key){
                parentNode.left = newNode;                
            }
            else
            {
                parentNode.right = newNode;
            }
        }

        public static void InorderTreeWalk(Node node)
        {
            if (node != null)
            {
                InorderTreeWalk(node.left);
                Console.WriteLine(node.key);
                InorderTreeWalk(node.right);
            }
        }

        public static Node Minimum(Node node)
        {
            while (node.left != null)
            {
                node = node.left;
            }
            return node;
        }
        public static Node Maximum(Node node)
        {
            while (node.right != null)
            {
                node = node.right;
            }
            return node;
        }

        public static Node Predecessor(Node node)
        {
            Node predecessorNode = null;
            
            if (node.left != null)
            {
                return Maximum(node);
            }
            predecessorNode = node.parent;
            while ((predecessorNode!=null) & (node == predecessorNode.left))
            {
                node = predecessorNode;
                predecessorNode = predecessorNode.parent;
            }
            return predecessorNode;
        }

        public static Node Successor(Node node)
        {
            Node successorNode = null;

            if (node.right != null)
            {
                return Minimum(node);
            }
            successorNode = node.parent;
            while ((successorNode != null) & (node == successorNode.right))
            {
                node = successorNode;
                successorNode = successorNode.parent;
            }
            return successorNode;
        }

        public static Node Search(Node node, int keyValue)
        {
            while (node!=null | node.key!=keyValue)
            {
                if (keyValue < node.key)
                {
                    node = node.left;
                }
                else
                {
                    node = node.right;
                }
            }
            return node;
        }

        /*
        public static void LeftRotate(ref Tree T, Node leftNode)
        {
            Node rightNode = leftNode.right;
            leftNode.right = rightNode.left; // левое поддерево правого(дочернего) узла
                                             // отдаём корневому узлу в правое поддерево
            if (rightNode.left != null)
            {
                rightNode.left.parent = leftNode;
            }
            leftNode.parent = rightNode.parent; //родителя в потомка
            if (leftNode.parent == null)
            {
                T.root = rightNode;
            }
            else if (leftNode == leftNode.parent.left)
            {
                leftNode.parent.left = rightNode;
            }
            else
            {
                leftNode.parent.right = rightNode;
            }
            rightNode.left = leftNode;

            leftNode.parent = rightNode;

        }
        public static void RightRotate(ref Tree T, Node rightNode)
        {
            Node leftNode = rightNode.left;
            leftNode.left = rightNode.right;

        }
        */


        public static int Depth(Node node)
        {
            int depth = 0;
            if (node != null)
            {
                int leftD = Depth(node.left);
                int rightD = Depth(node.right);
                depth = Math.Max(leftD, rightD)+1;
            }
            return depth;
        }

        public static void Print(Tree T)
        {
            Queue<Node> nodes = new Queue<Node>();
            Node node = T.root;
            int depth = Depth(node);
            int numberOfSpaces;
            int elemPerLevel;
            string levelPrint = "";
            nodes.Enqueue(T.root);
            for (int level = 0; level < depth; level++)
            {
                numberOfSpaces = Convert.ToInt32(Math.Pow(2, (depth - 1 - level))) - 1;
                elemPerLevel = Convert.ToInt32(Math.Pow(2, level));
                for (int elemPrint = 0; elemPrint < elemPerLevel; elemPrint++)
                {
                    node = nodes.Dequeue();
                    if (node != null)
                    {
                        nodes.Enqueue(node.left);
                        nodes.Enqueue(node.right);
                    }
                    else
                    {
                        nodes.Enqueue(null);
                        nodes.Enqueue(null);
                    }
                    levelPrint = string.Concat(levelPrint, string.Concat(Enumerable.Repeat(" ", numberOfSpaces)));
                    if (node != null)
                    {
                        levelPrint = string.Concat(levelPrint, node.key);
                    }
                    else
                    {
                        levelPrint = string.Concat(levelPrint, ".");
                    }
                    levelPrint = string.Concat(levelPrint, string.Concat(Enumerable.Repeat(" ", numberOfSpaces + 1)));
                }

                Console.WriteLine(levelPrint);
                levelPrint = "";
            }
            nodes.Clear();

        }
        public static void PrintPoints(Tree T)
        {
            Node node = T.root;
            int depth = Depth(node);
            int numberOfSpaces;
            int elemPerLevel;
            string levelPrint = "";
            for (int level = 0; level < depth; level++)
            {
                numberOfSpaces = Convert.ToInt32(Math.Pow(2, (depth - 1-level))) - 1;
                elemPerLevel = Convert.ToInt32(Math.Pow(2, level));
                for (int elemNumber = 0; elemNumber< elemPerLevel; elemNumber++)
                {
                    levelPrint = string.Concat(levelPrint, string.Concat(Enumerable.Repeat(" ", numberOfSpaces)));
                    levelPrint = string.Concat(levelPrint, ".");
                    levelPrint = string.Concat(levelPrint, string.Concat(Enumerable.Repeat(" ", numberOfSpaces+1)));

                }
                Console.WriteLine(levelPrint);
                levelPrint = "";
            }
        }
    }
    class Input
    {
        public static Tree MyInput()
        {
            Console.WriteLine("Введите количество узлов вашего дерева:");
            int numberOfNodes = Convert.ToInt32(Console.ReadLine());
            Tree T = new Tree();
            for (int i = 0; i < numberOfNodes; i++)
            {
                Console.WriteLine("Введите узел " + (i + 1) + ":");
                int newKey = Convert.ToInt32(Console.ReadLine());
                Tree.Insert(ref T, newKey);
            }
            return T;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {


            Console.WriteLine("Hello World!");
            Console.WriteLine("Ташкова Анна 403 группа" + Environment.NewLine + 
                "Вариант №8(4):" + Environment.NewLine + 
                "Splay - дерево");

            Console.WriteLine("---------------------" + Environment.NewLine);
            Tree T = Input.MyInput();
            Tree.InorderTreeWalk(T.root);
            Tree.Print(T);
            //Tree.LeftRotate(ref T, T.root);
            //Tree.Print(T);
        }

    }
}
