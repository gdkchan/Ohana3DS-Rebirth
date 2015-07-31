using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using Ohana3DS_Rebirth.Ohana.Containers;

namespace Ohana3DS_Rebirth
{
    public partial class OContainerForm : OForm
    {
        GenericContainer.OContainer container;

        public OContainerForm()
        {
            InitializeComponent();
        }

        public void launch(GenericContainer.OContainer _container)
        {
            container = _container;
            foreach (GenericContainer.OContainerEntry entry in container.content) FileList.addItem(entry.name);
        }

        private void BtnLoad_Click(object sender, EventArgs e)
        {
            if (FileList.SelectedIndex == -1) return;
            ((FrmMain)Owner).open(new MemoryStream(container.content[FileList.SelectedIndex].data), container.content[FileList.SelectedIndex].name);
        }

        private void BtnExtract_Click(object sender, EventArgs e)
        {
            if (FileList.SelectedIndex == -1) return;
            using (SaveFileDialog saveDlg = new SaveFileDialog())
            {
                saveDlg.Title = "Extract file";
                saveDlg.FileName = container.content[FileList.SelectedIndex].name;
                saveDlg.Filter = "All files|*.*";
                if (saveDlg.ShowDialog() == DialogResult.OK) File.WriteAllBytes(saveDlg.FileName, container.content[FileList.SelectedIndex].data);
            }
        }

        private void BtnExtractAll_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog browserDlg = new FolderBrowserDialog())
            {
                browserDlg.Description = "Extract files";
                if (browserDlg.ShowDialog() == DialogResult.OK)
                {
                    foreach (GenericContainer.OContainerEntry entry in container.content)
                    {
                        string fileName = Path.Combine(browserDlg.SelectedPath, entry.name);
                        File.WriteAllBytes(fileName, entry.data);
                    }
                }
            }
        }
    }
}
