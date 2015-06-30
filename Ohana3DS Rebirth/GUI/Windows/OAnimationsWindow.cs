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

        bool pausedSA = true;
        bool pausedMA = true;

        bool animLoadedSA;
        bool animLoadedMA;

        public OAnimationsWindow()
        {
            InitializeComponent();
            Height = 400;
        }

        public void initialize(RenderEngine renderEngine)
        {
            renderer = renderEngine;
            updateListSA();
            updateListMA();
        }

        private void Control_MouseEnter(object sender, EventArgs e)
        {
            ((Control)sender).BackColor = Color.FromArgb(0x7f, ColorManager.hover);
        }

        private void Control_MouseLeave(object sender, EventArgs e)
        {
            ((Control)sender).BackColor = Color.Transparent;
        }

        #region "Skeletal Animations"
        private void updateListSA()
        {
            AnimationsListSA.flush();
            if (renderer != null)
            {
                renderer.loadSkeletalAnimation(-1);

                foreach (RenderBase.OSkeletalAnimation animation in renderer.model.skeletalAnimation)
                {
                    AnimationsListSA.addItem(animation.name);
                }
            }
            AnimationsListSA.Refresh();
            if (renderer.model.skeletalAnimation.Count == 0) SkeletalAnimationsGroup.collapse();
        }

        private void BtnPlayPauseSA_MouseDown(object sender, MouseEventArgs e)
        {
            if (!animLoadedSA || e.Button != MouseButtons.Left) return;
            if (pausedSA)
            {
                BtnPlayPauseSA.Image = Properties.Resources.icn_big_pause;
                pausedSA = false;
                renderer.playSkeletalAnimation();
            }
            else
            {
                BtnPlayPauseSA.Image = Properties.Resources.icn_big_play;
                pausedSA = true;
                renderer.pauseSkeletalAnimation();
            }
        }

        private void BtnStopSA_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            BtnPlayPauseSA.Image = Properties.Resources.icn_big_play;
            pausedSA = true;
            renderer.stopSkeletalAnimation();
        }

        private void BtnNextSA_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            renderer.loadSkeletalAnimation(renderer.currentSkeletalAnimationIndex + 1);
            AnimationsListSA.SelectedIndex++;
        }

        private void BtnPreviousSA_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            if (renderer.currentSkeletalAnimationIndex > 0) renderer.loadSkeletalAnimation(renderer.currentSkeletalAnimationIndex - 1);
            AnimationsListSA.SelectedIndex--;
        }

        private void AnimationsListSA_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AnimationsListSA.SelectedIndex == -1) return;
            animLoadedSA = renderer.loadSkeletalAnimation(AnimationsListSA.SelectedIndex);
        }

        private void BtnImportSA_Click(object sender, EventArgs e)
        {
            Object importedData = FileImporter.import(FileImporter.importFileType.skeletalAnimation);
            if (importedData != null)
            {
                renderer.model.addSekeletalAnimaton((List<RenderBase.OSkeletalAnimation>)importedData);
                updateListSA();
            }
        }

        private void BtnDeleteSA_Click(object sender, EventArgs e)
        {
            if (AnimationsListSA.SelectedIndex == -1) return;
            renderer.model.skeletalAnimation.RemoveAt(AnimationsListSA.SelectedIndex);
            AnimationsListSA.removeItem(AnimationsListSA.SelectedIndex);
        }

        private void BtnClearSA_Click(object sender, EventArgs e)
        {
            renderer.stopSkeletalAnimation();
            animLoadedSA = false;
            BtnPlayPauseSA.Image = Properties.Resources.icn_big_play;
            pausedSA = true;
            renderer.model.skeletalAnimation.Clear();
            updateListSA();
        }
        #endregion

        #region "Material Animations"
        private void updateListMA()
        {
            AnimationsListMA.flush();
            if (renderer != null)
            {
                renderer.loadMaterialAnimation(-1);

                foreach (RenderBase.OMaterialAnimation animation in renderer.model.materialAnimation)
                {
                    AnimationsListMA.addItem(animation.name);
                }
            }
            AnimationsListMA.Refresh();
            if (renderer.model.materialAnimation.Count == 0) MaterialAnimationsGroup.collapse();
        }

        private void BtnPlayPauseMA_MouseDown(object sender, MouseEventArgs e)
        {
            if (!animLoadedMA || e.Button != MouseButtons.Left) return;
            if (pausedMA)
            {
                BtnPlayPauseMA.Image = Properties.Resources.icn_big_pause;
                pausedMA = false;
                renderer.playMaterialAnimation();
            }
            else
            {
                BtnPlayPauseMA.Image = Properties.Resources.icn_big_play;
                pausedMA = true;
                renderer.pauseMaterialAnimation();
            }
        }

        private void BtnStopMA_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            BtnPlayPauseMA.Image = Properties.Resources.icn_big_play;
            pausedMA = true;
            renderer.stopMaterialAnimation();
        }

        private void BtnNextMA_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            renderer.loadMaterialAnimation(renderer.currentMaterialAnimationIndex + 1);
            AnimationsListMA.SelectedIndex++;
        }

        private void BtnPreviousMA_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            if (renderer.currentMaterialAnimationIndex > 0) renderer.loadMaterialAnimation(renderer.currentMaterialAnimationIndex - 1);
            AnimationsListMA.SelectedIndex--;
        }

        private void AnimationsListMA_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AnimationsListMA.SelectedIndex == -1) return;
            animLoadedMA = renderer.loadMaterialAnimation(AnimationsListMA.SelectedIndex);
        }

        private void BtnImportMA_Click(object sender, EventArgs e)
        {
            Object importedData = FileImporter.import(FileImporter.importFileType.materialAnimation);
            if (importedData != null)
            {
                renderer.model.addMaterialAnimation((List<RenderBase.OMaterialAnimation>)importedData);
                updateListMA();
            }
        }

        private void BtnDeleteMA_Click(object sender, EventArgs e)
        {
            if (AnimationsListMA.SelectedIndex == -1) return;
            renderer.model.materialAnimation.RemoveAt(AnimationsListMA.SelectedIndex);
            AnimationsListMA.removeItem(AnimationsListMA.SelectedIndex);
        }

        private void BtnClearMA_Click(object sender, EventArgs e)
        {
            renderer.stopMaterialAnimation();
            animLoadedMA = false;
            BtnPlayPauseMA.Image = Properties.Resources.icn_big_play;
            pausedMA = true;
            renderer.model.materialAnimation.Clear();
            updateListMA();
        }
        #endregion

    }
}
