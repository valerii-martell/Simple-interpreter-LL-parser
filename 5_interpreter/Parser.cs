using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    static class ParseClass
    {
        public static void Analyze(this string s)
        {
            Parser.SEMICOLON.Parse(s);
        }
        private static void Parse(this Parser parser, string s)
        {
            switch (parser)
            {
                case Parser.SEMICOLON:
                    s = s.Trim();
                    if (s.EndsWith(";"))
                        Parser.STATEMENT_LIST.Parse(s.Substring(0, s.Length - 1));
                    else
                        throw new ParseException("Betrayal! Semicolon missing");
                    break;

                case Parser.STATEMENT_LIST: //bad code, need rewrite
                    string[] separators = new string[] { " then", " else" };
                    string[] parts = s.Trim().Split(separators, StringSplitOptions.None);
                    int p = 0;
                    foreach (string part in parts)
                    {
                        string str = "";
                        if (p == 1) str = " then" + part;
                        else if (p == 2) str = " else" + part;
                        else str = part;
                        Parser.STATEMENT.Parse(str);
                        p++;
                    }
                    break;

                case Parser.STATEMENT:
                    if (s.StartsWith("if"))
                        Parser.IF.Parse(s.Substring(2));
                    else if (s.StartsWith(" then "))
                        Parser.UNIT.Parse(s.Substring(5).Trim());
                    else if (s.StartsWith(" else "))
                        Parser.UNIT.Parse(s.Substring(5).Trim());
                    else
                        throw new ParseException("Betrayal! Illegal conditional operator" + s);
                    break;

                case Parser.IF:
                    int i = s.IndexOf("(");
                    int j = s.IndexOf(")");
                    if (i != -1 && j != -1)
                    {
                        Parser.LOGICAL.Parse(s.Substring(i + 1, j - 2).Trim());
                        //Parser.STATEMENT.Parse(s.Substring(j + 1).Trim());
                    }
                    else
                        throw new ParseException("Betrayal! Bracket missing" + s);
                    break;

                case Parser.EXPRESSION:
                    //string[] terminals = new string[] { "==", "++", "--", "=", "?", ":", "*", "[", "]", ";", "{", "}", "+", "-", "/", "(", ")", "<", ">" };
                    string[] operators = new string[] { "/", "+", "-", "*" };
                    string[] exParts = s.Split(operators, StringSplitOptions.None);
                    foreach (string part in exParts)
                    {
                        if (part != "")
                            Parser.VARIABLE.Parse(part.Trim());
                        else
                            throw new ParseException("Betrayal! Illegal variable name" + s);
                    }
                    break;


                case Parser.LOGICAL:
                    if (s.Contains("<"))
                    {
                        i = s.IndexOf("<");
                        Parser.EXPRESSION.Parse(s.Substring(0, i).Trim());
                        Parser.EXPRESSION.Parse(s.Substring(i + 1).Trim());

                    }
                    else if (s.Contains(">"))
                    {
                        i = s.IndexOf(">");
                        Parser.EXPRESSION.Parse(s.Substring(0, i).Trim());
                        Parser.EXPRESSION.Parse(s.Substring(i + 1).Trim());
                    }
                    else
                        throw new ParseException("Betrayal! Illegal logical " + s);
                    break;

                case Parser.VARIABLE:
                    string[] regulars = new string[] { "begin", "end", "if", "then", "else" };

                    foreach (string regular in regulars)
                    {
                        if (s == regular)
                        {
                            throw new ParseException("Betrayal! Illegal variable name (variable can not be a regular expression) " + s);
                            break;
                        }
                    }
                    break;

                case Parser.UNIT:
                    if (!s.StartsWith("begin "))
                        throw new ParseException("Betrayal! begin missing: " + s);
                    if (!s.EndsWith(" end"))
                        throw new ParseException("Betrayal! end missing " + s);
                    break;
            }
        }
    }
    enum Parser
    {
        SEMICOLON,
        IF,
        UNIT,
        STATEMENT_LIST,
        STATEMENT,
        LOGICAL,
        EXPRESSION,
        VARIABLE
    }
}
