using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Ohana3DS_Rebirth.Properties;

namespace Ohana3DS_Rebirth.GUI
{
    public partial class OComboBox : UserControl
    {
        private int listHeight = 256;
        private bool toggle, autoSize;
        private string[] items;

        public event EventHandler SelectedIndexChanged;

        /// <summary>
        ///     Height of the DropDown list.
        /// </summary>
        public int ListHeight
        {
            get
            {
                return listHeight;
            }
            set
            {
                listHeight = value;
            }
        }

        /// <summary>
        ///     Height of the OComboBox bar (when the list is collapsed).
        /// </summary>
        public int BarHeight
        {
            get
            {
                return topBox.Height;
            }
            set
            {
                topBox.Height = value;
                Height = value;
            }
        }

        public Font ListFont
        {
            get
            {
                return list.Font;
            }
            set
            {
                list.Font = value;
            }
        }

        public Font BarFont
        {
            get
            {
                return LblSelectedItem.Font;
            }
            set
            {
                LblSelectedItem.Font = value;
            }
        }

        /// <summary>
        ///     Ignores the ListHeight and calculate the Height of the DropDown list.
        ///     The Height will be the Num. of Items * Height of each Item.
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
            }
        }
        
        /// <summary>
        ///     Array with all items on the List.
        /// </summary>
        public string[] Items
        {
            get
            {
                return items;
            }
            set
            {
                items = value;
                list.flush();
                list.addRange(items);
                LblSelectedItem.Text = list.itemAt(list.SelectedIndex);
            }
        }

        public override string Text
        {
            get
            {
                return LblSelectedItem.Text;
            }
            set
            {
                LblSelectedItem.Text = value;
            }
        }

        public int SelectedIndex
        {
            get
            {
                return list.SelectedIndex;
            }
            set
            {
                list.SelectedIndex = value;
            }
        }

        public OComboBox()
        {
            InitializeComponent();
        }

        private void OComboBox_EnabledChanged(object sender, EventArgs e)
        {
            BtnToggle.Visible = Enabled;
        }

        private void BtnToggle_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (toggle)
                {
                    list.Visible = false;
                    Height = topBox.Height;
                    BtnToggle.Image = Resources.ui_down;
                }
                else
                {
                    list.Visible = true;
                    Height = (autoSize ? list.Count * list.ItemHeight : listHeight) + topBox.Height;
                    BtnToggle.Image = Resources.ui_up;
                }

                toggle = !toggle;
            }
        }

        private void list_SelectedIndexChanged(object sender, EventArgs e)
        {
            LblSelectedItem.Text = list.itemAt(list.SelectedIndex);
            if (SelectedIndexChanged != null) SelectedIndexChanged(this, EventArgs.Empty);
        }
    }
}
