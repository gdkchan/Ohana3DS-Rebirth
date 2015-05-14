namespace Ohana3DS_Rebirth.GUI
{
    partial class OList
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
            this.ListScroll = new Ohana3DS_Rebirth.GUI.OVScroll(this.components);
            this.SuspendLayout();
            // 
            // ListScroll
            // 
            this.ListScroll.BarColor = System.Drawing.Color.White;
            this.ListScroll.BarColorHover = System.Drawing.Color.WhiteSmoke;
            this.ListScroll.Dock = System.Windows.Forms.DockStyle.Right;
            this.ListScroll.Location = new System.Drawing.Point(248, 0);
            this.ListScroll.MaximumScroll = 100;
            this.ListScroll.Name = "ListScroll";
            this.ListScroll.Size = new System.Drawing.Size(8, 400);
            this.ListScroll.TabIndex = 0;
            this.ListScroll.Text = "ovScroll1";
            this.ListScroll.Value = 0;
            this.ListScroll.Visible = false;
            this.ListScroll.ScrollChanged += new System.EventHandler(this.ListScroll_ScrollChanged);
            // 
            // OList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.ListScroll);
            this.Name = "OList";
            this.Size = new System.Drawing.Size(256, 400);
            this.ResumeLayout(false);

        }

        #endregion

        private OVScroll ListScroll;
    }
}
