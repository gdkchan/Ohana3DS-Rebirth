namespace Ohana3DS_Rebirth.GUI
{
    partial class ODockWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ODockWindow));
            this.WindowTop = new System.Windows.Forms.Panel();
            this.LblTitle = new System.Windows.Forms.Label();
            this.BtnPin = new System.Windows.Forms.PictureBox();
            this.BtnMax = new System.Windows.Forms.PictureBox();
            this.BtnClose = new System.Windows.Forms.PictureBox();
            this.WindowTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BtnPin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnClose)).BeginInit();
            this.SuspendLayout();
            // 
            // WindowTop
            // 
            this.WindowTop.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("WindowTop.BackgroundImage")));
            this.WindowTop.Controls.Add(this.LblTitle);
            this.WindowTop.Controls.Add(this.BtnPin);
            this.WindowTop.Controls.Add(this.BtnMax);
            this.WindowTop.Controls.Add(this.BtnClose);
            this.WindowTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.WindowTop.Location = new System.Drawing.Point(0, 0);
            this.WindowTop.Name = "WindowTop";
            this.WindowTop.Size = new System.Drawing.Size(256, 16);
            this.WindowTop.TabIndex = 0;
            this.WindowTop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.WindowTop_MouseDown);
            this.WindowTop.MouseMove += new System.Windows.Forms.MouseEventHandler(this.WindowTop_MouseMove);
            this.WindowTop.MouseUp += new System.Windows.Forms.MouseEventHandler(this.WindowTop_MouseUp);
            // 
            // LblTitle
            // 
            this.LblTitle.AutoSize = true;
            this.LblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTitle.Location = new System.Drawing.Point(0, 0);
            this.LblTitle.Name = "LblTitle";
            this.LblTitle.Size = new System.Drawing.Size(30, 15);
            this.LblTitle.TabIndex = 7;
            this.LblTitle.Text = "Title";
            this.LblTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.WindowTop_MouseDown);
            this.LblTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.WindowTop_MouseMove);
            this.LblTitle.MouseUp += new System.Windows.Forms.MouseEventHandler(this.WindowTop_MouseUp);
            // 
            // BtnPin
            // 
            this.BtnPin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnPin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.BtnPin.Image = global::Ohana3DS_Rebirth.Properties.Resources.icn_dockable;
            this.BtnPin.Location = new System.Drawing.Point(208, 0);
            this.BtnPin.Name = "BtnPin";
            this.BtnPin.Size = new System.Drawing.Size(16, 16);
            this.BtnPin.TabIndex = 6;
            this.BtnPin.TabStop = false;
            this.BtnPin.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnPin_MouseDown);
            this.BtnPin.MouseEnter += new System.EventHandler(this.BtnPin_MouseEnter);
            this.BtnPin.MouseLeave += new System.EventHandler(this.BtnPin_MouseLeave);
            // 
            // BtnMax
            // 
            this.BtnMax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnMax.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.BtnMax.Image = global::Ohana3DS_Rebirth.Properties.Resources.icn_maximize;
            this.BtnMax.Location = new System.Drawing.Point(224, 0);
            this.BtnMax.Name = "BtnMax";
            this.BtnMax.Size = new System.Drawing.Size(16, 16);
            this.BtnMax.TabIndex = 5;
            this.BtnMax.TabStop = false;
            this.BtnMax.MouseEnter += new System.EventHandler(this.BtnMax_MouseEnter);
            this.BtnMax.MouseLeave += new System.EventHandler(this.BtnMax_MouseLeave);
            // 
            // BtnClose
            // 
            this.BtnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.BtnClose.Image = global::Ohana3DS_Rebirth.Properties.Resources.icn_close;
            this.BtnClose.Location = new System.Drawing.Point(240, 0);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(16, 16);
            this.BtnClose.TabIndex = 4;
            this.BtnClose.TabStop = false;
            this.BtnClose.MouseEnter += new System.EventHandler(this.BtnClose_MouseEnter);
            this.BtnClose.MouseLeave += new System.EventHandler(this.BtnClose_MouseLeave);
            // 
            // ODockWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.Controls.Add(this.WindowTop);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.Name = "ODockWindow";
            this.Size = new System.Drawing.Size(256, 256);
            this.WindowTop.ResumeLayout(false);
            this.WindowTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BtnPin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnClose)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel WindowTop;
        private System.Windows.Forms.PictureBox BtnPin;
        private System.Windows.Forms.PictureBox BtnMax;
        private System.Windows.Forms.PictureBox BtnClose;
        protected System.Windows.Forms.Label LblTitle;
    }
}
