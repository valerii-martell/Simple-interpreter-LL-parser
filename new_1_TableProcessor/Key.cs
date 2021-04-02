using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableProcessor
{
    struct Key
    {
        public string StringKey { get; set; }
        public byte ByteKey { get; set; }

        public Key(string stringKey, byte byteKey)
        {
            StringKey = stringKey;
            ByteKey = byteKey;
        }
        public bool IsEquals(Key key)
        {
            if (ByteKey == key.ByteKey &&
                StringKey == key.StringKey)
                return true;
            else
                return false;
        }
        public override string ToString()
        {
            return "Key[" + StringKey + "; " + ByteKey + "]";
        }
    }
}
