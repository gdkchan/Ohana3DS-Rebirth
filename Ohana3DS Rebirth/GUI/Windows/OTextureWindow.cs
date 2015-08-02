using System;
using System.Collections.Generic;
using System.Windows.Forms;

using Ohana3DS_Rebirth.Ohana;

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

        public void initialize(List<RenderBase.OTexture> textures)
        {
            renderer = new RenderEngine();
            renderer.model = new RenderBase.OModelGroup();
            renderer.model.texture = textures;
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

        private void BtnExport_Click(object sender, EventArgs e)
        {
            FileIO.export(FileIO.fileType.texture, renderer.model, new List<int> { TextureList.SelectedIndex });
        }

        private void BtnImport_Click(object sender, System.EventArgs e)
        {
            Object importedData = FileIO.import(FileIO.fileType.texture);
            if (importedData != null)
            {
                renderer.model.texture.AddRange((List<RenderBase.OTexture>)importedData);
                renderer.updateTextures();
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
            renderer.model.texture.RemoveAt(TextureList.SelectedIndex);
            renderer.removeTexture(TextureList.SelectedIndex);
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
