using System;
using System.Drawing;
using System.Windows.Forms;

using Ohana3DS_Rebirth.Properties;

namespace Ohana3DS_Rebirth.GUI
{
    public partial class OCheckBox : Control
    {
        private Color boxColor = Color.Black;
        private bool _checked;
        private bool autoSize;

        public event EventHandler CheckedChanged;

        public bool AutomaticSize
        {
            get
            {
                return autoSize;
            }
            set
            {
                autoSize = value;
                Refresh();
            }
        }

        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
                Refresh();
            }
        }

        /// <summary>
        ///     Color of the box where the ticked sign appears.
        /// </summary>
        public Color BoxColor
        {
            get
            {
                return boxColor;
            }
            set
            {
                boxColor = value;
                Refresh();
            }
        }

        /// <summary>
        ///     True if the Checkbox is checked, false otherwise.
        /// </summary>
        public bool Checked
        {
            get
            {
                return _checked;
            }
            set
            {
                _checked = value;
                Refresh();
            }
        }

        public OCheckBox()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            int w = Resources.icn_ticked.Width;
            int h = Resources.icn_ticked.Height;

            //Draw box around
            Color lineColor = ForeColor;
            if (!Enabled) lineColor = SystemColors.InactiveCaptionText;
            e.Graphics.DrawLine(new Pen(lineColor), new Point(0, Height - 1), new Point(w - 1, Height - 1));
            e.Graphics.DrawLine(new Pen(lineColor), new Point(0, Height - 1), new Point(0, Height - 2));
            e.Graphics.DrawLine(new Pen(lineColor), new Point(w - 1, Height - 1), new Point(w - 1, Height - 2));

            //Ticked icon (if checked)
            Rectangle checkRect = new Rectangle(0, (Height / 2) - (h / 2), w, h);
            if (_checked) e.Graphics.DrawImage(Resources.icn_ticked, checkRect);

            //Draw text at the right of the box
            string text = autoSize ? Text : DrawingUtils.clampText(e.Graphics, Text, Font, Width);
            SizeF textSize = DrawingUtils.measureText(e.Graphics, text, Font);
            if (autoSize) Size = new Size((int)textSize.Width + checkRect.Width, (int)textSize.Height);
            e.Graphics.DrawString(text, Font, new SolidBrush(Enabled ? ForeColor : Color.Silver), new Point(checkRect.Width, (Height / 2) - ((int)textSize.Height / 2)));

            base.OnPaint(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int h = Resources.icn_ticked.Height;

                if (ClientRectangle.Contains(e.Location))
                {
                    _checked = !_checked;
                    if (CheckedChanged != null) CheckedChanged(this, EventArgs.Empty);
                    Refresh();
                }
            }

            base.OnMouseDown(e);
        }
    }
}
