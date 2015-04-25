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
            this.DockContainer = new Ohana3DS_Rebirth.GUI.ODock();
            this.WindowManager = new Ohana3DS_Rebirth.GUI.OWindowManager(this.components);
            this.MainMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuImport = new System.Windows.Forms.ToolStripMenuItem();
            this.ContentContainer.SuspendLayout();
            this.MainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // ContentContainer
            // 
            this.ContentContainer.Controls.Add(this.DockContainer);
            this.ContentContainer.Controls.Add(this.WindowManager);
            this.ContentContainer.MinimumSize = new System.Drawing.Size(128, 32);
            this.ContentContainer.Size = new System.Drawing.Size(632, 472);
            this.ContentContainer.Controls.SetChildIndex(this.WindowManager, 0);
            this.ContentContainer.Controls.SetChildIndex(this.DockContainer, 0);
            // 
            // LblTitle
            // 
            this.LblTitle.Size = new System.Drawing.Size(76, 19);
            this.LblTitle.Text = "Ohana3DS";
            this.LblTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LblTitle_MouseDown);
            this.LblTitle.MouseEnter += new System.EventHandler(this.LblTitle_MouseEnter);
            this.LblTitle.MouseLeave += new System.EventHandler(this.LblTitle_MouseLeave);
            // 
            // DockContainer
            // 
            this.DockContainer.BackColor = System.Drawing.Color.Transparent;
            this.DockContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DockContainer.Location = new System.Drawing.Point(0, 20);
            this.DockContainer.Name = "DockContainer";
            this.DockContainer.Size = new System.Drawing.Size(632, 428);
            this.DockContainer.TabIndex = 14;
            // 
            // WindowManager
            // 
            this.WindowManager.BackColor = System.Drawing.Color.Transparent;
            this.WindowManager.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.WindowManager.Location = new System.Drawing.Point(0, 448);
            this.WindowManager.Name = "WindowManager";
            this.WindowManager.Size = new System.Drawing.Size(632, 24);
            this.WindowManager.TabIndex = 15;
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuOpen,
            this.mnuImport});
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(153, 70);
            // 
            // mnuOpen
            // 
            this.mnuOpen.Name = "mnuOpen";
            this.mnuOpen.Size = new System.Drawing.Size(152, 22);
            this.mnuOpen.Text = "&Open";
            this.mnuOpen.Click += new System.EventHandler(this.mnuOpen_Click);
            // 
            // mnuImport
            // 
            this.mnuImport.Name = "mnuImport";
            this.mnuImport.Size = new System.Drawing.Size(152, 22);
            this.mnuImport.Text = "&Import";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(640, 480);
            this.MinimumSize = new System.Drawing.Size(256, 128);
            this.Name = "FrmMain";
            this.Text = "Ohana3DS";
            this.Controls.SetChildIndex(this.ContentContainer, 0);
            this.ContentContainer.ResumeLayout(false);
            this.MainMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private GUI.ODock DockContainer;
        private GUI.OWindowManager WindowManager;
        private System.Windows.Forms.ContextMenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem mnuOpen;
        private System.Windows.Forms.ToolStripMenuItem mnuImport;
    }
}
