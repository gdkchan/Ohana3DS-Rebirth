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
            this.Content = new Ohana3DS_Rebirth.GUI.OScrollablePanel();
            this.MaterialAnimationsGroup = new Ohana3DS_Rebirth.GUI.OGroupBox();
            this.AnimationsListMA = new Ohana3DS_Rebirth.GUI.OList();
            this.ControlsPanelMA = new System.Windows.Forms.TableLayoutPanel();
            this.BtnPlayPauseMA = new System.Windows.Forms.PictureBox();
            this.BtnStopMA = new System.Windows.Forms.PictureBox();
            this.BtnPreviousMA = new System.Windows.Forms.PictureBox();
            this.BtnNextMA = new System.Windows.Forms.PictureBox();
            this.TopControlsMA = new System.Windows.Forms.TableLayoutPanel();
            this.BtnDeleteMA = new Ohana3DS_Rebirth.GUI.OButton();
            this.BtnClearMA = new Ohana3DS_Rebirth.GUI.OButton();
            this.BtnExportMA = new Ohana3DS_Rebirth.GUI.OButton();
            this.BtnImportMA = new Ohana3DS_Rebirth.GUI.OButton();
            this.SkeletalAnimationsGroup = new Ohana3DS_Rebirth.GUI.OGroupBox();
            this.AnimationsListSA = new Ohana3DS_Rebirth.GUI.OList();
            this.TopControlsSA = new System.Windows.Forms.TableLayoutPanel();
            this.BtnDeleteSA = new Ohana3DS_Rebirth.GUI.OButton();
            this.BtnClearSA = new Ohana3DS_Rebirth.GUI.OButton();
            this.BtnExportSA = new Ohana3DS_Rebirth.GUI.OButton();
            this.BtnImportSA = new Ohana3DS_Rebirth.GUI.OButton();
            this.ControlsPanelSA = new System.Windows.Forms.TableLayoutPanel();
            this.BtnPlayPauseSA = new System.Windows.Forms.PictureBox();
            this.BtnStopSA = new System.Windows.Forms.PictureBox();
            this.BtnPreviousSA = new System.Windows.Forms.PictureBox();
            this.BtnNextSA = new System.Windows.Forms.PictureBox();
            this.Content.ContentArea.SuspendLayout();
            this.Content.SuspendLayout();
            this.MaterialAnimationsGroup.ContentArea.SuspendLayout();
            this.MaterialAnimationsGroup.SuspendLayout();
            this.ControlsPanelMA.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BtnPlayPauseMA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnStopMA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnPreviousMA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnNextMA)).BeginInit();
            this.TopControlsMA.SuspendLayout();
            this.SkeletalAnimationsGroup.ContentArea.SuspendLayout();
            this.SkeletalAnimationsGroup.SuspendLayout();
            this.TopControlsSA.SuspendLayout();
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
            this.Content.ContentArea.Controls.Add(this.MaterialAnimationsGroup);
            this.Content.ContentArea.Controls.Add(this.SkeletalAnimationsGroup);
            this.Content.ContentArea.Location = new System.Drawing.Point(0, 0);
            this.Content.ContentArea.Name = "ContentArea";
            this.Content.ContentArea.Size = new System.Drawing.Size(256, 512);
            this.Content.ContentArea.TabIndex = 0;
            this.Content.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Content.Location = new System.Drawing.Point(0, 16);
            this.Content.Name = "Content";
            this.Content.Size = new System.Drawing.Size(256, 784);
            this.Content.TabIndex = 1;
            // 
            // MaterialAnimationsGroup
            // 
            this.MaterialAnimationsGroup.BackColor = System.Drawing.Color.Black;
            // 
            // MaterialAnimationsGroup.ContentArea
            // 
            this.MaterialAnimationsGroup.ContentArea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.MaterialAnimationsGroup.ContentArea.Controls.Add(this.AnimationsListMA);
            this.MaterialAnimationsGroup.ContentArea.Controls.Add(this.ControlsPanelMA);
            this.MaterialAnimationsGroup.ContentArea.Controls.Add(this.TopControlsMA);
            this.MaterialAnimationsGroup.ContentArea.Location = new System.Drawing.Point(1, 16);
            this.MaterialAnimationsGroup.ContentArea.Name = "ContentArea";
            this.MaterialAnimationsGroup.ContentArea.Size = new System.Drawing.Size(254, 239);
            this.MaterialAnimationsGroup.ContentArea.TabIndex = 0;
            this.MaterialAnimationsGroup.ContentColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.MaterialAnimationsGroup.Dock = System.Windows.Forms.DockStyle.Top;
            this.MaterialAnimationsGroup.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaterialAnimationsGroup.Location = new System.Drawing.Point(0, 256);
            this.MaterialAnimationsGroup.Name = "MaterialAnimationsGroup";
            this.MaterialAnimationsGroup.Size = new System.Drawing.Size(256, 256);
            this.MaterialAnimationsGroup.TabIndex = 2;
            this.MaterialAnimationsGroup.Title = "Material Animations";
            // 
            // AnimationsListMA
            // 
            this.AnimationsListMA.BackColor = System.Drawing.Color.Transparent;
            this.AnimationsListMA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AnimationsListMA.HeaderHeight = 24;
            this.AnimationsListMA.ItemHeight = 16;
            this.AnimationsListMA.Location = new System.Drawing.Point(0, 24);
            this.AnimationsListMA.Name = "AnimationsListMA";
            this.AnimationsListMA.SelectedIndex = -1;
            this.AnimationsListMA.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(15)))), ((int)(((byte)(82)))), ((int)(((byte)(186)))));
            this.AnimationsListMA.Size = new System.Drawing.Size(254, 189);
            this.AnimationsListMA.TabIndex = 7;
            this.AnimationsListMA.SelectedIndexChanged += new System.EventHandler(this.AnimationsListMA_SelectedIndexChanged);
            // 
            // ControlsPanelMA
            // 
            this.ControlsPanelMA.ColumnCount = 6;
            this.ControlsPanelMA.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.ControlsPanelMA.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.ControlsPanelMA.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.ControlsPanelMA.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.ControlsPanelMA.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.ControlsPanelMA.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.ControlsPanelMA.Controls.Add(this.BtnPlayPauseMA, 2, 0);
            this.ControlsPanelMA.Controls.Add(this.BtnStopMA, 3, 0);
            this.ControlsPanelMA.Controls.Add(this.BtnPreviousMA, 1, 0);
            this.ControlsPanelMA.Controls.Add(this.BtnNextMA, 4, 0);
            this.ControlsPanelMA.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ControlsPanelMA.Location = new System.Drawing.Point(0, 213);
            this.ControlsPanelMA.Name = "ControlsPanelMA";
            this.ControlsPanelMA.RowCount = 1;
            this.ControlsPanelMA.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ControlsPanelMA.Size = new System.Drawing.Size(254, 26);
            this.ControlsPanelMA.TabIndex = 6;
            // 
            // BtnPlayPauseMA
            // 
            this.BtnPlayPauseMA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnPlayPauseMA.Image = global::Ohana3DS_Rebirth.Properties.Resources.icn_big_play;
            this.BtnPlayPauseMA.Location = new System.Drawing.Point(104, 3);
            this.BtnPlayPauseMA.Name = "BtnPlayPauseMA";
            this.BtnPlayPauseMA.Size = new System.Drawing.Size(20, 20);
            this.BtnPlayPauseMA.TabIndex = 0;
            this.BtnPlayPauseMA.TabStop = false;
            this.BtnPlayPauseMA.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnPlayPauseMA_MouseDown);
            this.BtnPlayPauseMA.MouseEnter += new System.EventHandler(this.Control_MouseEnter);
            this.BtnPlayPauseMA.MouseLeave += new System.EventHandler(this.Control_MouseLeave);
            // 
            // BtnStopMA
            // 
            this.BtnStopMA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnStopMA.Image = global::Ohana3DS_Rebirth.Properties.Resources.icn_big_stop;
            this.BtnStopMA.Location = new System.Drawing.Point(130, 3);
            this.BtnStopMA.Name = "BtnStopMA";
            this.BtnStopMA.Size = new System.Drawing.Size(20, 20);
            this.BtnStopMA.TabIndex = 1;
            this.BtnStopMA.TabStop = false;
            this.BtnStopMA.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnStopMA_MouseDown);
            this.BtnStopMA.MouseEnter += new System.EventHandler(this.Control_MouseEnter);
            this.BtnStopMA.MouseLeave += new System.EventHandler(this.Control_MouseLeave);
            // 
            // BtnPreviousMA
            // 
            this.BtnPreviousMA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnPreviousMA.Image = global::Ohana3DS_Rebirth.Properties.Resources.icn_big_previous;
            this.BtnPreviousMA.Location = new System.Drawing.Point(78, 3);
            this.BtnPreviousMA.Name = "BtnPreviousMA";
            this.BtnPreviousMA.Size = new System.Drawing.Size(20, 20);
            this.BtnPreviousMA.TabIndex = 2;
            this.BtnPreviousMA.TabStop = false;
            this.BtnPreviousMA.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnPreviousMA_MouseDown);
            this.BtnPreviousMA.MouseEnter += new System.EventHandler(this.Control_MouseEnter);
            this.BtnPreviousMA.MouseLeave += new System.EventHandler(this.Control_MouseLeave);
            // 
            // BtnNextMA
            // 
            this.BtnNextMA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnNextMA.Image = global::Ohana3DS_Rebirth.Properties.Resources.icn_big_next;
            this.BtnNextMA.Location = new System.Drawing.Point(156, 3);
            this.BtnNextMA.Name = "BtnNextMA";
            this.BtnNextMA.Size = new System.Drawing.Size(20, 20);
            this.BtnNextMA.TabIndex = 3;
            this.BtnNextMA.TabStop = false;
            this.BtnNextMA.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnNextMA_MouseDown);
            this.BtnNextMA.MouseEnter += new System.EventHandler(this.Control_MouseEnter);
            this.BtnNextMA.MouseLeave += new System.EventHandler(this.Control_MouseLeave);
            // 
            // TopControlsMA
            // 
            this.TopControlsMA.ColumnCount = 4;
            this.TopControlsMA.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.TopControlsMA.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.TopControlsMA.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.TopControlsMA.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.TopControlsMA.Controls.Add(this.BtnDeleteMA, 0, 0);
            this.TopControlsMA.Controls.Add(this.BtnClearMA, 0, 0);
            this.TopControlsMA.Controls.Add(this.BtnExportMA, 0, 0);
            this.TopControlsMA.Controls.Add(this.BtnImportMA, 0, 0);
            this.TopControlsMA.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopControlsMA.Location = new System.Drawing.Point(0, 0);
            this.TopControlsMA.Name = "TopControlsMA";
            this.TopControlsMA.RowCount = 1;
            this.TopControlsMA.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TopControlsMA.Size = new System.Drawing.Size(254, 24);
            this.TopControlsMA.TabIndex = 5;
            // 
            // BtnDeleteMA
            // 
            this.BtnDeleteMA.BackColor = System.Drawing.Color.Transparent;
            this.BtnDeleteMA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnDeleteMA.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDeleteMA.Image = global::Ohana3DS_Rebirth.Properties.Resources.icn_close;
            this.BtnDeleteMA.Location = new System.Drawing.Point(129, 3);
            this.BtnDeleteMA.Name = "BtnDeleteMA";
            this.BtnDeleteMA.Size = new System.Drawing.Size(57, 18);
            this.BtnDeleteMA.TabIndex = 9;
            this.BtnDeleteMA.Text = "Delete";
            this.BtnDeleteMA.Click += new System.EventHandler(this.BtnDeleteMA_Click);
            // 
            // BtnClearMA
            // 
            this.BtnClearMA.BackColor = System.Drawing.Color.Transparent;
            this.BtnClearMA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnClearMA.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnClearMA.Image = global::Ohana3DS_Rebirth.Properties.Resources.icn_trash;
            this.BtnClearMA.Location = new System.Drawing.Point(192, 3);
            this.BtnClearMA.Name = "BtnClearMA";
            this.BtnClearMA.Size = new System.Drawing.Size(59, 18);
            this.BtnClearMA.TabIndex = 8;
            this.BtnClearMA.Text = "Clear";
            this.BtnClearMA.Click += new System.EventHandler(this.BtnClearMA_Click);
            // 
            // BtnExportMA
            // 
            this.BtnExportMA.BackColor = System.Drawing.Color.Transparent;
            this.BtnExportMA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnExportMA.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnExportMA.Image = global::Ohana3DS_Rebirth.Properties.Resources.icn_arrow_up;
            this.BtnExportMA.Location = new System.Drawing.Point(3, 3);
            this.BtnExportMA.Name = "BtnExportMA";
            this.BtnExportMA.Size = new System.Drawing.Size(57, 18);
            this.BtnExportMA.TabIndex = 7;
            this.BtnExportMA.Text = "Export";
            // 
            // BtnImportMA
            // 
            this.BtnImportMA.BackColor = System.Drawing.Color.Transparent;
            this.BtnImportMA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnImportMA.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnImportMA.Image = global::Ohana3DS_Rebirth.Properties.Resources.icn_arrow_down;
            this.BtnImportMA.Location = new System.Drawing.Point(66, 3);
            this.BtnImportMA.Name = "BtnImportMA";
            this.BtnImportMA.Size = new System.Drawing.Size(57, 18);
            this.BtnImportMA.TabIndex = 6;
            this.BtnImportMA.Text = "Import";
            this.BtnImportMA.Click += new System.EventHandler(this.BtnImportMA_Click);
            // 
            // SkeletalAnimationsGroup
            // 
            this.SkeletalAnimationsGroup.BackColor = System.Drawing.Color.Black;
            // 
            // SkeletalAnimationsGroup.ContentArea
            // 
            this.SkeletalAnimationsGroup.ContentArea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.SkeletalAnimationsGroup.ContentArea.Controls.Add(this.AnimationsListSA);
            this.SkeletalAnimationsGroup.ContentArea.Controls.Add(this.TopControlsSA);
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
            this.AnimationsListSA.SelectedIndexChanged += new System.EventHandler(this.AnimationsListSA_SelectedIndexChanged);
            // 
            // TopControlsSA
            // 
            this.TopControlsSA.ColumnCount = 4;
            this.TopControlsSA.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.TopControlsSA.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.TopControlsSA.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.TopControlsSA.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.TopControlsSA.Controls.Add(this.BtnDeleteSA, 0, 0);
            this.TopControlsSA.Controls.Add(this.BtnClearSA, 0, 0);
            this.TopControlsSA.Controls.Add(this.BtnExportSA, 0, 0);
            this.TopControlsSA.Controls.Add(this.BtnImportSA, 0, 0);
            this.TopControlsSA.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopControlsSA.Location = new System.Drawing.Point(0, 0);
            this.TopControlsSA.Name = "TopControlsSA";
            this.TopControlsSA.RowCount = 1;
            this.TopControlsSA.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TopControlsSA.Size = new System.Drawing.Size(254, 24);
            this.TopControlsSA.TabIndex = 4;
            // 
            // BtnDeleteSA
            // 
            this.BtnDeleteSA.BackColor = System.Drawing.Color.Transparent;
            this.BtnDeleteSA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnDeleteSA.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDeleteSA.Image = global::Ohana3DS_Rebirth.Properties.Resources.icn_close;
            this.BtnDeleteSA.Location = new System.Drawing.Point(129, 3);
            this.BtnDeleteSA.Name = "BtnDeleteSA";
            this.BtnDeleteSA.Size = new System.Drawing.Size(57, 18);
            this.BtnDeleteSA.TabIndex = 9;
            this.BtnDeleteSA.Text = "Delete";
            this.BtnDeleteSA.Click += new System.EventHandler(this.BtnDeleteSA_Click);
            // 
            // BtnClearSA
            // 
            this.BtnClearSA.BackColor = System.Drawing.Color.Transparent;
            this.BtnClearSA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnClearSA.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnClearSA.Image = global::Ohana3DS_Rebirth.Properties.Resources.icn_trash;
            this.BtnClearSA.Location = new System.Drawing.Point(192, 3);
            this.BtnClearSA.Name = "BtnClearSA";
            this.BtnClearSA.Size = new System.Drawing.Size(59, 18);
            this.BtnClearSA.TabIndex = 8;
            this.BtnClearSA.Text = "Clear";
            this.BtnClearSA.Click += new System.EventHandler(this.BtnClearSA_Click);
            // 
            // BtnExportSA
            // 
            this.BtnExportSA.BackColor = System.Drawing.Color.Transparent;
            this.BtnExportSA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnExportSA.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnExportSA.Image = global::Ohana3DS_Rebirth.Properties.Resources.icn_arrow_up;
            this.BtnExportSA.Location = new System.Drawing.Point(3, 3);
            this.BtnExportSA.Name = "BtnExportSA";
            this.BtnExportSA.Size = new System.Drawing.Size(57, 18);
            this.BtnExportSA.TabIndex = 7;
            this.BtnExportSA.Text = "Export";
            // 
            // BtnImportSA
            // 
            this.BtnImportSA.BackColor = System.Drawing.Color.Transparent;
            this.BtnImportSA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnImportSA.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnImportSA.Image = global::Ohana3DS_Rebirth.Properties.Resources.icn_arrow_down;
            this.BtnImportSA.Location = new System.Drawing.Point(66, 3);
            this.BtnImportSA.Name = "BtnImportSA";
            this.BtnImportSA.Size = new System.Drawing.Size(57, 18);
            this.BtnImportSA.TabIndex = 6;
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
            // OAnimationsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Content);
            this.Name = "OAnimationsWindow";
            this.Size = new System.Drawing.Size(256, 800);
            this.Controls.SetChildIndex(this.Content, 0);
            this.Content.ContentArea.ResumeLayout(false);
            this.Content.ResumeLayout(false);
            this.Content.PerformLayout();
            this.MaterialAnimationsGroup.ContentArea.ResumeLayout(false);
            this.MaterialAnimationsGroup.ResumeLayout(false);
            this.MaterialAnimationsGroup.PerformLayout();
            this.ControlsPanelMA.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BtnPlayPauseMA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnStopMA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnPreviousMA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnNextMA)).EndInit();
            this.TopControlsMA.ResumeLayout(false);
            this.SkeletalAnimationsGroup.ContentArea.ResumeLayout(false);
            this.SkeletalAnimationsGroup.ResumeLayout(false);
            this.SkeletalAnimationsGroup.PerformLayout();
            this.TopControlsSA.ResumeLayout(false);
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
        private System.Windows.Forms.TableLayoutPanel ControlsPanelSA;
        private System.Windows.Forms.PictureBox BtnPlayPauseSA;
        private System.Windows.Forms.PictureBox BtnStopSA;
        private System.Windows.Forms.PictureBox BtnPreviousSA;
        private System.Windows.Forms.PictureBox BtnNextSA;
        private System.Windows.Forms.TableLayoutPanel TopControlsSA;
        private OButton BtnClearSA;
        private OButton BtnExportSA;
        private OButton BtnImportSA;
        private OButton BtnDeleteSA;
        private OGroupBox MaterialAnimationsGroup;
        private System.Windows.Forms.TableLayoutPanel TopControlsMA;
        private OButton BtnDeleteMA;
        private OButton BtnClearMA;
        private OButton BtnExportMA;
        private OButton BtnImportMA;
        private OList AnimationsListMA;
        private System.Windows.Forms.TableLayoutPanel ControlsPanelMA;
        private System.Windows.Forms.PictureBox BtnPlayPauseMA;
        private System.Windows.Forms.PictureBox BtnStopMA;
        private System.Windows.Forms.PictureBox BtnPreviousMA;
        private System.Windows.Forms.PictureBox BtnNextMA;

    }
}
