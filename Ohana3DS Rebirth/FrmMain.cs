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
            MainMenu.Renderer = new GUI.OMenuStrip();
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            WindowManager.flush();
        }

        private void LblTitle_MouseDown(object sender, MouseEventArgs e)
        {
            MainMenu.Show(Left + LblTitle.Left, Top + LblTitle.Top + LblTitle.Height);
            if (e.Button == MouseButtons.Left) MainMenu.Show(Left + LblTitle.Left, Top + LblTitle.Top + LblTitle.Height);
        }

        private void mnuAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ohana3DS Rebirth made by gdkchan.", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void mnuOpen_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openDlg = new OpenFileDialog())
            {
                openDlg.Filter = "All supported files|*.bch;*.cx;*.mm;*.gr;*.pc";
                openDlg.Filter += "|Binary CTR H3D|*.bch";
                openDlg.Filter += "|CTR Compressed file|*.cx";
                openDlg.Filter += "|Pokémon Overworld model|*.mm";
                openDlg.Filter += "|Pokémon Map model|*.gr";
                openDlg.Filter += "|Pokémon Species model|*.pc";
                openDlg.Filter += "|All files|*.*";
                if (openDlg.ShowDialog() != DialogResult.OK) return;
                open(openDlg.FileName, openDlg.FilterIndex);
            }
        }

        private void open(string fileName, int type)
        {
            WindowManager.flush();

            FileIdentifier.fileFormat format = FileIdentifier.identify(fileName);
            string name = Path.GetFileNameWithoutExtension(fileName);

            Stream data = new FileStream(fileName, FileMode.Open);
            switch (format)
            {
                case FileIdentifier.fileFormat.BLZCompressed:
                    byte[] decompressedData = Ohana.Compressions.BLZ.decompress(data);
                    byte[] content = new byte[decompressedData.Length - 1];
                    Buffer.BlockCopy(decompressedData, 1, content, 0, content.Length);
                    data = new MemoryStream(content);
                    format = FileIdentifier.identify(data);
                    break;
            }

            switch (format)
            {
                case FileIdentifier.fileFormat.H3D:
                    byte[] buffer = new byte[data.Length];
                    data.Read(buffer, 0, buffer.Length);
                    data.Close();
                    data.Dispose();
                    launchModel(BCH.load(new MemoryStream(buffer)), name);
                    break;
                case FileIdentifier.fileFormat.PkmnContainer:
                    Ohana.Containers.GenericContainer.OContainer container = Ohana.Containers.PkmnContainer.load(data);
                    switch (container.fileIdentifier)
                    {
                        case "PC": //Pokémon model
                            launchModel(BCH.load(new MemoryStream(container.content[0].data)), name);
                            break;
                        case "MM": //Pokémon Overworld model
                            launchModel(BCH.load(new MemoryStream(container.content[0].data)), name);
                            break;
                        case "GR": //Pokémon Map model
                            launchModel(BCH.load(new MemoryStream(container.content[1].data)), name);
                            break;
                    }
                    //TODO: Add windows for extra data

                    break;
                default:
                    MessageBox.Show("Unsupported file format!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;
            }
        }

        /// <summary>
        ///     Opens the windows used for a model.
        ///     More windows can be opened later, for files with model and other data.
        /// </summary>
        /// <param name="model">The model</param>
        /// <param name="name">The file name (without the full path and extension)</param>
        private void launchModel(RenderBase.OModelGroup model, string name)
        {
            GUI.OViewportWindow viewportWindow = new GUI.OViewportWindow();
            GUI.OModelWindow modelWindow = new GUI.OModelWindow();
            GUI.OTextureWindow textureWindow = new GUI.OTextureWindow();
            GUI.OLightWindow lightWindow = new GUI.OLightWindow();
            GUI.OCameraWindow cameraWindow = new GUI.OCameraWindow();
            GUI.OAnimationsWindow animationWindow = new GUI.OAnimationsWindow();

            viewportWindow.Title = name;
            modelWindow.Title = "Models";
            textureWindow.Title = "Textures";
            lightWindow.Title = "Lights";
            cameraWindow.Title = "Cameras";
            animationWindow.Title = "Animations";

            RenderEngine renderer = new RenderEngine();
            renderer.model = model;

            launchWindow(viewportWindow);
            DockContainer.dockMainWindow();
            launchWindow(modelWindow, false);
            launchWindow(textureWindow, false);
            launchWindow(lightWindow, false);
            launchWindow(cameraWindow, false);
            launchWindow(animationWindow, false);

            WindowManager.Refresh();

            modelWindow.initialize(renderer);
            textureWindow.initialize(renderer);
            lightWindow.initialize(renderer);
            cameraWindow.initialize(renderer);
            animationWindow.initialize(renderer);
            viewportWindow.initialize(renderer);
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
