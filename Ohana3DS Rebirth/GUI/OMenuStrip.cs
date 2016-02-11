//OMenuStrip made for Ohana3DS by gdkchan
//Custom menu design, should be used with "Renderer" property of a Menu

using System.Drawing;
using System.Windows.Forms;

using Ohana3DS_Rebirth.Properties;

namespace Ohana3DS_Rebirth.GUI
{
    public partial class OMenuStrip : ToolStripRenderer
    {
        private Color bgColor = Color.FromArgb(16, 16, 16);
        private Color separator = Color.Gray;
        private Color itemColor = Color.WhiteSmoke;
        private Color itemHover = ColorManager.ui_hoveredLight;
        private Color itemSelect = ColorManager.ui_hoveredDark;

        public OMenuStrip()
        {
            InitializeComponent();
        }

        protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
        {
            Rectangle bgRect = new Rectangle(0, 0, e.ToolStrip.Width, e.ToolStrip.Height);
            Rectangle border = new Rectangle(0, 0, e.ToolStrip.Width - 1, e.ToolStrip.Height - 1);

            e.Graphics.FillRectangle(new SolidBrush(bgColor), bgRect);
            e.Graphics.FillRectangle(new SolidBrush(bgColor), e.AffectedBounds);
            e.Graphics.DrawRectangle(new Pen(separator), border);

            Point pt1 = new Point(e.AffectedBounds.Width, e.AffectedBounds.Y);
            Point pt2 = new Point(pt1.X, pt1.Y + e.AffectedBounds.Height);
            e.Graphics.DrawLine(new Pen(separator), pt1, pt2);

            base.OnRenderImageMargin(e);
        }

        protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e)
        {
            Rectangle src = new Rectangle(Point.Empty, Resources.ui_icon_tick.Size);
            Rectangle dst = new Rectangle(new Point(4, 2), Resources.ui_icon_tick.Size);
            e.Graphics.DrawImage(Resources.ui_icon_tick, dst, src, GraphicsUnit.Pixel);
        }

        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
        {
            Point pt1 = new Point(32, 3);
            Point pt2 = new Point(e.Item.Width, 3);
            e.Graphics.DrawLine(new Pen(separator), pt1, pt2);

            base.OnRenderSeparator(e);
        }

        protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
        {
            e.ArrowColor = itemColor;

            base.OnRenderArrow(e);
        }

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            if (e.Item.Enabled)
            {
                if (e.Item.Selected)
                {
                    //If item is selected
                    Rectangle rect = new Rectangle(3, 2, e.Item.Width - 5, e.Item.Height - 3);
                    e.Graphics.FillRectangle(new SolidBrush(itemHover), rect);
                }

                //If item is MenuHeader and menu is dropped down
                if (((ToolStripMenuItem)e.Item).DropDown.Visible && e.Item.IsOnDropDown == false)
                {
                    Rectangle rect = new Rectangle(3, 2, e.Item.Width - 5, e.Item.Height - 3);
                    e.Graphics.FillRectangle(new SolidBrush(itemSelect), rect);
                }

                e.Item.ForeColor = itemColor;
            }

            base.OnRenderMenuItemBackground(e);
        }
    }
}
