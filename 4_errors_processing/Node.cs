using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    class Node
    {
        private static int count = 0;

        private int id;
        private string value;
        private Node left;
        private Node rigth;

        public Node(string value)
        {
            this.value = value;
            this.id = count;
            count++;
        }
        public string Value
        {
            get { return value; }
        }
        public Node Left
        {
            get { return left; }
            set { this.left = value; }
        }
        public Node Rigth
        {
            get { return rigth; }
            set {this.rigth=value;}
        }
        public void print()
        {
            Console.WriteLine(("Node {0}; Value: {1}; Left: {2}; Right: {3}"), id, value,
                    left == null ? "<none>" : left.Value,
                    rigth == null ? "<none>" : rigth.Value);
        }
    }
}
