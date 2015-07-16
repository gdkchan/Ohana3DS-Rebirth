using System;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Runtime.InteropServices;

namespace Ohana3DS_Rebirth.GUI
{
    [Designer(typeof(OScrollablePanelDesigner))]
    public partial class OScrollablePanel : UserControl
    {
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
        private const int WM_SETREDRAW = 11;

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

        public void SuspendDrawing()
        {
            SendMessage(Handle, WM_SETREDRAW, 0, 0);
        }

        public void ResumeDrawing()
        {
            SendMessage(Handle, WM_SETREDRAW, 1, 0);
            Refresh();
        }

        private void Control_Layout(Object sender, EventArgs e)
        {
            recalc();
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
