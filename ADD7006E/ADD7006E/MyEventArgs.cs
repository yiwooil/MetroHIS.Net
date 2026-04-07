using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7006E
{
    class MyEventArgs : EventArgs
    {
        public bool dataOk;
        public CData data;
        public string kind;
    }
}
