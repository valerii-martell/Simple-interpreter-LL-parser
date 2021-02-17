using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    class Token
    {
        private char value;
        private Token left;
        private Token right;
        private Token parent;
        private bool terminal;
        
        public Token(char value, Token left, Token right, bool terminal)
        {
            this.value = value;
            this.left = left;
            this.right = right;
            this.terminal = terminal;
        }
        public char Value
        {
            get { return value; }
            set { this.value = value; }
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
