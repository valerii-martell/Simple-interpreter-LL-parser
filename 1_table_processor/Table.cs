using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    static class Coincidence
    {
        public static string ReplaceAll(this string str)
        {
            str = str.Replace('і', 'i');
            str = str.Replace('І', 'I');
            str = str.Replace('е', 'e');
            str = str.Replace('Е', 'E');
            str = str.Replace('м', 'M');
            str = str.Replace('Н', 'H');
            str = str.Replace('о', 'o');
            str = str.Replace('О', 'O');
            str = str.Replace('Р', 'P');
            str = str.Replace('х', 'x');
            str = str.Replace('Х', 'X');
            str = str.Replace('Т', 'T');
            str = str.Replace('у', 'y');
            str = str.Replace('р', 'p');
            str = str.Replace('а', 'a');
            str = str.Replace('А', 'A');
            str = str.Replace('к', 'k');
            str = str.Replace('К', 'K');
            str = str.Replace('с', 'c');
            str = str.Replace('С', 'C');
            str = str.Replace('В', 'B');
            return str;
        }

    }
    class Table
    {
        private List<TableRow> table = new List<TableRow>();
        private int point = 0;
        private int linPoint = 0;
        private int binPoint = 0;
        private TableRow buffer = null;

        public Table(List<TableRow> table)
        {
            this.table = table;
        }
        public Table(params TableRow[] table)
        {
            foreach(TableRow tableRow in table)
            {
                this.table.Add(tableRow);
            }
        }
        public TableRow SelectByDirectAddress(int address)
        {
            return table[address];
        }
        public Table SelectByDirectKey(ushort key)
        {
            Table result = new Table();
            if (point >= table.Count) point = 0;
            for (int i = point+1; i < table.Count; i++)
            {
                if (table[i].Key.UshortKey == key)
                {
                    result.Insert(table[i]);
                    point=i;
                    break;
                }
            }
            if (!result.table.Any())
            {
                point = 0;
                for (int i = point+1; i < table.Count; i++)
                {
                    if (table[i].Key.UshortKey == key)
                    {
                        result.Insert(table[i]);
                        point = i;
                        break;
                    }
                }
            }
            return result;
        }
        public void Insert(TableRow tableRow)
        {
            this.table.Add(tableRow);
        }
        public void Delete(Key key)
        {
            foreach(TableRow tableRow in table)
            {
                if (tableRow.Key.IsEquals(key))
                {
                    table.Remove(tableRow);
                    break;
                }
            }
        }
        public void Clear()
        {
            table.Clear();
        }
        public void Update(Key key, Union value)
        {
            foreach (TableRow tableRow in table)
            {
                if (tableRow.Key.IsEquals(key))
                    tableRow.Value=value;
            }
        }
        public Table SelectBySimilarSearch(Key key)
        {
            string searchKey=key.StringKey.ReplaceAll().ToLower();

            Table result = new Table();
            int maxSimilarity = 0;
            while (searchKey.Length > 0)
            {
                foreach (TableRow tableRow in table)
                {
                    string thisKey = tableRow.Key.StringKey.ReplaceAll().ToLower();
                    int tempSimilarity = 0;

                    for (int i = 0; i < Math.Min(thisKey.Length, searchKey.Length);)
                    {
                        if (thisKey[i] == searchKey[i]) 
                        {
                            tempSimilarity++;
                            i++;
                            result.Delete(tableRow.Key);
                            result.Insert(tableRow);
                        }
                        else
                        {
                            if (tempSimilarity > maxSimilarity)
                            {
                                maxSimilarity = tempSimilarity;
                                result.Clear();
                            }
                            break;
                        }
                    }
                }
                if (result.table.Any()) break;
                else searchKey.Substring(0, searchKey.Length - 1);
            }
            return result;
        }
        private bool Сontains(Key key)
        {
            foreach(TableRow tableRow in table)
            {
                if (tableRow.Key.IsEquals(key)) return true;
            }
            return false;
        }
        
        public TableRow SelectByLinearSearch(Key key)
        {
            TableRow result = null;
                if (linPoint >= table.Count) linPoint = 0;
                for (int i = linPoint; i < table.Count; i++)
                {
                    if (table[i].Key.StringKey.Length > key.StringKey.Length)
                    {
                        if (table[i].Key.StringKey.Contains(key.StringKey))
                        {
                            result = table[i];
                            linPoint = i+1;
                            break;
                        }
                    }
                    else
                    {
                        if (table[i].Key.StringKey == key.StringKey)
                        {
                            result = table[i];
                            linPoint = i+1;
                            break;
                        }
                    }
                }
                if (result==null)
                {
                    linPoint = 0;
                    for (int i = linPoint; i < table.Count; i++)
                    {
                        if (table[i].Key.StringKey.Length > key.StringKey.Length)
                        {
                            if (table[i].Key.StringKey.Contains(key.StringKey))
                            {
                                result = table[i];
                                linPoint = i+1;
                                break;
                            }
                        }
                        else
                        {
                            if (table[i].Key.StringKey == key.StringKey)
                            {
                                result = table[i];
                                linPoint = i+1;
                                break;
                            }
                        }
                    }
                }
            return result;
        }
        private int CompareString(string s1, string s2)
        {
            int n = Math.Min(s1.Length, s2.Length);

            for (int i = 0; i < n; i++)
            {
                if (s1[i] != s2[i])
                {
                    return s1[i] - s2[i];
                }
            }
            return 0;
        }
        private int CompareKey(Key key1, Key key2)
        {
            int result = 0;
            if (CompareString(key1.StringKey, key2.StringKey) > 0)
            {
                result = 1;
            }
            else if (CompareString(key1.StringKey, key2.StringKey) < 0)
            {
                result = -1;
            }
            else if (key1.UshortKey - key2.UshortKey > 0)
            {
                result = 1;
            }
            else if (key1.UshortKey - key2.UshortKey < 0)
            {
                result = -1;
            }
            return result;
        }


        public int BinarySearch(List<string> keys, string key, int low, int high) //from Wikipedia
        {
            //keys.Sort();
            int i = -1;
            if (keys != null)
            {
                int mid;
                while (low < high)
                {
                    mid = (low + high) / 2; 
                    if (keys[mid].Contains(key) || (keys[mid].Length >= key.Length && keys[mid].Substring(0, key.Length)==key))
                    {
                        i = mid;
                        break;
                    }
                    else
                    {
                        if (CompareString(keys[mid], key)>0)
                        {
                            high = mid;
                        }
                        else
                        {
                            low = mid + 1;
                        }
                    }
                }
            }
            return i;
        }
        public TableRow SelectByBinarySearch(Key key)
        {
            TableRow result = null;         
            List<TableRow> copy = new List<TableRow>();

            if (binPoint >= table.Count) binPoint = 0;
            for (int i=0; i<table.Count; i++)
            {
                copy.Add(table[i]);
            }

            List<string> keys = new List<string>();            
            for (int i = 0; i < copy.Count; i++)
                keys.Add(copy[i].Key.StringKey);

            //int index = keys.BinarySearch(key.StringKey);
            int index = BinarySearch(keys, key.StringKey, binPoint+1, keys.Count);
            if (index >= 0)
            {
                binPoint=index;
                foreach (TableRow tableRow in copy)
                {
                    if (tableRow.Key.StringKey == keys[binPoint])
                    {
                        result = tableRow;
                        break;
                    }
                }
            }
            if (result==null)
            {
                index = BinarySearch(keys, key.StringKey, 0, binPoint);
                if (index >= 0)
                {
                    binPoint=index;
                    foreach (TableRow tableRow in copy)
                    {
                        if (tableRow.Key.StringKey == keys[binPoint])
                        {
                            result = tableRow;
                            break;
                        }
                    }
                }
            }
            return result;
        }
       
        public override string ToString()
        {
            if (table.Any())
            {
                string str = "";
                foreach (TableRow tableRow in table)
                    str += tableRow +"\n";
                return str.Substring(0, str.Length-1);
            }
            else
            {
                return "No results";
            }
        }
    }
}
