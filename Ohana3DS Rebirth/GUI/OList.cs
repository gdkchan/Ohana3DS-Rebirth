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
            public string text;

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
            public string text;

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
                if (value >= -1 && value < list.Count)
                {
                    selectedIndex = value;
                    if (selectedIndex != oldIndex && SelectedIndexChanged != null) SelectedIndexChanged(this, EventArgs.Empty);
                    oldIndex = selectedIndex;
                    if (selectedIndex > -1) updateScroll(); else Refresh();
                }
            }
        }

        /// <summary>
        ///     Total number of items on the list.
        /// </summary>
        public int Count
        {
            get
            {
                return list.Count;
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
        ///     Adds a Collection of listItemGroup to the list.
        /// </summary>
        /// <param name="itemList">The Collection</param>
        public void addRange(IEnumerable<listItemGroup> itemList)
        {
            list.AddRange(itemList);
        }

        /// <summary>
        ///     Adds a Array of listItem to the list.
        /// </summary>
        /// <param name="itemList">The Array</param>
        public void addRange(listItem[] itemList)
        {
            foreach (listItem item in itemList) addItem(item);
        }

        /// <summary>
        ///     Adds a Array of String to the list.
        /// </summary>
        /// <param name="itemList">The Array</param>
        public void addRange(string[] itemList)
        {
            foreach (string item in itemList) addItem(item);
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
        ///     Removes the item at given index.
        /// </summary>
        /// <param name="index">Item index</param>
        public void removeItem(int index)
        {
            if (index >= list.Count || index < 0) return;
            if (selectedIndex == list.Count - 1) selectedIndex--;
            foreach (listItem subItem in list[index].columns) if (subItem.thumbnail != null) subItem.thumbnail.Dispose();
            list.RemoveAt(index);
            recalcScroll();
            updateScroll();
            if (SelectedIndexChanged != null) SelectedIndexChanged(this, EventArgs.Empty);
            oldIndex = selectedIndex;
        }

        /// <summary>
        ///     Changes the Text of a Item at the given Index.
        /// </summary>
        /// <param name="index">Index number of the item</param>
        /// <param name="newText">New text</param>
        public void changeItem(int index, string newText)
        {
            if (index >= list.Count || index < 0) return;
            listItemGroup item = list[index];
            list.RemoveAt(index);
            item.columns[0].text = newText;
            list.Insert(index, item);
            Refresh();
        }

        /// <summary>
        ///     Changes the Item at the given Index.
        /// </summary>
        /// <param name="index">Index number of the item</param>
        /// <param name="newText">New item</param>
        public void changeItem(int index, listItemGroup newItem)
        {
            if (index >= list.Count || index < 0) return;
            list.RemoveAt(index);
            list.Insert(index, newItem);
            Refresh();
        }

        /// <summary>
        ///     Returns the text of the Item at the given Index.
        /// </summary>
        /// <param name="index">Index where the item is located</param>
        /// <returns></returns>
        public string itemAt(int index)
        {
            if (index >= list.Count || index < 0) return null;
            return list[index].columns[0].text;
        }

        /// <summary>
        ///     Erase the list.
        /// </summary>
        public void flush()
        {
            for (int i = 0; i < list.Count; i++)
            {
                foreach (listItem subItem in list[i].columns) if (subItem.thumbnail != null) subItem.thumbnail.Dispose();
            }

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
            if (totalSize > Height) startY = -ListScroll.Value;
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
                    e.Graphics.DrawString(DrawingUtils.clampText(e.Graphics, header.text, font, columnWidth), font, new SolidBrush(ForeColor), new Point(columnX, startY + ((headerSize / 2) - (textHeight / 2))));
                    font.Dispose();

                    columnX += columnWidth;
                    i++;
                }

                startY += headerSize;
            }

            //Renderiza os itens da lista
            for (int j = 0; j < list.Count; j++)
            {
                listItemGroup item = list[j];

                if (startY >= -tileSize)
                {
                    if (startY > Height) break;

                    Rectangle itemRect = new Rectangle(0, startY, Width, tileSize);

                    //Selecionado
                    if (clicked)
                    {
                        if (itemRect.Contains(mousePosition))
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

                        if (subItem.thumbnail != null)
                        {
                            float ar = (float)subItem.thumbnail.Width / subItem.thumbnail.Height;
                            int w = (int)(tileSize * ar);
                            int h = (int)(columnWidth / ar);
                            Rectangle dst;
                            if (w > columnWidth)
                                dst = new Rectangle(x, startY + ((tileSize / 2) - (h / 2)), columnWidth, h);
                            else
                                dst = new Rectangle(x + ((columnWidth / 2) - (w / 2)), startY, w, tileSize);

                            Rectangle src = new Rectangle(0, 0, subItem.thumbnail.Width, subItem.thumbnail.Height);
                            e.Graphics.DrawImage(subItem.thumbnail, dst, src, GraphicsUnit.Pixel);
                        }

                        int textHeight = (int)e.Graphics.MeasureString(subItem.text, Font).Height;
                        e.Graphics.DrawString(DrawingUtils.clampText(e.Graphics, subItem.text, Font, columnWidth), Font, new SolidBrush(ForeColor), new Point(x, startY + ((tileSize / 2) - (textHeight / 2))));

                        x += columnWidth;
                        i++;
                    }
                }

                startY += tileSize;
                index++;
            }

            if (clicked)
            {
                selectedIndex = -1;
                if (selectedIndex != oldIndex && SelectedIndexChanged != null) SelectedIndexChanged(this, EventArgs.Empty);
                oldIndex = selectedIndex;
            }
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
                    ListScroll.Value = positionY;
                else if (positionY > startY + Height)
                    ListScroll.Value = (positionY + tileSize - 1) - Height;
            }

            Refresh();
        }

        private void ListScroll_ScrollChanged(object sender, EventArgs e)
        {
            Refresh();
        }
    }
}
