using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automat
{
    class Node
    {
        private int id; //Nude number
        private Node dlm; //Node after dlm signal
        private Node cfr; //Node after cfr signal
        private Node dif; //Node after dif signal

        public Node(int id, Node dlm, Node cfr)
        {
            this.id = id;
            this.dlm = dlm;
            this.cfr = cfr;
            this.dif = this;
        }
        public Node(int id)
        {
            this.id = id;
            this.dif = this;
        }

        public int Id
        {
            get { return id; }
        }
        public Node Dlm
        {
            get { return dlm; }
            set { this.dlm = value; }
        }
        public Node Cfr
        {
            get { return cfr; }
            set { this.cfr = value; }
        }
        public Node Dif
        {
            get { return dif; }
            set { this.dif = value; }
        }
    }
}
