using System;
using System.Windows.Forms;
using System.IO;

using Ohana3DS_Rebirth.Ohana;
using Ohana3DS_Rebirth.Ohana.Models.GenericFormats;
using Ohana3DS_Rebirth.Properties;

namespace Ohana3DS_Rebirth.GUI.Forms
{
    public partial class OModelExportForm : OForm
    {
        RenderBase.OModelGroup mdls;
        int mdlIndex;

        public OModelExportForm(RenderBase.OModelGroup models, int modelIndex = -1)
        {
            InitializeComponent();

            mdls = models;
            mdlIndex = modelIndex;
            if (modelIndex > -1) TxtModelName.Text = mdls.model[mdlIndex].name;
        }

        private void OModelExportForm_Load(object sender, EventArgs e)
        {
            TxtOutFolder.Text = Settings.Default.meOutFolder;

            switch (Settings.Default.meFormat)
            {
                case 0: RadioDAE.Checked = true; break;
                case 1: RadioSMD.Checked = true; break;
                case 2: RadioOBJ.Checked = true; break;
                case 3: RadioCMDL.Checked = true; break;
            }

            ChkExportAllModels.Checked = mdlIndex > -1 ? Settings.Default.meExportAllMdls : true;
            ChkExportAllAnimations.Checked = Settings.Default.meExportAllAnms;
            TxtModelName.Enabled = !ChkExportAllModels.Checked;
            ChkExportAllAnimations.Enabled = !ChkExportAllModels.Checked;
            if (!ChkExportAllAnimations.Enabled) ChkExportAllAnimations.Checked = false;
        }

        private void OModelExportForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter: ok(); break;
                case Keys.Escape: Close(); break;
            }
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            ok();
        }

        private void ok()
        {
            if (!Directory.Exists(TxtOutFolder.Text))
            {
                MessageBox.Show("Invalid output directory!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            Settings.Default.meOutFolder = TxtOutFolder.Text;

            int format = 0;
            if (RadioSMD.Checked)
                format = 1;
            else if (RadioOBJ.Checked)
                format = 2;
            else if (RadioCMDL.Checked)
                format = 3;

            Settings.Default.meFormat = format;
            Settings.Default.meExportAllMdls = ChkExportAllModels.Checked;
            Settings.Default.meExportAllAnms = ChkExportAllAnimations.Checked;

            Settings.Default.Save();

            if (ChkExportAllModels.Checked && !ChkExportAllAnimations.Checked)
            {
                for (int i = 0; i < mdls.model.Count; i++)
                {
                    string fileName = Path.Combine(TxtOutFolder.Text, mdls.model[i].name);

                    switch (format)
                    {
                        case 0: DAE.export(mdls, fileName + ".dae", i); break;
                        case 1: SMD.export(mdls, fileName + ".smd", i); break;
                        case 2: OBJ.export(mdls, fileName + ".obj", i); break;
                        case 3: CMDL.export(mdls, fileName + ".cmdl", i); break;
                    }
                }
            }
            else if (mdlIndex > -1)
            {
                string fileName = Path.Combine(TxtOutFolder.Text, TxtModelName.Text);

                switch (format)
                {
                    case 0: DAE.export(mdls, fileName + ".dae", mdlIndex); break;
                    case 1:
                        SMD.export(mdls, fileName + ".smd", mdlIndex);
                        if (ChkExportAllAnimations.Checked)
                        {
                            for (int i = 0; i < mdls.skeletalAnimation.list.Count; i++)
                            {
                                string name = mdls.skeletalAnimation.list[i].name + ".smd";
                                SMD.export(mdls, Path.Combine(TxtOutFolder.Text, name), mdlIndex, i);
                            }
                        }
                        break;
                    case 2: OBJ.export(mdls, fileName + ".obj", mdlIndex); break;
                    case 3: CMDL.export(mdls, fileName + ".cmdl", mdlIndex); break;
                }
            }

            Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ChkExportAllModels_CheckedChanged(object sender, EventArgs e)
        {
            TxtModelName.Enabled = !ChkExportAllModels.Checked;
            ChkExportAllAnimations.Enabled = !ChkExportAllModels.Checked;
            if (!ChkExportAllAnimations.Enabled) ChkExportAllAnimations.Checked = false;
        }

        private void BtnBrowseFolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                if (dlg.ShowDialog() == DialogResult.OK) TxtOutFolder.Text = dlg.SelectedPath;
            }
        }
    }
}
