namespace Ohana3DS_Rebirth.GUI
{
    partial class OComboBox
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
            this.topBox = new System.Windows.Forms.TableLayoutPanel();
            this.BtnToggle = new Ohana3DS_Rebirth.GUI.OButton();
            this.LblSelectedItem = new Ohana3DS_Rebirth.GUI.OLabel();
            this.list = new Ohana3DS_Rebirth.GUI.OList();
            this.topBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // topBox
            // 
            this.topBox.ColumnCount = 2;
            this.topBox.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.topBox.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.topBox.Controls.Add(this.BtnToggle, 1, 0);
            this.topBox.Controls.Add(this.LblSelectedItem, 0, 0);
            this.topBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.topBox.Location = new System.Drawing.Point(0, 0);
            this.topBox.Name = "topBox";
            this.topBox.RowCount = 1;
            this.topBox.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.topBox.Size = new System.Drawing.Size(256, 24);
            this.topBox.TabIndex = 0;
            // 
            // BtnToggle
            // 
            this.BtnToggle.BackColor = System.Drawing.Color.Transparent;
            this.BtnToggle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnToggle.Image = global::Ohana3DS_Rebirth.Properties.Resources.ui_down;
            this.BtnToggle.Location = new System.Drawing.Point(240, 0);
            this.BtnToggle.Margin = new System.Windows.Forms.Padding(0);
            this.BtnToggle.Name = "BtnToggle";
            this.BtnToggle.Size = new System.Drawing.Size(16, 24);
            this.BtnToggle.TabIndex = 4;
            this.BtnToggle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnToggle_MouseDown);
            // 
            // LblSelectedItem
            // 
            this.LblSelectedItem.AutomaticSize = false;
            this.LblSelectedItem.BackColor = System.Drawing.Color.Transparent;
            this.LblSelectedItem.Centered = false;
            this.LblSelectedItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblSelectedItem.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSelectedItem.Location = new System.Drawing.Point(0, 0);
            this.LblSelectedItem.Margin = new System.Windows.Forms.Padding(0);
            this.LblSelectedItem.Name = "LblSelectedItem";
            this.LblSelectedItem.Size = new System.Drawing.Size(240, 24);
            this.LblSelectedItem.TabIndex = 0;
            // 
            // list
            // 
            this.list.BackColor = System.Drawing.Color.Transparent;
            this.list.Dock = System.Windows.Forms.DockStyle.Fill;
            this.list.HeaderHeight = 24;
            this.list.ItemHeight = 16;
            this.list.Location = new System.Drawing.Point(0, 24);
            this.list.Name = "list";
            this.list.SelectedIndex = -1;
            this.list.Size = new System.Drawing.Size(256, 0);
            this.list.TabIndex = 1;
            this.list.Visible = false;
            this.list.SelectedIndexChanged += new System.EventHandler(this.list_SelectedIndexChanged);
            // 
            // OComboBox
            // 
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.list);
            this.Controls.Add(this.topBox);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "OComboBox";
            this.Size = new System.Drawing.Size(256, 24);
            this.EnabledChanged += new System.EventHandler(this.OComboBox_EnabledChanged);
            this.topBox.ResumeLayout(false);
            this.topBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel topBox;
        private OButton BtnToggle;
        private Ohana3DS_Rebirth.GUI.OLabel LblSelectedItem;
        private OList list;

    }
}
