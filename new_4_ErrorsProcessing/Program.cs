using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrorsProcessing
{
    class Program
    {
        public static void Main(String[] args)
        {
            /*string program =
                "begin " +
                    "if ((a>=b) and (b+2>c+a) or (b/2<>k-c)) then " +
                        "begin " +
                            "integer a:=b+1; k:=d+r; " +
                            "begin " +
                                "k:=(f+k)*g/(j-l); " +
                            "end; " +
                            "begin " +
                                "m:=l; " +//;
                            "end; " +
                            "begin " +

                            "end; " +
                        "end " +
                    "else " +
                        "if (a<>c) then " +
                            "begin " +
                                "real b:=a; " +
                            "end; " +
                "end;";
            //string program = "if (a>=b) then begin a:=b; end else begin a:=b; end;";*/
            string program = "if (a>b) then begin; c:=d; a:=b end;";
            Console.WriteLine("Input string: " + program);

            try
            {
                //Console.WriteLine("Input string:  " + program);
                //Console.WriteLine("Lexical analyze results: ");
                //Console.WriteLine(program.LexicalAnalyze());
                Console.WriteLine("Syntax analyze results: ");
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
