using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

using Ohana3DS_Rebirth.Ohana;

namespace Ohana3DS_Rebirth
{
    public partial class FrmMain : OForm
    {

        public FrmMain()
        {
            InitializeComponent();
            WindowManager.initialize(DockContainer);

            MaximumSize = Screen.PrimaryScreen.WorkingArea.Size;
            MainMenu.Renderer = new GUI.OMenuStrip();
            this.AllowDrop = true;
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            WindowManager.flush();
        }

        private void LblTitle_MouseEnter(object sender, EventArgs e)
        {
            LblTitle.BackColor = Color.FromArgb(0x7f, 15, 82, 186);
        }

        private void LblTitle_MouseLeave(object sender, EventArgs e)
        {
            LblTitle.BackColor = Color.Transparent;
        }

        private void LblTitle_MouseDown(object sender, MouseEventArgs e)
        {
            MainMenu.Show(Left + LblTitle.Left, Top + LblTitle.Top + LblTitle.Height);
            if (e.Button == MouseButtons.Left) MainMenu.Show(Left + LblTitle.Left, Top + LblTitle.Top + LblTitle.Height);
        }

        private void mnuOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDlg = new OpenFileDialog
            {
                Filter = "Binary CTR H3D File|*.bch|Overworld Model|*.mm"
            };
            if (openDlg.ShowDialog() != DialogResult.OK) return;
            open(openDlg.FileName, openDlg.FilterIndex);
        }

        private void open(string fileName, int type)
        {
            WindowManager.flush();

            FileIdentifier.fileFormat format = FileIdentifier.identify(fileName);

            RenderBase.OModelGroup model;
            GUI.OModelWindow modelWindow = new GUI.OModelWindow();
            GUI.OTextureWindow textureWindow = new GUI.OTextureWindow();
            GUI.OAnimationsWindow animWindow = new GUI.OAnimationsWindow();
            GUI.OCameraWindow cameraWindow = new GUI.OCameraWindow();
            RenderEngine renderer = new RenderEngine();
            string name = Path.GetFileNameWithoutExtension(fileName);
            modelWindow.Title = "Model";
            textureWindow.Title = "Textures";
            animWindow.Title = "Animations";
            cameraWindow.Title = "Cameras";

            switch (format)
            {                    
                case FileIdentifier.fileFormat.H3D:

                    model = BCH.load(fileName);
                    launchWindow(modelWindow);
                    DockContainer.dockMainWindow();
                    launchWindow(textureWindow, false);
                    launchWindow(animWindow, false);
                    launchWindow(cameraWindow, false);
                    WindowManager.Refresh();

                    renderer.model = model;
                    //Ohana.GenericFormats.SMD.export(model, "D:\\teste.smd");

                    textureWindow.initialize(renderer);
                    animWindow.initialize(renderer);
                    cameraWindow.initialize(renderer);
                    modelWindow.initialize(renderer);

                    break;
                case FileIdentifier.fileFormat.MM:

                    model = Containers.loadMM(fileName);
                    launchWindow(modelWindow);
                    DockContainer.dockMainWindow();
                    launchWindow(textureWindow, false);
                    launchWindow(animWindow, false);
                    launchWindow(cameraWindow, false);
                    WindowManager.Refresh();

                    renderer.model = model;
                    //Ohana.GenericFormats.SMD.export(model, "D:\\teste.smd");

                    textureWindow.initialize(renderer);
                    animWindow.initialize(renderer);
                    cameraWindow.initialize(renderer);
                    modelWindow.initialize(renderer);
                    break;
                default:
                    MessageBox.Show("Unsupported file format!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;
            }
        }

        private void launchWindow(GUI.ODockWindow window, bool visible = true)
        {
            window.Visible = visible;
            DockContainer.launch(window);
            WindowManager.addWindow(window);
        }

        private void FrmMain_DragDrop(object sender, DragEventArgs e)
        {
            //stubbed
        }

        private void FrmMain_DragEnter(object sender, DragEventArgs e)
        {
            //stubbed
        }
    }
}
