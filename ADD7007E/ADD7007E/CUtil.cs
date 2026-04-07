using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD7007E
{
    class CUtil
    {
        static public string GetDate(string data)
        {
            string ret = "";
            if (data.Length >= 8)
            {
                ret = data.Substring(0, 8);
            }
            else
            {
                ret = data;
            }
            return ret;
        }

        static public string GetTime(string data)
        {
            string ret = "";
            if (data.Length >= 12)
            {
                ret = data.Substring(8, 4);
            }
            else if (data.Length >= 9)
            {
                ret = data.Substring(8);
            }
            return ret;
        }

        static public string GetDateTime(string date, string time)
        {
            if (date.Length > 8) date = date.Substring(0, 8);
            if (time.Length > 4) time = time.Substring(0, 4);

            return date + time;
        }

        static public string GetDateTime(string date, string hh, string mm)
        {
            if (date.Length > 8) date = date.Substring(0, 8);
            if (hh.Length > 2) hh = hh.Substring(0, 2);
            if (mm.Length > 2) mm = mm.Substring(0, 2);

            return date + hh + mm;
        }

        static public string GetRBString(System.Windows.Forms.RadioButton rb1, System.Windows.Forms.RadioButton rb2)
        {
            return GetYNString(rb1.Checked, rb2.Checked);
        }

        static public string GetRBString(System.Windows.Forms.RadioButton rb1, System.Windows.Forms.RadioButton rb2, System.Windows.Forms.RadioButton rb3)
        {
            return GetYNString(rb1.Checked, rb2.Checked, rb3.Checked);
        }

        static public string GetRBString(System.Windows.Forms.RadioButton rb1, System.Windows.Forms.RadioButton rb2, System.Windows.Forms.RadioButton rb3, System.Windows.Forms.RadioButton rb4, System.Windows.Forms.RadioButton rb5)
        {
            return GetYNString(rb1.Checked, rb2.Checked, rb3.Checked, rb4.Checked, rb5.Checked);
        }

        static private string GetYNString(bool y_value, bool n_value)
        {
            if (y_value == true) return "1";
            if (n_value == true) return "2";
            return "";
        }

        static private string GetYNString(bool value1, bool value2, bool value3)
        {
            if (value1 == true) return "1";
            if (value2 == true) return "2";
            if (value3 == true) return "3";
            return "";
        }

        static private string GetYNString(bool value1, bool value2, bool value3, bool value4, bool value5)
        {
            if (value1 == true) return "1";
            if (value2 == true) return "2";
            if (value3 == true) return "3";
            if (value4 == true) return "4";
            if (value5 == true) return "5";
            return "";
        }

        static public void SetGridCombo(DevExpress.XtraGrid.Columns.GridColumn column, params string[] items)
        {
            DevExpress.XtraEditors.Repository.RepositoryItemComboBox cbo = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            column.ColumnEdit = cbo;
            cbo.Items.Clear();
            foreach (string item in items)
            {
                cbo.Items.Add(item);
            }
        }

        static public void SetGridCheckedCombo(DevExpress.XtraGrid.Columns.GridColumn column, params string[] items)
        {
            DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit cbo = new DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit();
            column.ColumnEdit = cbo;
            cbo.Items.Clear();
            foreach (string item in items)
            {
                cbo.Items.Add(item);
            }
        }

        static public void RefreshGrid(DevExpress.XtraGrid.GridControl grid, DevExpress.XtraGrid.Views.Grid.GridView view)
        {
            if (grid.InvokeRequired)
            {
                // 폼 이외의 스레드에서 호출한 경우
                grid.BeginInvoke(new Action(() => view.RefreshData()));
            }
            else
            {
                // 폼에서 호출한 경우
                view.RefreshData();
                System.Windows.Forms.Application.DoEvents();
            }
        }

        static public string GetGridCheckedComboCode(string p_value, Dictionary<string, string> dicValue)
        {
            string ret = "";
            List<string> lst = new List<string>();
            string[] ary = p_value.Split('/');
            for (int i = 0; i < ary.Length; i++)
            {
                string val = GetDicKey(ary[i].Trim(), dicValue);
                if (val != "") lst.Add(val);
            }
            ret = string.Join("/", lst.ToArray());
            return ret;
        }

        static public string GetGridCheckedComboName(string p_value, Dictionary<string, string> dicValue)
        {
            string ret = "";
            List<string> lst = new List<string>();
            string[] ary = p_value.Split('/');
            for (int i = 0; i < ary.Length; i++)
            {
                string val = GetDicValue(ary[i].Trim(), dicValue);
                if (val != "") lst.Add(val);                
            }
            ret = string.Join(", ", lst.ToArray());
            return ret;
        }

        static private string GetDicValue(string p_value, Dictionary<string, string> dicValue)
        {
            foreach (var pair in dicValue)
            {
                if (pair.Key == p_value)
                {
                    return pair.Value;
                }
            }
            return "";
        }

        static private string GetDicKey(string p_value, Dictionary<string, string> dicValue)
        {
            foreach (var pair in dicValue)
            {
                if (pair.Value == p_value)
                {
                    return pair.Key;
                }
            }
            return "";
        }

        static public void SetComboboxSelectedValue(System.Windows.Forms.ComboBox cbo, string p_value)
        {
            cbo.SelectedValue = p_value;
            if (cbo.SelectedIndex == -1) cbo.SelectedValue = "";
        }

        static public string GetComboboxSelectedValue(System.Windows.Forms.ComboBox cbo)
        {
            return cbo.SelectedValue.ToString();
        }

        static public int GetMaxDropDownWidth(System.Windows.Forms.ComboBox cbo)
        {
            int maxWidth = 0;
            foreach (var item in cbo.Items)
            {
                int itemWidth = TextRenderer.MeasureText(item.ToString(), cbo.Font).Width;
                if (itemWidth > maxWidth)
                    maxWidth = itemWidth;
            }
            return maxWidth + 20;// 약간의 여유(20)
        }
    }
}
