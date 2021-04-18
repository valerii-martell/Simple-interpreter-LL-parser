using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automat
{
    enum Signal
    {
        dlm,
        cfr,
        dif,
        err
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
        private static void cfr(Graph graph)
        {
            if (graph.Current != null)
            {
                Console.Write("cfr\t" + graph.Current.Id + "\t" + ">\t");
                graph.Current = graph.Current.Cfr;
                Console.WriteLine(graph.Current.Id);
            }
        }
        private static void dif(Graph graph)
        {
            if (graph.Current != null)
            {
                Console.Write("dif\t" + graph.Current.Id + "\t" + ">\t");
                graph.Current = graph.Current.Dif;
                Console.WriteLine(graph.Current.Id);
            }
        }
        public static void Signal(this Graph graph, Signal signal)
        {
            if (graph.Current.Equals(graph.Finish))
            {
                return;
            }
            if (signal.Equals(Automat.Signal.cfr))
            {
                cfr(graph);
            }
            else if (signal.Equals(Automat.Signal.dlm))
            {
                dlm(graph);
            }
            else if (!signal.Equals(Automat.Signal.dlm) && !signal.Equals(Automat.Signal.cfr))
            {
                dif(graph);
            }
            else
            {
                //throw new GraphException("Unknow signal " + "\t" + graph.Current.Id + "\t->\tERROR;");
                //Console.WriteLine(signal.ToString() + "\t" + graph.Current.Id + "\t->\tERROR. Unknown signal;");
                //graph.Current = graph.Finish;
            }
        }
    }

    class GraphException : Exception
    {
        public GraphException(string message) : base(message)
        {
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

            Node node0 = new Node(0);
            Node node1 = new Node(1);
            Node node2 = new Node(2);
            Node node3 = new Node(3);
            Node node4 = new Node(4);
            Node node5 = new Node(5);
            Node node6 = new Node(6);
            Node node7 = new Node(7);
            Node node8 = new Node(8);
            Node node9 = new Node(9);

            node0.Dlm = node1;
            node0.Cfr = node1;

            node1.Dlm = node2;
            node1.Cfr = node2;

            node2.Dlm = node3;
            node2.Cfr = node3;

            node3.Dlm = node7;
            node3.Cfr = node4;

            node4.Dlm = node5;
            node4.Cfr = node5;

            node5.Dlm = node6;
            node5.Cfr = node8;

            node6.Dlm = node7;
            node6.Cfr = node7;

            node7.Dlm = node8;
            node7.Cfr = node8;

            node8.Dlm = node9;
            node8.Cfr = node9;

            node9.Dlm = node9;
            node9.Cfr = node9;

            nodes.Add(node0);
            nodes.Add(node1);
            nodes.Add(node2);
            nodes.Add(node3);
            nodes.Add(node4);
            nodes.Add(node5);
            nodes.Add(node6);
            nodes.Add(node7);
            nodes.Add(node8);
            nodes.Add(node9);

            current = node0;
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
