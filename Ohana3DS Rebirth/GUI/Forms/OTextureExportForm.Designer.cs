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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OTextureExportForm));
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
            this.ContentContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.ContentContainer.Controls.Add(this.BtnOk);
            this.ContentContainer.Controls.Add(this.BtnCancel);
            this.ContentContainer.Controls.Add(this.ChkExportAllTextures);
            this.ContentContainer.Controls.Add(this.TxtTextureName);
            this.ContentContainer.Controls.Add(this.LblDummyTextureName);
            this.ContentContainer.Controls.Add(this.BtnBrowseFolder);
            this.ContentContainer.Controls.Add(this.TxtOutFolder);
            this.ContentContainer.Controls.Add(this.LblDummyOutFolder);
            this.ContentContainer.Location = new System.Drawing.Point(1, 1);
            this.ContentContainer.Size = new System.Drawing.Size(382, 382);
            this.ContentContainer.Controls.SetChildIndex(this.LblDummyOutFolder, 0);
            this.ContentContainer.Controls.SetChildIndex(this.TxtOutFolder, 0);
            this.ContentContainer.Controls.SetChildIndex(this.BtnBrowseFolder, 0);
            this.ContentContainer.Controls.SetChildIndex(this.LblDummyTextureName, 0);
            this.ContentContainer.Controls.SetChildIndex(this.TxtTextureName, 0);
            this.ContentContainer.Controls.SetChildIndex(this.ChkExportAllTextures, 0);
            this.ContentContainer.Controls.SetChildIndex(this.BtnCancel, 0);
            this.ContentContainer.Controls.SetChildIndex(this.BtnOk, 0);
            // 
            // BtnOk
            // 
            this.BtnOk.Centered = true;
            this.BtnOk.Image = global::Ohana3DS_Rebirth.Properties.Resources.ui_icon_tick;
            this.BtnOk.Location = new System.Drawing.Point(237, 347);
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.Size = new System.Drawing.Size(64, 24);
            this.BtnOk.TabIndex = 32;
            this.BtnOk.Text = "OK";
            this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Centered = true;
            this.BtnCancel.Image = global::Ohana3DS_Rebirth.Properties.Resources.ui_icon_block;
            this.BtnCancel.Location = new System.Drawing.Point(307, 347);
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
            this.ChkExportAllTextures.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkExportAllTextures.Location = new System.Drawing.Point(11, 124);
            this.ChkExportAllTextures.Name = "ChkExportAllTextures";
            this.ChkExportAllTextures.Size = new System.Drawing.Size(259, 17);
            this.ChkExportAllTextures.TabIndex = 30;
            this.ChkExportAllTextures.Text = "Export all textures (with their internal names)";
            this.ChkExportAllTextures.CheckedChanged += new System.EventHandler(this.ChkExportAllTextures_CheckedChanged);
            // 
            // TxtTextureName
            // 
            this.TxtTextureName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.TxtTextureName.CharacterWhiteList = null;
            this.TxtTextureName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtTextureName.Location = new System.Drawing.Point(11, 98);
            this.TxtTextureName.Name = "TxtTextureName";
            this.TxtTextureName.Size = new System.Drawing.Size(360, 20);
            this.TxtTextureName.TabIndex = 29;
            this.TxtTextureName.Text = "texture_name";
            // 
            // LblDummyTextureName
            // 
            this.LblDummyTextureName.AutomaticSize = true;
            this.LblDummyTextureName.BackColor = System.Drawing.Color.Transparent;
            this.LblDummyTextureName.Centered = false;
            this.LblDummyTextureName.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblDummyTextureName.Location = new System.Drawing.Point(11, 75);
            this.LblDummyTextureName.Name = "LblDummyTextureName";
            this.LblDummyTextureName.Size = new System.Drawing.Size(78, 17);
            this.LblDummyTextureName.TabIndex = 28;
            this.LblDummyTextureName.Text = "Texture name:";
            // 
            // BtnBrowseFolder
            // 
            this.BtnBrowseFolder.Centered = true;
            this.BtnBrowseFolder.Image = global::Ohana3DS_Rebirth.Properties.Resources.ui_icon_folder;
            this.BtnBrowseFolder.Location = new System.Drawing.Point(339, 49);
            this.BtnBrowseFolder.Name = "BtnBrowseFolder";
            this.BtnBrowseFolder.Size = new System.Drawing.Size(32, 20);
            this.BtnBrowseFolder.TabIndex = 27;
            this.BtnBrowseFolder.Click += new System.EventHandler(this.BtnBrowseFolder_Click);
            // 
            // TxtOutFolder
            // 
            this.TxtOutFolder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.TxtOutFolder.CharacterWhiteList = null;
            this.TxtOutFolder.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtOutFolder.Location = new System.Drawing.Point(11, 49);
            this.TxtOutFolder.Name = "TxtOutFolder";
            this.TxtOutFolder.Size = new System.Drawing.Size(322, 20);
            this.TxtOutFolder.TabIndex = 26;
            this.TxtOutFolder.Text = "C:\\";
            // 
            // LblDummyOutFolder
            // 
            this.LblDummyOutFolder.AutomaticSize = true;
            this.LblDummyOutFolder.BackColor = System.Drawing.Color.Transparent;
            this.LblDummyOutFolder.Centered = false;
            this.LblDummyOutFolder.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblDummyOutFolder.Location = new System.Drawing.Point(11, 26);
            this.LblDummyOutFolder.Name = "LblDummyOutFolder";
            this.LblDummyOutFolder.Size = new System.Drawing.Size(78, 17);
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
            this.Text = "Export texture";
            this.TitleIcon = ((System.Drawing.Image)(resources.GetObject("$this.TitleIcon")));
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