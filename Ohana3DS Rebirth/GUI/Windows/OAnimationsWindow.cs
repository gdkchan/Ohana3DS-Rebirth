using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

using Ohana3DS_Rebirth.Ohana;

namespace Ohana3DS_Rebirth.GUI
{
    public partial class OAnimationsWindow : ODockWindow
    {
        RenderEngine renderer;
        private bool paused = true;
        private bool animLoaded = false;

        public OAnimationsWindow()
        {
            InitializeComponent();
        }

        public void initialize(RenderEngine renderEngine)
        {
            renderer = renderEngine;
            updateList();
        }

        private void updateList()
        {
            AnimationsListSA.flush();
            if (renderer != null)
            {
                renderer.loadAnimation(-1);
                foreach (RenderBase.OSkeletalAnimation animation in renderer.animations)
                {
                    AnimationsListSA.addItem(animation.name);
                }
            }
        }

        private void Control_MouseEnter(object sender, EventArgs e)
        {
            ((Control)sender).BackColor = Color.FromArgb(0x7f, ColorManager.hover);
        }

        private void Control_MouseLeave(object sender, EventArgs e)
        {
            ((Control)sender).BackColor = Color.Transparent;
        }

        private void BtnPlayPauseSA_MouseDown(object sender, MouseEventArgs e)
        {
            if (!animLoaded || e.Button != MouseButtons.Left) return;
            if (paused)
            {
                BtnPlayPauseSA.Image = Properties.Resources.icn_big_pause;
                paused = false;
                renderer.playAnimation();
            }
            else
            {
                BtnPlayPauseSA.Image = Properties.Resources.icn_big_play;
                paused = true;
                renderer.pauseAnimation();
            }
        }

        private void BtnStopSA_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            BtnPlayPauseSA.Image = Properties.Resources.icn_big_play;
            paused = true;
            renderer.stopAnimation();
        }

        private void BtnNextSA_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            renderer.loadAnimation(renderer.currentAnimationIndex + 1);
            AnimationsListSA.SelectedIndex++;
        }

        private void BtnPreviousSA_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            if (renderer.currentAnimationIndex > 0) renderer.loadAnimation(renderer.currentAnimationIndex - 1);
            AnimationsListSA.SelectedIndex--;
        }

        private void AnimationsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AnimationsListSA.SelectedIndex == -1) return;
            animLoaded = renderer.loadAnimation(AnimationsListSA.SelectedIndex);
        }

        private void BtnImportSA_Click(object sender, EventArgs e)
        {
            Object importedData = FileImporter.import(FileImporter.importFileType.skeletalAnimation);
            if (importedData != null)
            {
                renderer.model.addSekeletalAnimaton((List<RenderBase.OSkeletalAnimation>)importedData);
                updateList();
            }
        }

        private void BtnClearSA_Click(object sender, EventArgs e)
        {
            renderer.stopAnimation();
            animLoaded = false;
            BtnPlayPauseSA.Image = Properties.Resources.icn_big_play;
            paused = true;
            renderer.model.skeletalAnimation.Clear();
            updateList();
        }
    }
}
