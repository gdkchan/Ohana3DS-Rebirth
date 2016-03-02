namespace Ohana3DS_Rebirth.GUI
{
    partial class OImagePanel
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
        /// Método necessário para suporte do Designer - não modifique
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.TopControls = new System.Windows.Forms.TableLayoutPanel();
            this.BtnExport = new Ohana3DS_Rebirth.GUI.OButton();
            this.TexturePreview = new System.Windows.Forms.PictureBox();
            this.TopControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TexturePreview)).BeginInit();
            this.SuspendLayout();
            // 
            // TopControls
            // 
            this.TopControls.ColumnCount = 1;
            this.TopControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.TopControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.TopControls.Controls.Add(this.BtnExport, 0, 0);
            this.TopControls.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopControls.Location = new System.Drawing.Point(0, 0);
            this.TopControls.Name = "TopControls";
            this.TopControls.RowCount = 1;
            this.TopControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TopControls.Size = new System.Drawing.Size(256, 24);
            this.TopControls.TabIndex = 6;
            // 
            // BtnExport
            // 
            this.BtnExport.BackColor = System.Drawing.Color.Transparent;
            this.BtnExport.Centered = true;
            this.BtnExport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnExport.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnExport.Image = global::Ohana3DS_Rebirth.Properties.Resources.ui_icon_upload;
            this.BtnExport.Location = new System.Drawing.Point(2, 2);
            this.BtnExport.Margin = new System.Windows.Forms.Padding(2);
            this.BtnExport.Name = "BtnExport";
            this.BtnExport.Size = new System.Drawing.Size(252, 20);
            this.BtnExport.TabIndex = 7;
            this.BtnExport.Text = "Export";
            this.BtnExport.Click += new System.EventHandler(this.BtnExport_Click);
            // 
            // TexturePreview
            // 
            this.TexturePreview.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.TexturePreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TexturePreview.Location = new System.Drawing.Point(0, 24);
            this.TexturePreview.Name = "TexturePreview";
            this.TexturePreview.Size = new System.Drawing.Size(256, 232);
            this.TexturePreview.TabIndex = 7;
            this.TexturePreview.TabStop = false;
            // 
            // OImagePanel
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.Controls.Add(this.TexturePreview);
            this.Controls.Add(this.TopControls);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "OImagePanel";
            this.Size = new System.Drawing.Size(256, 256);
            this.TopControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TexturePreview)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel TopControls;
        private OButton BtnExport;
        private System.Windows.Forms.PictureBox TexturePreview;
    }
}
