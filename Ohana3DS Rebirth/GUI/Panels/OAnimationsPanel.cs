using System;
using System.Drawing;
using System.Windows.Forms;

using Ohana3DS_Rebirth.Ohana;

namespace Ohana3DS_Rebirth.GUI
{
    public partial class OAnimationsPanel : UserControl, IPanel
    {
        private RenderEngine renderer;
        private RenderEngine.animationControl control;
        private RenderBase.OAnimationListBase animations;
        private FileIO.fileType type;

        private bool paused = true;
        private bool isAnimationLoaded;

        public OAnimationsPanel()
        {
            InitializeComponent();
        }

        public void launch(object data)
        {
            RenderBase.OModelGroup group = (RenderBase.OModelGroup)data;
            animations = new RenderBase.OAnimationListBase();

            foreach (RenderBase.OAnimationBase mAnim in group.materialAnimation.list)
            {
                animations.list.Add(mAnim);
            }

            foreach (RenderBase.OAnimationBase sAnim in group.skeletalAnimation.list)
            {
                animations.list.Add(sAnim);
            }

            foreach (RenderBase.OAnimationBase vAnim in group.visibilityAnimation.list)
            {
                animations.list.Add(vAnim);
            }

            updateList();
        }

        public void finalize()
        {
            AnimationsList.flush();
        }

        public void launch(RenderEngine renderEngine, FileIO.fileType type)
        {
            renderer = renderEngine;
            this.type = type;
            switch (type)
            {
                case FileIO.fileType.skeletalAnimation:
                    control = renderer.ctrlSA;
                    animations = renderer.models.skeletalAnimation;
                    break;
                case FileIO.fileType.materialAnimation:
                    control = renderer.ctrlMA;
                    animations = renderer.models.materialAnimation;
                    break;
                case FileIO.fileType.visibilityAnimation:
                    control = renderer.ctrlVA;
                    animations = renderer.models.visibilityAnimation;
                    break;
            }
            
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
            ((Control)sender).BackColor = ColorManager.ui_hoveredDark;
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
                BtnPlayPause.Image = Properties.Resources.ui_icon_pause;
                paused = false;
                control.play();
            }
            else
            {
                BtnPlayPause.Image = Properties.Resources.ui_icon_play;
                paused = true;
                control.pause();
            }
        }

        private void BtnStop_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            BtnPlayPause.Image = Properties.Resources.ui_icon_play;
            control.stop();
            paused = true;
            Seeker.Value = 0;
        }

        private void BtnPrevious_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            if (control.CurrentAnimation > 0)
            {
                control.load(control.CurrentAnimation - 1);
                AnimationsList.SelectedIndex--;
            }
        }

        private void BtnNext_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            if (control.CurrentAnimation < animations.list.Count - 1)
            {
                control.load(control.CurrentAnimation + 1);
                AnimationsList.SelectedIndex++;
            }
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

        private void BtnImport_Click(object sender, EventArgs e)
        {
            RenderBase.OAnimationListBase animation = (RenderBase.OAnimationListBase)FileIO.import(type);
            if (animation != null)
            {
                animations.list.AddRange(animation.list);
                foreach (RenderBase.OAnimationBase anim in animation.list) AnimationsList.addItem(anim.name);
                AnimationsList.Refresh();
            }
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            switch (type)
            {
                case FileIO.fileType.skeletalAnimation:
                    if (renderer.CurrentModel == -1)
                    {
                        MessageBox.Show(
                            "A skeleton is necessary to export an Skeletal Animation." + Environment.NewLine +
                            "You must select a model before exporting!",
                            "Warning",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
                        return;
                    }
                    if (control.CurrentAnimation == -1) return;
                    FileIO.export(type, renderer.models, renderer.CurrentModel, control.CurrentAnimation);
                    break;
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
                BtnPlayPause.Image = Properties.Resources.ui_icon_play;
                paused = true;
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            control.stop();
            isAnimationLoaded = false;
            BtnPlayPause.Image = Properties.Resources.ui_icon_play;
            paused = true;

            animations.list.Clear();
            updateList();
        }
    }
}
