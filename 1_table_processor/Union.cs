using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    struct Union
    {
        private string str;
        private ushort ush;

        public Union(string str, ushort ush)
        {
            this.str = str;
            this.ush = ush;
        }
        public string Str
        {
            get { return this.str; }
            set { this.str = value; }
        }
        public ushort Ush
        {
            get { return this.ush; }
            set { this.ush = value; }
        }
        public override string ToString()
        {
            return "Value[" + str + "; " + ush + "]";
        }
    }
}