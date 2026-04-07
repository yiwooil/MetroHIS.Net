using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Microsoft.Win32;

namespace ADB0206Q
{
    public partial class ADB0206Q_01 : Form
    {
        public String colFieldName;
        public String colFieldCaption;

        private bool IsFirst;

        public ADB0206Q_01()
        {
            InitializeComponent();
            IsFirst = true;
        }

        private void ADB0206Q_01_Activated(object sender, EventArgs e)
        {
            if (IsFirst == false) return;
            IsFirst = false;

            List<CColData> list = new List<CColData>();

            String[] names = colFieldName.Split(',');
            String[] captions = colFieldCaption.Split(',');
            for (int idx = 0; idx < names.Length; idx++)
            {
                if (names[idx] != "")
                {
                    CColData data = new CColData();
                    data.COLID = names[idx];
                    data.COLNM = captions[idx];
                    data.SEL = GetSel(data.COLID);

                    list.Add(data);
                }
            }
            grdMain.DataSource = list;
            grdMain.Refresh();
        }

        private bool GetSel(String colid)
        {
            RegistryKey reg;
            reg = Registry.CurrentUser.CreateSubKey("MetroHIS.NET").CreateSubKey("ADB").CreateSubKey("ADB0206Q.COLUMNS");
            String sel = reg.GetValue(colid,  "T").ToString();
            return (sel=="T");
        }

        private void SetSel(String colid, bool value)
        {
            RegistryKey reg;
            reg = Registry.CurrentUser.CreateSubKey("MetroHIS.NET").CreateSubKey("ADB").CreateSubKey("ADB0206Q.COLUMNS");
            reg.SetValue(colid, (value == true ? "T" : ""));
        }

        private void repositoryItemCheckEdit1_CheckedChanged(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.CheckEdit edit = sender as DevExpress.XtraEditors.CheckEdit;
            int row = grdMainView.FocusedRowHandle;
            List<CColData> list = (List<CColData>)grdMain.DataSource;
            String colid = list[row].COLID;
            String colname = list[row].COLNM;
            this.SetSel(colid, edit.Checked);
        }
    }
}
