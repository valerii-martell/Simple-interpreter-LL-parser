using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
    class Program
    {
        public static void Main(String[] args)
        {
            string program = "float b, d, a[5]; int c; b=2,0; d=1,0; a[c] = c / 2,0; b!=c?d:2*a[c]; b = b + 100;";
            //string program = "float b, a, c, d; b=2,0; c=2; d=1,0; a[c] = c / 2,0;";
            Console.WriteLine("Input string: " + program);

            try
            {
                program.Analyze();
                program.Interprete();

            }
            catch (ParseException e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadKey();
        }
    }
}
