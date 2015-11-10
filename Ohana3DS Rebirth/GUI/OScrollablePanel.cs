using System;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Ohana3DS_Rebirth.GUI
{
    [Designer(typeof(OScrollablePanelDesigner))]
    public partial class OScrollablePanel : UserControl
    {
        private bool suspended;

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

        protected override void OnControlAdded(ControlEventArgs e)
        {
            e.Control.Layout += Control_Layout;

            base.OnControlAdded(e);
        }

        private void Control_Layout(Object sender, EventArgs e)
        {
            if (!suspended) recalc();
        }

        private void recalc()
        {
            int maxY = 0;
            foreach (Control child in ContentPanel.Controls)
            {
                int y = child.Top + child.Height;
                if (child.Visible && y > maxY)
                {
                    maxY = y;
                }
            }
            int height = maxY;
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
        }

        /// <summary>
        ///     Stops calculating the scroll and size.
        /// </summary>
        public void suspendUpdate()
        {
            suspended = true;
        }

        /// <summary>
        ///     Continues calculating the scroll and size and forces an update.
        /// </summary>
        public void resumeUpdate()
        {
            suspended = false;
            recalc();
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

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (PnlVScroll.Visible)
            {
                PnlVScroll.Value = e.Delta > 0
                    ? Math.Max(PnlVScroll.Value - 32, 0)
                    : Math.Min(PnlVScroll.Value + 32, PnlVScroll.MaximumScroll);

                ContentPanel.Top = -PnlVScroll.Value;
            }

            base.OnMouseWheel(e);
        }

        private void VScroll_ScrollChanged(object sender, EventArgs e)
        {
            ContentPanel.Top = -PnlVScroll.Value;
            ContentPanel.Refresh();
        }
    }
}
