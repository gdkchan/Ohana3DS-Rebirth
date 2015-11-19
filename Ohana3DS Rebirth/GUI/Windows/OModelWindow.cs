using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

using Ohana3DS_Rebirth.Ohana;

namespace Ohana3DS_Rebirth.GUI
{
    public partial class OModelWindow : ODockWindow
    {
        RenderEngine renderer;

        public OModelWindow()
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
            ModelList.flush();
            foreach (RenderBase.OModel model in renderer.model.model) ModelList.addItem(model.name);
            if (ModelList.Count > 0) ModelList.SelectedIndex = 0;
            ModelList.Refresh();
        }

        private void ModelList_SelectedIndexChanged(object sender, EventArgs e)
        {
            renderer.CurrentModel = ModelList.SelectedIndex;
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            FileIO.export(FileIO.fileType.model, renderer.model, new List<int> { ModelList.SelectedIndex });
        }

        private void BtnImport_Click(object sender, EventArgs e)
        {
            Object importedData = FileIO.import(FileIO.fileType.model);
            if (importedData != null)
            {
                renderer.model.model.AddRange((List<RenderBase.OModel>)importedData);
                foreach (RenderBase.OModel model in (List<RenderBase.OModel>)importedData) ModelList.addItem(model.name);
                ModelList.Refresh();
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (ModelList.SelectedIndex == -1) return;
            renderer.model.model.RemoveAt(ModelList.SelectedIndex);
            ModelList.removeItem(ModelList.SelectedIndex);
            renderer.CurrentModel = ModelList.SelectedIndex;
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            renderer.CurrentModel = -1;
            renderer.model.model.Clear();
            updateList();
        }
    }
}
