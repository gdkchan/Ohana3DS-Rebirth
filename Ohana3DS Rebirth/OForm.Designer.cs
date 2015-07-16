namespace Ohana3DS_Rebirth
{
    partial class OForm
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

        #region código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte do Designer - não modifique
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OForm));
            this.ContentContainer = new System.Windows.Forms.Panel();
            this.TitleBar = new System.Windows.Forms.Panel();
            this.BtnMinimize = new System.Windows.Forms.PictureBox();
            this.BtnMinMax = new System.Windows.Forms.PictureBox();
            this.BtnClose = new System.Windows.Forms.PictureBox();
            this.LblTitle = new Ohana3DS_Rebirth.GUI.OButton();
            this.ContentContainer.SuspendLayout();
            this.TitleBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BtnMinimize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnMinMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnClose)).BeginInit();
            this.SuspendLayout();
            // 
            // ContentContainer
            // 
            this.ContentContainer.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ContentContainer.BackgroundImage")));
            this.ContentContainer.Controls.Add(this.TitleBar);
            this.ContentContainer.Location = new System.Drawing.Point(4, 4);
            this.ContentContainer.MinimumSize = new System.Drawing.Size(128, 64);
            this.ContentContainer.Name = "ContentContainer";
            this.ContentContainer.Size = new System.Drawing.Size(640, 472);
            this.ContentContainer.TabIndex = 0;
            // 
            // TitleBar
            // 
            this.TitleBar.BackColor = System.Drawing.Color.Transparent;
            this.TitleBar.Controls.Add(this.LblTitle);
            this.TitleBar.Controls.Add(this.BtnMinimize);
            this.TitleBar.Controls.Add(this.BtnMinMax);
            this.TitleBar.Controls.Add(this.BtnClose);
            this.TitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.TitleBar.Location = new System.Drawing.Point(0, 0);
            this.TitleBar.Name = "TitleBar";
            this.TitleBar.Size = new System.Drawing.Size(640, 20);
            this.TitleBar.TabIndex = 9;
            this.TitleBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OForm_MouseDown);
            // 
            // BtnMinimize
            // 
            this.BtnMinimize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnMinimize.BackColor = System.Drawing.Color.Transparent;
            this.BtnMinimize.Image = ((System.Drawing.Image)(resources.GetObject("BtnMinimize.Image")));
            this.BtnMinimize.Location = new System.Drawing.Point(592, 0);
            this.BtnMinimize.Name = "BtnMinimize";
            this.BtnMinimize.Size = new System.Drawing.Size(16, 20);
            this.BtnMinimize.TabIndex = 3;
            this.BtnMinimize.TabStop = false;
            this.BtnMinimize.Click += new System.EventHandler(this.BtnMinimize_Click);
            this.BtnMinimize.MouseEnter += new System.EventHandler(this.BtnMinimize_MouseEnter);
            this.BtnMinimize.MouseLeave += new System.EventHandler(this.Btn_MouseLeave);
            // 
            // BtnMinMax
            // 
            this.BtnMinMax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnMinMax.BackColor = System.Drawing.Color.Transparent;
            this.BtnMinMax.Image = global::Ohana3DS_Rebirth.Properties.Resources.btnmaximize;
            this.BtnMinMax.Location = new System.Drawing.Point(608, 0);
            this.BtnMinMax.Name = "BtnMinMax";
            this.BtnMinMax.Size = new System.Drawing.Size(16, 20);
            this.BtnMinMax.TabIndex = 2;
            this.BtnMinMax.TabStop = false;
            this.BtnMinMax.Click += new System.EventHandler(this.BtnMinMax_Click);
            this.BtnMinMax.MouseEnter += new System.EventHandler(this.BtnMinMax_MouseEnter);
            this.BtnMinMax.MouseLeave += new System.EventHandler(this.Btn_MouseLeave);
            // 
            // BtnClose
            // 
            this.BtnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnClose.BackColor = System.Drawing.Color.Transparent;
            this.BtnClose.Image = ((System.Drawing.Image)(resources.GetObject("BtnClose.Image")));
            this.BtnClose.Location = new System.Drawing.Point(624, 0);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(16, 20);
            this.BtnClose.TabIndex = 1;
            this.BtnClose.TabStop = false;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            this.BtnClose.MouseEnter += new System.EventHandler(this.BtnClose_MouseEnter);
            this.BtnClose.MouseLeave += new System.EventHandler(this.Btn_MouseLeave);
            // 
            // LblTitle
            // 
            this.LblTitle.BackColor = System.Drawing.Color.Transparent;
            this.LblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTitle.ForeColor = System.Drawing.Color.White;
            this.LblTitle.Location = new System.Drawing.Point(0, 0);
            this.LblTitle.Name = "LblTitle";
            this.LblTitle.Size = new System.Drawing.Size(31, 19);
            this.LblTitle.TabIndex = 12;
            this.LblTitle.Text = "Title";
            this.LblTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OForm_MouseDown);
            // 
            // OForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(648, 480);
            this.Controls.Add(this.ContentContainer);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(128, 64);
            this.Name = "OForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Título";
            this.ResizeBegin += new System.EventHandler(this.OForm_ResizeBegin);
            this.ResizeEnd += new System.EventHandler(this.OForm_ResizeEnd);
            this.Layout += new System.Windows.Forms.LayoutEventHandler(this.OForm_Layout);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OForm_MouseDown);
            this.Resize += new System.EventHandler(this.OForm_Resize);
            this.ContentContainer.ResumeLayout(false);
            this.TitleBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BtnMinimize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnMinMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnClose)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox BtnMinimize;
        private System.Windows.Forms.PictureBox BtnMinMax;
        private System.Windows.Forms.PictureBox BtnClose;
        protected System.Windows.Forms.Panel ContentContainer;
        private System.Windows.Forms.Panel TitleBar;
        protected GUI.OButton LblTitle;
    }
}

