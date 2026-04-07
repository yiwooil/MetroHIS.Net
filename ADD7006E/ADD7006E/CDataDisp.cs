using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7006E
{
    class CDataDisp
    {
        private string[] m_VALUE = new string[14];

        public string VALUE0 { get { return m_VALUE[0]; } }
        public string VALUE1 { get { return m_VALUE[1]; } }
        public string VALUE2 { get { return m_VALUE[2]; } }
        public string VALUE3 { get { return m_VALUE[3]; } }
        public string VALUE4 { get { return m_VALUE[4]; } }
        public string VALUE5 { get { return m_VALUE[5]; } }
        public string VALUE6 { get { return m_VALUE[6]; } }
        public string VALUE7 { get { return m_VALUE[7]; } }
        public string VALUE8 { get { return m_VALUE[8]; } }
        public string VALUE9 { get { return m_VALUE[9]; } }
        public string VALUE10 { get { return m_VALUE[10]; } }
        public string VALUE11 { get { return m_VALUE[11]; } }
        public string VALUE12 { get { return m_VALUE[12]; } }
        public string VALUE13 { get { return m_VALUE[13]; } }

        public int ColumnCount;

        private string[] m_IsCAPTION = new string[14];

        public string IsCAPTION0 { get { return m_IsCAPTION[0]; } }
        public string IsCAPTION1 { get { return m_IsCAPTION[1]; } }
        public string IsCAPTION2 { get { return m_IsCAPTION[2]; } }
        public string IsCAPTION3 { get { return m_IsCAPTION[3]; } }
        public string IsCAPTION4 { get { return m_IsCAPTION[4]; } }
        public string IsCAPTION5 { get { return m_IsCAPTION[5]; } }
        public string IsCAPTION6 { get { return m_IsCAPTION[6]; } }
        public string IsCAPTION7 { get { return m_IsCAPTION[7]; } }
        public string IsCAPTION8 { get { return m_IsCAPTION[8]; } }
        public string IsCAPTION9 { get { return m_IsCAPTION[9]; } }
        public string IsCAPTION10 { get { return m_IsCAPTION[10]; } }
        public string IsCAPTION11 { get { return m_IsCAPTION[11]; } }
        public string IsCAPTION12 { get { return m_IsCAPTION[12]; } }
        public string IsCAPTION13 { get { return m_IsCAPTION[13]; } }

        private CDataDisp()
        {
            for (int i = 0; i < m_VALUE.Length; i++)
            {
                m_VALUE[i] = "";
            }

            ColumnCount = 0;

            for (int i = 0; i < m_IsCAPTION.Length; i++)
            {
                m_IsCAPTION[i] = "";
            }
        }

        public CDataDisp SetIsCaption(string is_caption)
        {
            for (int i = 0; i < is_caption.Length; i++)
            {
                string str = is_caption.Substring(i, 1);
                if (str == "2")
                {
                    // 값이 "2"이면 뒤를 모두 "1"로 만든다.
                    for (int ii = i; ii < m_IsCAPTION.Length; ii++)
                    {
                        m_IsCAPTION[ii] = "1";
                    }
                    break;
                }
                else
                {
                    m_IsCAPTION[i] = str;
                }
                
            }
            return this;
        }

        public CDataDisp(string value0)
            : this()
        {
            m_VALUE[0] = value0;

            ColumnCount = 1;
        }

        public CDataDisp(string value0, string value1, string value2)
            : this()
        {
            m_VALUE[0] = value0;
            m_VALUE[1] = value1;
            m_VALUE[2] = value2;

            ColumnCount = 3;
        }

        public CDataDisp(string value0, string value1)
            : this()
        {
            m_VALUE[0] = value0;
            m_VALUE[1] = value1;

            ColumnCount = 2;
        }

        public CDataDisp(string value0, string value1, string value2, string value3)
            : this()
        {
            m_VALUE[0] = value0;
            m_VALUE[1] = value1;
            m_VALUE[2] = value2;
            m_VALUE[3] = value3;

            ColumnCount = 4;
        }

        public CDataDisp(string value0, string value1, string value2, string value3, string value4)
            : this()
        {
            m_VALUE[0] = value0;
            m_VALUE[1] = value1;
            m_VALUE[2] = value2;
            m_VALUE[3] = value3;
            m_VALUE[4] = value4;

            ColumnCount = 5;
        }

        public CDataDisp(string value0, string value1, string value2, string value3, string value4, string value5)
            : this()
        {
            m_VALUE[0] = value0;
            m_VALUE[1] = value1;
            m_VALUE[2] = value2;
            m_VALUE[3] = value3;
            m_VALUE[4] = value4;
            m_VALUE[5] = value5;

            ColumnCount = 6;
        }

        public CDataDisp(string value0, string value1, string value2, string value3, string value4, string value5, string value6)
            : this()
        {
            m_VALUE[0] = value0;
            m_VALUE[1] = value1;
            m_VALUE[2] = value2;
            m_VALUE[3] = value3;
            m_VALUE[4] = value4;
            m_VALUE[5] = value5;
            m_VALUE[6] = value6;

            ColumnCount = 7;
        }

        public CDataDisp(string value0, string value1, string value2, string value3, string value4, string value5, string value6, string value7)
            : this()
        {
            m_VALUE[0] = value0;
            m_VALUE[1] = value1;
            m_VALUE[2] = value2;
            m_VALUE[3] = value3;
            m_VALUE[4] = value4;
            m_VALUE[5] = value5;
            m_VALUE[6] = value6;
            m_VALUE[7] = value7;

            ColumnCount = 8;
        }

        public CDataDisp(string value0, string value1, string value2, string value3, string value4, string value5, string value6, string value7, string value8, string value9)
            : this()
        {
            m_VALUE[0] = value0;
            m_VALUE[1] = value1;
            m_VALUE[2] = value2;
            m_VALUE[3] = value3;
            m_VALUE[4] = value4;
            m_VALUE[5] = value5;
            m_VALUE[6] = value6;
            m_VALUE[7] = value7;
            m_VALUE[8] = value8;
            m_VALUE[9] = value9;

            ColumnCount = 10;
        }

        public CDataDisp(string value0, string value1, string value2, string value3, string value4, string value5, string value6, string value7, string value8, string value9, string value10)
            : this()
        {
            m_VALUE[0] = value0;
            m_VALUE[1] = value1;
            m_VALUE[2] = value2;
            m_VALUE[3] = value3;
            m_VALUE[4] = value4;
            m_VALUE[5] = value5;
            m_VALUE[6] = value6;
            m_VALUE[7] = value7;
            m_VALUE[8] = value8;
            m_VALUE[9] = value9;
            m_VALUE[10] = value10;

            ColumnCount = 11;
        }

        public CDataDisp(string value0, string value1, string value2, string value3, string value4, string value5, string value6, string value7, string value8, string value9, string value10, string value11)
            : this()
        {
            m_VALUE[0] = value0;
            m_VALUE[1] = value1;
            m_VALUE[2] = value2;
            m_VALUE[3] = value3;
            m_VALUE[4] = value4;
            m_VALUE[5] = value5;
            m_VALUE[6] = value6;
            m_VALUE[7] = value7;
            m_VALUE[8] = value8;
            m_VALUE[9] = value9;
            m_VALUE[10] = value10;
            m_VALUE[11] = value11;

            ColumnCount = 12;
        }

        public CDataDisp(string value0, string value1, string value2, string value3, string value4, string value5, string value6, string value7, string value8, string value9, string value10, string value11, string value12, string value13)
            : this()
        {
            m_VALUE[0] = value0;
            m_VALUE[1] = value1;
            m_VALUE[2] = value2;
            m_VALUE[3] = value3;
            m_VALUE[4] = value4;
            m_VALUE[5] = value5;
            m_VALUE[6] = value6;
            m_VALUE[7] = value7;
            m_VALUE[8] = value8;
            m_VALUE[9] = value9;
            m_VALUE[10] = value10;
            m_VALUE[11] = value11;
            m_VALUE[12] = value12;
            m_VALUE[13] = value13;

            ColumnCount = 14;
        }
    }
}
