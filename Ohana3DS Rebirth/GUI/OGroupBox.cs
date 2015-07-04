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
        ///     Total Height of the control when it is expanded.
        /// </summary>
        public int ExpandedHeight
        {
            get
            {
                return originalHeight;
            }
            set
            {
                originalHeight = value;
            }
        }

        private void OGroupBox_Layout(object sender, LayoutEventArgs e)
        {
            ContentPanel.Size = new Size(Width - 2, Height - 17);
            BtnToggle.Location = new Point(Width - 16, 0);
            updateTitle();
        }

        private void BtnToggle_MouseEnter(object sender, EventArgs e)
        {
            BtnToggle.BackColor = Color.FromArgb(0x7f, ColorManager.hover);
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
            LblTitle.Text = DrawingHelper.clampText(title, LblTitle.Font, Width - 16);
        }

        private void expand()
        {
            BtnToggle.Image = Properties.Resources.icn_collapse;
            Height = originalHeight;
            collapsed = false;
        }

        private void collapse()
        {
            BtnToggle.Image = Properties.Resources.icn_expand;
            Height = 16;
            collapsed = true;
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
