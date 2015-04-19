namespace Ohana3DS_Rebirth
{
    partial class FrmMain
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">verdade se for necessário descartar os recursos gerenciados; caso contrário, falso.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte do Designer - não modifique
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.MnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.Status = new Ohana3DS_Rebirth.GUI.OStatusStrip(this.components);
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.DockContainer = new Ohana3DS_Rebirth.GUI.ODock();
            this.MainMenu.SuspendLayout();
            this.Status.SuspendLayout();
            this.SuspendLayout();
            // 
            // LblTitle
            // 
            this.LblTitle.Size = new System.Drawing.Size(76, 19);
            this.LblTitle.Text = "Ohana3DS";
            // 
            // MainMenu
            // 
            this.MainMenu.BackColor = System.Drawing.SystemColors.Control;
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MnuFile});
            this.MainMenu.Location = new System.Drawing.Point(0, 28);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(640, 24);
            this.MainMenu.TabIndex = 10;
            // 
            // MnuFile
            // 
            this.MnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MnuOpen});
            this.MnuFile.Name = "MnuFile";
            this.MnuFile.Size = new System.Drawing.Size(37, 20);
            this.MnuFile.Text = "&File";
            // 
            // MnuOpen
            // 
            this.MnuOpen.Name = "MnuOpen";
            this.MnuOpen.Size = new System.Drawing.Size(103, 22);
            this.MnuOpen.Text = "&Open";
            this.MnuOpen.Click += new System.EventHandler(this.MnuOpen_Click);
            // 
            // Status
            // 
            this.Status.BackColor = System.Drawing.Color.Black;
            this.Status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.Status.Location = new System.Drawing.Point(0, 458);
            this.Status.Name = "Status";
            this.Status.Size = new System.Drawing.Size(640, 22);
            this.Status.TabIndex = 11;
            this.Status.Text = "oStatusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.ForeColor = System.Drawing.Color.White;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // DockContainer
            // 
            this.DockContainer.BackColor = System.Drawing.Color.Transparent;
            this.DockContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DockContainer.Location = new System.Drawing.Point(0, 52);
            this.DockContainer.Name = "DockContainer";
            this.DockContainer.Size = new System.Drawing.Size(640, 406);
            this.DockContainer.TabIndex = 12;
            this.DockContainer.Text = "oDock1";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(640, 480);
            this.Controls.Add(this.DockContainer);
            this.Controls.Add(this.Status);
            this.Controls.Add(this.MainMenu);
            this.MainMenuStrip = this.MainMenu;
            this.Name = "FrmMain";
            this.Text = "Ohana3DS";
            this.Layout += new System.Windows.Forms.LayoutEventHandler(this.FrmMain_Layout);
            this.Controls.SetChildIndex(this.MainMenu, 0);
            this.Controls.SetChildIndex(this.Status, 0);
            this.Controls.SetChildIndex(this.DockContainer, 0);
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.Status.ResumeLayout(false);
            this.Status.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem MnuFile;
        private GUI.OStatusStrip Status;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripMenuItem MnuOpen;
        private GUI.ODock DockContainer;




    }
}
