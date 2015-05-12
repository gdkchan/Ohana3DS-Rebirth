namespace Ohana3DS_Rebirth.GUI
{
    partial class OTextureWindow
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
            this.TextureList = new Ohana3DS_Rebirth.GUI.OList();
            this.SuspendLayout();
            // 
            // TextureList
            // 
            this.TextureList.BackColor = System.Drawing.Color.Transparent;
            this.TextureList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextureList.Location = new System.Drawing.Point(0, 16);
            this.TextureList.Name = "TextureList";
            this.TextureList.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(15)))), ((int)(((byte)(82)))), ((int)(((byte)(186)))));
            this.TextureList.ItemHeight = 128;
            this.TextureList.Size = new System.Drawing.Size(256, 384);
            this.TextureList.TabIndex = 1;
            // 
            // OTextureWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TextureList);
            this.Name = "OTextureWindow";
            this.Size = new System.Drawing.Size(256, 400);
            this.Controls.SetChildIndex(this.TextureList, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private OList TextureList;

    }
}
