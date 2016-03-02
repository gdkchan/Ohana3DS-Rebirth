using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using Ohana3DS_Rebirth.Properties;

namespace Ohana3DS_Rebirth
{
    public partial class OForm : Form
    {
        const int gripSize = 4;
        private bool resizable = true;
        private bool minimizeBox = true;

        private const int HT_CAPTION = 2;
        private const int WM_SETREDRAW = 0xb;
        private const int WM_NCHITTEST = 0x84;
        private const int WM_NCLBUTTONDOWN = 0xa1;

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        public bool Resizable
        {
            get
            {
                return resizable;
            }
            set
            {
                resizable = value;
                BtnMinMax.Visible = value;
            }
        }

        public bool ShowMinimize
        {
            get
            {
                return minimizeBox;
            }
            set
            {
                minimizeBox = value;
                BtnMinimize.Visible = minimizeBox;
            }
        }

        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
                LblTitle.Text = value;
            }
        }

        public Image TitleIcon
        {
            get
            {
                return PicIcon.Image;
            }
            set
            {
                PicIcon.Image = value;
            }
        }

        public void SuspendDrawing()
        {
            SendMessage(Handle, WM_SETREDRAW, 0, 0);
        }

        public void ResumeDrawing()
        {
            SendMessage(Handle, WM_SETREDRAW, 1, 0);
            Refresh();
        }

        #region "WndProc Form Resize"
        protected override void WndProc(ref Message m)
        {
            const int HTLEFT = 10;
            const int HTRIGHT = 11;
            const int HTTOP = 12;
            const int HTTOPLEFT = 13;
            const int HTTOPRIGHT = 14;
            const int HTBOTTOM = 15;
            const int HTBOTTOMLEFT = 16;
            const int HTBOTTOMRIGHT = 17;

            if (m.Msg == WM_NCHITTEST && WindowState == FormWindowState.Normal && resizable)
            {
                int x = m.LParam.ToInt32() & 0xffff;
                int y = (int)((m.LParam.ToInt32() & 0xffff0000) >> 16);
                Point pt = PointToClient(new Point(x, y));
                if (pt.X < gripSize && pt.Y < gripSize)
                {
                    m.Result = (IntPtr)HTTOPLEFT;
                    return;
                }
                if (pt.X >= ClientSize.Width - gripSize && pt.Y < gripSize)
                {
                    m.Result = (IntPtr)HTTOPRIGHT;
                    return;
                }
                if (pt.X < gripSize && pt.Y >= ClientSize.Height - gripSize)
                {
                    m.Result = (IntPtr)HTBOTTOMLEFT;
                    return;
                }
                if (pt.X >= ClientSize.Width - gripSize && pt.Y >= ClientSize.Height - gripSize)
                {
                    m.Result = (IntPtr)HTBOTTOMRIGHT;
                    return;
                }
                if (pt.X < gripSize)
                {
                    m.Result = (IntPtr)HTLEFT;
                    return;
                }
                if (pt.X >= ClientSize.Width - gripSize)
                {
                    m.Result = (IntPtr)HTRIGHT;
                    return;
                }
                if (pt.Y < gripSize)
                {
                    m.Result = (IntPtr)HTTOP;
                    return;
                }
                if (pt.Y >= ClientSize.Height - gripSize)
                {
                    m.Result = (IntPtr)HTBOTTOM;
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
            DoubleBuffered = true;
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
            if (WindowState != FormWindowState.Maximized && resizable)
            {
                BtnMinMax.Image = Resources.ui_icon_maximize;

                ContentContainer.Location = new Point(gripSize, gripSize);
                ContentContainer.Size = new Size(Width - gripSize * 2, Height - gripSize * 2);
            }
            else
            {
                BtnMinMax.Image = Resources.ui_icon_unmaximize;

                ContentContainer.Location = new Point(1, 1);
                ContentContainer.Size = new Size(Width - 2, Height - 2);
            }
        }

        private void OForm_MouseMove(object sender, MouseEventArgs e)
        {
            SendMessage(Handle, WM_NCHITTEST, 0, e.X | (e.Y << 16));
        }

        private void BtnClose_MouseEnter(object sender, EventArgs e)
        {
            BtnClose.BackgroundImage = Resources.ui_icon_closehover;
        }

        private void BtnMinMax_MouseEnter(object sender, EventArgs e)
        {
            switch (WindowState)
            {
                case FormWindowState.Maximized:
                    BtnMinMax.BackgroundImage = Resources.ui_icon_unmaximizehover;
                    break;
                case FormWindowState.Normal:
                    BtnMinMax.BackgroundImage = Resources.ui_icon_maximizehover;
                    break;
            }
        }

        private void BtnMinimize_MouseEnter(object sender, EventArgs e)
        {
            BtnMinimize.BackgroundImage = Resources.ui_icon_minimizehover;
        }

        private void Btn_MouseLeave(object sender, EventArgs e)
        {
            PictureBox Btn = (PictureBox)sender;
            Btn.BackgroundImage = null;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnMinMax_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
            {
                BtnMinMax.Image = Resources.ui_icon_maximize;
                WindowState = FormWindowState.Normal;
            }
            else
            {
                BtnMinMax.Image = Resources.ui_icon_unmaximize;
                MaximumSize = Screen.FromControl(this).WorkingArea.Size;
                WindowState = FormWindowState.Maximized;
            }
        }

        private void BtnMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
        #endregion
    }
}
