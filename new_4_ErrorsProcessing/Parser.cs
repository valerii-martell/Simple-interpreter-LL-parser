using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ErrorsProcessing
{
    static class ParseClass
    {
        private static string[] terminals = new string[]
                {":=", "==", "++", "--", "?", "*", "[", "]", ";", "{", "}", "+", "-", "/", "(", ")", "<", ">", ",", ".", " "};

        private static string[] reservedWords = new string[]
            { "and", "array", "begin", "case", "const",
                    "div", "do", "downto", "else", "end",
                    "file", "for", "function", "goto", "if",
                    "in", "label", "mod", "nil", "not",
                    "of", "or",  "packed", "procedure", "program",
                    "record",  "repeat", "set", "then", "to",
                    "type", "until",   "var", "while", "with",
                    "integer", "real", "char", "boolean"};

        public static void Analyze(this string s)
        {
            s = s.Replace("\n", " ");
            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex("[ ]{2,}", options);
            s = regex.Replace(s, " ");
            Parser.STATEMENT_LIST.Parse(s);
            
        }
        private static void Parse(this Parser parser, string s)
        {
            switch (parser)
            {
                case Parser.STATEMENT_LIST:
                    //Console.WriteLine("Statement list "+ s);
                    //begin end count
                    s = s.Trim();
                    if (s.StartsWith("if "))
                    {
                        Parser.IF.Parse(s);
                    }
                    else if (s.StartsWith("begin "))
                    {
                        if (s == "begin end;" || s == "begin end")
                        {
                            //throw new ParseException("Error! End expected " + s.Split(' ').Last());
                        } else if (s.EndsWith("; end") || s.EndsWith(";end;") || s.EndsWith("; end;") || s.EndsWith(";end") || s.EndsWith(" end;") || s.EndsWith(" end"))
                        {
                            //Console.WriteLine(s.Substring(0, s.LastIndexOf("end")).Trim().Substring(6));
                            Parser.STATEMENT_LIST.Parse(s.Substring(0, s.LastIndexOf("end")).Trim().Substring(6));
                        }
                        else
                        {
                            throw new ParseException("Error! End expected " + s.Split(' ').Last());
                        }
                        //Regex.Matches(s, "OU=").Count;
                    }
                    else if ((s.EndsWith("; end") || s.EndsWith(";end;") || s.EndsWith("; end;") || s.EndsWith(";end") || s.EndsWith(" end;") || s.EndsWith(" end")) && (!s.Contains("begin ")))
                    {
                        string[] parts = s.Trim().Split(new string[] { " end", " " }, StringSplitOptions.None);
                        if (parts.Last()==";" || parts.Last() == " ;" || parts.Last() == "; " || parts.Last() == " ; ")
                            throw new ParseException("Error! Begin expected " + parts[parts.Length-2]);
                        else
                           throw new ParseException("Error! Begin expected " + parts.Last());
                    }
                    else
                    {
                        string[] parts = s.Trim().Split(new string[] { "begin " }, StringSplitOptions.None);
                        if (parts.Length > 1)
                        {
                            for (int k = 0; k < parts.Length; k++)
                            {
                                if (k==0)
                                {
                                    Parser.STATEMENT_LIST.Parse(parts[k]);
                                }
                                else
                                {
                                    //Console.WriteLine("begin " + parts[k]);
                                    Parser.STATEMENT_LIST.Parse("begin " + parts[k]);
                                }
                            }
                        }
                        else
                        {
                            parts = s.Trim().Split(new string[] { "; ", ";" }, StringSplitOptions.None);
                            for(int p = 0; p < parts.Length; p++)
                            {
                                if (p != parts.Length - 1)
                                {
                                    parts[p] += ";";
                                }
                            }
                            foreach (string part in parts)
                            {
                                if (part != "")
                                {
                                    //Console.WriteLine("Just part " + part.Trim()+";");
                                    Parser.STATEMENT.Parse(part.Trim());
                                }

                            }
                        }
                    }
                    break;

                case Parser.STATEMENT:
                    s = s.Trim();
                    //Console.WriteLine("STATEMENT " + s);
                    if (!s.EndsWith(";"))
                    {
                        throw new ParseException("Error! Semicolon expected " + s);
                    }
                    else
                    {
                        s = s.Substring(0, s.Length-1);
                    }

                    if (s.Contains(">") || 
                        s.Contains(">=") || 
                        s.Contains("<") || 
                        s.Contains("<=") || 
                        s.Contains("==") ||
                        s.Contains("<>") ||
                        s.Contains(" and ") ||
                        s.Contains(" or ") ||
                        s.Contains(" xor ") ||
                        s.Contains(" not "))
                    {
                        Parser.LOGICAL.Parse("("+s+")");
                    }
                    else
                    {
                        Parser.INIT.Parse(s);
                    }
                    break;

                case Parser.INIT:
                    if (s.StartsWith("integer "))
                    {
                        Parser.ARITHMETIC.Parse(s.Substring(7));
                    }
                    else if (s.StartsWith("real "))
                    {
                        Parser.ARITHMETIC.Parse(s.Substring(4));
                    }
                    else
                    {
                        Parser.ARITHMETIC.Parse(s);
                    }
                    break;

                case Parser.OPERATIONS:
                    int i = 0, j = 0;
                    string buf = s;
                    if (s.StartsWith("("))
                    {
                        while (s.StartsWith("("))
                        {
                            s = s.Remove(0, 1);
                            i++;
                        }
                    }
                    else
                    {
                        throw new ParseException("Error! Logic open-bracket expected " + s);
                    }

                    if (s.EndsWith(")"))
                    {
                        while (s.EndsWith(")"))
                        {
                            s = s.Remove(s.Length - 1, 1);
                            j++;
                        }
                    }
                    else
                    {
                        throw new ParseException("Error! Logic close-bracket expected " + s);
                    }

                    string[] operators = new string[] { "/", "+", "-", "*" };
                    string[] expParts = s.Split(operators, StringSplitOptions.None);
                    foreach (string expPart in expParts)
                    {
                        int bufInt; double bufDouble;
                        bool isInt = Int32.TryParse(expPart.Trim(), out bufInt);
                        bool isDouble = Double.TryParse(expPart.Trim(), out bufDouble);
                        if (!(isInt || isDouble))
                        {
                            Parser.VARIABLE.Parse(expPart.Trim());
                        }
                        else
                        {
                            //Console.WriteLine("Number " + expPart);
                        }
                    }
                    break;

                case Parser.ARITHMETIC:
                    if (!s.EndsWith(" bool"))
                    {
                        if (s.Contains(":="))
                        {
                            string[] conds = s.Split(new string[] { ":=" }, StringSplitOptions.None);
                            if (conds[0] == " ")
                            {
                                throw new ParseException("Error! Variable name expected " + conds[1]);
                            }
                            else
                            {
                                Parser.OPERATIONS.Parse("("+conds[1]+")");
                            }
                        }
                        else
                        {
                            throw new ParseException("Error! Operation result expected " + s);
                        }
                    }
                    else
                    {
                        Parser.OPERATIONS.Parse("("+s.Substring(0, s.LastIndexOf(" bool"))+")");
                    }
                    break;

                case Parser.VARIABLE:
                    //Console.WriteLine("Variable " + s);
                    foreach (string terminal in terminals)
                    {
                        if (s.Contains(terminal))
                        {
                            throw new ParseException("Error! Invalid variable name " + s);
                        }
                    }
                    foreach (string reservedWord in reservedWords)
                    {
                        if (s == reservedWord)
                        {
                            throw new ParseException("Error! Invalid variable name " + s);
                        }
                    }
                    //Console.WriteLine("Variable " + s);
                    break;

                case Parser.IF:
                    //Console.WriteLine("IF " + s);
                    if (!s.Contains(" then "))
                    {
                        throw new ParseException("Error! Then expected " + s);
                    }
                    else if (s.Contains(")then "))
                    {
                        s = s.Substring(0, s.IndexOf(")then")) + " " + s.Substring(s.IndexOf("then"));
                    }
                    else
                    {
                        //if
                        //Console.WriteLine(s.Substring(0, s.IndexOf(" then")).Trim().Substring(3));
                        Parser.LOGICAL.Parse(s.Substring(0, s.IndexOf(" then")).Trim().Substring(3));

                        if (s.Contains(" else "))
                        {
                            string block1 = s.Substring(s.IndexOf(" then ")).Substring(0, s.IndexOf(" else ")-s.IndexOf(" then ")).Substring(6);
                            if (!block1.Trim().EndsWith("; end;"))
                            {
                                block1 = block1.Substring(0, block1.LastIndexOf("end;")).Trim() + "; end;";
                            }
                            else if (!block1.Trim().EndsWith("; end"))
                            {
                                block1 = block1.Substring(0, block1.LastIndexOf("end")).Trim() + "; end";
                            }
                            string block2 = s.Substring(s.IndexOf(" else ")).Substring(6);
                            //Console.WriteLine("BLOCK1 " + block1);
                            //Console.WriteLine("BLOCK2 " + block2);
                            if (block1.EndsWith(";"))
                            {
                                throw new ParseException("Error! Extra semicolon " + block1);
                            }
                            else
                            {
                                Parser.STATEMENT_LIST.Parse(block1);
                                Parser.STATEMENT_LIST.Parse(block2);
                            }
                        }
                        else
                        {
                            string st = s.Substring(s.IndexOf(" then ")).Substring(6);
                            if (!st.Trim().EndsWith("; end;"))
                            {
                                st = st.Substring(0, st.LastIndexOf("end;")).Trim() + "; end;";
                            }
                            else if (!st.Trim().EndsWith("; end"))
                            {
                                st = st.Substring(0, st.LastIndexOf("end")).Trim() + "; end";
                            }
                            Parser.STATEMENT_LIST.Parse(st);
                            
                        }
                    }
                    break;

                case Parser.LOGICAL:
                    //Console.WriteLine("Logic:" + s);

                    i = 0; j = 0;
                    buf = s;
                    if (s.StartsWith("("))
                    {
                        while (s.StartsWith("("))
                        {
                            s = s.Remove(0, 1);
                            i++;
                        }
                    }
                    else
                    {
                        throw new ParseException("Error! Logic open-bracket expected " + s);
                    }

                    if (s.EndsWith(")"))
                    {
                        while (s.EndsWith(")"))
                        {
                            s = s.Remove(s.Length-1, 1);
                            j++;
                        }
                    }
                    else
                    {
                        throw new ParseException("Error! Logic close-bracket expected " + s);
                    }
                    
                    //Console.WriteLine(i);
                    //Console.WriteLine(j);
                    s = buf;
                    if ((i == j) || ((s.StartsWith("boolean") && i<j)))
                    {
                        //Console.WriteLine(s.Substring(0, s.Length-j).Substring(i));
                        Parser.EXPRESSION.Parse(s.Substring(0, s.Length - j).Substring(i));
                    }
                    else
                    {
                        throw new ParseException("Error! There is different count of open- and close-bracket " + s);
                    }
                    break;

                case Parser.EXPRESSION:
                    //Console.WriteLine("Expression: " + s);
                    if (s.Contains(" and ") ||
                        s.Contains(" or ") ||
                        s.Contains(" xor ")  ||
                        s.Contains(" not "))
                    {
                        string[] condParts = s.Split(new string[] { " and ", " or ", " not ", " xor " }, StringSplitOptions.None);
                        for (i = 0; i < condParts.Length; i++)
                        {
                            //Console.WriteLine(i + " " + condParts[i]);
                            if (i == 0)
                            {
                                if (!condParts[i].EndsWith(")"))
                                    throw new ParseException("Error! Logic close-bracket expected " + "(" + condParts[i]);
                                else
                                    Parser.EXPRESSION.Parse(condParts[i].Substring(0, condParts[i].Length - 1));
                            }
                            else if (i == condParts.Length -1)
                            {
                                if (!condParts[i].StartsWith("("))
                                    throw new ParseException("Error! Logic open-bracket expected " + condParts[i] + ")");
                                else
                                    Parser.EXPRESSION.Parse(condParts[i].Substring(1, condParts[i].Length - 1));
                            }
                            else
                            {
                                if (!condParts[i].EndsWith(")"))
                                    throw new ParseException("Error! Logic close-bracket expected " + "(" + condParts[i]);
                                else if (!condParts[i].StartsWith("("))
                                    throw new ParseException("Error! Logic open-bracket expected " + condParts[i] + ")");
                                else
                                    Parser.EXPRESSION.Parse(condParts[i].Substring(0, condParts[i].Length - 1).Substring(1));
                            }
                        }
                    }
                    else
                    {
                        string[] logicParts = s.Split(new string[] { "<>", ">=", ">", "<=", "<", "=" }, StringSplitOptions.None);
                        if (logicParts.Length != 2)
                        {
                            throw new ParseException("Error! There are too much logic operators " + s);
                        }
                        else
                        {
                            Parser.ARITHMETIC.Parse(logicParts[0] + " bool");
                            Parser.ARITHMETIC.Parse(logicParts[1] + " bool");
                        }
                    }
                    break;
            }
        }
    }
    enum Parser
    {
        SEMICOLON,
        FLOAT,
        INT,
        IF,
        UNIT,
        TERNAR,
        ASSIGNMENT,
        STATEMENT_LIST,
        STATEMENT,
        LOGICAL,
        EXPRESSION,
        VARIABLE,
        INIT,
        ARITHMETIC,
        OPERATIONS
    }
}
