using System;
using Ohana3DS_Rebirth.Ohana;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Ohana3DS_Rebirth.GUI
{
    public partial class OTextureWindow : ODockWindow
    {
        RenderEngine renderer;

        public OTextureWindow()
        {
            InitializeComponent();
        }

        public void initialize(RenderEngine renderEngine)
        {
            renderer = renderEngine;
            updateList();
        }

        private void updateList()
        {
            TextureList.flush();
            TextureList.addColumn(new OList.columnHeader(128, "#"));
            TextureList.addColumn(new OList.columnHeader(128, "Name"));
            foreach (RenderBase.OTexture texture in renderer.model.texture)
            {
                OList.listItemGroup item = new OList.listItemGroup();
                item.columns.Add(new OList.listItem(null, texture.texture));
                item.columns.Add(new OList.listItem(texture.name));
                TextureList.addItem(item);
            }
            TextureList.Refresh();
        }

        private void BtnImport_Click(object sender, System.EventArgs e)
        {
            Object importedData = FileImporter.import(FileImporter.importFileType.texture);
            if (importedData != null)
            {
                renderer.model.addTexture((List<RenderBase.OTexture>)importedData);
                renderer.updateTextures();
                updateList();
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (TextureList.SelectedIndex == -1) return;
            renderer.model.texture.RemoveAt(TextureList.SelectedIndex);
            renderer.updateTextures();
            TextureList.removeItem(TextureList.SelectedIndex);
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            renderer.model.texture.Clear();
            renderer.updateTextures();
            updateList();
        }
    }
}
