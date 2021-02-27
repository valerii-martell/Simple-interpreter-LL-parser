using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    class Analyzer
    {
        private static string[] tokens = new string[]
        {"==", "++", "+", "--", "-", "=", "?", ":", "*", "[", "]", ";", "{", "}", "-", "/", "(", ")", "<", ">"};

        public Tree parse(string input)
        {
            string[] array = StringToArray(input);
            Tree tree = new Tree();
            Tree.Root=tree.ArrayToTree(array, 0, array.Length - 1);
            return tree;
        }

        private void Check(string str)
        {
            bool result = true; //for full check
            foreach (string token in tokens)
            {
                if (str[0].ToString() == token) { result = false; Console.WriteLine("First symbol must be not a terminal"); }
                break;
            }
            
            if (!str.Contains("=")) { result = false; Console.WriteLine("= is absent"); }
            else if (str.Contains("?") && (!str.Contains(":"))) { result = false; Console.WriteLine(": is absent"); }
            else if (str.Contains(":") && (!str.Contains("?"))) { result = false; Console.WriteLine("? is absent"); }
            else if (str.Contains("[") && (!str.Contains("]"))) { result = false; Console.WriteLine("] is absent"); }
            else if (str.Contains("]") && (!str.Contains("["))) { result = false; Console.WriteLine("[ is absent"); }
            else if (!str.Contains(";")) { result = false; Console.WriteLine("; is absent"); }
        }

        private string[] StringToArray(string input)
        {
            Console.WriteLine("Input string:  " + input);
            Check(input);


            string[] variables = input.Split(tokens, StringSplitOptions.None);

            List<string> variablesList = new List<string>();
            foreach (string str in variables)
            {
                if (!str.Equals(""))
                {
                    variablesList.Add(str);
                }
            }

            List<string> tokensList = fillTokenList(input);

            List<string> expressionList = new List<string>();

            int resultLength = tokensList.Count + variablesList.Count;
            for (int i = 0; i < resultLength; i++)
            {

                string newElement = null;
                foreach (string posibleToken in tokens)
                {
                    string substring = input.Length < posibleToken.Length ? "" : input.Substring(0, posibleToken.Length);
                    if (substring.Equals(posibleToken))
                    {
                        newElement = posibleToken;
                        try
                        {
                            input = input.Replace(newElement, "");
                        }
                        catch (Exception e)
                        {
                            newElement = string.Format("\\%s", newElement);
                            input = input.Replace(newElement, "");
                            newElement = newElement.Substring(1);
                        }
                        break;
                    }
                }
                if (newElement == null)
                {
                    newElement = variablesList[0];
                    variablesList.RemoveAt(0);
                    input = input.Replace(newElement, "");
                }

                expressionList.Add(newElement);
            }

            string[] result = new string[expressionList.Count];
            result = expressionList.ToArray();
            return result;
        }

        private List<string> fillTokenList(string input)
        {
            List<string> tokensList = new List<string>();
            string inputCopy = input;

            for (int i = 0; i < tokens.Length;)
            {
                if (inputCopy.Contains(tokens[i]))
                {
                    try
                    {
                        inputCopy = inputCopy.Replace(tokens[i], "");
                    }
                    catch (Exception e)
                    {
                        tokens[i] = string.Format("\\%s", tokens[i]);
                        inputCopy = inputCopy.Replace(tokens[i], "");
                        tokens[i] = tokens[i].Substring(1);
                    }
                    finally
                    {
                        tokensList.Add(tokens[i]);
                    }
                }
                else
                {
                    i++;
                }
            }
            return tokensList;
        }
    }
}
