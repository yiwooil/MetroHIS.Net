using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7003E
{
    class CInfoTable
    {
        public string ITEM { get; set; }
        public string CONTENT { get; set; }

        public CInfoTable(string p_item, string p_content)
        {
            ITEM = p_item;
            CONTENT = p_content;
        }
    }
}
