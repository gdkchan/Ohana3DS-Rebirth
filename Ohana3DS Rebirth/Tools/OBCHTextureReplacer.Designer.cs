namespace Ohana3DS_Rebirth.Tools
{
    partial class OBCHTextureReplacer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OBCHTextureReplacer));
            this.PicPreview = new System.Windows.Forms.PictureBox();
            this.TextureList = new Ohana3DS_Rebirth.GUI.OList();
            this.BtnReplaceAll = new Ohana3DS_Rebirth.GUI.OButton();
            this.BtnReplace = new Ohana3DS_Rebirth.GUI.OButton();
            this.BtnExportAll = new Ohana3DS_Rebirth.GUI.OButton();
            this.BtnExport = new Ohana3DS_Rebirth.GUI.OButton();
            this.SplitPanel = new System.Windows.Forms.SplitContainer();
            this.ButtonsLayout = new System.Windows.Forms.TableLayoutPanel();
            this.TopMenu = new System.Windows.Forms.MenuStrip();
            this.MenuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuSave = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuSaveAndPreview = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuSeparator0 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.ContentContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SplitPanel)).BeginInit();
            this.SplitPanel.Panel1.SuspendLayout();
            this.SplitPanel.Panel2.SuspendLayout();
            this.SplitPanel.SuspendLayout();
            this.ButtonsLayout.SuspendLayout();
            this.TopMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // ContentContainer
            // 
            this.ContentContainer.Controls.Add(this.SplitPanel);
            this.ContentContainer.Controls.Add(this.TopMenu);
            this.ContentContainer.Size = new System.Drawing.Size(632, 472);
            this.ContentContainer.Controls.SetChildIndex(this.TopMenu, 0);
            this.ContentContainer.Controls.SetChildIndex(this.SplitPanel, 0);
            // 
            // PicPreview
            // 
            this.PicPreview.BackgroundImage = global::Ohana3DS_Rebirth.Properties.Resources.ui_alpha_grid;
            this.PicPreview.Location = new System.Drawing.Point(0, 0);
            this.PicPreview.Margin = new System.Windows.Forms.Padding(0);
            this.PicPreview.Name = "PicPreview";
            this.PicPreview.Size = new System.Drawing.Size(64, 64);
            this.PicPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.PicPreview.TabIndex = 10;
            this.PicPreview.TabStop = false;
            // 
            // TextureList
            // 
            this.TextureList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.TextureList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextureList.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextureList.HeaderHeight = 24;
            this.TextureList.ItemHeight = 24;
            this.TextureList.Location = new System.Drawing.Point(0, 30);
            this.TextureList.Margin = new System.Windows.Forms.Padding(0);
            this.TextureList.Name = "TextureList";
            this.TextureList.SelectedIndex = -1;
            this.TextureList.Size = new System.Drawing.Size(288, 398);
            this.TextureList.TabIndex = 17;
            this.TextureList.SelectedIndexChanged += new System.EventHandler(this.TextureList_SelectedIndexChanged);
            // 
            // BtnReplaceAll
            // 
            this.BtnReplaceAll.Centered = true;
            this.BtnReplaceAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnReplaceAll.Image = null;
            this.BtnReplaceAll.Location = new System.Drawing.Point(219, 3);
            this.BtnReplaceAll.Name = "BtnReplaceAll";
            this.BtnReplaceAll.Size = new System.Drawing.Size(66, 24);
            this.BtnReplaceAll.TabIndex = 3;
            this.BtnReplaceAll.Text = "Replace all";
            this.BtnReplaceAll.Click += new System.EventHandler(this.BtnReplaceAll_Click);
            // 
            // BtnReplace
            // 
            this.BtnReplace.Centered = true;
            this.BtnReplace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnReplace.Image = null;
            this.BtnReplace.Location = new System.Drawing.Point(147, 3);
            this.BtnReplace.Name = "BtnReplace";
            this.BtnReplace.Size = new System.Drawing.Size(66, 24);
            this.BtnReplace.TabIndex = 2;
            this.BtnReplace.Text = "Replace";
            this.BtnReplace.Click += new System.EventHandler(this.BtnReplace_Click);
            // 
            // BtnExportAll
            // 
            this.BtnExportAll.Centered = true;
            this.BtnExportAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnExportAll.Image = null;
            this.BtnExportAll.Location = new System.Drawing.Point(75, 3);
            this.BtnExportAll.Name = "BtnExportAll";
            this.BtnExportAll.Size = new System.Drawing.Size(66, 24);
            this.BtnExportAll.TabIndex = 1;
            this.BtnExportAll.Text = "Export all";
            this.BtnExportAll.Click += new System.EventHandler(this.BtnExportAll_Click);
            // 
            // BtnExport
            // 
            this.BtnExport.Centered = true;
            this.BtnExport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnExport.Image = null;
            this.BtnExport.Location = new System.Drawing.Point(3, 3);
            this.BtnExport.Name = "BtnExport";
            this.BtnExport.Size = new System.Drawing.Size(66, 24);
            this.BtnExport.TabIndex = 0;
            this.BtnExport.Text = "Export";
            this.BtnExport.Click += new System.EventHandler(this.BtnExport_Click);
            // 
            // SplitPanel
            // 
            this.SplitPanel.BackColor = System.Drawing.Color.Black;
            this.SplitPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitPanel.Location = new System.Drawing.Point(0, 44);
            this.SplitPanel.Name = "SplitPanel";
            // 
            // SplitPanel.Panel1
            // 
            this.SplitPanel.Panel1.Controls.Add(this.TextureList);
            this.SplitPanel.Panel1.Controls.Add(this.ButtonsLayout);
            // 
            // SplitPanel.Panel2
            // 
            this.SplitPanel.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.SplitPanel.Panel2.Controls.Add(this.PicPreview);
            this.SplitPanel.Size = new System.Drawing.Size(632, 428);
            this.SplitPanel.SplitterDistance = 288;
            this.SplitPanel.TabIndex = 17;
            // 
            // ButtonsLayout
            // 
            this.ButtonsLayout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.ButtonsLayout.ColumnCount = 4;
            this.ButtonsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.ButtonsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.ButtonsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.ButtonsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.ButtonsLayout.Controls.Add(this.BtnReplaceAll, 3, 0);
            this.ButtonsLayout.Controls.Add(this.BtnExport, 0, 0);
            this.ButtonsLayout.Controls.Add(this.BtnReplace, 2, 0);
            this.ButtonsLayout.Controls.Add(this.BtnExportAll, 1, 0);
            this.ButtonsLayout.Dock = System.Windows.Forms.DockStyle.Top;
            this.ButtonsLayout.Location = new System.Drawing.Point(0, 0);
            this.ButtonsLayout.Name = "ButtonsLayout";
            this.ButtonsLayout.RowCount = 1;
            this.ButtonsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ButtonsLayout.Size = new System.Drawing.Size(288, 30);
            this.ButtonsLayout.TabIndex = 0;
            // 
            // TopMenu
            // 
            this.TopMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuFile});
            this.TopMenu.Location = new System.Drawing.Point(0, 20);
            this.TopMenu.Name = "TopMenu";
            this.TopMenu.Size = new System.Drawing.Size(632, 24);
            this.TopMenu.TabIndex = 16;
            // 
            // MenuFile
            // 
            this.MenuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuOpen,
            this.MenuSave,
            this.MenuSaveAndPreview,
            this.MenuSeparator0,
            this.MenuExit});
            this.MenuFile.Name = "MenuFile";
            this.MenuFile.Size = new System.Drawing.Size(37, 20);
            this.MenuFile.Text = "&File";
            // 
            // MenuOpen
            // 
            this.MenuOpen.Image = global::Ohana3DS_Rebirth.Properties.Resources.ui_icon_folder;
            this.MenuOpen.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.MenuOpen.Name = "MenuOpen";
            this.MenuOpen.Size = new System.Drawing.Size(187, 26);
            this.MenuOpen.Text = "&Open (O)";
            this.MenuOpen.Click += new System.EventHandler(this.MenuOpen_Click);
            // 
            // MenuSave
            // 
            this.MenuSave.Image = global::Ohana3DS_Rebirth.Properties.Resources.ui_icon_floppy;
            this.MenuSave.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.MenuSave.Name = "MenuSave";
            this.MenuSave.Size = new System.Drawing.Size(187, 26);
            this.MenuSave.Text = "&Save (S)";
            this.MenuSave.Click += new System.EventHandler(this.MenuSave_Click);
            // 
            // MenuSaveAndPreview
            // 
            this.MenuSaveAndPreview.Image = global::Ohana3DS_Rebirth.Properties.Resources.ui_icon_floppy;
            this.MenuSaveAndPreview.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.MenuSaveAndPreview.Name = "MenuSaveAndPreview";
            this.MenuSaveAndPreview.Size = new System.Drawing.Size(187, 26);
            this.MenuSaveAndPreview.Text = "&Save and preview (P)";
            this.MenuSaveAndPreview.Click += new System.EventHandler(this.MenuSaveAndPreview_Click);
            // 
            // MenuSeparator0
            // 
            this.MenuSeparator0.Name = "MenuSeparator0";
            this.MenuSeparator0.Size = new System.Drawing.Size(184, 6);
            // 
            // MenuExit
            // 
            this.MenuExit.Image = global::Ohana3DS_Rebirth.Properties.Resources.ui_icon_block;
            this.MenuExit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.MenuExit.Name = "MenuExit";
            this.MenuExit.Size = new System.Drawing.Size(187, 26);
            this.MenuExit.Text = "&Exit";
            this.MenuExit.Click += new System.EventHandler(this.MenuExit_Click);
            // 
            // OBCHTextureReplacer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 480);
            this.KeyPreview = true;
            this.MainMenuStrip = this.TopMenu;
            this.Name = "OBCHTextureReplacer";
            this.Text = "BCH texture replacer";
            this.TitleIcon = ((System.Drawing.Image)(resources.GetObject("$this.TitleIcon")));
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OBCHTextureReplacer_KeyDown);
            this.ContentContainer.ResumeLayout(false);
            this.ContentContainer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicPreview)).EndInit();
            this.SplitPanel.Panel1.ResumeLayout(false);
            this.SplitPanel.Panel2.ResumeLayout(false);
            this.SplitPanel.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitPanel)).EndInit();
            this.SplitPanel.ResumeLayout(false);
            this.ButtonsLayout.ResumeLayout(false);
            this.TopMenu.ResumeLayout(false);
            this.TopMenu.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox PicPreview;
        private GUI.OList TextureList;
        private GUI.OButton BtnReplaceAll;
        private GUI.OButton BtnReplace;
        private GUI.OButton BtnExportAll;
        private GUI.OButton BtnExport;
        private System.Windows.Forms.SplitContainer SplitPanel;
        private System.Windows.Forms.TableLayoutPanel ButtonsLayout;
        private System.Windows.Forms.MenuStrip TopMenu;
        private System.Windows.Forms.ToolStripMenuItem MenuFile;
        private System.Windows.Forms.ToolStripMenuItem MenuOpen;
        private System.Windows.Forms.ToolStripMenuItem MenuSave;
        private System.Windows.Forms.ToolStripMenuItem MenuSaveAndPreview;
        private System.Windows.Forms.ToolStripSeparator MenuSeparator0;
        private System.Windows.Forms.ToolStripMenuItem MenuExit;
    }
}