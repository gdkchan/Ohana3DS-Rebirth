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
            this.Panel = new System.Windows.Forms.Panel();
            this.BtnLoad = new Ohana3DS_Rebirth.GUI.OButton();
            this.ContentContainer.SuspendLayout();
            this.Panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ContentContainer
            // 
            this.ContentContainer.Controls.Add(this.FileList);
            this.ContentContainer.Controls.Add(this.Panel);
            this.ContentContainer.Size = new System.Drawing.Size(312, 248);
            this.ContentContainer.Controls.SetChildIndex(this.Panel, 0);
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
            // Panel
            // 
            this.Panel.Controls.Add(this.BtnLoad);
            this.Panel.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel.Location = new System.Drawing.Point(0, 20);
            this.Panel.Name = "Panel";
            this.Panel.Size = new System.Drawing.Size(312, 24);
            this.Panel.TabIndex = 11;
            // 
            // BtnLoad
            // 
            this.BtnLoad.Centered = true;
            this.BtnLoad.Hover = true;
            this.BtnLoad.Image = null;
            this.BtnLoad.Location = new System.Drawing.Point(0, 0);
            this.BtnLoad.Margin = new System.Windows.Forms.Padding(0);
            this.BtnLoad.Name = "BtnLoad";
            this.BtnLoad.Size = new System.Drawing.Size(64, 24);
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
            this.Panel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private GUI.OList FileList;
        private System.Windows.Forms.Panel Panel;
        private GUI.OButton BtnLoad;
    }
}