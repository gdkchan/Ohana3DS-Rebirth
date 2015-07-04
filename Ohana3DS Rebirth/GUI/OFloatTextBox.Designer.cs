namespace Ohana3DS_Rebirth.GUI
{
    partial class OFloatTextBox
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
            this.MainPanel = new System.Windows.Forms.TableLayoutPanel();
            this.TextBox = new Ohana3DS_Rebirth.GUI.OTextBox();
            this.SeekBar = new Ohana3DS_Rebirth.GUI.OSeekBar();
            this.MainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainPanel
            // 
            this.MainPanel.ColumnCount = 2;
            this.MainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.MainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MainPanel.Controls.Add(this.TextBox, 0, 0);
            this.MainPanel.Controls.Add(this.SeekBar, 1, 0);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(0, 0);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.RowCount = 1;
            this.MainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MainPanel.Size = new System.Drawing.Size(256, 24);
            this.MainPanel.TabIndex = 0;
            // 
            // TextBox
            // 
            this.TextBox.BackColor = System.Drawing.Color.Black;
            this.TextBox.CharacterWhiteList = "0123456789.-";
            this.TextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextBox.Location = new System.Drawing.Point(3, 3);
            this.TextBox.Name = "TextBox";
            this.TextBox.Size = new System.Drawing.Size(58, 18);
            this.TextBox.TabIndex = 0;
            this.TextBox.Text = "0.0";
            this.TextBox.ChangedText += new System.EventHandler(this.TextBox_ChangedText);
            // 
            // SeekBar
            // 
            this.SeekBar.BackColor = System.Drawing.Color.Transparent;
            this.SeekBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SeekBar.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(46)))), ((int)(((byte)(84)))));
            this.SeekBar.Location = new System.Drawing.Point(64, 0);
            this.SeekBar.Margin = new System.Windows.Forms.Padding(0);
            this.SeekBar.MaximumSeek = 2000;
            this.SeekBar.Name = "SeekBar";
            this.SeekBar.Size = new System.Drawing.Size(192, 24);
            this.SeekBar.TabIndex = 1;
            this.SeekBar.TabStop = false;
            this.SeekBar.Value = 1000;
            this.SeekBar.Seek += new System.EventHandler(this.SeekBar_Seek);
            this.SeekBar.SeekStart += new System.EventHandler(this.SeekBar_SeekStart);
            this.SeekBar.SeekEnd += new System.EventHandler(this.SeekBar_SeekEnd);
            // 
            // OFloatTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.MainPanel);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "OFloatTextBox";
            this.Size = new System.Drawing.Size(256, 24);
            this.MainPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel MainPanel;
        private OTextBox TextBox;
        private OSeekBar SeekBar;
    }
}
