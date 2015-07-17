namespace Ohana3DS_Rebirth.GUI
{
    partial class OAddLightDialog
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
            this.LblLightName = new Ohana3DS_Rebirth.GUI.OLabel();
            this.TxtLightName = new Ohana3DS_Rebirth.GUI.OTextBox();
            this.LblLightType = new Ohana3DS_Rebirth.GUI.OLabel();
            this.CmbLightType = new Ohana3DS_Rebirth.GUI.OComboBox();
            this.BtnOk = new Ohana3DS_Rebirth.GUI.OButton();
            this.BtnCancel = new Ohana3DS_Rebirth.GUI.OButton();
            this.ContentContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // ContentContainer
            // 
            this.ContentContainer.Controls.Add(this.BtnCancel);
            this.ContentContainer.Controls.Add(this.BtnOk);
            this.ContentContainer.Controls.Add(this.CmbLightType);
            this.ContentContainer.Controls.Add(this.LblLightType);
            this.ContentContainer.Controls.Add(this.TxtLightName);
            this.ContentContainer.Controls.Add(this.LblLightName);
            this.ContentContainer.Location = new System.Drawing.Point(0, 0);
            this.ContentContainer.Size = new System.Drawing.Size(320, 240);
            this.ContentContainer.Controls.SetChildIndex(this.LblLightName, 0);
            this.ContentContainer.Controls.SetChildIndex(this.TxtLightName, 0);
            this.ContentContainer.Controls.SetChildIndex(this.LblLightType, 0);
            this.ContentContainer.Controls.SetChildIndex(this.CmbLightType, 0);
            this.ContentContainer.Controls.SetChildIndex(this.BtnOk, 0);
            this.ContentContainer.Controls.SetChildIndex(this.BtnCancel, 0);
            // 
            // LblTitle
            // 
            this.LblTitle.Size = new System.Drawing.Size(100, 19);
            this.LblTitle.Text = "Add light";
            // 
            // LblLightName
            // 
            this.LblLightName.AutomaticSize = false;
            this.LblLightName.BackColor = System.Drawing.Color.Transparent;
            this.LblLightName.Centered = false;
            this.LblLightName.Location = new System.Drawing.Point(12, 26);
            this.LblLightName.Name = "LblLightName";
            this.LblLightName.Size = new System.Drawing.Size(296, 16);
            this.LblLightName.TabIndex = 10;
            this.LblLightName.Text = "Light name:";
            // 
            // TxtLightName
            // 
            this.TxtLightName.BackColor = System.Drawing.Color.Black;
            this.TxtLightName.CharacterWhiteList = null;
            this.TxtLightName.Location = new System.Drawing.Point(12, 48);
            this.TxtLightName.Name = "TxtLightName";
            this.TxtLightName.Size = new System.Drawing.Size(296, 22);
            this.TxtLightName.TabIndex = 11;
            this.TxtLightName.Text = "new_light";
            // 
            // LblLightType
            // 
            this.LblLightType.AutomaticSize = false;
            this.LblLightType.BackColor = System.Drawing.Color.Transparent;
            this.LblLightType.Centered = false;
            this.LblLightType.Location = new System.Drawing.Point(12, 76);
            this.LblLightType.Name = "LblLightType";
            this.LblLightType.Size = new System.Drawing.Size(296, 16);
            this.LblLightType.TabIndex = 12;
            this.LblLightType.Text = "Light type:";
            // 
            // CmbLightType
            // 
            this.CmbLightType.AutomaticSize = true;
            this.CmbLightType.BackColor = System.Drawing.Color.Black;
            this.CmbLightType.BarFont = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbLightType.BarHeight = 24;
            this.CmbLightType.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbLightType.ForeColor = System.Drawing.Color.White;
            this.CmbLightType.Items = new string[] {
        "Fragment",
        "Vertex",
        "Ambient",
        "Hemisphere"};
            this.CmbLightType.ListFont = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbLightType.ListHeight = 256;
            this.CmbLightType.Location = new System.Drawing.Point(12, 98);
            this.CmbLightType.Name = "CmbLightType";
            this.CmbLightType.SelectedIndex = 0;
            this.CmbLightType.Size = new System.Drawing.Size(296, 22);
            this.CmbLightType.TabIndex = 13;
            // 
            // BtnOk
            // 
            this.BtnOk.Centered = true;
            this.BtnOk.Hover = true;
            this.BtnOk.Image = global::Ohana3DS_Rebirth.Properties.Resources.icn_ticked;
            this.BtnOk.Location = new System.Drawing.Point(174, 208);
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.Size = new System.Drawing.Size(64, 20);
            this.BtnOk.TabIndex = 14;
            this.BtnOk.Text = "OK";
            this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Centered = true;
            this.BtnCancel.Hover = true;
            this.BtnCancel.Image = global::Ohana3DS_Rebirth.Properties.Resources.icn_close;
            this.BtnCancel.Location = new System.Drawing.Point(244, 208);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(64, 20);
            this.BtnCancel.TabIndex = 15;
            this.BtnCancel.Text = "Cancel";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // OAddLightDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 240);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "OAddLightDialog";
            this.Resizable = false;
            this.ShowMinimize = false;
            this.Text = "OAddLightDialog";
            this.ContentContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private OButton BtnCancel;
        private OButton BtnOk;
        private OComboBox CmbLightType;
        private OLabel LblLightType;
        private OTextBox TxtLightName;
        private OLabel LblLightName;
    }
}