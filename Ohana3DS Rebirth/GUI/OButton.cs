using System;
using System.Drawing;
using System.Windows.Forms;

namespace Ohana3DS_Rebirth.GUI
{
    public partial class OButton : Control
    {
        private Color bgColor;
        private Bitmap img;
        private bool centered = true;
        private bool hover;

        public OButton()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            InitializeComponent();
        }

        /// <summary>
        ///     Optional image to show on the button.
        /// </summary>
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

        /// <summary>
        ///     Background color of the button, when not being hovered.
        /// </summary>
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

        /// <summary>
        ///     Center the text and image (if used) inside the button.
        /// </summary>
        public bool Centered
        {
            get
            {
                return centered;
            }
            set
            {
                centered = value;
                Refresh();
            }
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            Brush bg = new SolidBrush(hover ? ColorManager.ui_hoveredDark : ColorManager.ui_bgDark);
            pevent.Graphics.FillRectangle(bg, ClientRectangle);

            int bx = Width - 1;
            int by = Height - 1;
            pevent.Graphics.DrawLine(new Pen(Color.FromArgb(64, Color.White)), 0, 0, bx, 0);
            pevent.Graphics.DrawLine(new Pen(Color.FromArgb(64, Color.White)), 0, 1, 0, by);
            pevent.Graphics.DrawLine(new Pen(Color.FromArgb(64, Color.Black)), 0, by, bx, by);
            pevent.Graphics.DrawLine(new Pen(Color.FromArgb(64, Color.Black)), bx, 1, bx, by);

            string text = null;
            if (Text.Length > 0) text = DrawingUtils.clampText(pevent.Graphics, Text, Font, Width - (img != null ? img.Width : 0) - 2);
            SizeF textSize = DrawingUtils.measureText(pevent.Graphics, text, Font);
            int width = (int)textSize.Width;
            int yImage = 0;
            if (img != null)
            {
                width += img.Width;
                yImage = (Height / 2) - (img.Height / 2);
            }

            int x = centered ? (Width / 2) - (width / 2) : 0;
            int yText = (Height / 2) - (int)(textSize.Height / 2);
            Brush textBrush = new SolidBrush(Enabled ? ForeColor : Color.Silver);
            if (img != null)
            {
                pevent.Graphics.DrawImage(img, new Rectangle(x, yImage, img.Width, img.Height));
                pevent.Graphics.DrawString(text, Font, textBrush, new Point(x + img.Width, yText));
            }
            else
                pevent.Graphics.DrawString(text, Font, textBrush, new Point(x, yText));

            base.OnPaint(pevent);
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

        protected override void OnResize(EventArgs e)
        {
            Refresh();

            base.OnResize(e);
        }
    }
}
