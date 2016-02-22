namespace Ohana3DS_Rebirth.Tools
{
    partial class OSm4shModelCreator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OSm4shModelCreator));
            this.BtnCreate = new Ohana3DS_Rebirth.GUI.OButton();
            this.BtnCancel = new Ohana3DS_Rebirth.GUI.OButton();
            this.TxtOutModel = new Ohana3DS_Rebirth.GUI.OTextBox();
            this.LblDummyOutputModel = new Ohana3DS_Rebirth.GUI.OLabel();
            this.BtnOpenInputModel = new Ohana3DS_Rebirth.GUI.OButton();
            this.TxtInModel = new Ohana3DS_Rebirth.GUI.OTextBox();
            this.LblDummyInputModel = new Ohana3DS_Rebirth.GUI.OLabel();
            this.BtnSaveOutputModel = new Ohana3DS_Rebirth.GUI.OButton();
            this.ContentContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // ContentContainer
            // 
            this.ContentContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.ContentContainer.Controls.Add(this.BtnSaveOutputModel);
            this.ContentContainer.Controls.Add(this.BtnCreate);
            this.ContentContainer.Controls.Add(this.BtnCancel);
            this.ContentContainer.Controls.Add(this.TxtOutModel);
            this.ContentContainer.Controls.Add(this.LblDummyOutputModel);
            this.ContentContainer.Controls.Add(this.BtnOpenInputModel);
            this.ContentContainer.Controls.Add(this.TxtInModel);
            this.ContentContainer.Controls.Add(this.LblDummyInputModel);
            this.ContentContainer.Location = new System.Drawing.Point(1, 1);
            this.ContentContainer.Size = new System.Drawing.Size(382, 382);
            this.ContentContainer.Controls.SetChildIndex(this.LblDummyInputModel, 0);
            this.ContentContainer.Controls.SetChildIndex(this.TxtInModel, 0);
            this.ContentContainer.Controls.SetChildIndex(this.BtnOpenInputModel, 0);
            this.ContentContainer.Controls.SetChildIndex(this.LblDummyOutputModel, 0);
            this.ContentContainer.Controls.SetChildIndex(this.TxtOutModel, 0);
            this.ContentContainer.Controls.SetChildIndex(this.BtnCancel, 0);
            this.ContentContainer.Controls.SetChildIndex(this.BtnCreate, 0);
            this.ContentContainer.Controls.SetChildIndex(this.BtnSaveOutputModel, 0);
            // 
            // BtnCreate
            // 
            this.BtnCreate.Centered = true;
            this.BtnCreate.Image = global::Ohana3DS_Rebirth.Properties.Resources.ui_icon_tick;
            this.BtnCreate.Location = new System.Drawing.Point(237, 347);
            this.BtnCreate.Name = "BtnCreate";
            this.BtnCreate.Size = new System.Drawing.Size(64, 24);
            this.BtnCreate.TabIndex = 32;
            this.BtnCreate.Text = "Create";
            this.BtnCreate.Click += new System.EventHandler(this.BtnCreate_Click);
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
            // TxtOutModel
            // 
            this.TxtOutModel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.TxtOutModel.CharacterWhiteList = null;
            this.TxtOutModel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtOutModel.Location = new System.Drawing.Point(11, 98);
            this.TxtOutModel.Name = "TxtOutModel";
            this.TxtOutModel.Size = new System.Drawing.Size(322, 20);
            this.TxtOutModel.TabIndex = 29;
            this.TxtOutModel.Text = "C:\\Output.mbn";
            // 
            // LblDummyOutputModel
            // 
            this.LblDummyOutputModel.AutomaticSize = true;
            this.LblDummyOutputModel.BackColor = System.Drawing.Color.Transparent;
            this.LblDummyOutputModel.Centered = false;
            this.LblDummyOutputModel.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblDummyOutputModel.Location = new System.Drawing.Point(11, 75);
            this.LblDummyOutputModel.Name = "LblDummyOutputModel";
            this.LblDummyOutputModel.Size = new System.Drawing.Size(117, 17);
            this.LblDummyOutputModel.TabIndex = 28;
            this.LblDummyOutputModel.Text = "Output *.mbn model:";
            // 
            // BtnOpenInputModel
            // 
            this.BtnOpenInputModel.Centered = true;
            this.BtnOpenInputModel.Image = global::Ohana3DS_Rebirth.Properties.Resources.ui_icon_folder;
            this.BtnOpenInputModel.Location = new System.Drawing.Point(339, 49);
            this.BtnOpenInputModel.Name = "BtnOpenInputModel";
            this.BtnOpenInputModel.Size = new System.Drawing.Size(32, 20);
            this.BtnOpenInputModel.TabIndex = 27;
            this.BtnOpenInputModel.Click += new System.EventHandler(this.BtnOpenInputModel_Click);
            // 
            // TxtInModel
            // 
            this.TxtInModel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.TxtInModel.CharacterWhiteList = null;
            this.TxtInModel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtInModel.Location = new System.Drawing.Point(11, 49);
            this.TxtInModel.Name = "TxtInModel";
            this.TxtInModel.Size = new System.Drawing.Size(322, 20);
            this.TxtInModel.TabIndex = 26;
            this.TxtInModel.Text = "C:\\Input.obj";
            // 
            // LblDummyInputModel
            // 
            this.LblDummyInputModel.AutomaticSize = true;
            this.LblDummyInputModel.BackColor = System.Drawing.Color.Transparent;
            this.LblDummyInputModel.Centered = false;
            this.LblDummyInputModel.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblDummyInputModel.Location = new System.Drawing.Point(11, 26);
            this.LblDummyInputModel.Name = "LblDummyInputModel";
            this.LblDummyInputModel.Size = new System.Drawing.Size(72, 17);
            this.LblDummyInputModel.TabIndex = 25;
            this.LblDummyInputModel.Text = "Input model:";
            // 
            // BtnSaveOutputModel
            // 
            this.BtnSaveOutputModel.Centered = true;
            this.BtnSaveOutputModel.Image = global::Ohana3DS_Rebirth.Properties.Resources.ui_icon_folder;
            this.BtnSaveOutputModel.Location = new System.Drawing.Point(339, 98);
            this.BtnSaveOutputModel.Name = "BtnSaveOutputModel";
            this.BtnSaveOutputModel.Size = new System.Drawing.Size(32, 20);
            this.BtnSaveOutputModel.TabIndex = 33;
            this.BtnSaveOutputModel.Click += new System.EventHandler(this.BtnSaveOutputModel_Click);
            // 
            // OSm4shModelCreator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 384);
            this.KeyPreview = true;
            this.Name = "OSm4shModelCreator";
            this.Resizable = false;
            this.ShowMinimize = false;
            this.Text = "Sm4sh model creator";
            this.TitleIcon = ((System.Drawing.Image)(resources.GetObject("$this.TitleIcon")));
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OTextureExportForm_KeyDown);
            this.ContentContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private GUI.OButton BtnCreate;
        private GUI.OButton BtnCancel;
        private GUI.OTextBox TxtOutModel;
        private GUI.OLabel LblDummyOutputModel;
        private GUI.OButton BtnOpenInputModel;
        private GUI.OTextBox TxtInModel;
        private GUI.OLabel LblDummyInputModel;
        private GUI.OButton BtnSaveOutputModel;
    }
}