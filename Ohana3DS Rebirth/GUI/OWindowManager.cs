//OWindowManager made for Ohana3DS by gdkchan
//This should help to manage (show/hide) the Windows of the ODock control

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

using Ohana3DS_Rebirth.Properties;

namespace Ohana3DS_Rebirth.GUI
{
    public partial class OWindowManager : Control
    {
        const int windowWidth = 192;

        public List<ODockWindow> windowList = new List<ODockWindow>();

        private ODock parentDock;

        private bool hasLeftScroll, hasRightScroll;
        private int scrollX, maxScrollX;
        private int scrollGoal;
        private Timer smoothScroll = new Timer();

        public OWindowManager()
        {
            init();
            InitializeComponent();
        }

        public OWindowManager(IContainer container)
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
            SetStyle(ControlStyles.DoubleBuffer, true);
            smoothScroll.Interval = 10;
            smoothScroll.Tick += new EventHandler(smoothScroll_Tick);
        }

        public void initialize(ODock dockWindow)
        {
            parentDock = dockWindow;
        }

        /// <summary>
        ///     Closes all windows and release unmanaged resources.
        /// </summary>
        public void flush()
        {
            parentDock.flush();
            foreach (ODockWindow window in windowList)
            {
                window.dispose();
            }
            windowList.Clear();
            this.Refresh();
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
        ///     Adds a Window to the bar.
        /// </summary>
        /// <param name="window">The Window to be added</param>
        public void addWindow(ODockWindow window)
        {
            window.VisibleChanged += new EventHandler(Window_VisibleChanged);

            windowList.Add(window);
        }

        private void triggerVisibility(Object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            ODockWindow window = (ODockWindow)item.Tag;

            window.Visible = !window.Visible;
            item.Checked = window.Visible;

            this.Refresh();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            int left = scrollX * -1;
            foreach (ODockWindow window in windowList)
            {
                Rectangle rect = new Rectangle(left, 4, windowWidth, this.Height - 8);
                Rectangle rectShadow = new Rectangle(rect.X, rect.Y + rect.Height, rect.Width, this.Height - (rect.Y + rect.Height));

                if (window.Visible)
                {
                    Color baseColor = Color.FromArgb(21, 46, 84);
                    e.Graphics.FillRectangle(new SolidBrush(baseColor), rect);
                    e.Graphics.FillRectangle(new LinearGradientBrush(rectShadow, baseColor, Color.Transparent, LinearGradientMode.Vertical), rectShadow);
                }
                else
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(0x3f, Color.Black)), new Rectangle(rect.X, rect.Y, rect.Width, rect.Height + 4));
                }

                Font font = new Font("Segoe UI", 10);
                e.Graphics.DrawString(window.Title, font, new SolidBrush(Color.White), new Point(rect.Left + 16, rect.Top));
                font.Dispose();

                Rectangle iconRect = new Rectangle(rect.X, rect.Y + (rect.Height / 2) - 8, 16, 16);
                
                left += windowWidth + 4;
            }

            maxScrollX = (scrollX + (left - 4)) - this.Width;
            Rectangle leftScrollRect = new Rectangle(0, ((this.Height / 2) - 8) + 2, 16, 16);
            Rectangle rightScrollRect = new Rectangle(this.Width - 16, ((this.Height / 2) - 8) + 2, 16, 16);

            if (scrollX > 0) { e.Graphics.DrawImage(Resources.icn_wm_scroll_left, leftScrollRect); hasLeftScroll = true; } else { hasLeftScroll = false; }
            if ((left - 4) > this.Width) { e.Graphics.DrawImage(Resources.icn_wm_scroll_right, rightScrollRect); hasRightScroll = true; } else { hasRightScroll = false; }

 	        base.OnPaint(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Rectangle mouseRect = new Rectangle(this.PointToClient(Cursor.Position), new Size(1, 1));
                Rectangle leftScrollRect = new Rectangle(0, ((this.Height / 2) - 8) + 2, 16, 16);
                Rectangle rightScrollRect = new Rectangle(this.Width - 16, ((this.Height / 2) - 8) + 2, 16, 16);

                if (hasLeftScroll && mouseRect.IntersectsWith(leftScrollRect))
                {
                    scrollGoal -= windowWidth / 8;
                    smoothScroll.Enabled = true;
                    return;
                }

                if (hasRightScroll && mouseRect.IntersectsWith(rightScrollRect))
                {
                    scrollGoal += windowWidth / 8;
                    smoothScroll.Enabled = true;
                    return;
                }

                int left = scrollX * -1;
                for (int i = 0; i < windowList.Count; i++)
                {
                    Rectangle rect = new Rectangle(left, 4, windowWidth, this.Height - 8);
                    
                    if (mouseRect.IntersectsWith(rect))
                    {
                        windowList[i].Visible = !windowList[i].Visible;
                        this.Refresh();
                    }

                    left += windowWidth + 4;
                }
            }

            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            this.Refresh();
     
            base.OnMouseMove(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            this.Refresh();

            base.OnMouseLeave(e);
        }

        protected override void OnResize(EventArgs e)
        {
            smoothScroll.Enabled = false;
            scrollX = 0;
            scrollGoal = 0;

            this.Refresh();

            base.OnResize(e);
        }

        private void Window_VisibleChanged(Object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void smoothScroll_Tick(Object sender, EventArgs e)
        {
            if (scrollGoal != 0)
            {
                if (scrollGoal < 0)
                {
                    scrollGoal++;
                    scrollX = Math.Max(scrollX - 8, 0); 
                }
                else
                {
                    scrollGoal--;
                    scrollX = Math.Min(scrollX + 8, maxScrollX);
                }
                this.Refresh();
            }
            else
            {
                smoothScroll.Enabled = false;
            }
        }
    }
}
