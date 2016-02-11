using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Ohana3DS_Rebirth.GUI
{
    public partial class OFloatTextBox : UserControl
    {
        uint decimalPlaces = 1;
        float minVal = -100.0f;
        float maxVal = 100.0f;
        float val = 0;
        bool seeking;

        public event EventHandler ValueChanged;

        public OFloatTextBox()
        {
            InitializeComponent();
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

                if (value != Color.Transparent)
                    TextBox.BackColor = value;
                else
                    TextBox.BackColor = Color.Black;
            }
        }

        public float Value
        {
            get
            {
                return val;
            }
            set
            {
                val = value;
                TextBox.Text = val.ToString(CultureInfo.InvariantCulture);
                updateSeekBar();
            }
        }

        public float MinimumValue
        {
            get
            {
                return minVal;
            }
            set
            {
                minVal = value;
                updateSeekBarMinMax();
            }
        }

        public float MaximumValue
        {
            get
            {
                return maxVal;
            }
            set
            {
                maxVal = value;
                updateSeekBarMinMax();
            }
        }

        public uint DecimalPlaces
        {
            get
            {
                return decimalPlaces;
            }
            set
            {
                decimalPlaces = value;
            }
        }

        private void SeekBar_Seek(object sender, EventArgs e)
        {
            val = (float)((double)SeekBar.Value / Math.Pow(10, decimalPlaces)) + minVal;
            TextBox.Text = val.ToString(CultureInfo.InvariantCulture);
            TextBox.Refresh();
            if (ValueChanged != null) ValueChanged(this, EventArgs.Empty);
        }

        private void SeekBar_SeekStart(object sender, EventArgs e)
        {
            seeking = true;
        }

        private void SeekBar_SeekEnd(object sender, EventArgs e)
        {
            seeking = false;
        }

        private void TextBox_ChangedText(object sender, EventArgs e)
        {
            if (seeking) return;
            float output;
            if (float.TryParse(TextBox.Text, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat, out output))
            {
                val = output;
                updateSeekBar();
                if (ValueChanged != null) ValueChanged(this, EventArgs.Empty);
            }
        }

        private void updateSeekBar()
        {
            if (val > maxVal)
                SeekBar.Value = SeekBar.MaximumSeek;
            else if (val < minVal)
                SeekBar.Value = 0;
            else
                SeekBar.Value = (int)Math.Round((val - minVal) * Math.Pow(10, decimalPlaces));
        }

        private void updateSeekBarMinMax()
        {
            SeekBar.MaximumSeek = (int)Math.Round((maxVal - minVal) * Math.Pow(10, decimalPlaces));
        }
    }
}
