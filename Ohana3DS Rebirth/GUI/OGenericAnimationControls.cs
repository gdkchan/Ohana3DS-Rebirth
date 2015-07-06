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
    public partial class OGenericAnimationControls : UserControl
    {
        private RenderBase.OAnimationListBase animations;
        private RenderEngine.animationControl control;
        private FileImporter.importFileType importType;

        private bool paused = true;
        private bool isAnimationLoaded;

        public OGenericAnimationControls()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Initializes the control with animation data.
        /// </summary>
        /// <param name="anms">The list with animations</param>
        /// <param name="ctrl">The animation control</param>
        /// <param name="type">The animation type (used on Import option)</param>
        public void initialize(RenderBase.OAnimationListBase anms, RenderEngine.animationControl ctrl, FileImporter.importFileType type)
        {
            animations = anms;
            control = ctrl;
            importType = type;
            control.FrameChanged += Control_FrameChanged;
            updateList();
        }

        private void updateList()
        {
            AnimationsList.flush();
            if (control != null)
            {
                control.load(-1);

                foreach (RenderBase.OAnimationBase animation in animations.list)
                {
                    AnimationsList.addItem(animation.name);
                }
            }
            AnimationsList.Refresh();
        }

        private void Control_MouseEnter(object sender, EventArgs e)
        {
            ((Control)sender).BackColor = Color.FromArgb(0x7f, ColorManager.hover);
        }

        private void Control_MouseLeave(object sender, EventArgs e)
        {
            ((Control)sender).BackColor = Color.Transparent;
        }

        private void BtnPlayPause_MouseDown(object sender, MouseEventArgs e)
        {
            if (!isAnimationLoaded || e.Button != MouseButtons.Left) return;
            if (paused)
            {
                BtnPlayPause.Image = Properties.Resources.icn_big_pause;
                paused = false;
                control.play();
            }
            else
            {
                BtnPlayPause.Image = Properties.Resources.icn_big_play;
                paused = true;
                control.pause();
            }
        }

        private void BtnStop_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            BtnPlayPause.Image = Properties.Resources.icn_big_play;
            paused = true;
            control.stop();
        }

        private void BtnPrevious_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            if (control.CurrentAnimation > 0) control.load(control.CurrentAnimation - 1);
            AnimationsList.SelectedIndex--;
        }

        private void BtnNext_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            if (control.CurrentAnimation < animations.list.Count - 1) control.load(control.CurrentAnimation + 1);
            AnimationsList.SelectedIndex++;
        }

        private void Control_FrameChanged(object sender, EventArgs e)
        {
            if (!Seeker.ManualSeeking && !paused) Seeker.Value = (int)control.Frame;
        }

        private void AnimationsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AnimationsList.SelectedIndex == -1) return;
            isAnimationLoaded = control.load(AnimationsList.SelectedIndex);
            Seeker.MaximumSeek = (int)animations.list[AnimationsList.SelectedIndex].frameSize;
            Seeker.Value = 0;
        }

        private void Seeker_Seek(object sender, EventArgs e)
        {
            control.Frame = Seeker.Value;
        }

        private void Seeker_SeekStart(object sender, EventArgs e)
        {
            control.pause();
        }

        private void Seeker_SeekEnd(object sender, EventArgs e)
        {
            if (!paused) control.play();
        }

        private void Speed_Seek(object sender, EventArgs e)
        {
            control.animationStep = (float)Speed.Value / 100;
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            //TODO
        }

        private void BtnImport_Click(object sender, EventArgs e)
        {
            RenderBase.OAnimationListBase animation = (RenderBase.OAnimationListBase)FileImporter.import(importType);
            if (animation != null)
            {
                animations.list.AddRange(animation.list);
                foreach (RenderBase.OAnimationBase anim in animation.list) AnimationsList.addItem(anim.name);
                AnimationsList.Refresh();
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (AnimationsList.SelectedIndex == -1) return;
            animations.list.RemoveAt(AnimationsList.SelectedIndex);
            AnimationsList.removeItem(AnimationsList.SelectedIndex);
            if (animations.list.Count == 0)
            {
                control.stop();
                isAnimationLoaded = false;
                BtnPlayPause.Image = Properties.Resources.icn_big_play;
                paused = true;
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            control.stop();
            isAnimationLoaded = false;
            BtnPlayPause.Image = Properties.Resources.icn_big_play;
            paused = true;
            animations.list.Clear();
            updateList();
        }
    }
}
