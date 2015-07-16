namespace Ohana3DS_Rebirth.GUI
{
    partial class OLightWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OLightWindow));
            this.TopControls = new System.Windows.Forms.TableLayoutPanel();
            this.BtnClear = new Ohana3DS_Rebirth.GUI.OButton();
            this.BtnExport = new Ohana3DS_Rebirth.GUI.OButton();
            this.BtnImport = new Ohana3DS_Rebirth.GUI.OButton();
            this.TopControlsExtended = new System.Windows.Forms.TableLayoutPanel();
            this.BtnReset = new Ohana3DS_Rebirth.GUI.OButton();
            this.BtnDelete = new Ohana3DS_Rebirth.GUI.OButton();
            this.BtnAdd = new Ohana3DS_Rebirth.GUI.OButton();
            this.LightList = new Ohana3DS_Rebirth.GUI.OList();
            this.NameGroup = new Ohana3DS_Rebirth.GUI.OGroupBox();
            this.TxtLightName = new Ohana3DS_Rebirth.GUI.OTextBox();
            this.LightGroup = new Ohana3DS_Rebirth.GUI.OGroupBox();
            this.HemisphereLightPanel = new System.Windows.Forms.Panel();
            this.SkyDirZ = new Ohana3DS_Rebirth.GUI.OFloatTextBox();
            this.SkyDirY = new Ohana3DS_Rebirth.GUI.OFloatTextBox();
            this.SkyDirX = new Ohana3DS_Rebirth.GUI.OFloatTextBox();
            this.LblSkyDir = new Ohana3DS_Rebirth.GUI.OLabel();
            this.GroundColor = new Ohana3DS_Rebirth.GUI.ORgbaColorBox();
            this.LblGroundColor = new Ohana3DS_Rebirth.GUI.OLabel();
            this.SkyColor = new Ohana3DS_Rebirth.GUI.ORgbaColorBox();
            this.LblSkyColor = new Ohana3DS_Rebirth.GUI.OLabel();
            this.HLightEnabled = new Ohana3DS_Rebirth.GUI.OCheckBox();
            this.AmbientLightPanel = new System.Windows.Forms.Panel();
            this.AAmbientColor = new Ohana3DS_Rebirth.GUI.ORgbaColorBox();
            this.LblAAmbientColor = new Ohana3DS_Rebirth.GUI.OLabel();
            this.ALightEnabled = new Ohana3DS_Rebirth.GUI.OCheckBox();
            this.VertexLightPanel = new System.Windows.Forms.Panel();
            this.VDistanceAttGroup = new Ohana3DS_Rebirth.GUI.OGroupBox();
            this.QuadraticAtt = new Ohana3DS_Rebirth.GUI.OFloatTextBox();
            this.LblQuadraticAtt = new Ohana3DS_Rebirth.GUI.OLabel();
            this.LinearAtt = new Ohana3DS_Rebirth.GUI.OFloatTextBox();
            this.LblLinearAtt = new Ohana3DS_Rebirth.GUI.OLabel();
            this.ConstantAtt = new Ohana3DS_Rebirth.GUI.OFloatTextBox();
            this.LblConstantAtt = new Ohana3DS_Rebirth.GUI.OLabel();
            this.VAttEnabled = new Ohana3DS_Rebirth.GUI.OCheckBox();
            this.VAngularAttGroup = new Ohana3DS_Rebirth.GUI.OGroupBox();
            this.Exponent = new Ohana3DS_Rebirth.GUI.OFloatTextBox();
            this.LblExp = new Ohana3DS_Rebirth.GUI.OLabel();
            this.CutOffAngle = new Ohana3DS_Rebirth.GUI.OFloatTextBox();
            this.LblCutOffAngle = new Ohana3DS_Rebirth.GUI.OLabel();
            this.VDirZ = new Ohana3DS_Rebirth.GUI.OFloatTextBox();
            this.VDirY = new Ohana3DS_Rebirth.GUI.OFloatTextBox();
            this.VDirX = new Ohana3DS_Rebirth.GUI.OFloatTextBox();
            this.LblVDirection = new Ohana3DS_Rebirth.GUI.OLabel();
            this.VPosZ = new Ohana3DS_Rebirth.GUI.OFloatTextBox();
            this.VPosY = new Ohana3DS_Rebirth.GUI.OFloatTextBox();
            this.VPosX = new Ohana3DS_Rebirth.GUI.OFloatTextBox();
            this.LblVPosition = new Ohana3DS_Rebirth.GUI.OLabel();
            this.VLightTypePanel = new System.Windows.Forms.TableLayoutPanel();
            this.VRadioPoint = new Ohana3DS_Rebirth.GUI.ORadioButton();
            this.VRadioSpot = new Ohana3DS_Rebirth.GUI.ORadioButton();
            this.VRadioDirectional = new Ohana3DS_Rebirth.GUI.ORadioButton();
            this.VDiffuseColor = new Ohana3DS_Rebirth.GUI.ORgbaColorBox();
            this.LblVDiffuseColor = new Ohana3DS_Rebirth.GUI.OLabel();
            this.VAmbientColor = new Ohana3DS_Rebirth.GUI.ORgbaColorBox();
            this.LblVAmbientColor = new Ohana3DS_Rebirth.GUI.OLabel();
            this.VLightEnabled = new Ohana3DS_Rebirth.GUI.OCheckBox();
            this.FragmentLightPanel = new System.Windows.Forms.Panel();
            this.FDistanceAttGroup = new Ohana3DS_Rebirth.GUI.OGroupBox();
            this.AttEnd = new Ohana3DS_Rebirth.GUI.OFloatTextBox();
            this.LblAttEnd = new Ohana3DS_Rebirth.GUI.OLabel();
            this.AttStart = new Ohana3DS_Rebirth.GUI.OFloatTextBox();
            this.LblAttStart = new Ohana3DS_Rebirth.GUI.OLabel();
            this.TxtDistanceAttLUT = new Ohana3DS_Rebirth.GUI.OTextBox();
            this.LblDALUT = new Ohana3DS_Rebirth.GUI.OLabel();
            this.FAttEnabled = new Ohana3DS_Rebirth.GUI.OCheckBox();
            this.FAngularAttGroup = new Ohana3DS_Rebirth.GUI.OGroupBox();
            this.InputScale = new Ohana3DS_Rebirth.GUI.OComboBox();
            this.LblInputScale = new Ohana3DS_Rebirth.GUI.OLabel();
            this.InputAngle = new Ohana3DS_Rebirth.GUI.OComboBox();
            this.LblInputAngle = new Ohana3DS_Rebirth.GUI.OLabel();
            this.TxtAngularAttLUT = new Ohana3DS_Rebirth.GUI.OTextBox();
            this.LblAALUT = new Ohana3DS_Rebirth.GUI.OLabel();
            this.FDirZ = new Ohana3DS_Rebirth.GUI.OFloatTextBox();
            this.FDirY = new Ohana3DS_Rebirth.GUI.OFloatTextBox();
            this.FDirX = new Ohana3DS_Rebirth.GUI.OFloatTextBox();
            this.LblFDirection = new Ohana3DS_Rebirth.GUI.OLabel();
            this.FPosZ = new Ohana3DS_Rebirth.GUI.OFloatTextBox();
            this.FPosY = new Ohana3DS_Rebirth.GUI.OFloatTextBox();
            this.FPosX = new Ohana3DS_Rebirth.GUI.OFloatTextBox();
            this.LblFPosition = new Ohana3DS_Rebirth.GUI.OLabel();
            this.FLightTypePanel = new System.Windows.Forms.TableLayoutPanel();
            this.FRadioPoint = new Ohana3DS_Rebirth.GUI.ORadioButton();
            this.FRadioSpot = new Ohana3DS_Rebirth.GUI.ORadioButton();
            this.FRadioDirectional = new Ohana3DS_Rebirth.GUI.ORadioButton();
            this.Spec1Color = new Ohana3DS_Rebirth.GUI.ORgbaColorBox();
            this.LblSpec1 = new Ohana3DS_Rebirth.GUI.OLabel();
            this.Spec0Color = new Ohana3DS_Rebirth.GUI.ORgbaColorBox();
            this.LblSpec0 = new Ohana3DS_Rebirth.GUI.OLabel();
            this.FTwoSidedDiffuse = new Ohana3DS_Rebirth.GUI.OCheckBox();
            this.FDiffuseColor = new Ohana3DS_Rebirth.GUI.ORgbaColorBox();
            this.LblFDiffuseColor = new Ohana3DS_Rebirth.GUI.OLabel();
            this.FAmbientColor = new Ohana3DS_Rebirth.GUI.ORgbaColorBox();
            this.LblFAmbientColor = new Ohana3DS_Rebirth.GUI.OLabel();
            this.FLightEnabled = new Ohana3DS_Rebirth.GUI.OCheckBox();
            this.LightUsePanel = new System.Windows.Forms.TableLayoutPanel();
            this.RadioVertex = new Ohana3DS_Rebirth.GUI.ORadioButton();
            this.RadioFragment = new Ohana3DS_Rebirth.GUI.ORadioButton();
            this.RadioHemisphere = new Ohana3DS_Rebirth.GUI.ORadioButton();
            this.RadioAmbient = new Ohana3DS_Rebirth.GUI.ORadioButton();
            this.Content = new Ohana3DS_Rebirth.GUI.OScrollablePanel();
            this.TopControls.SuspendLayout();
            this.TopControlsExtended.SuspendLayout();
            this.NameGroup.ContentArea.SuspendLayout();
            this.NameGroup.SuspendLayout();
            this.LightGroup.ContentArea.SuspendLayout();
            this.LightGroup.SuspendLayout();
            this.HemisphereLightPanel.SuspendLayout();
            this.AmbientLightPanel.SuspendLayout();
            this.VertexLightPanel.SuspendLayout();
            this.VDistanceAttGroup.ContentArea.SuspendLayout();
            this.VDistanceAttGroup.SuspendLayout();
            this.VAngularAttGroup.ContentArea.SuspendLayout();
            this.VAngularAttGroup.SuspendLayout();
            this.VLightTypePanel.SuspendLayout();
            this.FragmentLightPanel.SuspendLayout();
            this.FDistanceAttGroup.ContentArea.SuspendLayout();
            this.FDistanceAttGroup.SuspendLayout();
            this.FAngularAttGroup.ContentArea.SuspendLayout();
            this.FAngularAttGroup.SuspendLayout();
            this.FLightTypePanel.SuspendLayout();
            this.LightUsePanel.SuspendLayout();
            this.Content.ContentArea.SuspendLayout();
            this.Content.SuspendLayout();
            this.SuspendLayout();
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
            this.TopControls.TabIndex = 12;
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
            this.TopControlsExtended.TabIndex = 22;
            // 
            // BtnReset
            // 
            this.BtnReset.BackColor = System.Drawing.Color.Transparent;
            this.BtnReset.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnReset.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnReset.Image = global::Ohana3DS_Rebirth.Properties.Resources.icn_lightbulb;
            this.BtnReset.Location = new System.Drawing.Point(170, 0);
            this.BtnReset.Margin = new System.Windows.Forms.Padding(0);
            this.BtnReset.Name = "BtnReset";
            this.BtnReset.Size = new System.Drawing.Size(86, 24);
            this.BtnReset.TabIndex = 14;
            this.BtnReset.Text = "Reset";
            this.BtnReset.Click += new System.EventHandler(this.BtnReset_Click);
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
            // LightList
            // 
            this.LightList.BackColor = System.Drawing.Color.Transparent;
            this.LightList.Dock = System.Windows.Forms.DockStyle.Top;
            this.LightList.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LightList.HeaderHeight = 24;
            this.LightList.ItemHeight = 16;
            this.LightList.Location = new System.Drawing.Point(0, 48);
            this.LightList.Name = "LightList";
            this.LightList.SelectedIndex = -1;
            this.LightList.Size = new System.Drawing.Size(256, 256);
            this.LightList.TabIndex = 23;
            this.LightList.SelectedIndexChanged += new System.EventHandler(this.LightList_SelectedIndexChanged);
            // 
            // NameGroup
            // 
            this.NameGroup.AutomaticSize = false;
            this.NameGroup.BackColor = System.Drawing.Color.Black;
            this.NameGroup.Collapsed = false;
            // 
            // NameGroup.ContentArea
            // 
            this.NameGroup.ContentArea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.NameGroup.ContentArea.Controls.Add(this.TxtLightName);
            this.NameGroup.ContentArea.Location = new System.Drawing.Point(1, 16);
            this.NameGroup.ContentArea.Name = "ContentArea";
            this.NameGroup.ContentArea.Size = new System.Drawing.Size(254, 20);
            this.NameGroup.ContentArea.TabIndex = 0;
            this.NameGroup.ContentColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.NameGroup.Dock = System.Windows.Forms.DockStyle.Top;
            this.NameGroup.Enabled = false;
            this.NameGroup.ExpandedHeight = 20;
            this.NameGroup.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NameGroup.Location = new System.Drawing.Point(0, 304);
            this.NameGroup.Name = "NameGroup";
            this.NameGroup.Size = new System.Drawing.Size(256, 37);
            this.NameGroup.TabIndex = 25;
            this.NameGroup.Title = "Name";
            // 
            // TxtLightName
            // 
            this.TxtLightName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.TxtLightName.CharacterWhiteList = null;
            this.TxtLightName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtLightName.Location = new System.Drawing.Point(0, 0);
            this.TxtLightName.Name = "TxtLightName";
            this.TxtLightName.Size = new System.Drawing.Size(254, 20);
            this.TxtLightName.TabIndex = 0;
            // 
            // LightGroup
            // 
            this.LightGroup.AutomaticSize = true;
            this.LightGroup.BackColor = System.Drawing.Color.Black;
            this.LightGroup.Collapsed = false;
            // 
            // LightGroup.ContentArea
            // 
            this.LightGroup.ContentArea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.LightGroup.ContentArea.Controls.Add(this.HemisphereLightPanel);
            this.LightGroup.ContentArea.Controls.Add(this.AmbientLightPanel);
            this.LightGroup.ContentArea.Controls.Add(this.VertexLightPanel);
            this.LightGroup.ContentArea.Controls.Add(this.FragmentLightPanel);
            this.LightGroup.ContentArea.Controls.Add(this.LightUsePanel);
            this.LightGroup.ContentArea.Location = new System.Drawing.Point(1, 16);
            this.LightGroup.ContentArea.Name = "ContentArea";
            this.LightGroup.ContentArea.Size = new System.Drawing.Size(254, 1941);
            this.LightGroup.ContentArea.TabIndex = 0;
            this.LightGroup.ContentColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.LightGroup.Dock = System.Windows.Forms.DockStyle.Top;
            this.LightGroup.Enabled = false;
            this.LightGroup.ExpandedHeight = 1941;
            this.LightGroup.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LightGroup.Location = new System.Drawing.Point(0, 341);
            this.LightGroup.Name = "LightGroup";
            this.LightGroup.Size = new System.Drawing.Size(256, 1958);
            this.LightGroup.TabIndex = 26;
            this.LightGroup.Title = "Light";
            // 
            // HemisphereLightPanel
            // 
            this.HemisphereLightPanel.AutoSize = true;
            this.HemisphereLightPanel.Controls.Add(this.SkyDirZ);
            this.HemisphereLightPanel.Controls.Add(this.SkyDirY);
            this.HemisphereLightPanel.Controls.Add(this.SkyDirX);
            this.HemisphereLightPanel.Controls.Add(this.LblSkyDir);
            this.HemisphereLightPanel.Controls.Add(this.GroundColor);
            this.HemisphereLightPanel.Controls.Add(this.LblGroundColor);
            this.HemisphereLightPanel.Controls.Add(this.SkyColor);
            this.HemisphereLightPanel.Controls.Add(this.LblSkyColor);
            this.HemisphereLightPanel.Controls.Add(this.HLightEnabled);
            this.HemisphereLightPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.HemisphereLightPanel.Location = new System.Drawing.Point(0, 1644);
            this.HemisphereLightPanel.Name = "HemisphereLightPanel";
            this.HemisphereLightPanel.Size = new System.Drawing.Size(254, 297);
            this.HemisphereLightPanel.TabIndex = 37;
            this.HemisphereLightPanel.Visible = false;
            // 
            // SkyDirZ
            // 
            this.SkyDirZ.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.SkyDirZ.DecimalPlaces = ((uint)(1u));
            this.SkyDirZ.Dock = System.Windows.Forms.DockStyle.Top;
            this.SkyDirZ.Location = new System.Drawing.Point(0, 275);
            this.SkyDirZ.Margin = new System.Windows.Forms.Padding(0);
            this.SkyDirZ.MaximumValue = 1F;
            this.SkyDirZ.MinimumValue = -1F;
            this.SkyDirZ.Name = "SkyDirZ";
            this.SkyDirZ.Size = new System.Drawing.Size(254, 22);
            this.SkyDirZ.TabIndex = 72;
            this.SkyDirZ.Value = 0F;
            this.SkyDirZ.ValueChanged += new System.EventHandler(this.SkyDirZ_ValueChanged);
            // 
            // SkyDirY
            // 
            this.SkyDirY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.SkyDirY.DecimalPlaces = ((uint)(1u));
            this.SkyDirY.Dock = System.Windows.Forms.DockStyle.Top;
            this.SkyDirY.Location = new System.Drawing.Point(0, 253);
            this.SkyDirY.Margin = new System.Windows.Forms.Padding(0);
            this.SkyDirY.MaximumValue = 1F;
            this.SkyDirY.MinimumValue = -1F;
            this.SkyDirY.Name = "SkyDirY";
            this.SkyDirY.Size = new System.Drawing.Size(254, 22);
            this.SkyDirY.TabIndex = 71;
            this.SkyDirY.Value = 0F;
            this.SkyDirY.ValueChanged += new System.EventHandler(this.SkyDirY_ValueChanged);
            // 
            // SkyDirX
            // 
            this.SkyDirX.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.SkyDirX.DecimalPlaces = ((uint)(1u));
            this.SkyDirX.Dock = System.Windows.Forms.DockStyle.Top;
            this.SkyDirX.Location = new System.Drawing.Point(0, 231);
            this.SkyDirX.Margin = new System.Windows.Forms.Padding(0);
            this.SkyDirX.MaximumValue = 1F;
            this.SkyDirX.MinimumValue = -1F;
            this.SkyDirX.Name = "SkyDirX";
            this.SkyDirX.Size = new System.Drawing.Size(254, 22);
            this.SkyDirX.TabIndex = 70;
            this.SkyDirX.Value = 0F;
            this.SkyDirX.ValueChanged += new System.EventHandler(this.SkyDirX_ValueChanged);
            // 
            // LblSkyDir
            // 
            this.LblSkyDir.AutomaticSize = false;
            this.LblSkyDir.Centered = false;
            this.LblSkyDir.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblSkyDir.Location = new System.Drawing.Point(0, 218);
            this.LblSkyDir.Name = "LblSkyDir";
            this.LblSkyDir.Size = new System.Drawing.Size(254, 13);
            this.LblSkyDir.TabIndex = 69;
            this.LblSkyDir.Text = "Sky direction (X/Y/Z):";
            // 
            // GroundColor
            // 
            this.GroundColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.GroundColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.GroundColor.Dock = System.Windows.Forms.DockStyle.Top;
            this.GroundColor.ForeColor = System.Drawing.Color.White;
            this.GroundColor.Location = new System.Drawing.Point(0, 130);
            this.GroundColor.Margin = new System.Windows.Forms.Padding(0);
            this.GroundColor.Name = "GroundColor";
            this.GroundColor.Size = new System.Drawing.Size(254, 88);
            this.GroundColor.TabIndex = 68;
            this.GroundColor.ColorChanged += new System.EventHandler(this.GroundColor_ColorChanged);
            // 
            // LblGroundColor
            // 
            this.LblGroundColor.AutomaticSize = false;
            this.LblGroundColor.Centered = false;
            this.LblGroundColor.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblGroundColor.Location = new System.Drawing.Point(0, 117);
            this.LblGroundColor.Name = "LblGroundColor";
            this.LblGroundColor.Size = new System.Drawing.Size(254, 13);
            this.LblGroundColor.TabIndex = 67;
            this.LblGroundColor.Text = "Ground color:";
            // 
            // SkyColor
            // 
            this.SkyColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.SkyColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.SkyColor.Dock = System.Windows.Forms.DockStyle.Top;
            this.SkyColor.ForeColor = System.Drawing.Color.White;
            this.SkyColor.Location = new System.Drawing.Point(0, 29);
            this.SkyColor.Margin = new System.Windows.Forms.Padding(0);
            this.SkyColor.Name = "SkyColor";
            this.SkyColor.Size = new System.Drawing.Size(254, 88);
            this.SkyColor.TabIndex = 35;
            this.SkyColor.ColorChanged += new System.EventHandler(this.SkyColor_ColorChanged);
            // 
            // LblSkyColor
            // 
            this.LblSkyColor.AutomaticSize = false;
            this.LblSkyColor.Centered = false;
            this.LblSkyColor.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblSkyColor.Location = new System.Drawing.Point(0, 16);
            this.LblSkyColor.Name = "LblSkyColor";
            this.LblSkyColor.Size = new System.Drawing.Size(254, 13);
            this.LblSkyColor.TabIndex = 34;
            this.LblSkyColor.Text = "Sky color:";
            // 
            // HLightEnabled
            // 
            this.HLightEnabled.BoxColor = System.Drawing.Color.Black;
            this.HLightEnabled.Checked = false;
            this.HLightEnabled.Dock = System.Windows.Forms.DockStyle.Top;
            this.HLightEnabled.Location = new System.Drawing.Point(0, 0);
            this.HLightEnabled.Name = "HLightEnabled";
            this.HLightEnabled.Size = new System.Drawing.Size(254, 16);
            this.HLightEnabled.TabIndex = 66;
            this.HLightEnabled.Text = "Light enabled";
            this.HLightEnabled.Click += new System.EventHandler(this.HLightEnabled_Click);
            // 
            // AmbientLightPanel
            // 
            this.AmbientLightPanel.AutoSize = true;
            this.AmbientLightPanel.Controls.Add(this.AAmbientColor);
            this.AmbientLightPanel.Controls.Add(this.LblAAmbientColor);
            this.AmbientLightPanel.Controls.Add(this.ALightEnabled);
            this.AmbientLightPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.AmbientLightPanel.Location = new System.Drawing.Point(0, 1527);
            this.AmbientLightPanel.Name = "AmbientLightPanel";
            this.AmbientLightPanel.Size = new System.Drawing.Size(254, 117);
            this.AmbientLightPanel.TabIndex = 36;
            this.AmbientLightPanel.Visible = false;
            // 
            // AAmbientColor
            // 
            this.AAmbientColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.AAmbientColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.AAmbientColor.Dock = System.Windows.Forms.DockStyle.Top;
            this.AAmbientColor.ForeColor = System.Drawing.Color.White;
            this.AAmbientColor.Location = new System.Drawing.Point(0, 29);
            this.AAmbientColor.Margin = new System.Windows.Forms.Padding(0);
            this.AAmbientColor.Name = "AAmbientColor";
            this.AAmbientColor.Size = new System.Drawing.Size(254, 88);
            this.AAmbientColor.TabIndex = 35;
            this.AAmbientColor.ColorChanged += new System.EventHandler(this.AAmbientColor_ColorChanged);
            // 
            // LblAAmbientColor
            // 
            this.LblAAmbientColor.AutomaticSize = false;
            this.LblAAmbientColor.Centered = false;
            this.LblAAmbientColor.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblAAmbientColor.Location = new System.Drawing.Point(0, 16);
            this.LblAAmbientColor.Name = "LblAAmbientColor";
            this.LblAAmbientColor.Size = new System.Drawing.Size(254, 13);
            this.LblAAmbientColor.TabIndex = 34;
            this.LblAAmbientColor.Text = "Ambient color:";
            // 
            // ALightEnabled
            // 
            this.ALightEnabled.BoxColor = System.Drawing.Color.Black;
            this.ALightEnabled.Checked = false;
            this.ALightEnabled.Dock = System.Windows.Forms.DockStyle.Top;
            this.ALightEnabled.Location = new System.Drawing.Point(0, 0);
            this.ALightEnabled.Name = "ALightEnabled";
            this.ALightEnabled.Size = new System.Drawing.Size(254, 16);
            this.ALightEnabled.TabIndex = 66;
            this.ALightEnabled.Text = "Light enabled";
            this.ALightEnabled.CheckedChanged += new System.EventHandler(this.ALightEnabled_CheckedChanged);
            // 
            // VertexLightPanel
            // 
            this.VertexLightPanel.AutoSize = true;
            this.VertexLightPanel.Controls.Add(this.VDistanceAttGroup);
            this.VertexLightPanel.Controls.Add(this.VAngularAttGroup);
            this.VertexLightPanel.Controls.Add(this.VDirZ);
            this.VertexLightPanel.Controls.Add(this.VDirY);
            this.VertexLightPanel.Controls.Add(this.VDirX);
            this.VertexLightPanel.Controls.Add(this.LblVDirection);
            this.VertexLightPanel.Controls.Add(this.VPosZ);
            this.VertexLightPanel.Controls.Add(this.VPosY);
            this.VertexLightPanel.Controls.Add(this.VPosX);
            this.VertexLightPanel.Controls.Add(this.LblVPosition);
            this.VertexLightPanel.Controls.Add(this.VLightTypePanel);
            this.VertexLightPanel.Controls.Add(this.VDiffuseColor);
            this.VertexLightPanel.Controls.Add(this.LblVDiffuseColor);
            this.VertexLightPanel.Controls.Add(this.VAmbientColor);
            this.VertexLightPanel.Controls.Add(this.LblVAmbientColor);
            this.VertexLightPanel.Controls.Add(this.VLightEnabled);
            this.VertexLightPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.VertexLightPanel.Location = new System.Drawing.Point(0, 902);
            this.VertexLightPanel.Name = "VertexLightPanel";
            this.VertexLightPanel.Size = new System.Drawing.Size(254, 625);
            this.VertexLightPanel.TabIndex = 35;
            this.VertexLightPanel.Visible = false;
            // 
            // VDistanceAttGroup
            // 
            this.VDistanceAttGroup.AutomaticSize = true;
            this.VDistanceAttGroup.BackColor = System.Drawing.Color.Black;
            this.VDistanceAttGroup.Collapsed = false;
            // 
            // VDistanceAttGroup.ContentArea
            // 
            this.VDistanceAttGroup.ContentArea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.VDistanceAttGroup.ContentArea.Controls.Add(this.QuadraticAtt);
            this.VDistanceAttGroup.ContentArea.Controls.Add(this.LblQuadraticAtt);
            this.VDistanceAttGroup.ContentArea.Controls.Add(this.LinearAtt);
            this.VDistanceAttGroup.ContentArea.Controls.Add(this.LblLinearAtt);
            this.VDistanceAttGroup.ContentArea.Controls.Add(this.ConstantAtt);
            this.VDistanceAttGroup.ContentArea.Controls.Add(this.LblConstantAtt);
            this.VDistanceAttGroup.ContentArea.Controls.Add(this.VAttEnabled);
            this.VDistanceAttGroup.ContentArea.Location = new System.Drawing.Point(1, 16);
            this.VDistanceAttGroup.ContentArea.Name = "ContentArea";
            this.VDistanceAttGroup.ContentArea.Size = new System.Drawing.Size(252, 121);
            this.VDistanceAttGroup.ContentArea.TabIndex = 0;
            this.VDistanceAttGroup.ContentColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.VDistanceAttGroup.Dock = System.Windows.Forms.DockStyle.Top;
            this.VDistanceAttGroup.Enabled = false;
            this.VDistanceAttGroup.ExpandedHeight = 121;
            this.VDistanceAttGroup.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VDistanceAttGroup.Location = new System.Drawing.Point(0, 487);
            this.VDistanceAttGroup.Name = "VDistanceAttGroup";
            this.VDistanceAttGroup.Size = new System.Drawing.Size(254, 138);
            this.VDistanceAttGroup.TabIndex = 54;
            this.VDistanceAttGroup.Title = "Distance Attenuation";
            // 
            // QuadraticAtt
            // 
            this.QuadraticAtt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.QuadraticAtt.DecimalPlaces = ((uint)(6u));
            this.QuadraticAtt.Dock = System.Windows.Forms.DockStyle.Top;
            this.QuadraticAtt.Location = new System.Drawing.Point(0, 99);
            this.QuadraticAtt.Margin = new System.Windows.Forms.Padding(0);
            this.QuadraticAtt.MaximumValue = 1F;
            this.QuadraticAtt.MinimumValue = 0F;
            this.QuadraticAtt.Name = "QuadraticAtt";
            this.QuadraticAtt.Size = new System.Drawing.Size(252, 22);
            this.QuadraticAtt.TabIndex = 53;
            this.QuadraticAtt.Value = 0F;
            this.QuadraticAtt.ValueChanged += new System.EventHandler(this.QuadraticAtt_ValueChanged);
            // 
            // LblQuadraticAtt
            // 
            this.LblQuadraticAtt.AutomaticSize = false;
            this.LblQuadraticAtt.Centered = false;
            this.LblQuadraticAtt.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblQuadraticAtt.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblQuadraticAtt.Location = new System.Drawing.Point(0, 86);
            this.LblQuadraticAtt.Name = "LblQuadraticAtt";
            this.LblQuadraticAtt.Size = new System.Drawing.Size(252, 13);
            this.LblQuadraticAtt.TabIndex = 52;
            this.LblQuadraticAtt.Text = "Quadratic attenuation:";
            // 
            // LinearAtt
            // 
            this.LinearAtt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.LinearAtt.DecimalPlaces = ((uint)(6u));
            this.LinearAtt.Dock = System.Windows.Forms.DockStyle.Top;
            this.LinearAtt.Location = new System.Drawing.Point(0, 64);
            this.LinearAtt.Margin = new System.Windows.Forms.Padding(0);
            this.LinearAtt.MaximumValue = 1F;
            this.LinearAtt.MinimumValue = 0F;
            this.LinearAtt.Name = "LinearAtt";
            this.LinearAtt.Size = new System.Drawing.Size(252, 22);
            this.LinearAtt.TabIndex = 54;
            this.LinearAtt.Value = 0F;
            this.LinearAtt.ValueChanged += new System.EventHandler(this.LinearAtt_ValueChanged);
            // 
            // LblLinearAtt
            // 
            this.LblLinearAtt.AutomaticSize = false;
            this.LblLinearAtt.Centered = false;
            this.LblLinearAtt.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblLinearAtt.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblLinearAtt.Location = new System.Drawing.Point(0, 51);
            this.LblLinearAtt.Name = "LblLinearAtt";
            this.LblLinearAtt.Size = new System.Drawing.Size(252, 13);
            this.LblLinearAtt.TabIndex = 49;
            this.LblLinearAtt.Text = "Linear attenuation:";
            // 
            // ConstantAtt
            // 
            this.ConstantAtt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.ConstantAtt.DecimalPlaces = ((uint)(6u));
            this.ConstantAtt.Dock = System.Windows.Forms.DockStyle.Top;
            this.ConstantAtt.Location = new System.Drawing.Point(0, 29);
            this.ConstantAtt.Margin = new System.Windows.Forms.Padding(0);
            this.ConstantAtt.MaximumValue = 1F;
            this.ConstantAtt.MinimumValue = 0F;
            this.ConstantAtt.Name = "ConstantAtt";
            this.ConstantAtt.Size = new System.Drawing.Size(252, 22);
            this.ConstantAtt.TabIndex = 56;
            this.ConstantAtt.Value = 0F;
            this.ConstantAtt.ValueChanged += new System.EventHandler(this.ConstantAtt_ValueChanged);
            // 
            // LblConstantAtt
            // 
            this.LblConstantAtt.AutomaticSize = false;
            this.LblConstantAtt.Centered = false;
            this.LblConstantAtt.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblConstantAtt.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblConstantAtt.Location = new System.Drawing.Point(0, 16);
            this.LblConstantAtt.Name = "LblConstantAtt";
            this.LblConstantAtt.Size = new System.Drawing.Size(252, 13);
            this.LblConstantAtt.TabIndex = 50;
            this.LblConstantAtt.Text = "Constant attenuation:";
            // 
            // VAttEnabled
            // 
            this.VAttEnabled.BoxColor = System.Drawing.Color.Black;
            this.VAttEnabled.Checked = false;
            this.VAttEnabled.Dock = System.Windows.Forms.DockStyle.Top;
            this.VAttEnabled.Location = new System.Drawing.Point(0, 0);
            this.VAttEnabled.Name = "VAttEnabled";
            this.VAttEnabled.Size = new System.Drawing.Size(252, 16);
            this.VAttEnabled.TabIndex = 55;
            this.VAttEnabled.Text = "Attenuation enabled";
            this.VAttEnabled.CheckedChanged += new System.EventHandler(this.VAttEnabled_CheckedChanged);
            // 
            // VAngularAttGroup
            // 
            this.VAngularAttGroup.AutomaticSize = true;
            this.VAngularAttGroup.BackColor = System.Drawing.Color.Black;
            this.VAngularAttGroup.Collapsed = false;
            // 
            // VAngularAttGroup.ContentArea
            // 
            this.VAngularAttGroup.ContentArea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.VAngularAttGroup.ContentArea.Controls.Add(this.Exponent);
            this.VAngularAttGroup.ContentArea.Controls.Add(this.LblExp);
            this.VAngularAttGroup.ContentArea.Controls.Add(this.CutOffAngle);
            this.VAngularAttGroup.ContentArea.Controls.Add(this.LblCutOffAngle);
            this.VAngularAttGroup.ContentArea.Location = new System.Drawing.Point(1, 16);
            this.VAngularAttGroup.ContentArea.Name = "ContentArea";
            this.VAngularAttGroup.ContentArea.Size = new System.Drawing.Size(252, 70);
            this.VAngularAttGroup.ContentArea.TabIndex = 0;
            this.VAngularAttGroup.ContentColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.VAngularAttGroup.Dock = System.Windows.Forms.DockStyle.Top;
            this.VAngularAttGroup.Enabled = false;
            this.VAngularAttGroup.ExpandedHeight = 70;
            this.VAngularAttGroup.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VAngularAttGroup.Location = new System.Drawing.Point(0, 400);
            this.VAngularAttGroup.Name = "VAngularAttGroup";
            this.VAngularAttGroup.Size = new System.Drawing.Size(254, 87);
            this.VAngularAttGroup.TabIndex = 48;
            this.VAngularAttGroup.Title = "Angular Attenuation";
            // 
            // Exponent
            // 
            this.Exponent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.Exponent.DecimalPlaces = ((uint)(0u));
            this.Exponent.Dock = System.Windows.Forms.DockStyle.Top;
            this.Exponent.Location = new System.Drawing.Point(0, 48);
            this.Exponent.Margin = new System.Windows.Forms.Padding(0);
            this.Exponent.MaximumValue = 128F;
            this.Exponent.MinimumValue = 0F;
            this.Exponent.Name = "Exponent";
            this.Exponent.Size = new System.Drawing.Size(252, 22);
            this.Exponent.TabIndex = 61;
            this.Exponent.Value = 0F;
            this.Exponent.ValueChanged += new System.EventHandler(this.Exponent_ValueChanged);
            // 
            // LblExp
            // 
            this.LblExp.AutomaticSize = false;
            this.LblExp.Centered = false;
            this.LblExp.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblExp.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblExp.Location = new System.Drawing.Point(0, 35);
            this.LblExp.Name = "LblExp";
            this.LblExp.Size = new System.Drawing.Size(252, 13);
            this.LblExp.TabIndex = 57;
            this.LblExp.Text = "Exponent:";
            // 
            // CutOffAngle
            // 
            this.CutOffAngle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.CutOffAngle.DecimalPlaces = ((uint)(6u));
            this.CutOffAngle.Dock = System.Windows.Forms.DockStyle.Top;
            this.CutOffAngle.Location = new System.Drawing.Point(0, 13);
            this.CutOffAngle.Margin = new System.Windows.Forms.Padding(0);
            this.CutOffAngle.MaximumValue = 3.141593F;
            this.CutOffAngle.MinimumValue = 0F;
            this.CutOffAngle.Name = "CutOffAngle";
            this.CutOffAngle.Size = new System.Drawing.Size(252, 22);
            this.CutOffAngle.TabIndex = 60;
            this.CutOffAngle.Value = 0F;
            this.CutOffAngle.ValueChanged += new System.EventHandler(this.CutOffAngle_ValueChanged);
            // 
            // LblCutOffAngle
            // 
            this.LblCutOffAngle.AutomaticSize = false;
            this.LblCutOffAngle.Centered = false;
            this.LblCutOffAngle.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblCutOffAngle.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblCutOffAngle.Location = new System.Drawing.Point(0, 0);
            this.LblCutOffAngle.Name = "LblCutOffAngle";
            this.LblCutOffAngle.Size = new System.Drawing.Size(252, 13);
            this.LblCutOffAngle.TabIndex = 59;
            this.LblCutOffAngle.Text = "Cut-off angle:";
            // 
            // VDirZ
            // 
            this.VDirZ.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.VDirZ.DecimalPlaces = ((uint)(1u));
            this.VDirZ.Dock = System.Windows.Forms.DockStyle.Top;
            this.VDirZ.Location = new System.Drawing.Point(0, 378);
            this.VDirZ.Margin = new System.Windows.Forms.Padding(0);
            this.VDirZ.MaximumValue = 1F;
            this.VDirZ.MinimumValue = -1F;
            this.VDirZ.Name = "VDirZ";
            this.VDirZ.Size = new System.Drawing.Size(254, 22);
            this.VDirZ.TabIndex = 45;
            this.VDirZ.Value = 0F;
            this.VDirZ.ValueChanged += new System.EventHandler(this.VDirZ_ValueChanged);
            // 
            // VDirY
            // 
            this.VDirY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.VDirY.DecimalPlaces = ((uint)(1u));
            this.VDirY.Dock = System.Windows.Forms.DockStyle.Top;
            this.VDirY.Location = new System.Drawing.Point(0, 356);
            this.VDirY.Margin = new System.Windows.Forms.Padding(0);
            this.VDirY.MaximumValue = 1F;
            this.VDirY.MinimumValue = -1F;
            this.VDirY.Name = "VDirY";
            this.VDirY.Size = new System.Drawing.Size(254, 22);
            this.VDirY.TabIndex = 44;
            this.VDirY.Value = 0F;
            this.VDirY.ValueChanged += new System.EventHandler(this.VDirY_ValueChanged);
            // 
            // VDirX
            // 
            this.VDirX.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.VDirX.DecimalPlaces = ((uint)(1u));
            this.VDirX.Dock = System.Windows.Forms.DockStyle.Top;
            this.VDirX.Location = new System.Drawing.Point(0, 334);
            this.VDirX.Margin = new System.Windows.Forms.Padding(0);
            this.VDirX.MaximumValue = 1F;
            this.VDirX.MinimumValue = -1F;
            this.VDirX.Name = "VDirX";
            this.VDirX.Size = new System.Drawing.Size(254, 22);
            this.VDirX.TabIndex = 43;
            this.VDirX.Value = 0F;
            this.VDirX.ValueChanged += new System.EventHandler(this.VDirX_ValueChanged);
            // 
            // LblVDirection
            // 
            this.LblVDirection.AutomaticSize = false;
            this.LblVDirection.Centered = false;
            this.LblVDirection.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblVDirection.Location = new System.Drawing.Point(0, 321);
            this.LblVDirection.Name = "LblVDirection";
            this.LblVDirection.Size = new System.Drawing.Size(254, 13);
            this.LblVDirection.TabIndex = 42;
            this.LblVDirection.Text = "Direction (X/Y/Z):";
            // 
            // VPosZ
            // 
            this.VPosZ.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.VPosZ.DecimalPlaces = ((uint)(1u));
            this.VPosZ.Dock = System.Windows.Forms.DockStyle.Top;
            this.VPosZ.Enabled = false;
            this.VPosZ.Location = new System.Drawing.Point(0, 299);
            this.VPosZ.Margin = new System.Windows.Forms.Padding(0);
            this.VPosZ.MaximumValue = 100F;
            this.VPosZ.MinimumValue = -100F;
            this.VPosZ.Name = "VPosZ";
            this.VPosZ.Size = new System.Drawing.Size(254, 22);
            this.VPosZ.TabIndex = 63;
            this.VPosZ.Value = 0F;
            this.VPosZ.ValueChanged += new System.EventHandler(this.VPosZ_ValueChanged);
            // 
            // VPosY
            // 
            this.VPosY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.VPosY.DecimalPlaces = ((uint)(1u));
            this.VPosY.Dock = System.Windows.Forms.DockStyle.Top;
            this.VPosY.Enabled = false;
            this.VPosY.Location = new System.Drawing.Point(0, 277);
            this.VPosY.Margin = new System.Windows.Forms.Padding(0);
            this.VPosY.MaximumValue = 100F;
            this.VPosY.MinimumValue = -100F;
            this.VPosY.Name = "VPosY";
            this.VPosY.Size = new System.Drawing.Size(254, 22);
            this.VPosY.TabIndex = 62;
            this.VPosY.Value = 0F;
            this.VPosY.ValueChanged += new System.EventHandler(this.VPosY_ValueChanged);
            // 
            // VPosX
            // 
            this.VPosX.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.VPosX.DecimalPlaces = ((uint)(1u));
            this.VPosX.Dock = System.Windows.Forms.DockStyle.Top;
            this.VPosX.Enabled = false;
            this.VPosX.Location = new System.Drawing.Point(0, 255);
            this.VPosX.Margin = new System.Windows.Forms.Padding(0);
            this.VPosX.MaximumValue = 100F;
            this.VPosX.MinimumValue = -100F;
            this.VPosX.Name = "VPosX";
            this.VPosX.Size = new System.Drawing.Size(254, 22);
            this.VPosX.TabIndex = 61;
            this.VPosX.Value = 0F;
            this.VPosX.ValueChanged += new System.EventHandler(this.VPosX_ValueChanged);
            // 
            // LblVPosition
            // 
            this.LblVPosition.AutomaticSize = false;
            this.LblVPosition.Centered = false;
            this.LblVPosition.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblVPosition.Enabled = false;
            this.LblVPosition.Location = new System.Drawing.Point(0, 242);
            this.LblVPosition.Name = "LblVPosition";
            this.LblVPosition.Size = new System.Drawing.Size(254, 13);
            this.LblVPosition.TabIndex = 64;
            this.LblVPosition.Text = "Position (X/Y/Z):";
            // 
            // VLightTypePanel
            // 
            this.VLightTypePanel.ColumnCount = 3;
            this.VLightTypePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.VLightTypePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.VLightTypePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.VLightTypePanel.Controls.Add(this.VRadioPoint, 0, 0);
            this.VLightTypePanel.Controls.Add(this.VRadioSpot, 0, 0);
            this.VLightTypePanel.Controls.Add(this.VRadioDirectional, 0, 0);
            this.VLightTypePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.VLightTypePanel.Location = new System.Drawing.Point(0, 218);
            this.VLightTypePanel.Name = "VLightTypePanel";
            this.VLightTypePanel.RowCount = 1;
            this.VLightTypePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.VLightTypePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.VLightTypePanel.Size = new System.Drawing.Size(254, 24);
            this.VLightTypePanel.TabIndex = 65;
            // 
            // VRadioPoint
            // 
            this.VRadioPoint.AutoSize = true;
            this.VRadioPoint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.VRadioPoint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VRadioPoint.ForeColor = System.Drawing.Color.White;
            this.VRadioPoint.Location = new System.Drawing.Point(169, 1);
            this.VRadioPoint.Margin = new System.Windows.Forms.Padding(1);
            this.VRadioPoint.Name = "VRadioPoint";
            this.VRadioPoint.Size = new System.Drawing.Size(84, 22);
            this.VRadioPoint.TabIndex = 7;
            this.VRadioPoint.Text = "Point";
            this.VRadioPoint.UseVisualStyleBackColor = false;
            this.VRadioPoint.CheckedChanged += new System.EventHandler(this.VRadioPoint_CheckedChanged);
            // 
            // VRadioSpot
            // 
            this.VRadioSpot.AutoSize = true;
            this.VRadioSpot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.VRadioSpot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VRadioSpot.ForeColor = System.Drawing.Color.White;
            this.VRadioSpot.Location = new System.Drawing.Point(85, 1);
            this.VRadioSpot.Margin = new System.Windows.Forms.Padding(1);
            this.VRadioSpot.Name = "VRadioSpot";
            this.VRadioSpot.Size = new System.Drawing.Size(82, 22);
            this.VRadioSpot.TabIndex = 5;
            this.VRadioSpot.Text = "Spot";
            this.VRadioSpot.UseVisualStyleBackColor = false;
            this.VRadioSpot.CheckedChanged += new System.EventHandler(this.VRadioSpot_CheckedChanged);
            // 
            // VRadioDirectional
            // 
            this.VRadioDirectional.AutoSize = true;
            this.VRadioDirectional.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.VRadioDirectional.Checked = true;
            this.VRadioDirectional.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VRadioDirectional.ForeColor = System.Drawing.Color.White;
            this.VRadioDirectional.Location = new System.Drawing.Point(1, 1);
            this.VRadioDirectional.Margin = new System.Windows.Forms.Padding(1);
            this.VRadioDirectional.Name = "VRadioDirectional";
            this.VRadioDirectional.Size = new System.Drawing.Size(82, 22);
            this.VRadioDirectional.TabIndex = 4;
            this.VRadioDirectional.TabStop = true;
            this.VRadioDirectional.Text = "Directional";
            this.VRadioDirectional.UseVisualStyleBackColor = false;
            this.VRadioDirectional.CheckedChanged += new System.EventHandler(this.VRadioDirectional_CheckedChanged);
            // 
            // VDiffuseColor
            // 
            this.VDiffuseColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.VDiffuseColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.VDiffuseColor.Dock = System.Windows.Forms.DockStyle.Top;
            this.VDiffuseColor.ForeColor = System.Drawing.Color.White;
            this.VDiffuseColor.Location = new System.Drawing.Point(0, 130);
            this.VDiffuseColor.Margin = new System.Windows.Forms.Padding(0);
            this.VDiffuseColor.Name = "VDiffuseColor";
            this.VDiffuseColor.Size = new System.Drawing.Size(254, 88);
            this.VDiffuseColor.TabIndex = 37;
            this.VDiffuseColor.ColorChanged += new System.EventHandler(this.VDiffuseColor_ColorChanged);
            // 
            // LblVDiffuseColor
            // 
            this.LblVDiffuseColor.AutomaticSize = false;
            this.LblVDiffuseColor.Centered = false;
            this.LblVDiffuseColor.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblVDiffuseColor.Location = new System.Drawing.Point(0, 117);
            this.LblVDiffuseColor.Name = "LblVDiffuseColor";
            this.LblVDiffuseColor.Size = new System.Drawing.Size(254, 13);
            this.LblVDiffuseColor.TabIndex = 36;
            this.LblVDiffuseColor.Text = "Diffuse color:";
            // 
            // VAmbientColor
            // 
            this.VAmbientColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.VAmbientColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.VAmbientColor.Dock = System.Windows.Forms.DockStyle.Top;
            this.VAmbientColor.ForeColor = System.Drawing.Color.White;
            this.VAmbientColor.Location = new System.Drawing.Point(0, 29);
            this.VAmbientColor.Margin = new System.Windows.Forms.Padding(0);
            this.VAmbientColor.Name = "VAmbientColor";
            this.VAmbientColor.Size = new System.Drawing.Size(254, 88);
            this.VAmbientColor.TabIndex = 35;
            this.VAmbientColor.ColorChanged += new System.EventHandler(this.VAmbientColor_ColorChanged);
            // 
            // LblVAmbientColor
            // 
            this.LblVAmbientColor.AutomaticSize = false;
            this.LblVAmbientColor.Centered = false;
            this.LblVAmbientColor.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblVAmbientColor.Location = new System.Drawing.Point(0, 16);
            this.LblVAmbientColor.Name = "LblVAmbientColor";
            this.LblVAmbientColor.Size = new System.Drawing.Size(254, 13);
            this.LblVAmbientColor.TabIndex = 34;
            this.LblVAmbientColor.Text = "Ambient color:";
            // 
            // VLightEnabled
            // 
            this.VLightEnabled.BoxColor = System.Drawing.Color.Black;
            this.VLightEnabled.Checked = false;
            this.VLightEnabled.Dock = System.Windows.Forms.DockStyle.Top;
            this.VLightEnabled.Location = new System.Drawing.Point(0, 0);
            this.VLightEnabled.Name = "VLightEnabled";
            this.VLightEnabled.Size = new System.Drawing.Size(254, 16);
            this.VLightEnabled.TabIndex = 66;
            this.VLightEnabled.Text = "Light enabled";
            this.VLightEnabled.Click += new System.EventHandler(this.VLightEnabled_Click);
            // 
            // FragmentLightPanel
            // 
            this.FragmentLightPanel.AutoSize = true;
            this.FragmentLightPanel.Controls.Add(this.FDistanceAttGroup);
            this.FragmentLightPanel.Controls.Add(this.FAngularAttGroup);
            this.FragmentLightPanel.Controls.Add(this.FDirZ);
            this.FragmentLightPanel.Controls.Add(this.FDirY);
            this.FragmentLightPanel.Controls.Add(this.FDirX);
            this.FragmentLightPanel.Controls.Add(this.LblFDirection);
            this.FragmentLightPanel.Controls.Add(this.FPosZ);
            this.FragmentLightPanel.Controls.Add(this.FPosY);
            this.FragmentLightPanel.Controls.Add(this.FPosX);
            this.FragmentLightPanel.Controls.Add(this.LblFPosition);
            this.FragmentLightPanel.Controls.Add(this.FLightTypePanel);
            this.FragmentLightPanel.Controls.Add(this.Spec1Color);
            this.FragmentLightPanel.Controls.Add(this.LblSpec1);
            this.FragmentLightPanel.Controls.Add(this.Spec0Color);
            this.FragmentLightPanel.Controls.Add(this.LblSpec0);
            this.FragmentLightPanel.Controls.Add(this.FTwoSidedDiffuse);
            this.FragmentLightPanel.Controls.Add(this.FDiffuseColor);
            this.FragmentLightPanel.Controls.Add(this.LblFDiffuseColor);
            this.FragmentLightPanel.Controls.Add(this.FAmbientColor);
            this.FragmentLightPanel.Controls.Add(this.LblFAmbientColor);
            this.FragmentLightPanel.Controls.Add(this.FLightEnabled);
            this.FragmentLightPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.FragmentLightPanel.Location = new System.Drawing.Point(0, 24);
            this.FragmentLightPanel.Name = "FragmentLightPanel";
            this.FragmentLightPanel.Size = new System.Drawing.Size(254, 878);
            this.FragmentLightPanel.TabIndex = 34;
            // 
            // FDistanceAttGroup
            // 
            this.FDistanceAttGroup.AutomaticSize = true;
            this.FDistanceAttGroup.BackColor = System.Drawing.Color.Black;
            this.FDistanceAttGroup.Collapsed = false;
            // 
            // FDistanceAttGroup.ContentArea
            // 
            this.FDistanceAttGroup.ContentArea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.FDistanceAttGroup.ContentArea.Controls.Add(this.AttEnd);
            this.FDistanceAttGroup.ContentArea.Controls.Add(this.LblAttEnd);
            this.FDistanceAttGroup.ContentArea.Controls.Add(this.AttStart);
            this.FDistanceAttGroup.ContentArea.Controls.Add(this.LblAttStart);
            this.FDistanceAttGroup.ContentArea.Controls.Add(this.TxtDistanceAttLUT);
            this.FDistanceAttGroup.ContentArea.Controls.Add(this.LblDALUT);
            this.FDistanceAttGroup.ContentArea.Controls.Add(this.FAttEnabled);
            this.FDistanceAttGroup.ContentArea.Location = new System.Drawing.Point(1, 16);
            this.FDistanceAttGroup.ContentArea.Name = "ContentArea";
            this.FDistanceAttGroup.ContentArea.Size = new System.Drawing.Size(252, 121);
            this.FDistanceAttGroup.ContentArea.TabIndex = 0;
            this.FDistanceAttGroup.ContentColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.FDistanceAttGroup.Dock = System.Windows.Forms.DockStyle.Top;
            this.FDistanceAttGroup.Enabled = false;
            this.FDistanceAttGroup.ExpandedHeight = 121;
            this.FDistanceAttGroup.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FDistanceAttGroup.Location = new System.Drawing.Point(0, 740);
            this.FDistanceAttGroup.Name = "FDistanceAttGroup";
            this.FDistanceAttGroup.Size = new System.Drawing.Size(254, 138);
            this.FDistanceAttGroup.TabIndex = 54;
            this.FDistanceAttGroup.Title = "Distance Attenuation";
            // 
            // AttEnd
            // 
            this.AttEnd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.AttEnd.DecimalPlaces = ((uint)(0u));
            this.AttEnd.Dock = System.Windows.Forms.DockStyle.Top;
            this.AttEnd.Location = new System.Drawing.Point(0, 99);
            this.AttEnd.Margin = new System.Windows.Forms.Padding(0);
            this.AttEnd.MaximumValue = 100F;
            this.AttEnd.MinimumValue = 0F;
            this.AttEnd.Name = "AttEnd";
            this.AttEnd.Size = new System.Drawing.Size(252, 22);
            this.AttEnd.TabIndex = 53;
            this.AttEnd.Value = 0F;
            this.AttEnd.ValueChanged += new System.EventHandler(this.AttEnd_ValueChanged);
            // 
            // LblAttEnd
            // 
            this.LblAttEnd.AutomaticSize = false;
            this.LblAttEnd.Centered = false;
            this.LblAttEnd.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblAttEnd.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblAttEnd.Location = new System.Drawing.Point(0, 86);
            this.LblAttEnd.Name = "LblAttEnd";
            this.LblAttEnd.Size = new System.Drawing.Size(252, 13);
            this.LblAttEnd.TabIndex = 52;
            this.LblAttEnd.Text = "Attenuation end:";
            // 
            // AttStart
            // 
            this.AttStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.AttStart.DecimalPlaces = ((uint)(0u));
            this.AttStart.Dock = System.Windows.Forms.DockStyle.Top;
            this.AttStart.Location = new System.Drawing.Point(0, 64);
            this.AttStart.Margin = new System.Windows.Forms.Padding(0);
            this.AttStart.MaximumValue = 100F;
            this.AttStart.MinimumValue = 0F;
            this.AttStart.Name = "AttStart";
            this.AttStart.Size = new System.Drawing.Size(252, 22);
            this.AttStart.TabIndex = 54;
            this.AttStart.Value = 0F;
            this.AttStart.ValueChanged += new System.EventHandler(this.AttStart_ValueChanged);
            // 
            // LblAttStart
            // 
            this.LblAttStart.AutomaticSize = false;
            this.LblAttStart.Centered = false;
            this.LblAttStart.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblAttStart.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblAttStart.Location = new System.Drawing.Point(0, 51);
            this.LblAttStart.Name = "LblAttStart";
            this.LblAttStart.Size = new System.Drawing.Size(252, 13);
            this.LblAttStart.TabIndex = 49;
            this.LblAttStart.Text = "Attenuation start:";
            // 
            // TxtDistanceAttLUT
            // 
            this.TxtDistanceAttLUT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.TxtDistanceAttLUT.CharacterWhiteList = null;
            this.TxtDistanceAttLUT.Dock = System.Windows.Forms.DockStyle.Top;
            this.TxtDistanceAttLUT.Location = new System.Drawing.Point(0, 29);
            this.TxtDistanceAttLUT.Name = "TxtDistanceAttLUT";
            this.TxtDistanceAttLUT.Size = new System.Drawing.Size(252, 22);
            this.TxtDistanceAttLUT.TabIndex = 51;
            this.TxtDistanceAttLUT.ChangedText += new System.EventHandler(this.TxtDistanceAttLUT_ChangedText);
            // 
            // LblDALUT
            // 
            this.LblDALUT.AutomaticSize = false;
            this.LblDALUT.Centered = false;
            this.LblDALUT.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblDALUT.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblDALUT.Location = new System.Drawing.Point(0, 16);
            this.LblDALUT.Name = "LblDALUT";
            this.LblDALUT.Size = new System.Drawing.Size(252, 13);
            this.LblDALUT.TabIndex = 50;
            this.LblDALUT.Text = "LookUp Table name:";
            // 
            // FAttEnabled
            // 
            this.FAttEnabled.BoxColor = System.Drawing.Color.Black;
            this.FAttEnabled.Checked = false;
            this.FAttEnabled.Dock = System.Windows.Forms.DockStyle.Top;
            this.FAttEnabled.Location = new System.Drawing.Point(0, 0);
            this.FAttEnabled.Name = "FAttEnabled";
            this.FAttEnabled.Size = new System.Drawing.Size(252, 16);
            this.FAttEnabled.TabIndex = 55;
            this.FAttEnabled.Text = "Attenuation enabled";
            this.FAttEnabled.CheckedChanged += new System.EventHandler(this.FAttEnabled_CheckedChanged);
            // 
            // FAngularAttGroup
            // 
            this.FAngularAttGroup.AutomaticSize = true;
            this.FAngularAttGroup.BackColor = System.Drawing.Color.Black;
            this.FAngularAttGroup.Collapsed = false;
            // 
            // FAngularAttGroup.ContentArea
            // 
            this.FAngularAttGroup.ContentArea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.FAngularAttGroup.ContentArea.Controls.Add(this.InputScale);
            this.FAngularAttGroup.ContentArea.Controls.Add(this.LblInputScale);
            this.FAngularAttGroup.ContentArea.Controls.Add(this.InputAngle);
            this.FAngularAttGroup.ContentArea.Controls.Add(this.LblInputAngle);
            this.FAngularAttGroup.ContentArea.Controls.Add(this.TxtAngularAttLUT);
            this.FAngularAttGroup.ContentArea.Controls.Add(this.LblAALUT);
            this.FAngularAttGroup.ContentArea.Location = new System.Drawing.Point(1, 16);
            this.FAngularAttGroup.ContentArea.Name = "ContentArea";
            this.FAngularAttGroup.ContentArea.Size = new System.Drawing.Size(252, 105);
            this.FAngularAttGroup.ContentArea.TabIndex = 0;
            this.FAngularAttGroup.ContentColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.FAngularAttGroup.Dock = System.Windows.Forms.DockStyle.Top;
            this.FAngularAttGroup.Enabled = false;
            this.FAngularAttGroup.ExpandedHeight = 105;
            this.FAngularAttGroup.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FAngularAttGroup.Location = new System.Drawing.Point(0, 618);
            this.FAngularAttGroup.Name = "FAngularAttGroup";
            this.FAngularAttGroup.Size = new System.Drawing.Size(254, 122);
            this.FAngularAttGroup.TabIndex = 48;
            this.FAngularAttGroup.Title = "Angular Attenuation";
            // 
            // InputScale
            // 
            this.InputScale.AutomaticSize = true;
            this.InputScale.BackColor = System.Drawing.Color.Transparent;
            this.InputScale.BarFont = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InputScale.BarHeight = 22;
            this.InputScale.Dock = System.Windows.Forms.DockStyle.Top;
            this.InputScale.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InputScale.ForeColor = System.Drawing.Color.White;
            this.InputScale.Items = new string[] {
        "1",
        "2",
        "4",
        "8",
        "0.25 (Quarter)",
        "0.5 (Half)"};
            this.InputScale.ListFont = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InputScale.ListHeight = 256;
            this.InputScale.Location = new System.Drawing.Point(0, 83);
            this.InputScale.Name = "InputScale";
            this.InputScale.SelectedIndex = 0;
            this.InputScale.Size = new System.Drawing.Size(252, 22);
            this.InputScale.TabIndex = 61;
            this.InputScale.SelectedIndexChanged += new System.EventHandler(this.InputScale_SelectedIndexChanged);
            // 
            // LblInputScale
            // 
            this.LblInputScale.AutomaticSize = false;
            this.LblInputScale.Centered = false;
            this.LblInputScale.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblInputScale.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblInputScale.Location = new System.Drawing.Point(0, 70);
            this.LblInputScale.Name = "LblInputScale";
            this.LblInputScale.Size = new System.Drawing.Size(252, 13);
            this.LblInputScale.TabIndex = 59;
            this.LblInputScale.Text = "Input angle scale:";
            // 
            // InputAngle
            // 
            this.InputAngle.AutomaticSize = true;
            this.InputAngle.BackColor = System.Drawing.Color.Transparent;
            this.InputAngle.BarFont = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InputAngle.BarHeight = 22;
            this.InputAngle.Dock = System.Windows.Forms.DockStyle.Top;
            this.InputAngle.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InputAngle.ForeColor = System.Drawing.Color.White;
            this.InputAngle.Items = new string[] {
        "N·H (Normal, Half-angle)",
        "V·H (View, Half-angle)",
        "N·V (Normal, View)",
        "L·N (Light, Normal)",
        "-L·P (Inverse light, Spotlight)",
        "cos(ϕ)"};
            this.InputAngle.ListFont = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InputAngle.ListHeight = 256;
            this.InputAngle.Location = new System.Drawing.Point(0, 48);
            this.InputAngle.Name = "InputAngle";
            this.InputAngle.SelectedIndex = 0;
            this.InputAngle.Size = new System.Drawing.Size(252, 22);
            this.InputAngle.TabIndex = 55;
            this.InputAngle.SelectedIndexChanged += new System.EventHandler(this.InputAngle_SelectedIndexChanged);
            // 
            // LblInputAngle
            // 
            this.LblInputAngle.AutomaticSize = false;
            this.LblInputAngle.Centered = false;
            this.LblInputAngle.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblInputAngle.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblInputAngle.Location = new System.Drawing.Point(0, 35);
            this.LblInputAngle.Name = "LblInputAngle";
            this.LblInputAngle.Size = new System.Drawing.Size(252, 13);
            this.LblInputAngle.TabIndex = 56;
            this.LblInputAngle.Text = "Input angle:";
            // 
            // TxtAngularAttLUT
            // 
            this.TxtAngularAttLUT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.TxtAngularAttLUT.CharacterWhiteList = null;
            this.TxtAngularAttLUT.Dock = System.Windows.Forms.DockStyle.Top;
            this.TxtAngularAttLUT.Location = new System.Drawing.Point(0, 13);
            this.TxtAngularAttLUT.Name = "TxtAngularAttLUT";
            this.TxtAngularAttLUT.Size = new System.Drawing.Size(252, 22);
            this.TxtAngularAttLUT.TabIndex = 58;
            this.TxtAngularAttLUT.ChangedText += new System.EventHandler(this.TxtAngularAttLUT_ChangedText);
            // 
            // LblAALUT
            // 
            this.LblAALUT.AutomaticSize = false;
            this.LblAALUT.Centered = false;
            this.LblAALUT.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblAALUT.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblAALUT.Location = new System.Drawing.Point(0, 0);
            this.LblAALUT.Name = "LblAALUT";
            this.LblAALUT.Size = new System.Drawing.Size(252, 13);
            this.LblAALUT.TabIndex = 57;
            this.LblAALUT.Text = "LookUp Table name:";
            // 
            // FDirZ
            // 
            this.FDirZ.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.FDirZ.DecimalPlaces = ((uint)(1u));
            this.FDirZ.Dock = System.Windows.Forms.DockStyle.Top;
            this.FDirZ.Location = new System.Drawing.Point(0, 596);
            this.FDirZ.Margin = new System.Windows.Forms.Padding(0);
            this.FDirZ.MaximumValue = 1F;
            this.FDirZ.MinimumValue = -1F;
            this.FDirZ.Name = "FDirZ";
            this.FDirZ.Size = new System.Drawing.Size(254, 22);
            this.FDirZ.TabIndex = 45;
            this.FDirZ.Value = 0F;
            this.FDirZ.ValueChanged += new System.EventHandler(this.FDirZ_ValueChanged);
            // 
            // FDirY
            // 
            this.FDirY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.FDirY.DecimalPlaces = ((uint)(1u));
            this.FDirY.Dock = System.Windows.Forms.DockStyle.Top;
            this.FDirY.Location = new System.Drawing.Point(0, 574);
            this.FDirY.Margin = new System.Windows.Forms.Padding(0);
            this.FDirY.MaximumValue = 1F;
            this.FDirY.MinimumValue = -1F;
            this.FDirY.Name = "FDirY";
            this.FDirY.Size = new System.Drawing.Size(254, 22);
            this.FDirY.TabIndex = 44;
            this.FDirY.Value = 0F;
            this.FDirY.ValueChanged += new System.EventHandler(this.FDirY_ValueChanged);
            // 
            // FDirX
            // 
            this.FDirX.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.FDirX.DecimalPlaces = ((uint)(1u));
            this.FDirX.Dock = System.Windows.Forms.DockStyle.Top;
            this.FDirX.Location = new System.Drawing.Point(0, 552);
            this.FDirX.Margin = new System.Windows.Forms.Padding(0);
            this.FDirX.MaximumValue = 1F;
            this.FDirX.MinimumValue = -1F;
            this.FDirX.Name = "FDirX";
            this.FDirX.Size = new System.Drawing.Size(254, 22);
            this.FDirX.TabIndex = 43;
            this.FDirX.Value = 0F;
            this.FDirX.ValueChanged += new System.EventHandler(this.FDirX_ValueChanged);
            // 
            // LblFDirection
            // 
            this.LblFDirection.AutomaticSize = false;
            this.LblFDirection.Centered = false;
            this.LblFDirection.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblFDirection.Location = new System.Drawing.Point(0, 539);
            this.LblFDirection.Name = "LblFDirection";
            this.LblFDirection.Size = new System.Drawing.Size(254, 13);
            this.LblFDirection.TabIndex = 42;
            this.LblFDirection.Text = "Direction (X/Y/Z):";
            // 
            // FPosZ
            // 
            this.FPosZ.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.FPosZ.DecimalPlaces = ((uint)(1u));
            this.FPosZ.Dock = System.Windows.Forms.DockStyle.Top;
            this.FPosZ.Enabled = false;
            this.FPosZ.Location = new System.Drawing.Point(0, 517);
            this.FPosZ.Margin = new System.Windows.Forms.Padding(0);
            this.FPosZ.MaximumValue = 100F;
            this.FPosZ.MinimumValue = -100F;
            this.FPosZ.Name = "FPosZ";
            this.FPosZ.Size = new System.Drawing.Size(254, 22);
            this.FPosZ.TabIndex = 63;
            this.FPosZ.Value = 0F;
            this.FPosZ.ValueChanged += new System.EventHandler(this.FPosZ_ValueChanged);
            // 
            // FPosY
            // 
            this.FPosY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.FPosY.DecimalPlaces = ((uint)(1u));
            this.FPosY.Dock = System.Windows.Forms.DockStyle.Top;
            this.FPosY.Enabled = false;
            this.FPosY.Location = new System.Drawing.Point(0, 495);
            this.FPosY.Margin = new System.Windows.Forms.Padding(0);
            this.FPosY.MaximumValue = 100F;
            this.FPosY.MinimumValue = -100F;
            this.FPosY.Name = "FPosY";
            this.FPosY.Size = new System.Drawing.Size(254, 22);
            this.FPosY.TabIndex = 62;
            this.FPosY.Value = 0F;
            this.FPosY.ValueChanged += new System.EventHandler(this.FPosY_ValueChanged);
            // 
            // FPosX
            // 
            this.FPosX.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.FPosX.DecimalPlaces = ((uint)(1u));
            this.FPosX.Dock = System.Windows.Forms.DockStyle.Top;
            this.FPosX.Enabled = false;
            this.FPosX.Location = new System.Drawing.Point(0, 473);
            this.FPosX.Margin = new System.Windows.Forms.Padding(0);
            this.FPosX.MaximumValue = 100F;
            this.FPosX.MinimumValue = -100F;
            this.FPosX.Name = "FPosX";
            this.FPosX.Size = new System.Drawing.Size(254, 22);
            this.FPosX.TabIndex = 61;
            this.FPosX.Value = 0F;
            this.FPosX.ValueChanged += new System.EventHandler(this.FPosX_ValueChanged);
            // 
            // LblFPosition
            // 
            this.LblFPosition.AutomaticSize = false;
            this.LblFPosition.Centered = false;
            this.LblFPosition.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblFPosition.Enabled = false;
            this.LblFPosition.Location = new System.Drawing.Point(0, 460);
            this.LblFPosition.Name = "LblFPosition";
            this.LblFPosition.Size = new System.Drawing.Size(254, 13);
            this.LblFPosition.TabIndex = 64;
            this.LblFPosition.Text = "Position (X/Y/Z):";
            // 
            // FLightTypePanel
            // 
            this.FLightTypePanel.ColumnCount = 3;
            this.FLightTypePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.FLightTypePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.FLightTypePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.FLightTypePanel.Controls.Add(this.FRadioPoint, 0, 0);
            this.FLightTypePanel.Controls.Add(this.FRadioSpot, 0, 0);
            this.FLightTypePanel.Controls.Add(this.FRadioDirectional, 0, 0);
            this.FLightTypePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.FLightTypePanel.Location = new System.Drawing.Point(0, 436);
            this.FLightTypePanel.Name = "FLightTypePanel";
            this.FLightTypePanel.RowCount = 1;
            this.FLightTypePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.FLightTypePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.FLightTypePanel.Size = new System.Drawing.Size(254, 24);
            this.FLightTypePanel.TabIndex = 65;
            // 
            // FRadioPoint
            // 
            this.FRadioPoint.AutoSize = true;
            this.FRadioPoint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.FRadioPoint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FRadioPoint.ForeColor = System.Drawing.Color.White;
            this.FRadioPoint.Location = new System.Drawing.Point(169, 1);
            this.FRadioPoint.Margin = new System.Windows.Forms.Padding(1);
            this.FRadioPoint.Name = "FRadioPoint";
            this.FRadioPoint.Size = new System.Drawing.Size(84, 22);
            this.FRadioPoint.TabIndex = 7;
            this.FRadioPoint.Text = "Point";
            this.FRadioPoint.UseVisualStyleBackColor = false;
            this.FRadioPoint.CheckedChanged += new System.EventHandler(this.RadioPoint_CheckedChanged);
            // 
            // FRadioSpot
            // 
            this.FRadioSpot.AutoSize = true;
            this.FRadioSpot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.FRadioSpot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FRadioSpot.ForeColor = System.Drawing.Color.White;
            this.FRadioSpot.Location = new System.Drawing.Point(85, 1);
            this.FRadioSpot.Margin = new System.Windows.Forms.Padding(1);
            this.FRadioSpot.Name = "FRadioSpot";
            this.FRadioSpot.Size = new System.Drawing.Size(82, 22);
            this.FRadioSpot.TabIndex = 5;
            this.FRadioSpot.Text = "Spot";
            this.FRadioSpot.UseVisualStyleBackColor = false;
            this.FRadioSpot.CheckedChanged += new System.EventHandler(this.RadioSpot_CheckedChanged);
            // 
            // FRadioDirectional
            // 
            this.FRadioDirectional.AutoSize = true;
            this.FRadioDirectional.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.FRadioDirectional.Checked = true;
            this.FRadioDirectional.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FRadioDirectional.ForeColor = System.Drawing.Color.White;
            this.FRadioDirectional.Location = new System.Drawing.Point(1, 1);
            this.FRadioDirectional.Margin = new System.Windows.Forms.Padding(1);
            this.FRadioDirectional.Name = "FRadioDirectional";
            this.FRadioDirectional.Size = new System.Drawing.Size(82, 22);
            this.FRadioDirectional.TabIndex = 4;
            this.FRadioDirectional.TabStop = true;
            this.FRadioDirectional.Text = "Directional";
            this.FRadioDirectional.UseVisualStyleBackColor = false;
            this.FRadioDirectional.CheckedChanged += new System.EventHandler(this.RadioDirectional_CheckedChanged);
            // 
            // Spec1Color
            // 
            this.Spec1Color.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.Spec1Color.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.Spec1Color.Dock = System.Windows.Forms.DockStyle.Top;
            this.Spec1Color.ForeColor = System.Drawing.Color.White;
            this.Spec1Color.Location = new System.Drawing.Point(0, 348);
            this.Spec1Color.Margin = new System.Windows.Forms.Padding(0);
            this.Spec1Color.Name = "Spec1Color";
            this.Spec1Color.Size = new System.Drawing.Size(254, 88);
            this.Spec1Color.TabIndex = 41;
            this.Spec1Color.Load += new System.EventHandler(this.Spec1Color_Load);
            // 
            // LblSpec1
            // 
            this.LblSpec1.AutomaticSize = false;
            this.LblSpec1.Centered = false;
            this.LblSpec1.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblSpec1.Location = new System.Drawing.Point(0, 335);
            this.LblSpec1.Name = "LblSpec1";
            this.LblSpec1.Size = new System.Drawing.Size(254, 13);
            this.LblSpec1.TabIndex = 40;
            this.LblSpec1.Text = "Specular 1 color:";
            // 
            // Spec0Color
            // 
            this.Spec0Color.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.Spec0Color.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.Spec0Color.Dock = System.Windows.Forms.DockStyle.Top;
            this.Spec0Color.ForeColor = System.Drawing.Color.White;
            this.Spec0Color.Location = new System.Drawing.Point(0, 247);
            this.Spec0Color.Margin = new System.Windows.Forms.Padding(0);
            this.Spec0Color.Name = "Spec0Color";
            this.Spec0Color.Size = new System.Drawing.Size(254, 88);
            this.Spec0Color.TabIndex = 39;
            this.Spec0Color.Load += new System.EventHandler(this.Spec0Color_Load);
            // 
            // LblSpec0
            // 
            this.LblSpec0.AutomaticSize = false;
            this.LblSpec0.Centered = false;
            this.LblSpec0.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblSpec0.Location = new System.Drawing.Point(0, 234);
            this.LblSpec0.Name = "LblSpec0";
            this.LblSpec0.Size = new System.Drawing.Size(254, 13);
            this.LblSpec0.TabIndex = 38;
            this.LblSpec0.Text = "Specular 0 color:";
            // 
            // FTwoSidedDiffuse
            // 
            this.FTwoSidedDiffuse.BoxColor = System.Drawing.Color.Black;
            this.FTwoSidedDiffuse.Checked = false;
            this.FTwoSidedDiffuse.Dock = System.Windows.Forms.DockStyle.Top;
            this.FTwoSidedDiffuse.Location = new System.Drawing.Point(0, 218);
            this.FTwoSidedDiffuse.Name = "FTwoSidedDiffuse";
            this.FTwoSidedDiffuse.Size = new System.Drawing.Size(254, 16);
            this.FTwoSidedDiffuse.TabIndex = 67;
            this.FTwoSidedDiffuse.Text = "Use two-sided diffuse";
            this.FTwoSidedDiffuse.CheckedChanged += new System.EventHandler(this.FTwoSidedDiffuse_CheckedChanged);
            // 
            // FDiffuseColor
            // 
            this.FDiffuseColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.FDiffuseColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.FDiffuseColor.Dock = System.Windows.Forms.DockStyle.Top;
            this.FDiffuseColor.ForeColor = System.Drawing.Color.White;
            this.FDiffuseColor.Location = new System.Drawing.Point(0, 130);
            this.FDiffuseColor.Margin = new System.Windows.Forms.Padding(0);
            this.FDiffuseColor.Name = "FDiffuseColor";
            this.FDiffuseColor.Size = new System.Drawing.Size(254, 88);
            this.FDiffuseColor.TabIndex = 37;
            this.FDiffuseColor.ColorChanged += new System.EventHandler(this.FDiffuseColor_ColorChanged);
            // 
            // LblFDiffuseColor
            // 
            this.LblFDiffuseColor.AutomaticSize = false;
            this.LblFDiffuseColor.Centered = false;
            this.LblFDiffuseColor.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblFDiffuseColor.Location = new System.Drawing.Point(0, 117);
            this.LblFDiffuseColor.Name = "LblFDiffuseColor";
            this.LblFDiffuseColor.Size = new System.Drawing.Size(254, 13);
            this.LblFDiffuseColor.TabIndex = 36;
            this.LblFDiffuseColor.Text = "Diffuse color:";
            // 
            // FAmbientColor
            // 
            this.FAmbientColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.FAmbientColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.FAmbientColor.Dock = System.Windows.Forms.DockStyle.Top;
            this.FAmbientColor.ForeColor = System.Drawing.Color.White;
            this.FAmbientColor.Location = new System.Drawing.Point(0, 29);
            this.FAmbientColor.Margin = new System.Windows.Forms.Padding(0);
            this.FAmbientColor.Name = "FAmbientColor";
            this.FAmbientColor.Size = new System.Drawing.Size(254, 88);
            this.FAmbientColor.TabIndex = 35;
            this.FAmbientColor.ColorChanged += new System.EventHandler(this.FAmbientColor_ColorChanged);
            // 
            // LblFAmbientColor
            // 
            this.LblFAmbientColor.AutomaticSize = false;
            this.LblFAmbientColor.Centered = false;
            this.LblFAmbientColor.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblFAmbientColor.Location = new System.Drawing.Point(0, 16);
            this.LblFAmbientColor.Name = "LblFAmbientColor";
            this.LblFAmbientColor.Size = new System.Drawing.Size(254, 13);
            this.LblFAmbientColor.TabIndex = 34;
            this.LblFAmbientColor.Text = "Ambient color:";
            // 
            // FLightEnabled
            // 
            this.FLightEnabled.BoxColor = System.Drawing.Color.Black;
            this.FLightEnabled.Checked = false;
            this.FLightEnabled.Dock = System.Windows.Forms.DockStyle.Top;
            this.FLightEnabled.Location = new System.Drawing.Point(0, 0);
            this.FLightEnabled.Name = "FLightEnabled";
            this.FLightEnabled.Size = new System.Drawing.Size(254, 16);
            this.FLightEnabled.TabIndex = 66;
            this.FLightEnabled.Text = "Light enabled";
            this.FLightEnabled.CheckedChanged += new System.EventHandler(this.FLightEnabled_CheckedChanged);
            // 
            // LightUsePanel
            // 
            this.LightUsePanel.ColumnCount = 4;
            this.LightUsePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.LightUsePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.LightUsePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.LightUsePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.LightUsePanel.Controls.Add(this.RadioVertex, 0, 0);
            this.LightUsePanel.Controls.Add(this.RadioFragment, 0, 0);
            this.LightUsePanel.Controls.Add(this.RadioHemisphere, 0, 0);
            this.LightUsePanel.Controls.Add(this.RadioAmbient, 0, 0);
            this.LightUsePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.LightUsePanel.Location = new System.Drawing.Point(0, 0);
            this.LightUsePanel.Name = "LightUsePanel";
            this.LightUsePanel.RowCount = 1;
            this.LightUsePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.LightUsePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.LightUsePanel.Size = new System.Drawing.Size(254, 24);
            this.LightUsePanel.TabIndex = 25;
            // 
            // RadioVertex
            // 
            this.RadioVertex.AutoSize = true;
            this.RadioVertex.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.RadioVertex.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RadioVertex.ForeColor = System.Drawing.Color.White;
            this.RadioVertex.Location = new System.Drawing.Point(127, 1);
            this.RadioVertex.Margin = new System.Windows.Forms.Padding(1);
            this.RadioVertex.Name = "RadioVertex";
            this.RadioVertex.Size = new System.Drawing.Size(61, 22);
            this.RadioVertex.TabIndex = 7;
            this.RadioVertex.Text = "Vertex";
            this.RadioVertex.UseVisualStyleBackColor = false;
            this.RadioVertex.CheckedChanged += new System.EventHandler(this.RadioVertex_CheckedChanged);
            // 
            // RadioFragment
            // 
            this.RadioFragment.AutoSize = true;
            this.RadioFragment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.RadioFragment.Checked = true;
            this.RadioFragment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RadioFragment.ForeColor = System.Drawing.Color.White;
            this.RadioFragment.Location = new System.Drawing.Point(190, 1);
            this.RadioFragment.Margin = new System.Windows.Forms.Padding(1);
            this.RadioFragment.Name = "RadioFragment";
            this.RadioFragment.Size = new System.Drawing.Size(63, 22);
            this.RadioFragment.TabIndex = 6;
            this.RadioFragment.TabStop = true;
            this.RadioFragment.Text = "Fragment";
            this.RadioFragment.UseVisualStyleBackColor = false;
            this.RadioFragment.CheckedChanged += new System.EventHandler(this.RadioFragment_CheckedChanged);
            // 
            // RadioHemisphere
            // 
            this.RadioHemisphere.AutoSize = true;
            this.RadioHemisphere.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.RadioHemisphere.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RadioHemisphere.ForeColor = System.Drawing.Color.White;
            this.RadioHemisphere.Location = new System.Drawing.Point(1, 1);
            this.RadioHemisphere.Margin = new System.Windows.Forms.Padding(1);
            this.RadioHemisphere.Name = "RadioHemisphere";
            this.RadioHemisphere.Size = new System.Drawing.Size(61, 22);
            this.RadioHemisphere.TabIndex = 5;
            this.RadioHemisphere.Text = "Hemisphere";
            this.RadioHemisphere.UseVisualStyleBackColor = false;
            this.RadioHemisphere.CheckedChanged += new System.EventHandler(this.RadioHemisphere_CheckedChanged);
            // 
            // RadioAmbient
            // 
            this.RadioAmbient.AutoSize = true;
            this.RadioAmbient.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.RadioAmbient.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RadioAmbient.ForeColor = System.Drawing.Color.White;
            this.RadioAmbient.Location = new System.Drawing.Point(64, 1);
            this.RadioAmbient.Margin = new System.Windows.Forms.Padding(1);
            this.RadioAmbient.Name = "RadioAmbient";
            this.RadioAmbient.Size = new System.Drawing.Size(61, 22);
            this.RadioAmbient.TabIndex = 4;
            this.RadioAmbient.Text = "Ambient";
            this.RadioAmbient.UseVisualStyleBackColor = false;
            this.RadioAmbient.CheckedChanged += new System.EventHandler(this.RadioAmbient_CheckedChanged);
            // 
            // Content
            // 
            this.Content.BackColor = System.Drawing.Color.Transparent;
            // 
            // Content.ContentArea
            // 
            this.Content.ContentArea.BackColor = System.Drawing.Color.Transparent;
            this.Content.ContentArea.Controls.Add(this.LightGroup);
            this.Content.ContentArea.Controls.Add(this.NameGroup);
            this.Content.ContentArea.Controls.Add(this.LightList);
            this.Content.ContentArea.Controls.Add(this.TopControlsExtended);
            this.Content.ContentArea.Controls.Add(this.TopControls);
            this.Content.ContentArea.Location = new System.Drawing.Point(0, 0);
            this.Content.ContentArea.Name = "ContentArea";
            this.Content.ContentArea.Size = new System.Drawing.Size(256, 2299);
            this.Content.ContentArea.TabIndex = 2;
            this.Content.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Content.Location = new System.Drawing.Point(0, 16);
            this.Content.Name = "Content";
            this.Content.Size = new System.Drawing.Size(256, 2299);
            this.Content.TabIndex = 27;
            // 
            // OLightWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Content);
            this.Name = "OLightWindow";
            this.Size = new System.Drawing.Size(256, 2315);
            this.Controls.SetChildIndex(this.Content, 0);
            this.TopControls.ResumeLayout(false);
            this.TopControlsExtended.ResumeLayout(false);
            this.NameGroup.ContentArea.ResumeLayout(false);
            this.NameGroup.ResumeLayout(false);
            this.NameGroup.PerformLayout();
            this.LightGroup.ContentArea.ResumeLayout(false);
            this.LightGroup.ContentArea.PerformLayout();
            this.LightGroup.ResumeLayout(false);
            this.LightGroup.PerformLayout();
            this.HemisphereLightPanel.ResumeLayout(false);
            this.AmbientLightPanel.ResumeLayout(false);
            this.VertexLightPanel.ResumeLayout(false);
            this.VDistanceAttGroup.ContentArea.ResumeLayout(false);
            this.VDistanceAttGroup.ResumeLayout(false);
            this.VDistanceAttGroup.PerformLayout();
            this.VAngularAttGroup.ContentArea.ResumeLayout(false);
            this.VAngularAttGroup.ResumeLayout(false);
            this.VAngularAttGroup.PerformLayout();
            this.VLightTypePanel.ResumeLayout(false);
            this.VLightTypePanel.PerformLayout();
            this.FragmentLightPanel.ResumeLayout(false);
            this.FDistanceAttGroup.ContentArea.ResumeLayout(false);
            this.FDistanceAttGroup.ResumeLayout(false);
            this.FDistanceAttGroup.PerformLayout();
            this.FAngularAttGroup.ContentArea.ResumeLayout(false);
            this.FAngularAttGroup.ResumeLayout(false);
            this.FAngularAttGroup.PerformLayout();
            this.FLightTypePanel.ResumeLayout(false);
            this.FLightTypePanel.PerformLayout();
            this.LightUsePanel.ResumeLayout(false);
            this.LightUsePanel.PerformLayout();
            this.Content.ContentArea.ResumeLayout(false);
            this.Content.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel TopControls;
        private OButton BtnClear;
        private OButton BtnExport;
        private OButton BtnImport;
        private System.Windows.Forms.TableLayoutPanel TopControlsExtended;
        private OButton BtnReset;
        private OButton BtnDelete;
        private OButton BtnAdd;
        private OList LightList;
        private OGroupBox NameGroup;
        private OTextBox TxtLightName;
        private OGroupBox LightGroup;
        private System.Windows.Forms.TableLayoutPanel LightUsePanel;
        private ORadioButton RadioVertex;
        private ORadioButton RadioFragment;
        private ORadioButton RadioHemisphere;
        private ORadioButton RadioAmbient;
        private OScrollablePanel Content;
        private System.Windows.Forms.Panel FragmentLightPanel;
        private OLabel LblFDirection;
        private ORgbaColorBox Spec1Color;
        private OLabel LblSpec1;
        private ORgbaColorBox Spec0Color;
        private OLabel LblSpec0;
        private ORgbaColorBox FDiffuseColor;
        private OLabel LblFDiffuseColor;
        private ORgbaColorBox FAmbientColor;
        private OLabel LblFAmbientColor;
        private OFloatTextBox FDirZ;
        private OFloatTextBox FDirY;
        private OFloatTextBox FDirX;
        private OGroupBox FAngularAttGroup;
        private OLabel LblAttStart;
        private OFloatTextBox AttEnd;
        private OLabel LblAttEnd;
        private OTextBox TxtDistanceAttLUT;
        private OLabel LblDALUT;
        private OGroupBox FDistanceAttGroup;
        private OLabel LblInputScale;
        private OComboBox InputAngle;
        private OLabel LblInputAngle;
        private OTextBox TxtAngularAttLUT;
        private OLabel LblAALUT;
        private OFloatTextBox AttStart;
        private OCheckBox FAttEnabled;
        private OFloatTextBox FPosZ;
        private OFloatTextBox FPosY;
        private OFloatTextBox FPosX;
        private OLabel LblFPosition;
        private System.Windows.Forms.TableLayoutPanel FLightTypePanel;
        private ORadioButton FRadioPoint;
        private ORadioButton FRadioSpot;
        private ORadioButton FRadioDirectional;
        private OCheckBox FLightEnabled;
        private OCheckBox FTwoSidedDiffuse;
        private System.Windows.Forms.Panel VertexLightPanel;
        private OGroupBox VDistanceAttGroup;
        private OFloatTextBox QuadraticAtt;
        private OLabel LblQuadraticAtt;
        private OLabel LblLinearAtt;
        private OLabel LblConstantAtt;
        private OCheckBox VAttEnabled;
        private OGroupBox VAngularAttGroup;
        private OFloatTextBox CutOffAngle;
        private OLabel LblCutOffAngle;
        private OLabel LblExp;
        private OFloatTextBox VDirZ;
        private OFloatTextBox VDirY;
        private OFloatTextBox VDirX;
        private OLabel LblVDirection;
        private OFloatTextBox VPosZ;
        private OFloatTextBox VPosY;
        private OFloatTextBox VPosX;
        private OLabel LblVPosition;
        private System.Windows.Forms.TableLayoutPanel VLightTypePanel;
        private ORadioButton VRadioPoint;
        private ORadioButton VRadioSpot;
        private ORadioButton VRadioDirectional;
        private ORgbaColorBox VDiffuseColor;
        private OLabel LblVDiffuseColor;
        private ORgbaColorBox VAmbientColor;
        private OLabel LblVAmbientColor;
        private OCheckBox VLightEnabled;
        private OFloatTextBox LinearAtt;
        private OFloatTextBox ConstantAtt;
        private OFloatTextBox Exponent;
        private System.Windows.Forms.Panel AmbientLightPanel;
        private ORgbaColorBox AAmbientColor;
        private OLabel LblAAmbientColor;
        private OCheckBox ALightEnabled;
        private System.Windows.Forms.Panel HemisphereLightPanel;
        private OFloatTextBox SkyDirZ;
        private OFloatTextBox SkyDirY;
        private OFloatTextBox SkyDirX;
        private OLabel LblSkyDir;
        private ORgbaColorBox GroundColor;
        private OLabel LblGroundColor;
        private ORgbaColorBox SkyColor;
        private OLabel LblSkyColor;
        private OCheckBox HLightEnabled;
        private OComboBox InputScale;

    }
}
