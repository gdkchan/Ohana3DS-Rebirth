using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Ohana3DS_Rebirth
{
    public partial class OForm : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int WM_NCHITTEST = 0x84;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  //Turn on WS_EX_COMPOSITED
                return cp;
            }
        }
       
        #region "WndProc Form Resize"
            protected override void WndProc(ref Message m)
            {
                const int HTBOTTOMRIGHT = 17;

                if (m.Msg == WM_NCHITTEST && this.WindowState == FormWindowState.Normal)
                {
                    int x = (int)(m.LParam.ToInt32() & 0xFFFF);
                    int y = (int)((m.LParam.ToInt32() & 0xFFFF0000) >> 16);
                    Point pt = PointToClient(new Point(x, y));
                    if (pt.X > ClientSize.Width - 24 && pt.Y > ClientSize.Height - 24)
                    {
                        m.Result = (IntPtr)HTBOTTOMRIGHT;
                        return;
                    }
                }
                base.WndProc(ref m);
            }
        #endregion

        #region "Initialize/Control Box"
            public OForm()
            {
                InitializeComponent();
                this.MinimumSize = new Size(128, 74);
                this.DoubleBuffered = true;
            }

            private void OForm_MouseDown(object sender, MouseEventArgs e)
            {
                if (e.Button == MouseButtons.Left && e.Y < 28)
                {
                    ReleaseCapture();
                    SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                }
            }

            private void OForm_Layout(object sender, LayoutEventArgs e)
            {
                if (this.WindowState != FormWindowState.Maximized)
                {
                    BtnMinMax.Image = Ohana3DS_Rebirth.Properties.Resources.btnmaximize;
                }
                else
                {
                    BtnMinMax.Image = Ohana3DS_Rebirth.Properties.Resources.btnnormal;
                }
            }

            private void OForm_MouseMove(object sender, MouseEventArgs e)
            {
                SendMessage(Handle, WM_NCHITTEST, 0, (int)(e.X | (e.Y << 16)));
            }

            private void BtnClose_MouseEnter(object sender, EventArgs e)
            {
                BtnClose.BackgroundImage = Ohana3DS_Rebirth.Properties.Resources.hover_red;
            }

            private void BtnMinMax_MouseEnter(object sender, EventArgs e)
            {
                switch (this.WindowState)
                {
                    case FormWindowState.Maximized:
                        BtnMinMax.BackgroundImage = Ohana3DS_Rebirth.Properties.Resources.hover_normal;
                    break;
                    case FormWindowState.Normal:
                        BtnMinMax.BackgroundImage = Ohana3DS_Rebirth.Properties.Resources.hover_maximize;
                    break;
                }
            }

            private void BtnMinimize_MouseEnter(object sender, EventArgs e)
            {
                BtnMinimize.BackgroundImage = Ohana3DS_Rebirth.Properties.Resources.hover_minimize;
            }

            private void Btn_MouseLeave(object sender, EventArgs e)
            {
                PictureBox Btn = (PictureBox)sender;
                Btn.BackgroundImage = null;
            }

            private void BtnClose_Click(object sender, EventArgs e)
            {
                this.Close();
            }

            private void BtnMinMax_Click(object sender, EventArgs e)
            {
                if (this.WindowState == FormWindowState.Maximized)
                {
                    BtnMinMax.Image = Ohana3DS_Rebirth.Properties.Resources.btnmaximize;
                    this.WindowState = FormWindowState.Normal;
                }
                else
                {
                    BtnMinMax.Image = Ohana3DS_Rebirth.Properties.Resources.btnnormal;
                    this.MaximumSize = Screen.PrimaryScreen.WorkingArea.Size;
                    this.WindowState = FormWindowState.Maximized;
                }
            }

            private void BtnMinimize_Click(object sender, EventArgs e)
            {
                this.WindowState = FormWindowState.Minimized;
            }
        #endregion
    }
}
