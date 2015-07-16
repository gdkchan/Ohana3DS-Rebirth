namespace Ohana3DS_Rebirth.GUI
{
    partial class ORgbaColorBox
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
            this.RgbaTable = new System.Windows.Forms.TableLayoutPanel();
            this.LblA = new Ohana3DS_Rebirth.GUI.OLabel();
            this.LblB = new Ohana3DS_Rebirth.GUI.OLabel();
            this.LblG = new Ohana3DS_Rebirth.GUI.OLabel();
            this.Checkerboard = new System.Windows.Forms.Panel();
            this.SelectedColor = new System.Windows.Forms.PictureBox();
            this.LblR = new Ohana3DS_Rebirth.GUI.OLabel();
            this.SeekR = new Ohana3DS_Rebirth.GUI.OFloatTextBox();
            this.SeekG = new Ohana3DS_Rebirth.GUI.OFloatTextBox();
            this.SeekB = new Ohana3DS_Rebirth.GUI.OFloatTextBox();
            this.SeekA = new Ohana3DS_Rebirth.GUI.OFloatTextBox();
            this.RgbaTable.SuspendLayout();
            this.Checkerboard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SelectedColor)).BeginInit();
            this.SuspendLayout();
            // 
            // RgbaTable
            // 
            this.RgbaTable.ColumnCount = 3;
            this.RgbaTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.RgbaTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.RgbaTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.RgbaTable.Controls.Add(this.LblA, 1, 3);
            this.RgbaTable.Controls.Add(this.LblB, 1, 2);
            this.RgbaTable.Controls.Add(this.LblG, 1, 1);
            this.RgbaTable.Controls.Add(this.Checkerboard, 0, 0);
            this.RgbaTable.Controls.Add(this.LblR, 1, 0);
            this.RgbaTable.Controls.Add(this.SeekR, 2, 0);
            this.RgbaTable.Controls.Add(this.SeekG, 2, 1);
            this.RgbaTable.Controls.Add(this.SeekB, 2, 2);
            this.RgbaTable.Controls.Add(this.SeekA, 2, 3);
            this.RgbaTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RgbaTable.Location = new System.Drawing.Point(0, 0);
            this.RgbaTable.Name = "RgbaTable";
            this.RgbaTable.RowCount = 4;
            this.RgbaTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.RgbaTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.RgbaTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.RgbaTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.RgbaTable.Size = new System.Drawing.Size(256, 88);
            this.RgbaTable.TabIndex = 0;
            // 
            // LblA
            // 
            this.LblA.AutomaticSize = false;
            this.LblA.BackColor = System.Drawing.Color.Transparent;
            this.LblA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblA.Location = new System.Drawing.Point(16, 66);
            this.LblA.Margin = new System.Windows.Forms.Padding(0);
            this.LblA.Name = "LblA";
            this.LblA.Size = new System.Drawing.Size(16, 22);
            this.LblA.TabIndex = 7;
            this.LblA.Text = "A";
            // 
            // LblB
            // 
            this.LblB.AutomaticSize = false;
            this.LblB.BackColor = System.Drawing.Color.Transparent;
            this.LblB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblB.Location = new System.Drawing.Point(16, 44);
            this.LblB.Margin = new System.Windows.Forms.Padding(0);
            this.LblB.Name = "LblB";
            this.LblB.Size = new System.Drawing.Size(16, 22);
            this.LblB.TabIndex = 5;
            this.LblB.Text = "B";
            // 
            // LblG
            // 
            this.LblG.AutomaticSize = false;
            this.LblG.BackColor = System.Drawing.Color.Transparent;
            this.LblG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblG.Location = new System.Drawing.Point(16, 22);
            this.LblG.Margin = new System.Windows.Forms.Padding(0);
            this.LblG.Name = "LblG";
            this.LblG.Size = new System.Drawing.Size(16, 22);
            this.LblG.TabIndex = 3;
            this.LblG.Text = "G";
            // 
            // Checkerboard
            // 
            this.Checkerboard.BackgroundImage = global::Ohana3DS_Rebirth.Properties.Resources.ui_alpha_grid;
            this.Checkerboard.Controls.Add(this.SelectedColor);
            this.Checkerboard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Checkerboard.Location = new System.Drawing.Point(0, 0);
            this.Checkerboard.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.Checkerboard.Name = "Checkerboard";
            this.RgbaTable.SetRowSpan(this.Checkerboard, 4);
            this.Checkerboard.Size = new System.Drawing.Size(16, 88);
            this.Checkerboard.TabIndex = 0;
            // 
            // SelectedColor
            // 
            this.SelectedColor.BackColor = System.Drawing.Color.Transparent;
            this.SelectedColor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SelectedColor.Location = new System.Drawing.Point(0, 0);
            this.SelectedColor.Margin = new System.Windows.Forms.Padding(0);
            this.SelectedColor.Name = "SelectedColor";
            this.SelectedColor.Size = new System.Drawing.Size(16, 88);
            this.SelectedColor.TabIndex = 0;
            this.SelectedColor.TabStop = false;
            // 
            // LblR
            // 
            this.LblR.AutomaticSize = false;
            this.LblR.BackColor = System.Drawing.Color.Transparent;
            this.LblR.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblR.Location = new System.Drawing.Point(16, 0);
            this.LblR.Margin = new System.Windows.Forms.Padding(0);
            this.LblR.Name = "LblR";
            this.LblR.Size = new System.Drawing.Size(16, 22);
            this.LblR.TabIndex = 1;
            this.LblR.Text = "R";
            // 
            // SeekR
            // 
            this.SeekR.BackColor = System.Drawing.Color.Black;
            this.SeekR.DecimalPlaces = ((uint)(0u));
            this.SeekR.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SeekR.Location = new System.Drawing.Point(32, 0);
            this.SeekR.Margin = new System.Windows.Forms.Padding(0);
            this.SeekR.MaximumValue = 255F;
            this.SeekR.MinimumValue = 0F;
            this.SeekR.Name = "SeekR";
            this.SeekR.Size = new System.Drawing.Size(224, 22);
            this.SeekR.TabIndex = 8;
            this.SeekR.Value = 0F;
            this.SeekR.ValueChanged += new System.EventHandler(this.SeekR_ValueChanged);
            // 
            // SeekG
            // 
            this.SeekG.BackColor = System.Drawing.Color.Black;
            this.SeekG.DecimalPlaces = ((uint)(0u));
            this.SeekG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SeekG.Location = new System.Drawing.Point(32, 22);
            this.SeekG.Margin = new System.Windows.Forms.Padding(0);
            this.SeekG.MaximumValue = 255F;
            this.SeekG.MinimumValue = 0F;
            this.SeekG.Name = "SeekG";
            this.SeekG.Size = new System.Drawing.Size(224, 22);
            this.SeekG.TabIndex = 9;
            this.SeekG.Value = 0F;
            this.SeekG.ValueChanged += new System.EventHandler(this.SeekG_ValueChanged);
            // 
            // SeekB
            // 
            this.SeekB.BackColor = System.Drawing.Color.Black;
            this.SeekB.DecimalPlaces = ((uint)(0u));
            this.SeekB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SeekB.Location = new System.Drawing.Point(32, 44);
            this.SeekB.Margin = new System.Windows.Forms.Padding(0);
            this.SeekB.MaximumValue = 255F;
            this.SeekB.MinimumValue = 0F;
            this.SeekB.Name = "SeekB";
            this.SeekB.Size = new System.Drawing.Size(224, 22);
            this.SeekB.TabIndex = 10;
            this.SeekB.Value = 0F;
            this.SeekB.ValueChanged += new System.EventHandler(this.SeekB_ValueChanged);
            // 
            // SeekA
            // 
            this.SeekA.BackColor = System.Drawing.Color.Black;
            this.SeekA.DecimalPlaces = ((uint)(0u));
            this.SeekA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SeekA.Location = new System.Drawing.Point(32, 66);
            this.SeekA.Margin = new System.Windows.Forms.Padding(0);
            this.SeekA.MaximumValue = 255F;
            this.SeekA.MinimumValue = 0F;
            this.SeekA.Name = "SeekA";
            this.SeekA.Size = new System.Drawing.Size(224, 22);
            this.SeekA.TabIndex = 11;
            this.SeekA.Value = 0F;
            this.SeekA.ValueChanged += new System.EventHandler(this.SeekA_ValueChanged);
            // 
            // ORgbaColorBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.RgbaTable);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "ORgbaColorBox";
            this.Size = new System.Drawing.Size(256, 88);
            this.RgbaTable.ResumeLayout(false);
            this.Checkerboard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SelectedColor)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel RgbaTable;
        private OLabel LblA;
        private OLabel LblB;
        private OLabel LblG;
        private System.Windows.Forms.Panel Checkerboard;
        private System.Windows.Forms.PictureBox SelectedColor;
        private OLabel LblR;
        private OFloatTextBox SeekR;
        private OFloatTextBox SeekG;
        private OFloatTextBox SeekB;
        private OFloatTextBox SeekA;
    }
}
