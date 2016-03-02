namespace Ohana3DS_Rebirth.GUI
{
    partial class OViewportPanel
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
            this.Screen = new System.Windows.Forms.PictureBox();
            this.Splitter = new System.Windows.Forms.SplitContainer();
            this.VisibilityAnimationsGroup = new Ohana3DS_Rebirth.GUI.OGroupBox();
            this.VisibilityAnimationsPanel = new Ohana3DS_Rebirth.GUI.OAnimationsPanel();
            this.MaterialAnimationsGroup = new Ohana3DS_Rebirth.GUI.OGroupBox();
            this.MaterialAnimationsPanel = new Ohana3DS_Rebirth.GUI.OAnimationsPanel();
            this.SkeletalAnimationsGroup = new Ohana3DS_Rebirth.GUI.OGroupBox();
            this.SkeletalAnimationsPanel = new Ohana3DS_Rebirth.GUI.OAnimationsPanel();
            this.TexturesGroup = new Ohana3DS_Rebirth.GUI.OGroupBox();
            this.TexturesPanel = new Ohana3DS_Rebirth.GUI.OTexturesPanel();
            this.ModelsGroup = new Ohana3DS_Rebirth.GUI.OGroupBox();
            this.ModelsPanel = new Ohana3DS_Rebirth.GUI.OModelsPanel();
            ((System.ComponentModel.ISupportInitialize)(this.Screen)).BeginInit();
            this.Splitter.Panel1.SuspendLayout();
            this.Splitter.Panel2.SuspendLayout();
            this.Splitter.SuspendLayout();
            this.VisibilityAnimationsGroup.ContentArea.SuspendLayout();
            this.VisibilityAnimationsGroup.SuspendLayout();
            this.MaterialAnimationsGroup.ContentArea.SuspendLayout();
            this.MaterialAnimationsGroup.SuspendLayout();
            this.SkeletalAnimationsGroup.ContentArea.SuspendLayout();
            this.SkeletalAnimationsGroup.SuspendLayout();
            this.TexturesGroup.ContentArea.SuspendLayout();
            this.TexturesGroup.SuspendLayout();
            this.ModelsGroup.ContentArea.SuspendLayout();
            this.ModelsGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // Screen
            // 
            this.Screen.BackColor = System.Drawing.Color.Black;
            this.Screen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Screen.Location = new System.Drawing.Point(0, 0);
            this.Screen.Name = "Screen";
            this.Screen.Size = new System.Drawing.Size(348, 512);
            this.Screen.TabIndex = 0;
            this.Screen.TabStop = false;
            this.Screen.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Screen_MouseDown);
            this.Screen.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Screen_MouseMove);
            this.Screen.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Screen_MouseUp);
            this.Screen.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.Screen_MouseWheel);
            this.Screen.Resize += new System.EventHandler(this.Screen_Resize);
            // 
            // Splitter
            // 
            this.Splitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Splitter.Location = new System.Drawing.Point(0, 0);
            this.Splitter.Name = "Splitter";
            // 
            // Splitter.Panel1
            // 
            this.Splitter.Panel1.BackColor = System.Drawing.Color.Transparent;
            this.Splitter.Panel1.Controls.Add(this.VisibilityAnimationsGroup);
            this.Splitter.Panel1.Controls.Add(this.MaterialAnimationsGroup);
            this.Splitter.Panel1.Controls.Add(this.SkeletalAnimationsGroup);
            this.Splitter.Panel1.Controls.Add(this.TexturesGroup);
            this.Splitter.Panel1.Controls.Add(this.ModelsGroup);
            this.Splitter.Panel1.Layout += new System.Windows.Forms.LayoutEventHandler(this.Splitter_Panel1_Layout);
            // 
            // Splitter.Panel2
            // 
            this.Splitter.Panel2.Controls.Add(this.Screen);
            this.Splitter.Size = new System.Drawing.Size(512, 512);
            this.Splitter.SplitterDistance = 160;
            this.Splitter.TabIndex = 1;
            // 
            // VisibilityAnimationsGroup
            // 
            this.VisibilityAnimationsGroup.AutomaticSize = false;
            this.VisibilityAnimationsGroup.BackColor = System.Drawing.Color.Black;
            this.VisibilityAnimationsGroup.Collapsed = true;
            // 
            // VisibilityAnimationsGroup.ContentArea
            // 
            this.VisibilityAnimationsGroup.ContentArea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.VisibilityAnimationsGroup.ContentArea.Controls.Add(this.VisibilityAnimationsPanel);
            this.VisibilityAnimationsGroup.ContentArea.Location = new System.Drawing.Point(0, 24);
            this.VisibilityAnimationsGroup.ContentArea.Name = "ContentArea";
            this.VisibilityAnimationsGroup.ContentArea.Size = new System.Drawing.Size(160, 0);
            this.VisibilityAnimationsGroup.ContentArea.TabIndex = 0;
            this.VisibilityAnimationsGroup.ContentColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.VisibilityAnimationsGroup.Dock = System.Windows.Forms.DockStyle.Top;
            this.VisibilityAnimationsGroup.ExpandedHeight = 256;
            this.VisibilityAnimationsGroup.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VisibilityAnimationsGroup.Location = new System.Drawing.Point(0, 96);
            this.VisibilityAnimationsGroup.Name = "VisibilityAnimationsGroup";
            this.VisibilityAnimationsGroup.Size = new System.Drawing.Size(160, 24);
            this.VisibilityAnimationsGroup.TabIndex = 7;
            this.VisibilityAnimationsGroup.Title = "Visibility animations";
            this.VisibilityAnimationsGroup.GroupBoxExpanded += new System.EventHandler(this.Group_GroupBoxExpanded);
            // 
            // VisibilityAnimationsPanel
            // 
            this.VisibilityAnimationsPanel.BackColor = System.Drawing.Color.Transparent;
            this.VisibilityAnimationsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VisibilityAnimationsPanel.ForeColor = System.Drawing.Color.White;
            this.VisibilityAnimationsPanel.Location = new System.Drawing.Point(0, 0);
            this.VisibilityAnimationsPanel.Name = "VisibilityAnimationsPanel";
            this.VisibilityAnimationsPanel.Size = new System.Drawing.Size(160, 0);
            this.VisibilityAnimationsPanel.TabIndex = 0;
            // 
            // MaterialAnimationsGroup
            // 
            this.MaterialAnimationsGroup.AutomaticSize = false;
            this.MaterialAnimationsGroup.BackColor = System.Drawing.Color.Black;
            this.MaterialAnimationsGroup.Collapsed = true;
            // 
            // MaterialAnimationsGroup.ContentArea
            // 
            this.MaterialAnimationsGroup.ContentArea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.MaterialAnimationsGroup.ContentArea.Controls.Add(this.MaterialAnimationsPanel);
            this.MaterialAnimationsGroup.ContentArea.Location = new System.Drawing.Point(0, 24);
            this.MaterialAnimationsGroup.ContentArea.Name = "ContentArea";
            this.MaterialAnimationsGroup.ContentArea.Size = new System.Drawing.Size(160, 0);
            this.MaterialAnimationsGroup.ContentArea.TabIndex = 0;
            this.MaterialAnimationsGroup.ContentColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.MaterialAnimationsGroup.Dock = System.Windows.Forms.DockStyle.Top;
            this.MaterialAnimationsGroup.ExpandedHeight = 256;
            this.MaterialAnimationsGroup.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaterialAnimationsGroup.Location = new System.Drawing.Point(0, 72);
            this.MaterialAnimationsGroup.Name = "MaterialAnimationsGroup";
            this.MaterialAnimationsGroup.Size = new System.Drawing.Size(160, 24);
            this.MaterialAnimationsGroup.TabIndex = 6;
            this.MaterialAnimationsGroup.Title = "Material animations";
            this.MaterialAnimationsGroup.GroupBoxExpanded += new System.EventHandler(this.Group_GroupBoxExpanded);
            // 
            // MaterialAnimationsPanel
            // 
            this.MaterialAnimationsPanel.BackColor = System.Drawing.Color.Transparent;
            this.MaterialAnimationsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MaterialAnimationsPanel.ForeColor = System.Drawing.Color.White;
            this.MaterialAnimationsPanel.Location = new System.Drawing.Point(0, 0);
            this.MaterialAnimationsPanel.Name = "MaterialAnimationsPanel";
            this.MaterialAnimationsPanel.Size = new System.Drawing.Size(160, 0);
            this.MaterialAnimationsPanel.TabIndex = 0;
            // 
            // SkeletalAnimationsGroup
            // 
            this.SkeletalAnimationsGroup.AutomaticSize = false;
            this.SkeletalAnimationsGroup.BackColor = System.Drawing.Color.Black;
            this.SkeletalAnimationsGroup.Collapsed = true;
            // 
            // SkeletalAnimationsGroup.ContentArea
            // 
            this.SkeletalAnimationsGroup.ContentArea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.SkeletalAnimationsGroup.ContentArea.Controls.Add(this.SkeletalAnimationsPanel);
            this.SkeletalAnimationsGroup.ContentArea.Location = new System.Drawing.Point(0, 24);
            this.SkeletalAnimationsGroup.ContentArea.Name = "ContentArea";
            this.SkeletalAnimationsGroup.ContentArea.Size = new System.Drawing.Size(160, 0);
            this.SkeletalAnimationsGroup.ContentArea.TabIndex = 0;
            this.SkeletalAnimationsGroup.ContentColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.SkeletalAnimationsGroup.Dock = System.Windows.Forms.DockStyle.Top;
            this.SkeletalAnimationsGroup.ExpandedHeight = 256;
            this.SkeletalAnimationsGroup.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SkeletalAnimationsGroup.Location = new System.Drawing.Point(0, 48);
            this.SkeletalAnimationsGroup.Name = "SkeletalAnimationsGroup";
            this.SkeletalAnimationsGroup.Size = new System.Drawing.Size(160, 24);
            this.SkeletalAnimationsGroup.TabIndex = 8;
            this.SkeletalAnimationsGroup.Title = "Skeletal animations";
            this.SkeletalAnimationsGroup.GroupBoxExpanded += new System.EventHandler(this.Group_GroupBoxExpanded);
            // 
            // SkeletalAnimationsPanel
            // 
            this.SkeletalAnimationsPanel.BackColor = System.Drawing.Color.Transparent;
            this.SkeletalAnimationsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SkeletalAnimationsPanel.ForeColor = System.Drawing.Color.White;
            this.SkeletalAnimationsPanel.Location = new System.Drawing.Point(0, 0);
            this.SkeletalAnimationsPanel.Name = "SkeletalAnimationsPanel";
            this.SkeletalAnimationsPanel.Size = new System.Drawing.Size(160, 0);
            this.SkeletalAnimationsPanel.TabIndex = 0;
            // 
            // TexturesGroup
            // 
            this.TexturesGroup.AutomaticSize = false;
            this.TexturesGroup.BackColor = System.Drawing.Color.Black;
            this.TexturesGroup.Collapsed = true;
            // 
            // TexturesGroup.ContentArea
            // 
            this.TexturesGroup.ContentArea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.TexturesGroup.ContentArea.Controls.Add(this.TexturesPanel);
            this.TexturesGroup.ContentArea.Location = new System.Drawing.Point(0, 24);
            this.TexturesGroup.ContentArea.Name = "ContentArea";
            this.TexturesGroup.ContentArea.Size = new System.Drawing.Size(160, 0);
            this.TexturesGroup.ContentArea.TabIndex = 0;
            this.TexturesGroup.ContentColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.TexturesGroup.Dock = System.Windows.Forms.DockStyle.Top;
            this.TexturesGroup.ExpandedHeight = 256;
            this.TexturesGroup.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TexturesGroup.Location = new System.Drawing.Point(0, 24);
            this.TexturesGroup.Name = "TexturesGroup";
            this.TexturesGroup.Size = new System.Drawing.Size(160, 24);
            this.TexturesGroup.TabIndex = 1;
            this.TexturesGroup.Title = "Textures";
            this.TexturesGroup.GroupBoxExpanded += new System.EventHandler(this.Group_GroupBoxExpanded);
            // 
            // TexturesPanel
            // 
            this.TexturesPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.TexturesPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TexturesPanel.ForeColor = System.Drawing.Color.White;
            this.TexturesPanel.Location = new System.Drawing.Point(0, 0);
            this.TexturesPanel.Name = "TexturesPanel";
            this.TexturesPanel.Size = new System.Drawing.Size(160, 0);
            this.TexturesPanel.TabIndex = 0;
            // 
            // ModelsGroup
            // 
            this.ModelsGroup.AutomaticSize = false;
            this.ModelsGroup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.ModelsGroup.Collapsed = true;
            // 
            // ModelsGroup.ContentArea
            // 
            this.ModelsGroup.ContentArea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.ModelsGroup.ContentArea.Controls.Add(this.ModelsPanel);
            this.ModelsGroup.ContentArea.Location = new System.Drawing.Point(0, 24);
            this.ModelsGroup.ContentArea.Name = "ContentArea";
            this.ModelsGroup.ContentArea.Size = new System.Drawing.Size(160, 0);
            this.ModelsGroup.ContentArea.TabIndex = 0;
            this.ModelsGroup.ContentColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.ModelsGroup.Dock = System.Windows.Forms.DockStyle.Top;
            this.ModelsGroup.ExpandedHeight = 256;
            this.ModelsGroup.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ModelsGroup.Location = new System.Drawing.Point(0, 0);
            this.ModelsGroup.Name = "ModelsGroup";
            this.ModelsGroup.Size = new System.Drawing.Size(160, 24);
            this.ModelsGroup.TabIndex = 0;
            this.ModelsGroup.Title = "Models";
            this.ModelsGroup.GroupBoxExpanded += new System.EventHandler(this.Group_GroupBoxExpanded);
            // 
            // ModelsPanel
            // 
            this.ModelsPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.ModelsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ModelsPanel.ForeColor = System.Drawing.Color.White;
            this.ModelsPanel.Location = new System.Drawing.Point(0, 0);
            this.ModelsPanel.Name = "ModelsPanel";
            this.ModelsPanel.Size = new System.Drawing.Size(160, 0);
            this.ModelsPanel.TabIndex = 0;
            // 
            // OViewportPanel
            // 
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.Splitter);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "OViewportPanel";
            this.Size = new System.Drawing.Size(512, 512);
            this.Load += new System.EventHandler(this.OViewportPanel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Screen)).EndInit();
            this.Splitter.Panel1.ResumeLayout(false);
            this.Splitter.Panel2.ResumeLayout(false);
            this.Splitter.ResumeLayout(false);
            this.VisibilityAnimationsGroup.ContentArea.ResumeLayout(false);
            this.VisibilityAnimationsGroup.ResumeLayout(false);
            this.MaterialAnimationsGroup.ContentArea.ResumeLayout(false);
            this.MaterialAnimationsGroup.ResumeLayout(false);
            this.SkeletalAnimationsGroup.ContentArea.ResumeLayout(false);
            this.SkeletalAnimationsGroup.ResumeLayout(false);
            this.TexturesGroup.ContentArea.ResumeLayout(false);
            this.TexturesGroup.ResumeLayout(false);
            this.ModelsGroup.ContentArea.ResumeLayout(false);
            this.ModelsGroup.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox Screen;
        private System.Windows.Forms.SplitContainer Splitter;
        private OGroupBox ModelsGroup;
        private OModelsPanel ModelsPanel;
        private OGroupBox TexturesGroup;
        private OGroupBox VisibilityAnimationsGroup;
        private OAnimationsPanel VisibilityAnimationsPanel;
        private OGroupBox MaterialAnimationsGroup;
        private OAnimationsPanel MaterialAnimationsPanel;
        private OGroupBox SkeletalAnimationsGroup;
        private OAnimationsPanel SkeletalAnimationsPanel;
        private OTexturesPanel TexturesPanel;
    }
}
