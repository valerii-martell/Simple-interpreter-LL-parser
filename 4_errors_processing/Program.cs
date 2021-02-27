using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            Analyzer analyzer = new Analyzer();
            Tree tree = analyzer.parse("b=c?d:b*a[n];");
            tree.print();

            Console.ReadKey();
        }

    }
}
