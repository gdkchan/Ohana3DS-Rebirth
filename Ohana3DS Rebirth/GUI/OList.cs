using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Ohana3DS_Rebirth.GUI
{
    public partial class OList : UserControl
    {
        private int tileSize = 64;
        private int headerSize = 24;

        Color selectedColor = ColorManager.highlight;
        Point mousePosition;
        bool clicked;

        public event EventHandler SelectedIndexChanged;
        private int oldIndex = -1;

        public class listItem
        {
            public Bitmap thumbnail;
            public String text;

            public listItem(string _text, Bitmap _thumbnail = null)
            {
                text = _text;
                thumbnail = _thumbnail;
            }
        }
        public class listItemGroup
        {
            public List<listItem> columns;
            public listItemGroup()
            {
                columns = new List<listItem>();
            }
        }
        List<listItemGroup> list = new List<listItemGroup>();

        public class columnHeader
        {
            public int width;
            public String text;

            public columnHeader(int _width, string _text)
            {
                width = _width;
                text = _text;
            }
        }
        List<columnHeader> columns = new List<columnHeader>();

        int selectedIndex = -1;
        private bool showHeader = false;

        public OList()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            InitializeComponent();
        }

        /// <summary>
        ///     Color of the bar when a item is selected.
        /// </summary>
        public Color SelectionColor
        {
            get
            {
                return selectedColor;
            }
            set
            {
                selectedColor = value;
            }
        }

        /// <summary>
        ///     The height of each item on the list.
        /// </summary>
        public int ItemHeight
        {
            get
            {
                return tileSize;
            }
            set
            {
                tileSize = value;
            }
        }

        /// <summary>
        ///     The height of the header.
        /// </summary>
        public int HeaderHeight
        {
            get
            {
                return headerSize;
            }
            set
            {
                headerSize = value;
            }
        }

        /// <summary>
        ///     The index of the selected item. -1 means no item selected.
        /// </summary>
        public int SelectedIndex
        {
            get
            {
                return selectedIndex;
            }
            set
            {
                if (value > -1 && value < list.Count)
                {
                    selectedIndex = value;
                    updateScroll();
                    if (selectedIndex != oldIndex && SelectedIndexChanged != null) SelectedIndexChanged(this, EventArgs.Empty);
                    oldIndex = selectedIndex;
                }
            }
        }

        /// <summary>
        ///     Adds a item to the list. At least one column is necessary.
        /// </summary>
        /// <param name="item">The item with one or more columns</param>
        public void addItem(listItemGroup item)
        {
            list.Add(item);
            recalcScroll();
        }

        /// <summary>
        ///     Adds a item to the list.
        /// </summary>
        /// <param name="item"></param>
        public void addItem(listItem item)
        {
            listItemGroup newItem = new listItemGroup();
            newItem.columns.Add(item);
            addItem(newItem);
        }

        /// <summary>
        ///     Adds a text to the list.
        /// </summary>
        /// <param name="text"></param>
        public void addItem(string text)
        {
            listItemGroup newItem = new listItemGroup();
            newItem.columns.Add(new listItem(text));
            addItem(newItem);
        }

        /// <summary>
        ///     Adds a new column to the list.
        /// </summary>
        /// <param name="column"></param>
        public void addColumn(columnHeader column)
        {
            columns.Add(column);
            showHeader = true;
        }

        /// <summary>
        ///     Erase the list.
        /// </summary>
        public void flush()
        {
            list.Clear();
            columns.Clear();
            selectedIndex = -1;
            oldIndex = -1;
            recalcScroll();
            showHeader = false;
        }

        private void recalcScroll()
        {
            int totalSize = (list.Count * tileSize) + (showHeader ? headerSize : 0);
            if (totalSize > Height)
            {
                ListScroll.MaximumScroll = totalSize - Height;
                ListScroll.Visible = true;
            }
            else
            {
                ListScroll.Value = 0;
                ListScroll.Visible = false;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (list.Count == 0) return;

            int totalSize = (list.Count * tileSize) + (showHeader ? headerSize : 0);
            int startY = 0;
            if (totalSize > Height) startY = ListScroll.Value * -1;
            int index = 0;

            //Renderiza a parte do Header
            int i = 0;
            if (showHeader)
            {
                int columnX = 0;
                
                foreach (columnHeader header in columns)
                {
                    int columnWidth;
                    if (i == columns.Count - 1) columnWidth = Width - columnX; else columnWidth = header.width;
                    if (columnWidth < 2) break;

                    Rectangle rect = new Rectangle(columnX, startY, columnWidth - 1, headerSize);
                    e.Graphics.FillRectangle(new LinearGradientBrush(rect, Color.Transparent, Color.FromArgb(0x2f, 0x2f, 0x2f), LinearGradientMode.Vertical), rect);
                    e.Graphics.DrawLine(new Pen(Color.FromArgb(0x1f, 0x1f, 0x1f)), new Point(columnX, startY + headerSize), new Point(columnX + (columnWidth - 2), startY + headerSize));

                    Font font = new Font(Font.FontFamily, Font.Size, FontStyle.Bold);
                    int textHeight = (int)e.Graphics.MeasureString(header.text, font).Height;
                    e.Graphics.DrawString(DrawingHelper.clampText(header.text, font, columnWidth), font, new SolidBrush(ForeColor), new Point(columnX, startY + ((headerSize / 2) - (textHeight / 2))));
                    font.Dispose();

                    columnX += columnWidth;
                    i++;
                }

                startY += headerSize;
            }

            //Renderiza os itens da lista
            foreach (listItemGroup item in list)
            {
                if (startY >= -tileSize)
                {
                    if (startY > Height) break;

                    Rectangle itemRect = new Rectangle(0, startY, Width, tileSize);
                    Rectangle mouseRect = new Rectangle(mousePosition, new Size(1, 1));

                    //Selecionado
                    if (clicked)
                    {
                        if (itemRect.IntersectsWith(mouseRect))
                        {
                            e.Graphics.FillRectangle(new SolidBrush(selectedColor), new Rectangle(0, startY, Width, tileSize));
                            selectedIndex = index;
                            if (selectedIndex != oldIndex && SelectedIndexChanged != null) SelectedIndexChanged(this, EventArgs.Empty);
                            oldIndex = selectedIndex;
                            clicked = false;
                        }
                    }
                    else
                    {
                        if (index == selectedIndex) e.Graphics.FillRectangle(new SolidBrush(selectedColor), new Rectangle(0, startY, Width, tileSize));
                    }

                    //Textos e afins
                    i = 0;
                    int x = 0;
                    foreach (listItem subItem in item.columns)
                    {
                        int columnWidth;
                        if (i == columns.Count - 1 || columns.Count == 0) columnWidth = Width - x; else columnWidth = columns[i].width;
                        if (columnWidth < 1) break;

                        int textX = x;
                        if (subItem.thumbnail != null)
                        {
                            float aspectRatio = subItem.thumbnail.Width / subItem.thumbnail.Height;
                            int width = (int)(tileSize * aspectRatio);
                            if (width > columnWidth)
                            {
                                int height = Math.Min(tileSize, subItem.thumbnail.Height);
                                e.Graphics.DrawImage(subItem.thumbnail, new Rectangle(x, startY + ((tileSize / 2) - (height / 2)), columnWidth, height), new Rectangle((subItem.thumbnail.Width / 2) - (columnWidth / 2), 0, columnWidth, subItem.thumbnail.Height), GraphicsUnit.Pixel);
                            }
                            else
                            {
                                e.Graphics.DrawImage(subItem.thumbnail, new Rectangle(x + ((columnWidth / 2) - (width / 2)), startY, width, tileSize));
                            }
                            textX += width;
                        }
                        int textHeight = (int)e.Graphics.MeasureString(subItem.text, Font).Height;
                        e.Graphics.DrawString(DrawingHelper.clampText(subItem.text, Font, columnWidth), Font, new SolidBrush(ForeColor), new Point(textX, startY + ((tileSize / 2) - (textHeight / 2))));

                        x += columnWidth;
                        i++;
                    }
                }

                startY += tileSize;
                index++;
            }

            if (clicked) selectedIndex = -1;
            clicked = false;

            base.OnPaint(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mousePosition = e.Location;
                clicked = true;
                Refresh();
            }

            base.OnMouseDown(e);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (ListScroll.Visible)
            {
                ListScroll.Value = e.Delta > 0 
                    ? Math.Max(ListScroll.Value - 32, 0) 
                    : Math.Min(ListScroll.Value + 32, ListScroll.MaximumScroll);
                Refresh();
            }

            base.OnMouseWheel(e);
        }

        protected override void OnLayout(LayoutEventArgs e)
        {
            recalcScroll();
            Refresh();

            base.OnLayout(e);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Up: if (selectedIndex > 0) selectedIndex--; break;
                case Keys.Down: if (selectedIndex < list.Count - 1) selectedIndex++; break;
            }
            if (keyData == Keys.Up || keyData == Keys.Down)
            {
                updateScroll();
                if (selectedIndex != oldIndex && SelectedIndexChanged != null) SelectedIndexChanged(this, EventArgs.Empty);
                oldIndex = selectedIndex;
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void updateScroll()
        {
            int header = showHeader ? headerSize : 0;
            int totalSize = (list.Count * tileSize) + header;
            int startY = totalSize > Height ? ListScroll.Value : 0;
            int positionY = (selectedIndex * tileSize) + header;
            if (totalSize > Height)
            {
                if (positionY < startY)
                {
                    ListScroll.Value = positionY;
                }
                else if (positionY > startY + Height)
                {
                    ListScroll.Value = (positionY + tileSize - 1) - Height;
                }
            }
            Refresh();
        }

        private void ListScroll_ScrollChanged(object sender, EventArgs e)
        {
            Refresh();
        }
    }
}
