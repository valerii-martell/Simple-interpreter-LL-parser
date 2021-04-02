using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableProcessor
{
    class TableRow
    {
        public Key Key { get; set; }
        public Value Value { get; set; }
        public TableRow()
        {
        }
        public TableRow(Key key, Value value)
        {
            Key = key;
            Value = value;
        }

        public void UpdateRow(TableRow tableRow)
        {
            Key = tableRow.Key;
            Value = tableRow.Value;
        }
        public bool IsEquals(TableRow tableRow)
        {
            if (this.Key.IsEquals(tableRow.Key)) return true;
            return false;
        }
        public override string ToString()
        {
            return Key.ToString() + " " + Value.ToString();
        }
    }
}
