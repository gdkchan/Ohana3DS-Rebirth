using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Ohana3DS_Rebirth.Properties;

namespace Ohana3DS_Rebirth.GUI
{
    public partial class OCheckBox : Control
    {
        private Color boxColor = Color.Black;
        private bool _checked;

        public event EventHandler CheckedChanged;

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
            int h = Resources.icn_ticked.Height;
            Rectangle checkRect = new Rectangle(0, (Height / 2) - (h / 2), Resources.icn_ticked.Width, h);
            e.Graphics.FillRectangle(new SolidBrush(boxColor), checkRect);
            if (_checked) e.Graphics.DrawImage(Resources.icn_ticked, checkRect);

            string text = DrawingHelper.clampText(Text, Font, Width - Resources.icn_ticked.Width);
            SizeF textSize = e.Graphics.MeasureString(text, Font);
            e.Graphics.DrawString(text, Font, new SolidBrush(Enabled ? ForeColor : Color.Silver), new Point(checkRect.Width, (Height / 2) - ((int)textSize.Height / 2)));

            base.OnPaint(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int h = Resources.icn_ticked.Height;
                Rectangle checkRect = new Rectangle(0, (Height / 2) - (h / 2), Resources.icn_ticked.Width, h);

                if (checkRect.Contains(e.Location))
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
