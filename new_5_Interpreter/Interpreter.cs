using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
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
                        throw new ParseException("Error! Semicolon missing");
                    break;

                case Parser.STATEMENT_LIST:
                    string[] separators = new string[] { ";" };
                    string[] parts = s.Trim().Split(separators, StringSplitOptions.None);
                    foreach (string part in parts)
                    {
                        Parser.STATEMENT.Parse(part);
                    }
                    break;

                case Parser.STATEMENT:
                    if (s.StartsWith("float "))
                        Parser.FLOAT.Parse(s.Substring(5).Trim());
                    else if (s.StartsWith("int "))
                        Parser.INT.Parse(s.Substring(3).Trim());
                    else if (s.Contains('?') && s.Contains(':') && (s.IndexOf('?') < s.IndexOf(':')))
                        Parser.TERNAR.Parse(s.Trim());
                    break;

                case Parser.TERNAR:
                    string[] ternarsSeparatros = new string[] { "?", ":" };
                    string[] ternars = s.Split(ternarsSeparatros, StringSplitOptions.None);
                    Parser.LOGICAL.Parse(ternars[0].Trim());
                    Parser.EXPRESSION.Parse(ternars[1].Trim());
                    Parser.EXPRESSION.Parse(ternars[2].Trim());
                    break;

                case Parser.EXPRESSION:
                    //string[] terminals = new string[] { "==", "++", "--", "=", "?", ":", "*", "[", "]", ";", "{", "}", "+", "-", "/", "(", ")", "<", ">" };
                    string[] operators = new string[] { "/", "+", "-", "*" };
                    string[] exParts = s.Split(operators, StringSplitOptions.None);
                    foreach (string exPart in exParts)
                    {
                        if (exPart != "")
                            Parser.VARIABLE.Parse(exPart.Trim());
                        else
                            throw new ParseException("Error! Illegal variable name" + s);
                    }
                    break;


                case Parser.LOGICAL:
                    if (s.Contains("<"))
                    {
                        int i = s.IndexOf("<");
                        Parser.EXPRESSION.Parse(s.Substring(0, i).Trim());
                        Parser.EXPRESSION.Parse(s.Substring(i + 1).Trim());

                    }
                    else if (s.Contains(">"))
                    {
                        int i = s.IndexOf(">");
                        Parser.EXPRESSION.Parse(s.Substring(0, i).Trim());
                        Parser.EXPRESSION.Parse(s.Substring(i + 1).Trim());
                    }
                    else if (s.Contains(">="))
                    {
                        int i = s.IndexOf(">=");
                        Parser.EXPRESSION.Parse(s.Substring(0, i).Trim());
                        Parser.EXPRESSION.Parse(s.Substring(i + 1).Trim());
                    }
                    else if (s.Contains("<="))
                    {
                        int i = s.IndexOf("<=");
                        Parser.EXPRESSION.Parse(s.Substring(0, i).Trim());
                        Parser.EXPRESSION.Parse(s.Substring(i + 1).Trim());
                    }
                    else if (s.Contains("=="))
                    {
                        int i = s.IndexOf("==");
                        Parser.EXPRESSION.Parse(s.Substring(0, i).Trim());
                        Parser.EXPRESSION.Parse(s.Substring(i + 1).Trim());
                    }
                    else if (s.Contains("!="))
                    {
                        int i = s.IndexOf("!=");
                        Parser.EXPRESSION.Parse(s.Substring(0, i).Trim());
                        Parser.EXPRESSION.Parse(s.Substring(i + 1).Trim());
                    }
                    else
                        throw new ParseException("Error! Illegal logical " + s);
                    break;

                case Parser.VARIABLE:
                    string[] regulars = new string[] { "float", "int" };
                    string[] terminals = new string[] { "==", "++", "--", "=", "?", ":", "*", "[", "]", ";", "{", "}", "+", "-", "/", "(", ")", "<", ">" };
                    foreach (string regular in regulars)
                    {
                        if (s == regular)
                        {
                            throw new ParseException("Error! Illegal variable name (variable can not be a regular expression) " + s);
                            break;
                        }
                    }
                    if (s.Contains('[') && s.Contains(']'))
                    {
                        int i = s.IndexOf("[");
                        int j = s.IndexOf("]");
                        if (i != -1 && j != -1)
                        {
                            string[] bracket = new string[] { "[", "]" };
                            string[] arrayParts = s.Split(bracket, StringSplitOptions.None);
                            foreach (string arrayPart in arrayParts)
                            {
                                Parser.VARIABLE.Parse(arrayPart.Trim());
                            }
                        }
                    }
                    else if ((s.Contains('[') && !s.Contains(']')) || (!s.Contains('[') && s.Contains(']')) || (s.Contains('(') || s.Contains(')')))
                    {
                        throw new ParseException("Error! Illegal variable name ");
                    }

                    break;

                case Parser.FLOAT:
                    string[] floatVariables = s.Trim().Split(',');
                    foreach (string floatVariable in floatVariables)
                    {
                        if (!floatVariable.Contains("="))
                        {
                            Parser.VARIABLE.Parse(floatVariable.Trim());
                        }
                        else
                        {
                            Parser.INIT.Parse(floatVariable.Trim());
                        }
                    }
                    break;

                case Parser.INT:
                    string[] intVariables = s.Trim().Split(',');
                    foreach (string intVariable in intVariables)
                    {
                        if (!intVariable.Contains("="))
                        {
                            Parser.VARIABLE.Parse(intVariable.Trim());
                        }
                        else
                        {
                            Parser.INIT.Parse(intVariable.Trim());
                        }
                    }
                    break;

                case Parser.INIT:
                    string[] initParts = s.Trim().Split('=');
                    Parser.VARIABLE.Parse(initParts[0].Trim());
                    Parser.EXPRESSION.Parse(initParts[0].Trim());
                    break;
            }
        }

        public static void Interprete(this string s)
        {
            Dictionary<string, string> variables = new Dictionary<string, string>();
            string[] statements = s.Trim().Split(';');
            foreach (string statemen in statements)
            {
                string statement = statemen.Trim();
                //Console.WriteLine(statement);
                if (statement.StartsWith("float "))
                {
                    string buffer = statement.Substring(5).Trim();
                    string[] variablesBuf = buffer.Split(',');
                    foreach (string variableBuf in variablesBuf)
                    {
                        if (variableBuf.Contains("[") && (variableBuf.Contains("]")))
                        {
                            if (!variables.ContainsKey((variableBuf.Substring(0, variableBuf.IndexOf("[") + 1) + "0]").Trim()))
                            {
                                int i = variableBuf.IndexOf("[");
                                int j = variableBuf.IndexOf("]");
                                int length = Int32.Parse(variableBuf.Substring(i + 1, j - i - 1));
                                for (int l = 0; l < length; l++)
                                {
                                    string name = (variableBuf.Substring(0, i + 1) + l + "]").Trim();
                                    Console.WriteLine("Float array element " + name);
                                    variables.Add(name, null);
                                }
                            }
                            else
                            {
                                throw new ParseException("Error! Array name " + variableBuf.Substring(0, variableBuf.IndexOf("[")) + " already use");
                            }
                        }
                        else
                        {
                            if (!variables.ContainsKey(variableBuf.Trim()))
                            {
                                variables.Add(variableBuf.Trim(), null);
                                Console.WriteLine("Added float variable " + variableBuf.Trim());
                            }
                            else
                            {
                                throw new ParseException("Error! Variable name " + variableBuf + " already use");
                            }

                        }
                    }
                }
                else if (statement.StartsWith("int "))
                {
                    string buffer = statement.Substring(3).Trim();
                    string[] variablesBuf = buffer.Split(',');
                    foreach (string variableBuf in variablesBuf)
                    {
                        if (variableBuf.Contains("[") && (variableBuf.Contains("]")))
                        {
                            if (!variables.ContainsKey((variableBuf.Substring(0, variableBuf.IndexOf("[") + 1) + "0]").Trim()))
                            {
                                int i = variableBuf.IndexOf("[");
                                int j = variableBuf.IndexOf("]");
                                int length = Int32.Parse(variableBuf.Substring(i + 1, j - i - 1));
                                for (int l = 0; l < length; l++)
                                {
                                    string name = (variableBuf.Substring(0, i + 1) + l + "]").Trim();
                                    Console.WriteLine("Int array element " + name);
                                    variables.Add(name, null);
                                }
                            }
                            else
                            {
                                throw new ParseException("Error! Array name " + variableBuf.Substring(0, variableBuf.IndexOf("[")) + " already use");
                            }
                        }
                        else
                        {
                            if (!variables.ContainsKey(variableBuf.Trim()))
                            {
                                variables.Add(variableBuf.Trim(), null);
                                Console.WriteLine("Added int variable " + variableBuf.Trim());
                            }
                            else
                            {
                                throw new ParseException("Error! Variable name " + variableBuf + " already use");
                            }

                        }
                    }
                }
                else if (statement.Contains('=')
                     && (!statement.Contains("?"))
                     && (!statement.Contains(":"))
                     && (!statement.Contains("float "))
                     && (!statement.Contains("int ")))
                {
                    string[] parts = statement.Trim().Split('=');
                    if (parts[0].Contains("[") && parts[0].Contains("]"))
                    {
                        int i = parts[0].IndexOf("[");
                        int j = parts[0].IndexOf("]");
                        string index = parts[0].Substring(i + 1, j - i - 1);
                        if (variables.ContainsKey(index))
                            index = variables[index];
                        parts[0] = (parts[0].Substring(0, i + 1) + index + "]").Trim();
                    }

                    if (variables.ContainsKey(parts[0].Trim()))
                    {
                        if (parts[1].Contains('+'))
                        {
                            string[] vars = parts[1].Trim().Split('+');
                            float a;
                            float b;
                            if (variables.ContainsKey(vars[0].Trim()))
                            {
                                a = float.Parse(variables[vars[0].Trim()]);
                            }
                            else
                            {
                                try
                                {
                                    a = float.Parse(vars[0].Trim());
                                }
                                catch (Exception e)
                                {
                                    throw new ParseException("Error! Must  be a number " + vars[0]);
                                }
                            }
                            if (variables.ContainsKey(vars[1].Trim()))
                            {
                                b = float.Parse(variables[vars[1].Trim()]);
                            }
                            else
                            {
                                try
                                {
                                    b = float.Parse(vars[1].Trim());
                                }
                                catch (Exception e)
                                {
                                    throw new ParseException("Error! Must  be a number " + vars[1]);
                                }
                            }
                            float result = a + b;
                            variables[parts[0]] = result.ToString();
                            Console.WriteLine("Result = " + variables[parts[0]]);
                        }
                        else if (parts[1].Contains('-'))
                        {
                            string[] vars = parts[1].Trim().Split('-');
                            float a;
                            float b;
                            if (variables.ContainsKey(vars[0].Trim()))
                            {
                                a = float.Parse(variables[vars[0].Trim()]);
                            }
                            else
                            {
                                try
                                {
                                    a = float.Parse(vars[0].Trim());
                                }
                                catch (Exception e)
                                {
                                    throw new ParseException("Error! Must  be a number " + vars[0]);
                                }
                            }
                            if (variables.ContainsKey(vars[1].Trim()))
                            {
                                b = float.Parse(variables[vars[1].Trim()]);
                            }
                            else
                            {
                                try
                                {
                                    b = float.Parse(vars[1].Trim());
                                }
                                catch (Exception e)
                                {
                                    throw new ParseException("Error! Must  be a number " + vars[1]);
                                }
                            }
                            float result = a - b;
                            variables[parts[0]] = result.ToString();
                            Console.WriteLine(parts[0] + " now = " + variables[parts[0]]);
                        }
                        else if (parts[1].Contains('*'))
                        {
                            string[] vars = parts[1].Trim().Split('*');
                            float a;
                            float b;
                            if (variables.ContainsKey(vars[0].Trim()))
                            {
                                a = float.Parse(variables[vars[0].Trim()]);
                            }
                            else
                            {
                                try
                                {
                                    a = float.Parse(vars[0].Trim());
                                }
                                catch (Exception e)
                                {
                                    throw new ParseException("Error! Must  be a number " + vars[0]);
                                }
                            }
                            if (variables.ContainsKey(vars[1].Trim()))
                            {
                                b = float.Parse(variables[vars[1].Trim()]);
                            }
                            else
                            {
                                try
                                {
                                    b = float.Parse(vars[1].Trim());
                                }
                                catch (Exception e)
                                {
                                    throw new ParseException("Error! Must  be a number " + vars[1]);
                                }
                            }
                            float result = a * b;
                            variables[parts[0]] = result.ToString();
                            Console.WriteLine(parts[0] + " now = " + variables[parts[0]]);
                        }
                        else if (parts[1].Contains('/'))
                        {
                            string[] vars = parts[1].Trim().Split('/');
                            float a;
                            float b;
                            if (variables.ContainsKey(vars[0].Trim()))
                            {
                                a = float.Parse(variables[vars[0].Trim()]);
                            }
                            else
                            {
                                try
                                {
                                    a = float.Parse(vars[0].Trim());
                                }
                                catch (Exception e)
                                {
                                    throw new ParseException("Error! Must  be a number " + vars[0]);
                                }
                            }
                            if (variables.ContainsKey(vars[1].Trim()))
                            {
                                b = float.Parse(variables[vars[1].Trim()]);
                            }
                            else
                            {
                                try
                                {
                                    b = float.Parse(vars[1].Trim());
                                }
                                catch (Exception e)
                                {
                                    throw new ParseException("Error! Must  be a number " + vars[1]);
                                }
                            }
                            float result = a / b;
                            variables[parts[0]] = result.ToString();
                            Console.WriteLine(parts[0] + " now = " + variables[parts[0]]);
                        }
                        else
                        {
                            variables[parts[0]] = parts[1];
                            Console.WriteLine(parts[0] + " initialized with value " + variables[parts[0]]);
                        }
                    }
                    else
                    {
                        throw new ParseException("Error! Variable " + parts[0] + " not initialized");
                    }
                }
                else if (statement.Contains("?") && statement.Contains(":"))
                {
                    bool result = false;
                    string[] ternarSymbols = { "?", ":" };
                    string[] ternarParts = statement.Trim().Split(ternarSymbols, StringSplitOptions.None);
                    /*Console.WriteLine(ternarParts[0]);
                    Console.WriteLine(ternarParts[1]);
                    Console.WriteLine(ternarParts[2]);*/

                    if (ternarParts[0].Contains("<"))
                    {
                        string[] logicPartss = ternarParts[0].Trim().Split('<');
                        if (float.Parse(variables[logicPartss[0].Trim()]) < float.Parse(variables[logicPartss[1].Trim()]))
                            result = true;
                        else
                            result = false;
                    }
                    else if (ternarParts[0].Contains(">"))
                    {
                        string[] logicPartss = ternarParts[0].Trim().Split('>');
                        if (float.Parse(variables[logicPartss[0].Trim()]) > float.Parse(variables[logicPartss[1].Trim()]))
                            result = true;
                        else
                            result = false;
                    }
                    else if (ternarParts[0].Contains(">="))
                    {
                        string[] buff = { ">=" };
                        string[] logicPartss = ternarParts[0].Trim().Split(buff, StringSplitOptions.None);
                        if (float.Parse(variables[logicPartss[0].Trim()]) >= float.Parse(variables[logicPartss[1].Trim()]))
                            result = true;
                        else
                            result = false;
                    }
                    else if (ternarParts[0].Contains("<="))
                    {
                        string[] buff = { "<=" };
                        string[] logicPartss = ternarParts[0].Trim().Split(buff, StringSplitOptions.None);
                        if (float.Parse(variables[logicPartss[0].Trim()]) <= float.Parse(variables[logicPartss[1].Trim()]))
                            result = true;
                        else
                            result = false;
                    }
                    else if (ternarParts[0].Contains("=="))
                    {
                        string[] buff = { "==" };
                        string[] logicPartss = ternarParts[0].Trim().Split(buff, StringSplitOptions.None);
                        /*Console.WriteLine(logicPartss[0]);
                        Console.WriteLine(logicPartss[1]);*/
                        if (float.Parse(variables[logicPartss[0]]) == Int32.Parse(variables[logicPartss[1]]))
                            result = true;
                        else
                            result = false;
                    }
                    else if (ternarParts[0].Contains("!="))
                    {
                        string[] buff = { "!=" };
                        string[] logicPartss = ternarParts[0].Trim().Split(buff, StringSplitOptions.None);
                        if (float.Parse(variables[logicPartss[0].Trim()]) != float.Parse(variables[logicPartss[1].Trim()]))
                            result = true;
                        else
                            result = false;
                    }
                    if (result == true)
                    {
                        if (ternarParts[1].Contains('+'))
                        {
                            string[] vars = ternarParts[1].Trim().Split('+');
                            float a;
                            float b;
                            if (vars[0].Contains("[") && vars[0].Contains("]"))
                            {
                                int i = vars[0].IndexOf("[");
                                int j = vars[0].IndexOf("]");
                                string index = vars[0].Substring(i + 1, j - i - 1);
                                if (variables.ContainsKey(index))
                                    index = variables[index];
                                vars[0] = (vars[0].Substring(0, i + 1) + index + "]").Trim();
                            }
                            if (vars[1].Contains("[") && vars[1].Contains("]"))
                            {
                                int i = vars[1].IndexOf("[");
                                int j = vars[1].IndexOf("]");
                                string index = vars[1].Substring(i + 1, j - i - 1);
                                if (variables.ContainsKey(index))
                                    index = variables[index];
                                vars[1] = (vars[1].Substring(0, i + 1) + index + "]").Trim();
                            }
                            if (variables.ContainsKey(vars[0].Trim()))
                            {
                                a = float.Parse(variables[vars[0].Trim()]);
                            }
                            else
                            {
                                try
                                {
                                    a = float.Parse(vars[0].Trim());
                                }
                                catch (Exception e)
                                {
                                    throw new ParseException("Error! Must  be a number " + vars[0]);
                                }
                            }
                            if (variables.ContainsKey(vars[1].Trim()))
                            {
                                b = float.Parse(variables[vars[1].Trim()]);
                            }
                            else
                            {
                                try
                                {
                                    b = float.Parse(vars[1].Trim());
                                }
                                catch (Exception e)
                                {
                                    throw new ParseException("Error! Must  be a number " + vars[1]);
                                }
                            }
                            float res = a + b;
                            Console.WriteLine("Ternar return true, operation result: " + res);
                        }
                        else if (ternarParts[1].Contains('-'))
                        {
                            string[] vars = ternarParts[1].Trim().Split('-');
                            if (vars[0].Contains("[") && vars[0].Contains("]"))
                            {
                                int i = vars[0].IndexOf("[");
                                int j = vars[0].IndexOf("]");
                                string index = vars[0].Substring(i + 1, j - i - 1);
                                if (variables.ContainsKey(index))
                                    index = variables[index];
                                vars[0] = (vars[0].Substring(0, i + 1) + index + "]").Trim();
                            }
                            if (vars[1].Contains("[") && vars[1].Contains("]"))
                            {
                                int i = vars[1].IndexOf("[");
                                int j = vars[1].IndexOf("]");
                                string index = vars[1].Substring(i + 1, j - i - 1);
                                if (variables.ContainsKey(index))
                                    index = variables[index];
                                vars[1] = (vars[1].Substring(0, i + 1) + index + "]").Trim();
                            }
                            float a;
                            float b;
                            if (variables.ContainsKey(vars[0].Trim()))
                            {
                                a = float.Parse(variables[vars[0].Trim()]);
                            }
                            else
                            {
                                try
                                {
                                    a = float.Parse(vars[0].Trim());
                                }
                                catch (Exception e)
                                {
                                    throw new ParseException("Error! Must  be a number " + vars[0]);
                                }
                            }
                            if (variables.ContainsKey(vars[1].Trim()))
                            {
                                b = float.Parse(variables[vars[1].Trim()]);
                            }
                            else
                            {
                                try
                                {
                                    b = float.Parse(vars[1].Trim());
                                }
                                catch (Exception e)
                                {
                                    throw new ParseException("Error! Must  be a number " + vars[1]);
                                }
                            }
                            float res = a - b;
                            Console.WriteLine("Ternar return true, operation result " + res);
                        }
                        else if (ternarParts[1].Contains('*'))
                        {
                            string[] vars = ternarParts[1].Trim().Split('*');
                            if (vars[0].Contains("[") && vars[0].Contains("]"))
                            {
                                int i = vars[0].IndexOf("[");
                                int j = vars[0].IndexOf("]");
                                string index = vars[0].Substring(i + 1, j - i - 1);
                                if (variables.ContainsKey(index))
                                    index = variables[index];
                                vars[0] = (vars[0].Substring(0, i + 1) + index + "]").Trim();
                            }
                            if (vars[1].Contains("[") && vars[1].Contains("]"))
                            {
                                int i = vars[1].IndexOf("[");
                                int j = vars[1].IndexOf("]");
                                string index = vars[1].Substring(i + 1, j - i - 1);
                                if (variables.ContainsKey(index))
                                    index = variables[index];
                                vars[1] = (vars[1].Substring(0, i + 1) + index + "]").Trim();
                            }
                            float a;
                            float b;
                            if (variables.ContainsKey(vars[0].Trim()))
                            {
                                a = float.Parse(variables[vars[0].Trim()]);
                            }
                            else
                            {
                                try
                                {
                                    a = float.Parse(vars[0].Trim());
                                }
                                catch (Exception e)
                                {
                                    throw new ParseException("Error! Must  be a number " + vars[0]);
                                }
                            }
                            if (variables.ContainsKey(vars[1].Trim()))
                            {
                                b = float.Parse(variables[vars[1].Trim()]);
                            }
                            else
                            {
                                try
                                {
                                    b = float.Parse(vars[1].Trim());
                                }
                                catch (Exception e)
                                {
                                    throw new ParseException("Error! Must  be a number " + vars[1]);
                                }
                            }
                            float res = a * b;
                            Console.WriteLine("Ternar return true, operation result " + res);
                        }
                        else if (ternarParts[1].Contains('/'))
                        {
                            string[] vars = ternarParts[1].Trim().Split('/');
                            if (vars[0].Contains("[") && vars[0].Contains("]"))
                            {
                                int i = vars[0].IndexOf("[");
                                int j = vars[0].IndexOf("]");
                                string index = vars[0].Substring(i + 1, j - i - 1);
                                if (variables.ContainsKey(index))
                                    index = variables[index];
                                vars[0] = (vars[0].Substring(0, i + 1) + index + "]").Trim();
                            }
                            if (vars[1].Contains("[") && vars[1].Contains("]"))
                            {
                                int i = vars[1].IndexOf("[");
                                int j = vars[1].IndexOf("]");
                                string index = vars[1].Substring(i + 1, j - i - 1);
                                if (variables.ContainsKey(index))
                                    index = variables[index];
                                vars[1] = (vars[1].Substring(0, i + 1) + index + "]").Trim();
                            }
                            float a;
                            float b;
                            if (variables.ContainsKey(vars[0].Trim()))
                            {
                                a = float.Parse(variables[vars[0].Trim()]);
                            }
                            else
                            {
                                try
                                {
                                    a = float.Parse(vars[0].Trim());
                                }
                                catch (Exception e)
                                {
                                    throw new ParseException("Error! Must  be a number " + vars[0]);
                                }
                            }
                            if (variables.ContainsKey(vars[1].Trim()))
                            {
                                b = float.Parse(variables[vars[1].Trim()]);
                            }
                            else
                            {
                                try
                                {
                                    b = float.Parse(vars[1].Trim());
                                }
                                catch (Exception e)
                                {
                                    throw new ParseException("Error! Must  be a number " + vars[1]);
                                }
                            }
                            float res = a / b;
                            Console.WriteLine("Ternar return true, operation result " + res);
                        }
                        else
                        {
                            Console.WriteLine(ternarParts[1]);
                            Console.WriteLine(variables[ternarParts[1]]);
                        }
                    }
                    else
                    {
                        if (ternarParts[2].Contains('+'))
                        {
                            string[] vars = ternarParts[2].Trim().Split('+');
                            if (vars[0].Contains("[") && vars[0].Contains("]"))
                            {
                                int i = vars[0].IndexOf("[");
                                int j = vars[0].IndexOf("]");
                                string index = vars[0].Substring(i + 1, j - i - 1);
                                if (variables.ContainsKey(index))
                                    index = variables[index];
                                vars[0] = (vars[0].Substring(0, i + 1) + index + "]").Trim();
                            }
                            if (vars[1].Contains("[") && vars[1].Contains("]"))
                            {
                                int i = vars[1].IndexOf("[");
                                int j = vars[1].IndexOf("]");
                                string index = vars[1].Substring(i + 1, j - i - 1);
                                if (variables.ContainsKey(index))
                                    index = variables[index];
                                vars[1] = (vars[1].Substring(0, i + 1) + index + "]").Trim();
                            }
                            float a;
                            float b;
                            if (variables.ContainsKey(vars[0].Trim()))
                            {
                                a = float.Parse(variables[vars[0].Trim()]);
                            }
                            else
                            {
                                try
                                {
                                    a = float.Parse(vars[0].Trim());
                                }
                                catch (Exception e)
                                {
                                    throw new ParseException("Error! Must  be a number " + vars[0]);
                                }
                            }
                            if (variables.ContainsKey(vars[1].Trim()))
                            {
                                b = float.Parse(variables[vars[1].Trim()]);
                            }
                            else
                            {
                                try
                                {
                                    b = float.Parse(vars[1].Trim());
                                }
                                catch (Exception e)
                                {
                                    throw new ParseException("Error! Must  be a number " + vars[1]);
                                }
                            }
                            float res = a + b;
                            Console.WriteLine("Ternar return false, operation result " + res);
                        }
                        else if (ternarParts[2].Contains('-'))
                        {
                            string[] vars = ternarParts[2].Trim().Split('-');
                            if (vars[0].Contains("[") && vars[0].Contains("]"))
                            {
                                int i = vars[0].IndexOf("[");
                                int j = vars[0].IndexOf("]");
                                string index = vars[0].Substring(i + 1, j - i - 1);
                                if (variables.ContainsKey(index))
                                    index = variables[index];
                                vars[0] = (vars[0].Substring(0, i + 1) + index + "]").Trim();
                            }
                            if (vars[1].Contains("[") && vars[1].Contains("]"))
                            {
                                int i = vars[1].IndexOf("[");
                                int j = vars[1].IndexOf("]");
                                string index = vars[1].Substring(i + 1, j - i - 1);
                                if (variables.ContainsKey(index))
                                    index = variables[index];
                                vars[1] = (vars[1].Substring(0, i + 1) + index + "]").Trim();
                            }
                            float a;
                            float b;
                            if (variables.ContainsKey(vars[0].Trim()))
                            {
                                a = float.Parse(variables[vars[0].Trim()]);
                            }
                            else
                            {
                                try
                                {
                                    a = float.Parse(vars[0].Trim());
                                }
                                catch (Exception e)
                                {
                                    throw new ParseException("Error! Must  be a number " + vars[0]);
                                }
                            }
                            if (variables.ContainsKey(vars[1].Trim()))
                            {
                                b = float.Parse(variables[vars[1].Trim()]);
                            }
                            else
                            {
                                try
                                {
                                    b = float.Parse(vars[1].Trim());
                                }
                                catch (Exception e)
                                {
                                    throw new ParseException("Error! Must  be a number " + vars[1]);
                                }
                            }
                            float res = a - b;
                            Console.WriteLine("Ternar return false, operation result " + res);
                        }
                        else if (ternarParts[2].Contains('*'))
                        {
                            string[] vars = ternarParts[2].Trim().Split('*');
                            if (vars[0].Contains("[") && vars[0].Contains("]"))
                            {
                                int i = vars[0].IndexOf("[");
                                int j = vars[0].IndexOf("]");
                                string index = vars[0].Substring(i + 1, j - i - 1);
                                if (variables.ContainsKey(index))
                                    index = variables[index];
                                vars[0] = (vars[0].Substring(0, i + 1) + index + "]").Trim();
                            }
                            if (vars[1].Contains("[") && vars[1].Contains("]"))
                            {
                                int i = vars[1].IndexOf("[");
                                int j = vars[1].IndexOf("]");
                                string index = vars[1].Substring(i + 1, j - i - 1);
                                if (variables.ContainsKey(index))
                                    index = variables[index];
                                vars[1] = (vars[1].Substring(0, i + 1) + index + "]").Trim();
                            }
                            float a;
                            float b;
                            if (variables.ContainsKey(vars[0].Trim()))
                            {
                                a = float.Parse(variables[vars[0].Trim()]);
                            }
                            else
                            {
                                try
                                {
                                    a = float.Parse(vars[0].Trim());
                                }
                                catch (Exception e)
                                {
                                    throw new ParseException("Error! Must  be a number " + vars[0]);
                                }
                            }
                            if (variables.ContainsKey(vars[1].Trim()))
                            {
                                b = float.Parse(variables[vars[1].Trim()]);
                            }
                            else
                            {
                                try
                                {
                                    b = float.Parse(vars[1].Trim());
                                }
                                catch (Exception e)
                                {
                                    throw new ParseException("Error! Must  be a number " + vars[1]);
                                }
                            }
                            float res = a * b;
                            Console.WriteLine("Ternar return false, operation result " + res);
                        }
                        else if (ternarParts[2].Contains('/'))
                        {
                            string[] vars = ternarParts[2].Trim().Split('/');
                            if (vars[0].Contains("[") && vars[0].Contains("]"))
                            {
                                int i = vars[0].IndexOf("[");
                                int j = vars[0].IndexOf("]");
                                string index = vars[0].Substring(i + 1, j - i - 1);
                                if (variables.ContainsKey(index))
                                    index = variables[index];
                                vars[0] = (vars[0].Substring(0, i + 1) + index + "]").Trim();
                            }
                            if (vars[1].Contains("[") && vars[1].Contains("]"))
                            {
                                int i = vars[1].IndexOf("[");
                                int j = vars[1].IndexOf("]");
                                string index = vars[1].Substring(i + 1, j - i - 1);
                                if (variables.ContainsKey(index))
                                    index = variables[index];
                                vars[1] = (vars[1].Substring(0, i + 1) + index + "]").Trim();
                            }
                            float a;
                            float b;
                            if (variables.ContainsKey(vars[0].Trim()))
                            {
                                a = float.Parse(variables[vars[0].Trim()]);
                            }
                            else
                            {
                                try
                                {
                                    a = float.Parse(vars[0].Trim());
                                }
                                catch (Exception e)
                                {
                                    throw new ParseException("Error! Must  be a number " + vars[0]);
                                }
                            }
                            if (variables.ContainsKey(vars[1].Trim()))
                            {
                                b = float.Parse(variables[vars[1].Trim()]);
                            }
                            else
                            {
                                try
                                {
                                    b = float.Parse(vars[1].Trim());
                                }
                                catch (Exception e)
                                {
                                    throw new ParseException("Error! Must  be a number " + vars[1]);
                                }
                            }
                            float res = a / b;
                            Console.WriteLine("Ternar return false, operation result " + res);
                        }
                        else
                        {
                            Console.WriteLine(variables[ternarParts[2]]);
                        }
                    }

                }
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
        INIT
    }
}
