using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            Tree tree = new Tree();
            tree.print();
            Console.WriteLine();

            Graph graph = new Graph();

            graph.Signal(Signal.ltr);
            graph.Signal(Signal.dlm);
            graph.Signal(Signal.dlm);
            graph.Signal(Signal.dlm);
            graph.Signal(Signal.dlm);
            graph.Signal(Signal.dlm);
            graph.Signal(Signal.ltr);
            graph.Signal(Signal.ltr);
            graph.Signal(Signal.dlm);
            graph.Signal(Signal.dlm);
            graph.Signal(Signal.dlm);
            graph.Signal(Signal.dlm);

            Console.WriteLine();

            graph = new Graph();
            graph.Signal(Signal.ltr);
            graph.Signal(Signal.dlm);
            graph.Signal(Signal.dlm);
            graph.Signal(Signal.dlm);
            graph.Signal(Signal.dlm);
            graph.Signal(Signal.dlm);
            graph.Signal(Signal.ltr);
            graph.Signal(Signal.ltr);
            graph.Signal(Signal.ltr);

            Console.ReadKey();
        }
    }
}
