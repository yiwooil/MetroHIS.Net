using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7007E
{
    public class RemakeRequestedEventArgs<T> : EventArgs where T: class
    {
        public T data { get; private set; }

        // 결과 리턴용
        public bool Success { get; set; }
        public string FailureMessage { get; set; }

        public RemakeRequestedEventArgs(T data)
        {
            this.data = data;
            this.Success = false;
            this.FailureMessage = "";
        }
    }
}
