using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

using Ohana3DS_Rebirth.Properties;

namespace Ohana3DS_Rebirth.GUI
{
    public partial class OSeekBar : Control
    {
        private int seekX;
        private int knobX;

        private int knobSize = 16;
        private int seek;
        private bool mouseDrag;

        private Color fillColor = ColorManager.highlight;

        private int max = 100;

        public event EventHandler Seek;
        public event EventHandler SeekStart;
        public event EventHandler SeekEnd;

        public OSeekBar()
        {
            init();
            InitializeComponent();
        }

        private void init()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
            }
        }

        /// <summary>
        ///     Color of elapsed portion of the Seek Bar.
        /// </summary>
        public Color FillColor
        {
            get
            {
                return fillColor;
            }
            set
            {
                fillColor = value;
            }
        }

        /// <summary>
        ///     The maximum Value the seek can have.
        /// </summary>
        public int MaximumSeek
        {
            get
            {
                return max;
            }
            set
            {
                if (value < 1) throw new Exception("OSeekBar: Maximum value MUST be greater than 0!");
                max = value;
                recalcSize();
                if (seekX > value)
                {
                    seekX = value;
                    knobX = (int)Math.Max(Width - knobSize, 0);
                    Refresh();
                }
            }
        }

        /// <summary>
        ///     The current value of the seek (smaller than or equal to MaximumSeek).
        /// </summary>
        public int Value
        {
            get
            {
                return seekX;
            }
            set
            {
                if (seekX == value) return;
                if (value > max) throw new Exception("OSeekBar: The Value set is greater than the maximum value!");
                if (value < 0) throw new Exception("OSeekBar: Value can't be less than 0!");
                seekX = value;
                knobX = (int)(((float)seekX / max) * Math.Max(Width - knobSize, 0));
                Refresh();
            }
        }

        /// <summary>
        ///     Returns true if user is manually seeking the bar.
        ///     Useful to stop updating it if user is changing position manually.
        /// </summary>
        public bool ManualSeeking
        {
            get
            {
                return mouseDrag;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(Color.Black), new Rectangle(8, (Height / 2) - 2, Width - 16, 4));
            e.Graphics.FillRectangle(new SolidBrush(fillColor), new Rectangle(9, (Height / 2) - 1, knobX, 2));
            e.Graphics.DrawImage(Resources.ui_knob, knobX, (Height / 2) - (knobSize / 2), knobSize, knobSize);
            base.OnPaint(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Rectangle knobRect = new Rectangle(knobX, (Height / 2) - (knobSize / 2), knobSize, knobSize);
                Rectangle mouseRect = new Rectangle(e.X, e.Y, 1, 1);
                if (knobRect.IntersectsWith(mouseRect))
                {
                    seek = e.X - knobX;
                    mouseDrag = true;
                    if (SeekStart != null) SeekStart(this, EventArgs.Empty);
                }
                else
                {
                    int x = e.X - (knobSize / 2);
                    if (x < 0) x = 0;
                    else if (x > Width - knobSize) x = Width - knobSize;
                    knobX = x;

                    seekX = (int)(((float)knobX / Math.Max(Width - knobSize, 1)) * max);
                    if (Seek != null) Seek(this, EventArgs.Empty);
                    Refresh();
                }
            }

            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            Focus();
            if (e.Button == MouseButtons.Left)
            {
                mouseDrag = false;
                if (SeekEnd != null) SeekEnd(this, EventArgs.Empty);
            }
            base.OnMouseUp(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            Rectangle knobRect = new Rectangle(knobX, (Height / 2) - (knobSize / 2), knobSize, knobSize);
            Rectangle mouseRect = new Rectangle(e.X, e.Y, 1, 1);

            if (e.Button == MouseButtons.Left)
            {
                if (mouseDrag)
                {
                    int x = e.X - seek;
                    if (x < 0) x = 0;
                    else if (x > Width - knobSize) x = Math.Max(Width - knobSize, 0);
                    knobX = x;

                    seekX = (int)(((float)x / Math.Max(Width - knobSize, 1)) * max);
                    if (Seek != null) Seek(this, EventArgs.Empty);
                    Refresh();
                }
            }

            base.OnMouseMove(e);
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            recalcSize();

            base.OnLayout(levent);
        }

        private void recalcSize()
        {
            knobX = (int)(((float)seekX / max) * (Width - knobSize));
            Refresh();
        }
    }
}
