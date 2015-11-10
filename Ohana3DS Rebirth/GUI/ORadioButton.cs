using System;
using System.Drawing;
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

            string text = DrawingUtils.clampText(pevent.Graphics, Text, Font, Width);
            SizeF textSize = DrawingUtils.measureText(pevent.Graphics, text, Font);
            int width = (int)textSize.Width;
            int x = Math.Max(0, (Width / 2) - (width / 2));
            int yText = (Height / 2) - (int)(textSize.Height / 2);
            pevent.Graphics.DrawString(text, Font, new SolidBrush(Enabled ? ForeColor : Color.Silver), new Point(x, yText));
        }
    }
}
