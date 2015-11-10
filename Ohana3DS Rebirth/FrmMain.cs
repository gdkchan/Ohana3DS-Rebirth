using System;
using System.Windows.Forms;
using System.IO;

using Ohana3DS_Rebirth.Ohana;
using Ohana3DS_Rebirth.Ohana.ModelFormats;
using Ohana3DS_Rebirth.Ohana.TextureFormats;
using Ohana3DS_Rebirth.Properties;

namespace Ohana3DS_Rebirth
{
    public partial class FrmMain : OForm
    {
        bool isSettingsOpen;

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
                openDlg.Filter = "All supported files|*.bch;*.cx;*.lz;*.cmp;*.mm;*.gr;*.pc;*.pack;*.fpt;*.dmp;*.rel;*.bcres;*.bcmdl;*.bctex;*.bcskla;*.mdl;*.tex";
                openDlg.Filter += "|Binary CTR H3D|*.bch";
                openDlg.Filter += "|Compressed file|*.cx;*.lz;*.cmp";
                openDlg.Filter += "|Pokémon Overworld model|*.mm";
                openDlg.Filter += "|Pokémon Map model|*.gr";
                openDlg.Filter += "|Pokémon Species model|*.pc";
                openDlg.Filter += "|Dragon Quest VII Package|*.pack";
                openDlg.Filter += "|Dragon Quest VII Container|*.fpt";
                openDlg.Filter += "|Dragon Quest VII Texture|*.dmp";
                openDlg.Filter += "|Forbidden Magna CGFX|*.rel";
                openDlg.Filter += "|Binary CTR Resource|*.bcres";
                openDlg.Filter += "|Binary CTR Model|*.bcmdl";
                openDlg.Filter += "|Binary CTR Texture|*.bctex";
                openDlg.Filter += "|Binary CTR Skeletal Animation|*.bcskla";
                openDlg.Filter += "|Fantasy Life Model|*.mdl";
                openDlg.Filter += "|Fantasy Life Texture|*.tex";
                openDlg.Filter += "|All files|*.*";
                if (openDlg.ShowDialog() != DialogResult.OK) return;
                open(openDlg.FileName);
            }
        }

        private void mnuSettings_Click(object sender, EventArgs e)
        {
            if (!isSettingsOpen)
            {
                FrmSettings form = new FrmSettings();
                form.FormClosing += FrmSettings_FormClosing;
                form.Show();
                isSettingsOpen = true;
            }
        }

        private void FrmSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            isSettingsOpen = false;
        }

        public void open(string fileName)
        {
            string name = Path.GetFileNameWithoutExtension(fileName);
            open(new FileStream(fileName, FileMode.Open), name);
        }

        public void open(Stream data, string name)
        {
            WindowManager.flush();

            FileIO.fileFormat format = FileIO.identify(data);
            if (FileIO.isCompressed(format)) FileIO.decompress(ref data, ref format);

            byte[] buffer;
            OContainerForm containerForm;
            switch (format)
            {
                case FileIO.fileFormat.H3D:
                    buffer = new byte[data.Length];
                    data.Read(buffer, 0, buffer.Length);
                    data.Close();
                    launchModel(BCH.load(new MemoryStream(buffer)), name);
                    break;
                case FileIO.fileFormat.PkmnContainer:
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
                case FileIO.fileFormat.CGFX:
                    buffer = new byte[data.Length];
                    data.Read(buffer, 0, buffer.Length);
                    data.Close();
                    launchModel(CGFX.load(new MemoryStream(buffer)), name);
                    break;
                case FileIO.fileFormat.zmdl:
                    buffer = new byte[data.Length];
                    data.Read(buffer, 0, buffer.Length);
                    data.Close();
                    launchModel(ZMDL.load(new MemoryStream(buffer)), name);
                    break;
                case FileIO.fileFormat.ztex:
                    GUI.OTextureWindow textureWindow = new GUI.OTextureWindow();

                    textureWindow.Title = name;

                    launchWindow(textureWindow);
                    DockContainer.dockMainWindow();
                    WindowManager.Refresh();

                    textureWindow.initialize(ZTEX.load(data));
                    break;
                case FileIO.fileFormat.DQVIIPack:
                    containerForm = new OContainerForm();
                    containerForm.launch(Ohana.Containers.DQVIIPack.load(data));
                    containerForm.Show(this);
                    break;
                case FileIO.fileFormat.FPT0:
                    containerForm = new OContainerForm();
                    containerForm.launch(Ohana.Containers.FPT0.load(data));
                    containerForm.Show(this);
                    break;
                case FileIO.fileFormat.NLK2:
                    buffer = new byte[data.Length - 0x80];
                    data.Seek(0x80, SeekOrigin.Begin);
                    data.Read(buffer, 0, buffer.Length);
                    data.Close();
                    launchModel(CGFX.load(new MemoryStream(buffer)), name);
                    break;
                case FileIO.fileFormat.DMPTexture:
                    GUI.OSingleTextureWindow singleTextureWindow = new GUI.OSingleTextureWindow();

                    singleTextureWindow.Title = name;

                    launchWindow(singleTextureWindow);
                    DockContainer.dockMainWindow();
                    WindowManager.Refresh();

                    singleTextureWindow.initialize(DMP.load(data).texture);
                    break;
                default:
                    MessageBox.Show("Unsupported file format!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    data.Close();
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
            GUI.OAnimationsWindow animationWindow = new GUI.OAnimationsWindow();

            viewportWindow.Title = name;

            RenderEngine renderer = new RenderEngine();
            renderer.MSAALevel = Settings.Default.reAALevel;
            renderer.bgColor = Settings.Default.reBgColor;
            renderer.useLegacyTexturing = Settings.Default.reUseLegacyTexturing;
            renderer.showGrid = Settings.Default.reShowGrids;
            renderer.model = model;

            launchWindow(viewportWindow);
            DockContainer.dockMainWindow();
            launchWindow(modelWindow, false);
            launchWindow(textureWindow, false);
            launchWindow(animationWindow, false);

            WindowManager.Refresh();

            modelWindow.initialize(renderer);
            textureWindow.initialize(renderer);
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
