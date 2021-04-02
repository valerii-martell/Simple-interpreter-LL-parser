using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableProcessor
{
    struct Value
    {
        public float Val { get; set; }

        public Value(float value)
        {
            Val = value;
        }
        public override string ToString()
        {
            return "Value[" + Val + "]";
        }
    }
}