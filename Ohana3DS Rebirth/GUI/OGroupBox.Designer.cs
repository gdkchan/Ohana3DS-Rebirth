namespace Ohana3DS_Rebirth.GUI
{
    partial class OGroupBox
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
            this.ContentPanel = new System.Windows.Forms.Panel();
            this.BtnToggle = new System.Windows.Forms.PictureBox();
            this.LblTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.BtnToggle)).BeginInit();
            this.SuspendLayout();
            // 
            // ContentPanel
            // 
            this.ContentPanel.Location = new System.Drawing.Point(1, 16);
            this.ContentPanel.Name = "ContentPanel";
            this.ContentPanel.Size = new System.Drawing.Size(254, 239);
            this.ContentPanel.TabIndex = 0;
            // 
            // BtnToggle
            // 
            this.BtnToggle.BackColor = System.Drawing.Color.Transparent;
            this.BtnToggle.Image = global::Ohana3DS_Rebirth.Properties.Resources.icn_collapse;
            this.BtnToggle.Location = new System.Drawing.Point(240, 0);
            this.BtnToggle.Name = "BtnToggle";
            this.BtnToggle.Size = new System.Drawing.Size(16, 16);
            this.BtnToggle.TabIndex = 5;
            this.BtnToggle.TabStop = false;
            this.BtnToggle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnToggle_MouseDown);
            this.BtnToggle.MouseEnter += new System.EventHandler(this.BtnToggle_MouseEnter);
            this.BtnToggle.MouseLeave += new System.EventHandler(this.BtnToggle_MouseLeave);
            // 
            // LblTitle
            // 
            this.LblTitle.AutoSize = true;
            this.LblTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTitle.ForeColor = System.Drawing.Color.White;
            this.LblTitle.Location = new System.Drawing.Point(0, 0);
            this.LblTitle.Name = "LblTitle";
            this.LblTitle.Size = new System.Drawing.Size(30, 15);
            this.LblTitle.TabIndex = 6;
            this.LblTitle.Text = "Title";
            // 
            // OGroupBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.LblTitle);
            this.Controls.Add(this.BtnToggle);
            this.Controls.Add(this.ContentPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "OGroupBox";
            this.Size = new System.Drawing.Size(256, 256);
            this.Layout += new System.Windows.Forms.LayoutEventHandler(this.OGroupBox_Layout);
            ((System.ComponentModel.ISupportInitialize)(this.BtnToggle)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel ContentPanel;
        private System.Windows.Forms.PictureBox BtnToggle;
        private System.Windows.Forms.Label LblTitle;
    }
}
