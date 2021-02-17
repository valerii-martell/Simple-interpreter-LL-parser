using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    enum Signal
    {
        dlm,
        ltr
    }

    static class SignalOperations
    {
        private static void dlm(Graph graph)
        {
            if (graph.Current != null)
            {
                Console.Write("dlm\t" + graph.Current.Id + "\t" + ">\t");
                graph.Current = graph.Current.Dlm;
                Console.WriteLine(graph.Current.Id);
            }
        }
        private static void ltr(Graph graph)
        {
            if (graph.Current != null)
            {
                Console.Write("ltr\t" + graph.Current.Id + "\t" + ">\t");
                graph.Current = graph.Current.Ltr;
                Console.WriteLine(graph.Current.Id);
            }
        }
        public static void Signal(this Graph graph, Signal signal)
        {
            if (graph.Current.Equals(graph.Finish))
            {
                return;
            }
            if (signal.Equals(Lab2.Signal.ltr))
            {
                ltr(graph);
            }
            else if (signal.Equals(Lab2.Signal.dlm))
            {
                dlm(graph);
            }
            else
            {
                Console.WriteLine(signal + "\t" + graph.Current.Id + "\t->\tERROR");
                graph.Current = null;
            }
        }
    }

    class Graph
    {
        private List<Node> nodes;
        private Node current;
        private Node finish;

        public Graph()
        {
            nodes = new List<Node>();

            Node node1 = new Node(1);
            Node node2 = new Node(2);
            Node node3 = new Node(3);
            Node node4 = new Node(4);
            Node node5 = new Node(5);
            Node node6 = new Node(6);
            Node node7 = new Node(7);
            Node node8 = new Node(8);
            Node node9 = new Node(9);

            node1.Dlm = node2;
            node1.Ltr = node2;

            node2.Dlm = node3;
            node2.Ltr = node3;

            node3.Dlm = node3;
            node3.Ltr = node4;

            node4.Dlm = node5;
            node4.Ltr = node5;

            node5.Dlm = node6;
            node5.Ltr = node9;

            node6.Dlm = node7;
            node6.Ltr = node7;

            node7.Dlm = node8;
            node7.Ltr = node8;

            node8.Dlm = node9;
            node8.Ltr = node9;

            node9.Dlm = node9;
            node9.Ltr = node9;

            nodes.Add(node1);
            nodes.Add(node2);
            nodes.Add(node3);
            nodes.Add(node4);
            nodes.Add(node5);
            nodes.Add(node6);
            nodes.Add(node7);
            nodes.Add(node8);
            nodes.Add(node9);

            current = node1;
            finish = node9;
        }

        public Node Current
        {
            get { return this.current; }
            set { this.current = value; }
        }
        public Node Finish
        {
            get { return this.finish; }
            set { this.finish = value; }
        }
    }
}
