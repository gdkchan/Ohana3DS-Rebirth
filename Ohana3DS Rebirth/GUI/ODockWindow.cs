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
        bool drag;
        int mouseX;
        int mouseY;

        public event EventHandler MoveEnded;

        public ODockWindow()
        {
            InitializeComponent();
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
    }
}
