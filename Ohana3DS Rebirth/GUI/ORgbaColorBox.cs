using System;
using System.Drawing;
using System.Windows.Forms;

namespace Ohana3DS_Rebirth.GUI
{
    public partial class ORgbaColorBox : UserControl
    {
        public event EventHandler ColorChanged;

        public ORgbaColorBox()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            InitializeComponent();
        }

        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
                SeekA.BackColor = value;
                SeekR.BackColor = value;
                SeekG.BackColor = value;
                SeekB.BackColor = value;
            }
        }

        public Color Color
        {
            get
            {
                return currentColor();
            }
            set
            {
                SeekA.Value = value.A;
                SeekR.Value = value.R;
                SeekG.Value = value.G;
                SeekB.Value = value.B;
                updateColor(false);
            }
        }

        private void SeekR_ValueChanged(object sender, EventArgs e)
        {
            updateColor();
        }

        private void SeekG_ValueChanged(object sender, EventArgs e)
        {
            updateColor();
        }

        private void SeekB_ValueChanged(object sender, EventArgs e)
        {
            updateColor();
        }

        private void SeekA_ValueChanged(object sender, EventArgs e)
        {
            updateColor();
        }

        private void updateColor(bool colorChanged = true)
        {
            SelectedColor.BackColor = currentColor();
            if (ColorChanged != null && colorChanged) ColorChanged(this, EventArgs.Empty);
        }

        private Color currentColor()
        {
            int a = clamp((int)SeekA.Value);
            int r = clamp((int)SeekR.Value);
            int g = clamp((int)SeekG.Value);
            int b = clamp((int)SeekB.Value);
            return Color.FromArgb(a, r, g, b);
        }

        private int clamp(int value)
        {
            return Math.Max(Math.Min(value, 255), 0);
        }
    }
}
