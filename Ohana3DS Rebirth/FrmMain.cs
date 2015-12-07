using System;
using System.Windows.Forms;
using System.IO;

using Ohana3DS_Rebirth.Ohana;
using Ohana3DS_Rebirth.Ohana.Models;
using Ohana3DS_Rebirth.Ohana.Textures;
using Ohana3DS_Rebirth.Ohana.Containers;
using Ohana3DS_Rebirth.GUI;
using Ohana3DS_Rebirth.GUI.Forms;
using Ohana3DS_Rebirth.Properties;

namespace Ohana3DS_Rebirth
{
    public partial class FrmMain : OForm
    {
        bool isSettingsOpen;

        bool hasFileToOpen;
        string fileToOpen;

        public FrmMain()
        {
            InitializeComponent();
            WindowManager.initialize(DockContainer);
            MainMenu.Renderer = new OMenuStrip();
        }

        public void setFileToOpen(string fileName)
        {
            hasFileToOpen = true;
            fileToOpen = fileName;
        }

        private void FrmMain_Shown(object sender, EventArgs e)
        {
            if (hasFileToOpen) open(fileToOpen);
            hasFileToOpen = false;
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            WindowManager.flush();
        }

        private void LblTitle_MouseDown(object sender, MouseEventArgs e)
        {
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
                openDlg.Filter = "All supported files|*.bch;*.bcmdl;*.bcres;*.bcskla;*.bctex;*.cx;*.lz;*.cmp;*.fpt;*.pack;*.dmp;*.zmdl;*.ztex;*.rel;*.mdl.xml;*.smes;*.texi.xml;*.gr;*.mm;*.pc;*.mbn";
                openDlg.Filter += "|Binary CTR H3D|*.bch";
                openDlg.Filter += "|Binary CTR Model|*.bcmdl";
                openDlg.Filter += "|Binary CTR Resource|*.bcres";
                openDlg.Filter += "|Binary CTR Skeletal Animation|*.bcskla";
                openDlg.Filter += "|Binary CTR Texture|*.bctex";
                openDlg.Filter += "|Compressed file|*.cx;*.lz;*.cmp";
                openDlg.Filter += "|Dragon Quest VII Container|*.fpt";
                openDlg.Filter += "|Dragon Quest VII Package|*.pack";
                openDlg.Filter += "|Dragon Quest VII Texture|*.dmp";
                openDlg.Filter += "|Fantasy Life Model|*.zmdl";
                openDlg.Filter += "|Fantasy Life Texture|*.ztex";
                openDlg.Filter += "|Forbidden Magna CGFX|*.rel";
                openDlg.Filter += "|New Love Plus Model|*.mdl.xml";
                openDlg.Filter += "|New Love Plus Mesh|*.smes";
                openDlg.Filter += "|New Love Plus Texture|*.texi.xml";
                openDlg.Filter += "|Pokémon Map model|*.gr";
                openDlg.Filter += "|Pokémon Overworld model|*.mm";
                openDlg.Filter += "|Pokémon Species model|*.pc";
                openDlg.Filter += "|Sm4sh Model|*.mbn";
                openDlg.Filter += "|All files|*.*";

                if (openDlg.ShowDialog() == DialogResult.OK) open(openDlg.FileName);
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

        private delegate void openFile(string fileNmame);

        public void open(string fileName)
        {
            string name = Path.GetFileNameWithoutExtension(fileName);
            string extension = Path.GetExtension(fileName).ToLower();
            switch (extension) //Handle case-specific files than can't be identified by signature here
            {
                case ".mbn":
                    WindowManager.flush();
                    launchModel(MBN.load(fileName), name);
                    return;
                case ".xml":
                    switch (Path.GetExtension(Path.GetFileNameWithoutExtension(fileName)).ToLower())
                    {
                        case ".mdl":
                            WindowManager.flush();
                            launchModel(NLP.load(fileName), name);
                            return;
                        case ".texi":
                            WindowManager.flush();
                            OSingleTextureWindow singleTextureWindow = new OSingleTextureWindow();

                            singleTextureWindow.Title = name;

                            launchWindow(singleTextureWindow);
                            DockContainer.dockMainWindow();
                            WindowManager.Refresh();

                            singleTextureWindow.initialize(NLP.loadTexture(fileName).texture);
                            return;
                    }
                    break;
            }
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
                case FileIO.fileFormat.dq7DMP:
                    OSingleTextureWindow singleTextureWindow = new OSingleTextureWindow();

                    singleTextureWindow.Title = name;

                    launchWindow(singleTextureWindow);
                    DockContainer.dockMainWindow();
                    WindowManager.Refresh();

                    singleTextureWindow.initialize(DMP.load(data).texture);
                    break;
                case FileIO.fileFormat.dq7FPT0:
                    containerForm = new OContainerForm();
                    containerForm.launch(FPT0.load(data));
                    containerForm.Show(this);
                    break;
                case FileIO.fileFormat.dq7Model:
                    containerForm = new OContainerForm();
                    containerForm.launch(DQVIIPack.load(data));
                    containerForm.Show(this);
                    break;
                case FileIO.fileFormat.flZMdl: launchModel(ZMDL.load(data), name); break;
                case FileIO.fileFormat.flZTex:
                    OTextureWindow textureWindow = new OTextureWindow();

                    textureWindow.Title = name;

                    launchWindow(textureWindow);
                    DockContainer.dockMainWindow();
                    WindowManager.Refresh();

                    textureWindow.initialize(ZTEX.load(data));
                    break;
                case FileIO.fileFormat.fmNLK2:
                    buffer = new byte[data.Length - 0x80];
                    data.Seek(0x80, SeekOrigin.Begin);
                    data.Read(buffer, 0, buffer.Length);
                    data.Close();
                    launchModel(CGFX.load(new MemoryStream(buffer)), name);
                    break;
                case FileIO.fileFormat.nlpSMes: launchModel(NLP.loadMesh(data), name); break;
                case FileIO.fileFormat.nw4cCGfx: launchModel(CGFX.load(data), name); break;
                case FileIO.fileFormat.nw4cH3D:
                    buffer = new byte[data.Length];
                    data.Read(buffer, 0, buffer.Length);
                    data.Close();
                    launchModel(BCH.load(new MemoryStream(buffer)), name);
                    break;
                case FileIO.fileFormat.pkmnContainer:
                    GenericContainer.OContainer container = PkmnContainer.load(data);
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
            OViewportWindow viewportWindow = new OViewportWindow();
            OModelWindow modelWindow = new OModelWindow();
            OTextureWindow textureWindow = new OTextureWindow();
            OAnimationsWindow animationWindow = new OAnimationsWindow();

            viewportWindow.Title = name;

            RenderEngine renderer = new RenderEngine();
            renderer.MSAALevel = Settings.Default.reAALevel;
            renderer.bgColor = Settings.Default.reBgColor;
            renderer.useLegacyTexturing = Settings.Default.reUseLegacyTexturing;
            renderer.showGrid = Settings.Default.reShowGrids;
            renderer.showHUD = Settings.Default.reShowHUD;
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

        private void launchWindow(ODockWindow window, bool visible = true)
        {
            window.Visible = visible;
            DockContainer.launch(window);
            WindowManager.addWindow(window);
        }

        private void FrmMain_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void FrmMain_DragEnter(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length > 0)
            {
                openFile openFileDelegate = new openFile(open);
                BeginInvoke(openFileDelegate, files[0]);
                Activate();
            }
        }
    }
}
