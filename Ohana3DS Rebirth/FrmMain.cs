using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Ohana3DS_Rebirth
{
    public partial class FrmMain : Ohana3DS_Rebirth.OForm
    {
        public FrmMain()
        {
            InitializeComponent();
            this.SuspendLayout();
            MainMenu.Renderer = new GUI.OMenuStrip();
            MainMenu.BackColor = Color.Transparent;
            DockContainer.Initialize(this);
            DockContainer.Launch_Form(new Form1());
            DockContainer.Launch_Form(new Form1());
            DockContainer.Launch_Form(new Form1());
            DockContainer.Launch_Form(new Form1());
            this.ResumeLayout();
        }

        private void FrmMain_Layout(object sender, LayoutEventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized) {
                Status.SizingGrip = false;
            } else {
                Status.SizingGrip = true;
            }
        }

        private void MnuOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenDlg = new OpenFileDialog();
            OpenDlg.Filter = "Binary CTR H3D File|*.bch";
            if (OpenDlg.ShowDialog() == DialogResult.OK)
            {
                Ohana.BCH.Load(OpenDlg.FileName);
            }
        }
    }
}
