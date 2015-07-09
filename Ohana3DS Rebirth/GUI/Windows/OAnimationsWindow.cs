using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

using Ohana3DS_Rebirth.Ohana;

namespace Ohana3DS_Rebirth.GUI
{
    public partial class OAnimationsWindow : ODockWindow
    {
        public OAnimationsWindow()
        {
            InitializeComponent();
            Height = 400;
        }

        public void initialize(RenderEngine renderer)
        {
            if (renderer.model.skeletalAnimation.list.Count == 0) SkeletalAnimationsGroup.Collapsed = true;
            if (renderer.model.materialAnimation.list.Count == 0) MaterialAnimationsGroup.Collapsed = true;

            SkeletalAnimationControl.initialize(renderer, renderer.ctrlSA, renderer.model.skeletalAnimation, FileIO.fileType.skeletalAnimation);
            MaterialAnimationControl.initialize(renderer, renderer.ctrlMA, renderer.model.materialAnimation, FileIO.fileType.materialAnimation);
        }
    }
}
