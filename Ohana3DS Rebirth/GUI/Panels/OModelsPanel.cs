using System;
using System.Collections.Generic;
using System.Windows.Forms;

using Ohana3DS_Rebirth.Ohana;

namespace Ohana3DS_Rebirth.GUI
{
    public partial class OModelsPanel : UserControl
    {
        RenderEngine renderer;

        public OModelsPanel()
        {
            InitializeComponent();
        }

        public void launch(RenderEngine renderEngine)
        {
            renderer = renderEngine;
            updateList();
        }

        private void updateList()
        {
            ModelList.flush();
            foreach (RenderBase.OModel model in renderer.models.model) ModelList.addItem(model.name);
            if (ModelList.Count > 0) ModelList.SelectedIndex = 0;
            ModelList.Refresh();
        }

        private void ModelList_SelectedIndexChanged(object sender, EventArgs e)
        {
            renderer.CurrentModel = ModelList.SelectedIndex;
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            FileIO.export(FileIO.fileType.model, renderer.models, ModelList.SelectedIndex);
        }

        private void BtnImport_Click(object sender, EventArgs e)
        {
            object importedData = FileIO.import(FileIO.fileType.model);
            if (importedData != null)
            {
                renderer.models.model.AddRange((List<RenderBase.OModel>)importedData);
                foreach (RenderBase.OModel model in (List<RenderBase.OModel>)importedData) ModelList.addItem(model.name);
                ModelList.Refresh();
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (ModelList.SelectedIndex == -1) return;

            renderer.models.model.RemoveAt(ModelList.SelectedIndex);
            renderer.CurrentModel = ModelList.SelectedIndex;

            ModelList.removeItem(ModelList.SelectedIndex);
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            renderer.CurrentModel = -1;
            renderer.models.model.Clear();
            updateList();
        }
    }
}
