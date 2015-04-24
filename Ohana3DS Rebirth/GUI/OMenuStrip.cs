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
    public partial class OMenuStrip : ToolStripRenderer
    {
        private Color bgColor1 = Color.FromArgb(24, 24, 24);
        private Color bgColor2 = Color.Black;
        private Color itemColor = Color.WhiteSmoke;
        private Color itemHover = Color.FromArgb(0x5f, 15, 82, 186);
        private Color itemSelect = Color.FromArgb(0x7f, 15, 82, 186);

        public OMenuStrip()
        {
            InitializeComponent();
        }

        protected override void OnRenderImageMargin(System.Windows.Forms.ToolStripRenderEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(bgColor1), new Rectangle(0, 0, e.ToolStrip.Width, e.ToolStrip.Height));
            e.Graphics.FillRectangle(new SolidBrush(bgColor1), e.AffectedBounds);
            e.Graphics.FillRectangle(new SolidBrush(bgColor1), new Rectangle(e.AffectedBounds.Width, 2, 1, e.AffectedBounds.Height));

            base.OnRenderImageMargin(e);
        }

        protected override void OnRenderItemCheck(System.Windows.Forms.ToolStripItemImageRenderEventArgs e)
        {
            Rectangle rect = new Rectangle(4, 2, 18, 18);
            e.Graphics.FillRectangle(new SolidBrush(Color.Transparent), rect);
            e.Graphics.DrawImage(Ohana3DS_Rebirth.Properties.Resources.icn_ticked, new Point(5, 3));
        }

        protected override void OnRenderSeparator(System.Windows.Forms.ToolStripSeparatorRenderEventArgs e)
        {
            Rectangle rect = new Rectangle(32, 3, e.Item.Width - 32, 1);
            Rectangle rect2 = new Rectangle(32, 4, e.Item.Width - 32, 1);
            e.Graphics.FillRectangle(new SolidBrush(bgColor2), rect);
            e.Graphics.FillRectangle(new SolidBrush(itemColor), rect2);

            base.OnRenderSeparator(e);
        }

        protected override void OnRenderArrow(System.Windows.Forms.ToolStripArrowRenderEventArgs e)
        {
            e.ArrowColor = itemColor;

            base.OnRenderArrow(e);
        }

        protected override void OnRenderMenuItemBackground(System.Windows.Forms.ToolStripItemRenderEventArgs e)
        {
            if (e.Item.Enabled)
            {
                if (e.Item.Selected)
                {
                    //If item is selected
                    Rectangle Rect = new Rectangle(3, 2, e.Item.Width - 5, e.Item.Height - 3);
                    e.Graphics.FillRectangle(new SolidBrush(itemHover), Rect);
                }

                //If item is MenuHeader and menu is dropped down
                if (((ToolStripMenuItem)e.Item).DropDown.Visible && e.Item.IsOnDropDown == false)
                {
                    Rectangle Rect = new Rectangle(3, 2, e.Item.Width - 5, e.Item.Height - 3);
                    e.Graphics.FillRectangle(new SolidBrush(itemSelect), Rect);
                }
                if (e.Item.IsOnDropDown == false)
                {
                    //Make font Upper Case for Menu Header
                    e.Item.Text = e.Item.Text.ToUpper();
                }
                e.Item.ForeColor = itemColor;
            }
            else
            {
                e.Item.ForeColor = bgColor1;
            }

            base.OnRenderMenuItemBackground(e);
        }
    }
}
