using System;
using System.Windows.Forms;

using Ohana3DS_Rebirth.Ohana;

namespace Ohana3DS_Rebirth.GUI
{
    public partial class OImagePanel : UserControl, IPanel
    {
        string name;

        public OImagePanel()
        {
            InitializeComponent();
        }

        public void finalize()
        {
            //Nothing to dispose here
        }

        public void launch(object data)
        {
            RenderBase.OTexture texture = (RenderBase.OTexture)data;
            name = texture.name;
            TexturePreview.BackgroundImage = texture.texture; 
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveDlg = new SaveFileDialog())
            {
                saveDlg.Title = "Export Texture";
                saveDlg.FileName = name;
                saveDlg.Filter = "PNG Image|*.png";
                if (saveDlg.ShowDialog() == DialogResult.OK) TexturePreview.BackgroundImage.Save(saveDlg.FileName);
            }
        }
    }
}
