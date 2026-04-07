using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ADD0602E
{
    class CXPrt
    {
        public static void PrintBox_A4(DevExpress.XtraPrinting.BrickGraphics brickGraphics, float x1, float y1, float x2, float y2)
        {
            PrintBox_A4(brickGraphics, x1, y1, x2, y2, 1);
        }

        public static void PrintBox_A4(DevExpress.XtraPrinting.BrickGraphics brickGraphics, float x1, float y1, float x2, float y2, int vWidth)
        {
            DevExpress.XtraPrinting.Brick brick = brickGraphics.DrawRect(new System.Drawing.RectangleF(x1, y1, x2 - x1, y2 - y1), DevExpress.XtraPrinting.BorderSide.All, Color.Transparent, Color.Empty);
        }

        public static void PrintTxt_A4(DevExpress.XtraPrinting.BrickGraphics brickGraphics, float x1, float y1, float x2, float y2, string vData, string vFName, int vFSize, bool vBold, int vRLC)
        {
            DevExpress.XtraPrinting.TextBrick textBrick = brickGraphics.DrawString(vData, Color.Black, new RectangleF(x1 + 1, y1 + 1, x2 - x1 - 2, y2 - y1 - 2), DevExpress.XtraPrinting.BorderSide.None);
            textBrick.Font = new Font(vFName, vFSize, vBold == true ? FontStyle.Bold : FontStyle.Regular);
            if (vRLC == 2)
            {
                // 가운데 정렬
                textBrick.StringFormat = textBrick.StringFormat.ChangeAlignment(StringAlignment.Center);
            }
            else if (vRLC == 1)
            {
                // 오른쪽 정렬
                textBrick.StringFormat = textBrick.StringFormat.ChangeAlignment(StringAlignment.Far);
            }
            else
            {
                // 왼쪽 정렬
                textBrick.StringFormat = textBrick.StringFormat.ChangeAlignment(StringAlignment.Near);
            }
        }
    }
}
