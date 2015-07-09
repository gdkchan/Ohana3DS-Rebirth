namespace Ohana3DS_Rebirth.GUI
{
    partial class OCameraWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OCameraWindow));
            this.Content = new Ohana3DS_Rebirth.GUI.OScrollablePanel();
            this.ProjectionGroup = new Ohana3DS_Rebirth.GUI.OGroupBox();
            this.ARButtonsPanel = new System.Windows.Forms.Panel();
            this.BtnARBottom = new Ohana3DS_Rebirth.GUI.OButton();
            this.BtnARTop = new Ohana3DS_Rebirth.GUI.OButton();
            this.PPARatio = new Ohana3DS_Rebirth.GUI.OFloatTextBox();
            this.LblARatio = new System.Windows.Forms.Label();
            this.OPHeight = new Ohana3DS_Rebirth.GUI.OFloatTextBox();
            this.LblHeight = new System.Windows.Forms.Label();
            this.PPFovy = new Ohana3DS_Rebirth.GUI.OFloatTextBox();
            this.LblFovy = new System.Windows.Forms.Label();
            this.PZFar = new Ohana3DS_Rebirth.GUI.OFloatTextBox();
            this.LblZFar = new System.Windows.Forms.Label();
            this.PZNear = new Ohana3DS_Rebirth.GUI.OFloatTextBox();
            this.LblZNear = new System.Windows.Forms.Label();
            this.ProjectionTypePanel = new System.Windows.Forms.TableLayoutPanel();
            this.RadioOrtho = new Ohana3DS_Rebirth.GUI.ORadioButton();
            this.RadioPersp = new Ohana3DS_Rebirth.GUI.ORadioButton();
            this.ViewGroup = new Ohana3DS_Rebirth.GUI.OGroupBox();
            this.ATwist = new Ohana3DS_Rebirth.GUI.OFloatTextBox();
            this.LblTwist = new System.Windows.Forms.Label();
            this.LAUpVecZ = new Ohana3DS_Rebirth.GUI.OFloatTextBox();
            this.LAUpVecY = new Ohana3DS_Rebirth.GUI.OFloatTextBox();
            this.LAUpVecX = new Ohana3DS_Rebirth.GUI.OFloatTextBox();
            this.LblUpVector = new System.Windows.Forms.Label();
            this.RRotZ = new Ohana3DS_Rebirth.GUI.OFloatTextBox();
            this.RRotY = new Ohana3DS_Rebirth.GUI.OFloatTextBox();
            this.RRotX = new Ohana3DS_Rebirth.GUI.OFloatTextBox();
            this.LblRotation = new System.Windows.Forms.Label();
            this.TargetZ = new Ohana3DS_Rebirth.GUI.OFloatTextBox();
            this.TargetY = new Ohana3DS_Rebirth.GUI.OFloatTextBox();
            this.TargetX = new Ohana3DS_Rebirth.GUI.OFloatTextBox();
            this.LblTargetPos = new System.Windows.Forms.Label();
            this.ViewTypePanel = new System.Windows.Forms.TableLayoutPanel();
            this.RadioVR = new Ohana3DS_Rebirth.GUI.ORadioButton();
            this.RadioVLAT = new Ohana3DS_Rebirth.GUI.ORadioButton();
            this.RadioVAT = new Ohana3DS_Rebirth.GUI.ORadioButton();
            this.TransformGroup = new Ohana3DS_Rebirth.GUI.OGroupBox();
            this.TTransZ = new Ohana3DS_Rebirth.GUI.OFloatTextBox();
            this.TTransY = new Ohana3DS_Rebirth.GUI.OFloatTextBox();
            this.TTransX = new Ohana3DS_Rebirth.GUI.OFloatTextBox();
            this.NameGroup = new Ohana3DS_Rebirth.GUI.OGroupBox();
            this.TxtCameraName = new Ohana3DS_Rebirth.GUI.OTextBox();
            this.CameraList = new Ohana3DS_Rebirth.GUI.OList();
            this.TopControlsExtended = new System.Windows.Forms.TableLayoutPanel();
            this.BtnDelete = new Ohana3DS_Rebirth.GUI.OButton();
            this.BtnAdd = new Ohana3DS_Rebirth.GUI.OButton();
            this.TopControls = new System.Windows.Forms.TableLayoutPanel();
            this.BtnClear = new Ohana3DS_Rebirth.GUI.OButton();
            this.BtnExport = new Ohana3DS_Rebirth.GUI.OButton();
            this.BtnImport = new Ohana3DS_Rebirth.GUI.OButton();
            this.BtnReset = new Ohana3DS_Rebirth.GUI.OButton();
            this.Content.ContentArea.SuspendLayout();
            this.Content.SuspendLayout();
            this.ProjectionGroup.ContentArea.SuspendLayout();
            this.ProjectionGroup.SuspendLayout();
            this.ARButtonsPanel.SuspendLayout();
            this.ProjectionTypePanel.SuspendLayout();
            this.ViewGroup.ContentArea.SuspendLayout();
            this.ViewGroup.SuspendLayout();
            this.ViewTypePanel.SuspendLayout();
            this.TransformGroup.ContentArea.SuspendLayout();
            this.TransformGroup.SuspendLayout();
            this.NameGroup.ContentArea.SuspendLayout();
            this.NameGroup.SuspendLayout();
            this.TopControlsExtended.SuspendLayout();
            this.TopControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // Content
            // 
            this.Content.BackColor = System.Drawing.Color.Transparent;
            // 
            // Content.ContentArea
            // 
            this.Content.ContentArea.BackColor = System.Drawing.Color.Transparent;
            this.Content.ContentArea.Controls.Add(this.ProjectionGroup);
            this.Content.ContentArea.Controls.Add(this.ViewGroup);
            this.Content.ContentArea.Controls.Add(this.TransformGroup);
            this.Content.ContentArea.Controls.Add(this.NameGroup);
            this.Content.ContentArea.Controls.Add(this.CameraList);
            this.Content.ContentArea.Controls.Add(this.TopControlsExtended);
            this.Content.ContentArea.Controls.Add(this.TopControls);
            this.Content.ContentArea.Location = new System.Drawing.Point(0, 0);
            this.Content.ContentArea.Name = "ContentArea";
            this.Content.ContentArea.Size = new System.Drawing.Size(256, 977);
            this.Content.ContentArea.TabIndex = 2;
            this.Content.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Content.Location = new System.Drawing.Point(0, 16);
            this.Content.Name = "Content";
            this.Content.Size = new System.Drawing.Size(256, 977);
            this.Content.TabIndex = 1;
            // 
            // ProjectionGroup
            // 
            this.ProjectionGroup.BackColor = System.Drawing.Color.Black;
            this.ProjectionGroup.Collapsed = false;
            // 
            // ProjectionGroup.ContentArea
            // 
            this.ProjectionGroup.ContentArea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.ProjectionGroup.ContentArea.Controls.Add(this.ARButtonsPanel);
            this.ProjectionGroup.ContentArea.Controls.Add(this.PPARatio);
            this.ProjectionGroup.ContentArea.Controls.Add(this.LblARatio);
            this.ProjectionGroup.ContentArea.Controls.Add(this.OPHeight);
            this.ProjectionGroup.ContentArea.Controls.Add(this.LblHeight);
            this.ProjectionGroup.ContentArea.Controls.Add(this.PPFovy);
            this.ProjectionGroup.ContentArea.Controls.Add(this.LblFovy);
            this.ProjectionGroup.ContentArea.Controls.Add(this.PZFar);
            this.ProjectionGroup.ContentArea.Controls.Add(this.LblZFar);
            this.ProjectionGroup.ContentArea.Controls.Add(this.PZNear);
            this.ProjectionGroup.ContentArea.Controls.Add(this.LblZNear);
            this.ProjectionGroup.ContentArea.Controls.Add(this.ProjectionTypePanel);
            this.ProjectionGroup.ContentArea.Location = new System.Drawing.Point(1, 16);
            this.ProjectionGroup.ContentArea.Name = "ContentArea";
            this.ProjectionGroup.ContentArea.Size = new System.Drawing.Size(254, 222);
            this.ProjectionGroup.ContentArea.TabIndex = 0;
            this.ProjectionGroup.ContentColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.ProjectionGroup.Dock = System.Windows.Forms.DockStyle.Top;
            this.ProjectionGroup.ExpandedHeight = 239;
            this.ProjectionGroup.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProjectionGroup.Location = new System.Drawing.Point(0, 738);
            this.ProjectionGroup.Name = "ProjectionGroup";
            this.ProjectionGroup.Size = new System.Drawing.Size(256, 239);
            this.ProjectionGroup.TabIndex = 20;
            this.ProjectionGroup.Title = "Projection";
            // 
            // ARButtonsPanel
            // 
            this.ARButtonsPanel.Controls.Add(this.BtnARBottom);
            this.ARButtonsPanel.Controls.Add(this.BtnARTop);
            this.ARButtonsPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ARButtonsPanel.Location = new System.Drawing.Point(0, 198);
            this.ARButtonsPanel.Name = "ARButtonsPanel";
            this.ARButtonsPanel.Size = new System.Drawing.Size(254, 24);
            this.ARButtonsPanel.TabIndex = 27;
            // 
            // BtnARBottom
            // 
            this.BtnARBottom.Image = global::Ohana3DS_Rebirth.Properties.Resources.icn_bottom_scrn;
            this.BtnARBottom.Location = new System.Drawing.Point(80, 0);
            this.BtnARBottom.Margin = new System.Windows.Forms.Padding(0);
            this.BtnARBottom.Name = "BtnARBottom";
            this.BtnARBottom.Size = new System.Drawing.Size(96, 24);
            this.BtnARBottom.TabIndex = 1;
            this.BtnARBottom.Text = "Bottom Scrn";
            this.BtnARBottom.Click += new System.EventHandler(this.BtnARBottom_Click);
            // 
            // BtnARTop
            // 
            this.BtnARTop.Image = global::Ohana3DS_Rebirth.Properties.Resources.icn_top_scrn;
            this.BtnARTop.Location = new System.Drawing.Point(0, 0);
            this.BtnARTop.Margin = new System.Windows.Forms.Padding(0);
            this.BtnARTop.Name = "BtnARTop";
            this.BtnARTop.Size = new System.Drawing.Size(80, 24);
            this.BtnARTop.TabIndex = 0;
            this.BtnARTop.Text = "Top Scrn";
            this.BtnARTop.Click += new System.EventHandler(this.BtnARTop_Click);
            // 
            // PPARatio
            // 
            this.PPARatio.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.PPARatio.DecimalPlaces = ((uint)(6u));
            this.PPARatio.Dock = System.Windows.Forms.DockStyle.Top;
            this.PPARatio.Location = new System.Drawing.Point(0, 177);
            this.PPARatio.Margin = new System.Windows.Forms.Padding(0);
            this.PPARatio.MaximumValue = 2F;
            this.PPARatio.MinimumValue = 1F;
            this.PPARatio.Name = "PPARatio";
            this.PPARatio.Size = new System.Drawing.Size(254, 22);
            this.PPARatio.TabIndex = 22;
            this.PPARatio.Value = 1F;
            this.PPARatio.ValueChanged += new System.EventHandler(this.PPARatio_ValueChanged);
            // 
            // LblARatio
            // 
            this.LblARatio.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblARatio.Location = new System.Drawing.Point(0, 164);
            this.LblARatio.Name = "LblARatio";
            this.LblARatio.Size = new System.Drawing.Size(254, 13);
            this.LblARatio.TabIndex = 23;
            this.LblARatio.Text = "Aspect Ratio:";
            // 
            // OPHeight
            // 
            this.OPHeight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.OPHeight.DecimalPlaces = ((uint)(0u));
            this.OPHeight.Dock = System.Windows.Forms.DockStyle.Top;
            this.OPHeight.Enabled = false;
            this.OPHeight.Location = new System.Drawing.Point(0, 142);
            this.OPHeight.Margin = new System.Windows.Forms.Padding(0);
            this.OPHeight.MaximumValue = 800F;
            this.OPHeight.MinimumValue = 0F;
            this.OPHeight.Name = "OPHeight";
            this.OPHeight.Size = new System.Drawing.Size(254, 22);
            this.OPHeight.TabIndex = 25;
            this.OPHeight.Value = 0F;
            this.OPHeight.ValueChanged += new System.EventHandler(this.OPHeight_ValueChanged);
            // 
            // LblHeight
            // 
            this.LblHeight.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblHeight.Location = new System.Drawing.Point(0, 129);
            this.LblHeight.Name = "LblHeight";
            this.LblHeight.Size = new System.Drawing.Size(254, 13);
            this.LblHeight.TabIndex = 26;
            this.LblHeight.Text = "Height:";
            // 
            // PPFovy
            // 
            this.PPFovy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.PPFovy.DecimalPlaces = ((uint)(6u));
            this.PPFovy.Dock = System.Windows.Forms.DockStyle.Top;
            this.PPFovy.Location = new System.Drawing.Point(0, 107);
            this.PPFovy.Margin = new System.Windows.Forms.Padding(0);
            this.PPFovy.MaximumValue = 3.141593F;
            this.PPFovy.MinimumValue = 0F;
            this.PPFovy.Name = "PPFovy";
            this.PPFovy.Size = new System.Drawing.Size(254, 22);
            this.PPFovy.TabIndex = 20;
            this.PPFovy.Value = 0F;
            this.PPFovy.ValueChanged += new System.EventHandler(this.PPFovy_ValueChanged);
            // 
            // LblFovy
            // 
            this.LblFovy.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblFovy.Location = new System.Drawing.Point(0, 94);
            this.LblFovy.Name = "LblFovy";
            this.LblFovy.Size = new System.Drawing.Size(254, 13);
            this.LblFovy.TabIndex = 21;
            this.LblFovy.Text = "Field of View Y:";
            // 
            // PZFar
            // 
            this.PZFar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.PZFar.DecimalPlaces = ((uint)(0u));
            this.PZFar.Dock = System.Windows.Forms.DockStyle.Top;
            this.PZFar.Location = new System.Drawing.Point(0, 72);
            this.PZFar.Margin = new System.Windows.Forms.Padding(0);
            this.PZFar.MaximumValue = 10000F;
            this.PZFar.MinimumValue = 1F;
            this.PZFar.Name = "PZFar";
            this.PZFar.Size = new System.Drawing.Size(254, 22);
            this.PZFar.TabIndex = 18;
            this.PZFar.Value = 1F;
            this.PZFar.ValueChanged += new System.EventHandler(this.PZFar_ValueChanged);
            // 
            // LblZFar
            // 
            this.LblZFar.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblZFar.Location = new System.Drawing.Point(0, 59);
            this.LblZFar.Name = "LblZFar";
            this.LblZFar.Size = new System.Drawing.Size(254, 13);
            this.LblZFar.TabIndex = 19;
            this.LblZFar.Text = "Z Far:";
            // 
            // PZNear
            // 
            this.PZNear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.PZNear.DecimalPlaces = ((uint)(2u));
            this.PZNear.Dock = System.Windows.Forms.DockStyle.Top;
            this.PZNear.Location = new System.Drawing.Point(0, 37);
            this.PZNear.Margin = new System.Windows.Forms.Padding(0);
            this.PZNear.MaximumValue = 1F;
            this.PZNear.MinimumValue = 0.01F;
            this.PZNear.Name = "PZNear";
            this.PZNear.Size = new System.Drawing.Size(254, 22);
            this.PZNear.TabIndex = 16;
            this.PZNear.Value = 0.01F;
            this.PZNear.ValueChanged += new System.EventHandler(this.PZNear_ValueChanged);
            // 
            // LblZNear
            // 
            this.LblZNear.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblZNear.Location = new System.Drawing.Point(0, 24);
            this.LblZNear.Name = "LblZNear";
            this.LblZNear.Size = new System.Drawing.Size(254, 13);
            this.LblZNear.TabIndex = 17;
            this.LblZNear.Text = "Z Near:";
            // 
            // ProjectionTypePanel
            // 
            this.ProjectionTypePanel.ColumnCount = 2;
            this.ProjectionTypePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.ProjectionTypePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.ProjectionTypePanel.Controls.Add(this.RadioOrtho, 0, 0);
            this.ProjectionTypePanel.Controls.Add(this.RadioPersp, 0, 0);
            this.ProjectionTypePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.ProjectionTypePanel.Location = new System.Drawing.Point(0, 0);
            this.ProjectionTypePanel.Name = "ProjectionTypePanel";
            this.ProjectionTypePanel.RowCount = 1;
            this.ProjectionTypePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.ProjectionTypePanel.Size = new System.Drawing.Size(254, 24);
            this.ProjectionTypePanel.TabIndex = 24;
            // 
            // RadioOrtho
            // 
            this.RadioOrtho.AutoSize = true;
            this.RadioOrtho.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.RadioOrtho.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RadioOrtho.ForeColor = System.Drawing.Color.White;
            this.RadioOrtho.Location = new System.Drawing.Point(128, 1);
            this.RadioOrtho.Margin = new System.Windows.Forms.Padding(1);
            this.RadioOrtho.Name = "RadioOrtho";
            this.RadioOrtho.Size = new System.Drawing.Size(125, 22);
            this.RadioOrtho.TabIndex = 5;
            this.RadioOrtho.Text = "Orthogonal";
            this.RadioOrtho.UseVisualStyleBackColor = false;
            this.RadioOrtho.CheckedChanged += new System.EventHandler(this.RadioOrtho_CheckedChanged);
            // 
            // RadioPersp
            // 
            this.RadioPersp.AutoSize = true;
            this.RadioPersp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.RadioPersp.Checked = true;
            this.RadioPersp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RadioPersp.ForeColor = System.Drawing.Color.White;
            this.RadioPersp.Location = new System.Drawing.Point(1, 1);
            this.RadioPersp.Margin = new System.Windows.Forms.Padding(1);
            this.RadioPersp.Name = "RadioPersp";
            this.RadioPersp.Size = new System.Drawing.Size(125, 22);
            this.RadioPersp.TabIndex = 4;
            this.RadioPersp.TabStop = true;
            this.RadioPersp.Text = "Perspective";
            this.RadioPersp.UseVisualStyleBackColor = false;
            this.RadioPersp.CheckedChanged += new System.EventHandler(this.RadioPersp_CheckedChanged);
            // 
            // ViewGroup
            // 
            this.ViewGroup.BackColor = System.Drawing.Color.Black;
            this.ViewGroup.Collapsed = false;
            // 
            // ViewGroup.ContentArea
            // 
            this.ViewGroup.ContentArea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.ViewGroup.ContentArea.Controls.Add(this.ATwist);
            this.ViewGroup.ContentArea.Controls.Add(this.LblTwist);
            this.ViewGroup.ContentArea.Controls.Add(this.LAUpVecZ);
            this.ViewGroup.ContentArea.Controls.Add(this.LAUpVecY);
            this.ViewGroup.ContentArea.Controls.Add(this.LAUpVecX);
            this.ViewGroup.ContentArea.Controls.Add(this.LblUpVector);
            this.ViewGroup.ContentArea.Controls.Add(this.RRotZ);
            this.ViewGroup.ContentArea.Controls.Add(this.RRotY);
            this.ViewGroup.ContentArea.Controls.Add(this.RRotX);
            this.ViewGroup.ContentArea.Controls.Add(this.LblRotation);
            this.ViewGroup.ContentArea.Controls.Add(this.TargetZ);
            this.ViewGroup.ContentArea.Controls.Add(this.TargetY);
            this.ViewGroup.ContentArea.Controls.Add(this.TargetX);
            this.ViewGroup.ContentArea.Controls.Add(this.LblTargetPos);
            this.ViewGroup.ContentArea.Controls.Add(this.ViewTypePanel);
            this.ViewGroup.ContentArea.Location = new System.Drawing.Point(1, 16);
            this.ViewGroup.ContentArea.Name = "ContentArea";
            this.ViewGroup.ContentArea.Size = new System.Drawing.Size(254, 295);
            this.ViewGroup.ContentArea.TabIndex = 0;
            this.ViewGroup.ContentColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.ViewGroup.Dock = System.Windows.Forms.DockStyle.Top;
            this.ViewGroup.ExpandedHeight = 312;
            this.ViewGroup.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ViewGroup.Location = new System.Drawing.Point(0, 426);
            this.ViewGroup.Name = "ViewGroup";
            this.ViewGroup.Size = new System.Drawing.Size(256, 312);
            this.ViewGroup.TabIndex = 10;
            this.ViewGroup.Title = "View";
            // 
            // ATwist
            // 
            this.ATwist.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.ATwist.DecimalPlaces = ((uint)(6u));
            this.ATwist.Dock = System.Windows.Forms.DockStyle.Top;
            this.ATwist.Location = new System.Drawing.Point(0, 274);
            this.ATwist.Margin = new System.Windows.Forms.Padding(0);
            this.ATwist.MaximumValue = 3.141593F;
            this.ATwist.MinimumValue = -3.141593F;
            this.ATwist.Name = "ATwist";
            this.ATwist.Size = new System.Drawing.Size(254, 22);
            this.ATwist.TabIndex = 31;
            this.ATwist.Value = 0F;
            this.ATwist.ValueChanged += new System.EventHandler(this.ATwist_ValueChanged);
            // 
            // LblTwist
            // 
            this.LblTwist.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblTwist.Location = new System.Drawing.Point(0, 261);
            this.LblTwist.Name = "LblTwist";
            this.LblTwist.Size = new System.Drawing.Size(254, 13);
            this.LblTwist.TabIndex = 30;
            this.LblTwist.Text = "Twist:";
            // 
            // LAUpVecZ
            // 
            this.LAUpVecZ.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.LAUpVecZ.DecimalPlaces = ((uint)(1u));
            this.LAUpVecZ.Dock = System.Windows.Forms.DockStyle.Top;
            this.LAUpVecZ.Enabled = false;
            this.LAUpVecZ.Location = new System.Drawing.Point(0, 239);
            this.LAUpVecZ.Margin = new System.Windows.Forms.Padding(0);
            this.LAUpVecZ.MaximumValue = 1F;
            this.LAUpVecZ.MinimumValue = -1F;
            this.LAUpVecZ.Name = "LAUpVecZ";
            this.LAUpVecZ.Size = new System.Drawing.Size(254, 22);
            this.LAUpVecZ.TabIndex = 7;
            this.LAUpVecZ.Value = 0F;
            this.LAUpVecZ.ValueChanged += new System.EventHandler(this.LAUpVecZ_ValueChanged);
            // 
            // LAUpVecY
            // 
            this.LAUpVecY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.LAUpVecY.DecimalPlaces = ((uint)(1u));
            this.LAUpVecY.Dock = System.Windows.Forms.DockStyle.Top;
            this.LAUpVecY.Enabled = false;
            this.LAUpVecY.Location = new System.Drawing.Point(0, 217);
            this.LAUpVecY.Margin = new System.Windows.Forms.Padding(0);
            this.LAUpVecY.MaximumValue = 1F;
            this.LAUpVecY.MinimumValue = -1F;
            this.LAUpVecY.Name = "LAUpVecY";
            this.LAUpVecY.Size = new System.Drawing.Size(254, 22);
            this.LAUpVecY.TabIndex = 6;
            this.LAUpVecY.Value = 0F;
            this.LAUpVecY.ValueChanged += new System.EventHandler(this.LAUpVecY_ValueChanged);
            // 
            // LAUpVecX
            // 
            this.LAUpVecX.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.LAUpVecX.DecimalPlaces = ((uint)(1u));
            this.LAUpVecX.Dock = System.Windows.Forms.DockStyle.Top;
            this.LAUpVecX.Enabled = false;
            this.LAUpVecX.Location = new System.Drawing.Point(0, 195);
            this.LAUpVecX.Margin = new System.Windows.Forms.Padding(0);
            this.LAUpVecX.MaximumValue = 1F;
            this.LAUpVecX.MinimumValue = -1F;
            this.LAUpVecX.Name = "LAUpVecX";
            this.LAUpVecX.Size = new System.Drawing.Size(254, 22);
            this.LAUpVecX.TabIndex = 5;
            this.LAUpVecX.Value = 0F;
            this.LAUpVecX.ValueChanged += new System.EventHandler(this.LAUpVecX_ValueChanged);
            // 
            // LblUpVector
            // 
            this.LblUpVector.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblUpVector.Location = new System.Drawing.Point(0, 182);
            this.LblUpVector.Name = "LblUpVector";
            this.LblUpVector.Size = new System.Drawing.Size(254, 13);
            this.LblUpVector.TabIndex = 4;
            this.LblUpVector.Text = "Upward Vector (X/Y/Z):";
            // 
            // RRotZ
            // 
            this.RRotZ.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.RRotZ.DecimalPlaces = ((uint)(6u));
            this.RRotZ.Dock = System.Windows.Forms.DockStyle.Top;
            this.RRotZ.Enabled = false;
            this.RRotZ.Location = new System.Drawing.Point(0, 160);
            this.RRotZ.Margin = new System.Windows.Forms.Padding(0);
            this.RRotZ.MaximumValue = 3.141593F;
            this.RRotZ.MinimumValue = -3.141593F;
            this.RRotZ.Name = "RRotZ";
            this.RRotZ.Size = new System.Drawing.Size(254, 22);
            this.RRotZ.TabIndex = 29;
            this.RRotZ.Value = 0F;
            this.RRotZ.ValueChanged += new System.EventHandler(this.RRotZ_ValueChanged);
            // 
            // RRotY
            // 
            this.RRotY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.RRotY.DecimalPlaces = ((uint)(6u));
            this.RRotY.Dock = System.Windows.Forms.DockStyle.Top;
            this.RRotY.Enabled = false;
            this.RRotY.Location = new System.Drawing.Point(0, 138);
            this.RRotY.Margin = new System.Windows.Forms.Padding(0);
            this.RRotY.MaximumValue = 3.141593F;
            this.RRotY.MinimumValue = -3.141593F;
            this.RRotY.Name = "RRotY";
            this.RRotY.Size = new System.Drawing.Size(254, 22);
            this.RRotY.TabIndex = 28;
            this.RRotY.Value = 0F;
            this.RRotY.ValueChanged += new System.EventHandler(this.RRotY_ValueChanged);
            // 
            // RRotX
            // 
            this.RRotX.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.RRotX.DecimalPlaces = ((uint)(6u));
            this.RRotX.Dock = System.Windows.Forms.DockStyle.Top;
            this.RRotX.Enabled = false;
            this.RRotX.Location = new System.Drawing.Point(0, 116);
            this.RRotX.Margin = new System.Windows.Forms.Padding(0);
            this.RRotX.MaximumValue = 3.141593F;
            this.RRotX.MinimumValue = -3.141593F;
            this.RRotX.Name = "RRotX";
            this.RRotX.Size = new System.Drawing.Size(254, 22);
            this.RRotX.TabIndex = 26;
            this.RRotX.Value = 0F;
            this.RRotX.ValueChanged += new System.EventHandler(this.RRotX_ValueChanged);
            // 
            // LblRotation
            // 
            this.LblRotation.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblRotation.Location = new System.Drawing.Point(0, 103);
            this.LblRotation.Name = "LblRotation";
            this.LblRotation.Size = new System.Drawing.Size(254, 13);
            this.LblRotation.TabIndex = 27;
            this.LblRotation.Text = "Rotation (X/Y/Z):";
            // 
            // TargetZ
            // 
            this.TargetZ.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.TargetZ.DecimalPlaces = ((uint)(1u));
            this.TargetZ.Dock = System.Windows.Forms.DockStyle.Top;
            this.TargetZ.Location = new System.Drawing.Point(0, 81);
            this.TargetZ.Margin = new System.Windows.Forms.Padding(0);
            this.TargetZ.MaximumValue = 100F;
            this.TargetZ.MinimumValue = -100F;
            this.TargetZ.Name = "TargetZ";
            this.TargetZ.Size = new System.Drawing.Size(254, 22);
            this.TargetZ.TabIndex = 3;
            this.TargetZ.Value = 0F;
            this.TargetZ.ValueChanged += new System.EventHandler(this.TargetZ_ValueChanged);
            // 
            // TargetY
            // 
            this.TargetY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.TargetY.DecimalPlaces = ((uint)(1u));
            this.TargetY.Dock = System.Windows.Forms.DockStyle.Top;
            this.TargetY.Location = new System.Drawing.Point(0, 59);
            this.TargetY.Margin = new System.Windows.Forms.Padding(0);
            this.TargetY.MaximumValue = 100F;
            this.TargetY.MinimumValue = -100F;
            this.TargetY.Name = "TargetY";
            this.TargetY.Size = new System.Drawing.Size(254, 22);
            this.TargetY.TabIndex = 2;
            this.TargetY.Value = 0F;
            this.TargetY.ValueChanged += new System.EventHandler(this.TargetY_ValueChanged);
            // 
            // TargetX
            // 
            this.TargetX.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.TargetX.DecimalPlaces = ((uint)(1u));
            this.TargetX.Dock = System.Windows.Forms.DockStyle.Top;
            this.TargetX.Location = new System.Drawing.Point(0, 37);
            this.TargetX.Margin = new System.Windows.Forms.Padding(0);
            this.TargetX.MaximumValue = 100F;
            this.TargetX.MinimumValue = -100F;
            this.TargetX.Name = "TargetX";
            this.TargetX.Size = new System.Drawing.Size(254, 22);
            this.TargetX.TabIndex = 0;
            this.TargetX.Value = 0F;
            this.TargetX.ValueChanged += new System.EventHandler(this.TargetX_ValueChanged);
            // 
            // LblTargetPos
            // 
            this.LblTargetPos.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblTargetPos.Location = new System.Drawing.Point(0, 24);
            this.LblTargetPos.Name = "LblTargetPos";
            this.LblTargetPos.Size = new System.Drawing.Size(254, 13);
            this.LblTargetPos.TabIndex = 1;
            this.LblTargetPos.Text = "Target Position (X/Y/Z):";
            // 
            // ViewTypePanel
            // 
            this.ViewTypePanel.ColumnCount = 3;
            this.ViewTypePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.ViewTypePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.ViewTypePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.ViewTypePanel.Controls.Add(this.RadioVR, 0, 0);
            this.ViewTypePanel.Controls.Add(this.RadioVLAT, 0, 0);
            this.ViewTypePanel.Controls.Add(this.RadioVAT, 0, 0);
            this.ViewTypePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.ViewTypePanel.Location = new System.Drawing.Point(0, 0);
            this.ViewTypePanel.Name = "ViewTypePanel";
            this.ViewTypePanel.RowCount = 1;
            this.ViewTypePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ViewTypePanel.Size = new System.Drawing.Size(254, 24);
            this.ViewTypePanel.TabIndex = 25;
            // 
            // RadioVR
            // 
            this.RadioVR.AutoSize = true;
            this.RadioVR.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.RadioVR.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RadioVR.ForeColor = System.Drawing.Color.White;
            this.RadioVR.Location = new System.Drawing.Point(169, 1);
            this.RadioVR.Margin = new System.Windows.Forms.Padding(1);
            this.RadioVR.Name = "RadioVR";
            this.RadioVR.Size = new System.Drawing.Size(84, 22);
            this.RadioVR.TabIndex = 6;
            this.RadioVR.Text = "Rotate";
            this.RadioVR.UseVisualStyleBackColor = false;
            this.RadioVR.CheckedChanged += new System.EventHandler(this.RadioVR_CheckedChanged);
            // 
            // RadioVLAT
            // 
            this.RadioVLAT.AutoSize = true;
            this.RadioVLAT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.RadioVLAT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RadioVLAT.ForeColor = System.Drawing.Color.White;
            this.RadioVLAT.Location = new System.Drawing.Point(85, 1);
            this.RadioVLAT.Margin = new System.Windows.Forms.Padding(1);
            this.RadioVLAT.Name = "RadioVLAT";
            this.RadioVLAT.Size = new System.Drawing.Size(82, 22);
            this.RadioVLAT.TabIndex = 5;
            this.RadioVLAT.Text = "LookAt";
            this.RadioVLAT.UseVisualStyleBackColor = false;
            this.RadioVLAT.CheckedChanged += new System.EventHandler(this.RadioVLAT_CheckedChanged);
            // 
            // RadioVAT
            // 
            this.RadioVAT.AutoSize = true;
            this.RadioVAT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.RadioVAT.Checked = true;
            this.RadioVAT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RadioVAT.ForeColor = System.Drawing.Color.White;
            this.RadioVAT.Location = new System.Drawing.Point(1, 1);
            this.RadioVAT.Margin = new System.Windows.Forms.Padding(1);
            this.RadioVAT.Name = "RadioVAT";
            this.RadioVAT.Size = new System.Drawing.Size(82, 22);
            this.RadioVAT.TabIndex = 4;
            this.RadioVAT.TabStop = true;
            this.RadioVAT.Text = "Aim";
            this.RadioVAT.UseVisualStyleBackColor = false;
            this.RadioVAT.CheckedChanged += new System.EventHandler(this.RadioVAT_CheckedChanged);
            // 
            // TransformGroup
            // 
            this.TransformGroup.BackColor = System.Drawing.Color.Black;
            this.TransformGroup.Collapsed = false;
            // 
            // TransformGroup.ContentArea
            // 
            this.TransformGroup.ContentArea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.TransformGroup.ContentArea.Controls.Add(this.TTransZ);
            this.TransformGroup.ContentArea.Controls.Add(this.TTransY);
            this.TransformGroup.ContentArea.Controls.Add(this.TTransX);
            this.TransformGroup.ContentArea.Location = new System.Drawing.Point(1, 16);
            this.TransformGroup.ContentArea.Name = "ContentArea";
            this.TransformGroup.ContentArea.Size = new System.Drawing.Size(254, 65);
            this.TransformGroup.ContentArea.TabIndex = 0;
            this.TransformGroup.ContentColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.TransformGroup.Dock = System.Windows.Forms.DockStyle.Top;
            this.TransformGroup.ExpandedHeight = 82;
            this.TransformGroup.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TransformGroup.Location = new System.Drawing.Point(0, 344);
            this.TransformGroup.Name = "TransformGroup";
            this.TransformGroup.Size = new System.Drawing.Size(256, 82);
            this.TransformGroup.TabIndex = 9;
            this.TransformGroup.Title = "Position";
            // 
            // TTransZ
            // 
            this.TTransZ.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.TTransZ.DecimalPlaces = ((uint)(1u));
            this.TTransZ.Dock = System.Windows.Forms.DockStyle.Top;
            this.TTransZ.Location = new System.Drawing.Point(0, 44);
            this.TTransZ.Margin = new System.Windows.Forms.Padding(0);
            this.TTransZ.MaximumValue = 100F;
            this.TTransZ.MinimumValue = -100F;
            this.TTransZ.Name = "TTransZ";
            this.TTransZ.Size = new System.Drawing.Size(254, 22);
            this.TTransZ.TabIndex = 3;
            this.TTransZ.Value = 0F;
            this.TTransZ.ValueChanged += new System.EventHandler(this.TTransZ_ValueChanged);
            // 
            // TTransY
            // 
            this.TTransY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.TTransY.DecimalPlaces = ((uint)(1u));
            this.TTransY.Dock = System.Windows.Forms.DockStyle.Top;
            this.TTransY.Location = new System.Drawing.Point(0, 22);
            this.TTransY.Margin = new System.Windows.Forms.Padding(0);
            this.TTransY.MaximumValue = 100F;
            this.TTransY.MinimumValue = -100F;
            this.TTransY.Name = "TTransY";
            this.TTransY.Size = new System.Drawing.Size(254, 22);
            this.TTransY.TabIndex = 2;
            this.TTransY.Value = 0F;
            this.TTransY.ValueChanged += new System.EventHandler(this.TTransY_ValueChanged);
            // 
            // TTransX
            // 
            this.TTransX.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.TTransX.DecimalPlaces = ((uint)(1u));
            this.TTransX.Dock = System.Windows.Forms.DockStyle.Top;
            this.TTransX.Location = new System.Drawing.Point(0, 0);
            this.TTransX.Margin = new System.Windows.Forms.Padding(0);
            this.TTransX.MaximumValue = 100F;
            this.TTransX.MinimumValue = -100F;
            this.TTransX.Name = "TTransX";
            this.TTransX.Size = new System.Drawing.Size(254, 22);
            this.TTransX.TabIndex = 0;
            this.TTransX.Value = 0F;
            this.TTransX.ValueChanged += new System.EventHandler(this.TTransX_ValueChanged);
            // 
            // NameGroup
            // 
            this.NameGroup.BackColor = System.Drawing.Color.Black;
            this.NameGroup.Collapsed = false;
            // 
            // NameGroup.ContentArea
            // 
            this.NameGroup.ContentArea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.NameGroup.ContentArea.Controls.Add(this.TxtCameraName);
            this.NameGroup.ContentArea.Location = new System.Drawing.Point(1, 16);
            this.NameGroup.ContentArea.Name = "ContentArea";
            this.NameGroup.ContentArea.Size = new System.Drawing.Size(254, 23);
            this.NameGroup.ContentArea.TabIndex = 0;
            this.NameGroup.ContentColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.NameGroup.Dock = System.Windows.Forms.DockStyle.Top;
            this.NameGroup.ExpandedHeight = 40;
            this.NameGroup.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NameGroup.Location = new System.Drawing.Point(0, 304);
            this.NameGroup.Name = "NameGroup";
            this.NameGroup.Size = new System.Drawing.Size(256, 40);
            this.NameGroup.TabIndex = 10;
            this.NameGroup.Title = "Name";
            // 
            // TxtCameraName
            // 
            this.TxtCameraName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.TxtCameraName.CharacterWhiteList = null;
            this.TxtCameraName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtCameraName.Location = new System.Drawing.Point(0, 0);
            this.TxtCameraName.Name = "TxtCameraName";
            this.TxtCameraName.Size = new System.Drawing.Size(254, 23);
            this.TxtCameraName.TabIndex = 0;
            this.TxtCameraName.ChangedText += new System.EventHandler(this.TxtCameraName_ChangedText);
            // 
            // CameraList
            // 
            this.CameraList.BackColor = System.Drawing.Color.Transparent;
            this.CameraList.Dock = System.Windows.Forms.DockStyle.Top;
            this.CameraList.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CameraList.HeaderHeight = 24;
            this.CameraList.ItemHeight = 16;
            this.CameraList.Location = new System.Drawing.Point(0, 48);
            this.CameraList.Name = "CameraList";
            this.CameraList.SelectedIndex = -1;
            this.CameraList.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(15)))), ((int)(((byte)(82)))), ((int)(((byte)(186)))));
            this.CameraList.Size = new System.Drawing.Size(256, 256);
            this.CameraList.TabIndex = 8;
            this.CameraList.SelectedIndexChanged += new System.EventHandler(this.CameraList_SelectedIndexChanged);
            // 
            // TopControlsExtended
            // 
            this.TopControlsExtended.ColumnCount = 3;
            this.TopControlsExtended.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.TopControlsExtended.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.TopControlsExtended.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.TopControlsExtended.Controls.Add(this.BtnReset, 0, 0);
            this.TopControlsExtended.Controls.Add(this.BtnDelete, 0, 0);
            this.TopControlsExtended.Controls.Add(this.BtnAdd, 0, 0);
            this.TopControlsExtended.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopControlsExtended.Location = new System.Drawing.Point(0, 24);
            this.TopControlsExtended.Name = "TopControlsExtended";
            this.TopControlsExtended.RowCount = 1;
            this.TopControlsExtended.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TopControlsExtended.Size = new System.Drawing.Size(256, 24);
            this.TopControlsExtended.TabIndex = 21;
            // 
            // BtnDelete
            // 
            this.BtnDelete.BackColor = System.Drawing.Color.Transparent;
            this.BtnDelete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnDelete.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDelete.Image = ((System.Drawing.Bitmap)(resources.GetObject("BtnDelete.Image")));
            this.BtnDelete.Location = new System.Drawing.Point(85, 0);
            this.BtnDelete.Margin = new System.Windows.Forms.Padding(0);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(85, 24);
            this.BtnDelete.TabIndex = 13;
            this.BtnDelete.Text = "Delete";
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // BtnAdd
            // 
            this.BtnAdd.BackColor = System.Drawing.Color.Transparent;
            this.BtnAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnAdd.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAdd.Image = global::Ohana3DS_Rebirth.Properties.Resources.icn_expand;
            this.BtnAdd.Location = new System.Drawing.Point(0, 0);
            this.BtnAdd.Margin = new System.Windows.Forms.Padding(0);
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.Size = new System.Drawing.Size(85, 24);
            this.BtnAdd.TabIndex = 12;
            this.BtnAdd.Text = "Add";
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // TopControls
            // 
            this.TopControls.ColumnCount = 3;
            this.TopControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.TopControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.TopControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.TopControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.TopControls.Controls.Add(this.BtnClear, 0, 0);
            this.TopControls.Controls.Add(this.BtnExport, 0, 0);
            this.TopControls.Controls.Add(this.BtnImport, 0, 0);
            this.TopControls.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopControls.Location = new System.Drawing.Point(0, 0);
            this.TopControls.Name = "TopControls";
            this.TopControls.RowCount = 1;
            this.TopControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TopControls.Size = new System.Drawing.Size(256, 24);
            this.TopControls.TabIndex = 11;
            // 
            // BtnClear
            // 
            this.BtnClear.BackColor = System.Drawing.Color.Transparent;
            this.BtnClear.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnClear.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnClear.Image = ((System.Drawing.Bitmap)(resources.GetObject("BtnClear.Image")));
            this.BtnClear.Location = new System.Drawing.Point(170, 0);
            this.BtnClear.Margin = new System.Windows.Forms.Padding(0);
            this.BtnClear.Name = "BtnClear";
            this.BtnClear.Size = new System.Drawing.Size(86, 24);
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
            this.BtnExport.Location = new System.Drawing.Point(85, 0);
            this.BtnExport.Margin = new System.Windows.Forms.Padding(0);
            this.BtnExport.Name = "BtnExport";
            this.BtnExport.Size = new System.Drawing.Size(85, 24);
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
            this.BtnImport.Location = new System.Drawing.Point(0, 0);
            this.BtnImport.Margin = new System.Windows.Forms.Padding(0);
            this.BtnImport.Name = "BtnImport";
            this.BtnImport.Size = new System.Drawing.Size(85, 24);
            this.BtnImport.TabIndex = 6;
            this.BtnImport.Text = "Import";
            this.BtnImport.Click += new System.EventHandler(this.BtnImport_Click);
            // 
            // BtnReset
            // 
            this.BtnReset.BackColor = System.Drawing.Color.Transparent;
            this.BtnReset.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnReset.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnReset.Image = global::Ohana3DS_Rebirth.Properties.Resources.icn_camera;
            this.BtnReset.Location = new System.Drawing.Point(170, 0);
            this.BtnReset.Margin = new System.Windows.Forms.Padding(0);
            this.BtnReset.Name = "BtnReset";
            this.BtnReset.Size = new System.Drawing.Size(86, 24);
            this.BtnReset.TabIndex = 14;
            this.BtnReset.Text = "Reset";
            this.BtnReset.Click += new System.EventHandler(this.BtnReset_Click);
            // 
            // OCameraWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.Content);
            this.Name = "OCameraWindow";
            this.Size = new System.Drawing.Size(256, 993);
            this.Controls.SetChildIndex(this.Content, 0);
            this.Content.ContentArea.ResumeLayout(false);
            this.Content.ResumeLayout(false);
            this.ProjectionGroup.ContentArea.ResumeLayout(false);
            this.ProjectionGroup.ResumeLayout(false);
            this.ProjectionGroup.PerformLayout();
            this.ARButtonsPanel.ResumeLayout(false);
            this.ProjectionTypePanel.ResumeLayout(false);
            this.ProjectionTypePanel.PerformLayout();
            this.ViewGroup.ContentArea.ResumeLayout(false);
            this.ViewGroup.ResumeLayout(false);
            this.ViewGroup.PerformLayout();
            this.ViewTypePanel.ResumeLayout(false);
            this.ViewTypePanel.PerformLayout();
            this.TransformGroup.ContentArea.ResumeLayout(false);
            this.TransformGroup.ResumeLayout(false);
            this.TransformGroup.PerformLayout();
            this.NameGroup.ContentArea.ResumeLayout(false);
            this.NameGroup.ResumeLayout(false);
            this.NameGroup.PerformLayout();
            this.TopControlsExtended.ResumeLayout(false);
            this.TopControls.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private OScrollablePanel Content;
        private OGroupBox TransformGroup;
        private OList CameraList;
        private OFloatTextBox TTransX;
        private OFloatTextBox TTransZ;
        private OFloatTextBox TTransY;
        private OGroupBox ViewGroup;
        private OFloatTextBox LAUpVecZ;
        private OFloatTextBox LAUpVecY;
        private OFloatTextBox LAUpVecX;
        private System.Windows.Forms.Label LblUpVector;
        private OFloatTextBox TargetZ;
        private OFloatTextBox TargetY;
        private OFloatTextBox TargetX;
        private System.Windows.Forms.Label LblTargetPos;
        private System.Windows.Forms.TableLayoutPanel TopControls;
        private OButton BtnClear;
        private OButton BtnExport;
        private OButton BtnImport;
        private OGroupBox ProjectionGroup;
        private OFloatTextBox PPARatio;
        private System.Windows.Forms.Label LblARatio;
        private OFloatTextBox PPFovy;
        private System.Windows.Forms.Label LblFovy;
        private OFloatTextBox PZFar;
        private System.Windows.Forms.Label LblZFar;
        private OFloatTextBox PZNear;
        private System.Windows.Forms.Label LblZNear;
        private System.Windows.Forms.TableLayoutPanel ProjectionTypePanel;
        private ORadioButton RadioOrtho;
        private ORadioButton RadioPersp;
        private OFloatTextBox OPHeight;
        private System.Windows.Forms.Label LblHeight;
        private OFloatTextBox ATwist;
        private System.Windows.Forms.Label LblTwist;
        private OFloatTextBox RRotZ;
        private OFloatTextBox RRotY;
        private OFloatTextBox RRotX;
        private System.Windows.Forms.Label LblRotation;
        private System.Windows.Forms.TableLayoutPanel ViewTypePanel;
        private ORadioButton RadioVR;
        private ORadioButton RadioVLAT;
        private ORadioButton RadioVAT;
        private OGroupBox NameGroup;
        private OTextBox TxtCameraName;
        private System.Windows.Forms.TableLayoutPanel TopControlsExtended;
        private OButton BtnDelete;
        private OButton BtnAdd;
        private System.Windows.Forms.Panel ARButtonsPanel;
        private OButton BtnARBottom;
        private OButton BtnARTop;
        private OButton BtnReset;


    }
}
