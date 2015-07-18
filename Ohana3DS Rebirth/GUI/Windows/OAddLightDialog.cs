using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Ohana3DS_Rebirth.Ohana;

namespace Ohana3DS_Rebirth.GUI
{
    public partial class OAddLightDialog : OForm
    {
        public class OAddLightEventArgs : EventArgs
        {
            public struct responseStruct
            {
                public bool ok;
                public string lightName;
                public RenderBase.OLightUse lightUse;
            }
            public responseStruct response;

            public OAddLightEventArgs(bool ok, string lightName, RenderBase.OLightUse lightUse)
            {
                response.ok = ok;
                response.lightName = lightName;
                response.lightUse = lightUse;
            }

            public OAddLightEventArgs(bool ok)
            {
                response.ok = ok;
            }
        }

        public event EventHandler<OAddLightEventArgs> DialogCallback;

        public OAddLightDialog()
        {
            InitializeComponent();

            LstLightType.addItem("Fragment");
            LstLightType.addItem("Vertex");
            LstLightType.addItem("Ambient");
            LstLightType.addItem("Hemisphere");
            LstLightType.SelectedIndex = 0;
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            RenderBase.OLightUse lightUse;
            switch (LstLightType.SelectedIndex)
            {
                case 0: lightUse = RenderBase.OLightUse.fragment; break;
                case 1: lightUse = RenderBase.OLightUse.vertex; break;
                case 2: lightUse = RenderBase.OLightUse.ambient; break;
                case 3: lightUse = RenderBase.OLightUse.hemiSphere; break;
                default:
                    MessageBox.Show("Please select the light type!", "No light type", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
            }
            DialogCallback(this, new OAddLightEventArgs(true, TxtLightName.Text, (RenderBase.OLightUse)lightUse));
            Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogCallback(this, new OAddLightEventArgs(false));
            Close();
        }
    }
}
