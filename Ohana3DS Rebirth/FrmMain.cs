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
                Filter = "Binary CTR H3D File|*.bch"
            };
            if (openDlg.ShowDialog() != DialogResult.OK) return;

        private void openBCH(string filename)
        {
            
            WindowManager.flush();

            FileIdentifier.fileFormat format = FileIdentifier.identify(openDlg.FileName);
            switch (format)
            {
                case FileIdentifier.fileFormat.H3D:
                    GUI.OModelWindow modelWindow = new GUI.OModelWindow();
                    GUI.OTextureWindow textureWindow = new GUI.OTextureWindow();
                    GUI.OAnimWindow animWindow = new GUI.OAnimWindow();

                    String fileName = Path.GetFileNameWithoutExtension(openDlg.FileName);
                    modelWindow.Title = "Model [" + fileName + "]";
                    textureWindow.Title = "Textures [" + fileName + "]";
                    animWindow.Title = "Animations [" + fileName + "]";

                    launchWindow(modelWindow);
                    DockContainer.dockMainWindow();
                    launchWindow(textureWindow, false);
                    launchWindow(animWindow, false);
                    WindowManager.Refresh();

                    RenderEngine renderer = new RenderEngine();

                    RenderBase.OModelGroup model = BCH.load(openDlg.FileName);
                    renderer.model = model;
                    Application.DoEvents(); //Call this to avoid clicks on the OpenDialog going to ViewPort

                    textureWindow.initialize(model);
                    animWindow.initialize(renderer);
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
