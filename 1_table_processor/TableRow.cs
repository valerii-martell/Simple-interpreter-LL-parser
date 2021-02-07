using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class TableRow
    {
        private Key key;
        private Union value;
        public TableRow()
        {
        }
        public TableRow(Key key, Union value)
        {
            this.key = key;
            this.value = value;
        }
        public Key Key
        {
            get { return this.key; }
            set { this.key = value; }
        }
        public Union Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
        public void UpdateRow(TableRow tableRow)
        {
            this.key = tableRow.key;
            this.value = tableRow.value;
        }
        public bool IsEquals(TableRow tableRow)
        {
            if (this.Key.IsEquals(tableRow.Key)) return true;
            return false;
        }
        public override string ToString()
        {
            return key.ToString() +" "+ value.ToString();
        }
    }
}
