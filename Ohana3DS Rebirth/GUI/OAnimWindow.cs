using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Ohana3DS_Rebirth.Ohana;

namespace Ohana3DS_Rebirth.GUI
{
    public partial class OAnimWindow : ODockWindow
    {
        RenderEngine re = new RenderEngine();

        public OAnimWindow()
        {
            InitializeComponent();
        }

        private void oButtonPlay_Click(object sender, EventArgs e)
        {
            re.playAnimation();
        }

        private void animNumBox_ValueChanged(object sender, EventArgs e)
        {
            re.loadAnimation((int)animNumBox.Value);
        }

        private void oButtonLoad_Click(object sender, EventArgs e)
        {
            openAnimDialog.Filter = "Binary CTR H3D File|*.bch";
            if(openAnimDialog.ShowDialog() == DialogResult.OK){
                //RenderBase.OModelGroup anim = Ohana.BCH.load(openAnimDialog.FileName);
                //ModelData.inst.model.skeletalAnimation = anim.skeletalAnimation;
                oButtonPlay.Enabled = true;
                animNumBox.Enabled = true;
            }
        }
    }
}
