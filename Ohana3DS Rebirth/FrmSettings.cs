using System;
using System.Drawing;

using Ohana3DS_Rebirth.Properties;

namespace Ohana3DS_Rebirth
{
    public partial class FrmSettings : OForm
    {
        public FrmSettings()
        {
            InitializeComponent();
        }

        private void FrmSettings_Load(object sender, EventArgs e)
        {
            switch (Settings.Default.reAALevel)
            {
                case 0: RadioAANone.Checked = true; break;
                case 2: RadioAA2x.Checked = true; break;
                case 4: RadioAA4x.Checked = true; break;
                case 8: RadioAA8x.Checked = true; break;
            }

            ViewBgColorPicker.Color = Color.FromArgb(Settings.Default.reBgColor);
            ChkEnableFShader.Checked = !Settings.Default.reUseLegacyTexturing;
            ChkShowGrids.Checked = Settings.Default.reShowGrids;
            ChkShowHUD.Checked = Settings.Default.reShowHUD;
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            if (RadioAANone.Checked)
                Settings.Default.reAALevel = 0;
            else if (RadioAA2x.Checked)
                Settings.Default.reAALevel = 2;
            else if (RadioAA4x.Checked)
                Settings.Default.reAALevel = 4;
            else if (RadioAA8x.Checked)
                Settings.Default.reAALevel = 8;

            Settings.Default.reBgColor = ViewBgColorPicker.Color.ToArgb();
            Settings.Default.reUseLegacyTexturing = !ChkEnableFShader.Checked;
            Settings.Default.reShowGrids = ChkShowGrids.Checked;
            Settings.Default.reShowHUD = ChkShowHUD.Checked;

            Settings.Default.Save();
            Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            Settings.Default.Reset();
            Close();
        }
    }
}
