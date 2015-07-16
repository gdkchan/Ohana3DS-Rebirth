namespace Ohana3DS_Rebirth.GUI
{
    partial class OGenericAnimationControls
    {
        /// <summary> 
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">verdade se for necessário descartar os recursos gerenciados; caso contrário, falso.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region código gerado pelo Component Designer

        /// <summary> 
        /// Método necessário para o suporte do Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OGenericAnimationControls));
            this.ControlsPanel = new System.Windows.Forms.TableLayoutPanel();
            this.BtnPlayPause = new System.Windows.Forms.PictureBox();
            this.BtnStop = new System.Windows.Forms.PictureBox();
            this.BtnPrevious = new System.Windows.Forms.PictureBox();
            this.BtnNext = new System.Windows.Forms.PictureBox();
            this.TopControls = new System.Windows.Forms.TableLayoutPanel();
            this.AnimationsList = new Ohana3DS_Rebirth.GUI.OList();
            this.BtnDelete = new Ohana3DS_Rebirth.GUI.OButton();
            this.BtnClear = new Ohana3DS_Rebirth.GUI.OButton();
            this.BtnExport = new Ohana3DS_Rebirth.GUI.OButton();
            this.BtnImport = new Ohana3DS_Rebirth.GUI.OButton();
            this.Seeker = new Ohana3DS_Rebirth.GUI.OSeekBar();
            this.Speed = new Ohana3DS_Rebirth.GUI.OSeekBar();
            this.ControlsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BtnPlayPause)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnStop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnPrevious)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnNext)).BeginInit();
            this.TopControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // ControlsPanel
            // 
            this.ControlsPanel.ColumnCount = 6;
            this.ControlsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.ControlsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.ControlsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.ControlsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.ControlsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.ControlsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.ControlsPanel.Controls.Add(this.BtnPlayPause, 2, 0);
            this.ControlsPanel.Controls.Add(this.BtnStop, 3, 0);
            this.ControlsPanel.Controls.Add(this.BtnPrevious, 1, 0);
            this.ControlsPanel.Controls.Add(this.BtnNext, 4, 0);
            this.ControlsPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ControlsPanel.Location = new System.Drawing.Point(0, 304);
            this.ControlsPanel.Name = "ControlsPanel";
            this.ControlsPanel.RowCount = 1;
            this.ControlsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ControlsPanel.Size = new System.Drawing.Size(346, 26);
            this.ControlsPanel.TabIndex = 6;
            // 
            // BtnPlayPause
            // 
            this.BtnPlayPause.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnPlayPause.Image = ((System.Drawing.Image)(resources.GetObject("BtnPlayPause.Image")));
            this.BtnPlayPause.Location = new System.Drawing.Point(150, 3);
            this.BtnPlayPause.Name = "BtnPlayPause";
            this.BtnPlayPause.Size = new System.Drawing.Size(20, 20);
            this.BtnPlayPause.TabIndex = 0;
            this.BtnPlayPause.TabStop = false;
            this.BtnPlayPause.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnPlayPause_MouseDown);
            this.BtnPlayPause.MouseEnter += new System.EventHandler(this.Control_MouseEnter);
            this.BtnPlayPause.MouseLeave += new System.EventHandler(this.Control_MouseLeave);
            // 
            // BtnStop
            // 
            this.BtnStop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnStop.Image = ((System.Drawing.Image)(resources.GetObject("BtnStop.Image")));
            this.BtnStop.Location = new System.Drawing.Point(176, 3);
            this.BtnStop.Name = "BtnStop";
            this.BtnStop.Size = new System.Drawing.Size(20, 20);
            this.BtnStop.TabIndex = 1;
            this.BtnStop.TabStop = false;
            this.BtnStop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnStop_MouseDown);
            this.BtnStop.MouseEnter += new System.EventHandler(this.Control_MouseEnter);
            this.BtnStop.MouseLeave += new System.EventHandler(this.Control_MouseLeave);
            // 
            // BtnPrevious
            // 
            this.BtnPrevious.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnPrevious.Image = ((System.Drawing.Image)(resources.GetObject("BtnPrevious.Image")));
            this.BtnPrevious.Location = new System.Drawing.Point(124, 3);
            this.BtnPrevious.Name = "BtnPrevious";
            this.BtnPrevious.Size = new System.Drawing.Size(20, 20);
            this.BtnPrevious.TabIndex = 2;
            this.BtnPrevious.TabStop = false;
            this.BtnPrevious.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnPrevious_MouseDown);
            this.BtnPrevious.MouseEnter += new System.EventHandler(this.Control_MouseEnter);
            this.BtnPrevious.MouseLeave += new System.EventHandler(this.Control_MouseLeave);
            // 
            // BtnNext
            // 
            this.BtnNext.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnNext.Image = ((System.Drawing.Image)(resources.GetObject("BtnNext.Image")));
            this.BtnNext.Location = new System.Drawing.Point(202, 3);
            this.BtnNext.Name = "BtnNext";
            this.BtnNext.Size = new System.Drawing.Size(20, 20);
            this.BtnNext.TabIndex = 3;
            this.BtnNext.TabStop = false;
            this.BtnNext.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnNext_MouseDown);
            this.BtnNext.MouseEnter += new System.EventHandler(this.Control_MouseEnter);
            this.BtnNext.MouseLeave += new System.EventHandler(this.Control_MouseLeave);
            // 
            // TopControls
            // 
            this.TopControls.ColumnCount = 4;
            this.TopControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.TopControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.TopControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.TopControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.TopControls.Controls.Add(this.BtnDelete, 0, 0);
            this.TopControls.Controls.Add(this.BtnClear, 0, 0);
            this.TopControls.Controls.Add(this.BtnExport, 0, 0);
            this.TopControls.Controls.Add(this.BtnImport, 0, 0);
            this.TopControls.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopControls.Location = new System.Drawing.Point(0, 0);
            this.TopControls.Name = "TopControls";
            this.TopControls.RowCount = 1;
            this.TopControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TopControls.Size = new System.Drawing.Size(346, 24);
            this.TopControls.TabIndex = 9;
            // 
            // AnimationsList
            // 
            this.AnimationsList.BackColor = System.Drawing.Color.Transparent;
            this.AnimationsList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AnimationsList.HeaderHeight = 24;
            this.AnimationsList.ItemHeight = 16;
            this.AnimationsList.Location = new System.Drawing.Point(0, 24);
            this.AnimationsList.Name = "AnimationsList";
            this.AnimationsList.SelectedIndex = -1;
            this.AnimationsList.Size = new System.Drawing.Size(346, 248);
            this.AnimationsList.TabIndex = 7;
            this.AnimationsList.SelectedIndexChanged += new System.EventHandler(this.AnimationsList_SelectedIndexChanged);
            // 
            // BtnDelete
            // 
            this.BtnDelete.BackColor = System.Drawing.Color.Transparent;
            this.BtnDelete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnDelete.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDelete.Image = ((System.Drawing.Bitmap)(resources.GetObject("BtnDelete.Image")));
            this.BtnDelete.Location = new System.Drawing.Point(172, 0);
            this.BtnDelete.Margin = new System.Windows.Forms.Padding(0);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(86, 24);
            this.BtnDelete.TabIndex = 9;
            this.BtnDelete.Text = "Delete";
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // BtnClear
            // 
            this.BtnClear.BackColor = System.Drawing.Color.Transparent;
            this.BtnClear.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnClear.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnClear.Image = ((System.Drawing.Bitmap)(resources.GetObject("BtnClear.Image")));
            this.BtnClear.Location = new System.Drawing.Point(258, 0);
            this.BtnClear.Margin = new System.Windows.Forms.Padding(0);
            this.BtnClear.Name = "BtnClear";
            this.BtnClear.Size = new System.Drawing.Size(88, 24);
            this.BtnClear.TabIndex = 8;
            this.BtnClear.Text = "Clear";
            this.BtnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // BtnExport
            // 
            this.BtnExport.BackColor = System.Drawing.Color.Transparent;
            this.BtnExport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnExport.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnExport.Image = ((System.Drawing.Bitmap)(resources.GetObject("BtnExport.Image")));
            this.BtnExport.Location = new System.Drawing.Point(0, 0);
            this.BtnExport.Margin = new System.Windows.Forms.Padding(0);
            this.BtnExport.Name = "BtnExport";
            this.BtnExport.Size = new System.Drawing.Size(86, 24);
            this.BtnExport.TabIndex = 7;
            this.BtnExport.Text = "Export";
            this.BtnExport.Click += new System.EventHandler(this.BtnExport_Click);
            // 
            // BtnImport
            // 
            this.BtnImport.BackColor = System.Drawing.Color.Transparent;
            this.BtnImport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnImport.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnImport.Image = ((System.Drawing.Bitmap)(resources.GetObject("BtnImport.Image")));
            this.BtnImport.Location = new System.Drawing.Point(86, 0);
            this.BtnImport.Margin = new System.Windows.Forms.Padding(0);
            this.BtnImport.Name = "BtnImport";
            this.BtnImport.Size = new System.Drawing.Size(86, 24);
            this.BtnImport.TabIndex = 6;
            this.BtnImport.Text = "Import";
            this.BtnImport.Click += new System.EventHandler(this.BtnImport_Click);
            // 
            // Seeker
            // 
            this.Seeker.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Seeker.Location = new System.Drawing.Point(0, 288);
            this.Seeker.MaximumSeek = 100;
            this.Seeker.Name = "Seeker";
            this.Seeker.Size = new System.Drawing.Size(346, 16);
            this.Seeker.TabIndex = 8;
            this.Seeker.Value = 0;
            this.Seeker.Seek += new System.EventHandler(this.Seeker_Seek);
            this.Seeker.SeekStart += new System.EventHandler(this.Seeker_SeekStart);
            this.Seeker.SeekEnd += new System.EventHandler(this.Seeker_SeekEnd);
            // 
            // Speed
            // 
            this.Speed.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Speed.Location = new System.Drawing.Point(0, 272);
            this.Speed.MaximumSeek = 100;
            this.Speed.Name = "Speed";
            this.Speed.Size = new System.Drawing.Size(346, 16);
            this.Speed.TabIndex = 10;
            this.Speed.Value = 100;
            this.Speed.Seek += new System.EventHandler(this.Speed_Seek);
            // 
            // OGenericAnimationControls
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.AnimationsList);
            this.Controls.Add(this.TopControls);
            this.Controls.Add(this.Speed);
            this.Controls.Add(this.Seeker);
            this.Controls.Add(this.ControlsPanel);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "OGenericAnimationControls";
            this.Size = new System.Drawing.Size(346, 330);
            this.ControlsPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BtnPlayPause)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnStop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnPrevious)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnNext)).EndInit();
            this.TopControls.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private OSeekBar Seeker;
        private System.Windows.Forms.TableLayoutPanel ControlsPanel;
        private System.Windows.Forms.PictureBox BtnPlayPause;
        private System.Windows.Forms.PictureBox BtnStop;
        private System.Windows.Forms.PictureBox BtnPrevious;
        private System.Windows.Forms.PictureBox BtnNext;
        protected OList AnimationsList;
        private System.Windows.Forms.TableLayoutPanel TopControls;
        private OButton BtnDelete;
        private OButton BtnClear;
        private OButton BtnExport;
        private OButton BtnImport;
        private OSeekBar Speed;
    }
}
