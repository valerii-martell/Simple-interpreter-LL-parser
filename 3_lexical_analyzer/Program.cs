using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    static class LexicalAnalyzer
    {
        private static string[] terminals = new string[]
        {"==", "++", "--", "=", "?", ":", "*", "[", "]", ";", "{", "}", "+", "-", "/", "(", ")", "<", ">"};
        public static string LexicalAnalyze(this string analyzedString)
        {
            string result = "";
            string[] variables = analyzedString.Split(terminals, StringSplitOptions.None);
            foreach (string str in variables)
            {
                if (!str.Equals(""))
                {
                    try
                    {
                        int value = Int32.Parse(str);
                        result += value + " is a number\n";
                    }
                    catch (Exception e)
                    {
                        result += str + " is a variable\n";
                    }
                }
            }
            foreach (string token in terminals)
            {
                if (analyzedString.Contains(token))
                {
                    analyzedString = analyzedString.Replace(token, "");
                    result += token + " is a token\n";
                }
            }
            return result;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string str = "b=c?d:12*a*[m];";
            Console.WriteLine("Input string:  " + str);
            Console.WriteLine(str.LexicalAnalyze());

            Console.ReadKey();
        }
    }
}
