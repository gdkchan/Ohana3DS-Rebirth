using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Ohana3DS_Rebirth.GUI
{
    public partial class OWindowManager : Control
    {
        const int groupWidth = 192;

        private class windowGroup
        {
            public string title;
            public ContextMenuStrip menu;
            public List<ODockWindow> window;
            public List<int> indexList;

            public windowGroup()
            {
                menu = new ContextMenuStrip();
                menu.Renderer = new OMenuStrip();
                window = new List<ODockWindow>();
                indexList = new List<int>();
            }
        }
        private List<windowGroup> group = new List<windowGroup>();
        private windowGroup currentGroup = new windowGroup();

        private ODock parentDock;

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
        }

        public void Initialize(ODock dockWindow)
        {
            parentDock = dockWindow;
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
        ///     Creates a new group with all the Windows on the temporary Dock Window buffer.
        ///     The buffer is cleared after.
        /// </summary>
        /// <param name="groupName">The name of the group</param>
        public void createGroup(string groupName)
        {
            currentGroup.title = groupName;
            group.Add(currentGroup);
            currentGroup = new windowGroup(); 
            this.Refresh();
        }

        /// <summary>
        ///     Add a Window to the temporary Dock Window buffer.
        /// </summary>
        /// <param name="window">The Window to be added</param>
        public void addWindow(ODockWindow window)
        {
            window.VisibleChanged += new EventHandler(Window_VisibleChanged);

            currentGroup.indexList.Add((int)window.Tag);
            currentGroup.window.Add(window);

            ToolStripMenuItem item = new ToolStripMenuItem();
            item.Text = window.Title;
            item.Click += new EventHandler(triggerVisibility);
            item.Tag = window;
            item.Checked = true;
            currentGroup.menu.Items.Add(item);
        }

        private void triggerVisibility(Object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            ODockWindow window = (ODockWindow)item.Tag;

            window.Visible = !window.Visible;
            item.Checked = window.Visible;

            this.Refresh();
        }

        private bool hasVisibleWindows(int groupIndex)
        {
            for (int i = 0; i < group[groupIndex].window.Count; i++)
            {
                if (group[groupIndex].window[i].Visible) return true;
            }
            return false;
        }

        private void setWindowsVisibility(int groupIndex, bool visible)
        {
            for (int i = 0; i < group[groupIndex].window.Count; i++)
            {
                group[groupIndex].window[i].Visible = visible;
                ToolStripMenuItem item = (ToolStripMenuItem)group[groupIndex].menu.Items[i];
                item.Checked = visible;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            int left = 0;
            for (int i = 0; i < group.Count; i++)
            {
                Rectangle rect = new Rectangle(left, 4, groupWidth, this.Height - 4);
                Rectangle rect2 = new Rectangle(rect.X + 1, rect.Y, rect.Width - 2, rect.Height - 1);

                Color baseColor;
                if (hasVisibleWindows(i))
                {
                    baseColor = Color.FromArgb(15, 82, 186);
                    DrawingHelper.drawButtonFace(e.Graphics, rect, Color.FromArgb(0x7f, baseColor), Color.Black, Color.FromArgb(0x5f, baseColor), Color.FromArgb(0x7f, Color.Black));
                }
                else
                {
                    baseColor = Color.DarkGray;
                    DrawingHelper.drawButtonFace(e.Graphics, rect, Color.FromArgb(0xdf, Color.Black), Color.FromArgb(0x1f, baseColor), Color.FromArgb(0x5f, Color.Black), Color.FromArgb(0x3f, baseColor));
                }
                
                e.Graphics.DrawString(group[i].title, new Font("Segoe UI", 10), new SolidBrush(Color.White), new Point(rect.Left + 16, rect.Top));

                Rectangle upArrowRect = new Rectangle(rect.X, rect.Y + (rect.Height / 2) - 8, 16, 16);
                Rectangle closeRect = new Rectangle((rect.X + rect.Width) - 16, rect.Y + (rect.Height / 2) - 8, 16, 16);
                Rectangle mouseRect = new Rectangle(this.PointToClient(Cursor.Position), new Size(1, 1));
                Brush brush = new SolidBrush(Color.FromArgb(0x5f, baseColor));
                if (mouseRect.IntersectsWith(upArrowRect)) e.Graphics.FillRectangle(brush, new Rectangle(upArrowRect.X + 1, upArrowRect.Y, upArrowRect.Width - 2, upArrowRect.Height));
                if (mouseRect.IntersectsWith(closeRect)) e.Graphics.FillRectangle(brush, new Rectangle(closeRect.X + 1, closeRect.Y, closeRect.Width - 2, closeRect.Height));

                e.Graphics.DrawImage(Ohana3DS_Rebirth.Properties.Resources.icn_wm_up_arrow, upArrowRect.Location);
                e.Graphics.DrawImage(Ohana3DS_Rebirth.Properties.Resources.icn_wm_close, closeRect.Location);
                
                left += groupWidth + 4;
            }

 	        base.OnPaint(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
            {
                int left = 0;
                for (int i = 0; i < group.Count; i++)
                {
                    Rectangle rect = new Rectangle(left, 4, groupWidth, this.Height - 8);
                    Rectangle mouseRect = new Rectangle(this.PointToClient(Cursor.Position), new Size(1, 1));

                    if (e.Button == MouseButtons.Left)
                    {
                        Rectangle upArrowRect = new Rectangle(rect.X, rect.Y + (rect.Height / 2) - 8, 16, 16);
                        Rectangle closeRect = new Rectangle((rect.X + rect.Width) - 16, rect.Y + (rect.Height / 2) - 8, 16, 16);
                        
                        if (mouseRect.IntersectsWith(upArrowRect))
                        {
                            Point position = this.PointToScreen(new Point(upArrowRect.X, upArrowRect.Y));
                            group[i].menu.Show(position, ToolStripDropDownDirection.AboveRight);

                            return;
                        }

                        if (mouseRect.IntersectsWith(closeRect))
                        {
                            for (int j = 0; j < group[i].indexList.Count; j++)
                            {
                                parentDock.remove(group[i].indexList[j]);
                            }
                            group.RemoveAt(i);
                            this.Refresh();

                            return;
                        }
                    }

                    if (mouseRect.IntersectsWith(rect) && e.Button == MouseButtons.Right)
                    {
                        if (hasVisibleWindows(i)) setWindowsVisibility(i, false); else setWindowsVisibility(i, true);
                        this.Refresh();
                    }

                    left += groupWidth + 4;
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

        private void Window_VisibleChanged(Object sender, EventArgs e)
        {
            this.Refresh();
            for (int i = 0; i < group.Count; i++)
            {
                for (int j = 0; j < group[i].window.Count; j++)
                {
                    ToolStripMenuItem item = (ToolStripMenuItem)group[i].menu.Items[j];
                    item.Checked = group[i].window[j].Visible;
                }
            }
        }
    }
}
