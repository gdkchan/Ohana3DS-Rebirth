namespace Ohana3DS_Rebirth.GUI
{
    partial class OModelWindow
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

        #region código gerado pelo Component Designer

        /// <summary> 
        /// Método necessário para o suporte do Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Screen = new System.Windows.Forms.PictureBox();
            this.ModelMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MnuImport = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuExport = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuClear = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.Screen)).BeginInit();
            this.ModelMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // Screen
            // 
            this.Screen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Screen.Location = new System.Drawing.Point(0, 16);
            this.Screen.Name = "Screen";
            this.Screen.Size = new System.Drawing.Size(640, 480);
            this.Screen.TabIndex = 1;
            this.Screen.TabStop = false;
            this.Screen.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Screen_MouseDown);
            this.Screen.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Screen_MouseMove);
            this.Screen.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Screen_MouseUp);
            this.Screen.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.Screen_MouseWheel);
            this.Screen.Resize += new System.EventHandler(this.Screen_Resize);
            // 
            // ModelMenu
            // 
            this.ModelMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MnuImport,
            this.MnuExport,
            this.MnuClear});
            this.ModelMenu.Name = "ModelMenu";
            this.ModelMenu.Size = new System.Drawing.Size(111, 70);
            // 
            // MnuImport
            // 
            this.MnuImport.Image = global::Ohana3DS_Rebirth.Properties.Resources.icn_arrow_down;
            this.MnuImport.Name = "MnuImport";
            this.MnuImport.Size = new System.Drawing.Size(110, 22);
            this.MnuImport.Text = "&Import";
            this.MnuImport.Click += new System.EventHandler(this.MnuImport_Click);
            // 
            // MnuExport
            // 
            this.MnuExport.Image = global::Ohana3DS_Rebirth.Properties.Resources.icn_arrow_up;
            this.MnuExport.Name = "MnuExport";
            this.MnuExport.Size = new System.Drawing.Size(110, 22);
            this.MnuExport.Text = "&Export";
            // 
            // MnuClear
            // 
            this.MnuClear.Image = global::Ohana3DS_Rebirth.Properties.Resources.icn_trash;
            this.MnuClear.Name = "MnuClear";
            this.MnuClear.Size = new System.Drawing.Size(110, 22);
            this.MnuClear.Text = "&Clear";
            this.MnuClear.Click += new System.EventHandler(this.MnuClear_Click);
            // 
            // OModelWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Screen);
            this.Name = "OModelWindow";
            this.Size = new System.Drawing.Size(640, 496);
            this.Controls.SetChildIndex(this.Screen, 0);
            ((System.ComponentModel.ISupportInitialize)(this.Screen)).EndInit();
            this.ModelMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox Screen;
        private System.Windows.Forms.ContextMenuStrip ModelMenu;
        private System.Windows.Forms.ToolStripMenuItem MnuImport;
        private System.Windows.Forms.ToolStripMenuItem MnuExport;
        private System.Windows.Forms.ToolStripMenuItem MnuClear;




    }
}
