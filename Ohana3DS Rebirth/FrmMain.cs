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
            WindowManager.Initialize(DockContainer);
            for (int i = 0; i < 8; i++)
            {
                GUI.ODockWindow window = new GUI.ODockWindow();
                DockContainer.launch(window);
                WindowManager.addWindow(window);
            }
            WindowManager.createGroup("Lorem Ipsum");
            this.ResumeLayout();
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
