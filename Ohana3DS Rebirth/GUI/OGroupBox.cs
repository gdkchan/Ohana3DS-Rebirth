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
    [Designer(typeof(OGroupBoxDesigner))]
    public partial class OGroupBox : UserControl
    {
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
        private const int WM_SETREDRAW = 11;

        private String title;
        private int originalHeight = 256;
        private bool collapsed;
        private bool autoSize;

        public OGroupBox()
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

        public void SuspendDrawing()
        {
            SendMessage(Handle, WM_SETREDRAW, 0, 0);
        }

        public void ResumeDrawing()
        {
            SendMessage(Handle, WM_SETREDRAW, 1, 0);
            Refresh();
        }

        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Panel ContentArea
        {
            get { return ContentPanel; }
        }

        /// <summary>
        ///     Set to true to hide content, or false to show them.
        /// </summary>
        public bool Collapsed
        {
            get
            {
                return collapsed;
            }
            set
            {
                if (value) collapse(); else expand();
            }
        }

        /// <summary>
        ///     Set the expanded Height equal to the size of all controls.
        ///     It will NOT work if a Control uses the Dock Fill property, for obvious reasons.
        /// </summary>
        public bool AutomaticSize
        {
            get
            {
                return autoSize;
            }
            set
            {
                autoSize = value;
                recalc();
            }
        }

        /// <summary>
        ///     GroupBox Title text.
        /// </summary>
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
                updateTitle();
            }
        }

        /// <summary>
        ///     BackColor of the inner Panel.
        /// </summary>
        public Color ContentColor
        {
            get
            {
                return ContentPanel.BackColor;
            }
            set
            {
                ContentPanel.BackColor = value;
            }
        }

        /// <summary>
        ///     The Height of the control content when it is expanded.
        ///     It's not necessary to set this when AutomaticSize is enabled.
        /// </summary>
        public int ExpandedHeight
        {
            get
            {
                return originalHeight - 17;
            }
            set
            {
                originalHeight = value + 17;
            }
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            e.Control.Layout += Control_Layout;

            base.OnControlAdded(e);
        }

        private void Control_Layout(Object sender, EventArgs e)
        {
            recalc();
        }

        private void recalc()
        {
            if (autoSize)
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
                originalHeight = maxY + 17; //16 = Top, 1 = Bottom border
                if (!collapsed) Height = originalHeight;
            }
        }

        /// <summary>
        ///     Forces the size to be recalculated, if AutomaticSize is enabled.
        /// </summary>
        public void recalculateSize()
        {
            recalc();
        }

        private void OGroupBox_Layout(object sender, LayoutEventArgs e)
        {
            ContentPanel.Size = new Size(Width - 2, Height - 17);
            BtnToggle.Location = new Point(Width - 16, 0);
            updateTitle();
        }

        private void OGroupBox_EnabledChanged(object sender, EventArgs e)
        {
            BtnToggle.Visible = Enabled;
        } 

        private void BtnToggle_MouseEnter(object sender, EventArgs e)
        {
            BtnToggle.BackColor = ColorManager.highlight;
        }

        private void BtnToggle_MouseLeave(object sender, EventArgs e)
        {
            BtnToggle.BackColor = Color.Transparent;
        }

        private void BtnToggle_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            if (collapsed) expand(); else collapse();
        }

        private void updateTitle()
        {
            using (Graphics g = Graphics.FromHwnd(Handle))
            {
                LblTitle.Text = DrawingHelper.clampText(g, title, LblTitle.Font, Width - 16);
            }
        }

        private void expand()
        {
            recalc();
            collapsed = false;
            BtnToggle.Image = Properties.Resources.icn_collapse;
            Height = originalHeight;
        }

        private void collapse()
        {
            collapsed = true;
            BtnToggle.Image = Properties.Resources.icn_expand;
            Height = 16;
        }

        public class OGroupBoxDesigner : ParentControlDesigner
        {
            public override void Initialize(System.ComponentModel.IComponent component)
            {
                base.Initialize(component);

                if (this.Control is OGroupBox)
                {
                    this.EnableDesignMode(((OGroupBox)this.Control).ContentArea, "ContentArea");
                }
            }
        }
    }
}
