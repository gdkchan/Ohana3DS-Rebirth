using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Ohana3DS_Rebirth.GUI
{
    public partial class OSingleTextureWindow : ODockWindow
    {
        public OSingleTextureWindow()
        {
            InitializeComponent();
        }

        public void initialize(Bitmap texture)
        {
            TexturePreview.BackgroundImage = texture;
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveDlg = new SaveFileDialog())
            {
                saveDlg.Title = "Export Texture";
                saveDlg.Filter = "PNG Image|*.png";
                if (saveDlg.ShowDialog() == DialogResult.OK) TexturePreview.BackgroundImage.Save(saveDlg.FileName);
            }
        }
    }
}
