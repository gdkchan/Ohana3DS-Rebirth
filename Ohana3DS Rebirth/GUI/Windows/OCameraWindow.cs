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
    public partial class OCameraWindow : ODockWindow
    {
        RenderEngine renderer;

        public OCameraWindow()
        {
            InitializeComponent();
            NameGroup.Collapsed = true;
            TransformGroup.Collapsed = true;
            ViewGroup.Collapsed = true;
            ProjectionGroup.Collapsed = true;
            Height = 360;
        }

        public void initialize(RenderEngine renderEngine)
        {
            renderer = renderEngine;
        }

        private void RadioPersp_CheckedChanged(object sender, EventArgs e)
        {
            ProjectionGroup.SuspendDrawing();
            PPFovy.Enabled = true;
            OPHeight.Enabled = false;
            ProjectionGroup.ResumeDrawing();
        }

        private void RadioOrtho_CheckedChanged(object sender, EventArgs e)
        {
            ProjectionGroup.SuspendDrawing();
            PPFovy.Enabled = false;
            OPHeight.Enabled = true;
            ProjectionGroup.ResumeDrawing();
        }

        private void RadioVAT_CheckedChanged(object sender, EventArgs e)
        {
            ViewGroup.SuspendDrawing();
            TargetX.Enabled = true;
            TargetY.Enabled = true;
            TargetZ.Enabled = true;
            RRotX.Enabled = false;
            RRotY.Enabled = false;
            RRotZ.Enabled = false;
            LAUpVecX.Enabled = false;
            LAUpVecY.Enabled = false;
            LAUpVecZ.Enabled = false;
            ATwist.Enabled = true;
            ViewGroup.ResumeDrawing();
        }

        private void RadioVLAT_CheckedChanged(object sender, EventArgs e)
        {
            ViewGroup.SuspendDrawing();
            TargetX.Enabled = true;
            TargetY.Enabled = true;
            TargetZ.Enabled = true;
            RRotX.Enabled = false;
            RRotY.Enabled = false;
            RRotZ.Enabled = false;
            LAUpVecX.Enabled = true;
            LAUpVecY.Enabled = true;
            LAUpVecZ.Enabled = true;
            ATwist.Enabled = false;
            ViewGroup.ResumeDrawing();
        }

        private void RadioVR_CheckedChanged(object sender, EventArgs e)
        {
            ViewGroup.SuspendDrawing();
            TargetX.Enabled = false;
            TargetY.Enabled = false;
            TargetZ.Enabled = false;
            RRotX.Enabled = true;
            RRotY.Enabled = true;
            RRotZ.Enabled = true;
            LAUpVecX.Enabled = false;
            LAUpVecY.Enabled = false;
            LAUpVecZ.Enabled = false;
            ATwist.Enabled = false;
            ViewGroup.ResumeDrawing();
        }
    }
}
