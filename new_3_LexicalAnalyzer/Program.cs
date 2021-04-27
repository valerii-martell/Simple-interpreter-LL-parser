using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LexicalAnalyzer
{
    static class LexicalAnalyzer
    {
        private static string[] spaces = new string[] {" ", "\n", "\t"};
        private static string[] terminals = new string[]
        {"==", "++", "--", "=", "?", ":", "*", "[", "]", ";", "{", "}", "+", "-", "/", "(", ")", "<", ">", "...", ","};
        private static string[] errors = new string[]
        {"?", ":", "]", ";", "}", ")", ",", ".", "'", "`"};
        private static string[] reservedWords = new string[]
        {"auto", "break", "case", "char", "const", "continue", "default", "do", "double", "else", "enum",
         "extern", "float", "for", "goto", "if", "inline", "int", "long", "register", "restrict", "return",
         "short", "signed", "sizeof", "static", "struct", "switch", "typedef", "union", "unsigned", "void",
         "volatile", "while", "_Alignas", "_Alignof", "_Atomic", "_Bool", "_Complex", "_Generic", "_Imaginary",
         "_Noreturn", "_Static_assert", "_Thread_local", "sin" };
        public static string LexicalAnalyze(this string analyzedString)
        {
            string result = "";
            if (analyzedString.Trim() == "")
            {
                result += "This string is empty!";
            }
            else
            {
                string[] rows = analyzedString.Split(spaces, StringSplitOptions.None);
                for (int i = 0; i < rows.Length; i++)
                {
                    rows[i] = rows[i].Trim();
                    //Console.WriteLine(rows[i]);
                }
                foreach (string row in rows)
                {
                    //Console.WriteLine(row);
                    if (row != "")
                    {
                        bool isNorm = true;
                        foreach (string error in errors)
                        {
                            if ((row.StartsWith(error)) && (!row.StartsWith(error + " ")))
                            {
                                int position = analyzedString.IndexOf(error);
                                var regex = new Regex(Regex.Escape(error));
                                var newText = regex.Replace(analyzedString, error, 1);
                                result += "Position " + position + ". This is a mistake! Variable name can't start with this symbol " + row + "\n";
                                isNorm = false;
                                break;
                            }
                        }
                        if (isNorm)
                        {
                            string[] sentences = row.Split(terminals, StringSplitOptions.None);
                            for (int i = 0; i < sentences.Length; i++)
                            {
                                sentences[i] = sentences[i].Trim();
                            }
                            foreach (string terminal in terminals)
                            {
                                if (row.Contains(terminal))
                                {
                                    int position = analyzedString.IndexOf(terminal);
                                    var regex = new Regex(Regex.Escape(terminal));
                                    var newText = regex.Replace(analyzedString, terminal, 1);
                                    result += "Position " + position + ". This is a terminal " + terminal + "\n";
                                }
                            }
                            foreach (string sentence in sentences)
                            {
                                if (sentence != "")
                                {
                                    //Console.WriteLine(sentence);
                                    int position = analyzedString.IndexOf(sentence);
                                    var regex = new Regex(Regex.Escape(sentence));
                                    var newText = regex.Replace(analyzedString, sentence, 1);
                                    try
                                    {
                                        int ivalue = Int32.Parse(sentence);
                                        double dvalue = Double.Parse(sentence);
                                        result += "Position " + position + ". This is a number " + sentence + "\n";
                                        continue;
                                    }
                                    catch (Exception e)
                                    {

                                        if (Regex.IsMatch(sentence, @"^\d+"))
                                        {
                                            result += "Position " + position + ". This is a mistake! Variable name can't start with number " + sentence + "\n";
                                            continue;
                                        }
                                        else
                                        {
                                            bool f1 = false, f2 = false;
                                            foreach (string reservedWord in reservedWords)
                                            {
                                                if (sentence == reservedWord)
                                                {
                                                    result += "Position " + position + ". This is a reserved word " + sentence + "\n";
                                                    f1 = true;
                                                    break;
                                                }
                                            }
                                            if (f1 == f2)
                                            {
                                                result += "Position " + position + ". This is a variable " + sentence + "\n";
                                            }
                                        }
                                    }
                                }
                            }
                        }                                       
                    }                       
                }                       
            }
            return result;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //string str = "int main (void) {float float, 'c, d;...}";
            //string str = "b=c?d:2*a[n];";
            string str = "ifc<a)b=sn(2*a);else b=b2a;";
            Console.WriteLine("Input string:  " + str);
            Console.WriteLine(str.LexicalAnalyze());

            Console.ReadKey();
        }
    }
}
