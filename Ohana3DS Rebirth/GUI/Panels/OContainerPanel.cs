using System.IO;
using System.Windows.Forms;

using Ohana3DS_Rebirth.Ohana.Containers;

namespace Ohana3DS_Rebirth.GUI
{
    public partial class OContainerPanel : UserControl, IPanel
    {
        OContainer container;

        public OContainerPanel()
        {
            InitializeComponent();
        }

        public void finalize()
        {
            //Nothing needs disposing here...
        }

        public void launch(object data)
        {
            container = (OContainer)data;
            foreach (OContainer.fileEntry file in container.content) FileList.addItem(file.name);
            FileList.Refresh();
        }

        private void BtnExportAll_Click(object sender, System.EventArgs e)
        {
            using (FolderBrowserDialog browserDlg = new FolderBrowserDialog())
            {
                browserDlg.Description = "Export all files";
                if (browserDlg.ShowDialog() == DialogResult.OK)
                {
                    foreach (OContainer.fileEntry file in container.content)
                    {
                        string fileName = Path.Combine(browserDlg.SelectedPath, file.name);
                        File.WriteAllBytes(fileName, file.data);
                    }
                }
            }
        }

        private void BtnExport_Click(object sender, System.EventArgs e)
        {
            if (FileList.SelectedIndex == -1) return;
            using (SaveFileDialog saveDlg = new SaveFileDialog())
            {
                saveDlg.Title = "Export file";
                saveDlg.FileName = container.content[FileList.SelectedIndex].name;
                saveDlg.Filter = "All files|*.*";
                if (saveDlg.ShowDialog() == DialogResult.OK) File.WriteAllBytes(
                    saveDlg.FileName,
                    container.content[FileList.SelectedIndex].data);
            }
        }
    }
}
