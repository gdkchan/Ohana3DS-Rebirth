namespace Ohana3DS_Rebirth.GUI
{
    partial class OScrollablePanel
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
            this.PnlVScroll = new Ohana3DS_Rebirth.GUI.OVScroll(this.components);
            this.ContentPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // PnlVScroll
            // 
            this.PnlVScroll.BarColor = System.Drawing.Color.White;
            this.PnlVScroll.BarColorHover = System.Drawing.Color.WhiteSmoke;
            this.PnlVScroll.Location = new System.Drawing.Point(248, 0);
            this.PnlVScroll.MaximumScroll = 100;
            this.PnlVScroll.Name = "PnlVScroll";
            this.PnlVScroll.Size = new System.Drawing.Size(8, 256);
            this.PnlVScroll.TabIndex = 1;
            this.PnlVScroll.Value = 0;
            this.PnlVScroll.ScrollChanged += new System.EventHandler(this.VScroll_ScrollChanged);
            // 
            // ContentPanel
            // 
            this.ContentPanel.BackColor = System.Drawing.Color.Transparent;
            this.ContentPanel.Location = new System.Drawing.Point(0, 0);
            this.ContentPanel.Name = "ContentPanel";
            this.ContentPanel.Size = new System.Drawing.Size(248, 256);
            this.ContentPanel.TabIndex = 2;
            // 
            // OScrollablePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.ContentPanel);
            this.Controls.Add(this.PnlVScroll);
            this.Name = "OScrollablePanel";
            this.Size = new System.Drawing.Size(256, 256);
            this.ResumeLayout(false);

        }

        #endregion

        private OVScroll PnlVScroll;
        private System.Windows.Forms.Panel ContentPanel;
    }
}
