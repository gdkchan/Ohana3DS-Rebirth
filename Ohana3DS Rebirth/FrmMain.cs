﻿using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using Ohana3DS_Rebirth.GUI;
using Ohana3DS_Rebirth.Ohana;
using Ohana3DS_Rebirth.Properties;
using Ohana3DS_Rebirth.Tools;
using Ohana3DS_Rebirth.Ohana.Models.PocketMonsters;

namespace Ohana3DS_Rebirth
{
    public partial class FrmMain : OForm
    {
        bool hasFileToOpen;
        string fileToOpen;
        FileIO.file file;

        public FrmMain()
        {
            InitializeComponent();
            TopMenu.Renderer = new OMenuStrip();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            //Viewport menu settings
            switch (Settings.Default.reAntiAlias)
            {
                case 0: MenuViewAANone.Checked = true; break;
                case 2: MenuViewAA2x.Checked = true; break;
                case 4: MenuViewAA4x.Checked = true; break;
                case 8: MenuViewAA8x.Checked = true; break;
                case 16: MenuViewAA16x.Checked = true; break;
            }

            MenuViewShowGuidelines.Checked = Settings.Default.reShowGuidelines;
            MenuViewShowInformation.Checked = Settings.Default.reShowInformation;
            MenuViewShowAllMeshes.Checked = Settings.Default.reShowAllMeshes;

            MenuViewFragmentShader.Checked = Settings.Default.reFragmentShader;
            switch (Settings.Default.reLegacyTexturingMode)
            {
                case 0: MenuViewTexUseFirst.Checked = true; break;
                case 1: MenuViewTexUseLast.Checked = true; break;
            }
            if (MenuViewFragmentShader.Checked)
            {
                MenuViewTexUseFirst.Enabled = false;
                MenuViewTexUseLast.Enabled = false;
            }

            MenuViewShowSidebar.Checked = Settings.Default.viewShowSidebar;
            MenuViewWireframeMode.Checked = Settings.Default.reWireframeMode;

            if (hasFileToOpen)
            {
                RenderBase.OModelGroup group = PC.load(fileToOpen);
                group.model[0].name = Path.GetFileNameWithoutExtension(fileToOpen);

                object[] arguments = { 0, 0, Path.Combine(Path.GetDirectoryName(fileToOpen), Path.GetFileNameWithoutExtension(fileToOpen)) };

                FileIO.export(FileIO.fileType.model, group, arguments);

                file.data = null;

                Close();

                Application.Exit();
            }
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
            if (currentPanel != null) currentPanel.finalize();
        }

        delegate void openFile(string fileNmame);
        delegate void openFiles(string[] fileNames);

        FileIO.formatType currentFormat;
        IPanel currentPanel;

