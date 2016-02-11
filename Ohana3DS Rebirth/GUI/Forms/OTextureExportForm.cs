using System;
using System.IO;
using System.Windows.Forms;

using Ohana3DS_Rebirth.Ohana;
using Ohana3DS_Rebirth.Properties;

namespace Ohana3DS_Rebirth.GUI.Forms
{
    public partial class OTextureExportForm : OForm
    {
        RenderBase.OModelGroup mdls;
        int texIndex;

        public OTextureExportForm(RenderBase.OModelGroup models, int textureIndex = -1)
        {
            InitializeComponent();

            mdls = models;
            texIndex = textureIndex;
            if (textureIndex > -1) TxtTextureName.Text = mdls.texture[texIndex].name;
        }

        private void OTextureExportForm_Load(object sender, EventArgs e)
        {
            TxtOutFolder.Text = Settings.Default.teOutFolder;

            ChkExportAllTextures.Checked = texIndex > -1 ? Settings.Default.teExportAllTxs : true;
            TxtTextureName.Enabled = !ChkExportAllTextures.Checked;
        }

        private void OTextureExportForm_KeyDown(object sender, KeyEventArgs e)
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
            Settings.Default.teOutFolder = TxtOutFolder.Text;
            Settings.Default.teExportAllTxs = ChkExportAllTextures.Checked;
            Settings.Default.Save();

            if (ChkExportAllTextures.Checked)
            {
                foreach (RenderBase.OTexture tex in mdls.texture)
                {
                    string fileName = Path.Combine(TxtOutFolder.Text, tex.name) + ".png";
                    tex.texture.Save(fileName);
                }
            }
            else if (texIndex > -1)
            {
                string fileName = Path.Combine(TxtOutFolder.Text, TxtTextureName.Text) + ".png";
                mdls.texture[texIndex].texture.Save(fileName);
            }

            Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ChkExportAllTextures_CheckedChanged(object sender, EventArgs e)
        {
            TxtTextureName.Enabled = !ChkExportAllTextures.Checked;
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
