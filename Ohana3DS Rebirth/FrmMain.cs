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

            //testing only
            for (int i = 0; i < 4; i++)
            {
                GUI.ODockWindow window = new GUI.ODockWindow();
                window.Title = "window_" + Convert.ToChar(0x41 + i) + " #1";
                DockContainer.launch(window);
                WindowManager.addWindow(window);
            }
            WindowManager.createGroup("Group 1");
            for (int i = 0; i < 4; i++)
            {
                GUI.ODockWindow window = new GUI.ODockWindow();
                window.Title = "window_" + Convert.ToChar(0x41 + i) + " #2";
                DockContainer.launch(window);
                WindowManager.addWindow(window);
            }
            WindowManager.createGroup("Group 2");

            this.MaximumSize = Screen.PrimaryScreen.WorkingArea.Size;
            this.WindowState = FormWindowState.Maximized;
            MainMenu.Renderer = new GUI.OMenuStrip();
            this.ResumeLayout();
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
            OpenFileDialog OpenDlg = new OpenFileDialog();
            OpenDlg.Filter = "Binary CTR H3D File|*.bch";
            if (OpenDlg.ShowDialog() == DialogResult.OK)
            {
                Ohana.BCH.load(OpenDlg.FileName);
            }
        }
    }
}
