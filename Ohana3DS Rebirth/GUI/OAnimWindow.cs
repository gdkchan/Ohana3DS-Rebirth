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
        RenderEngine renderer;

        public OAnimWindow()
        {
            InitializeComponent();
        }

        public void initialize(RenderEngine renderEngine)
        {
            renderer = renderEngine;
        }

        private void oButtonPlay_Click(object sender, EventArgs e)
        {
            renderer.playAnimation();
        }

        private void animNumBox_ValueChanged(object sender, EventArgs e)
        {
            renderer.loadAnimation((int)animNumBox.Value);
        }

        private void oButtonLoad_Click(object sender, EventArgs e)
        {
            openAnimDialog.Filter = "Binary CTR H3D File|*.bch";
            if(openAnimDialog.ShowDialog() == DialogResult.OK){
                RenderBase.OModelGroup animation = Ohana.BCH.load(openAnimDialog.FileName);
                renderer.model.skeletalAnimation = animation.skeletalAnimation;
                oButtonPlay.Enabled = true;
                animNumBox.Enabled = true;
            }
        }
    }
}
