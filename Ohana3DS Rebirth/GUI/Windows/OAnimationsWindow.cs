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

        public OAnimationsWindow()
        {
            InitializeComponent();
            Height = 400;
        }

        public void initialize(RenderEngine renderEngine)
        {
            renderer = renderEngine;

            if (renderer.model.skeletalAnimation.list.Count == 0) SkeletalAnimationsGroup.Collapsed = true;
            if (renderer.model.materialAnimation.list.Count == 0) MaterialAnimationsGroup.Collapsed = true;
            
            SkeletalAnimationControl.initialize(renderer.model.skeletalAnimation, renderer.ctrlSA, FileImporter.importFileType.skeletalAnimation);
            MaterialAnimationControl.initialize(renderer.model.materialAnimation, renderer.ctrlMA, FileImporter.importFileType.materialAnimation);
        }
    }
}
