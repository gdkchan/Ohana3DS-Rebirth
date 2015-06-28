using System;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Ohana3DS_Rebirth.GUI
{
    [Designer(typeof(OScrollablePanelDesigner))]
    public partial class OScrollablePanel : UserControl
    {
        public OScrollablePanel()
        {
            init();
            InitializeComponent();
        }

        private void init()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Panel ContentArea
        {
            get { return ContentPanel; }
        }

        protected override void OnLayout(LayoutEventArgs e)
        {
            recalc();

            base.OnLayout(e);
        }

        private void recalc()
        {
            int maxTop = 0, maxHeight = 0;
            foreach (Control child in ContentPanel.Controls)
            {
                if (child.Top > maxTop || (child.Top == maxTop && child.Height > maxHeight))
                {
                    maxTop = child.Top;
                    maxHeight = child.Height;
                }
            }
            int height = maxTop + maxHeight;
            PnlVScroll.Visible = height > Height;
            ContentPanel.Size = new Size(Width - (PnlVScroll.Visible ? PnlVScroll.Width : 0), height);
            if (!PnlVScroll.Visible) ContentPanel.Top = 0;
            if (PnlVScroll.Visible)
            {
                PnlVScroll.MaximumScroll = ContentPanel.Height - Height;
                ContentPanel.Top = -PnlVScroll.Value;
            }
            PnlVScroll.Left = Width - PnlVScroll.Width;
            PnlVScroll.Height = Height;

            Refresh();
        }

        public class OScrollablePanelDesigner : ParentControlDesigner
        {
            public override void Initialize(System.ComponentModel.IComponent component)
            {
                base.Initialize(component);

                if (this.Control is OScrollablePanel)
                {
                    this.EnableDesignMode(((OScrollablePanel)this.Control).ContentArea, "ContentArea");
                }
            }
        }

        private void VScroll_ScrollChanged(object sender, EventArgs e)
        {
            ContentPanel.Top = -PnlVScroll.Value;
        }
    }
}
