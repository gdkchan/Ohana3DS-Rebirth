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
            this.ContentContainer.Location = new System.Drawing.Point(0, 0);
            this.ContentContainer.Size = new System.Drawing.Size(384, 384);
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
            // LblTitle
            // 
            this.LblTitle.Size = new System.Drawing.Size(98, 19);
            this.LblTitle.Text = "Model exporter";
            // 
            // LblDummyOutFolder
            // 
            this.LblDummyOutFolder.AutomaticSize = true;
            this.LblDummyOutFolder.Centered = false;
            this.LblDummyOutFolder.Location = new System.Drawing.Point(8, 26);
            this.LblDummyOutFolder.Name = "LblDummyOutFolder";
            this.LblDummyOutFolder.Size = new System.Drawing.Size(73, 16);
            this.LblDummyOutFolder.TabIndex = 10;
            this.LblDummyOutFolder.Text = "Output folder:";
            // 
            // TxtOutFolder
            // 
            this.TxtOutFolder.BackColor = System.Drawing.Color.Black;
            this.TxtOutFolder.CharacterWhiteList = null;
            this.TxtOutFolder.Location = new System.Drawing.Point(8, 48);
            this.TxtOutFolder.Name = "TxtOutFolder";
            this.TxtOutFolder.Size = new System.Drawing.Size(326, 20);
            this.TxtOutFolder.TabIndex = 11;
            this.TxtOutFolder.Text = "C:\\";
            // 
            // BtnBrowseFolder
            // 
            this.BtnBrowseFolder.Centered = true;
            this.BtnBrowseFolder.Hover = true;
            this.BtnBrowseFolder.Image = global::Ohana3DS_Rebirth.Properties.Resources.icn_open;
            this.BtnBrowseFolder.Location = new System.Drawing.Point(340, 48);
            this.BtnBrowseFolder.Name = "BtnBrowseFolder";
            this.BtnBrowseFolder.Size = new System.Drawing.Size(32, 20);
            this.BtnBrowseFolder.TabIndex = 12;
            this.BtnBrowseFolder.Click += new System.EventHandler(this.BtnBrowseFolder_Click);
            // 
            // LblDummyModelName
            // 
            this.LblDummyModelName.AutomaticSize = true;
            this.LblDummyModelName.Centered = false;
            this.LblDummyModelName.Location = new System.Drawing.Point(8, 74);
            this.LblDummyModelName.Name = "LblDummyModelName";
            this.LblDummyModelName.Size = new System.Drawing.Size(69, 16);
            this.LblDummyModelName.TabIndex = 13;
            this.LblDummyModelName.Text = "Model name:";
            // 
            // TxtModelName
            // 
            this.TxtModelName.BackColor = System.Drawing.Color.Black;
            this.TxtModelName.CharacterWhiteList = null;
            this.TxtModelName.Location = new System.Drawing.Point(8, 96);
            this.TxtModelName.Name = "TxtModelName";
            this.TxtModelName.Size = new System.Drawing.Size(364, 20);
            this.TxtModelName.TabIndex = 14;
            this.TxtModelName.Text = "model_name";
            // 
            // LblDummyFormat
            // 
            this.LblDummyFormat.AutomaticSize = true;
            this.LblDummyFormat.Centered = false;
            this.LblDummyFormat.Location = new System.Drawing.Point(8, 122);
            this.LblDummyFormat.Name = "LblDummyFormat";
            this.LblDummyFormat.Size = new System.Drawing.Size(41, 16);
            this.LblDummyFormat.TabIndex = 15;
            this.LblDummyFormat.Text = "Format:";
            // 
            // RadioCMDL
            // 
            this.RadioCMDL.AutoSize = true;
            this.RadioCMDL.BackColor = System.Drawing.Color.Black;
            this.RadioCMDL.ForeColor = System.Drawing.Color.White;
            this.RadioCMDL.Location = new System.Drawing.Point(166, 144);
            this.RadioCMDL.Name = "RadioCMDL";
            this.RadioCMDL.Size = new System.Drawing.Size(55, 17);
            this.RadioCMDL.TabIndex = 20;
            this.RadioCMDL.Text = "CMDL";
            this.RadioCMDL.UseVisualStyleBackColor = false;
            // 
            // RadioOBJ
            // 
            this.RadioOBJ.AutoSize = true;
            this.RadioOBJ.BackColor = System.Drawing.Color.Black;
            this.RadioOBJ.ForeColor = System.Drawing.Color.White;
            this.RadioOBJ.Location = new System.Drawing.Point(115, 144);
            this.RadioOBJ.Name = "RadioOBJ";
            this.RadioOBJ.Size = new System.Drawing.Size(45, 17);
            this.RadioOBJ.TabIndex = 19;
            this.RadioOBJ.Text = "OBJ";
            this.RadioOBJ.UseVisualStyleBackColor = false;
            // 
            // RadioSMD
            // 
            this.RadioSMD.AutoSize = true;
            this.RadioSMD.BackColor = System.Drawing.Color.Black;
            this.RadioSMD.ForeColor = System.Drawing.Color.White;
            this.RadioSMD.Location = new System.Drawing.Point(60, 144);
            this.RadioSMD.Name = "RadioSMD";
            this.RadioSMD.Size = new System.Drawing.Size(49, 17);
            this.RadioSMD.TabIndex = 18;
            this.RadioSMD.Text = "SMD";
            this.RadioSMD.UseVisualStyleBackColor = false;
            // 
            // RadioDAE
            // 
            this.RadioDAE.AutoSize = true;
            this.RadioDAE.BackColor = System.Drawing.Color.Black;
            this.RadioDAE.ForeColor = System.Drawing.Color.White;
            this.RadioDAE.Location = new System.Drawing.Point(8, 144);
            this.RadioDAE.Name = "RadioDAE";
            this.RadioDAE.Size = new System.Drawing.Size(46, 17);
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
            this.ChkExportAllModels.Location = new System.Drawing.Point(8, 167);
            this.ChkExportAllModels.Name = "ChkExportAllModels";
            this.ChkExportAllModels.Size = new System.Drawing.Size(243, 16);
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
            this.ChkExportAllAnimations.Location = new System.Drawing.Point(8, 189);
            this.ChkExportAllAnimations.Name = "ChkExportAllAnimations";
            this.ChkExportAllAnimations.Size = new System.Drawing.Size(198, 16);
            this.ChkExportAllAnimations.TabIndex = 22;
            this.ChkExportAllAnimations.Text = "Export all animations (if supported)";
            // 
            // BtnOk
            // 
            this.BtnOk.Centered = true;
            this.BtnOk.Hover = true;
            this.BtnOk.Image = global::Ohana3DS_Rebirth.Properties.Resources.icn_ticked;
            this.BtnOk.Location = new System.Drawing.Point(238, 348);
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.Size = new System.Drawing.Size(64, 24);
            this.BtnOk.TabIndex = 24;
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
            this.BtnCancel.TabIndex = 23;
            this.BtnCancel.Text = "Cancel";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // OModelExportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 384);
            this.Name = "OModelExportForm";
            this.Resizable = false;
            this.ShowMinimize = false;
            this.Text = "OModelExportForm";
            this.Load += new System.EventHandler(this.OModelExportForm_Load);
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