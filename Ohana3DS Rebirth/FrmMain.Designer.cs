namespace Ohana3DS_Rebirth
{
    partial class FrmMain
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

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte do Designer - não modifique
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.TopMenu = new System.Windows.Forms.MenuStrip();
            this.MenuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuFileSeparator0 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuViewport = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuViewAA = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuViewAANone = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuViewAA2x = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuViewAA4x = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuViewAA8x = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuViewAA16x = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuViewBg = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuViewBgBlack = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuViewBgGray = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuViewBgWhite = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuViewBgSeparator0 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuViewBgCustom = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuViewShow = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuViewShowGuidelines = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuViewShowInformation = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuViewShowAllMeshes = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuViewTexturing = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuViewFragmentShader = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuViewTexSeparator0 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuViewTexUseFirst = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuViewTexUseLast = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuViewSeparator0 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuViewShowSidebar = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuViewWireframeMode = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuTools = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuToolBCHTextureReplace = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuToolSm4shModelCreator = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.ContentContainer.SuspendLayout();
            this.TopMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // ContentContainer
            // 
            this.ContentContainer.Controls.Add(this.TopMenu);
            this.ContentContainer.MinimumSize = new System.Drawing.Size(128, 32);
            this.ContentContainer.Size = new System.Drawing.Size(792, 592);
            this.ContentContainer.Controls.SetChildIndex(this.TopMenu, 0);
            // 
            // TopMenu
            // 
            this.TopMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.TopMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuFile,
            this.MenuOptions,
            this.MenuTools,
            this.MenuHelp});
            this.TopMenu.Location = new System.Drawing.Point(0, 20);
            this.TopMenu.Name = "TopMenu";
            this.TopMenu.Size = new System.Drawing.Size(792, 24);
            this.TopMenu.TabIndex = 10;
            // 
            // MenuFile
            // 
            this.MenuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuOpen,
            this.MenuFileSeparator0,
            this.MenuExit});
            this.MenuFile.Name = "MenuFile";
            this.MenuFile.Size = new System.Drawing.Size(37, 20);
            this.MenuFile.Text = "&File";
            // 
            // MenuOpen
            // 
            this.MenuOpen.Name = "MenuOpen";
            this.MenuOpen.Size = new System.Drawing.Size(103, 22);
            this.MenuOpen.Text = "&Open";
            this.MenuOpen.Click += new System.EventHandler(this.MenuOpen_Click);
            // 
            // MenuFileSeparator0
            // 
            this.MenuFileSeparator0.Name = "MenuFileSeparator0";
            this.MenuFileSeparator0.Size = new System.Drawing.Size(100, 6);
            // 
            // MenuExit
            // 
            this.MenuExit.Name = "MenuExit";
            this.MenuExit.Size = new System.Drawing.Size(103, 22);
            this.MenuExit.Text = "&Exit";
            // 
            // MenuOptions
            // 
            this.MenuOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuViewport});
            this.MenuOptions.Name = "MenuOptions";
            this.MenuOptions.Size = new System.Drawing.Size(61, 20);
            this.MenuOptions.Text = "&Options";
            // 
            // MenuViewport
            // 
            this.MenuViewport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuViewAA,
            this.MenuViewBg,
            this.MenuViewShow,
            this.MenuViewTexturing,
            this.MenuViewSeparator0,
            this.MenuViewShowSidebar,
            this.MenuViewWireframeMode});
            this.MenuViewport.Name = "MenuViewport";
            this.MenuViewport.Size = new System.Drawing.Size(121, 22);
            this.MenuViewport.Text = "&Viewport";
            // 
            // MenuViewAA
            // 
            this.MenuViewAA.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuViewAANone,
            this.MenuViewAA2x,
            this.MenuViewAA4x,
            this.MenuViewAA8x,
            this.MenuViewAA16x});
            this.MenuViewAA.Name = "MenuViewAA";
            this.MenuViewAA.Size = new System.Drawing.Size(163, 22);
            this.MenuViewAA.Text = "&Anti-aliasing";
            // 
            // MenuViewAANone
            // 
            this.MenuViewAANone.Name = "MenuViewAANone";
            this.MenuViewAANone.Size = new System.Drawing.Size(103, 22);
            this.MenuViewAANone.Text = "&None";
            this.MenuViewAANone.Click += new System.EventHandler(this.MenuViewAANone_Click);
            // 
            // MenuViewAA2x
            // 
            this.MenuViewAA2x.Name = "MenuViewAA2x";
            this.MenuViewAA2x.Size = new System.Drawing.Size(103, 22);
            this.MenuViewAA2x.Text = "&2x";
            this.MenuViewAA2x.Click += new System.EventHandler(this.MenuViewAA2x_Click);
            // 
            // MenuViewAA4x
            // 
            this.MenuViewAA4x.Name = "MenuViewAA4x";
            this.MenuViewAA4x.Size = new System.Drawing.Size(103, 22);
            this.MenuViewAA4x.Text = "&4x";
            this.MenuViewAA4x.Click += new System.EventHandler(this.MenuViewAA4x_Click);
            // 
            // MenuViewAA8x
            // 
            this.MenuViewAA8x.Name = "MenuViewAA8x";
            this.MenuViewAA8x.Size = new System.Drawing.Size(103, 22);
            this.MenuViewAA8x.Text = "&8x";
            this.MenuViewAA8x.Click += new System.EventHandler(this.MenuViewAA8x_Click);
            // 
            // MenuViewAA16x
            // 
            this.MenuViewAA16x.Name = "MenuViewAA16x";
            this.MenuViewAA16x.Size = new System.Drawing.Size(103, 22);
            this.MenuViewAA16x.Text = "&16x";
            this.MenuViewAA16x.Click += new System.EventHandler(this.MenuViewAA16x_Click);
            // 
            // MenuViewBg
            // 
            this.MenuViewBg.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuViewBgBlack,
            this.MenuViewBgGray,
            this.MenuViewBgWhite,
            this.MenuViewBgSeparator0,
            this.MenuViewBgCustom});
            this.MenuViewBg.Name = "MenuViewBg";
            this.MenuViewBg.Size = new System.Drawing.Size(163, 22);
            this.MenuViewBg.Text = "&Background";
            // 
            // MenuViewBgBlack
            // 
            this.MenuViewBgBlack.Name = "MenuViewBgBlack";
            this.MenuViewBgBlack.Size = new System.Drawing.Size(125, 22);
            this.MenuViewBgBlack.Text = "&Black";
            this.MenuViewBgBlack.Click += new System.EventHandler(this.MenuViewBgBlack_Click);
            // 
            // MenuViewBgGray
            // 
            this.MenuViewBgGray.Name = "MenuViewBgGray";
            this.MenuViewBgGray.Size = new System.Drawing.Size(125, 22);
            this.MenuViewBgGray.Text = "&Gray";
            this.MenuViewBgGray.Click += new System.EventHandler(this.MenuViewBgGray_Click);
            // 
            // MenuViewBgWhite
            // 
            this.MenuViewBgWhite.Name = "MenuViewBgWhite";
            this.MenuViewBgWhite.Size = new System.Drawing.Size(125, 22);
            this.MenuViewBgWhite.Text = "&White";
            this.MenuViewBgWhite.Click += new System.EventHandler(this.MenuViewBgWhite_Click);
            // 
            // MenuViewBgSeparator0
            // 
            this.MenuViewBgSeparator0.Name = "MenuViewBgSeparator0";
            this.MenuViewBgSeparator0.Size = new System.Drawing.Size(122, 6);
            // 
            // MenuViewBgCustom
            // 
            this.MenuViewBgCustom.Name = "MenuViewBgCustom";
            this.MenuViewBgCustom.Size = new System.Drawing.Size(125, 22);
            this.MenuViewBgCustom.Text = "&Custom...";
            this.MenuViewBgCustom.Click += new System.EventHandler(this.MenuViewBgCustom_Click);
            // 
            // MenuViewShow
            // 
            this.MenuViewShow.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuViewShowGuidelines,
            this.MenuViewShowInformation,
            this.MenuViewShowAllMeshes});
            this.MenuViewShow.Name = "MenuViewShow";
            this.MenuViewShow.Size = new System.Drawing.Size(163, 22);
            this.MenuViewShow.Text = "&Show/hide";
            // 
            // MenuViewShowGuidelines
            // 
            this.MenuViewShowGuidelines.Name = "MenuViewShowGuidelines";
            this.MenuViewShowGuidelines.Size = new System.Drawing.Size(200, 22);
            this.MenuViewShowGuidelines.Text = "&Show guidelines";
            this.MenuViewShowGuidelines.Click += new System.EventHandler(this.MenuViewShowGuidelines_Click);
            // 
            // MenuViewShowInformation
            // 
            this.MenuViewShowInformation.Name = "MenuViewShowInformation";
            this.MenuViewShowInformation.Size = new System.Drawing.Size(200, 22);
            this.MenuViewShowInformation.Text = "&Show information";
            this.MenuViewShowInformation.Click += new System.EventHandler(this.MenuViewShowInformation_Click);
            // 
            // MenuViewShowAllMeshes
            // 
            this.MenuViewShowAllMeshes.Name = "MenuViewShowAllMeshes";
            this.MenuViewShowAllMeshes.Size = new System.Drawing.Size(200, 22);
            this.MenuViewShowAllMeshes.Text = "&Always show all meshes";
            this.MenuViewShowAllMeshes.Click += new System.EventHandler(this.MenuViewShowAllMeshes_Click);
            // 
            // MenuViewTexturing
            // 
            this.MenuViewTexturing.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuViewFragmentShader,
            this.MenuViewTexSeparator0,
            this.MenuViewTexUseFirst,
            this.MenuViewTexUseLast});
            this.MenuViewTexturing.Name = "MenuViewTexturing";
            this.MenuViewTexturing.Size = new System.Drawing.Size(163, 22);
            this.MenuViewTexturing.Text = "&Texturing";
            // 
            // MenuViewFragmentShader
            // 
            this.MenuViewFragmentShader.Name = "MenuViewFragmentShader";
            this.MenuViewFragmentShader.Size = new System.Drawing.Size(227, 22);
            this.MenuViewFragmentShader.Text = "&Enable Fragment Shader";
            this.MenuViewFragmentShader.Click += new System.EventHandler(this.MenuViewFragmentShader_Click);
            // 
            // MenuViewTexSeparator0
            // 
            this.MenuViewTexSeparator0.Name = "MenuViewTexSeparator0";
            this.MenuViewTexSeparator0.Size = new System.Drawing.Size(224, 6);
            // 
            // MenuViewTexUseFirst
            // 
            this.MenuViewTexUseFirst.Name = "MenuViewTexUseFirst";
            this.MenuViewTexUseFirst.Size = new System.Drawing.Size(227, 22);
            this.MenuViewTexUseFirst.Text = "&Always use first texture";
            this.MenuViewTexUseFirst.Click += new System.EventHandler(this.MenuViewTexUseFirst_Click);
            // 
            // MenuViewTexUseLast
            // 
            this.MenuViewTexUseLast.Name = "MenuViewTexUseLast";
            this.MenuViewTexUseLast.Size = new System.Drawing.Size(227, 22);
            this.MenuViewTexUseLast.Text = "&Use last texture when present";
            this.MenuViewTexUseLast.Click += new System.EventHandler(this.MenuViewTexUseLast_Click);
            // 
            // MenuViewSeparator0
            // 
            this.MenuViewSeparator0.Name = "MenuViewSeparator0";
            this.MenuViewSeparator0.Size = new System.Drawing.Size(160, 6);
            // 
            // MenuViewShowSidebar
            // 
            this.MenuViewShowSidebar.Name = "MenuViewShowSidebar";
            this.MenuViewShowSidebar.Size = new System.Drawing.Size(163, 22);
            this.MenuViewShowSidebar.Text = "&Show sidebar";
            this.MenuViewShowSidebar.Click += new System.EventHandler(this.MenuViewShowSidebar_Click);
            // 
            // MenuViewWireframeMode
            // 
            this.MenuViewWireframeMode.Name = "MenuViewWireframeMode";
            this.MenuViewWireframeMode.Size = new System.Drawing.Size(163, 22);
            this.MenuViewWireframeMode.Text = "&Wireframe mode";
            this.MenuViewWireframeMode.Click += new System.EventHandler(this.MenuViewWireframeMode_Click);
            // 
            // MenuTools
            // 
            this.MenuTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuToolBCHTextureReplace,
            this.MenuToolSm4shModelCreator});
            this.MenuTools.Name = "MenuTools";
            this.MenuTools.Size = new System.Drawing.Size(47, 20);
            this.MenuTools.Text = "&Tools";
            // 
            // MenuToolBCHTextureReplace
            // 
            this.MenuToolBCHTextureReplace.Name = "MenuToolBCHTextureReplace";
            this.MenuToolBCHTextureReplace.Size = new System.Drawing.Size(195, 22);
            this.MenuToolBCHTextureReplace.Text = "&BCH texture replacer...";
            this.MenuToolBCHTextureReplace.Click += new System.EventHandler(this.MenuToolBCHTextureReplace_Click);
            // 
            // MenuToolSm4shModelCreator
            // 
            this.MenuToolSm4shModelCreator.Name = "MenuToolSm4shModelCreator";
            this.MenuToolSm4shModelCreator.Size = new System.Drawing.Size(195, 22);
            this.MenuToolSm4shModelCreator.Text = "&Sm4sh model creator...";
            this.MenuToolSm4shModelCreator.Click += new System.EventHandler(this.MenuToolSm4shModelCreator_Click);
            // 
            // MenuHelp
            // 
            this.MenuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuAbout});
            this.MenuHelp.Name = "MenuHelp";
            this.MenuHelp.Size = new System.Drawing.Size(44, 20);
            this.MenuHelp.Text = "&Help";
            // 
            // MenuAbout
            // 
            this.MenuAbout.Name = "MenuAbout";
            this.MenuAbout.Size = new System.Drawing.Size(152, 22);
            this.MenuAbout.Text = "&About";
            this.MenuAbout.Click += new System.EventHandler(this.MenuAbout_Click);
            // 
            // FrmMain
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.MainMenuStrip = this.TopMenu;
            this.MinimumSize = new System.Drawing.Size(256, 128);
            this.Name = "FrmMain";
            this.Text = "Ohana3DS";
            this.TitleIcon = ((System.Drawing.Image)(resources.GetObject("$this.TitleIcon")));
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.Shown += new System.EventHandler(this.FrmMain_Shown);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.FrmMain_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.FrmMain_DragEnter);
            this.ContentContainer.ResumeLayout(false);
            this.ContentContainer.PerformLayout();
            this.TopMenu.ResumeLayout(false);
            this.TopMenu.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.MenuStrip TopMenu;
        private System.Windows.Forms.ToolStripMenuItem MenuFile;
        private System.Windows.Forms.ToolStripMenuItem MenuOpen;
        private System.Windows.Forms.ToolStripSeparator MenuFileSeparator0;
        private System.Windows.Forms.ToolStripMenuItem MenuExit;
        private System.Windows.Forms.ToolStripMenuItem MenuOptions;
        private System.Windows.Forms.ToolStripMenuItem MenuViewport;
        private System.Windows.Forms.ToolStripMenuItem MenuViewAA;
        private System.Windows.Forms.ToolStripMenuItem MenuViewAANone;
        private System.Windows.Forms.ToolStripMenuItem MenuViewAA2x;
        private System.Windows.Forms.ToolStripMenuItem MenuViewAA4x;
        private System.Windows.Forms.ToolStripMenuItem MenuViewAA8x;
        private System.Windows.Forms.ToolStripMenuItem MenuViewAA16x;
        private System.Windows.Forms.ToolStripMenuItem MenuViewBg;
        private System.Windows.Forms.ToolStripMenuItem MenuViewBgBlack;
        private System.Windows.Forms.ToolStripMenuItem MenuViewBgGray;
        private System.Windows.Forms.ToolStripMenuItem MenuViewBgWhite;
        private System.Windows.Forms.ToolStripSeparator MenuViewBgSeparator0;
        private System.Windows.Forms.ToolStripMenuItem MenuViewBgCustom;
        private System.Windows.Forms.ToolStripMenuItem MenuViewTexturing;
        private System.Windows.Forms.ToolStripMenuItem MenuViewFragmentShader;
        private System.Windows.Forms.ToolStripSeparator MenuViewTexSeparator0;
        private System.Windows.Forms.ToolStripMenuItem MenuViewTexUseFirst;
        private System.Windows.Forms.ToolStripMenuItem MenuViewTexUseLast;
        private System.Windows.Forms.ToolStripSeparator MenuViewSeparator0;
        private System.Windows.Forms.ToolStripMenuItem MenuTools;
        private System.Windows.Forms.ToolStripMenuItem MenuHelp;
        private System.Windows.Forms.ToolStripMenuItem MenuAbout;
        private System.Windows.Forms.ToolStripMenuItem MenuViewShow;
        private System.Windows.Forms.ToolStripMenuItem MenuViewShowGuidelines;
        private System.Windows.Forms.ToolStripMenuItem MenuViewShowInformation;
        private System.Windows.Forms.ToolStripMenuItem MenuViewShowAllMeshes;
        private System.Windows.Forms.ToolStripMenuItem MenuViewShowSidebar;
        private System.Windows.Forms.ToolStripMenuItem MenuViewWireframeMode;
        private System.Windows.Forms.ToolStripMenuItem MenuToolBCHTextureReplace;
        private System.Windows.Forms.ToolStripMenuItem MenuToolSm4shModelCreator;
    }
}
