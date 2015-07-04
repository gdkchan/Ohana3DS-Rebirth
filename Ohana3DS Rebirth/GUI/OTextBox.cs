using System;
using System.Drawing;
using System.Windows.Forms;

namespace Ohana3DS_Rebirth.GUI
{
    public partial class OTextBox : Control
    {
        CustomTextBox textBox = new CustomTextBox();

        public event EventHandler ChangedText;

        public OTextBox()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            InitializeComponent();
            Controls.Add(textBox);
            textBox.TextChanged += TextBox_TextChanged;
        }

        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
                if (value != Color.Transparent) textBox.BackColor = value;
            }
        }

        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;
                textBox.ForeColor = value;
            }
        }

        public override string Text
        {
            get
            {
                return textBox.Text;
            }
            set
            {
                textBox.Text = value;
            }
        }

        public string CharacterWhiteList
        {
            get
            {
                return textBox.CharacterWhiteList;
            }
            set
            {
                textBox.CharacterWhiteList = value;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Color lineColor = ForeColor;
            if (!Enabled) lineColor = SystemColors.InactiveCaptionText;
            e.Graphics.DrawLine(new Pen(lineColor), new Point(0, Height - 1), new Point(Width - 1, Height - 1));
            e.Graphics.DrawLine(new Pen(lineColor), new Point(0, Height - 1), new Point(0, Height - 2));
            e.Graphics.DrawLine(new Pen(lineColor), new Point(Width - 1, Height - 1), new Point(Width - 1, Height - 2));
            base.OnPaint(e);
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            textBox.Location = new Point(2, ((Height - 1) / 2) - (textBox.Height / 2));
            textBox.Size = new Size(Width - 4, textBox.Height);
            base.OnLayout(levent);
        }

        private void TextBox_TextChanged(Object sender, EventArgs e)
        {
            if (ChangedText != null) ChangedText(this, EventArgs.Empty);
        }

        private class CustomTextBox : TextBox
        {
            string charWhiteList;

            public CustomTextBox()
            {
                BackColor = Color.Black;
                ForeColor = Color.WhiteSmoke;
                BorderStyle = BorderStyle.None;
            }

            public string CharacterWhiteList
            {
                get
                {
                    return charWhiteList;
                }
                set
                {
                    charWhiteList = value;
                }
            }

            protected override void OnKeyPress(KeyPressEventArgs e)
            {
                e.Handled = false;
                if (charWhiteList != null)
                {
                    if (!charWhiteList.Contains(e.KeyChar.ToString()) && !Char.IsControl(e.KeyChar)) e.Handled = true;
                }

                base.OnKeyPress(e);
            }
        }
    }
}
