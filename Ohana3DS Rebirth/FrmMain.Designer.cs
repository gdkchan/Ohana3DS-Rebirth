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
            this.ContentContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // LblTitle
            // 
            this.LblTitle.Size = new System.Drawing.Size(76, 19);
            this.LblTitle.Text = "Ohana3DS";
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
            // DockContainer
            // 
            this.DockContainer.BackColor = System.Drawing.Color.Transparent;
            this.DockContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DockContainer.Location = new System.Drawing.Point(0, 28);
            this.DockContainer.Name = "DockContainer";
            this.DockContainer.Size = new System.Drawing.Size(632, 416);
            this.DockContainer.TabIndex = 14;
            this.DockContainer.Text = "oDock1";
            // 
            // WindowManager
            // 
            this.WindowManager.BackColor = System.Drawing.Color.Transparent;
            this.WindowManager.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.WindowManager.Location = new System.Drawing.Point(0, 444);
            this.WindowManager.Name = "WindowManager";
            this.WindowManager.Size = new System.Drawing.Size(632, 28);
            this.WindowManager.TabIndex = 15;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(640, 480);
            this.MinimumSize = new System.Drawing.Size(256, 128);
            this.Name = "FrmMain";
            this.Text = "Ohana3DS";
            this.ContentContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private GUI.ODock DockContainer;
        private GUI.OWindowManager WindowManager;


    }
}
