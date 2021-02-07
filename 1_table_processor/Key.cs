using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    struct Key
    {
        private string stringKey;
        private ushort ushortKey;

        public string StringKey { get { return this.stringKey; } }
        public ushort UshortKey { get { return this.ushortKey; } }

        public Key(string stringKey, ushort ushortKey)
        {
            this.stringKey = stringKey;
            this.ushortKey = ushortKey;
        }
        public bool IsEquals(Key key)
        {
            if (this.stringKey == key.StringKey &&
                this.ushortKey == key.UshortKey)
                return true;
            else
                return false;
        }
        public override string ToString()
        {
            return "Key[" + stringKey + "; "+ushortKey+"]";
        }
    }
}
