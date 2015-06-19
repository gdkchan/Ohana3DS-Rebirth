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
    public partial class OVScroll : Control
    {
        private int scrollY;
        private int scrollBarY;

        private int scrollBarSize = 32;
        private int scroll;
        private bool mouseDrag;
        private Color foreColor;

        private Color barColor = Color.White;
        private Color barColorHover = Color.WhiteSmoke;

        private int max = 100;

        public event EventHandler ScrollChanged;

        public OVScroll()
        {
            init();
            InitializeComponent();
        }

        public OVScroll(IContainer container)
        {
            container.Add(this);

            init();
            InitializeComponent();
        }

        private void init()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            foreColor = barColor;
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
        ///     Scroll bar color when mouse is outside of the bar.
        /// </summary>
        public Color BarColor
        {
            get
            {
                return barColor;
            }
            set
            {
                barColor = value;
            }
        }

        /// <summary>
        ///     Scroll bar color when mouse is hovering the bar.
        /// </summary>
        public Color BarColorHover
        {
            get
            {
                return barColorHover;
            }
            set
            {
                barColorHover = value;
            }
        }

        /// <summary>
        ///     The maximum Value the scroll can have.
        /// </summary>
        public int MaximumScroll
        {
            get
            {
                return max;
            }
            set
            {
                max = value;
                recalcSize();
                if (scrollY > value)
                {
                    scrollY = value;
                    scrollBarY = (int)(((float)scrollY / max) * (this.Height - scrollBarSize));
                    this.Refresh();
                }
            }
        }

        /// <summary>
        ///     The current value of the scroll (smaller than or equal to MaximumScroll).
        /// </summary>
        public int Value
        {
            get
            {
                return scrollY;
            }
            set
            {
                if (value > max) throw new Exception("OVscroll: The Value set is greater than the maximum value!");
                if (value < 0) throw new Exception("OVscroll: Value can't be less than 0!");
                scrollY = value;
                scrollBarY = (int)(((float)scrollY / max) * (this.Height - scrollBarSize));
                this.Refresh();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(foreColor), new Rectangle(0, scrollBarY, this.Width, scrollBarSize));
            base.OnPaint(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Rectangle scrollRect = new Rectangle(0, scrollBarY, this.Width, scrollBarSize);
                Rectangle mouseRect = new Rectangle(e.X, e.Y, 1, 1);
                if (scrollRect.IntersectsWith(mouseRect))
                {
                    scroll = e.Y - scrollBarY;
                    mouseDrag = true;
                }
            }

            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            this.Focus();
            if (e.Button == MouseButtons.Left) mouseDrag = false;
            base.OnMouseUp(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            Rectangle scrollRect = new Rectangle(0, scrollBarY, this.Width, scrollBarSize);
            Rectangle mouseRect = new Rectangle(e.X, e.Y, 1, 1);
            if (scrollRect.IntersectsWith(mouseRect))
            {
                if (foreColor != barColorHover)
                {
                    foreColor = barColorHover;
                    this.Refresh();
                }
            }
            else if (!mouseDrag)
            {
                if (foreColor != barColor)
                {
                    foreColor = barColor;
                    this.Refresh();
                }
            }

            if (e.Button == MouseButtons.Left)
            {
                if (mouseDrag)
                {
                    int y = e.Y - scroll;
                    if (y < 0) y = 0;
                    else if (y > this.Height - scrollBarSize) y = this.Height - scrollBarSize;
                    scrollBarY = y;

                    scrollY = (int)(((float)y / Math.Max((this.Height - scrollBarSize), 1)) * max);
                    if (this.ScrollChanged != null) this.ScrollChanged(this, EventArgs.Empty);
                    this.Refresh();
                }
            }

            base.OnMouseMove(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (!mouseDrag) foreColor = barColor;
            this.Refresh();
            base.OnMouseLeave(e);
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            recalcSize();

            base.OnLayout(levent);
        }

        private void recalcSize()
        {
            scrollBarSize = Math.Max(32, this.Height - max);
            scrollBarY = (int)(((float)scrollY / max) * (this.Height - scrollBarSize));
            this.Refresh();
        }
    }
}
