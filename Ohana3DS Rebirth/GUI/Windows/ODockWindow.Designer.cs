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
            this.BtnClose = new System.Windows.Forms.PictureBox();
            this.ContentContainer = new System.Windows.Forms.Panel();
            this.WindowTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BtnPin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnClose)).BeginInit();
            this.ContentContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // WindowTop
            // 
            this.WindowTop.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("WindowTop.BackgroundImage")));
            this.WindowTop.Controls.Add(this.LblTitle);
            this.WindowTop.Controls.Add(this.BtnPin);
            this.WindowTop.Controls.Add(this.BtnClose);
            this.WindowTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.WindowTop.Location = new System.Drawing.Point(0, 0);
            this.WindowTop.Name = "WindowTop";
            this.WindowTop.Size = new System.Drawing.Size(248, 16);
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
            this.BtnPin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.BtnPin.Image = global::Ohana3DS_Rebirth.Properties.Resources.icn_dockable;
            this.BtnPin.Location = new System.Drawing.Point(216, 0);
            this.BtnPin.Name = "BtnPin";
            this.BtnPin.Size = new System.Drawing.Size(16, 16);
            this.BtnPin.TabIndex = 6;
            this.BtnPin.TabStop = false;
            this.BtnPin.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnPin_MouseDown);
            this.BtnPin.MouseEnter += new System.EventHandler(this.BtnPin_MouseEnter);
            this.BtnPin.MouseLeave += new System.EventHandler(this.BtnPin_MouseLeave);
            // 
            // BtnClose
            // 
            this.BtnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.BtnClose.Image = global::Ohana3DS_Rebirth.Properties.Resources.icn_close;
            this.BtnClose.Location = new System.Drawing.Point(232, 0);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(16, 16);
            this.BtnClose.TabIndex = 4;
            this.BtnClose.TabStop = false;
            this.BtnClose.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnClose_MouseDown);
            this.BtnClose.MouseEnter += new System.EventHandler(this.BtnClose_MouseEnter);
            this.BtnClose.MouseLeave += new System.EventHandler(this.BtnClose_MouseLeave);
            // 
            // ContentContainer
            // 
            this.ContentContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.ContentContainer.Controls.Add(this.WindowTop);
            this.ContentContainer.Location = new System.Drawing.Point(4, 4);
            this.ContentContainer.Name = "ContentContainer";
            this.ContentContainer.Size = new System.Drawing.Size(248, 248);
            this.ContentContainer.TabIndex = 1;
            // 
            // ODockWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.ContentContainer);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.Name = "ODockWindow";
            this.Size = new System.Drawing.Size(256, 256);
            this.Layout += new System.Windows.Forms.LayoutEventHandler(this.ODockWindow_Layout);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ODockWindow_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ODockWindow_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ODockWindow_MouseUp);
            this.WindowTop.ResumeLayout(false);
            this.WindowTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BtnPin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnClose)).EndInit();
            this.ContentContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox BtnPin;
        private System.Windows.Forms.PictureBox BtnClose;
        private System.Windows.Forms.Panel WindowTop;
        private System.Windows.Forms.Label LblTitle;
        protected System.Windows.Forms.Panel ContentContainer;
    }
}
