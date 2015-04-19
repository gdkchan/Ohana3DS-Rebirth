using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Ohana3DS_Rebirth.GUI
{
    public partial class OStatusStrip : StatusStrip
    {
        public OStatusStrip()
        {
            InitializeComponent();
        }

        public OStatusStrip(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            switch (m.Msg)
            {
                case 0x84:
                    m.Result = new IntPtr(-1);
                    break;
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            e.Graphics.Clear(this.BackColor);
            if (this.SizingGrip == true)
            {
                e.Graphics.DrawImage(Ohana3DS_Rebirth.Properties.Resources.grip, new Rectangle(this.Width - 24, this.Height - 24, 24, 24), new Rectangle(0, 0, 24, 24), GraphicsUnit.Pixel);
            }
        }
    }
}
