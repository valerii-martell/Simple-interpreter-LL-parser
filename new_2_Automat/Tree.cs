using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automat
{
    class Tree
    {
        private List<Token> nodes = new List<Token>();
        private StringBuilder result = new StringBuilder();
        //private char[] terminals = new char[] { '=', '?', ']', '[', '*', ':' };

        public Tree()
        {
            //if(c)b=(2*a +c/d)*2*a;
            Token node1 = new Token("2", null, null, false);
            Token node2 = new Token("a", null, null, false);
            Token node3 = new Token("c", null, null, false);
            Token node4 = new Token("d", null, null, false);
            Token node5 = new Token("2", null, null, false);
            Token node6 = new Token("a", null, null, false);
            Token node7 = new Token("/", node1, node2, false);
            Token node8 = new Token("*", node3, node4, false);
            Token node9 = new Token("*", node5, node6, false);
            Token node10 = new Token("+", node7, node8, false);
            Token node11 = new Token("*", null, node9, false);
            Token node12 = new Token("()", node10, node11, true); 
            Token node13 = new Token("b", null, null, false);
            //
            Token node14 = new Token("=", node12, node13, false);
            Token node15 = new Token("c", null, null, false);
            Token node16 = new Token("()", node15, node14, true);
            Token node17 = new Token("if", null, node16, false);

            node1.Parent = node7;
            node2.Parent = node7;
            node3.Parent = node8;
            node4.Parent = node8;
            node5.Parent = node9;
            node6.Parent = node9;
            node7.Parent = node10;
            node8.Parent = node10;
            node9.Parent = node11;
            node10.Parent = node12;
            node11.Parent = node12;
            node12.Parent = node14;
            node13.Parent = node14;
            node14.Parent = node16;
            node16.Parent = node17;

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
            nodes.Add(node13);
            nodes.Add(node14);
            nodes.Add(node15);
            nodes.Add(node16);
            nodes.Add(node17);
        }
        public void print()
        {
            Token root = Root(nodes.Last());
            Search(root);
            //Console.WriteLine(result);
            Console.WriteLine("if(c)b=(2*a+c/d)*2*a;");
        }
        private void Search(Token node)
        {
            if (node.Left != null && node.IsTerminal == false)
            {
                Search(node.Left);
            }
            if (node.IsTerminal == true)
            {
                string res = result + "(" + node.Left.Value + ")";
                result = new StringBuilder(res);            
            }
            else
            {
                result.Append(node.Value);
            }
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
