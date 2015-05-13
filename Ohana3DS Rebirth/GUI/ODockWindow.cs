//ODock Window made for Ohana3DS by gdkchan
//Inherit from this control to make any window you want to be dockable.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ohana3DS_Rebirth.GUI
{
    public partial class ODockWindow : UserControl
    {
        private bool drag;
        private int mouseX;
        private int mouseY;

        private bool dockSwitch = false;

        private Bitmap hoverRed = new Bitmap(16, 16);
        private Bitmap hoverBlue = new Bitmap(16, 16);

        public event EventHandler MoveEnded;
        public event EventHandler ToggleDockable;

        private String title;
        public Control container;

        public ODockWindow()
        {
            Graphics g1 = Graphics.FromImage(hoverRed);
            Graphics g2 = Graphics.FromImage(hoverBlue);
            g1.FillRectangle(new SolidBrush(Color.FromArgb(0x7f, Color.Crimson)), new Rectangle(1, 1, hoverRed.Width - 2, hoverRed.Height - 2));
            g2.FillRectangle(new SolidBrush(Color.FromArgb(0x7f, Color.FromArgb(15, 82, 186))), new Rectangle(1, 1, hoverBlue.Width - 2, hoverBlue.Height - 2));
            
            InitializeComponent();
        }

        private void ODockWindow_Layout(object sender, LayoutEventArgs e)
        {
            updateTitle();
        }

        private void updateTitle()
        {
            LblTitle.Text = DrawingHelper.clampText(title, LblTitle.Font, this.Width - 32);
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
        }

        private void WindowTop_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                drag = true;
                mouseX = Cursor.Position.X - this.Left;
                mouseY = Cursor.Position.Y - this.Top;
            }
        }

        private void WindowTop_MouseMove(object sender, MouseEventArgs e)
        {
            if (drag)
            {
                int x = Cursor.Position.X - mouseX;
                int y = Cursor.Position.Y - mouseY;
                if (x < 0) x = 0;
                if (y < 0) y = 0;
                if (x >= container.Width) x = container.Width - 1;
                if (y >= container.Height) y = container.Height - 1;
                this.Location = new Point(x, y);
                this.BringToFront();
            }
        }

        private void WindowTop_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                drag = false;
                this.MoveEnded(this, EventArgs.Empty);
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
                if (e.Button == MouseButtons.Left) this.Visible = false;
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
                    this.ToggleDockable(this, EventArgs.Empty);
                    if (dockSwitch)
                    {
                        BtnPin.Image = Ohana3DS_Rebirth.Properties.Resources.icn_locked;
                    }
                    else
                    {
                        BtnPin.Image = Ohana3DS_Rebirth.Properties.Resources.icn_dockable;
                    }
                    BtnPin.BackgroundImage = hoverBlue;
                }
            }
        #endregion
    }
}
