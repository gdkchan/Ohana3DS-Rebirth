namespace Ohana3DS_Rebirth
{
    partial class OContainerForm
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
            this.FileList = new Ohana3DS_Rebirth.GUI.OList();
            this.TopControls = new System.Windows.Forms.TableLayoutPanel();
            this.BtnExtractAll = new Ohana3DS_Rebirth.GUI.OButton();
            this.BtnExtract = new Ohana3DS_Rebirth.GUI.OButton();
            this.BtnLoad = new Ohana3DS_Rebirth.GUI.OButton();
            this.ContentContainer.SuspendLayout();
            this.TopControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // ContentContainer
            // 
            this.ContentContainer.Controls.Add(this.FileList);
            this.ContentContainer.Controls.Add(this.TopControls);
            this.ContentContainer.Size = new System.Drawing.Size(312, 248);
            this.ContentContainer.Controls.SetChildIndex(this.TopControls, 0);
            this.ContentContainer.Controls.SetChildIndex(this.FileList, 0);
            // 
            // LblTitle
            // 
            this.LblTitle.Size = new System.Drawing.Size(64, 19);
            this.LblTitle.Text = "Container";
            // 
            // FileList
            // 
            this.FileList.BackColor = System.Drawing.Color.Transparent;
            this.FileList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FileList.HeaderHeight = 24;
            this.FileList.ItemHeight = 16;
            this.FileList.Location = new System.Drawing.Point(0, 44);
            this.FileList.Name = "FileList";
            this.FileList.SelectedIndex = -1;
            this.FileList.Size = new System.Drawing.Size(312, 204);
            this.FileList.TabIndex = 10;
            // 
            // TopControls
            // 
            this.TopControls.ColumnCount = 3;
            this.TopControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.TopControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.TopControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.TopControls.Controls.Add(this.BtnExtractAll, 2, 0);
            this.TopControls.Controls.Add(this.BtnExtract, 1, 0);
            this.TopControls.Controls.Add(this.BtnLoad, 0, 0);
            this.TopControls.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopControls.Location = new System.Drawing.Point(0, 20);
            this.TopControls.Name = "TopControls";
            this.TopControls.RowCount = 1;
            this.TopControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TopControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TopControls.Size = new System.Drawing.Size(312, 24);
            this.TopControls.TabIndex = 11;
            // 
            // BtnExtractAll
            // 
            this.BtnExtractAll.Centered = true;
            this.BtnExtractAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnExtractAll.Hover = true;
            this.BtnExtractAll.Image = global::Ohana3DS_Rebirth.Properties.Resources.icn_arrow_up;
            this.BtnExtractAll.Location = new System.Drawing.Point(208, 0);
            this.BtnExtractAll.Margin = new System.Windows.Forms.Padding(0);
            this.BtnExtractAll.Name = "BtnExtractAll";
            this.BtnExtractAll.Size = new System.Drawing.Size(104, 24);
            this.BtnExtractAll.TabIndex = 2;
            this.BtnExtractAll.Text = "Extract all";
            this.BtnExtractAll.Click += new System.EventHandler(this.BtnExtractAll_Click);
            // 
            // BtnExtract
            // 
            this.BtnExtract.Centered = true;
            this.BtnExtract.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnExtract.Hover = true;
            this.BtnExtract.Image = global::Ohana3DS_Rebirth.Properties.Resources.icn_arrow_up;
            this.BtnExtract.Location = new System.Drawing.Point(104, 0);
            this.BtnExtract.Margin = new System.Windows.Forms.Padding(0);
            this.BtnExtract.Name = "BtnExtract";
            this.BtnExtract.Size = new System.Drawing.Size(104, 24);
            this.BtnExtract.TabIndex = 1;
            this.BtnExtract.Text = "Extract";
            this.BtnExtract.Click += new System.EventHandler(this.BtnExtract_Click);
            // 
            // BtnLoad
            // 
            this.BtnLoad.Centered = true;
            this.BtnLoad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnLoad.Hover = true;
            this.BtnLoad.Image = global::Ohana3DS_Rebirth.Properties.Resources.icn_open;
            this.BtnLoad.Location = new System.Drawing.Point(0, 0);
            this.BtnLoad.Margin = new System.Windows.Forms.Padding(0);
            this.BtnLoad.Name = "BtnLoad";
            this.BtnLoad.Size = new System.Drawing.Size(104, 24);
            this.BtnLoad.TabIndex = 0;
            this.BtnLoad.Text = "Load";
            this.BtnLoad.Click += new System.EventHandler(this.BtnLoad_Click);
            // 
            // OContainerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 256);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "OContainerForm";
            this.Text = "OContainerForm";
            this.ContentContainer.ResumeLayout(false);
            this.TopControls.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private GUI.OList FileList;
        private System.Windows.Forms.TableLayoutPanel TopControls;
        private GUI.OButton BtnExtractAll;
        private GUI.OButton BtnExtract;
        private GUI.OButton BtnLoad;
    }
}