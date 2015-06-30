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
            this.TopControls = new System.Windows.Forms.TableLayoutPanel();
            this.BtnDelete = new Ohana3DS_Rebirth.GUI.OButton();
            this.BtnClear = new Ohana3DS_Rebirth.GUI.OButton();
            this.BtnExport = new Ohana3DS_Rebirth.GUI.OButton();
            this.BtnImport = new Ohana3DS_Rebirth.GUI.OButton();
            this.TopControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // TextureList
            // 
            this.TextureList.BackColor = System.Drawing.Color.Transparent;
            this.TextureList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextureList.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextureList.HeaderHeight = 24;
            this.TextureList.ItemHeight = 128;
            this.TextureList.Location = new System.Drawing.Point(0, 40);
            this.TextureList.Name = "TextureList";
            this.TextureList.SelectedIndex = -1;
            this.TextureList.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(15)))), ((int)(((byte)(82)))), ((int)(((byte)(186)))));
            this.TextureList.Size = new System.Drawing.Size(256, 360);
            this.TextureList.TabIndex = 1;
            // 
            // TopControls
            // 
            this.TopControls.ColumnCount = 4;
            this.TopControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.TopControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.TopControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.TopControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.TopControls.Controls.Add(this.BtnDelete, 0, 0);
            this.TopControls.Controls.Add(this.BtnClear, 0, 0);
            this.TopControls.Controls.Add(this.BtnExport, 0, 0);
            this.TopControls.Controls.Add(this.BtnImport, 0, 0);
            this.TopControls.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopControls.Location = new System.Drawing.Point(0, 16);
            this.TopControls.Name = "TopControls";
            this.TopControls.RowCount = 1;
            this.TopControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TopControls.Size = new System.Drawing.Size(256, 24);
            this.TopControls.TabIndex = 5;
            // 
            // BtnDelete
            // 
            this.BtnDelete.BackColor = System.Drawing.Color.Transparent;
            this.BtnDelete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnDelete.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDelete.Image = global::Ohana3DS_Rebirth.Properties.Resources.icn_close;
            this.BtnDelete.Location = new System.Drawing.Point(131, 3);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(58, 18);
            this.BtnDelete.TabIndex = 9;
            this.BtnDelete.Text = "Delete";
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // BtnClear
            // 
            this.BtnClear.BackColor = System.Drawing.Color.Transparent;
            this.BtnClear.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnClear.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnClear.Image = global::Ohana3DS_Rebirth.Properties.Resources.icn_trash;
            this.BtnClear.Location = new System.Drawing.Point(195, 3);
            this.BtnClear.Name = "BtnClear";
            this.BtnClear.Size = new System.Drawing.Size(58, 18);
            this.BtnClear.TabIndex = 8;
            this.BtnClear.Text = "Clear";
            this.BtnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // BtnExport
            // 
            this.BtnExport.BackColor = System.Drawing.Color.Transparent;
            this.BtnExport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnExport.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnExport.Image = global::Ohana3DS_Rebirth.Properties.Resources.icn_arrow_up;
            this.BtnExport.Location = new System.Drawing.Point(3, 3);
            this.BtnExport.Name = "BtnExport";
            this.BtnExport.Size = new System.Drawing.Size(58, 18);
            this.BtnExport.TabIndex = 7;
            this.BtnExport.Text = "Export";
            // 
            // BtnImport
            // 
            this.BtnImport.BackColor = System.Drawing.Color.Transparent;
            this.BtnImport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnImport.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnImport.Image = global::Ohana3DS_Rebirth.Properties.Resources.icn_arrow_down;
            this.BtnImport.Location = new System.Drawing.Point(67, 3);
            this.BtnImport.Name = "BtnImport";
            this.BtnImport.Size = new System.Drawing.Size(58, 18);
            this.BtnImport.TabIndex = 6;
            this.BtnImport.Text = "Import";
            this.BtnImport.Click += new System.EventHandler(this.BtnImport_Click);
            // 
            // OTextureWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TextureList);
            this.Controls.Add(this.TopControls);
            this.Name = "OTextureWindow";
            this.Size = new System.Drawing.Size(256, 400);
            this.Controls.SetChildIndex(this.TopControls, 0);
            this.Controls.SetChildIndex(this.TextureList, 0);
            this.TopControls.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private OList TextureList;
        private System.Windows.Forms.TableLayoutPanel TopControls;
        private OButton BtnDelete;
        private OButton BtnClear;
        private OButton BtnExport;
        private OButton BtnImport;

    }
}
