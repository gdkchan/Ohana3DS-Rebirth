using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

namespace Ohana3DS_Rebirth.GUI
{
    public partial class OHScroll : Control
    {
        private int scrollX;
        private int scrollBarX;

        private int scrollBarSize = 32;
        private int scroll;
        private bool mouseDrag;
        private Color foreColor;

        private Color barColor = Color.White;
        private Color barColorHover = Color.WhiteSmoke;

        private int max = 100;

        public event EventHandler ScrollChanged;

        public OHScroll()
        {
            init();
            InitializeComponent();
        }

        public OHScroll(IContainer container)
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
                if (scrollX > value)
                {
                    scrollX = value;
                    scrollBarX = (int)(((float)scrollX / max) * (Width - scrollBarSize));
                    Refresh();
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
                return scrollX;
            }
            set
            {
                if (value > max) throw new Exception("OHscroll: The Value set is greater than the maximum value!");
                if (value < 0) throw new Exception("OHscroll: Value can't be less than 0!");
                scrollX = value;
                scrollBarX = (int)(((float)scrollX / max) * (Width - scrollBarSize));
                Refresh();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(foreColor), new Rectangle(scrollBarX, 0, scrollBarSize, Height));
            base.OnPaint(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Rectangle scrollRect = new Rectangle(scrollBarX, 0, scrollBarSize, Height);
                Rectangle mouseRect = new Rectangle(e.X, e.Y, 1, 1);
                if (scrollRect.IntersectsWith(mouseRect))
                {
                    scroll = e.X - scrollBarX;
                    mouseDrag = true;
                }
            }

            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            Focus();
            if (e.Button == MouseButtons.Left) mouseDrag = false;
            base.OnMouseUp(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            Rectangle scrollRect = new Rectangle(scrollBarX, 0, scrollBarSize, Height);
            Rectangle mouseRect = new Rectangle(e.X, e.Y, 1, 1);
            if (scrollRect.IntersectsWith(mouseRect))
            {
                if (foreColor != barColorHover)
                {
                    foreColor = barColorHover;
                    Refresh();
                }
            }
            else if (!mouseDrag)
            {
                if (foreColor != barColor)
                {
                    foreColor = barColor;
                    Refresh();
                }
            }

            if (e.Button == MouseButtons.Left)
            {
                if (mouseDrag)
                {
                    int x = e.X - scroll;
                    if (x < 0) x = 0;
                    else if (x > Width - scrollBarSize) x = Width - scrollBarSize;
                    scrollBarX = x;

                    scrollX = (int)(((float)x / Math.Max((Width - scrollBarSize), 1)) * max);
                    if (ScrollChanged != null) ScrollChanged(this, EventArgs.Empty);
                    Refresh();
                }
            }

            base.OnMouseMove(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (!mouseDrag) foreColor = barColor;
            Refresh();
            base.OnMouseLeave(e);
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            recalcSize();

            base.OnLayout(levent);
        }

        private void recalcSize()
        {
            scrollBarSize = Math.Max(32, Width - max);
            scrollBarX = (int)(((float)scrollX / max) * (Width - scrollBarSize));
            Refresh();
        }
    }
}
