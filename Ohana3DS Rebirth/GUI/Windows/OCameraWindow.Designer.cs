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
            this.TScaleZ = new Ohana3DS_Rebirth.GUI.OFloatTextBox();
            this.TScaleY = new Ohana3DS_Rebirth.GUI.OFloatTextBox();
            this.TScaleX = new Ohana3DS_Rebirth.GUI.OFloatTextBox();
            this.LblTransformScale = new System.Windows.Forms.Label();
            this.TRotZ = new Ohana3DS_Rebirth.GUI.OFloatTextBox();
            this.TRotY = new Ohana3DS_Rebirth.GUI.OFloatTextBox();
            this.TRotX = new Ohana3DS_Rebirth.GUI.OFloatTextBox();
            this.LblTransformRotation = new System.Windows.Forms.Label();
            this.TTransZ = new Ohana3DS_Rebirth.GUI.OFloatTextBox();
            this.TTransY = new Ohana3DS_Rebirth.GUI.OFloatTextBox();
            this.TTransX = new Ohana3DS_Rebirth.GUI.OFloatTextBox();
            this.LblTransformTranslation = new System.Windows.Forms.Label();
            this.NameGroup = new Ohana3DS_Rebirth.GUI.OGroupBox();
            this.TxtCameraName = new Ohana3DS_Rebirth.GUI.OTextBox();
            this.CameraList = new Ohana3DS_Rebirth.GUI.OList();
            this.TopControls = new System.Windows.Forms.TableLayoutPanel();
            this.BtnDelete = new Ohana3DS_Rebirth.GUI.OButton();
            this.BtnClear = new Ohana3DS_Rebirth.GUI.OButton();
            this.BtnExport = new Ohana3DS_Rebirth.GUI.OButton();
            this.BtnImport = new Ohana3DS_Rebirth.GUI.OButton();
            this.Content.ContentArea.SuspendLayout();
            this.Content.SuspendLayout();
            this.ProjectionGroup.ContentArea.SuspendLayout();
            this.ProjectionGroup.SuspendLayout();
            this.ProjectionTypePanel.SuspendLayout();
            this.ViewGroup.ContentArea.SuspendLayout();
            this.ViewGroup.SuspendLayout();
            this.ViewTypePanel.SuspendLayout();
            this.TransformGroup.ContentArea.SuspendLayout();
            this.TransformGroup.SuspendLayout();
            this.NameGroup.ContentArea.SuspendLayout();
            this.NameGroup.SuspendLayout();
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
            this.Content.ContentArea.Controls.Add(this.TopControls);
            this.Content.ContentArea.Location = new System.Drawing.Point(0, 0);
            this.Content.ContentArea.Name = "ContentArea";
            this.Content.ContentArea.Size = new System.Drawing.Size(256, 1100);
            this.Content.ContentArea.TabIndex = 2;
            this.Content.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Content.Location = new System.Drawing.Point(0, 16);
            this.Content.Name = "Content";
            this.Content.Size = new System.Drawing.Size(256, 1100);
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
            this.ProjectionGroup.ContentArea.Size = new System.Drawing.Size(254, 198);
            this.ProjectionGroup.ContentArea.TabIndex = 0;
            this.ProjectionGroup.ContentColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.ProjectionGroup.Dock = System.Windows.Forms.DockStyle.Top;
            this.ProjectionGroup.ExpandedHeight = 180;
            this.ProjectionGroup.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProjectionGroup.Location = new System.Drawing.Point(0, 885);
            this.ProjectionGroup.Name = "ProjectionGroup";
            this.ProjectionGroup.Size = new System.Drawing.Size(256, 215);
            this.ProjectionGroup.TabIndex = 20;
            this.ProjectionGroup.Title = "Projection";
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
            this.PPARatio.Value = 1.666667F;
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
            this.OPHeight.Value = 240F;
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
            this.PPFovy.Value = 0.7853982F;
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
            this.PZFar.Value = 10000F;
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
            this.PZNear.DecimalPlaces = ((uint)(1u));
            this.PZNear.Dock = System.Windows.Forms.DockStyle.Top;
            this.PZNear.Location = new System.Drawing.Point(0, 37);
            this.PZNear.Margin = new System.Windows.Forms.Padding(0);
            this.PZNear.MaximumValue = 10F;
            this.PZNear.MinimumValue = 0.1F;
            this.PZNear.Name = "PZNear";
            this.PZNear.Size = new System.Drawing.Size(254, 22);
            this.PZNear.TabIndex = 16;
            this.PZNear.Value = 0.1F;
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
            this.ViewGroup.Location = new System.Drawing.Point(0, 573);
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
            this.ATwist.Enabled = false;
            this.ATwist.Location = new System.Drawing.Point(0, 274);
            this.ATwist.Margin = new System.Windows.Forms.Padding(0);
            this.ATwist.MaximumValue = 6.283185F;
            this.ATwist.MinimumValue = 0F;
            this.ATwist.Name = "ATwist";
            this.ATwist.Size = new System.Drawing.Size(254, 22);
            this.ATwist.TabIndex = 31;
            this.ATwist.Value = 0F;
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
            this.LAUpVecY.Value = 1F;
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
            this.RRotZ.MaximumValue = 6.283185F;
            this.RRotZ.MinimumValue = 0F;
            this.RRotZ.Name = "RRotZ";
            this.RRotZ.Size = new System.Drawing.Size(254, 22);
            this.RRotZ.TabIndex = 29;
            this.RRotZ.Value = 0F;
            // 
            // RRotY
            // 
            this.RRotY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.RRotY.DecimalPlaces = ((uint)(6u));
            this.RRotY.Dock = System.Windows.Forms.DockStyle.Top;
            this.RRotY.Enabled = false;
            this.RRotY.Location = new System.Drawing.Point(0, 138);
            this.RRotY.Margin = new System.Windows.Forms.Padding(0);
            this.RRotY.MaximumValue = 6.283185F;
            this.RRotY.MinimumValue = 0F;
            this.RRotY.Name = "RRotY";
            this.RRotY.Size = new System.Drawing.Size(254, 22);
            this.RRotY.TabIndex = 28;
            this.RRotY.Value = 0F;
            // 
            // RRotX
            // 
            this.RRotX.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.RRotX.DecimalPlaces = ((uint)(6u));
            this.RRotX.Dock = System.Windows.Forms.DockStyle.Top;
            this.RRotX.Enabled = false;
            this.RRotX.Location = new System.Drawing.Point(0, 116);
            this.RRotX.Margin = new System.Windows.Forms.Padding(0);
            this.RRotX.MaximumValue = 6.283185F;
            this.RRotX.MinimumValue = 0F;
            this.RRotX.Name = "RRotX";
            this.RRotX.Size = new System.Drawing.Size(254, 22);
            this.RRotX.TabIndex = 26;
            this.RRotX.Value = 0F;
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
            this.TransformGroup.ContentArea.Controls.Add(this.TScaleZ);
            this.TransformGroup.ContentArea.Controls.Add(this.TScaleY);
            this.TransformGroup.ContentArea.Controls.Add(this.TScaleX);
            this.TransformGroup.ContentArea.Controls.Add(this.LblTransformScale);
            this.TransformGroup.ContentArea.Controls.Add(this.TRotZ);
            this.TransformGroup.ContentArea.Controls.Add(this.TRotY);
            this.TransformGroup.ContentArea.Controls.Add(this.TRotX);
            this.TransformGroup.ContentArea.Controls.Add(this.LblTransformRotation);
            this.TransformGroup.ContentArea.Controls.Add(this.TTransZ);
            this.TransformGroup.ContentArea.Controls.Add(this.TTransY);
            this.TransformGroup.ContentArea.Controls.Add(this.TTransX);
            this.TransformGroup.ContentArea.Controls.Add(this.LblTransformTranslation);
            this.TransformGroup.ContentArea.Location = new System.Drawing.Point(1, 16);
            this.TransformGroup.ContentArea.Name = "ContentArea";
            this.TransformGroup.ContentArea.Size = new System.Drawing.Size(254, 236);
            this.TransformGroup.ContentArea.TabIndex = 0;
            this.TransformGroup.ContentColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.TransformGroup.Dock = System.Windows.Forms.DockStyle.Top;
            this.TransformGroup.ExpandedHeight = 253;
            this.TransformGroup.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TransformGroup.Location = new System.Drawing.Point(0, 320);
            this.TransformGroup.Name = "TransformGroup";
            this.TransformGroup.Size = new System.Drawing.Size(256, 253);
            this.TransformGroup.TabIndex = 9;
            this.TransformGroup.Title = "Transform";
            // 
            // TScaleZ
            // 
            this.TScaleZ.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.TScaleZ.DecimalPlaces = ((uint)(2u));
            this.TScaleZ.Dock = System.Windows.Forms.DockStyle.Top;
            this.TScaleZ.Location = new System.Drawing.Point(0, 215);
            this.TScaleZ.Margin = new System.Windows.Forms.Padding(0);
            this.TScaleZ.MaximumValue = 10F;
            this.TScaleZ.MinimumValue = 1F;
            this.TScaleZ.Name = "TScaleZ";
            this.TScaleZ.Size = new System.Drawing.Size(254, 22);
            this.TScaleZ.TabIndex = 11;
            this.TScaleZ.Value = 1F;
            // 
            // TScaleY
            // 
            this.TScaleY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.TScaleY.DecimalPlaces = ((uint)(2u));
            this.TScaleY.Dock = System.Windows.Forms.DockStyle.Top;
            this.TScaleY.Location = new System.Drawing.Point(0, 193);
            this.TScaleY.Margin = new System.Windows.Forms.Padding(0);
            this.TScaleY.MaximumValue = 10F;
            this.TScaleY.MinimumValue = 1F;
            this.TScaleY.Name = "TScaleY";
            this.TScaleY.Size = new System.Drawing.Size(254, 22);
            this.TScaleY.TabIndex = 10;
            this.TScaleY.Value = 1F;
            // 
            // TScaleX
            // 
            this.TScaleX.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.TScaleX.DecimalPlaces = ((uint)(2u));
            this.TScaleX.Dock = System.Windows.Forms.DockStyle.Top;
            this.TScaleX.Location = new System.Drawing.Point(0, 171);
            this.TScaleX.Margin = new System.Windows.Forms.Padding(0);
            this.TScaleX.MaximumValue = 10F;
            this.TScaleX.MinimumValue = 1F;
            this.TScaleX.Name = "TScaleX";
            this.TScaleX.Size = new System.Drawing.Size(254, 22);
            this.TScaleX.TabIndex = 9;
            this.TScaleX.Value = 1F;
            // 
            // LblTransformScale
            // 
            this.LblTransformScale.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblTransformScale.Location = new System.Drawing.Point(0, 158);
            this.LblTransformScale.Name = "LblTransformScale";
            this.LblTransformScale.Size = new System.Drawing.Size(254, 13);
            this.LblTransformScale.TabIndex = 8;
            this.LblTransformScale.Text = "Scale (X/Y/Z):";
            // 
            // TRotZ
            // 
            this.TRotZ.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.TRotZ.DecimalPlaces = ((uint)(6u));
            this.TRotZ.Dock = System.Windows.Forms.DockStyle.Top;
            this.TRotZ.Location = new System.Drawing.Point(0, 136);
            this.TRotZ.Margin = new System.Windows.Forms.Padding(0);
            this.TRotZ.MaximumValue = 6.283185F;
            this.TRotZ.MinimumValue = 0F;
            this.TRotZ.Name = "TRotZ";
            this.TRotZ.Size = new System.Drawing.Size(254, 22);
            this.TRotZ.TabIndex = 7;
            this.TRotZ.Value = 0F;
            // 
            // TRotY
            // 
            this.TRotY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.TRotY.DecimalPlaces = ((uint)(6u));
            this.TRotY.Dock = System.Windows.Forms.DockStyle.Top;
            this.TRotY.Location = new System.Drawing.Point(0, 114);
            this.TRotY.Margin = new System.Windows.Forms.Padding(0);
            this.TRotY.MaximumValue = 6.283185F;
            this.TRotY.MinimumValue = 0F;
            this.TRotY.Name = "TRotY";
            this.TRotY.Size = new System.Drawing.Size(254, 22);
            this.TRotY.TabIndex = 6;
            this.TRotY.Value = 0F;
            // 
            // TRotX
            // 
            this.TRotX.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.TRotX.DecimalPlaces = ((uint)(6u));
            this.TRotX.Dock = System.Windows.Forms.DockStyle.Top;
            this.TRotX.Location = new System.Drawing.Point(0, 92);
            this.TRotX.Margin = new System.Windows.Forms.Padding(0);
            this.TRotX.MaximumValue = 6.283185F;
            this.TRotX.MinimumValue = 0F;
            this.TRotX.Name = "TRotX";
            this.TRotX.Size = new System.Drawing.Size(254, 22);
            this.TRotX.TabIndex = 5;
            this.TRotX.Value = 0F;
            // 
            // LblTransformRotation
            // 
            this.LblTransformRotation.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblTransformRotation.Location = new System.Drawing.Point(0, 79);
            this.LblTransformRotation.Name = "LblTransformRotation";
            this.LblTransformRotation.Size = new System.Drawing.Size(254, 13);
            this.LblTransformRotation.TabIndex = 4;
            this.LblTransformRotation.Text = "Rotation (X/Y/Z):";
            // 
            // TTransZ
            // 
            this.TTransZ.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.TTransZ.DecimalPlaces = ((uint)(1u));
            this.TTransZ.Dock = System.Windows.Forms.DockStyle.Top;
            this.TTransZ.Location = new System.Drawing.Point(0, 57);
            this.TTransZ.Margin = new System.Windows.Forms.Padding(0);
            this.TTransZ.MaximumValue = 100F;
            this.TTransZ.MinimumValue = -100F;
            this.TTransZ.Name = "TTransZ";
            this.TTransZ.Size = new System.Drawing.Size(254, 22);
            this.TTransZ.TabIndex = 3;
            this.TTransZ.Value = 0F;
            // 
            // TTransY
            // 
            this.TTransY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.TTransY.DecimalPlaces = ((uint)(1u));
            this.TTransY.Dock = System.Windows.Forms.DockStyle.Top;
            this.TTransY.Location = new System.Drawing.Point(0, 35);
            this.TTransY.Margin = new System.Windows.Forms.Padding(0);
            this.TTransY.MaximumValue = 100F;
            this.TTransY.MinimumValue = -100F;
            this.TTransY.Name = "TTransY";
            this.TTransY.Size = new System.Drawing.Size(254, 22);
            this.TTransY.TabIndex = 2;
            this.TTransY.Value = 0F;
            // 
            // TTransX
            // 
            this.TTransX.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.TTransX.DecimalPlaces = ((uint)(1u));
            this.TTransX.Dock = System.Windows.Forms.DockStyle.Top;
            this.TTransX.Location = new System.Drawing.Point(0, 13);
            this.TTransX.Margin = new System.Windows.Forms.Padding(0);
            this.TTransX.MaximumValue = 100F;
            this.TTransX.MinimumValue = -100F;
            this.TTransX.Name = "TTransX";
            this.TTransX.Size = new System.Drawing.Size(254, 22);
            this.TTransX.TabIndex = 0;
            this.TTransX.Value = 0F;
            // 
            // LblTransformTranslation
            // 
            this.LblTransformTranslation.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblTransformTranslation.Location = new System.Drawing.Point(0, 0);
            this.LblTransformTranslation.Name = "LblTransformTranslation";
            this.LblTransformTranslation.Size = new System.Drawing.Size(254, 13);
            this.LblTransformTranslation.TabIndex = 1;
            this.LblTransformTranslation.Text = "Translation (X/Y/Z):";
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
            this.NameGroup.Location = new System.Drawing.Point(0, 280);
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
            // 
            // CameraList
            // 
            this.CameraList.BackColor = System.Drawing.Color.Transparent;
            this.CameraList.Dock = System.Windows.Forms.DockStyle.Top;
            this.CameraList.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CameraList.HeaderHeight = 24;
            this.CameraList.ItemHeight = 128;
            this.CameraList.Location = new System.Drawing.Point(0, 24);
            this.CameraList.Name = "CameraList";
            this.CameraList.SelectedIndex = -1;
            this.CameraList.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(15)))), ((int)(((byte)(82)))), ((int)(((byte)(186)))));
            this.CameraList.Size = new System.Drawing.Size(256, 256);
            this.CameraList.TabIndex = 8;
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
            this.TopControls.Size = new System.Drawing.Size(256, 24);
            this.TopControls.TabIndex = 11;
            // 
            // BtnDelete
            // 
            this.BtnDelete.BackColor = System.Drawing.Color.Transparent;
            this.BtnDelete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnDelete.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDelete.Image = ((System.Drawing.Bitmap)(resources.GetObject("BtnDelete.Image")));
            this.BtnDelete.Location = new System.Drawing.Point(128, 0);
            this.BtnDelete.Margin = new System.Windows.Forms.Padding(0);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(64, 24);
            this.BtnDelete.TabIndex = 9;
            this.BtnDelete.Text = "Delete";
            // 
            // BtnClear
            // 
            this.BtnClear.BackColor = System.Drawing.Color.Transparent;
            this.BtnClear.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnClear.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnClear.Image = ((System.Drawing.Bitmap)(resources.GetObject("BtnClear.Image")));
            this.BtnClear.Location = new System.Drawing.Point(192, 0);
            this.BtnClear.Margin = new System.Windows.Forms.Padding(0);
            this.BtnClear.Name = "BtnClear";
            this.BtnClear.Size = new System.Drawing.Size(64, 24);
            this.BtnClear.TabIndex = 8;
            this.BtnClear.Text = "Clear";
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
            this.BtnExport.Size = new System.Drawing.Size(64, 24);
            this.BtnExport.TabIndex = 7;
            this.BtnExport.Text = "Export";
            // 
            // BtnImport
            // 
            this.BtnImport.BackColor = System.Drawing.Color.Transparent;
            this.BtnImport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnImport.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnImport.Image = ((System.Drawing.Bitmap)(resources.GetObject("BtnImport.Image")));
            this.BtnImport.Location = new System.Drawing.Point(64, 0);
            this.BtnImport.Margin = new System.Windows.Forms.Padding(0);
            this.BtnImport.Name = "BtnImport";
            this.BtnImport.Size = new System.Drawing.Size(64, 24);
            this.BtnImport.TabIndex = 6;
            this.BtnImport.Text = "Import";
            // 
            // OCameraWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.Content);
            this.Name = "OCameraWindow";
            this.Size = new System.Drawing.Size(256, 1116);
            this.Controls.SetChildIndex(this.Content, 0);
            this.Content.ContentArea.ResumeLayout(false);
            this.Content.ResumeLayout(false);
            this.ProjectionGroup.ContentArea.ResumeLayout(false);
            this.ProjectionGroup.ResumeLayout(false);
            this.ProjectionGroup.PerformLayout();
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
            this.TopControls.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private OScrollablePanel Content;
        private OGroupBox TransformGroup;
        private OList CameraList;
        private OFloatTextBox TTransX;
        private System.Windows.Forms.Label LblTransformTranslation;
        private System.Windows.Forms.Label LblTransformRotation;
        private OFloatTextBox TTransZ;
        private OFloatTextBox TTransY;
        private OFloatTextBox TRotZ;
        private OFloatTextBox TRotY;
        private OFloatTextBox TRotX;
        private OFloatTextBox TScaleZ;
        private OFloatTextBox TScaleY;
        private OFloatTextBox TScaleX;
        private System.Windows.Forms.Label LblTransformScale;
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
        private OButton BtnDelete;
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


    }
}
