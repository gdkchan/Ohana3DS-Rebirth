//ODock Window made for Ohana3DS by gdkchan
//Inherit from this control to make any window you want to be dockable.

using System;
using System.Drawing;
using System.Windows.Forms;

using Ohana3DS_Rebirth.Properties;

namespace Ohana3DS_Rebirth.GUI
{
    public partial class ODockWindow : UserControl
    {
        private bool drag;
        private int mouseX;
        private int mouseY;

        private bool dockSwitch;

        private Bitmap hoverRed = new Bitmap(16, 16);
        private Bitmap hoverBlue = new Bitmap(16, 16);

        public event EventHandler MoveEnded;
        public event EventHandler ToggleDockable;

        private String title;
        public Control container;

        public ODockWindow()
        {
            using (Graphics g1 = Graphics.FromImage(hoverRed))
            {
                using (Graphics g2 = Graphics.FromImage(hoverBlue))
                {
                    g1.FillRectangle(new SolidBrush(Color.FromArgb(0x7f, ColorManager.hoverClose)), new Rectangle(1, 1, hoverRed.Width - 2, hoverRed.Height - 2));
                    g2.FillRectangle(new SolidBrush(ColorManager.highlight), new Rectangle(1, 1, hoverBlue.Width - 2, hoverBlue.Height - 2));
                }
            }

            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            InitializeComponent();
        }

        private void ODockWindow_Layout(object sender, LayoutEventArgs e)
        {
            updateTitle();
        }

        private void updateTitle()
        {
            LblTitle.Text = DrawingHelper.clampText(title, LblTitle.Font, Width - 32);
        }

        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
                updateTitle();
            }
        }

        public bool Drag
        {
            get
            {
                return drag;
            }
        }

        public virtual void dispose()
        {
            //Dispose all unmanaged stuff here!
            hoverRed.Dispose();
            hoverBlue.Dispose();
        }

        private void WindowTop_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                drag = true;
                mouseX = Cursor.Position.X - Left;
                mouseY = Cursor.Position.Y - Top;
            }
        }

        private void WindowTop_MouseMove(object sender, MouseEventArgs e)
        {
            if (drag)
            {
                int x = Math.Max(-(WindowTop.Width - 40), Math.Min(container.Width - 8, Cursor.Position.X - mouseX));
                int y = Math.Max(-(WindowTop.Height - 8), Math.Min(container.Height - 8, Cursor.Position.Y - mouseY));
                Location = new Point(x, y);
                BringToFront();
            }
        }

        private void WindowTop_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                drag = false;
                MoveEnded(this, EventArgs.Empty);
            }
        }

        #region "Control Box"
            private void BtnClose_MouseEnter(object sender, EventArgs e)
            {
                BtnClose.BackgroundImage = hoverRed;
            }

            private void BtnClose_MouseLeave(object sender, EventArgs e)
            {
                BtnClose.BackgroundImage = null;
            }

            private void BtnClose_MouseDown(object sender, MouseEventArgs e)
            {
                if (e.Button == MouseButtons.Left) Visible = false;
            }

            private void BtnPin_MouseEnter(object sender, EventArgs e)
            {
                BtnPin.BackgroundImage = hoverBlue;
            }

            private void BtnPin_MouseLeave(object sender, EventArgs e)
            {
                BtnPin.BackgroundImage = null;
            }

            private void BtnPin_MouseDown(object sender, MouseEventArgs e)
            {
                if (e.Button == MouseButtons.Left)
                {
                    dockSwitch = !dockSwitch;
                    ToggleDockable(this, EventArgs.Empty);
                    BtnPin.Image = dockSwitch 
                        ? Resources.icn_locked 
                        : Resources.icn_dockable;
                    BtnPin.BackgroundImage = hoverBlue;
                }
            }
        #endregion

    }
}
