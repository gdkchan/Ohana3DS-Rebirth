namespace Ohana3DS_Rebirth.GUI
{
    partial class OAnimWindow
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
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.animNumBox = new System.Windows.Forms.NumericUpDown();
            this.oLabel1 = new Ohana3DS_Rebirth.GUI.OLabel(this.components);
            this.oButtonPlay = new Ohana3DS_Rebirth.GUI.OButton();
            this.oButtonLoad = new Ohana3DS_Rebirth.GUI.OButton();
            this.openAnimDialog = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.animNumBox)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.animNumBox);
            this.groupBox1.Controls.Add(this.oLabel1);
            this.groupBox1.Controls.Add(this.oButtonPlay);
            this.groupBox1.Cursor = System.Windows.Forms.Cursors.Default;
            this.groupBox1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.groupBox1.Location = new System.Drawing.Point(0, 22);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(183, 147);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Controls";
            // 
            // animNumBox
            // 
            this.animNumBox.Enabled = false;
            this.animNumBox.Location = new System.Drawing.Point(46, 21);
            this.animNumBox.Name = "animNumBox";
            this.animNumBox.Size = new System.Drawing.Size(49, 22);
            this.animNumBox.TabIndex = 2;
            this.animNumBox.ValueChanged += new System.EventHandler(this.animNumBox_ValueChanged);
            // 
            // oLabel1
            // 
            this.oLabel1.AutomaticSize = false;
            this.oLabel1.Image = null;
            this.oLabel1.Location = new System.Drawing.Point(3, 22);
            this.oLabel1.Name = "oLabel1";
            this.oLabel1.Size = new System.Drawing.Size(37, 23);
            this.oLabel1.TabIndex = 1;
            this.oLabel1.Text = "Anim:";
            // 
            // oButtonPlay
            // 
            this.oButtonPlay.BackColor = System.Drawing.Color.Transparent;
            this.oButtonPlay.Enabled = false;
            this.oButtonPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.oButtonPlay.ForeColor = System.Drawing.Color.White;
            this.oButtonPlay.Location = new System.Drawing.Point(101, 20);
            this.oButtonPlay.Name = "oButtonPlay";
            this.oButtonPlay.Size = new System.Drawing.Size(75, 23);
            this.oButtonPlay.TabIndex = 0;
            this.oButtonPlay.Text = "Play";
            this.oButtonPlay.UseVisualStyleBackColor = false;
            this.oButtonPlay.Click += new System.EventHandler(this.oButtonPlay_Click);
            // 
            // oButtonLoad
            // 
            this.oButtonLoad.BackColor = System.Drawing.Color.Transparent;
            this.oButtonLoad.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.oButtonLoad.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.oButtonLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.oButtonLoad.ForeColor = System.Drawing.Color.White;
            this.oButtonLoad.Location = new System.Drawing.Point(3, 175);
            this.oButtonLoad.Name = "oButtonLoad";
            this.oButtonLoad.Size = new System.Drawing.Size(180, 23);
            this.oButtonLoad.TabIndex = 2;
            this.oButtonLoad.Text = "Load Animations";
            this.oButtonLoad.UseVisualStyleBackColor = false;
            this.oButtonLoad.Click += new System.EventHandler(this.oButtonLoad_Click);
            // 
            // OAnimWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.oButtonLoad);
            this.Controls.Add(this.groupBox1);
            this.Name = "OAnimWindow";
            this.Size = new System.Drawing.Size(188, 222);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.oButtonLoad, 0);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.animNumBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private OButton oButtonPlay;
        private OLabel oLabel1;
        private System.Windows.Forms.NumericUpDown animNumBox;
        private OButton oButtonLoad;
        private System.Windows.Forms.OpenFileDialog openAnimDialog;
    }
}
