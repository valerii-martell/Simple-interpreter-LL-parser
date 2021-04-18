using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automat
{
    class Program
    {
        static void Main(string[] args)
        {
            Tree tree = new Tree();
            tree.print();

            Console.WriteLine();
            Console.WriteLine("Стани 0..9; 3->7(dlm), 5->8(cfr), n->n(dif)");
            
            Graph graph = new Graph();

            graph.Signal(Signal.cfr); //1
            graph.Signal(Signal.dlm); //2
            graph.Signal(Signal.dlm); //3
            graph.Signal(Signal.cfr); //4
            graph.Signal(Signal.dif); //4
            graph.Signal(Signal.dif); //4
            graph.Signal(Signal.dlm); //5 
            graph.Signal(Signal.dlm); //6
            graph.Signal(Signal.dif); //6
            graph.Signal(Signal.dlm); //7
            graph.Signal(Signal.dlm); //8
            graph.Signal(Signal.dlm); //9

            Console.WriteLine();

            graph = new Graph();
            graph.Signal(Signal.dlm); //1
            graph.Signal(Signal.cfr); //2
            graph.Signal(Signal.cfr); //3
            graph.Signal(Signal.dlm); //7
            graph.Signal(Signal.dif); //7
            graph.Signal(Signal.dif); //7
            graph.Signal(Signal.dlm); //8 
            graph.Signal(Signal.cfr); //9

            Console.WriteLine();

            graph = new Graph();
            graph.Signal(Signal.dlm); //1
            graph.Signal(Signal.cfr); //2
            graph.Signal(Signal.dlm); //4
            graph.Signal(Signal.dlm); //5
            graph.Signal(Signal.dif); //5
            graph.Signal(Signal.cfr); //8
            graph.Signal(Signal.dif); //8
            graph.Signal(Signal.cfr); //9

            Console.WriteLine();

            graph = new Graph();
            graph.Signal(Signal.dlm); //1
            graph.Signal(Signal.cfr); //2
            graph.Signal(Signal.err); //4
            graph.Signal(Signal.dlm); //5
            graph.Signal(Signal.dif); //5
            graph.Signal(Signal.cfr); //8
            graph.Signal(Signal.dif); //8
            graph.Signal(Signal.cfr); //9
            
            Console.ReadKey();
        }
    }
}
