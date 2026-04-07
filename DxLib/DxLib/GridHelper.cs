using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DxLib
{
    public class GridHelper
    {
        public static void BandReCaption(DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView view, string[] caption)
        {
            for (int i = 0; i < caption.Length; i += 3)
            {
                foreach (DevExpress.XtraGrid.Views.BandedGrid.GridBand band in view.Bands)
                {
                    if (band.Caption == caption[i])
                    {
                        band.Caption = caption[i + 1] + Environment.NewLine + caption[i + 2];
                    }
                    if (band.Children.Count >= 0)
                    {
                        foreach (DevExpress.XtraGrid.Views.BandedGrid.GridBand band2 in band.Children)
                        {
                            if (band2.Caption == caption[i])
                            {
                                band2.Caption = caption[i + 1] + Environment.NewLine + caption[i + 2];
                            }
                        }
                    }
                }
            }
        }

        public static void HideColumnIfZero(string[] check_field, DevExpress.XtraGrid.Views.Grid.GridView view)
        {
            for (int i = 0; i < check_field.Length; i++)
            {
                bool show = false;
                for (int row = 0; row < view.RowCount; row++)
                {
                    if (show == false && ToDouble(view.GetRowCellValue(row, check_field[i]).ToString()) != 0) show = true;
                    if (show == true) break;
                }

                view.Columns[check_field[i]].Visible = show;
            }
        }

        public static void HideColumnIfZero(string[] check_field, DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView view)
        {
            for (int i = 0; i < check_field.Length; i++)
            {
                bool show = false;
                for (int row = 0; row < view.RowCount; row++)
                {
                    if (show == false && ToDouble(view.GetRowCellValue(row, check_field[i]).ToString()) != 0) show = true;
                    if (show == true) break;
                }

                view.Columns[check_field[i]].Visible = show;
            }
        }

        public static void HideColumnIfZeroInBand(string[] check_field, DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView view)
        {
            for (int i = 0; i < check_field.Length; i += 2)
            {
                bool show1 = false;
                bool show2 = false;
                for (int row = 0; row < view.RowCount; row++)
                {
                    if (show1 == false && ToDouble(view.GetRowCellValue(row, check_field[i]).ToString()) != 0) show1 = true;
                    if (show2 == false && ToDouble(view.GetRowCellValue(row, check_field[i + 1]).ToString()) != 0) show2 = true;
                    if (show1 == true && show2 == true) break;
                }
                // 순서가 중요함. [i + 1]을 먼저하고 [i]를 나중에 해야함.
                view.Columns[check_field[i + 1]].Visible = show1 || show2;
                view.Columns[check_field[i]].Visible = show1 || show2;
            }
        }

        public static void SetBandVisible(DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView view)
        {
            // band에 속해있는 column을 구한다.
            Dictionary<string, string> dicBand = new Dictionary<string, string>();
            foreach (DevExpress.XtraGrid.Views.BandedGrid.GridBand band in view.Bands)
            {
                if (band.Children.Count > 0)
                {
                    foreach (DevExpress.XtraGrid.Views.BandedGrid.GridBand band2 in band.Children)
                    {
                        if (band2.Columns.Count > 0)
                        {
                            for (int j = 0; j < band2.Columns.Count; j++)
                            {
                                if (dicBand.ContainsKey(band.Name) == true)
                                {
                                    dicBand[band.Name] = dicBand[band.Name] + "," + band2.Columns[j].FieldName;
                                }
                                else
                                {
                                    dicBand[band.Name] = band2.Columns[j].FieldName;
                                }
                                if (dicBand.ContainsKey(band2.Name) == true)
                                {
                                    dicBand[band2.Name] = dicBand[band2.Name] + "," + band2.Columns[j].FieldName;
                                }
                                else
                                {
                                    dicBand[band2.Name] = band2.Columns[j].FieldName;
                                }
                            }
                        }
                    }
                }
                if (band.Columns.Count > 0)
                {
                    for (int j = 0; j < band.Columns.Count; j++)
                    {
                        if (dicBand.ContainsKey(band.Name) == true)
                        {
                            dicBand[band.Name] = dicBand[band.Name] + "," + band.Columns[j].FieldName;
                        }
                        else
                        {
                            dicBand[band.Name] = band.Columns[j].FieldName;
                        }
                    }
                }
            }
            // band에 속해있는 column이 visible = false이면 band도 visible = false임.
            foreach (KeyValuePair<string, string> kv in dicBand)
            {
                string[] fields = kv.Value.Split(',');
                bool show = false;
                foreach (string field in fields)
                {
                    if (field != "" && view.Columns[field].Visible == true) show = true;
                }
                if (view.Bands[kv.Key] != null) view.Bands[kv.Key].Visible = show;
            }
        }

        private static double ToDouble(String Value)
        {
            double result = 0;
            double.TryParse(Value, out result);
            return result;
        }
    }
}
