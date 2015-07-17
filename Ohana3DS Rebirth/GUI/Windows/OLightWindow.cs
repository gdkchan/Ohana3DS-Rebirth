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
    public partial class OLightWindow : ODockWindow
    {
        RenderEngine renderer;
        RenderBase.OLight light;

        public OLightWindow()
        {
            InitializeComponent();
            NameGroup.Collapsed = true;
            LightGroup.Collapsed = true;
            Height = 352;
        }

        public void initialize(RenderEngine renderEngine)
        {
            renderer = renderEngine;
            updateList();
        }

        private void updateList()
        {
            LightList.flush();
            foreach (RenderBase.OLight light in renderer.model.light) LightList.addItem(light.name);
            LightList.Refresh();
        }

        private void LightList_SelectedIndexChanged(object sender, EventArgs e)
        {
            light = null;
            if (LightList.SelectedIndex == -1)
            {
                NameGroup.Enabled = false;
                LightGroup.Enabled = false;
                return;
            }
            else
            {
                NameGroup.Enabled = true;
                LightGroup.Enabled = true;
            }

            //Set light
            light = renderer.model.light[LightList.SelectedIndex];

            //Name
            TxtLightName.Text = light.name;

            Content.suspendUpdate();
            Content.SuspendLayout();

            //Light
            switch (light.lightUse)
            {
                case RenderBase.OLightUse.hemiSphere:
                    HLightEnabled.Checked = light.isLightEnabled;

                    SkyColor.Color = light.skyColor;
                    GroundColor.Color = light.groundColor;

                    SkyDirX.Value = light.direction.x;
                    SkyDirY.Value = light.direction.y;
                    SkyDirZ.Value = light.direction.z;

                    LerpFactor.Value = light.lerpFactor;

                    setHemisphere();

                    break;
                case RenderBase.OLightUse.ambient:
                    ALightEnabled.Checked = light.isLightEnabled;

                    AAmbientColor.Color = light.ambient;

                    setAmbient();

                    break;
                case RenderBase.OLightUse.vertex:
                    VLightEnabled.Checked = light.isLightEnabled;

                    VAmbientColor.Color = light.ambient;
                    VDiffuseColor.Color = light.diffuse;

                    VPosX.Value = light.transformTranslate.x;
                    VPosY.Value = light.transformTranslate.y;
                    VPosZ.Value = light.transformTranslate.z;

                    VDirX.Value = light.direction.x;
                    VDirY.Value = light.direction.y;
                    VDirZ.Value = light.direction.z;

                    InputAngle.SelectedIndex = (int)light.angleSampler.input;
                    InputScale.SelectedIndex = (int)light.angleSampler.scale;

                    CutOffAngle.Value = light.spotCutoffAngle;
                    Exponent.Value = light.spotExponent;

                    VAttEnabled.Checked = light.isDistanceAttenuationEnabled;
                    ConstantAtt.Value = light.distanceAttenuationConstant;
                    LinearAtt.Value = light.distanceAttenuationLinear;
                    QuadraticAtt.Value = light.distanceAttenuationQuadratic;

                    switch (light.lightType)
                    {
                        case RenderBase.OLightType.directional: VRadioDirectional.PerformClick(); break;
                        case RenderBase.OLightType.point: VRadioPoint.PerformClick(); break;
                        case RenderBase.OLightType.spot: VRadioSpot.PerformClick(); break;
                    }

                    setVertex();

                    break;
                case RenderBase.OLightUse.fragment:
                    FLightEnabled.Checked = light.isLightEnabled;
                    FTwoSidedDiffuse.Checked = light.isTwoSideDiffuse;

                    FAmbientColor.Color = light.ambient;
                    FDiffuseColor.Color = light.diffuse;
                    Spec0Color.Color = light.specular0;
                    Spec1Color.Color = light.specular1;

                    FPosX.Value = light.transformTranslate.x;
                    FPosY.Value = light.transformTranslate.y;
                    FPosZ.Value = light.transformTranslate.z;

                    FDirX.Value = light.direction.x;
                    FDirY.Value = light.direction.y;
                    FDirZ.Value = light.direction.z;

                    if (light.angleSampler.materialLUTName != null && light.angleSampler.samplerName != null)
                        TxtAngularAttLUT.Text = light.angleSampler.materialLUTName + "/" + light.angleSampler.samplerName;
                    
                    InputAngle.SelectedIndex = (int)light.angleSampler.input;
                    InputScale.SelectedIndex = (int)light.angleSampler.scale;

                    if (light.distanceSampler.materialLUTName != null && light.distanceSampler.samplerName != null)
                        TxtDistanceAttLUT.Text = light.distanceSampler.materialLUTName + "/" + light.distanceSampler.samplerName;
                    
                    FAttEnabled.Checked = light.isDistanceAttenuationEnabled;
                    AttStart.Value = light.attenuationStart;
                    AttEnd.Value = light.attenuationEnd;

                    switch (light.lightType)
                    {
                        case RenderBase.OLightType.directional: FRadioDirectional.PerformClick(); break;
                        case RenderBase.OLightType.point: FRadioPoint.PerformClick(); break;
                        case RenderBase.OLightType.spot: FRadioSpot.PerformClick(); break;
                    }

                    setFragment();
                    
                    break;
            }

            Content.ResumeLayout();
            Content.resumeUpdate();
        }

        #region "Fragment"
        private void RadioDirectional_CheckedChanged(object sender, EventArgs e)
        {
            LightGroup.SuspendDrawing();

            if (light != null) light.lightType = RenderBase.OLightType.directional;

            LblFPosition.Enabled = false;
            FPosX.Enabled = false;
            FPosY.Enabled = false;
            FPosZ.Enabled = false;

            LblFDirection.Enabled = true;
            FDirX.Enabled = true;
            FDirY.Enabled = true;
            FDirZ.Enabled = true;

            FAngularAttGroup.Enabled = false;
            FDistanceAttGroup.Enabled = false;

            LightGroup.ResumeDrawing();
        }

        private void RadioSpot_CheckedChanged(object sender, EventArgs e)
        {
            LightGroup.SuspendDrawing();

            if (light != null) light.lightType = RenderBase.OLightType.spot;

            LblFPosition.Enabled = true;
            FPosX.Enabled = true;
            FPosY.Enabled = true;
            FPosZ.Enabled = true;

            LblFDirection.Enabled = true;
            FDirX.Enabled = true;
            FDirY.Enabled = true;
            FDirZ.Enabled = true;

            FAngularAttGroup.Enabled = true;
            FDistanceAttGroup.Enabled = true;

            LightGroup.ResumeDrawing();
        }

        private void RadioPoint_CheckedChanged(object sender, EventArgs e)
        {
            LightGroup.SuspendDrawing();

            if (light != null) light.lightType = RenderBase.OLightType.point;

            LblFPosition.Enabled = true;
            FPosX.Enabled = true;
            FPosY.Enabled = true;
            FPosZ.Enabled = true;

            LblFDirection.Enabled = false;
            FDirX.Enabled = false;
            FDirY.Enabled = false;
            FDirZ.Enabled = false;

            FAngularAttGroup.Enabled = false;
            FDistanceAttGroup.Enabled = true;

            LightGroup.ResumeDrawing();
        }

        private void FLightEnabled_CheckedChanged(object sender, EventArgs e)
        {
            if (light != null) light.isLightEnabled = FLightEnabled.Checked;
        }

        private void FAmbientColor_ColorChanged(object sender, EventArgs e)
        {
            if (light != null) light.ambient = FAmbientColor.Color;
        }

        private void FDiffuseColor_ColorChanged(object sender, EventArgs e)
        {
            if (light != null) light.diffuse = FDiffuseColor.Color;
        }

        private void FTwoSidedDiffuse_CheckedChanged(object sender, EventArgs e)
        {
            if (light != null) light.isTwoSideDiffuse = FTwoSidedDiffuse.Checked;
        }

        private void Spec0Color_Load(object sender, EventArgs e)
        {
            if (light != null) light.specular0 = Spec0Color.Color;
        }

        private void Spec1Color_Load(object sender, EventArgs e)
        {
            if (light != null) light.specular1 = Spec1Color.Color;
        }

        private void FPosX_ValueChanged(object sender, EventArgs e)
        {
            if (light != null) light.transformTranslate.x = FPosX.Value;
        }

        private void FPosY_ValueChanged(object sender, EventArgs e)
        {
            if (light != null) light.transformTranslate.y = FPosY.Value;
        }

        private void FPosZ_ValueChanged(object sender, EventArgs e)
        {
            if (light != null) light.transformTranslate.z = FPosZ.Value;
        }

        private void FDirX_ValueChanged(object sender, EventArgs e)
        {
            if (light != null) light.direction.x = FDirX.Value;
        }

        private void FDirY_ValueChanged(object sender, EventArgs e)
        {
            if (light != null) light.direction.y = FDirY.Value;
        }

        private void FDirZ_ValueChanged(object sender, EventArgs e)
        {
            if (light != null) light.direction.z = FDirZ.Value;
        }

        private void TxtAngularAttLUT_ChangedText(object sender, EventArgs e)
        {
            if (light != null && TxtAngularAttLUT.Text.Contains("/"))
            {
                string[] names = TxtAngularAttLUT.Text.Split(Convert.ToChar("/"));
                light.angleSampler.materialLUTName = names[0];
                light.angleSampler.samplerName = names[1];
            }
        }

        private void InputAngle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (light != null) light.angleSampler.input = (RenderBase.OFragmentSamplerInput)InputAngle.SelectedIndex;
        }

        private void InputScale_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (light != null) light.angleSampler.scale = (RenderBase.OFragmentSamplerScale)InputScale.SelectedIndex;
        }

        private void FAttEnabled_CheckedChanged(object sender, EventArgs e)
        {
            if (light != null) light.isDistanceAttenuationEnabled = FAttEnabled.Checked;
        }

        private void TxtDistanceAttLUT_ChangedText(object sender, EventArgs e)
        {
            if (light != null && TxtDistanceAttLUT.Text.Contains("/"))
            {
                string[] names = TxtDistanceAttLUT.Text.Split(Convert.ToChar("/"));
                light.distanceSampler.materialLUTName = names[0];
                light.distanceSampler.samplerName = names[1];
            }
        }

        private void AttStart_ValueChanged(object sender, EventArgs e)
        {
            if (light != null) light.attenuationStart = AttStart.Value;
        }

        private void AttEnd_ValueChanged(object sender, EventArgs e)
        {
            if (light != null) light.attenuationEnd = AttEnd.Value;
        }
        #endregion

        #region "Vertex"
        private void VRadioDirectional_CheckedChanged(object sender, EventArgs e)
        {
            LightGroup.SuspendDrawing();

            if (light != null) light.lightType = RenderBase.OLightType.directional;

            LblVPosition.Enabled = false;
            VPosX.Enabled = false;
            VPosY.Enabled = false;
            VPosZ.Enabled = false;

            LblVDirection.Enabled = true;
            VDirX.Enabled = true;
            VDirY.Enabled = true;
            VDirZ.Enabled = true;

            VAngularAttGroup.Enabled = false;
            VDistanceAttGroup.Enabled = false;

            LightGroup.ResumeDrawing();
        }

        private void VRadioSpot_CheckedChanged(object sender, EventArgs e)
        {
            LightGroup.SuspendDrawing();

            if (light != null) light.lightType = RenderBase.OLightType.spot;

            LblVPosition.Enabled = true;
            VPosX.Enabled = true;
            VPosY.Enabled = true;
            VPosZ.Enabled = true;

            LblVDirection.Enabled = true;
            VDirX.Enabled = true;
            VDirY.Enabled = true;
            VDirZ.Enabled = true;

            VAngularAttGroup.Enabled = true;
            VDistanceAttGroup.Enabled = true;

            LightGroup.ResumeDrawing();
        }

        private void VRadioPoint_CheckedChanged(object sender, EventArgs e)
        {
            LightGroup.SuspendDrawing();

            if (light != null) light.lightType = RenderBase.OLightType.point;

            LblVPosition.Enabled = true;
            VPosX.Enabled = true;
            VPosY.Enabled = true;
            VPosZ.Enabled = true;

            LblFDirection.Enabled = false;
            VDirX.Enabled = false;
            VDirY.Enabled = false;
            VDirZ.Enabled = false;

            VAngularAttGroup.Enabled = false;
            VDistanceAttGroup.Enabled = true;

            LightGroup.ResumeDrawing();
        }

        private void VLightEnabled_Click(object sender, EventArgs e)
        {
            if (light != null) light.isLightEnabled = VLightEnabled.Checked;
        }

        private void VAmbientColor_ColorChanged(object sender, EventArgs e)
        {
            if (light != null) light.ambient = VAmbientColor.Color;
        }

        private void VDiffuseColor_ColorChanged(object sender, EventArgs e)
        {
            if (light != null) light.diffuse = VDiffuseColor.Color;
        }

        private void VPosX_ValueChanged(object sender, EventArgs e)
        {
            if (light != null) light.transformTranslate.x = VPosX.Value;
        }

        private void VPosY_ValueChanged(object sender, EventArgs e)
        {
            if (light != null) light.transformTranslate.y = VPosY.Value;
        }

        private void VPosZ_ValueChanged(object sender, EventArgs e)
        {
            if (light != null) light.transformTranslate.z = VPosZ.Value;
        }

        private void VDirX_ValueChanged(object sender, EventArgs e)
        {
            if (light != null) light.direction.x = VDirX.Value;
        }

        private void VDirY_ValueChanged(object sender, EventArgs e)
        {
            if (light != null) light.direction.y = VDirY.Value;
        }

        private void VDirZ_ValueChanged(object sender, EventArgs e)
        {
            if (light != null) light.direction.z = VDirZ.Value;
        }

        private void CutOffAngle_ValueChanged(object sender, EventArgs e)
        {
            if (light != null) light.spotCutoffAngle = CutOffAngle.Value;
        }

        private void Exponent_ValueChanged(object sender, EventArgs e)
        {
            if (light != null) light.spotExponent = Exponent.Value;
        }

        private void VAttEnabled_CheckedChanged(object sender, EventArgs e)
        {
            if (light != null) light.isDistanceAttenuationEnabled = VAttEnabled.Checked;
        }

        private void ConstantAtt_ValueChanged(object sender, EventArgs e)
        {
            if (light != null) light.distanceAttenuationConstant = ConstantAtt.Value;
        }

        private void LinearAtt_ValueChanged(object sender, EventArgs e)
        {
            if (light != null) light.distanceAttenuationLinear = LinearAtt.Value;
        }

        private void QuadraticAtt_ValueChanged(object sender, EventArgs e)
        {
            if (light != null) light.distanceAttenuationQuadratic = QuadraticAtt.Value;
        }
        #endregion

        #region "Ambient"
        private void ALightEnabled_CheckedChanged(object sender, EventArgs e)
        {
            if (light != null) light.isLightEnabled = ALightEnabled.Checked;
        }

        private void AAmbientColor_ColorChanged(object sender, EventArgs e)
        {
            if (light != null) light.ambient = AAmbientColor.Color;
        }
        #endregion

        #region "Hemisphere"
        private void HLightEnabled_Click(object sender, EventArgs e)
        {
            if (light != null) light.isLightEnabled = HLightEnabled.Checked;
        }

        private void SkyColor_ColorChanged(object sender, EventArgs e)
        {
            if (light != null) light.skyColor = SkyColor.Color;
        }

        private void GroundColor_ColorChanged(object sender, EventArgs e)
        {
            if (light != null) light.groundColor = GroundColor.Color;
        }

        private void SkyDirX_ValueChanged(object sender, EventArgs e)
        {
            if (light != null) light.direction.x = SkyDirX.Value;
        }

        private void SkyDirY_ValueChanged(object sender, EventArgs e)
        {
            if (light != null) light.direction.y = SkyDirY.Value;
        }

        private void SkyDirZ_ValueChanged(object sender, EventArgs e)
        {
            if (light != null) light.direction.z = SkyDirZ.Value;
        }

        private void LerpFactor_ValueChanged(object sender, EventArgs e)
        {
            if (light != null) light.lerpFactor = LerpFactor.Value;
        }
        #endregion

        private void setHemisphere()
        {
            HemisphereLightPanel.Visible = true;
            AmbientLightPanel.Visible = false;
            VertexLightPanel.Visible = false;
            FragmentLightPanel.Visible = false;

            LightGroup.recalculateSize();
        }

        private void setAmbient()
        {
            HemisphereLightPanel.Visible = false;
            AmbientLightPanel.Visible = true;
            VertexLightPanel.Visible = false;
            FragmentLightPanel.Visible = false;

            LightGroup.recalculateSize();
        }

        private void setVertex()
        {
            HemisphereLightPanel.Visible = false;
            AmbientLightPanel.Visible = false;
            VertexLightPanel.Visible = true;
            FragmentLightPanel.Visible = false;

            LightGroup.recalculateSize();
        }

        private void setFragment()
        {
            HemisphereLightPanel.Visible = false;
            AmbientLightPanel.Visible = false;
            VertexLightPanel.Visible = false;
            FragmentLightPanel.Visible = true;

            LightGroup.recalculateSize();
        }

        private void BtnImport_Click(object sender, EventArgs e)
        {
            Object importedData = FileIO.import(FileIO.fileType.light);
            if (importedData != null)
            {
                renderer.model.light.AddRange((List<RenderBase.OLight>)importedData);
                foreach (RenderBase.OLight light in (List<RenderBase.OLight>)importedData) LightList.addItem(light.name);
                LightList.Refresh();
            }
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            //TODO
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            renderer.model.light.Clear();
            updateList();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            OAddLightDialog dlg = new OAddLightDialog();
            dlg.DialogCallback += AddWindow_Callback;
            dlg.Show();
        }

        private void AddWindow_Callback(Object sender, OAddLightDialog.OAddLightEventArgs e)
        {
            if (e.response.ok)
            {
                RenderBase.OLight lgt = new RenderBase.OLight();
                lgt.name = e.response.lightName;
                lgt.transformTranslate = new RenderBase.OVector3(0, 10f, 0);
                lgt.transformRotate = new RenderBase.OVector3();
                lgt.transformScale = new RenderBase.OVector3(1, 1, 1);
                lgt.direction = new RenderBase.OVector3();
                lgt.ambient = Color.Black;
                lgt.diffuse = Color.White;
                lgt.specular0 = Color.White;
                lgt.specular1 = Color.White;
                lgt.lightUse = e.response.lightUse;
                lgt.lightType = RenderBase.OLightType.point;
                lgt.isDistanceAttenuationEnabled = true;
                lgt.attenuationStart = 5;
                lgt.attenuationEnd = 2;
                lgt.distanceSampler.materialLUTName = "TableNameHere";
                lgt.distanceSampler.samplerName = "SamplerNameHere";

                renderer.model.light.Add(lgt);
                LightList.addItem(e.response.lightName);
                LightList.SelectedIndex = LightList.Count - 1;
                LightList.Refresh();
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (LightList.SelectedIndex == -1) return;
            renderer.model.light.RemoveAt(LightList.SelectedIndex);
            LightList.removeItem(LightList.SelectedIndex);
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            LightList.SelectedIndex = -1;
        }
    }
}
