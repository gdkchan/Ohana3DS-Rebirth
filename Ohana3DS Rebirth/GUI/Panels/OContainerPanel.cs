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
            if (container.data != null)
            {
                container.data.Close();
                container.data = null;
            }
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
                        string dir = Path.GetDirectoryName(fileName);
                        if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
                        if (file.loadFromDisk)
                        {
                            byte[] buffer = new byte[file.fileLength];
                            container.data.Seek(file.fileOffset, SeekOrigin.Begin);
                            container.data.Read(buffer, 0, buffer.Length);
                            File.WriteAllBytes(fileName, buffer);
                        }
                        else
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
                if (saveDlg.ShowDialog() == DialogResult.OK)
                {
                    OContainer.fileEntry file = container.content[FileList.SelectedIndex];

                    if (file.loadFromDisk)
                    {
                        byte[] buffer = new byte[file.fileLength];
                        container.data.Seek(file.fileOffset, SeekOrigin.Begin);
                        container.data.Read(buffer, 0, buffer.Length);
                        File.WriteAllBytes(saveDlg.FileName, buffer);
                    }
                    else
                        File.WriteAllBytes(saveDlg.FileName, file.data);
                }
            }
        }
    }
}
