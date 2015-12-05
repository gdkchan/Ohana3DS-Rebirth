//ODock Window made for Ohana3DS by gdkchan
//Inherit from this control to make any window you want to be dockable.

using System;
using System.Drawing;
using System.Windows.Forms;

using Ohana3DS_Rebirth.Properties;

namespace Ohana3DS_Rebirth.GUI
{
    public partial class ODockWindow : UserControl
    {
        const int gripSize = 4;

        const int minimumWidth = 128;
        const int minimumHeight = 64;

        private bool drag;
        private int mouseX;
        private int mouseY;

        private Point dragStart;
        private Point originalLocation;
        private Size originalSize;
        private bool isResize;
        private enum resizeDirection
        {
            topLeft,
            topRight,
            bottomLeft,
            bottomRight,
            top,
            bottom,
            left,
            right
        }
        resizeDirection resizeDir;

        private bool dockSwitch;

        private Bitmap hoverRed = new Bitmap(16, 16);
        private Bitmap hoverBlue = new Bitmap(16, 16);

        private Bitmap icon;
        public Bitmap Icon
        {
            get
            {
                return icon;
            }
            set
            {
                icon = value;
            }
        }

        private bool resizable = true;
        public bool Resizable
        {
            get
            {
                return resizable;
            }
            set
            {
                resizable = value;
                resize();
            }
        }

        public event EventHandler MoveEnded;
        public event EventHandler ToggleDockable;

        private string title;
        public ODock container;

        public ODockWindow()
        {
            using (Graphics g1 = Graphics.FromImage(hoverRed))
            {
                using (Graphics g2 = Graphics.FromImage(hoverBlue))
                {
                    g1.FillRectangle(new SolidBrush(Color.FromArgb(0x7f, ColorManager.hoverClose)), new Rectangle(1, 1, hoverRed.Width - 2, hoverRed.Height - 2));
                    g2.FillRectangle(new SolidBrush(ColorManager.highlight), new Rectangle(1, 1, hoverBlue.Width - 2, hoverBlue.Height - 2));
                }
            }

            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            InitializeComponent();
        }

