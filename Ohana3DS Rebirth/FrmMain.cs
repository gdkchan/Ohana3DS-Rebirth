using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

using Ohana3DS_Rebirth.Ohana;

namespace Ohana3DS_Rebirth
{
    public partial class FrmMain : Ohana3DS_Rebirth.OForm
    {
        public FrmMain()
        {
            InitializeComponent();
            WindowManager.Initialize(DockContainer);

            this.MaximumSize = Screen.PrimaryScreen.WorkingArea.Size;
            this.WindowState = FormWindowState.Maximized;
            MainMenu.Renderer = new GUI.OMenuStrip();
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
            MainMenu.Show(this.Left + LblTitle.Left, this.Top + LblTitle.Top + LblTitle.Height);
            if (e.Button == MouseButtons.Left) MainMenu.Show(this.Left + LblTitle.Left, this.Top + LblTitle.Top + LblTitle.Height);
        }

        private void mnuOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDlg = new OpenFileDialog();
            openDlg.Filter = "Binary CTR H3D File|*.bch";
            if (openDlg.ShowDialog() == DialogResult.OK)
            {
                FileIdentifier.fileFormat format = FileIdentifier.identify(openDlg.FileName);
                switch (format)
                {
                    case FileIdentifier.fileFormat.H3D:
                        GUI.OModelWindow modelWindow = new GUI.OModelWindow();
                        GUI.OTextureWindow textureWindow = new GUI.OTextureWindow();

                        String fileName = Path.GetFileNameWithoutExtension(openDlg.FileName);
                        modelWindow.Title = fileName + " :: Model";
                        textureWindow.Title = fileName + " :: Texture";

                        DockContainer.launch(modelWindow);
                        DockContainer.launch(textureWindow, modelWindow.Width);

                        WindowManager.addWindow(modelWindow);
                        WindowManager.addWindow(textureWindow);

                        WindowManager.createGroup(Path.GetFileName(openDlg.FileName));
                        RenderBase.OModelGroup model = Ohana.BCH.load(openDlg.FileName);

                        textureWindow.initialize(model);
                        modelWindow.initialize(model); //Always initialize the renderer at last, 'cause DirectX do weird stuff

                        break;

                    default:
                        MessageBox.Show("Unsupported file format!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        break;
                }
            }
        }
    }
}
