namespace Ohana3DS_Rebirth.GUI.Forms
{
    partial class OTextureExportForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.BtnOk = new Ohana3DS_Rebirth.GUI.OButton();
            this.BtnCancel = new Ohana3DS_Rebirth.GUI.OButton();
            this.ChkExportAllTextures = new Ohana3DS_Rebirth.GUI.OCheckBox();
            this.TxtTextureName = new Ohana3DS_Rebirth.GUI.OTextBox();
            this.LblDummyTextureName = new Ohana3DS_Rebirth.GUI.OLabel();
            this.BtnBrowseFolder = new Ohana3DS_Rebirth.GUI.OButton();
            this.TxtOutFolder = new Ohana3DS_Rebirth.GUI.OTextBox();
            this.LblDummyOutFolder = new Ohana3DS_Rebirth.GUI.OLabel();
            this.ContentContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // ContentContainer
            // 
            this.ContentContainer.Controls.Add(this.BtnOk);
            this.ContentContainer.Controls.Add(this.BtnCancel);
            this.ContentContainer.Controls.Add(this.ChkExportAllTextures);
            this.ContentContainer.Controls.Add(this.TxtTextureName);
            this.ContentContainer.Controls.Add(this.LblDummyTextureName);
            this.ContentContainer.Controls.Add(this.BtnBrowseFolder);
            this.ContentContainer.Controls.Add(this.TxtOutFolder);
            this.ContentContainer.Controls.Add(this.LblDummyOutFolder);
            this.ContentContainer.Location = new System.Drawing.Point(0, 0);
            this.ContentContainer.Size = new System.Drawing.Size(384, 384);
            this.ContentContainer.Controls.SetChildIndex(this.LblDummyOutFolder, 0);
            this.ContentContainer.Controls.SetChildIndex(this.TxtOutFolder, 0);
            this.ContentContainer.Controls.SetChildIndex(this.BtnBrowseFolder, 0);
            this.ContentContainer.Controls.SetChildIndex(this.LblDummyTextureName, 0);
            this.ContentContainer.Controls.SetChildIndex(this.TxtTextureName, 0);
            this.ContentContainer.Controls.SetChildIndex(this.ChkExportAllTextures, 0);
            this.ContentContainer.Controls.SetChildIndex(this.BtnCancel, 0);
            this.ContentContainer.Controls.SetChildIndex(this.BtnOk, 0);
            // 
            // LblTitle
            // 
            this.LblTitle.Size = new System.Drawing.Size(105, 19);
            this.LblTitle.Text = "Texture exporter";
            // 
            // BtnOk
            // 
            this.BtnOk.Centered = true;
            this.BtnOk.Hover = true;
            this.BtnOk.Image = global::Ohana3DS_Rebirth.Properties.Resources.icn_ticked;
            this.BtnOk.Location = new System.Drawing.Point(238, 348);
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.Size = new System.Drawing.Size(64, 24);
            this.BtnOk.TabIndex = 32;
            this.BtnOk.Text = "OK";
            this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Centered = true;
            this.BtnCancel.Hover = true;
            this.BtnCancel.Image = global::Ohana3DS_Rebirth.Properties.Resources.icn_close;
            this.BtnCancel.Location = new System.Drawing.Point(308, 348);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(64, 24);
            this.BtnCancel.TabIndex = 31;
            this.BtnCancel.Text = "Cancel";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // ChkExportAllTextures
            // 
            this.ChkExportAllTextures.AutomaticSize = true;
            this.ChkExportAllTextures.BackColor = System.Drawing.Color.Transparent;
            this.ChkExportAllTextures.BoxColor = System.Drawing.Color.Black;
            this.ChkExportAllTextures.Checked = false;
            this.ChkExportAllTextures.Location = new System.Drawing.Point(8, 122);
            this.ChkExportAllTextures.Name = "ChkExportAllTextures";
            this.ChkExportAllTextures.Size = new System.Drawing.Size(245, 16);
            this.ChkExportAllTextures.TabIndex = 30;
            this.ChkExportAllTextures.Text = "Export all textures (with their internal names)";
            this.ChkExportAllTextures.CheckedChanged += new System.EventHandler(this.ChkExportAllTextures_CheckedChanged);
            // 
            // TxtTextureName
            // 
            this.TxtTextureName.BackColor = System.Drawing.Color.Black;
            this.TxtTextureName.CharacterWhiteList = null;
            this.TxtTextureName.Location = new System.Drawing.Point(8, 96);
            this.TxtTextureName.Name = "TxtTextureName";
            this.TxtTextureName.Size = new System.Drawing.Size(364, 20);
            this.TxtTextureName.TabIndex = 29;
            this.TxtTextureName.Text = "texture_name";
            // 
            // LblDummyTextureName
            // 
            this.LblDummyTextureName.AutomaticSize = true;
            this.LblDummyTextureName.Centered = false;
            this.LblDummyTextureName.Location = new System.Drawing.Point(8, 74);
            this.LblDummyTextureName.Name = "LblDummyTextureName";
            this.LblDummyTextureName.Size = new System.Drawing.Size(72, 16);
            this.LblDummyTextureName.TabIndex = 28;
            this.LblDummyTextureName.Text = "Texture name:";
            // 
            // BtnBrowseFolder
            // 
            this.BtnBrowseFolder.Centered = true;
            this.BtnBrowseFolder.Hover = true;
            this.BtnBrowseFolder.Image = global::Ohana3DS_Rebirth.Properties.Resources.icn_open;
            this.BtnBrowseFolder.Location = new System.Drawing.Point(340, 48);
            this.BtnBrowseFolder.Name = "BtnBrowseFolder";
            this.BtnBrowseFolder.Size = new System.Drawing.Size(32, 20);
            this.BtnBrowseFolder.TabIndex = 27;
            this.BtnBrowseFolder.Click += new System.EventHandler(this.BtnBrowseFolder_Click);
            // 
            // TxtOutFolder
            // 
            this.TxtOutFolder.BackColor = System.Drawing.Color.Black;
            this.TxtOutFolder.CharacterWhiteList = null;
            this.TxtOutFolder.Location = new System.Drawing.Point(8, 48);
            this.TxtOutFolder.Name = "TxtOutFolder";
            this.TxtOutFolder.Size = new System.Drawing.Size(326, 20);
            this.TxtOutFolder.TabIndex = 26;
            this.TxtOutFolder.Text = "C:\\";
            // 
            // LblDummyOutFolder
            // 
            this.LblDummyOutFolder.AutomaticSize = true;
            this.LblDummyOutFolder.Centered = false;
            this.LblDummyOutFolder.Location = new System.Drawing.Point(8, 26);
            this.LblDummyOutFolder.Name = "LblDummyOutFolder";
            this.LblDummyOutFolder.Size = new System.Drawing.Size(73, 16);
            this.LblDummyOutFolder.TabIndex = 25;
            this.LblDummyOutFolder.Text = "Output folder:";
            // 
            // OTextureExportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 384);
            this.KeyPreview = true;
            this.Name = "OTextureExportForm";
            this.Resizable = false;
            this.ShowMinimize = false;
            this.Text = "OTextureExportForm";
            this.Load += new System.EventHandler(this.OTextureExportForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OTextureExportForm_KeyDown);
            this.ContentContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private OButton BtnOk;
        private OButton BtnCancel;
        private OCheckBox ChkExportAllTextures;
        private OTextBox TxtTextureName;
        private OLabel LblDummyTextureName;
        private OButton BtnBrowseFolder;
        private OTextBox TxtOutFolder;
        private OLabel LblDummyOutFolder;
    }
}