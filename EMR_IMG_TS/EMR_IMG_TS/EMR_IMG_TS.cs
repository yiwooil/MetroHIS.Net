using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EMR_IMG_TS
{
    public partial class EMR_IMG_TS : Form
    {
        private String m_User;
        private String m_Pwd;
        private String m_Prjcd;
        private String m_Addpara;
        private String m_HospMulti;

        private String m_SacnPath = "";

        private PictureBox[] picMain = new PictureBox[10];
        private PrintDocument printDoc = new PrintDocument();

        private int m_CurrentPrintIndex = 0; // 현재 출력중인 페이지 번호
        private int m_PageCount = 0;
        private int m_tsLeft = 5;
        private int m_tsRight = 5;
        private int m_tsWidth = 200;
        private int m_tsHeight = 200;
        private int m_imgWidth = 0;
        private int m_imgHeight = 0;

        private bool IsFirst;
        private bool OnPgm = false;

        public EMR_IMG_TS()
        {
            InitializeComponent();

            // PrintPage 이벤트 핸들러 등록
            printDoc.PrintPage += new PrintPageEventHandler(PrintDoc_PrintPage);
        }

        public EMR_IMG_TS(String user, String pwd, String prjcd, String addpara)
            : this()
        {
            m_User = user;
            m_Pwd = pwd;
            m_Prjcd = prjcd;
            m_Addpara = addpara;
            m_HospMulti = GetHospmulti();

            CreatePopupMenu();

            m_SacnPath = MetroLib.INIHelper.ReadIniFile("C:/Metro/DLL/MetroHIS.ini", "MetroHis", "SCANFILEPATH");
        }

        private string GetHospmulti()
        {
            try
            {
                string ret = "";
                string strConn = MetroLib.DBHelper.GetConnectionString();
                using (OleDbConnection conn = new OleDbConnection(strConn))
                {
                    conn.Open();
                    string sql = "";
                    sql = "SELECT MULTIFG FROM TA94 WHERE USRID=? AND PRJID=?";
                    using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                    {
                        cmd.Parameters.Add(new OleDbParameter("@1", m_User));
                        cmd.Parameters.Add(new OleDbParameter("@2", m_Prjcd));

                        OleDbDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            ret = reader["MULTIFG"].ToString();
                        }
                    }
                }
                return ret;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return "";
            }
        }

        private void CreatePopupMenu()
        {
            //ContextMenu cm = new ContextMenu();
            //cm.MenuItems.Add("구입일자순 정렬", new EventHandler(mnuSelFst_Click));
            //grdMain.ContextMenu = cm;
        }

        private void EMR_IMG_TS_Load(object sender, EventArgs e)
        {
            IsFirst = true;
        }

        private void EMR_IMG_TS_Activated(object sender, EventArgs e)
        {
            if (IsFirst == false) return;
            IsFirst = false;

            for (int i = 0; i < 10; i++)
            {
                picMain[i] = new PictureBox();
                picMain[i].Left = 3;
                picMain[i].Top = 3;
                picMain[i].Width = 100;
                picMain[i].Height = 100;

                panMain.Controls.Add(picMain[i]);
            }

            OnPgm = true;

            cboZoom.Text = MetroLib.RegHelper.GetValue(this.Name, "Zoom", "100");
            txtLeftMargin.Text = MetroLib.RegHelper.GetValue(this.Name, "LeftMargin", "30");
            txtRightMargin.Text = MetroLib.RegHelper.GetValue(this.Name, "RightMargin", "30");
            txtTopMargin.Text = MetroLib.RegHelper.GetValue(this.Name, "TopMargin", "20");
            txtBottomMargin.Text = MetroLib.RegHelper.GetValue(this.Name, "BottomMargin", "20");
            
            OnPgm = false;

            Query();
        }

        private void Query()
        {
            for (int i = 0; i < 10; i++)
            {
                picMain[i].Visible = false;
            }

            string[] addpara = (m_Addpara + (char)21 + (char)21 + (char)21 + (char)21 + (char)21).Split((char)21);
            string pid = addpara[0];
            string bdiv = addpara[1];
            string exdt = addpara[2];
            string seq = addpara[3];
            string rptcd = addpara[4];

            if (m_Addpara == "")
            {
                pid = "T00000011";
                bdiv = "O";
                exdt = "20250701";
                seq = "1";
                rptcd = "ZZ01";
            }

            string path = "";
            string tsaStatus = "";
            string subPageList = "";
            string tsaDate = "";
            string tsaTime = "";

            string strConn = MetroLib.DBHelper.GetConnectionString();

            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                m_PageCount = 0;

                // 첫 번째 페이지 
                string sql = "SELECT PATH,TSA_STATUS,SUB_PAGE_LIST,SYSDT,SYSTM,TSA_DATE,TSA_TIME FROM TG02 WHERE PID=? AND BDIV=? AND EXDT=? AND SEQ=? AND RPTCD=?";

                List<Object> para = new List<object>();
                para.Add(pid);
                para.Add(bdiv);
                para.Add(exdt);
                para.Add(seq);
                para.Add(rptcd);

                MetroLib.SqlHelper.GetDataReader(sql, para, conn, delegate(OleDbDataReader reader)
                {
                    m_PageCount = 1;

                    path = reader["PATH"].ToString();
                    tsaStatus = reader["TSA_STATUS"].ToString();
                    subPageList = reader["SUB_PAGE_LIST"].ToString();
                    //tsaDate = reader["SYSDT"].ToString();
                    //tsaTime = reader["SYSTM"].ToString();
                    tsaDate = reader["TSA_DATE"].ToString();
                    tsaTime = reader["TSA_TIME"].ToString();

                    return MetroLib.SqlHelper.BREAK;
                });

                if (subPageList != "")
                {
                    string[] subPage = subPageList.Split(';');
                    m_PageCount = subPage.Length + 1;
                }
                LoadImage(path, tsaStatus, 0, tsaDate, tsaTime);

                // 두 번째 페이지부터는 이곳에서 불러온다.
                for (int pageNo = 1; pageNo < m_PageCount; pageNo++)
                {
                    string[] subPage = subPageList.Split(';');

                    para.Clear();
                    para.Add(pid);
                    para.Add(bdiv);
                    para.Add(exdt);
                    para.Add(subPage[pageNo - 1]);
                    para.Add(rptcd);

                    MetroLib.SqlHelper.GetDataReader(sql, para, conn, delegate(OleDbDataReader reader)
                    {
                        path = reader["PATH"].ToString();
                        tsaStatus = reader["TSA_STATUS"].ToString();
                        //tsaDate = reader["SYSDT"].ToString();
                        //tsaTime = reader["SYSTM"].ToString();
                        tsaDate = reader["TSA_DATE"].ToString();
                        tsaTime = reader["TSA_TIME"].ToString();

                        return MetroLib.SqlHelper.BREAK;
                    });
                    LoadImage(path, tsaStatus, pageNo, tsaDate, tsaTime);
                }

                // 환자 기본 정보
                sql = "SELECT PNM FROM TA01 WHERE PID=?";
                para.Clear();
                para.Add(pid);
                MetroLib.SqlHelper.GetDataReader(sql, para, conn, delegate(OleDbDataReader reader)
                {
                    txtPid.Text = pid;
                    txtPnm.Text = reader["PNM"].ToString();

                    return MetroLib.SqlHelper.BREAK;
                });

            }

            ActionZoom();

        }

        private void LoadImage(string path, string tsaStatus, int pageNo, string tsaDate, string tsaTime)
        {
            if (File.Exists(m_SacnPath + path) == true)
            {
                Image img = Image.FromFile(m_SacnPath + path);

                using (Graphics g = Graphics.FromImage(img))
                {
                    if (tsaStatus.Equals("S", StringComparison.CurrentCultureIgnoreCase) && tsaDate != "")
                    {
                        // 2025.08.21 WOOIL - TSA_DATE에 값이 있는 경우만
                        using (Image tsImg = Properties.Resources.time_stamp_image_1)
                        {
                            using (Bitmap bmp = ResizeImage(tsImg, m_tsWidth, m_tsHeight))
                            {
                                bmp.MakeTransparent(Color.White);
                                g.DrawImage(bmp, m_tsLeft, m_tsRight);
                                string dispDate = tsaDate.Substring(0, 4) + "." + tsaDate.Substring(4, 2) + "." + tsaDate.Substring(6, 2);
                                g.DrawString(dispDate, new Font("굴림체", 18, FontStyle.Bold), new SolidBrush(Color.Black), m_tsLeft + 30, m_tsHeight - 100);
                            }
                        }
                    }
                }

                picMain[pageNo].SizeMode = PictureBoxSizeMode.Normal;

                picMain[pageNo].Image = img;
                picMain[pageNo].Width = img.Width;
                picMain[pageNo].Height = img.Height;
                picMain[pageNo].Visible = true;

                picMain[pageNo].SizeMode = PictureBoxSizeMode.Normal;

                m_imgWidth = img.Width;
                m_imgHeight = img.Height;
            }
        }

        private Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        private void PictureBoxLineUp()
        {
            for (int i = 1; i < 10; i++)
            {
                int left = 0;
                int top = 0;

                left = picMain[i - 1].Left + picMain[i - 1].Width + 10; // 이전 콘트롤 오른쪽에 위치시키려고 해본다.

                if (left + picMain[i].Width > panMain.Left + panMain.Width)
                {
                    left = 3; // panel을 벗어나면 아래로 보낸다.
                    top = picMain[i - 1].Top + picMain[i - 1].Height + 10;
                }
                else
                {
                    top = picMain[i - 1].Top;
                }

                picMain[i].Left = left;
                picMain[i].Top = top;
            }
        }

        private void cboZoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (OnPgm) return;
            ActionZoom();
            MetroLib.RegHelper.SaveValue(this.Name, "Zoom", cboZoom.Text.Trim());
        }

        private void cboZoom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                ActionZoom();
                MetroLib.RegHelper.SaveValue(this.Name, "Zoom", cboZoom.Text.Trim());
            }
        }

        private void ActionZoom()
        {
            string p_zoom = cboZoom.Text.ToString().Trim();

            float zoom = (float)(MetroLib.StrHelper.ToDouble(p_zoom) / 100.0);
            if (zoom < 0.1) return;

            int w = (int)(m_imgWidth * zoom);
            int h = (int)(m_imgHeight * zoom);

            for (int i = 0; i < m_PageCount; i++)
            {
                picMain[i].Width = w;
                picMain[i].Height = h;

                picMain[i].SizeMode = PictureBoxSizeMode.StretchImage;
            }

            PictureBoxLineUp();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (m_PageCount == 0)
            {
                MessageBox.Show("출력할 이미지가 없습니다.");
                return;
            }

            m_CurrentPrintIndex = 1; // 출력 인덱스 초기화

            int leftMargin = 0;
            int rightMargin = 0;
            int topMargin = 0;
            int bottomMargin = 0;

            if (int.TryParse(txtLeftMargin.Text.Trim(), out leftMargin) == false) leftMargin = 100;
            if (int.TryParse(txtRightMargin.Text.Trim(), out rightMargin) == false) rightMargin = 100;
            if (int.TryParse(txtTopMargin.Text.Trim(), out topMargin) == false) topMargin = 100;
            if (int.TryParse(txtBottomMargin.Text.Trim(), out bottomMargin) == false) bottomMargin = 100;            

            printDoc.DefaultPageSettings.Margins = new Margins(leftMargin, rightMargin, topMargin, bottomMargin);

            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDoc;
            printDialog.UseEXDialog = true;
            DialogResult dr = printDialog.ShowDialog();

            if (dr == DialogResult.OK)
            {
                printDoc.Print();
            }
        }

        private void PrintDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            if (m_CurrentPrintIndex <= m_PageCount && picMain[m_CurrentPrintIndex - 1].Image != null)
            {
                Image img = picMain[m_CurrentPrintIndex - 1].Image;

                // 출력 영역에 맞게 이미지 크기 조절 (비율 유지)
                Rectangle marginBounds = e.MarginBounds;
                Size imgSize = GetScaledSize(img, marginBounds.Size);

                // 이미지 출력 위치 중앙 정렬
                int x = marginBounds.Left + (marginBounds.Width - imgSize.Width) / 2;
                int y = marginBounds.Top + (marginBounds.Height - imgSize.Height) / 2;

                e.Graphics.DrawImage(img, new Rectangle(x, y, imgSize.Width, imgSize.Height));

                m_CurrentPrintIndex++;
                e.HasMorePages = m_CurrentPrintIndex <= m_PageCount;
            }
            else
            {
                // 출력할 이미지가 더 이상 없으면 출력 종료
                e.HasMorePages = false;
            }
        }

        private Size GetScaledSize(Image img, Size maxSize)
        {
            double ratioX = (double)maxSize.Width / img.Width;
            double ratioY = (double)maxSize.Height / img.Height;
            double ratio = Math.Min(ratioX, ratioY);

            int newWidth = (int)(img.Width * ratio);
            int newHeight = (int)(img.Height * ratio);

            return new Size(newWidth, newHeight);
        }

        private void txtLeftMargin_Leave(object sender, EventArgs e)
        {
            MetroLib.RegHelper.SaveValue(this.Name, "LeftMargin", txtLeftMargin.Text.Trim());
        }

        private void txtRightMargin_Leave(object sender, EventArgs e)
        {
            MetroLib.RegHelper.SaveValue(this.Name, "RightMargin", txtRightMargin.Text.Trim());
        }

        private void txtTopMargin_Leave(object sender, EventArgs e)
        {
            MetroLib.RegHelper.SaveValue(this.Name, "TopMargin", txtTopMargin.Text.Trim());
        }

        private void txtBottomMargin_Leave(object sender, EventArgs e)
        {
            MetroLib.RegHelper.SaveValue(this.Name, "BottomMargin", txtBottomMargin.Text.Trim());
        }

        private void cboZoom_Leave(object sender, EventArgs e)
        {
            ActionZoom();
            MetroLib.RegHelper.SaveValue(this.Name, "Zoom", cboZoom.Text.Trim());
        }

        private void EMR_IMG_TS_Resize(object sender, EventArgs e)
        {
            ActionZoom();
        }
    }
}
