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
        RenderBase.OCamera camera;

        public OCameraWindow()
        {
            InitializeComponent();
            NameGroup.Collapsed = true;
            TransformGroup.Collapsed = true;
            ViewGroup.Collapsed = true;
            ProjectionGroup.Collapsed = true;
            Height = 384;
        }

        public void initialize(RenderEngine renderEngine)
        {
            renderer = renderEngine;
            updateList();
        }

        private void updateList()
        {
            CameraList.flush();
            foreach (RenderBase.OCamera camera in renderer.model.camera) CameraList.addItem(camera.name);
            CameraList.Refresh();
        }

        private void RadioPersp_CheckedChanged(object sender, EventArgs e)
        {
            camera.projection = RenderBase.OCameraProjection.perspective;
            ProjectionGroup.SuspendDrawing();
            PPFovy.Enabled = true;
            OPHeight.Enabled = false;
            PPARatio.Enabled = true;
            BtnARTop.Enabled = true;
            BtnARBottom.Enabled = true;
            ProjectionGroup.ResumeDrawing();
        }

        private void RadioOrtho_CheckedChanged(object sender, EventArgs e)
        {
            camera.projection = RenderBase.OCameraProjection.orthogonal;
            ProjectionGroup.SuspendDrawing();
            PPFovy.Enabled = false;
            OPHeight.Enabled = true;
            PPARatio.Enabled = false;
            BtnARTop.Enabled = false;
            BtnARBottom.Enabled = false;
            ProjectionGroup.ResumeDrawing();
        }

        private void RadioVAT_CheckedChanged(object sender, EventArgs e)
        {
            camera.view = RenderBase.OCameraView.aimTarget;
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
            camera.view = RenderBase.OCameraView.lookAtTarget;
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
            camera.view = RenderBase.OCameraView.rotate;
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

        private void CameraList_SelectedIndexChanged(object sender, EventArgs e)
        {
            camera = null;
            renderer.currentCamera = CameraList.SelectedIndex;
            if (CameraList.SelectedIndex == -1) return;

            camera = renderer.model.camera[CameraList.SelectedIndex];

            //Name
            TxtCameraName.Text = camera.name;

            //Transform
            TTransX.Value = camera.transformTranslate.x;
            TTransY.Value = camera.transformTranslate.y;
            TTransZ.Value = camera.transformTranslate.z;

            //View
            switch (camera.view)
            {
                case RenderBase.OCameraView.aimTarget: RadioVAT.PerformClick(); break;
                case RenderBase.OCameraView.lookAtTarget: RadioVLAT.PerformClick(); break;
                case RenderBase.OCameraView.rotate: RadioVR.PerformClick(); break;
            }
            TargetX.Value = camera.target.x;
            TargetY.Value = camera.target.y;
            TargetZ.Value = camera.target.z;

            RRotX.Value = camera.rotation.x;
            RRotY.Value = camera.rotation.y;
            RRotZ.Value = camera.rotation.z;

            LAUpVecX.Value = camera.upVector.x;
            LAUpVecY.Value = camera.upVector.y;
            LAUpVecZ.Value = camera.upVector.z;

            ATwist.Value = camera.twist;

            //Projection
            switch (camera.projection)
            {
                case RenderBase.OCameraProjection.perspective: RadioPersp.PerformClick(); break;
                case RenderBase.OCameraProjection.orthogonal: RadioOrtho.PerformClick(); break;
            }
            PZNear.Value = camera.zNear;
            PZFar.Value = camera.zFar;
            PPFovy.Value = camera.fieldOfViewY;
            OPHeight.Value = camera.height;
            PPARatio.Value = camera.aspectRatio;

            renderer.resetCamera();
        }

        private void TTransX_ValueChanged(object sender, EventArgs e)
        {
            if (camera != null) camera.transformTranslate.x = TTransX.Value;
        }

        private void TTransY_ValueChanged(object sender, EventArgs e)
        {
            if (camera != null) camera.transformTranslate.y = TTransY.Value;
        }

        private void TTransZ_ValueChanged(object sender, EventArgs e)
        {
            if (camera != null) camera.transformTranslate.z = TTransZ.Value;
        }

        private void TargetX_ValueChanged(object sender, EventArgs e)
        {
            if (camera != null) camera.target.x = TargetX.Value;
        }

        private void TargetY_ValueChanged(object sender, EventArgs e)
        {
            if (camera != null) camera.target.y = TargetY.Value;
        }

        private void TargetZ_ValueChanged(object sender, EventArgs e)
        {
            if (camera != null) camera.target.z = TargetZ.Value;
        }

        private void RRotX_ValueChanged(object sender, EventArgs e)
        {
            if (camera != null) camera.rotation.x = RRotX.Value;
        }

        private void RRotY_ValueChanged(object sender, EventArgs e)
        {
            if (camera != null) camera.rotation.y = RRotY.Value;
        }

        private void RRotZ_ValueChanged(object sender, EventArgs e)
        {
            if (camera != null) camera.rotation.z = RRotZ.Value;
        }

        private void LAUpVecX_ValueChanged(object sender, EventArgs e)
        {
            if (camera != null) camera.upVector.x = LAUpVecX.Value;
        }

        private void LAUpVecY_ValueChanged(object sender, EventArgs e)
        {
            if (camera != null) camera.upVector.y = LAUpVecY.Value;
        }

        private void LAUpVecZ_ValueChanged(object sender, EventArgs e)
        {
            if (camera != null) camera.upVector.z = LAUpVecZ.Value;
        }

        private void ATwist_ValueChanged(object sender, EventArgs e)
        {
            if (camera != null) camera.twist = ATwist.Value;
        }

        private void PZNear_ValueChanged(object sender, EventArgs e)
        {
            if (camera != null) camera.zNear = PZNear.Value;
        }

        private void PZFar_ValueChanged(object sender, EventArgs e)
        {
            if (camera != null) camera.zFar = PZFar.Value;
        }

        private void PPFovy_ValueChanged(object sender, EventArgs e)
        {
            if (camera != null) camera.fieldOfViewY = PPFovy.Value;
        }

        private void OPHeight_ValueChanged(object sender, EventArgs e)
        {
            if (camera != null) camera.height = OPHeight.Value;
        }

        private void PPARatio_ValueChanged(object sender, EventArgs e)
        {
            if (camera != null) camera.aspectRatio = PPARatio.Value;
        }

        private void TxtCameraName_ChangedText(object sender, EventArgs e)
        {
            if (camera != null)
            {
                camera.name = TxtCameraName.Text;
                CameraList.changeItem(CameraList.SelectedIndex, camera.name);
            }
        }

        private void BtnImport_Click(object sender, EventArgs e)
        {
            Object importedData = FileIO.import(FileIO.fileType.camera);
            if (importedData != null)
            {
                renderer.model.camera.AddRange((List<RenderBase.OCamera>)importedData);
                foreach (RenderBase.OCamera cam in (List<RenderBase.OCamera>)importedData) CameraList.addItem(cam.name);
                CameraList.Refresh();
            }
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            //TODO
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            renderer.model.camera.Clear();
            updateList();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            string currentName = null;
            int i = 0;

            bool found = true;
            while (found)
            {
                currentName = String.Format("camera_{0}", i);
                found = false;
                foreach (RenderBase.OCamera cam in renderer.model.camera) if (cam.name == currentName) found = true;
                i++;
            }

            RenderBase.OCamera camera = new RenderBase.OCamera();
            camera.name = currentName;
            camera.transformTranslate = new RenderBase.OVector3(0, 0, 20f);
            camera.transformRotate = new RenderBase.OVector3();
            camera.transformScale = new RenderBase.OVector3(1, 1, 1);
            camera.view = RenderBase.OCameraView.lookAtTarget;
            camera.target = new RenderBase.OVector3();
            camera.rotation = new RenderBase.OVector3();
            camera.upVector = new RenderBase.OVector3(0, 1, 0);
            camera.aspectRatio = 400f / 240f;
            camera.fieldOfViewY = (float)Math.PI / 4;
            camera.height = 240;
            camera.zNear = 0.1f;
            camera.zFar = 1000;

            renderer.model.camera.Add(camera);
            CameraList.addItem(currentName);
            CameraList.SelectedIndex = CameraList.Count - 1;
            CameraList.Refresh();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (CameraList.SelectedIndex == -1) return;
            renderer.model.camera.RemoveAt(CameraList.SelectedIndex);
            CameraList.removeItem(CameraList.SelectedIndex);
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            CameraList.SelectedIndex = -1;
        }

        private void BtnARTop_Click(object sender, EventArgs e)
        {
            PPARatio.Value = 400f / 240f;
        }

        private void BtnARBottom_Click(object sender, EventArgs e)
        {
            PPARatio.Value = 320f / 240f;
        }
    }
}
