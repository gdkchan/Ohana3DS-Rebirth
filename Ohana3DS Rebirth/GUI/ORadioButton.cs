using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ohana3DS_Rebirth.GUI
{
    public partial class ORadioButton : RadioButton
    {
        const int radioSize = 16;

        public ORadioButton()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            InitializeComponent();

            BackColor = Color.Black;
            ForeColor = Color.White;
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            pevent.Graphics.Clear(BackColor);
            if (Checked) pevent.Graphics.FillRectangle(new SolidBrush(ColorManager.highlight), new Rectangle(0, 0, Width, Height));

            SizeF textSize = pevent.Graphics.MeasureString(Text, Font);
            int width = (int)textSize.Width;
            int x = Math.Max(0, (Width / 2) - (width / 2));
            int yText = (Height / 2) - (int)(textSize.Height / 2);
            pevent.Graphics.DrawString(DrawingHelper.clampText(Text, Font, Width), Font, new SolidBrush(ForeColor), new Point(x, yText));
        }
    }
}
