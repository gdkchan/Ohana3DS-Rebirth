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
        private bool pause = false;

        public OAnimWindow()
        {
            InitializeComponent();
            oButtonPause.BackgroundImage = Properties.Resources.play;
            oButtonStop.BackgroundImage = Properties.Resources.stop;
        }

        public void initialize(RenderEngine renderEngine)
        {
            renderer = renderEngine;
        }

        private void oButtonPlay_Click(object sender, EventArgs e)
        {
            oButtonPause.BackgroundImage = Properties.Resources.pause;
            renderer.loadAnimation((int)animNumBox.Value);
            renderer.playAnimation();
        }

        private void oButtonPause_Click(object sender, EventArgs e)
        {
            if(pause){
                oButtonPause.BackgroundImage = Properties.Resources.pause;
                pause = false;
                renderer.playAnimation();
            }
            else
            {
                oButtonPause.BackgroundImage = Properties.Resources.play;
                pause = true;
                renderer.pauseAnimation();
            }
        }

        private void oButtonStop_Click(object sender, EventArgs e)
        {
            oButtonPause.BackgroundImage = Properties.Resources.play;
            pause = true;
            renderer.stopAnimation();
        }

        private void oButtonLoad_Click(object sender, EventArgs e)
        {
            openAnimDialog.Filter = "Binary CTR H3D File|*.bch";
            if(openAnimDialog.ShowDialog() == DialogResult.OK){
                RenderBase.OModelGroup animation = Ohana.BCH.load(openAnimDialog.FileName);
                renderer.model.skeletalAnimation = animation.skeletalAnimation;
                oButtonPlay.Enabled = true;
                oButtonPause.Enabled = true;
                oButtonStop.Enabled = true;
                animNumBox.Enabled = true;
            }
        }
    }
}
