namespace Ohana3DS_Rebirth.GUI.Forms
{
    partial class OModelExportForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OModelExportForm));
            this.LblDummyOutFolder = new Ohana3DS_Rebirth.GUI.OLabel();
            this.TxtOutFolder = new Ohana3DS_Rebirth.GUI.OTextBox();
            this.BtnBrowseFolder = new Ohana3DS_Rebirth.GUI.OButton();
            this.LblDummyModelName = new Ohana3DS_Rebirth.GUI.OLabel();
            this.TxtModelName = new Ohana3DS_Rebirth.GUI.OTextBox();
            this.LblDummyFormat = new Ohana3DS_Rebirth.GUI.OLabel();
            this.RadioCMDL = new Ohana3DS_Rebirth.GUI.ORadioButton();
            this.RadioOBJ = new Ohana3DS_Rebirth.GUI.ORadioButton();
            this.RadioSMD = new Ohana3DS_Rebirth.GUI.ORadioButton();
            this.RadioDAE = new Ohana3DS_Rebirth.GUI.ORadioButton();
            this.ChkExportAllModels = new Ohana3DS_Rebirth.GUI.OCheckBox();
            this.ChkExportAllAnimations = new Ohana3DS_Rebirth.GUI.OCheckBox();
            this.BtnOk = new Ohana3DS_Rebirth.GUI.OButton();
            this.BtnCancel = new Ohana3DS_Rebirth.GUI.OButton();
            this.ContentContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // ContentContainer
            // 
            this.ContentContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.ContentContainer.Controls.Add(this.BtnOk);
            this.ContentContainer.Controls.Add(this.BtnCancel);
            this.ContentContainer.Controls.Add(this.ChkExportAllAnimations);
            this.ContentContainer.Controls.Add(this.ChkExportAllModels);
            this.ContentContainer.Controls.Add(this.RadioCMDL);
            this.ContentContainer.Controls.Add(this.RadioOBJ);
            this.ContentContainer.Controls.Add(this.RadioSMD);
            this.ContentContainer.Controls.Add(this.RadioDAE);
            this.ContentContainer.Controls.Add(this.LblDummyFormat);
            this.ContentContainer.Controls.Add(this.TxtModelName);
            this.ContentContainer.Controls.Add(this.LblDummyModelName);
            this.ContentContainer.Controls.Add(this.BtnBrowseFolder);
            this.ContentContainer.Controls.Add(this.TxtOutFolder);
            this.ContentContainer.Controls.Add(this.LblDummyOutFolder);
            this.ContentContainer.Location = new System.Drawing.Point(1, 1);
            this.ContentContainer.Size = new System.Drawing.Size(382, 382);
            this.ContentContainer.Controls.SetChildIndex(this.LblDummyOutFolder, 0);
            this.ContentContainer.Controls.SetChildIndex(this.TxtOutFolder, 0);
            this.ContentContainer.Controls.SetChildIndex(this.BtnBrowseFolder, 0);
            this.ContentContainer.Controls.SetChildIndex(this.LblDummyModelName, 0);
            this.ContentContainer.Controls.SetChildIndex(this.TxtModelName, 0);
            this.ContentContainer.Controls.SetChildIndex(this.LblDummyFormat, 0);
            this.ContentContainer.Controls.SetChildIndex(this.RadioDAE, 0);
            this.ContentContainer.Controls.SetChildIndex(this.RadioSMD, 0);
            this.ContentContainer.Controls.SetChildIndex(this.RadioOBJ, 0);
            this.ContentContainer.Controls.SetChildIndex(this.RadioCMDL, 0);
            this.ContentContainer.Controls.SetChildIndex(this.ChkExportAllModels, 0);
            this.ContentContainer.Controls.SetChildIndex(this.ChkExportAllAnimations, 0);
            this.ContentContainer.Controls.SetChildIndex(this.BtnCancel, 0);
            this.ContentContainer.Controls.SetChildIndex(this.BtnOk, 0);
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
            this.LblDummyOutFolder.TabIndex = 10;
            this.LblDummyOutFolder.Text = "Output folder:";
            // 
            // TxtOutFolder
            // 
            this.TxtOutFolder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.TxtOutFolder.CharacterWhiteList = null;
            this.TxtOutFolder.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtOutFolder.Location = new System.Drawing.Point(11, 49);
            this.TxtOutFolder.Name = "TxtOutFolder";
            this.TxtOutFolder.Size = new System.Drawing.Size(322, 20);
            this.TxtOutFolder.TabIndex = 11;
            this.TxtOutFolder.Text = "C:\\";
            // 
            // BtnBrowseFolder
            // 
            this.BtnBrowseFolder.Centered = true;
            this.BtnBrowseFolder.Image = global::Ohana3DS_Rebirth.Properties.Resources.ui_icon_folder;
            this.BtnBrowseFolder.Location = new System.Drawing.Point(339, 49);
            this.BtnBrowseFolder.Name = "BtnBrowseFolder";
            this.BtnBrowseFolder.Size = new System.Drawing.Size(32, 20);
            this.BtnBrowseFolder.TabIndex = 12;
            this.BtnBrowseFolder.Click += new System.EventHandler(this.BtnBrowseFolder_Click);
            // 
            // LblDummyModelName
            // 
            this.LblDummyModelName.AutomaticSize = true;
            this.LblDummyModelName.BackColor = System.Drawing.Color.Transparent;
            this.LblDummyModelName.Centered = false;
            this.LblDummyModelName.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblDummyModelName.Location = new System.Drawing.Point(11, 75);
            this.LblDummyModelName.Name = "LblDummyModelName";
            this.LblDummyModelName.Size = new System.Drawing.Size(73, 17);
            this.LblDummyModelName.TabIndex = 13;
            this.LblDummyModelName.Text = "Model name:";
            // 
            // TxtModelName
            // 
            this.TxtModelName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.TxtModelName.CharacterWhiteList = null;
            this.TxtModelName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtModelName.Location = new System.Drawing.Point(11, 98);
            this.TxtModelName.Name = "TxtModelName";
            this.TxtModelName.Size = new System.Drawing.Size(360, 20);
            this.TxtModelName.TabIndex = 14;
            this.TxtModelName.Text = "model_name";
            // 
            // LblDummyFormat
            // 
            this.LblDummyFormat.AutomaticSize = true;
            this.LblDummyFormat.BackColor = System.Drawing.Color.Transparent;
            this.LblDummyFormat.Centered = false;
            this.LblDummyFormat.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblDummyFormat.Location = new System.Drawing.Point(11, 124);
            this.LblDummyFormat.Name = "LblDummyFormat";
            this.LblDummyFormat.Size = new System.Drawing.Size(44, 17);
            this.LblDummyFormat.TabIndex = 15;
            this.LblDummyFormat.Text = "Format:";
            // 
            // RadioCMDL
            // 
            this.RadioCMDL.AutoSize = true;
            this.RadioCMDL.BackColor = System.Drawing.Color.Black;
            this.RadioCMDL.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RadioCMDL.ForeColor = System.Drawing.Color.White;
            this.RadioCMDL.Location = new System.Drawing.Point(172, 147);
            this.RadioCMDL.Name = "RadioCMDL";
            this.RadioCMDL.Size = new System.Drawing.Size(58, 19);
            this.RadioCMDL.TabIndex = 20;
            this.RadioCMDL.Text = "CMDL";
            this.RadioCMDL.UseVisualStyleBackColor = false;
            // 
            // RadioOBJ
            // 
            this.RadioOBJ.AutoSize = true;
            this.RadioOBJ.BackColor = System.Drawing.Color.Black;
            this.RadioOBJ.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RadioOBJ.ForeColor = System.Drawing.Color.White;
            this.RadioOBJ.Location = new System.Drawing.Point(121, 147);
            this.RadioOBJ.Name = "RadioOBJ";
            this.RadioOBJ.Size = new System.Drawing.Size(45, 19);
            this.RadioOBJ.TabIndex = 19;
            this.RadioOBJ.Text = "OBJ";
            this.RadioOBJ.UseVisualStyleBackColor = false;
            // 
            // RadioSMD
            // 
            this.RadioSMD.AutoSize = true;
            this.RadioSMD.BackColor = System.Drawing.Color.Black;
            this.RadioSMD.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RadioSMD.ForeColor = System.Drawing.Color.White;
            this.RadioSMD.Location = new System.Drawing.Point(65, 147);
            this.RadioSMD.Name = "RadioSMD";
            this.RadioSMD.Size = new System.Drawing.Size(50, 19);
            this.RadioSMD.TabIndex = 18;
            this.RadioSMD.Text = "SMD";
            this.RadioSMD.UseVisualStyleBackColor = false;
            // 
            // RadioDAE
            // 
            this.RadioDAE.AutoSize = true;
            this.RadioDAE.BackColor = System.Drawing.Color.Black;
            this.RadioDAE.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RadioDAE.ForeColor = System.Drawing.Color.White;
            this.RadioDAE.Location = new System.Drawing.Point(11, 147);
            this.RadioDAE.Name = "RadioDAE";
            this.RadioDAE.Size = new System.Drawing.Size(47, 19);
            this.RadioDAE.TabIndex = 17;
            this.RadioDAE.Text = "DAE";
            this.RadioDAE.UseVisualStyleBackColor = false;
            // 
            // ChkExportAllModels
            // 
            this.ChkExportAllModels.AutomaticSize = true;
            this.ChkExportAllModels.BackColor = System.Drawing.Color.Transparent;
            this.ChkExportAllModels.BoxColor = System.Drawing.Color.Black;
            this.ChkExportAllModels.Checked = false;
            this.ChkExportAllModels.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkExportAllModels.Location = new System.Drawing.Point(11, 172);
            this.ChkExportAllModels.Name = "ChkExportAllModels";
            this.ChkExportAllModels.Size = new System.Drawing.Size(255, 17);
            this.ChkExportAllModels.TabIndex = 21;
            this.ChkExportAllModels.Text = "Export all models (with their internal names)";
            this.ChkExportAllModels.CheckedChanged += new System.EventHandler(this.ChkExportAllModels_CheckedChanged);
            // 
            // ChkExportAllAnimations
            // 
            this.ChkExportAllAnimations.AutomaticSize = true;
            this.ChkExportAllAnimations.BackColor = System.Drawing.Color.Transparent;
            this.ChkExportAllAnimations.BoxColor = System.Drawing.Color.Black;
            this.ChkExportAllAnimations.Checked = false;
            this.ChkExportAllAnimations.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkExportAllAnimations.Location = new System.Drawing.Point(11, 195);
            this.ChkExportAllAnimations.Name = "ChkExportAllAnimations";
            this.ChkExportAllAnimations.Size = new System.Drawing.Size(208, 17);
            this.ChkExportAllAnimations.TabIndex = 22;
            this.ChkExportAllAnimations.Text = "Export all animations (if supported)";
            // 
            // BtnOk
            // 
            this.BtnOk.Centered = true;
            this.BtnOk.Image = global::Ohana3DS_Rebirth.Properties.Resources.ui_icon_tick;
            this.BtnOk.Location = new System.Drawing.Point(237, 347);
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.Size = new System.Drawing.Size(64, 24);
            this.BtnOk.TabIndex = 24;
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
            this.BtnCancel.TabIndex = 23;
            this.BtnCancel.Text = "Cancel";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // OModelExportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 384);
            this.KeyPreview = true;
            this.Name = "OModelExportForm";
            this.Resizable = false;
            this.ShowMinimize = false;
            this.Text = "Export model";
            this.TitleIcon = ((System.Drawing.Image)(resources.GetObject("$this.TitleIcon")));
            this.Load += new System.EventHandler(this.OModelExportForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OModelExportForm_KeyDown);
            this.ContentContainer.ResumeLayout(false);
            this.ContentContainer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private OLabel LblDummyOutFolder;
        private OButton BtnBrowseFolder;
        private OTextBox TxtOutFolder;
        private OLabel LblDummyFormat;
        private OTextBox TxtModelName;
        private OLabel LblDummyModelName;
        private ORadioButton RadioCMDL;
        private ORadioButton RadioOBJ;
        private ORadioButton RadioSMD;
        private ORadioButton RadioDAE;
        private OCheckBox ChkExportAllModels;
        private OCheckBox ChkExportAllAnimations;
        private OButton BtnOk;
        private OButton BtnCancel;
    }
}