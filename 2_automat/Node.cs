using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    class Node
    {
        private int id; //Nude number
        private Node dlm; //Node after dlm signal
        private Node ltr; //Node after ltr signal

        public Node(int id, Node dlm, Node ltr)
        {
            this.id = id;
            this.dlm = dlm;
            this.ltr = ltr;
        }
        public Node(int id)
        {
            this.id = id;
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
        public Node Ltr
        {
            get { return ltr; }
            set { this.ltr = value; }
        }
    }
}
