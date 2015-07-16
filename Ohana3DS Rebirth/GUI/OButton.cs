using System;
using System.Drawing;
using System.Windows.Forms;

namespace Ohana3DS_Rebirth.GUI
{
    public partial class OButton : Control
    {
        private Color bgColor;
        private Bitmap img;

        public OButton()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            InitializeComponent();
        }

        public Bitmap Image
        {
            get
            {
                return img;
            }
            set
            {
                img = value;
                Refresh();
            }
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
                bgColor = value;
                Refresh();
            }
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            pevent.Graphics.FillRectangle(new SolidBrush(bgColor), new Rectangle(0, 0, Width, Height));

            string text = DrawingHelper.clampText(Text, Font, Width - (img != null ? img.Width : 0));
            SizeF textSize = pevent.Graphics.MeasureString(text, Font);
            int width = (int)textSize.Width;
            int yImage = 0;
            if (img != null)
            {
                width += img.Width;
                yImage = (Height / 2) - (img.Height / 2);
            }
            int x = Math.Max(0, (Width / 2) - (width / 2));
            int yText = (Height / 2) - (int)(textSize.Height / 2);
            if (img != null)
            {
                pevent.Graphics.DrawImage(img, new Rectangle(x, yImage, img.Width, img.Height));
                pevent.Graphics.DrawString(text, Font, new SolidBrush(Enabled ? ForeColor : Color.Silver), new Point(x + img.Width, yText));
            }
            else
            {
                pevent.Graphics.DrawString(text, Font, new SolidBrush(Enabled ? ForeColor : Color.Silver), new Point(x, yText));
            }

            base.OnPaint(pevent);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            bgColor = ColorManager.highlight;
            Refresh();

            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            bgColor = this.BackColor;
            Refresh();

            base.OnMouseLeave(e);
        }
    }
}
