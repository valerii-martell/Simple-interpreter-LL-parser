using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    class Tree
    {
        private List<Token> nodes = new List<Token>();
        private StringBuilder result = new StringBuilder();
        private char[] terminals = new char[] { '=', '?', ']', '[', '*', ':' };

        public Tree()
        {
            Token node1 = new Token('b', null, null, false);
            Token node2 = new Token('c', null, null, false);
            Token node3 = new Token('=', node1, node2, true);
            Token node4 = new Token('d', null, null, false);
            Token node5 = new Token('?', node3, node4, true);
            Token node6 = new Token('n', null, null, false);
            Token node7 = new Token(']', node6, null, true);
            Token node8 = new Token('a', null, null, false);
            Token node9 = new Token('[', node8, node7, true);
            Token node10 = new Token('4', null, null, false);
            Token node11 = new Token('*', node10, node9, true);
            Token node12 = new Token(':', node5, node11, true);

            node1.Parent = node3;
            node2.Parent = node3;
            node3.Parent = node5;
            node4.Parent = node5;
            node5.Parent = node12;
            node6.Parent = node7;
            node7.Parent = node9;
            node8.Parent = node9;
            node9.Parent = node11;
            node10.Parent = node11;
            node11.Parent = node12;

            nodes.Add(node1);
            nodes.Add(node2);
            nodes.Add(node3);
            nodes.Add(node4);
            nodes.Add(node5);
            nodes.Add(node6);
            nodes.Add(node7);
            nodes.Add(node8);
            nodes.Add(node9);
            nodes.Add(node10);
            nodes.Add(node11);
            nodes.Add(node12);
        }
        public void print()
        {
            Token root = Root(nodes[0]);
            Search(root);
            Console.WriteLine(result);
        }
        private void Search(Token node)
        {
            if (node.Left != null)
            {
                Search(node.Left);
            }
            result.Append(node.Value);
            if (node.Right != null)
            {
                Search(node.Right);
            }
        }
        private Token Root(Token node)
        {
            Token result = node;
            if (node.Parent != null)
            {
                result = Root(node.Parent);
            }
            return result;
        }
    }
}
