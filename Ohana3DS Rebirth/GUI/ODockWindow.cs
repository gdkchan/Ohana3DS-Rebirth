using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ohana3DS_Rebirth.GUI
{
    public partial class ODockWindow : UserControl
    {
        private bool drag;
        private int mouseX;
        private int mouseY;

        private bool dockSwitch = false;

        public event EventHandler MoveEnded;

        public ODockWindow()
        {
            InitializeComponent();
        }

        public string Title
        {
            get
            {
                return LblTitle.Text;
            }
        }

        public bool Drag
        {
            get
            {
                return drag;
            }
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
                this.Location = new Point(Cursor.Position.X - mouseX, Cursor.Position.Y - mouseY);
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
                BtnClose.Image = Ohana3DS_Rebirth.Properties.Resources.icn_close_hover;
            }
            private void BtnClose_MouseLeave(object sender, EventArgs e)
            {
                BtnClose.Image = Ohana3DS_Rebirth.Properties.Resources.icn_close;
            }

            private void BtnMax_MouseEnter(object sender, EventArgs e)
            {
                BtnMax.Image = Ohana3DS_Rebirth.Properties.Resources.icn_maximize_hover;
            }
            private void BtnMax_MouseLeave(object sender, EventArgs e)
            {
                BtnMax.Image = Ohana3DS_Rebirth.Properties.Resources.icn_maximize;
            }

            private void BtnPin_MouseEnter(object sender, EventArgs e)
            {
                if (dockSwitch)
                {
                    BtnPin.Image = Ohana3DS_Rebirth.Properties.Resources.icn_locked_hover;
                }
                else
                {
                    BtnPin.Image = Ohana3DS_Rebirth.Properties.Resources.icn_dockable_hover;
                }
            }
            private void BtnPin_MouseLeave(object sender, EventArgs e)
            {
                if (dockSwitch)
                {
                    BtnPin.Image = Ohana3DS_Rebirth.Properties.Resources.icn_locked;
                }
                else
                {
                    BtnPin.Image = Ohana3DS_Rebirth.Properties.Resources.icn_dockable;
                }
            }
            private void BtnPin_MouseDown(object sender, MouseEventArgs e)
            {
                if (e.Button == MouseButtons.Left)
                {
                    dockSwitch = !dockSwitch;
                    if (dockSwitch)
                    {
                        BtnPin.Image = Ohana3DS_Rebirth.Properties.Resources.icn_locked_hover;
                    }
                    else
                    {
                        BtnPin.Image = Ohana3DS_Rebirth.Properties.Resources.icn_dockable_hover;
                    }
                }
            }
        #endregion
    }
}
