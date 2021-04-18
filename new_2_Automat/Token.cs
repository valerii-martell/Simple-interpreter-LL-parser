using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automat
{
    class Token
    {
        private string value;
        private Token left;
        private Token right;
        private Token parent;
        private bool terminal;
        private string prevvalue;

        public Token(string value, Token left, Token right, bool terminal)
        {
            this.value = value;
            this.left = left;
            this.right = right;
            this.terminal = terminal;
        }
        public string Value
        {
            get { return value; }
            set { this.value = value; }
        }
        public string PrevValue
        {
            get { return prevvalue; }
            set { this.prevvalue = value; }
        }
        public Token Left
        {
            get { return left; }
            set { this.left = value; }
        }
        public Token Right
        {
            get { return right; }
            set { this.right = value; }
        }
        public Token Parent
        {
            get { return parent; }
            set { this.parent = value; }
        }
        public bool IsTerminal
        {
            get { return terminal; }
            set { this.terminal = value; }
        }
    }
}
