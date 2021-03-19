using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lab5
{

    class Program
    {
        public static void Main(String[] args)
        {
            string program = "if (a > b) then begin end else begin end;";
            Console.WriteLine("Input string: " + program);

            try
            {
                program.Analyze();
                Console.WriteLine("All ok.");
            }
            catch (ParseException e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadKey();
        }
    }
}





