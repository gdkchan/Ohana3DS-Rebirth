using System;
using System.Collections.Generic;
using System.Windows.Forms;

using Ohana3DS_Rebirth.Ohana;

namespace Ohana3DS_Rebirth.GUI
{
    public partial class OTexturesPanel : UserControl, IPanel
    {
        RenderEngine renderer;

        public OTexturesPanel()
        {
            InitializeComponent();
        }

        public void launch(object data)
        {
            updateList((List<RenderBase.OTexture>)data);
        }

        public void launch(RenderEngine renderer)
        {
            this.renderer = renderer;
            updateList(renderer.models.texture);
        }

        public void finalize()
        {
            TextureList.flush();
        }

        private void updateList(List<RenderBase.OTexture> textures)
        {
            TextureList.flush();
            TextureList.addColumn(new OList.columnHeader(128, "#"));
            TextureList.addColumn(new OList.columnHeader(128, "Name"));
            foreach (RenderBase.OTexture texture in textures)
            {
                OList.listItemGroup item = new OList.listItemGroup();
                item.columns.Add(new OList.listItem(null, texture.texture));
                item.columns.Add(new OList.listItem(texture.name));
                TextureList.addItem(item);
            }
            TextureList.Refresh();
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            FileIO.export(FileIO.fileType.texture, renderer.models, TextureList.SelectedIndex);
        }

        private void BtnImport_Click(object sender, System.EventArgs e)
        {
            object importedData = FileIO.import(FileIO.fileType.texture);
            if (importedData != null)
            {
                if (renderer != null) renderer.addTextureRange((List<RenderBase.OTexture>)importedData);

                foreach (RenderBase.OTexture texture in (List<RenderBase.OTexture>)importedData)
                {
                    OList.listItemGroup item = new OList.listItemGroup();
                    item.columns.Add(new OList.listItem(null, texture.texture));
                    item.columns.Add(new OList.listItem(texture.name));
                    TextureList.addItem(item);
                }

                TextureList.Refresh();
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (TextureList.SelectedIndex == -1) return;
            if (renderer != null) renderer.removeTexture(TextureList.SelectedIndex);

            //Note: The SelectedIndex will change after this is called, so don't add it before the removeTexture!
            TextureList.removeItem(TextureList.SelectedIndex);
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            TextureList.flush(true);

            if (renderer != null)
            {
                renderer.models.texture.Clear();
                renderer.updateTextures();
            }
        }
    }
}
