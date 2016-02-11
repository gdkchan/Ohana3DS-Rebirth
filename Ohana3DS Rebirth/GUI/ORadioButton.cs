using System;
using System.Drawing;
using System.Windows.Forms;

namespace Ohana3DS_Rebirth.GUI
{
    public partial class ORadioButton : RadioButton
    {
        const int radioSize = 16;
        private bool hover;

        public ORadioButton()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            Brush bg;
            if (Checked)
                bg = new SolidBrush(ColorManager.ui_hoveredLight);
            else
                bg = new SolidBrush(hover ? ColorManager.ui_hoveredDark : ColorManager.ui_bgDark);

            pevent.Graphics.FillRectangle(bg, ClientRectangle);

            int bx = Width - 1;
            int by = Height - 1;
            pevent.Graphics.DrawLine(new Pen(Color.FromArgb(64, Color.White)), 0, 0, bx, 0);
            pevent.Graphics.DrawLine(new Pen(Color.FromArgb(64, Color.White)), 0, 1, 0, by);
            pevent.Graphics.DrawLine(new Pen(Color.FromArgb(64, Color.Black)), 0, by, bx, by);
            pevent.Graphics.DrawLine(new Pen(Color.FromArgb(64, Color.Black)), bx, 1, bx, by);

            string text = DrawingUtils.clampText(pevent.Graphics, Text, Font, Width);
            SizeF textSize = DrawingUtils.measureText(pevent.Graphics, text, Font);
            int width = (int)textSize.Width;
            int x = Math.Max(0, (Width / 2) - (width / 2));
            int yText = (Height / 2) - (int)(textSize.Height / 2);
            pevent.Graphics.DrawString(text, Font, new SolidBrush(Enabled ? ForeColor : Color.Silver), new Point(x, yText));
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            hover = true;
            Refresh();

            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            hover = false;
            Refresh();

            base.OnMouseLeave(e);
        }

        protected override void OnCheckedChanged(EventArgs e)
        {
            Refresh();

            base.OnCheckedChanged(e);
        }
    }
}
