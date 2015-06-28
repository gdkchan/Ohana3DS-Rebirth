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
    [Designer(typeof(OGroupBoxDesigner))]
    public partial class OGroupBox : UserControl
    {
        private String title;
        private Size originalSize;
        private bool toggle;

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

        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Panel ContentArea
        {
            get { return ContentPanel; }
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

        private void updateTitle()
        {
            LblTitle.Text = DrawingHelper.clampText(title, LblTitle.Font, Width - 16);
        }

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

        private void BtnToggle_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            if (toggle) expand(); else collapse();
            toggle = !toggle;
        }

        /// <summary>
        ///     Expands the GroupBox, so the content is accesible.
        /// </summary>
        public void expand()
        {
            BtnToggle.Image = Properties.Resources.icn_collapse;
            this.Size = originalSize;
        }

        /// <summary>
        ///     Collapses the GroupBox, so the content is not accesible and less space is used.
        /// </summary>
        public void collapse()
        {
            BtnToggle.Image = Properties.Resources.icn_expand;
            originalSize = this.Size;
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
