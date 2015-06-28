namespace Ohana3DS_Rebirth.GUI
{
    partial class OAnimationsWindow
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Content = new Ohana3DS_Rebirth.GUI.OScrollablePanel();
            this.SkeletalAnimationsGroup = new Ohana3DS_Rebirth.GUI.OGroupBox();
            this.AnimationsListSA = new Ohana3DS_Rebirth.GUI.OList();
            this.TopControlsPanelSA = new System.Windows.Forms.Panel();
            this.BtnClearSA = new Ohana3DS_Rebirth.GUI.OButton();
            this.BtnExportSA = new Ohana3DS_Rebirth.GUI.OButton();
            this.BtnImportSA = new Ohana3DS_Rebirth.GUI.OButton();
            this.ControlsPanelSA = new System.Windows.Forms.TableLayoutPanel();
            this.BtnPlayPauseSA = new System.Windows.Forms.PictureBox();
            this.BtnStopSA = new System.Windows.Forms.PictureBox();
            this.BtnPreviousSA = new System.Windows.Forms.PictureBox();
            this.BtnNextSA = new System.Windows.Forms.PictureBox();
            this.ListScroll = new Ohana3DS_Rebirth.GUI.OVScroll(this.components);
            this.Content.ContentArea.SuspendLayout();
            this.Content.SuspendLayout();
            this.SkeletalAnimationsGroup.ContentArea.SuspendLayout();
            this.SkeletalAnimationsGroup.SuspendLayout();
            this.TopControlsPanelSA.SuspendLayout();
            this.ControlsPanelSA.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BtnPlayPauseSA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnStopSA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnPreviousSA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnNextSA)).BeginInit();
            this.SuspendLayout();
            // 
            // Content
            // 
            this.Content.BackColor = System.Drawing.Color.Transparent;
            // 
            // Content.ContentArea
            // 
            this.Content.ContentArea.AutoSize = true;
            this.Content.ContentArea.BackColor = System.Drawing.Color.Transparent;
            this.Content.ContentArea.Controls.Add(this.SkeletalAnimationsGroup);
            this.Content.ContentArea.Location = new System.Drawing.Point(0, 0);
            this.Content.ContentArea.Name = "ContentArea";
            this.Content.ContentArea.Size = new System.Drawing.Size(256, 256);
            this.Content.ContentArea.TabIndex = 0;
            this.Content.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Content.Location = new System.Drawing.Point(0, 16);
            this.Content.Name = "Content";
            this.Content.Size = new System.Drawing.Size(256, 384);
            this.Content.TabIndex = 1;
            // 
            // SkeletalAnimationsGroup
            // 
            this.SkeletalAnimationsGroup.BackColor = System.Drawing.Color.Black;
            // 
            // SkeletalAnimationsGroup.ContentArea
            // 
            this.SkeletalAnimationsGroup.ContentArea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.SkeletalAnimationsGroup.ContentArea.Controls.Add(this.AnimationsListSA);
            this.SkeletalAnimationsGroup.ContentArea.Controls.Add(this.TopControlsPanelSA);
            this.SkeletalAnimationsGroup.ContentArea.Controls.Add(this.ControlsPanelSA);
            this.SkeletalAnimationsGroup.ContentArea.Location = new System.Drawing.Point(1, 16);
            this.SkeletalAnimationsGroup.ContentArea.Name = "ContentArea";
            this.SkeletalAnimationsGroup.ContentArea.Size = new System.Drawing.Size(254, 239);
            this.SkeletalAnimationsGroup.ContentArea.TabIndex = 0;
            this.SkeletalAnimationsGroup.ContentColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.SkeletalAnimationsGroup.Dock = System.Windows.Forms.DockStyle.Top;
            this.SkeletalAnimationsGroup.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SkeletalAnimationsGroup.Location = new System.Drawing.Point(0, 0);
            this.SkeletalAnimationsGroup.Name = "SkeletalAnimationsGroup";
            this.SkeletalAnimationsGroup.Size = new System.Drawing.Size(256, 256);
            this.SkeletalAnimationsGroup.TabIndex = 5;
            this.SkeletalAnimationsGroup.Title = "Skeletal Animations";
            // 
            // AnimationsListSA
            // 
            this.AnimationsListSA.BackColor = System.Drawing.Color.Transparent;
            this.AnimationsListSA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AnimationsListSA.HeaderHeight = 24;
            this.AnimationsListSA.ItemHeight = 16;
            this.AnimationsListSA.Location = new System.Drawing.Point(0, 24);
            this.AnimationsListSA.Name = "AnimationsListSA";
            this.AnimationsListSA.SelectedIndex = -1;
            this.AnimationsListSA.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(15)))), ((int)(((byte)(82)))), ((int)(((byte)(186)))));
            this.AnimationsListSA.Size = new System.Drawing.Size(254, 189);
            this.AnimationsListSA.TabIndex = 2;
            this.AnimationsListSA.SelectedIndexChanged += new System.EventHandler(this.AnimationsList_SelectedIndexChanged);
            // 
            // TopControlsPanelSA
            // 
            this.TopControlsPanelSA.Controls.Add(this.BtnClearSA);
            this.TopControlsPanelSA.Controls.Add(this.BtnExportSA);
            this.TopControlsPanelSA.Controls.Add(this.BtnImportSA);
            this.TopControlsPanelSA.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopControlsPanelSA.Location = new System.Drawing.Point(0, 0);
            this.TopControlsPanelSA.Name = "TopControlsPanelSA";
            this.TopControlsPanelSA.Size = new System.Drawing.Size(254, 24);
            this.TopControlsPanelSA.TabIndex = 3;
            // 
            // BtnClearSA
            // 
            this.BtnClearSA.BackColor = System.Drawing.Color.Transparent;
            this.BtnClearSA.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnClearSA.Image = global::Ohana3DS_Rebirth.Properties.Resources.icn_trash;
            this.BtnClearSA.Location = new System.Drawing.Point(136, 0);
            this.BtnClearSA.Name = "BtnClearSA";
            this.BtnClearSA.Size = new System.Drawing.Size(68, 24);
            this.BtnClearSA.TabIndex = 7;
            this.BtnClearSA.Text = "Clear";
            this.BtnClearSA.Click += new System.EventHandler(this.BtnClearSA_Click);
            // 
            // BtnExportSA
            // 
            this.BtnExportSA.BackColor = System.Drawing.Color.Transparent;
            this.BtnExportSA.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnExportSA.Image = global::Ohana3DS_Rebirth.Properties.Resources.icn_arrow_up;
            this.BtnExportSA.Location = new System.Drawing.Point(68, 0);
            this.BtnExportSA.Name = "BtnExportSA";
            this.BtnExportSA.Size = new System.Drawing.Size(68, 24);
            this.BtnExportSA.TabIndex = 6;
            this.BtnExportSA.Text = "Export";
            // 
            // BtnImportSA
            // 
            this.BtnImportSA.BackColor = System.Drawing.Color.Transparent;
            this.BtnImportSA.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnImportSA.Image = global::Ohana3DS_Rebirth.Properties.Resources.icn_arrow_down;
            this.BtnImportSA.Location = new System.Drawing.Point(0, 0);
            this.BtnImportSA.Name = "BtnImportSA";
            this.BtnImportSA.Size = new System.Drawing.Size(68, 24);
            this.BtnImportSA.TabIndex = 5;
            this.BtnImportSA.Text = "Import";
            this.BtnImportSA.Click += new System.EventHandler(this.BtnImportSA_Click);
            // 
            // ControlsPanelSA
            // 
            this.ControlsPanelSA.ColumnCount = 6;
            this.ControlsPanelSA.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.ControlsPanelSA.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.ControlsPanelSA.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.ControlsPanelSA.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.ControlsPanelSA.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.ControlsPanelSA.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.ControlsPanelSA.Controls.Add(this.BtnPlayPauseSA, 2, 0);
            this.ControlsPanelSA.Controls.Add(this.BtnStopSA, 3, 0);
            this.ControlsPanelSA.Controls.Add(this.BtnPreviousSA, 1, 0);
            this.ControlsPanelSA.Controls.Add(this.BtnNextSA, 4, 0);
            this.ControlsPanelSA.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ControlsPanelSA.Location = new System.Drawing.Point(0, 213);
            this.ControlsPanelSA.Name = "ControlsPanelSA";
            this.ControlsPanelSA.RowCount = 1;
            this.ControlsPanelSA.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ControlsPanelSA.Size = new System.Drawing.Size(254, 26);
            this.ControlsPanelSA.TabIndex = 0;
            // 
            // BtnPlayPauseSA
            // 
            this.BtnPlayPauseSA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnPlayPauseSA.Image = global::Ohana3DS_Rebirth.Properties.Resources.icn_big_play;
            this.BtnPlayPauseSA.Location = new System.Drawing.Point(104, 3);
            this.BtnPlayPauseSA.Name = "BtnPlayPauseSA";
            this.BtnPlayPauseSA.Size = new System.Drawing.Size(20, 20);
            this.BtnPlayPauseSA.TabIndex = 0;
            this.BtnPlayPauseSA.TabStop = false;
            this.BtnPlayPauseSA.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnPlayPauseSA_MouseDown);
            this.BtnPlayPauseSA.MouseEnter += new System.EventHandler(this.Control_MouseEnter);
            this.BtnPlayPauseSA.MouseLeave += new System.EventHandler(this.Control_MouseLeave);
            // 
            // BtnStopSA
            // 
            this.BtnStopSA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnStopSA.Image = global::Ohana3DS_Rebirth.Properties.Resources.icn_big_stop;
            this.BtnStopSA.Location = new System.Drawing.Point(130, 3);
            this.BtnStopSA.Name = "BtnStopSA";
            this.BtnStopSA.Size = new System.Drawing.Size(20, 20);
            this.BtnStopSA.TabIndex = 1;
            this.BtnStopSA.TabStop = false;
            this.BtnStopSA.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnStopSA_MouseDown);
            this.BtnStopSA.MouseEnter += new System.EventHandler(this.Control_MouseEnter);
            this.BtnStopSA.MouseLeave += new System.EventHandler(this.Control_MouseLeave);
            // 
            // BtnPreviousSA
            // 
            this.BtnPreviousSA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnPreviousSA.Image = global::Ohana3DS_Rebirth.Properties.Resources.icn_big_previous;
            this.BtnPreviousSA.Location = new System.Drawing.Point(78, 3);
            this.BtnPreviousSA.Name = "BtnPreviousSA";
            this.BtnPreviousSA.Size = new System.Drawing.Size(20, 20);
            this.BtnPreviousSA.TabIndex = 2;
            this.BtnPreviousSA.TabStop = false;
            this.BtnPreviousSA.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnPreviousSA_MouseDown);
            this.BtnPreviousSA.MouseEnter += new System.EventHandler(this.Control_MouseEnter);
            this.BtnPreviousSA.MouseLeave += new System.EventHandler(this.Control_MouseLeave);
            // 
            // BtnNextSA
            // 
            this.BtnNextSA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnNextSA.Image = global::Ohana3DS_Rebirth.Properties.Resources.icn_big_next;
            this.BtnNextSA.Location = new System.Drawing.Point(156, 3);
            this.BtnNextSA.Name = "BtnNextSA";
            this.BtnNextSA.Size = new System.Drawing.Size(20, 20);
            this.BtnNextSA.TabIndex = 3;
            this.BtnNextSA.TabStop = false;
            this.BtnNextSA.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnNextSA_MouseDown);
            this.BtnNextSA.MouseEnter += new System.EventHandler(this.Control_MouseEnter);
            this.BtnNextSA.MouseLeave += new System.EventHandler(this.Control_MouseLeave);
            // 
            // ListScroll
            // 
            this.ListScroll.BarColor = System.Drawing.Color.White;
            this.ListScroll.BarColorHover = System.Drawing.Color.WhiteSmoke;
            this.ListScroll.Dock = System.Windows.Forms.DockStyle.Right;
            this.ListScroll.Location = new System.Drawing.Point(246, 0);
            this.ListScroll.MaximumScroll = 100;
            this.ListScroll.Name = "ListScroll";
            this.ListScroll.Size = new System.Drawing.Size(8, 189);
            this.ListScroll.TabIndex = 0;
            this.ListScroll.Text = "ovScroll1";
            this.ListScroll.Value = 0;
            this.ListScroll.Visible = false;
            // 
            // OAnimationsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Content);
            this.Name = "OAnimationsWindow";
            this.Size = new System.Drawing.Size(256, 400);
            this.Controls.SetChildIndex(this.Content, 0);
            this.Content.ContentArea.ResumeLayout(false);
            this.Content.ResumeLayout(false);
            this.Content.PerformLayout();
            this.SkeletalAnimationsGroup.ContentArea.ResumeLayout(false);
            this.SkeletalAnimationsGroup.ResumeLayout(false);
            this.SkeletalAnimationsGroup.PerformLayout();
            this.TopControlsPanelSA.ResumeLayout(false);
            this.ControlsPanelSA.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BtnPlayPauseSA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnStopSA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnPreviousSA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnNextSA)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private OScrollablePanel Content;
        private OGroupBox SkeletalAnimationsGroup;
        private OList AnimationsListSA;
        private OVScroll ListScroll;
        private System.Windows.Forms.Panel TopControlsPanelSA;
        private OButton BtnExportSA;
        private OButton BtnImportSA;
        private System.Windows.Forms.TableLayoutPanel ControlsPanelSA;
        private System.Windows.Forms.PictureBox BtnPlayPauseSA;
        private System.Windows.Forms.PictureBox BtnStopSA;
        private System.Windows.Forms.PictureBox BtnPreviousSA;
        private System.Windows.Forms.PictureBox BtnNextSA;
        private OButton BtnClearSA;

    }
}