        private void updateTitle()
        {
            using (Graphics g = Graphics.FromHwnd(Handle))
            {
                LblTitle.Text = DrawingUtils.clampText(g, title, LblTitle.Font, Width - 32);
            }
        }

        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
                updateTitle();
            }
        }

        public bool Drag
        {
            get
            {
                return drag;
            }
        }

        public virtual void flush()
        {
            //Dispose all unmanaged stuff here!
            hoverRed.Dispose();
            hoverBlue.Dispose();
            if (icon != null) icon.Dispose();
        }

        private void WindowTop_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                drag = true;
                mouseX = Cursor.Position.X - Left;
                mouseY = Cursor.Position.Y - Top;
            }
        }

        private void WindowTop_MouseMove(object sender, MouseEventArgs e)
        {
            if (drag)
            {
                int x = Math.Max(-(WindowTop.Width - 40), Math.Min(container.Width - 8, Cursor.Position.X - mouseX));
                int y = Math.Max(-(WindowTop.Height - 8), Math.Min(container.Height - 8, Cursor.Position.Y - mouseY));
                Location = new Point(x, y);
                BringToFront();
            }
        }

        private void WindowTop_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                drag = false;
                MoveEnded(this, EventArgs.Empty);
            }
        }

        #region "Resize"
        private void ODockWindow_Layout(object sender, LayoutEventArgs e)
        {
            resize();
            updateTitle();
        }

        private void resize()
        {
            if (resizable)
            {
                ContentContainer.Location = new Point(gripSize, gripSize);
                ContentContainer.Size = new Size(Width - gripSize * 2, Height - gripSize * 2);
            }
            else
            {
                ContentContainer.Location = Point.Empty;
                ContentContainer.Size = Size;
            }
        }

        private void ODockWindow_MouseMove(object sender, MouseEventArgs e)
        {
            if (isResize)
            {
                int diffX = Cursor.Position.X - dragStart.X;
                int diffY = Cursor.Position.Y - dragStart.Y;

                container.SuspendDrawing();

                switch (resizeDir)
                {
                    case resizeDirection.topLeft: // \ Top Left
                        Cursor.Current = Cursors.SizeNWSE;
                        Width = Math.Max(originalSize.Width - diffX, minimumWidth);
                        Height = Math.Max(originalSize.Height - diffY, minimumHeight);
                        Left = (originalLocation.X + originalSize.Width) - Width;
                        Top = (originalLocation.Y + originalSize.Height) - Height;
                        break;
                    case resizeDirection.topRight: // / Top Right
                        Cursor.Current = Cursors.SizeNESW;
                        Width = Math.Max(originalSize.Width + diffX, minimumWidth);
                        Height = Math.Max(originalSize.Height - diffY, minimumHeight);
                        Top = (originalLocation.Y + originalSize.Height) - Height;
                        break;
                    case resizeDirection.bottomLeft: // / Bottom Left
                        Cursor.Current = Cursors.SizeNESW;
                        Width = Math.Max(originalSize.Width - diffX, minimumWidth);
                        Height = Math.Max(originalSize.Height + diffY, minimumHeight);
                        Left = (originalLocation.X + originalSize.Width) - Width;
                        break;
                    case resizeDirection.bottomRight: // \ Bottom Right
                        Cursor.Current = Cursors.SizeNWSE;
                        Width = Math.Max(originalSize.Width + diffX, minimumWidth);
                        Height = Math.Max(originalSize.Height + diffY, minimumHeight);
                        break;
                    case resizeDirection.left: // — Left
                        Cursor.Current = Cursors.SizeWE;
                        Width = Math.Max(originalSize.Width - diffX, minimumWidth);
                        Left = (originalLocation.X + originalSize.Width) - Width;
                        break;
                    case resizeDirection.right: // — Right
                        Cursor.Current = Cursors.SizeWE;
                        Width = Math.Max(originalSize.Width + diffX, minimumWidth);
                        break;
                    case resizeDirection.top: // | Top
                        Cursor.Current = Cursors.SizeNS;
                        Height = Math.Max(originalSize.Height - diffY, minimumHeight);
                        Top = (originalLocation.Y + originalSize.Height) - Height;
                        break;
                    case resizeDirection.bottom: // | Bottom
                        Cursor.Current = Cursors.SizeNS;
                        Height = Math.Max(originalSize.Height + diffY, minimumHeight);
                        break;
                }

                container.ResumeDrawing();
            }
            else
            {
                if (e.X < gripSize && e.Y < gripSize)
                {
                    Cursor.Current = Cursors.SizeNWSE;
                    resizeDir = resizeDirection.topLeft;
                }
                else if (e.X >= ClientSize.Width - gripSize && e.Y < gripSize)
                {
                    Cursor.Current = Cursors.SizeNESW;
                    resizeDir = resizeDirection.topRight;
                }
                else if (e.X < gripSize && e.Y >= ClientSize.Height - gripSize)
                {
                    Cursor.Current = Cursors.SizeNESW;
                    resizeDir = resizeDirection.bottomLeft;
                }
                else if (e.X >= ClientSize.Width - gripSize && e.Y >= ClientSize.Height - gripSize)
                {
                    Cursor.Current = Cursors.SizeNWSE;
                    resizeDir = resizeDirection.bottomRight;
                }
                else if (e.X < gripSize)
                {
                    Cursor.Current = Cursors.SizeWE;
                    resizeDir = resizeDirection.left;
                }
                else if (e.X >= ClientSize.Width - gripSize)
                {
                    Cursor.Current = Cursors.SizeWE;
                    resizeDir = resizeDirection.right;
                }
                else if (e.Y < gripSize)
                {
                    Cursor.Current = Cursors.SizeNS;
                    resizeDir = resizeDirection.top;
                }
                else if (e.Y >= ClientSize.Height - gripSize)
                {
                    Cursor.Current = Cursors.SizeNS;
                    resizeDir = resizeDirection.bottom;
                }
            }
        }

        private void ODockWindow_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                dragStart = Cursor.Position;
                originalLocation = Location;
                originalSize = Size;
                isResize = true;
            }
        }

        private void ODockWindow_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) isResize = false;
        }
        #endregion

        #region "Control Box"
        private void BtnClose_MouseEnter(object sender, EventArgs e)
            {
                BtnClose.BackgroundImage = hoverRed;
            }

            private void BtnClose_MouseLeave(object sender, EventArgs e)
            {
                BtnClose.BackgroundImage = null;
            }

            private void BtnClose_MouseDown(object sender, MouseEventArgs e)
            {
                if (e.Button == MouseButtons.Left) Visible = false;
            }

            private void BtnPin_MouseEnter(object sender, EventArgs e)
            {
                BtnPin.BackgroundImage = hoverBlue;
            }

            private void BtnPin_MouseLeave(object sender, EventArgs e)
            {
                BtnPin.BackgroundImage = null;
            }

            private void BtnPin_MouseDown(object sender, MouseEventArgs e)
            {
                if (e.Button == MouseButtons.Left)
                {
                    dockSwitch = !dockSwitch;
                    ToggleDockable(this, EventArgs.Empty);
                    BtnPin.Image = dockSwitch 
                        ? Resources.icn_locked 
                        : Resources.icn_dockable;
                    BtnPin.BackgroundImage = hoverBlue;
                }
            }
        #endregion

    }
}