        public void open(string fileName)
        {
            if (currentPanel != null)
            {
                currentPanel.finalize();
                ContentContainer.Controls.Remove((Control)currentPanel);
            }
            
            try
            {
                FileIO.file file = FileIO.load(fileName);
                currentFormat = file.type;

                if (currentFormat != FileIO.formatType.unsupported)
                {
                    switch (currentFormat)
                    {
                        case FileIO.formatType.container: currentPanel = new OContainerPanel(); break;
                        case FileIO.formatType.image: currentPanel = new OImagePanel(); break;
                        case FileIO.formatType.model: currentPanel = new OViewportPanel(); break;
                        case FileIO.formatType.texture: currentPanel = new OTexturesPanel(); break;
                        case FileIO.formatType.animation: currentPanel = new OAnimationsPanel(); break;
                    }

                    ((Control)currentPanel).Dock = DockStyle.Fill;
                    SuspendDrawing();
                    ContentContainer.Controls.Add((Control)currentPanel);
                    ContentContainer.Controls.SetChildIndex((Control)currentPanel, 0);
                    ResumeDrawing();
                    currentPanel.launch(file.data);
                }
                else
                    MessageBox.Show("Unsupported file format!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
            catch (Exception)
            {
                MessageBox.Show("Unsupported file format!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                if (currentPanel != null)
                {
                    currentPanel.finalize();
                    ContentContainer.Controls.Remove((Control)currentPanel);
                }
            }
        }

        public void openMultipleFiles(string[] fileNames)
        {
            if (currentPanel != null)
            {
                currentPanel.finalize();
                ContentContainer.Controls.Remove((Control)currentPanel);
            }

            RenderBase.OModelGroup model = new RenderBase.OModelGroup();
            string error = "";

            foreach (string fileName in fileNames)
            {
                try
                {
                    FileIO.file file = FileIO.load(fileName);

                    if (file.type == FileIO.formatType.model)
                    {
                        model.merge((RenderBase.OModelGroup)file.data);
                    }
                    else
                    {
                        error += "\n    " + System.IO.Path.GetFileName(fileName);
                        if (file.type != FileIO.formatType.unsupported) error += "*";
                    }
                }
                catch (Exception)
                {
                    error += "\n    " + System.IO.Path.GetFileName(fileName);
                }

            }

            currentPanel = new OViewportPanel();

            ((Control)currentPanel).Dock = DockStyle.Fill;
            SuspendDrawing();
            ContentContainer.Controls.Add((Control)currentPanel);
            ContentContainer.Controls.SetChildIndex((Control)currentPanel, 0);
            ResumeDrawing();
            currentPanel.launch(model);

            if (error.Length > 0)
                MessageBox.Show("Could not load the following files in MultiFile-Mode:\n" + error + "\n\n*Marked files are loadable in Single File-Mode", 
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void FrmMain_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length == 1)
            {
                openFile openFileDelegate = new openFile(open);
                BeginInvoke(openFileDelegate, files[0]);
                Activate();
            }
            else if (files.Length > 1)
            {
                openFiles openFilesDelegate = new openFiles(openMultipleFiles);
                BeginInvoke(openFilesDelegate, (object)files);
                Activate();
            }
        }

        private void FrmMain_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void destroyOpenPanels()
        {
            if (currentPanel != null)
            {
                currentPanel.finalize();
                ContentContainer.Controls.Remove((Control)currentPanel);
            }
        }

        #region "Menus"
        /*
         * File
         */

        //Open

        private void MenuOpen_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openDlg = new OpenFileDialog())
            {
                openDlg.Filter = "All files|*.*";
                if (openDlg.ShowDialog() == DialogResult.OK)
                {
                    destroyOpenPanels();
                    open(openDlg.FileName);
                }
            }
        }

        //Exit

        private void MenuExit_Click(object sender, EventArgs e) {
            //TODO clear maybe?
        }

        /*
         * Options -> Viewport
         */

        //Anti Aliasing

        private void MenuViewAANone_Click(object sender, EventArgs e)
        {
            setAACheckBox((ToolStripMenuItem)sender, 0);
        }

        private void MenuViewAA2x_Click(object sender, EventArgs e)
        {
            setAACheckBox((ToolStripMenuItem)sender, 2);
        }

        private void MenuViewAA4x_Click(object sender, EventArgs e)
        {
            setAACheckBox((ToolStripMenuItem)sender, 4);
        }

        private void MenuViewAA8x_Click(object sender, EventArgs e)
        {
            setAACheckBox((ToolStripMenuItem)sender, 8);
        }

        private void MenuViewAA16x_Click(object sender, EventArgs e)
        {
            setAACheckBox((ToolStripMenuItem)sender, 16);
        }

        private void setAACheckBox(ToolStripMenuItem control, int value)
        {
            MenuViewAANone.Checked = false;
            MenuViewAA2x.Checked = false;
            MenuViewAA4x.Checked = false;
            MenuViewAA8x.Checked = false;
            MenuViewAA16x.Checked = false;

            control.Checked = true;
            Settings.Default.reAntiAlias = value;
            Settings.Default.Save();
            updateViewportSettings();
            changesNeedsRestart();
        }

        //Background

        private void MenuViewBgBlack_Click(object sender, EventArgs e)
        {
            setViewportBgColor(Color.Black.ToArgb());
        }

        private void MenuViewBgGray_Click(object sender, EventArgs e)
        {
            setViewportBgColor(Color.DimGray.ToArgb());
        }

        private void MenuViewBgWhite_Click(object sender, EventArgs e)
        {
            setViewportBgColor(Color.White.ToArgb());
        }

        private void MenuViewBgCustom_Click(object sender, EventArgs e)
        {
            using (ColorDialog colorDlg = new ColorDialog())
            {
                if (colorDlg.ShowDialog() == DialogResult.OK) setViewportBgColor(colorDlg.Color.ToArgb());
            }
        }

        private void setViewportBgColor(int color)
        {
            Settings.Default.reBackgroundColor = color;
            Settings.Default.Save();
            updateViewportSettings();
        }

        //Show/hide

        private void MenuViewShowGuidelines_Click(object sender, EventArgs e)
        {
            MenuViewShowGuidelines.Checked = !MenuViewShowGuidelines.Checked;
            Settings.Default.reShowGuidelines = MenuViewShowGuidelines.Checked;
            Settings.Default.Save();
            updateViewportSettings();
        }

        private void MenuViewShowInformation_Click(object sender, EventArgs e)
        {
            ShowInformation();
        }
        private void ShowInformation()
        {
            MenuViewShowInformation.Checked = !MenuViewShowInformation.Checked;
            Settings.Default.reShowInformation = MenuViewShowInformation.Checked;
            Settings.Default.Save();
            updateViewportSettings();
        }

        private void MenuViewShowAllMeshes_Click(object sender, EventArgs e)
        {
            MenuViewShowAllMeshes.Checked = !MenuViewShowAllMeshes.Checked;
            Settings.Default.reShowAllMeshes = MenuViewShowAllMeshes.Checked;
            Settings.Default.Save();
            updateViewportSettings();
        }

        //Texturing

        private void MenuViewFragmentShader_Click(object sender, EventArgs e)
        {
            MenuViewFragmentShader.Checked = !MenuViewFragmentShader.Checked;

            if (MenuViewFragmentShader.Checked)
            {
                MenuViewTexUseFirst.Enabled = false;
                MenuViewTexUseLast.Enabled = false;
            }
            else
            {
                MenuViewTexUseFirst.Enabled = true;
                MenuViewTexUseLast.Enabled = true;
            }

            Settings.Default.reFragmentShader = MenuViewFragmentShader.Checked;
            Settings.Default.Save();
            updateViewportSettings();
            changesNeedsRestart();
        }

        private void MenuViewTexUseFirst_Click(object sender, EventArgs e)
        {
            MenuViewTexUseFirst.Checked = true;
            MenuViewTexUseLast.Checked = false;
            Settings.Default.reLegacyTexturingMode = 0;
            Settings.Default.Save();
            updateViewportSettings();
        }

        private void MenuViewTexUseLast_Click(object sender, EventArgs e)
        {
            MenuViewTexUseFirst.Checked = false;
            MenuViewTexUseLast.Checked = true;
            Settings.Default.reLegacyTexturingMode = 1;
            Settings.Default.Save();
            updateViewportSettings();
        }

        //Misc. UI

        private void MenuViewShowSidebar_Click(object sender, EventArgs e)
        {
            MenuViewShowSidebar.Checked = !MenuViewShowSidebar.Checked;
            Settings.Default.viewShowSidebar = MenuViewShowSidebar.Checked;
            Settings.Default.Save();
            updateViewportSettings();
        }

        private void MenuViewWireframeMode_Click(object sender, EventArgs e)
        {
            MenuViewWireframeMode.Checked = !MenuViewWireframeMode.Checked;
            Settings.Default.reWireframeMode = MenuViewWireframeMode.Checked;
            Settings.Default.Save();
            updateViewportSettings();
        }

        private void updateViewportSettings()
        {
            if (currentFormat == FileIO.formatType.model)
            {
                OViewportPanel viewport = (OViewportPanel)currentPanel;
                viewport.renderer.updateSettings();
                viewport.ShowSidebar = MenuViewShowSidebar.Checked;
            }
        }

        private void changesNeedsRestart()
        {
            if (currentFormat == FileIO.formatType.model)
            {
                MessageBox.Show(
                    "Please restart the rendering engine to make those changes take effect!",
                    "Information",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        /*
         * Tools
         */

        private void MenuToolBCHTextureReplace_Click(object sender, EventArgs e)
        {
            new OBCHTextureReplacer(this).Show();
        }

        private void MenuToolSm4shModelCreator_Click(object sender, EventArgs e)
        {
            new OSm4shModelCreator(this).Show();
        }

        /*
         * Help
         */

        //About

        private void MenuAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ohana3DS Rebirth made by gdkchan.", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        //Global keylistener
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {
            if (currentPanel != null) {
                switch (keyData) {
                    case Keys.I:
                        ShowInformation();
                        break;
                }
                return true;
            }

            // Call the base class
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
