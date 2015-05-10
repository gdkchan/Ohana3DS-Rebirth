//Ohana3DS Dock Control made by gdkchan
//
//TODO:
//- Some functions can be a bit optimized (+ reduce redundant code)

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
    public partial class ODock : Control
    {
        const int minimumWidth = 128;
        const int minimumHeight = 64;

        const int defaultSideSize = 192;

        int rightDockWidth = defaultSideSize;
        int leftDockWidth = defaultSideSize;
        int bottomDockHeight = defaultSideSize;
        int topDockHeight = defaultSideSize;

        float rightDockProportionW;
        float leftDockProportionW;
        float bottomDockProportionH;
        float topDockProportionH;

        const int gripSize = 4;

        public enum dockMode
        {
            Floating,
            Left,
            Right,
            Top,
            Bottom,
            Center
        }
        public enum resizeMode
        {
            windowOnly,
            entireSide
        }
        private class windowInfoStruct
        {
            public ODockWindow window;
            public Size originalSize;
            public Point originalLocation;
            public SizeF windowProportions;
            public int index;
            public dockMode dock;
            public bool dockable;
            public bool hasGrip;
            public bool denyDock;
        }
        List<windowInfoStruct> windowInfo = new List<windowInfoStruct>();

        private bool drag;
        private int dragIndex;
        private Point dragMousePosition;
        private resizeMode dragMode;
        private dockMode dragSide;

        public ODock()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.Resize += new EventHandler(Control_Resize);
            this.MouseMove += new MouseEventHandler(Control_MouseMove);
            this.MouseDown += new MouseEventHandler(Control_MouseDown);
            this.MouseUp += new MouseEventHandler(Control_MouseUp);
            InitializeComponent();
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
        ///     Add a Window on the Dock Container.
        ///     Please note that the Tag property of the Window will contains the indentification Index, so do not change!
        /// </summary>
        /// <param name="window">The Window (ODockWindow control)</param>
        /// <param name="x">Initial X position of the Window</param>
        /// <param name="y">Initial Y position of the Window</param>
        public void launch(ODockWindow window, int x = 0, int y = 0)
        {
            this.Controls.Add(window);
            window.Move += new EventHandler(Window_Move);
            window.MoveEnded += new EventHandler(Window_MoveEnded);
            window.VisibleChanged += new EventHandler(Window_VisibleChanged);
            window.ToggleDockable += new EventHandler(Window_ToggleDockable);

            int windowIndex = getAvailableIndex();
            if ((windowIndex & 0x80000000) != 0) throw new Exception("You added too many docks!");
            windowInfoStruct info = new windowInfoStruct();
            info.index = windowIndex;
            info.dock = dockMode.Floating;
            info.window = window;
            info.originalSize = window.Size;
            info.originalLocation = new Point(x, y);
            info.dockable = true;
            windowInfo.Add(info);
            window.Tag = windowIndex;
            window.Location = new Point(x, y);
            window.BringToFront();
        }

        private int getAvailableIndex()
        {
            int index = -1;
            bool found = true;
            while (found)
            {
                index++;
                found = false;
                for (int i = 0; i < windowInfo.Count; i++)
                {
                    if (windowInfo[i].index == index) found = true;
                }
            }

            return index;
        }

        private void Control_Resize(Object sender, EventArgs e)
        {
            if (!hasDockedWindow()) return;
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);

            rightDockWidth = (int)(rect.Width * rightDockProportionW);
            leftDockWidth = (int)(rect.Width * leftDockProportionW);
            bottomDockHeight = (int)(rect.Height * bottomDockProportionH);
            topDockHeight = (int)(rect.Height * topDockProportionH);

            resize(rect, dockMode.Right);
            resize(rect, dockMode.Left);
            resize(rect, dockMode.Bottom);
            resize(rect, dockMode.Top);
            resize(rect, dockMode.Center);
        }

        private void resize(Rectangle rect, dockMode dock)
        {
            Rectangle dockRect = getDockRect(dock);

            int top = dockRect.Top;
            int left = dockRect.Left;

            List<int> indexList = new List<int>();

            switch (dock)
            {
                case dockMode.Right:
                case dockMode.Left:
                    int indexRL = getTopmostWindow(dock);
                    while (indexRL > -1)
                    {
                        indexList.Add(indexRL);
                        indexRL = getBelowWindow(getRectangle(windowInfo[indexRL].window), dock, indexRL);
                    }

                    for (int i = 0; i < indexList.Count; i++)
                    {
                        int index = indexList[i];
                        windowInfo[index].window.Top = top;
                        windowInfo[index].window.Left = dockRect.Left;
                        windowInfo[index].window.Width = dockRect.Width;
                        if (i != indexList.Count - 1)
                        {
                            windowInfo[index].window.Height = (int)(rect.Height * windowInfo[index].windowProportions.Height) - gripSize;
                            windowInfo[index].hasGrip = true;
                        }
                        else
                        {
                            windowInfo[index].window.Height = ((dockRect.Y + dockRect.Height) - windowInfo[index].window.Top);
                            windowInfo[index].hasGrip = false;
                        }
                        top += windowInfo[index].window.Height + gripSize;
                    }

                    break;

                case dockMode.Bottom:
                case dockMode.Top:
                    int indexBT = getLeftmostWindow(dock);
                    while (indexBT > -1)
                    {
                        indexList.Add(indexBT);
                        indexBT = getRightWindow(getRectangle(windowInfo[indexBT].window), dock, indexBT);
                    }

                    for (int i = 0; i < indexList.Count; i++)
                    {
                        int index = indexList[i];
                        windowInfo[index].window.Top = dockRect.Top;
                        windowInfo[index].window.Left = left;
                        windowInfo[index].window.Height = dockRect.Height;
                        if (i != indexList.Count - 1)
                        {
                            windowInfo[index].window.Width = (int)(rect.Width * windowInfo[index].windowProportions.Width) - gripSize;
                            windowInfo[index].hasGrip = true;
                        }
                        else
                        {
                            windowInfo[index].window.Width = ((dockRect.X + dockRect.Width) - windowInfo[index].window.Left);
                            windowInfo[index].hasGrip = false;
                        }
                        left += windowInfo[index].window.Width + gripSize;
                    }

                    break;

                case dockMode.Center:
                    int centerIndex = getCenterWindow();
                    if (centerIndex > -1)
                    {
                        windowInfo[centerIndex].window.Location = dockRect.Location;
                        windowInfo[centerIndex].window.Size = dockRect.Size;
                    }

                    break;
            }
        }

        private void Control_MouseMove(Object sender, MouseEventArgs e)
        {
            if (drag)
            {
                Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);

                if (dragMode == resizeMode.entireSide)
                {
                    switch (dragSide)
                    {
                        case dockMode.Right:
                            bool canResizeRight = false;
                            int newWidthRight = (rect.X + rect.Width) - this.PointToClient(Cursor.Position).X;
                            if (newWidthRight < minimumWidth) newWidthRight = minimumWidth;
                            if (newWidthRight > this.Width - gripSize) newWidthRight = this.Width - gripSize;
                            int differenceRight = newWidthRight - rightDockWidth;
                            if (differenceRight > 0) //Aumentou
                            {
                                while (newWidthRight > rightDockWidth)
                                {
                                    canResizeRight = getSpaceToCreateDockSide(dockMode.Right, differenceRight - gripSize);
                                    if (canResizeRight) break;
                                    newWidthRight--;
                                    differenceRight = newWidthRight - rightDockWidth;
                                }
                            }
                            else if (differenceRight < 0) //Diminuiu
                            {
                                canResizeRight = true;
                                int diff = differenceRight * -1;

                                if (windowCount(dockMode.Bottom) > 0)
                                {
                                    int bottomWindowIndex = getRightmostWindow(dockMode.Bottom);
                                    windowInfo[bottomWindowIndex].window.Width += diff;
                                }

                                if (windowCount(dockMode.Center) > 0)
                                {
                                    int centerWindowIndex = getCenterWindow();
                                    windowInfo[centerWindowIndex].window.Width += diff;
                                }

                                if (windowCount(dockMode.Top) > 0)
                                {
                                    int topWindowIndex = getRightmostWindow(dockMode.Top);
                                    windowInfo[topWindowIndex].window.Width += diff;
                                }
                            }

                            if (canResizeRight)
                            {
                                rightDockWidth = newWidthRight;

                                for (int i = 0; i < windowInfo.Count; i++)
                                {
                                    if (windowInfo[i].dock == dragSide)
                                    {
                                        windowInfo[i].window.SuspendLayout();
                                        windowInfo[i].window.Width = rightDockWidth;
                                        windowInfo[i].window.Left = (rect.X + rect.Width) - rightDockWidth;
                                    }
                                }

                                for (int i = 0; i < windowInfo.Count; i++)
                                {
                                    if (windowInfo[i].dock == dragSide)
                                    {
                                        windowInfo[i].window.ResumeLayout();
                                    }
                                }
                            }

                            break;

                        case dockMode.Left:
                            bool canResizeLeft = false;
                            int newWidthLeft = this.PointToClient(Cursor.Position).X - rect.X;
                            if (newWidthLeft < minimumWidth) newWidthLeft = minimumWidth;
                            if (newWidthLeft > this.Width - gripSize) newWidthLeft = this.Width - gripSize;
                            int differenceLeft = newWidthLeft - leftDockWidth;
                            if (differenceLeft > 0) //Aumentou
                            {
                                while (newWidthLeft > leftDockWidth)
                                {
                                    canResizeLeft = getSpaceToCreateDockSide(dockMode.Left, differenceLeft - gripSize);
                                    if (canResizeLeft) break;
                                    newWidthLeft--;
                                    differenceLeft = newWidthLeft - leftDockWidth;
                                }
                            }
                            else if (differenceLeft < 0) //Diminuiu
                            {
                                canResizeLeft = true;
                                int diff = differenceLeft * -1;

                                if (windowCount(dockMode.Bottom) > 0)
                                {
                                    int bottomWindowIndex = getLeftmostWindow(dockMode.Bottom);
                                    windowInfo[bottomWindowIndex].window.Left -= diff;
                                    windowInfo[bottomWindowIndex].window.Width += diff;
                                }

                                if (windowCount(dockMode.Center) > 0)
                                {
                                    int centerWindowIndex = getCenterWindow();
                                    windowInfo[centerWindowIndex].window.Left -= diff;
                                    windowInfo[centerWindowIndex].window.Width += diff;
                                }

                                if (windowCount(dockMode.Top) > 0)
                                {
                                    int topWindowIndex = getLeftmostWindow(dockMode.Top);
                                    windowInfo[topWindowIndex].window.Left -= diff;
                                    windowInfo[topWindowIndex].window.Width += diff;
                                }
                            }

                            if (canResizeLeft)
                            {
                                leftDockWidth = newWidthLeft;

                                for (int i = 0; i < windowInfo.Count; i++)
                                {
                                    if (windowInfo[i].dock == dragSide)
                                    {
                                        windowInfo[i].window.SuspendLayout();
                                        windowInfo[i].window.Width = leftDockWidth;
                                    }
                                }

                                for (int i = 0; i < windowInfo.Count; i++)
                                {
                                    if (windowInfo[i].dock == dragSide)
                                    {
                                        windowInfo[i].window.ResumeLayout();
                                    }
                                }
                            }
                            
                            break;

                        case dockMode.Bottom:
                            bool canResizeBottom = false;
                            int newBottomHeight = (rect.Y + rect.Height) - this.PointToClient(Cursor.Position).Y;
                            if (newBottomHeight < minimumHeight) newBottomHeight = minimumHeight;
                            if (newBottomHeight > this.Height - gripSize) newBottomHeight = this.Height - gripSize;
                            int differenceBottom = newBottomHeight - bottomDockHeight;
                            if (differenceBottom > 0) //Aumentou
                            {
                                while (newBottomHeight > bottomDockHeight)
                                {
                                    canResizeBottom = getSpaceToCreateDockSide(dockMode.Bottom, differenceBottom - gripSize);
                                    if (canResizeBottom) break;
                                    newBottomHeight--;
                                    differenceBottom = newBottomHeight - bottomDockHeight;
                                }
                            }
                            else if (differenceBottom < 0) //Diminuiu
                            {
                                canResizeBottom = true;
                                int diff = differenceBottom * -1;

                                if (windowCount(dockMode.Center) > 0)
                                {
                                    int centerWindowIndex = getCenterWindow();
                                    windowInfo[centerWindowIndex].window.Height += diff;
                                }
                            }

                            if (canResizeBottom)
                            {
                                bottomDockHeight = newBottomHeight;

                                for (int i = 0; i < windowInfo.Count; i++)
                                {
                                    if (windowInfo[i].dock == dragSide)
                                    {
                                        windowInfo[i].window.SuspendLayout();
                                        windowInfo[i].window.Height = bottomDockHeight;
                                        windowInfo[i].window.Top = (rect.Y + rect.Height) - bottomDockHeight;
                                    }
                                }

                                for (int i = 0; i < windowInfo.Count; i++)
                                {
                                    if (windowInfo[i].dock == dragSide)
                                    {
                                        windowInfo[i].window.ResumeLayout();
                                    }
                                }
                            }

                            break;

                        case dockMode.Top:
                            bool canResizeTop = false;
                            int newTopHeight = this.PointToClient(Cursor.Position).Y - rect.Y;
                            if (newTopHeight < minimumHeight) newTopHeight = minimumHeight;
                            if (newTopHeight > this.Height - gripSize) newTopHeight = this.Height - gripSize;
                            int differenceTop = newTopHeight - topDockHeight;
                            if (differenceTop > 0) //Aumentou
                            {
                                while (newTopHeight > topDockHeight)
                                {
                                    canResizeTop = getSpaceToCreateDockSide(dockMode.Top, differenceTop - gripSize);
                                    if (canResizeTop) break;
                                    newTopHeight--;
                                    differenceTop = newTopHeight - topDockHeight;
                                }
                            }
                            else if (differenceTop < 0) //Diminuiu
                            {
                                canResizeTop = true;
                                int diff = differenceTop * -1;

                                if (windowCount(dockMode.Center) > 0)
                                {
                                    int centerWindowIndex = getCenterWindow();
                                    windowInfo[centerWindowIndex].window.Top -= diff;
                                    windowInfo[centerWindowIndex].window.Height += diff;
                                }
                            }

                            if (canResizeTop)
                            {
                                topDockHeight = newTopHeight;

                                for (int i = 0; i < windowInfo.Count; i++)
                                {
                                    if (windowInfo[i].dock == dragSide)
                                    {
                                        windowInfo[i].window.SuspendLayout();
                                        windowInfo[i].window.Height = topDockHeight;
                                    }
                                }

                                for (int i = 0; i < windowInfo.Count; i++)
                                {
                                    if (windowInfo[i].dock == dragSide)
                                    {
                                        windowInfo[i].window.ResumeLayout();
                                    }
                                }
                            }

                            break;
                    }
                }
                else if (dragMode == resizeMode.windowOnly)
                {
                    rect = getDockRect(windowInfo[dragIndex].dock);

                    switch (windowInfo[dragIndex].dock)
                    {
                        case dockMode.Right:
                        case dockMode.Left:
                            int dragDistanceY = Cursor.Position.Y - dragMousePosition.Y;

                            int belowWindow = getBelowWindow(getRectangle(windowInfo[dragIndex].window), windowInfo[dragIndex].dock, dragIndex);
                            if (belowWindow == -1) return;
                            int belowWindowHeight, belowWindowHeightMinimum;
                            int belowBelowWindow = getBelowWindow(getRectangle(windowInfo[belowWindow].window), windowInfo[dragIndex].dock, belowWindow);
                            if (windowInfo[belowWindow].hasGrip)
                            {
                                if (belowBelowWindow == -1) return;
                                belowWindowHeight = (windowInfo[belowBelowWindow].window.Top - (windowInfo[belowWindow].window.Top + dragDistanceY)) - gripSize;
                                belowWindowHeightMinimum = windowInfo[belowWindow].window.Height = (windowInfo[belowBelowWindow].window.Top - windowInfo[belowWindow].window.Top) - gripSize;
                            }
                            else
                            {
                                belowWindowHeight = (rect.Top + rect.Height) - (windowInfo[belowWindow].window.Top + dragDistanceY);
                                belowWindowHeightMinimum = windowInfo[belowWindow].window.Height = (rect.Top + rect.Height) - windowInfo[belowWindow].window.Top;
                            }

                            if ((windowInfo[dragIndex].window.Height + dragDistanceY >= minimumHeight) && (belowWindowHeight >= minimumHeight || (windowInfo[belowWindow].window.Height < minimumHeight && belowWindowHeight >= windowInfo[belowWindow].window.Height)))
                            {
                                windowInfo[dragIndex].window.Height += dragDistanceY;
                                windowInfo[belowWindow].window.Top += dragDistanceY;
                                windowInfo[belowWindow].window.Height = belowWindowHeight;
                            }
                            else if (windowInfo[dragIndex].window.Height + dragDistanceY < minimumHeight && belowWindowHeightMinimum > minimumHeight)
                            {
                                windowInfo[dragIndex].window.Height = minimumHeight;
                                windowInfo[belowWindow].window.Top = (windowInfo[dragIndex].window.Top + windowInfo[dragIndex].window.Height) + gripSize;
                                windowInfo[belowWindow].window.Height = belowWindowHeightMinimum;
                            }
                            else if (belowWindowHeight < minimumHeight)
                            {
                                int top;
                                
                                if (windowInfo[belowWindow].hasGrip)
                                {
                                    top = (windowInfo[belowBelowWindow].window.Top - windowInfo[belowWindow].window.Height) - gripSize;
                                }
                                else
                                {
                                    top = (rect.Top + rect.Height) - windowInfo[belowWindow].window.Height;
                                }

                                int height = (windowInfo[belowWindow].window.Top - windowInfo[dragIndex].window.Top) - gripSize;
                                if (height < minimumHeight) return;
                                windowInfo[belowWindow].window.Top = top;
                                windowInfo[belowWindow].window.Height = minimumHeight;
                                windowInfo[dragIndex].window.Height = height;
                            }

                            break;

                        case dockMode.Bottom:
                        case dockMode.Top:
                            int dragDistanceX = Cursor.Position.X - dragMousePosition.X;

                            int rightWindow = getRightWindow(getRectangle(windowInfo[dragIndex].window), windowInfo[dragIndex].dock, dragIndex);
                            if (rightWindow == -1) return;
                            int rightWindowWidth, rightWindowWidthMinimum;
                            int rightRightWindow = getRightWindow(getRectangle(windowInfo[rightWindow].window), windowInfo[dragIndex].dock, rightWindow);
                            if (windowInfo[rightWindow].hasGrip)
                            {
                                if (rightRightWindow == -1) return;
                                rightWindowWidth = (windowInfo[rightRightWindow].window.Left - (windowInfo[rightWindow].window.Left + dragDistanceX)) - gripSize;
                                rightWindowWidthMinimum = (windowInfo[rightRightWindow].window.Left - windowInfo[rightWindow].window.Left) - gripSize;
                            }
                            else
                            {
                                rightWindowWidth = (rect.Left + rect.Width) - (windowInfo[rightWindow].window.Left + dragDistanceX);
                                rightWindowWidthMinimum = (rect.Left + rect.Width) - windowInfo[rightWindow].window.Left;
                            }

                            if (windowInfo[dragIndex].window.Width + dragDistanceX >= minimumWidth && (rightWindowWidth >= minimumWidth || (windowInfo[rightWindow].window.Width < minimumWidth && rightWindowWidth >= windowInfo[rightWindow].window.Width)))
                            {
                                windowInfo[dragIndex].window.Width += dragDistanceX;
                                windowInfo[rightWindow].window.Left += dragDistanceX;
                                windowInfo[rightWindow].window.Width = rightWindowWidth;
                                
                            }
                            else if (windowInfo[dragIndex].window.Width + dragDistanceX < minimumWidth && rightWindowWidthMinimum > minimumWidth)
                            {
                                windowInfo[dragIndex].window.Width = minimumWidth;
                                windowInfo[rightWindow].window.Left = (windowInfo[dragIndex].window.Left + windowInfo[dragIndex].window.Width) + gripSize;
                                windowInfo[rightWindow].window.Width = rightWindowWidthMinimum;
                            }
                            else if (rightWindowWidth < minimumWidth)
                            {
                                int left;
                                
                                if (windowInfo[rightWindow].hasGrip)
                                {
                                    left = (windowInfo[rightRightWindow].window.Left - windowInfo[rightWindow].window.Width) - gripSize;
                                }
                                else
                                {
                                    left = (rect.Left + rect.Width) - windowInfo[rightWindow].window.Width;
                                }

                                int width = (windowInfo[rightWindow].window.Left - windowInfo[dragIndex].window.Left) - gripSize;
                                if (width < minimumWidth) return;
                                windowInfo[rightWindow].window.Left = left;
                                windowInfo[rightWindow].window.Width = minimumWidth;
                                windowInfo[dragIndex].window.Width = width;
                            }

                            break;
                    }
                }

                dragMousePosition = Cursor.Position;
                calculateProportions();
            }
            else
            {
                Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
                Rectangle mouseRect = new Rectangle(this.PointToClient(Cursor.Position), new Size(1, 1));

                for (int i = 0; i < windowInfo.Count; i++)
                {
                    if (windowInfo[i].hasGrip)
                    {
                        switch (windowInfo[i].dock)
                        {
                            case dockMode.Right:
                            case dockMode.Left:
                                Rectangle gripRectRL = new Rectangle(windowInfo[i].window.Left, windowInfo[i].window.Top + windowInfo[i].window.Height, windowInfo[i].window.Width, gripSize);
                                if (mouseRect.IntersectsWith(gripRectRL)) Cursor.Current = Cursors.HSplit;
                                break;

                            case dockMode.Bottom:
                            case dockMode.Top:
                                Rectangle gripRectBT = new Rectangle(windowInfo[i].window.Left + windowInfo[i].window.Width, windowInfo[i].window.Top, gripSize, windowInfo[i].window.Height);
                                if (mouseRect.IntersectsWith(gripRectBT)) Cursor.Current = Cursors.VSplit;
                                break;
                        }
                    }
                }

                Rectangle rightDockDrag = new Rectangle(((rect.X + rect.Width) - rightDockWidth) - gripSize, rect.Y, gripSize, rect.Height);
                Rectangle leftDockDrag = new Rectangle(rect.X +  leftDockWidth, rect.Y, gripSize, rect.Height);
                Rectangle bottomDockDrag = new Rectangle(rect.X, ((rect.Y + rect.Height) - bottomDockHeight) - gripSize, rect.Width, gripSize);
                Rectangle topDockDrag = new Rectangle(rect.X, rect.Y + topDockHeight, rect.Width, gripSize);

                if ((windowCount(dockMode.Right) > 0) && mouseRect.IntersectsWith(rightDockDrag)) Cursor.Current = Cursors.VSplit;
                if ((windowCount(dockMode.Left) > 0) && mouseRect.IntersectsWith(leftDockDrag)) Cursor.Current = Cursors.VSplit;
                if ((windowCount(dockMode.Bottom) > 0) && mouseRect.IntersectsWith(bottomDockDrag)) Cursor.Current = Cursors.HSplit;
                if ((windowCount(dockMode.Top) > 0) && mouseRect.IntersectsWith(topDockDrag)) Cursor.Current = Cursors.HSplit;
            }
        }

        private void Control_MouseDown(Object sender, MouseEventArgs e)
        {
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            Rectangle mouseRect = new Rectangle(this.PointToClient(Cursor.Position), new Size(1, 1));

            for (int i = 0; i < windowInfo.Count; i++)
            {
                if (windowInfo[i].hasGrip)
                {
                    switch (windowInfo[i].dock)
                    {
                        case dockMode.Right:
                        case dockMode.Left:
                            Rectangle gripRectRL = new Rectangle(windowInfo[i].window.Left, windowInfo[i].window.Top + windowInfo[i].window.Height, windowInfo[i].window.Width, gripSize);

                            if (mouseRect.IntersectsWith(gripRectRL))
                            {
                                Cursor.Current = Cursors.HSplit;
                                drag = true;
                                dragIndex = i;
                                dragMousePosition = Cursor.Position;
                                dragMode = resizeMode.windowOnly;
                                return;
                            }
                            break;

                        case dockMode.Bottom:
                        case dockMode.Top:
                            Rectangle gripRectBT = new Rectangle(windowInfo[i].window.Left + windowInfo[i].window.Width, windowInfo[i].window.Top, gripSize, windowInfo[i].window.Height);

                            if (mouseRect.IntersectsWith(gripRectBT))
                            {
                                Cursor.Current = Cursors.VSplit;
                                drag = true;
                                dragIndex = i;
                                dragMousePosition = Cursor.Position;
                                dragMode = resizeMode.windowOnly;
                                return;
                            }
                            break;
                    }
                }
            }

            Rectangle rightDockDrag = new Rectangle(((rect.X + rect.Width) - rightDockWidth) - gripSize, rect.Y, gripSize, rect.Height);
            Rectangle leftDockDrag = new Rectangle(rect.X + leftDockWidth, rect.Y, gripSize, rect.Height);
            Rectangle bottomDockDrag = new Rectangle(rect.X, ((rect.Y + rect.Height) - bottomDockHeight) - gripSize, rect.Width, gripSize);
            Rectangle topDockDrag = new Rectangle(rect.X, rect.Y + topDockHeight, rect.Width, gripSize);

            if ((windowCount(dockMode.Right) > 0) && mouseRect.IntersectsWith(rightDockDrag))
            {
                Cursor.Current = Cursors.VSplit;
                drag = true;
                dragMousePosition = Cursor.Position;
                dragMode = resizeMode.entireSide;
                dragSide = dockMode.Right;
            }
            else if ((windowCount(dockMode.Left) > 0) && mouseRect.IntersectsWith(leftDockDrag))
            {
                Cursor.Current = Cursors.VSplit;
                drag = true;
                dragMousePosition = Cursor.Position;
                dragMode = resizeMode.entireSide;
                dragSide = dockMode.Left;
            }
            else if ((windowCount(dockMode.Bottom) > 0) && mouseRect.IntersectsWith(bottomDockDrag))
            {
                Cursor.Current = Cursors.HSplit;
                drag = true;
                dragMousePosition = Cursor.Position;
                dragMode = resizeMode.entireSide;
                dragSide = dockMode.Bottom;
            }
            else if ((windowCount(dockMode.Top) > 0) && mouseRect.IntersectsWith(topDockDrag))
            {
                Cursor.Current = Cursors.HSplit;
                drag = true;
                dragMousePosition = Cursor.Position;
                dragMode = resizeMode.entireSide;
                dragSide = dockMode.Top;
            }
        }

        private void Control_MouseUp(Object sender, MouseEventArgs e)
        {
            if (drag) drag = false;
        }

        //
        //
        //

        private void Window_Move(Object sender, EventArgs e)
        {
            if (drag) return;

            ODockWindow window = (ODockWindow)sender;
            int infoIndex = getWindowInfoIndex((int)window.Tag);

            if (!windowInfo[infoIndex].window.Drag) return;

            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            Rectangle rightDock = getDockRect(dockMode.Right);
            Rectangle leftDock = getDockRect(dockMode.Left);
            Rectangle bottomDock = getDockRect(dockMode.Bottom);
            Rectangle topDock = getDockRect(dockMode.Top);
            Rectangle centerDock = getDockRect(dockMode.Center);

            Rectangle dockRect = new Rectangle(window.Location, window.Size);

            Rectangle windowDockRect = new Rectangle();
            switch (windowInfo[infoIndex].dock)
            {
                case dockMode.Right: windowDockRect = rightDock; break;
                case dockMode.Left: windowDockRect = leftDock; break;
                case dockMode.Bottom: windowDockRect = bottomDock; break;
                case dockMode.Top: windowDockRect = topDock; break;
                case dockMode.Center: windowDockRect = centerDock; break;
            }

            if (!dockRect.IntersectsWith(windowDockRect) || windowInfo[infoIndex].denyDock)
            {
                dockMode oldDock = windowInfo[infoIndex].dock;
                windowInfo[infoIndex].dock = dockMode.Floating;
                windowInfo[infoIndex].hasGrip = false;
                windowInfo[infoIndex].denyDock = false;
                windowInfo[infoIndex].window.Size = windowInfo[infoIndex].originalSize;
                windowInfo[infoIndex].window.BringToFront();
                autoArrange(oldDock, windowDockRect);

                //Caso um Dock Side tenha sido removido...
                if (oldDock == dockMode.Right && windowCount(dockMode.Right) == 0)
                {
                    int width = rightDockWidth + gripSize;
                    if (windowCount(dockMode.Bottom) > 0)
                    {
                        int index = getRightmostWindow(dockMode.Bottom);
                        windowInfo[index].window.Width += width;
                    }

                    if (windowCount(dockMode.Top) > 0)
                    {
                        int index = getRightmostWindow(dockMode.Top);
                        windowInfo[index].window.Width += width;
                    }
                }
                else if (oldDock == dockMode.Left && windowCount(dockMode.Left) == 0)
                {
                    int width = leftDockWidth + gripSize;
                    if (windowCount(dockMode.Bottom) > 0)
                    {
                        int index = getLeftmostWindow(dockMode.Bottom);
                        windowInfo[index].window.Left -= width;
                        windowInfo[index].window.Width += width;
                    }

                    if (windowCount(dockMode.Top) > 0)
                    {
                        int index = getLeftmostWindow(dockMode.Top);
                        windowInfo[index].window.Left -= width;
                        windowInfo[index].window.Width += width;
                    }
                }

                if (windowCount(dockMode.Center) > 0)
                {
                    int centerWindowIndex = 0;
                    centerWindowIndex = getCenterWindow();
                    windowInfo[centerWindowIndex].window.Location = centerDock.Location;
                    windowInfo[centerWindowIndex].window.Size = centerDock.Size;
                }

                calculateProportions();
                calculateMinimumSize();
            }
        }

        private void Window_MoveEnded(Object sender, EventArgs e)
        {
            Control window = (Control)sender;
            int infoIndex = getWindowInfoIndex((int)window.Tag);

            if (!windowInfo[infoIndex].dockable)
            {
                windowInfo[infoIndex].denyDock = true;
                dockMode oldDock = windowInfo[infoIndex].dock;
                windowInfo[infoIndex].dock = dockMode.Floating;
                windowInfo[infoIndex].window.Size = windowInfo[infoIndex].originalSize;
                windowInfo[infoIndex].window.BringToFront();

                autoArrange(oldDock, getDockRect(oldDock));
                arrangeCenter();

                return;
            }

            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);

            if (windowCount(dockMode.Right) == 0) rightDockWidth = defaultSideSize; //Reseta valores caso não tenha nada no Dock Side ainda
            if (windowCount(dockMode.Left) == 0) leftDockWidth = defaultSideSize;
            if (windowCount(dockMode.Bottom) == 0) bottomDockHeight = defaultSideSize;
            if (windowCount(dockMode.Top) == 0) topDockHeight = defaultSideSize;
            
            Rectangle rightDock = getDockRect(dockMode.Right);
            Rectangle leftDock = getDockRect(dockMode.Left);
            Rectangle bottomDock = getDockRect(dockMode.Bottom);
            Rectangle topDock = getDockRect(dockMode.Top);
            Rectangle centerDock = getDockRect(dockMode.Center);

            //Código que verifica se um Dock deve ocorrer, e posiciona caso isso ocorra
            Rectangle dockRect = new Rectangle(window.Location, window.Size);

            if (windowCount(dockMode.Center) == 0 && dockRect.IntersectsWith(centerDock))
            {
                if (centerDock.Width >= minimumWidth && centerDock.Height >= minimumHeight)
                {
                    window.Location = centerDock.Location;
                    window.Size = centerDock.Size;
                    window.SendToBack();

                    windowInfo[infoIndex].hasGrip = false;
                    windowInfo[infoIndex].dock = dockMode.Center;
                }
                else
                {
                    windowInfo[infoIndex].denyDock = true;
                }
            }
            else if (dockRect.IntersectsWith(rightDock) || dockRect.IntersectsWith(leftDock)) //Lados esquerdo e direito
            {
                dockMode currentDock;
                Rectangle currentRect;

                bool deny = false;

                if (dockRect.IntersectsWith(rightDock))
                {
                    currentDock = dockMode.Right;
                    currentRect = rightDock;
                }
                else
                {
                    currentDock = dockMode.Left;
                    currentRect = leftDock;
                }

                if (windowCount(currentDock) == 0)
                {
                    if (!getSpaceToCreateDockSide(currentDock, currentDock == dockMode.Right ? rightDockWidth : leftDockWidth))
                    {
                        windowInfo[infoIndex].denyDock = true;
                        deny = true;
                        window.BringToFront();
                    }
                }

                if (!deny)
                {
                    window.SuspendLayout();

                    windowInfo[infoIndex].dock = dockMode.Floating;
                    windowInfo[infoIndex].window.Size = windowInfo[infoIndex].originalSize;
                    autoArrange(currentDock, currentRect);

                    int aboveWindow = getAboveWindow(dockRect, currentDock, infoIndex);
                    int belowWindow = getBelowWindow(dockRect, currentDock, infoIndex);

                    if (aboveWindow > -1 && windowInfo[aboveWindow].window.Height / 2 > minimumHeight)
                    {
                        if (windowInfo[aboveWindow].hasGrip)
                        {
                            windowInfo[aboveWindow].window.Height = ((windowInfo[aboveWindow].window.Height + gripSize) / 2) - gripSize;
                        }
                        else
                        {
                            windowInfo[aboveWindow].window.Height = (windowInfo[aboveWindow].window.Height / 2) - gripSize;
                            windowInfo[aboveWindow].hasGrip = true;
                        }
                        int top = (windowInfo[aboveWindow].window.Top - rect.Top) + (windowInfo[aboveWindow].window.Height + gripSize);

                        window.Location = new Point(currentRect.X, rect.Top + top);
                        int height = currentRect.Height - top;
                        if (belowWindow > -1)
                        {
                            height = ((windowInfo[belowWindow].window.Top - rect.Top) - top) - gripSize;
                            windowInfo[infoIndex].hasGrip = true;
                        }
                        else
                        {
                            windowInfo[infoIndex].hasGrip = false;
                        }

                        window.Size = new Size(currentRect.Width, height);
                        window.SendToBack();

                        windowInfo[infoIndex].dock = currentDock;
                    }
                    else if (belowWindow > -1 && windowInfo[belowWindow].window.Height / 2 > minimumHeight)
                    {
                        int height;
                        if (windowInfo[belowWindow].hasGrip)
                        {
                            int belowBelowWindow = getBelowWindow(getRectangle(windowInfo[belowWindow].window), currentDock, getWindowInfoIndex((int)windowInfo[belowWindow].window.Tag));
                            height = (windowInfo[belowWindow].window.Height + gripSize) / 2;
                            windowInfo[belowWindow].window.Height = (windowInfo[belowBelowWindow].window.Top - (windowInfo[belowWindow].window.Top + height)) - gripSize;
                        }
                        else
                        {
                            height = windowInfo[belowWindow].window.Height / 2;
                            windowInfo[belowWindow].window.Height = (rect.Top + rect.Height) - (windowInfo[belowWindow].window.Top + height);
                        }

                        int top = windowInfo[belowWindow].window.Top;
                        windowInfo[belowWindow].window.Top += height;

                        window.Location = new Point(currentRect.X, top);
                        window.Size = new Size(currentRect.Width, height - gripSize);
                        window.SendToBack();

                        windowInfo[infoIndex].hasGrip = true;
                        windowInfo[infoIndex].dock = currentDock;
                    }
                    else if (aboveWindow == -1 && belowWindow == -1)
                    {
                        window.Location = currentRect.Location;
                        window.Size = currentRect.Size;
                        window.SendToBack();

                        windowInfo[infoIndex].hasGrip = false;
                        windowInfo[infoIndex].dock = currentDock;
                    }
                    else
                    {
                        windowInfo[infoIndex].denyDock = true;
                        window.BringToFront();
                    }

                    window.ResumeLayout();
                }
            }
            else if (dockRect.IntersectsWith(bottomDock) || dockRect.IntersectsWith(topDock))
            {
                dockMode currentDock;
                Rectangle currentRect;

                bool deny = false;

                if (dockRect.IntersectsWith(bottomDock))
                {
                    currentDock = dockMode.Bottom;
                    currentRect = bottomDock;
                }
                else
                {
                    currentDock = dockMode.Top;
                    currentRect = topDock;
                }

                if (windowCount(currentDock) == 0)
                {
                    if (!getSpaceToCreateDockSide(currentDock, currentDock == dockMode.Bottom ? bottomDockHeight : topDockHeight))
                    {
                        windowInfo[infoIndex].denyDock = true;
                        deny = true;
                        window.BringToFront();
                    }
                }

                if (!deny)
                {
                    window.SuspendLayout();

                    windowInfo[infoIndex].dock = dockMode.Floating;
                    windowInfo[infoIndex].window.Size = windowInfo[infoIndex].originalSize;
                    autoArrange(currentDock, currentRect);

                    int leftWindow = getLeftWindow(dockRect, currentDock, infoIndex);
                    int rightWindow = getRightWindow(dockRect, currentDock, infoIndex);

                    if (leftWindow > -1 && windowInfo[leftWindow].window.Width / 2 > minimumWidth)
                    {
                        if (windowInfo[leftWindow].hasGrip)
                        {
                            windowInfo[leftWindow].window.Width = ((windowInfo[leftWindow].window.Width + gripSize) / 2) - gripSize;
                        }
                        else
                        {
                            windowInfo[leftWindow].window.Width = (windowInfo[leftWindow].window.Width / 2) - gripSize;
                            windowInfo[leftWindow].hasGrip = true;
                        }
                        int left = (windowInfo[leftWindow].window.Left - rect.Left) + (windowInfo[leftWindow].window.Width + gripSize);

                        window.Location = new Point(rect.Left + left, currentRect.Y);
                        int width = currentRect.Width - left;
                        if (rightWindow > -1)
                        {
                            width = ((windowInfo[rightWindow].window.Left - rect.Left) - left) - gripSize;
                            windowInfo[infoIndex].hasGrip = true;
                        }
                        else
                        {
                            windowInfo[infoIndex].hasGrip = false;
                        }

                        window.Size = new Size(width, currentRect.Height);
                        window.SendToBack();

                        windowInfo[infoIndex].dock = currentDock;
                    }
                    else if (rightWindow > -1 && windowInfo[rightWindow].window.Width / 2 > minimumWidth)
                    {
                        int width;
                        if (windowInfo[rightWindow].hasGrip)
                        {
                            int rightRightWindow = getRightWindow(getRectangle(windowInfo[rightWindow].window), currentDock, getWindowInfoIndex((int)windowInfo[rightWindow].window.Tag));
                            width = (windowInfo[rightWindow].window.Width + gripSize) / 2;
                            windowInfo[rightWindow].window.Width = (windowInfo[rightRightWindow].window.Left - (windowInfo[rightWindow].window.Left + width)) - gripSize;
                        }
                        else
                        {
                            width = windowInfo[rightWindow].window.Width / 2;
                            windowInfo[rightWindow].window.Width = (rect.Left + rect.Width) - (windowInfo[rightWindow].window.Left + width);
                        }

                        int left = windowInfo[rightWindow].window.Left;
                        windowInfo[rightWindow].window.Left += width;

                        window.Location = new Point(left, currentRect.Y);
                        window.Size = new Size(width - gripSize, currentRect.Height);
                        window.SendToBack();

                        windowInfo[infoIndex].hasGrip = true;
                        windowInfo[infoIndex].dock = currentDock;
                    }
                    else if (leftWindow == -1 && rightWindow == -1)
                    {
                        window.Location = currentRect.Location;
                        window.Size = currentRect.Size;
                        window.SendToBack();

                        windowInfo[infoIndex].hasGrip = false;
                        windowInfo[infoIndex].dock = currentDock;
                    }
                    else
                    {
                        windowInfo[infoIndex].denyDock = true;
                        window.BringToFront();
                    }

                    window.ResumeLayout();
                }
            }
            else
            {
                windowInfo[infoIndex].denyDock = true;
            }

            autoArrangeAll();

            calculateProportions(true);
            calculateMinimumSize();
        }

        private void autoArrangeAll()
        {
            autoArrange(dockMode.Right, getDockRect(dockMode.Right));
            autoArrange(dockMode.Left, getDockRect(dockMode.Left));
            autoArrange(dockMode.Bottom, getDockRect(dockMode.Bottom));
            autoArrange(dockMode.Top, getDockRect(dockMode.Top));
            arrangeCenter();
        }

        private void Window_VisibleChanged(Object sender, EventArgs e)
        {
            Control window = (Control)sender;
            int infoIndex = getWindowInfoIndex((int)window.Tag);

            if (windowInfo[infoIndex].dock != dockMode.Floating)
            {
                dockMode oldDock = windowInfo[infoIndex].dock;
                windowInfo[infoIndex].dock = dockMode.Floating;
                windowInfo[infoIndex].window.Size = windowInfo[infoIndex].originalSize;
                windowInfo[infoIndex].window.Location = windowInfo[infoIndex].originalLocation;
                windowInfo[infoIndex].window.BringToFront();

                autoArrangeAll();

                calculateProportions();
                calculateMinimumSize();
            }
        }

        private void Window_ToggleDockable(Object sender, EventArgs e)
        {
            Control window = (Control)sender;
            int infoIndex = getWindowInfoIndex((int)window.Tag);
            windowInfo[infoIndex].dockable = !windowInfo[infoIndex].dockable;
        }

        private void arrangeCenter()
        {
            Rectangle centerDock = getDockRect(dockMode.Center);

            if (windowCount(dockMode.Center) > 0)
            {
                int centerWindowIndex = getCenterWindow();
                centerDock = getDockRect(dockMode.Center);
                windowInfo[centerWindowIndex].window.Location = centerDock.Location;
                windowInfo[centerWindowIndex].window.Size = centerDock.Size;
            }
        }

        public void remove(int tagIndex)
        {
            int infoIndex = getWindowInfoIndex(tagIndex);

            dockMode dock = windowInfo[infoIndex].dock;
            if (dock != dockMode.Floating) autoArrange(dock, getDockRect(dock));
            windowInfo[infoIndex].window.Dispose();
            windowInfo.RemoveAt(infoIndex);
        }

        /// <summary>
        ///     Get the Window placed exactly above the Rectangle passed to the function.
        ///     Returns -1 if it can't find any Window above.
        /// </summary>
        /// <param name="rect">The rectangle at where your window is located</param>
        /// <param name="dock">The docking mode that will be used</param>
        /// <param name="ignoreIndex">Index of the Window used to be ignored</param>
        /// <returns>Returns the Index for the Window on the list, or -1 if it can't be found</returns>
        private int getAboveWindow(Rectangle rect, dockMode dock, int ignoreIndex)
        {
            int index = -1;
            int top = 0;

            for (int i = 0; i < windowInfo.Count; i++)
            {
                if (windowInfo[i].dock == dock && windowInfo[i].window.Top < rect.Y && i != ignoreIndex)
                {
                    if (windowInfo[i].window.Top >= top)
                    {
                        top = windowInfo[i].window.Top;
                        index = i;
                    }
                }
            }

            return index;
        }

        /// <summary>
        ///     Get the Window placed exactly below the Rectangle passed to the function.
        ///     Returns -1 if it can't find any Window below.
        /// </summary>
        /// <param name="rect">The rectangle at where your window is located</param>
        /// <param name="dock">The docking mode that will be used</param>
        /// <param name="ignoreIndex">Index of the Window used to be ignored</param>
        /// <returns>Returns the Index for the Window on the list, or -1 if it can't be found</returns>
        private int getBelowWindow(Rectangle rect, dockMode dock, int ignoreIndex)
        {
            int index = -1;
            int top = int.MaxValue;

            for (int i = 0; i < windowInfo.Count; i++)
            {
                if (windowInfo[i].dock == dock && windowInfo[i].window.Top >= rect.Y && i != ignoreIndex)
                {
                    if (windowInfo[i].window.Top < top)
                    {
                        top = windowInfo[i].window.Top;
                        index = i;
                    }
                }
            }

            return index;
        }

        /// <summary>
        ///     Get the Window placed exactly at the left side of Rectangle passed to the function.
        ///     Returns -1 if it can't find any Window at the left.
        /// </summary>
        /// <param name="rect">The rectangle at where your window is located</param>
        /// <param name="dock">The docking mode that will be used</param>
        /// <param name="ignoreIndex">Index of the Window used to be ignored</param>
        /// <returns>Returns the Index for the Window on the list, or -1 if it can't be found</returns>
        private int getLeftWindow(Rectangle rect, dockMode dock, int ignoreIndex)
        {
            int index = -1;
            int left = 0;

            for (int i = 0; i < windowInfo.Count; i++)
            {
                if (windowInfo[i].dock == dock && windowInfo[i].window.Left < rect.X && i != ignoreIndex)
                {
                    if (windowInfo[i].window.Left >= left)
                    {
                        left = windowInfo[i].window.Left;
                        index = i;
                    }
                }
            }

            return index;
        }

        /// <summary>
        ///     Get the Window placed exactly at the right side of Rectangle passed to the function.
        ///     Returns -1 if it can't find any Window at the right.
        /// </summary>
        /// <param name="rect">The rectangle at where your window is located</param>
        /// <param name="dock">The docking mode that will be used</param>
        /// <param name="ignoreIndex">Index of the Window used to be ignored</param>
        /// <returns>Returns the Index for the Window on the list, or -1 if it can't be found</returns>
        private int getRightWindow(Rectangle rect, dockMode dock, int ignoreIndex)
        {
            int index = -1;
            int left = int.MaxValue;

            for (int i = 0; i < windowInfo.Count; i++)
            {
                if (windowInfo[i].dock == dock && windowInfo[i].window.Left >= rect.X && i != ignoreIndex)
                {
                    if (windowInfo[i].window.Left < left)
                    {
                        left = windowInfo[i].window.Left;
                        index = i;
                    }
                }
            }

            return index;
        }

        /// <summary>
        ///     Return the Index of a Window on the list based on the custom-set Tag property.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private int getWindowInfoIndex(int index)
        {
            for (int i = 0; i < windowInfo.Count; i++)
            {
                if (windowInfo[i].index == index) return i;
            }

            return -1;
        }

        /// <summary>
        ///     Automatically arrange the Windows on a given dock side when a Window is removed.
        /// </summary>
        /// <param name="dock"></param>
        /// <param name="dockRect"></param>
        private void autoArrange(dockMode dock, Rectangle dockRect)
        {
            int top = dockRect.Top;
            int left = dockRect.Left;

            switch (dock)
            {
                case dockMode.Right:
                case dockMode.Left:
                    int indexRL = getTopmostWindow(dock);
                    while (indexRL > -1)
                    {
                        int belowWindow = getBelowWindow(getRectangle(windowInfo[indexRL].window), dock, indexRL);

                        windowInfo[indexRL].window.Top = top;
                        windowInfo[indexRL].window.Left = dockRect.Left;
                        windowInfo[indexRL].window.Width = dockRect.Width;
                        if (belowWindow > -1)
                        {
                            windowInfo[indexRL].window.Height = (windowInfo[belowWindow].window.Top - windowInfo[indexRL].window.Top) - gripSize;
                            windowInfo[indexRL].hasGrip = true;
                        }
                        else
                        {
                            windowInfo[indexRL].window.Height = ((dockRect.Y + dockRect.Height) - windowInfo[indexRL].window.Top);
                            windowInfo[indexRL].hasGrip = false;
                        }
                        top += windowInfo[indexRL].window.Height + gripSize;
                        indexRL = belowWindow;
                    }

                break;
                        
                case dockMode.Bottom:
                case dockMode.Top:
                    int indexBT = getLeftmostWindow(dock);
                    while (indexBT > -1)
                    {
                        int rightWindow = getRightWindow(getRectangle(windowInfo[indexBT].window), dock, indexBT);

                        windowInfo[indexBT].window.Top = dockRect.Top;
                        windowInfo[indexBT].window.Left = left;
                        windowInfo[indexBT].window.Height = dockRect.Height;
                        if (rightWindow > -1)
                        {
                            windowInfo[indexBT].window.Width = (windowInfo[rightWindow].window.Left - windowInfo[indexBT].window.Left) - gripSize;
                            windowInfo[indexBT].hasGrip = true;
                        }
                        else
                        {
                            windowInfo[indexBT].window.Width = ((dockRect.X + dockRect.Width) - windowInfo[indexBT].window.Left);
                            windowInfo[indexBT].hasGrip = false;
                        }
                        left += windowInfo[indexBT].window.Width + gripSize;
                        indexBT = rightWindow;
                    }

                break;
            }
        }

        /// <summary>
        ///     Counts the number of Windows on a given dock side.
        /// </summary>
        /// <param name="dock"></param>
        /// <returns></returns>
        private int windowCount(dockMode dock)
        {
            int count = 0;
            for (int i = 0; i < windowInfo.Count; i++)
            {
                if (windowInfo[i].dock == dock) count++;
            }

            return count;
        }

        /// <summary>
        ///     Check if the control have any Window docked.
        /// </summary>
        /// <param name="dock"></param>
        /// <returns></returns>
        private bool hasDockedWindow()
        {
            for (int i = 0; i < windowInfo.Count; i++)
            {
                if (windowInfo[i].dock != dockMode.Floating) return true;
            }
            return false;
        }

        /// <summary>
        ///     Get the first window from top to bottom of a given dock side.
        /// </summary>
        /// <param name="dock"></param>
        /// <returns></returns>
        private int getTopmostWindow(dockMode dock)
        {
            int index = -1;
            int top = int.MaxValue;

            for (int i = 0; i < windowInfo.Count; i++)
            {
                if (windowInfo[i].dock == dock)
                {
                    if (windowInfo[i].window.Top < top)
                    {
                        top = windowInfo[i].window.Top;
                        index = i;
                    }
                }
            }

            return index;
        }

        /// <summary>
        ///     Get the last window from top to bottom of a given dock side.
        /// </summary>
        /// <param name="dock"></param>
        /// <returns></returns>
        private int getBottommostWindow(dockMode dock)
        {
            int index = -1;
            int top = 0;

            for (int i = 0; i < windowInfo.Count; i++)
            {
                if (windowInfo[i].dock == dock)
                {
                    if (windowInfo[i].window.Top >= top)
                    {
                        top = windowInfo[i].window.Top;
                        index = i;
                    }
                }
            }

            return index;
        }

        /// <summary>
        ///     Get the first window from left to right of a given dock side.
        /// </summary>
        /// <param name="dock"></param>
        /// <returns></returns>
        private int getLeftmostWindow(dockMode dock)
        {
            int index = -1;
            int left = int.MaxValue;

            for (int i = 0; i < windowInfo.Count; i++)
            {
                if (windowInfo[i].dock == dock)
                {
                    if (windowInfo[i].window.Left < left)
                    {
                        left = windowInfo[i].window.Left;
                        index = i;
                    }
                }
            }

            return index;
        }

        /// <summary>
        ///     Get the last window from left to right of a given dock side.
        /// </summary>
        /// <param name="dock"></param>
        /// <returns></returns>
        private int getRightmostWindow(dockMode dock)
        {
            int index = -1;
            int left = 0;

            for (int i = 0; i < windowInfo.Count; i++)
            {
                if (windowInfo[i].dock == dock)
                {
                    if (windowInfo[i].window.Left >= left)
                    {
                        left = windowInfo[i].window.Left;
                        index = i;
                    }
                }
            }

            return index;
        }

        /// <summary>
        ///     Returns the index of the Window at the center, or -1 if it doesnt have one.
        /// </summary>
        /// <returns></returns>
        private int getCenterWindow()
        {
            for (int i = 0; i < windowInfo.Count; i++) //Center
            {
                if (windowInfo[i].dock == dockMode.Center) return i;
            }

            return -1;
        }

        /// <summary>
        ///     Try to get enough space to create a dock side, reducing existing dock sides.
        /// </summary>
        /// <param name="dock"></param>
        /// <param name="newSpace"></param>
        /// <returns>Returns true if it get enough space or false if it doesn't</returns>
        private bool getSpaceToCreateDockSide(dockMode dock, int newSpace)
        {
            switch (dock)
            {
                case dockMode.Right:
                case dockMode.Left:
                    int bottomIndex = -1, topIndex = -1;
                    bool centerHasSpace = false;

                    int width = newSpace + gripSize;

                    //Primeiro verifica se é possivel encontrar um controle nos 3 docks do meio que dê para reduzir para caber o novo Dock Side
                    int bIndex = dock == dockMode.Right ? getRightmostWindow(dockMode.Bottom) : getLeftmostWindow(dockMode.Bottom); //Bottom
                    while (bIndex > -1)
                    {
                        if ((windowInfo[bIndex].window.Width - width) >= minimumWidth)
                        {
                            bottomIndex = bIndex;
                            break;
                        }

                        bIndex = dock == dockMode.Right ? getLeftWindow(getRectangle(windowInfo[bIndex].window), dockMode.Bottom, bIndex) : getRightWindow(getRectangle(windowInfo[bIndex].window), dockMode.Bottom, bIndex);
                    }

                    int tIndex = dock == dockMode.Right ? getRightmostWindow(dockMode.Top) : getLeftmostWindow(dockMode.Top); //Top
                    while (tIndex > -1)
                    {
                        if ((windowInfo[tIndex].window.Width - width) >= minimumWidth)
                        {
                            topIndex = tIndex;
                            break;
                        }

                        tIndex = dock == dockMode.Right ? getLeftWindow(getRectangle(windowInfo[tIndex].window), dockMode.Top, tIndex) : getRightWindow(getRectangle(windowInfo[tIndex].window), dockMode.Top, tIndex);
                    }

                    for (int i = 0; i < windowInfo.Count; i++) //Center
                    {
                        if (windowInfo[i].dock == dockMode.Center && (windowInfo[i].window.Width - width) >= minimumWidth) centerHasSpace = true;
                    }

                    //Redimensiona e reposiciona os *Windows/Controles* para dar espaço ao novo Dock Side
                    bool result = (bottomIndex > -1 || windowCount(dockMode.Bottom) == 0) && (topIndex > -1 || windowCount(dockMode.Top) == 0) && (centerHasSpace || windowCount(dockMode.Center) == 0);
                    if (width + (windowCount(dockMode.Right) > 0 ? rightDockWidth : 0) + (windowCount(dockMode.Left) > 0 ? leftDockWidth : 0) + gripSize * 2 > this.Width) result = false;

                    if (result)
                    {
                        if (dock == dockMode.Right) //Lado direito
                        {
                            if (bottomIndex > -1)
                            {
                                windowInfo[bottomIndex].window.Width -= width;

                                int index = getRightWindow(getRectangle(windowInfo[bottomIndex].window), dockMode.Bottom, bottomIndex);
                                while (index > -1)
                                {
                                    windowInfo[index].window.Left -= width;

                                    index = getRightWindow(getRectangle(windowInfo[index].window), dockMode.Bottom, index);
                                }
                            }

                            if (topIndex > -1)
                            {
                                windowInfo[topIndex].window.Width -= width;

                                int index = getRightWindow(getRectangle(windowInfo[topIndex].window), dockMode.Top, topIndex);
                                while (index > -1)
                                {
                                    windowInfo[index].window.Left -= width;

                                    index = getRightWindow(getRectangle(windowInfo[index].window), dockMode.Top, index);
                                }
                            }

                            if (windowCount(dockMode.Center) > 0)
                            {
                                int index = getCenterWindow();
                                windowInfo[index].window.Width -= width;
                            }
                        }
                        else //Lado esquerdo
                        {
                            if (bottomIndex > -1)
                            {
                                windowInfo[bottomIndex].window.Width -= width;
                                windowInfo[bottomIndex].window.Left += width;

                                int index = getLeftWindow(getRectangle(windowInfo[bottomIndex].window), dockMode.Bottom, bottomIndex);
                                while (index > -1)
                                {
                                    windowInfo[index].window.Left += width;

                                    index = getLeftWindow(getRectangle(windowInfo[index].window), dockMode.Bottom, index);
                                }
                            }

                            if (topIndex > -1)
                            {
                                windowInfo[topIndex].window.Width -= width;
                                windowInfo[topIndex].window.Left += width;

                                int index = getLeftWindow(getRectangle(windowInfo[topIndex].window), dockMode.Top, topIndex);
                                while (index > -1)
                                {
                                    windowInfo[index].window.Left += width;

                                    index = getLeftWindow(getRectangle(windowInfo[index].window), dockMode.Top, index);
                                }
                            }

                            if (windowCount(dockMode.Center) > 0)
                            {
                                int index = getCenterWindow();
                                windowInfo[index].window.Width -= width;
                                windowInfo[index].window.Left += width;
                            }
                        }
                    }

                    return result;

                case dockMode.Bottom:
                    int bottomHeight = newSpace + gripSize;
                    bool hasBottom = windowCount(dockMode.Top) == 0 || ((this.Height - topDockHeight) >= bottomHeight);
                    if (bottomHeight + (windowCount(dockMode.Bottom) > 0 ? bottomDockHeight : 0) + (windowCount(dockMode.Top) > 0 ? topDockHeight : 0) + gripSize * 2 > this.Height) hasBottom = false;

                    if (windowCount(dockMode.Center) > 0)
                    {
                        int index = getCenterWindow();
                        if (windowInfo[index].window.Height - bottomHeight >= minimumHeight)
                        {
                            windowInfo[index].window.Height -= bottomHeight;
                        }
                        else
                        {
                            hasBottom = false;
                        }
                    }

                    return hasBottom;
                case dockMode.Top:
                    int topHeight = newSpace + gripSize;
                    bool hasTop = windowCount(dockMode.Bottom) == 0 || ((this.Height - bottomDockHeight) >= topHeight);
                    if (topHeight + (windowCount(dockMode.Bottom) > 0 ? bottomDockHeight : 0) + (windowCount(dockMode.Top) > 0 ? topDockHeight : 0) + gripSize * 2 > this.Height) hasTop = false;

                    if (windowCount(dockMode.Center) > 0)
                    {
                        int index = getCenterWindow();
                        if (windowInfo[index].window.Height - topHeight >= minimumHeight)
                        {
                            windowInfo[index].window.Height -= topHeight;
                            windowInfo[index].window.Top += topHeight;
                        }
                        else
                        {
                            hasTop = false;
                        }
                    }

                    return hasTop;
            }

            return false;
        }

        /// <summary>
        ///     Get the Rectangle of the total area of a given dock side.
        /// </summary>
        /// <param name="dock"></param>
        /// <returns></returns>
        private Rectangle getDockRect(dockMode dock)
        {
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            Rectangle rightDock = new Rectangle((rect.X + rect.Width) - rightDockWidth, rect.Y, rightDockWidth, rect.Height);
            Rectangle leftDock = new Rectangle(rect.X, rect.Y, leftDockWidth, rect.Height);
            Rectangle bottomDock = new Rectangle(rect.X, (rect.Y + rect.Height) - bottomDockHeight, rect.Width, bottomDockHeight);
            Rectangle topDock = new Rectangle(rect.X, rect.Y, rect.Width, topDockHeight);
            Rectangle centerDock = new Rectangle(rect.Location, rect.Size);

            //Redimensiona os espaços de dock de acordo com outros Dock Sides existentes
            if (windowCount(dockMode.Right) > 0)
            {
                bottomDock.Width -= (rightDockWidth + gripSize);
                topDock.Width -= (rightDockWidth + gripSize);
                centerDock.Width -= (rightDockWidth + gripSize);
            }
            if (windowCount(dockMode.Left) > 0)
            {
                bottomDock.X += leftDockWidth + gripSize;
                bottomDock.Width -= (leftDockWidth + gripSize);

                topDock.X += leftDockWidth + gripSize;
                topDock.Width -= (leftDockWidth + gripSize);

                centerDock.X += leftDockWidth + gripSize;
                centerDock.Width -= (leftDockWidth + gripSize);
            }
            if (windowCount(dockMode.Bottom) > 0) centerDock.Height -= (bottomDockHeight + gripSize);
            if (windowCount(dockMode.Top) > 0)
            {
                centerDock.Y += topDockHeight + gripSize;
                centerDock.Height -= (topDockHeight + gripSize);
            }

            switch (dock)
            {
                case dockMode.Right: return rightDock;
                case dockMode.Left: return leftDock;
                case dockMode.Bottom: return bottomDock;
                case dockMode.Top: return topDock;
                case dockMode.Center: return centerDock;
            }

            return Rectangle.Empty;
        }

        /// <summary>
        ///     Calculate the proportions of all windows and Dock Sides relative to the size of this control.
        ///     It is used when the control is resized.
        /// </summary>
        /// <param name="ignoreSize">Set this to true if you want to update the proportions even if the width/height is too small or equal 0</param>
        private void calculateProportions(bool ignoreSize = false)
        {
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);

            for (int i = 0; i < windowInfo.Count; i++)
            {
                int width = windowInfo[i].window.Width;
                int height = windowInfo[i].window.Height;

                switch (windowInfo[i].dock)
                {
                    case dockMode.Right:
                    case dockMode.Left:
                        if (getBelowWindow(getRectangle(windowInfo[i].window), windowInfo[i].dock, i) > -1) height += gripSize;
                        break;
                    case dockMode.Bottom:
                    case dockMode.Top:
                        if (getRightWindow(getRectangle(windowInfo[i].window), windowInfo[i].dock, i) > -1) width += gripSize;
                        break;
                    case dockMode.Center:
                        if (windowCount(dockMode.Right) > 0) width += gripSize;
                        if (windowCount(dockMode.Left) > 0) width += gripSize;
                        if (windowCount(dockMode.Bottom) > 0) height += gripSize;
                        if (windowCount(dockMode.Top) > 0) height += gripSize;
                        break;
                }

                if (ignoreSize)
                {
                    windowInfo[i].windowProportions = new SizeF((float)width / rect.Width, (float)height / rect.Height);
                }
                else
                {
                    if (width >= minimumWidth && height >= minimumHeight) windowInfo[i].windowProportions = new SizeF((float)width / rect.Width, (float)height / rect.Height);
                }
            }

            rightDockProportionW = (float)rightDockWidth / rect.Width;
            leftDockProportionW = (float)leftDockWidth / rect.Width;
            bottomDockProportionH = (float)bottomDockHeight / rect.Height;
            topDockProportionH = (float)topDockHeight / rect.Height;
        }

        /// <summary>
        ///     Calculates the minimum size of this control, so all Windows can have at least one pixel of Width/Height and the resize grip.
        /// </summary>
        private void calculateMinimumSize()
        {
            int width = 0, height = 0;

            int bottomCount = windowCount(dockMode.Bottom);
            int topCount = windowCount(dockMode.Top);
            bool hasCenter = windowCount(dockMode.Center) > 0;
            if (bottomCount > 0 || topCount > 0)
            {
                width = ((Math.Max(bottomCount, topCount) * (1 + gripSize)) - gripSize);
            }
            else if (hasCenter)
            {
                width = 1; //Center
            }
            if (windowCount(dockMode.Right) > 0) width += 1 + gripSize;
            if (windowCount(dockMode.Left) > 0) width += 1 + gripSize;

            int rightCount = windowCount(dockMode.Right);
            int leftCount = windowCount(dockMode.Left);
            if (rightCount > 0 || leftCount > 0) height = ((Math.Max(rightCount, leftCount) * (1 + gripSize)) - gripSize);
            height = Math.Max(height, (bottomCount > 0 ? 1 + gripSize : 0) + (topCount > 0 ? 1 + gripSize : 0) + (hasCenter ? 1 + gripSize : 0));

            this.MinimumSize = new Size(width, height);
        }

        /// <summary>
        ///     Creates a rectangle from the Location/Size properties of a control.
        /// </summary>
        /// <param name="window"></param>
        /// <returns>The rectangle :P</returns>
        private Rectangle getRectangle(Control window)
        {
            return new Rectangle(window.Location, window.Size);
        }
    }
}
