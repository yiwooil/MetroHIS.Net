using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7007E
{
    public class QueryEventArgs : EventArgs
    {
        public string Message { get; private set; }

        public QueryEventArgs(string message)
        {
            Message = message;
        }
        
    }
}
