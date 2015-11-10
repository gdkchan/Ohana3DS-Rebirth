namespace Ohana3DS_Rebirth
{
    partial class FrmSettings
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
            this.LblDummyModelViewer = new System.Windows.Forms.Label();
            this.ChkEnableFShader = new Ohana3DS_Rebirth.GUI.OCheckBox();
            this.LblDummyMSAA = new Ohana3DS_Rebirth.GUI.OLabel();
            this.RadioAANone = new Ohana3DS_Rebirth.GUI.ORadioButton();
            this.RadioAA2x = new Ohana3DS_Rebirth.GUI.ORadioButton();
            this.RadioAA4x = new Ohana3DS_Rebirth.GUI.ORadioButton();
            this.RadioAA8x = new Ohana3DS_Rebirth.GUI.ORadioButton();
            this.ChkShowGrids = new Ohana3DS_Rebirth.GUI.OCheckBox();
            this.LblDummyBgColor = new Ohana3DS_Rebirth.GUI.OLabel();
            this.ViewBgColorPicker = new Ohana3DS_Rebirth.GUI.ORgbaColorBox();
            this.BtnCancel = new Ohana3DS_Rebirth.GUI.OButton();
            this.BtnOk = new Ohana3DS_Rebirth.GUI.OButton();
            this.BtnReset = new Ohana3DS_Rebirth.GUI.OButton();
            this.ContentContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // ContentContainer
            // 
            this.ContentContainer.Controls.Add(this.BtnReset);
            this.ContentContainer.Controls.Add(this.BtnOk);
            this.ContentContainer.Controls.Add(this.BtnCancel);
            this.ContentContainer.Controls.Add(this.ViewBgColorPicker);
            this.ContentContainer.Controls.Add(this.LblDummyBgColor);
            this.ContentContainer.Controls.Add(this.ChkShowGrids);
            this.ContentContainer.Controls.Add(this.RadioAA8x);
            this.ContentContainer.Controls.Add(this.RadioAA4x);
            this.ContentContainer.Controls.Add(this.RadioAA2x);
            this.ContentContainer.Controls.Add(this.RadioAANone);
            this.ContentContainer.Controls.Add(this.LblDummyMSAA);
            this.ContentContainer.Controls.Add(this.ChkEnableFShader);
            this.ContentContainer.Controls.Add(this.LblDummyModelViewer);
            this.ContentContainer.Location = new System.Drawing.Point(0, 0);
            this.ContentContainer.Size = new System.Drawing.Size(320, 384);
            this.ContentContainer.Controls.SetChildIndex(this.LblDummyModelViewer, 0);
            this.ContentContainer.Controls.SetChildIndex(this.ChkEnableFShader, 0);
            this.ContentContainer.Controls.SetChildIndex(this.LblDummyMSAA, 0);
            this.ContentContainer.Controls.SetChildIndex(this.RadioAANone, 0);
            this.ContentContainer.Controls.SetChildIndex(this.RadioAA2x, 0);
            this.ContentContainer.Controls.SetChildIndex(this.RadioAA4x, 0);
            this.ContentContainer.Controls.SetChildIndex(this.RadioAA8x, 0);
            this.ContentContainer.Controls.SetChildIndex(this.ChkShowGrids, 0);
            this.ContentContainer.Controls.SetChildIndex(this.LblDummyBgColor, 0);
            this.ContentContainer.Controls.SetChildIndex(this.ViewBgColorPicker, 0);
            this.ContentContainer.Controls.SetChildIndex(this.BtnCancel, 0);
            this.ContentContainer.Controls.SetChildIndex(this.BtnOk, 0);
            this.ContentContainer.Controls.SetChildIndex(this.BtnReset, 0);
            // 
            // LblTitle
            // 
            this.LblTitle.Size = new System.Drawing.Size(53, 19);
            this.LblTitle.Text = "Settings";
            // 
            // LblDummyModelViewer
            // 
            this.LblDummyModelViewer.AutoSize = true;
            this.LblDummyModelViewer.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblDummyModelViewer.Location = new System.Drawing.Point(8, 23);
            this.LblDummyModelViewer.Name = "LblDummyModelViewer";
            this.LblDummyModelViewer.Size = new System.Drawing.Size(90, 17);
            this.LblDummyModelViewer.TabIndex = 10;
            this.LblDummyModelViewer.Text = "Model Viewer";
            // 
            // ChkEnableFShader
            // 
            this.ChkEnableFShader.BackColor = System.Drawing.Color.Transparent;
            this.ChkEnableFShader.BoxColor = System.Drawing.Color.Black;
            this.ChkEnableFShader.Checked = false;
            this.ChkEnableFShader.Location = new System.Drawing.Point(11, 210);
            this.ChkEnableFShader.Name = "ChkEnableFShader";
            this.ChkEnableFShader.Size = new System.Drawing.Size(256, 16);
            this.ChkEnableFShader.TabIndex = 11;
            this.ChkEnableFShader.Text = "Enable Fragment Shader";
            // 
            // LblDummyMSAA
            // 
            this.LblDummyMSAA.AutomaticSize = false;
            this.LblDummyMSAA.BackColor = System.Drawing.Color.Transparent;
            this.LblDummyMSAA.Centered = false;
            this.LblDummyMSAA.Location = new System.Drawing.Point(11, 43);
            this.LblDummyMSAA.Name = "LblDummyMSAA";
            this.LblDummyMSAA.Size = new System.Drawing.Size(256, 16);
            this.LblDummyMSAA.TabIndex = 12;
            this.LblDummyMSAA.Text = "MultiSampled AntiAliasing level:";
            // 
            // RadioAANone
            // 
            this.RadioAANone.AutoSize = true;
            this.RadioAANone.BackColor = System.Drawing.Color.Black;
            this.RadioAANone.ForeColor = System.Drawing.Color.White;
            this.RadioAANone.Location = new System.Drawing.Point(12, 65);
            this.RadioAANone.Name = "RadioAANone";
            this.RadioAANone.Size = new System.Drawing.Size(53, 17);
            this.RadioAANone.TabIndex = 13;
            this.RadioAANone.Text = "None";
            this.RadioAANone.UseVisualStyleBackColor = false;
            // 
            // RadioAA2x
            // 
            this.RadioAA2x.AutoSize = true;
            this.RadioAA2x.BackColor = System.Drawing.Color.Black;
            this.RadioAA2x.ForeColor = System.Drawing.Color.White;
            this.RadioAA2x.Location = new System.Drawing.Point(71, 65);
            this.RadioAA2x.Name = "RadioAA2x";
            this.RadioAA2x.Size = new System.Drawing.Size(36, 17);
            this.RadioAA2x.TabIndex = 14;
            this.RadioAA2x.Text = "2x";
            this.RadioAA2x.UseVisualStyleBackColor = false;
            // 
            // RadioAA4x
            // 
            this.RadioAA4x.AutoSize = true;
            this.RadioAA4x.BackColor = System.Drawing.Color.Black;
            this.RadioAA4x.ForeColor = System.Drawing.Color.White;
            this.RadioAA4x.Location = new System.Drawing.Point(113, 65);
            this.RadioAA4x.Name = "RadioAA4x";
            this.RadioAA4x.Size = new System.Drawing.Size(36, 17);
            this.RadioAA4x.TabIndex = 15;
            this.RadioAA4x.Text = "4x";
            this.RadioAA4x.UseVisualStyleBackColor = false;
            // 
            // RadioAA8x
            // 
            this.RadioAA8x.AutoSize = true;
            this.RadioAA8x.BackColor = System.Drawing.Color.Black;
            this.RadioAA8x.ForeColor = System.Drawing.Color.White;
            this.RadioAA8x.Location = new System.Drawing.Point(155, 65);
            this.RadioAA8x.Name = "RadioAA8x";
            this.RadioAA8x.Size = new System.Drawing.Size(36, 17);
            this.RadioAA8x.TabIndex = 16;
            this.RadioAA8x.Text = "8x";
            this.RadioAA8x.UseVisualStyleBackColor = false;
            // 
            // ChkShowGrids
            // 
            this.ChkShowGrids.BackColor = System.Drawing.Color.Transparent;
            this.ChkShowGrids.BoxColor = System.Drawing.Color.Black;
            this.ChkShowGrids.Checked = false;
            this.ChkShowGrids.Location = new System.Drawing.Point(11, 232);
            this.ChkShowGrids.Name = "ChkShowGrids";
            this.ChkShowGrids.Size = new System.Drawing.Size(256, 16);
            this.ChkShowGrids.TabIndex = 17;
            this.ChkShowGrids.Text = "Show orientation grids";
            // 
            // LblDummyBgColor
            // 
            this.LblDummyBgColor.AutomaticSize = false;
            this.LblDummyBgColor.BackColor = System.Drawing.Color.Transparent;
            this.LblDummyBgColor.Centered = false;
            this.LblDummyBgColor.Location = new System.Drawing.Point(11, 88);
            this.LblDummyBgColor.Name = "LblDummyBgColor";
            this.LblDummyBgColor.Size = new System.Drawing.Size(256, 16);
            this.LblDummyBgColor.TabIndex = 18;
            this.LblDummyBgColor.Text = "Viewport background color:";
            // 
            // ViewBgColorPicker
            // 
            this.ViewBgColorPicker.BackColor = System.Drawing.Color.Transparent;
            this.ViewBgColorPicker.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ViewBgColorPicker.ForeColor = System.Drawing.Color.White;
            this.ViewBgColorPicker.Location = new System.Drawing.Point(12, 110);
            this.ViewBgColorPicker.Name = "ViewBgColorPicker";
            this.ViewBgColorPicker.Size = new System.Drawing.Size(179, 94);
            this.ViewBgColorPicker.TabIndex = 19;
            // 
            // BtnCancel
            // 
            this.BtnCancel.Centered = true;
            this.BtnCancel.Hover = true;
            this.BtnCancel.Image = null;
            this.BtnCancel.Location = new System.Drawing.Point(244, 348);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(64, 24);
            this.BtnCancel.TabIndex = 20;
            this.BtnCancel.Text = "Cancel";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnOk
            // 
            this.BtnOk.Centered = true;
            this.BtnOk.Hover = true;
            this.BtnOk.Image = null;
            this.BtnOk.Location = new System.Drawing.Point(174, 348);
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.Size = new System.Drawing.Size(64, 24);
            this.BtnOk.TabIndex = 21;
            this.BtnOk.Text = "OK";
            this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // BtnReset
            // 
            this.BtnReset.Centered = true;
            this.BtnReset.Hover = true;
            this.BtnReset.Image = null;
            this.BtnReset.Location = new System.Drawing.Point(12, 348);
            this.BtnReset.Name = "BtnReset";
            this.BtnReset.Size = new System.Drawing.Size(64, 24);
            this.BtnReset.TabIndex = 22;
            this.BtnReset.Text = "Reset";
            this.BtnReset.Click += new System.EventHandler(this.BtnReset_Click);
            // 
            // FrmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 384);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "FrmSettings";
            this.Resizable = false;
            this.Text = "FrmSettings";
            this.Load += new System.EventHandler(this.FrmSettings_Load);
            this.ContentContainer.ResumeLayout(false);
            this.ContentContainer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private GUI.OCheckBox ChkEnableFShader;
        private System.Windows.Forms.Label LblDummyModelViewer;
        private GUI.OButton BtnOk;
        private GUI.OButton BtnCancel;
        private GUI.ORgbaColorBox ViewBgColorPicker;
        private GUI.OLabel LblDummyBgColor;
        private GUI.OCheckBox ChkShowGrids;
        private GUI.ORadioButton RadioAA8x;
        private GUI.ORadioButton RadioAA4x;
        private GUI.ORadioButton RadioAA2x;
        private GUI.ORadioButton RadioAANone;
        private GUI.OLabel LblDummyMSAA;
        private GUI.OButton BtnReset;
    }
}