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
        public Color BgColor1 = Color.FromArgb(32, 32, 32); //Gray
        public Color BgColor2 = Color.Black;
        public Color ItemColor = Color.WhiteSmoke;
        public Color ItemHover = Color.FromArgb(200, 61, 170, 255);
        public Color ItemSelect = Color.FromArgb(200, 61, 170, 255);

        public OMenuStrip()
        {
            InitializeComponent();
        }

        private void Draw_Rounded_Rectangle(Graphics Gfx, Rectangle Rect, int Width, Color Color)
        {
            Gfx.SmoothingMode = SmoothingMode.HighQuality;
            Pen Pen = new Pen(Color);

            RectangleF Arc_Rect = new RectangleF(Rect.Location, new SizeF(Width, Width));

            //Top Left Arc
            Gfx.DrawArc(Pen, Arc_Rect, 180, 90);
            Gfx.DrawLine(Pen, Rect.X + Width / 2, Rect.Y, Rect.X + Rect.Width - Width / 2, Rect.Y);

            //Top Right Arc
            Arc_Rect.X = Rect.Right - Width;
            Gfx.DrawArc(Pen, Arc_Rect, 270, 90);
            Gfx.DrawLine(Pen, Rect.X + Rect.Width, Rect.Y + Width / 2, Rect.X + Rect.Width, Rect.Y + Rect.Height - Width / 2);

            //Bottom Right Arc
            Arc_Rect.Y = Rect.Bottom - Width;
            Gfx.DrawArc(Pen, Arc_Rect, 0, 90);
            Gfx.DrawLine(Pen, Rect.X + Width / 2, Rect.Y + Rect.Height, Rect.X + Rect.Width - Width / 2, Rect.Y + Rect.Height);

            //Bottom Left Arc
            Arc_Rect.X = Rect.Left;
            Gfx.DrawArc(Pen, Arc_Rect, 90, 90);
            Gfx.DrawLine(Pen, Rect.X, Rect.Y + Width / 2, Rect.X, Rect.Y + Rect.Height - Width / 2);
        }

        //Render Image Margin and Item background
        protected override void OnRenderImageMargin(System.Windows.Forms.ToolStripRenderEventArgs e)
        {
            base.OnRenderImageMargin(e);
            //Shadow at the right of image margin
            Rectangle Rect = new Rectangle(e.AffectedBounds.Width, 2, 1, e.AffectedBounds.Height);
            Rectangle Rect_2 = new Rectangle(e.AffectedBounds.Width + 1, 2, 1, e.AffectedBounds.Height);
            //Background
            Rectangle Rect_3 = new Rectangle(0, 0, e.ToolStrip.Width, e.ToolStrip.Height);
            //Border
            Rectangle Rect_4 = new Rectangle(0, 1, e.ToolStrip.Width - 1, e.ToolStrip.Height - 2);
            e.Graphics.FillRectangle(new SolidBrush(BgColor1), Rect_3);
            e.Graphics.FillRectangle(new SolidBrush(BgColor1), e.AffectedBounds);
            e.Graphics.FillRectangle(new SolidBrush(BgColor1), Rect);
            e.Graphics.FillRectangle(new SolidBrush(ItemColor), Rect_2);
            e.Graphics.DrawRectangle(new Pen(ItemColor), Rect_4);
        }

        //Render Checkmark
        protected override void OnRenderItemCheck(System.Windows.Forms.ToolStripItemImageRenderEventArgs e)
        {
            base.OnRenderItemCheck(e);
            Rectangle Rect = new Rectangle(4, 2, 18, 18);
            e.Graphics.FillRectangle(new SolidBrush(ItemColor), Rect);
            Draw_Rounded_Rectangle(e.Graphics, new Rectangle(Rect.Left - 1, Rect.Top - 1, Rect.Width, Rect.Height + 1), 4, ItemColor);
            e.Graphics.DrawImage(e.Image, new Point(5, 3));
        }

        //Render separator
        protected override void OnRenderSeparator(System.Windows.Forms.ToolStripSeparatorRenderEventArgs e)
        {
            base.OnRenderSeparator(e);
            Rectangle Rect = new Rectangle(32, 3, e.Item.Width - 32, 1);
            Rectangle Rect_2 = new Rectangle(32, 4, e.Item.Width - 32, 1);
            e.Graphics.FillRectangle(new SolidBrush(BgColor2), Rect);
            e.Graphics.FillRectangle(new SolidBrush(ItemColor), Rect_2);
        }

        //Render arrow
        protected override void OnRenderArrow(System.Windows.Forms.ToolStripArrowRenderEventArgs e)
        {
            e.ArrowColor = ItemColor;
            base.OnRenderArrow(e);
        }

        //Render MenuItem background
        protected override void OnRenderMenuItemBackground(System.Windows.Forms.ToolStripItemRenderEventArgs e)
        {
            base.OnRenderMenuItemBackground(e);
            if (e.Item.Enabled)
            {
                if (e.Item.Selected)
                {
                    //If item is selected
                    Rectangle Rect = new Rectangle(3, 2, e.Item.Width - 5, e.Item.Height - 3);
                    e.Graphics.FillRectangle(new SolidBrush(ItemHover), Rect);
                }

                //If item is MenuHeader and menu is dropped down: selection rectangle is now darker
                if (((ToolStripMenuItem)e.Item).DropDown.Visible && e.Item.IsOnDropDown == false)
                {
                    Rectangle Rect = new Rectangle(3, 2, e.Item.Width - 5, e.Item.Height - 3);
                    e.Graphics.FillRectangle(new SolidBrush(ItemSelect), Rect);
                }
                if (e.Item.IsOnDropDown == false)
                {
                    //Make font Upper Case for Menu Header
                    e.Item.Text = e.Item.Text.ToUpper();
                }
                e.Item.ForeColor = ItemColor;
            }
            else
            {
                e.Item.ForeColor = BgColor1;
            }
        }
    }
}
