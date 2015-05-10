//OLabel made for Ohana3DS by gdkchan
//Custom Label control with Image support

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
    public partial class OLabel : Control
    {
        Bitmap img;
        bool autoSize;

        public OLabel()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            InitializeComponent();
        }

        public OLabel(IContainer container)
        {
            container.Add(this);

            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            InitializeComponent();
        }

        public Bitmap Image
        {
            get
            {
                return img;
            }
            set
            {
                img = value;
                this.Refresh();
            }
        }

        public bool AutomaticSize
        {
            get
            {
                return autoSize;
            }
            set
            {
                autoSize = value;
                this.Refresh();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            int imgWidth = 0, imgHeight = 0;
            if (img != null)
            {
                imgWidth = img.Width;
                imgHeight = img.Height;
                e.Graphics.DrawImage(img, new Rectangle(0, (this.Height / 2) - (img.Height / 2), img.Width, img.Height));
            }

            SizeF textSize = e.Graphics.MeasureString(this.Text, this.Font);
            if (autoSize) this.Size = new Size(imgWidth + (int)textSize.Width, Math.Max(imgHeight, (int)textSize.Height));
            e.Graphics.DrawString(this.Text, this.Font, new SolidBrush(this.ForeColor), new Point(imgWidth + (((this.Width - imgWidth) / 2) - ((int)textSize.Width / 2)), 0));

            base.OnPaint(e);
        }
    }
}
