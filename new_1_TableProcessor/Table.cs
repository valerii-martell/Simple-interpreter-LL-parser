using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableProcessor
{
    class Table
    {
        private List<TableRow> table = new List<TableRow>();

        public Table(List<TableRow> table)
        {
            foreach (TableRow tableRow in table)
            {
                this.table.Add(tableRow);
            }
        }
        public TableRow SelectByDirectKey(byte key)
        {
            foreach (TableRow row in table)
            {
                if (row.Key.ByteKey == key)
                {
                    return row;
                }
            }
            return null;
        }

        public Table(params TableRow[] table)
        {
            foreach (TableRow tableRow in table)
            {
                this.table.Add(tableRow);
            }
        }
        public TableRow SelectByDirectAddress(int address)
        {
            return table[address];
        }
        
        public void Insert(TableRow tableRow)
        {
            bool flag = true;
            foreach(TableRow row in table)
            {
                if (row.Key.IsEquals(tableRow.Key))
                {
                    flag = false;
                    break;
                }
            }
            if (flag)
            {
                this.table.Add(tableRow);
            }
            else
            {
                Console.WriteLine("Error! The table already contains another row with this key");
            }   
        }
        public void Delete(Key key)
        {
            foreach (TableRow tableRow in table)
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
        public void Update(Key key, Value value)
        {
            bool flag = false;
            foreach (TableRow tableRow in table)
            {
                if (tableRow.Key.IsEquals(key))
                {
                    tableRow.Value = value;
                    flag = true;
                    break;
                }
            }
            if (!flag)
            {
                Console.WriteLine("Error! There are any rows in the table with this key");
            }
        }
        public TableRow SelectBySimilarSearch(Key key)
        {
            TableRow result = new TableRow();
            int maxSimilarity = 0;
            while (key.StringKey.Length > 0)
            {
                foreach (TableRow tableRow in table)
                {                 
                    int tempSimilarity = 0;
                    for (int i = 0; i < Math.Min(tableRow.Key.StringKey.Length, key.StringKey.Length);)
                    {
                        if (tableRow.Key.StringKey[i] == key.StringKey[i])
                        {
                            tempSimilarity++;
                            i++;
                            result = tableRow;
                        }
                        else
                        {
                            if (tempSimilarity > maxSimilarity)
                            {
                                maxSimilarity = tempSimilarity;
                                result = null;
                            }
                            break;
                        }
                    }
                }
                if (result != null) break;
                else key.StringKey.Substring(0, key.StringKey.Length - 1);
            }
            if (result.Key.StringKey != null)
            {
                return result;
            }
            else
            {
                Console.WriteLine("Error! There are no similar keys in the table");
                return null;
            }
            
        }
        private bool Сontains(Key key)
        {
            foreach (TableRow tableRow in table)
            {
                if (tableRow.Key.IsEquals(key)) return true;
            }
            return false;
        }

        public TableRow SelectByLinearSearch(Key key)
        {
            TableRow result = null;
            foreach(TableRow row in table)
            {
                if (row.Key.StringKey.Length > key.StringKey.Length)
                {
                    if (row.Key.StringKey.Contains(key.StringKey))
                    {
                        result = row;
                        break;
                    }
                }
                else
                {
                    if (row.Key.StringKey == key.StringKey)
                    {
                        result = row;
                        break;
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
            else if (key1.ByteKey - key2.ByteKey > 0)
            {
                result = 1;
            }
            else if (key1.ByteKey - key2.ByteKey < 0)
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
                    if (keys[mid].Contains(key) || (keys[mid].Length >= key.Length && keys[mid].Substring(0, key.Length) == key))
                    {
                        i = mid;
                        break;
                    }
                    else
                    {
                        if (CompareString(keys[mid], key) > 0)
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

            for (int i = 0; i < table.Count; i++)
            {
                copy.Add(table[i]);
            }

            List<string> keys = new List<string>();
            for (int i = 0; i < copy.Count; i++)
                keys.Add(copy[i].Key.StringKey);

            //int index = keys.BinarySearch(key.StringKey);
            int index = BinarySearch(keys, key.StringKey, 0, keys.Count);
            if (index >= 0)
            {
                foreach (TableRow tableRow in copy)
                {
                    if (tableRow.Key.StringKey == keys[index])
                    {
                        result = tableRow;
                        break;
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
                    str += tableRow + "\n";
                return str.Substring(0, str.Length - 1);
            }
            else
            {
                return "There are not any rows in the table";
            }
        }
    }
}
