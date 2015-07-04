namespace Ohana3DS_Rebirth.GUI
{
    partial class OAnimationsWindow
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
            this.Content = new Ohana3DS_Rebirth.GUI.OScrollablePanel();
            this.MaterialAnimationsGroup = new Ohana3DS_Rebirth.GUI.OGroupBox();
            this.MaterialAnimationControl = new Ohana3DS_Rebirth.GUI.OGenericAnimationControls();
            this.SkeletalAnimationsGroup = new Ohana3DS_Rebirth.GUI.OGroupBox();
            this.SkeletalAnimationControl = new Ohana3DS_Rebirth.GUI.OGenericAnimationControls();
            this.Content.ContentArea.SuspendLayout();
            this.Content.SuspendLayout();
            this.MaterialAnimationsGroup.ContentArea.SuspendLayout();
            this.MaterialAnimationsGroup.SuspendLayout();
            this.SkeletalAnimationsGroup.ContentArea.SuspendLayout();
            this.SkeletalAnimationsGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // Content
            // 
            this.Content.BackColor = System.Drawing.Color.Transparent;
            // 
            // Content.ContentArea
            // 
            this.Content.ContentArea.AutoSize = true;
            this.Content.ContentArea.BackColor = System.Drawing.Color.Transparent;
            this.Content.ContentArea.Controls.Add(this.MaterialAnimationsGroup);
            this.Content.ContentArea.Controls.Add(this.SkeletalAnimationsGroup);
            this.Content.ContentArea.Location = new System.Drawing.Point(0, 0);
            this.Content.ContentArea.Name = "ContentArea";
            this.Content.ContentArea.Size = new System.Drawing.Size(256, 512);
            this.Content.ContentArea.TabIndex = 0;
            this.Content.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Content.Location = new System.Drawing.Point(0, 16);
            this.Content.Name = "Content";
            this.Content.Size = new System.Drawing.Size(256, 784);
            this.Content.TabIndex = 1;
            // 
            // MaterialAnimationsGroup
            // 
            this.MaterialAnimationsGroup.BackColor = System.Drawing.Color.Black;
            // 
            // MaterialAnimationsGroup.ContentArea
            // 
            this.MaterialAnimationsGroup.ContentArea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.MaterialAnimationsGroup.ContentArea.Controls.Add(this.MaterialAnimationControl);
            this.MaterialAnimationsGroup.ContentArea.Location = new System.Drawing.Point(1, 16);
            this.MaterialAnimationsGroup.ContentArea.Name = "ContentArea";
            this.MaterialAnimationsGroup.ContentArea.Size = new System.Drawing.Size(254, 239);
            this.MaterialAnimationsGroup.ContentArea.TabIndex = 0;
            this.MaterialAnimationsGroup.ContentColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.MaterialAnimationsGroup.Dock = System.Windows.Forms.DockStyle.Top;
            this.MaterialAnimationsGroup.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaterialAnimationsGroup.Location = new System.Drawing.Point(0, 256);
            this.MaterialAnimationsGroup.Name = "MaterialAnimationsGroup";
            this.MaterialAnimationsGroup.Size = new System.Drawing.Size(256, 256);
            this.MaterialAnimationsGroup.TabIndex = 2;
            this.MaterialAnimationsGroup.Title = "Material Animations";
            // 
            // MaterialAnimationControl
            // 
            this.MaterialAnimationControl.BackColor = System.Drawing.Color.Transparent;
            this.MaterialAnimationControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MaterialAnimationControl.ForeColor = System.Drawing.Color.White;
            this.MaterialAnimationControl.Location = new System.Drawing.Point(0, 0);
            this.MaterialAnimationControl.Name = "MaterialAnimationControl";
            this.MaterialAnimationControl.Size = new System.Drawing.Size(254, 239);
            this.MaterialAnimationControl.TabIndex = 0;
            // 
            // SkeletalAnimationsGroup
            // 
            this.SkeletalAnimationsGroup.BackColor = System.Drawing.Color.Black;
            // 
            // SkeletalAnimationsGroup.ContentArea
            // 
            this.SkeletalAnimationsGroup.ContentArea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.SkeletalAnimationsGroup.ContentArea.Controls.Add(this.SkeletalAnimationControl);
            this.SkeletalAnimationsGroup.ContentArea.Location = new System.Drawing.Point(1, 16);
            this.SkeletalAnimationsGroup.ContentArea.Name = "ContentArea";
            this.SkeletalAnimationsGroup.ContentArea.Size = new System.Drawing.Size(254, 239);
            this.SkeletalAnimationsGroup.ContentArea.TabIndex = 0;
            this.SkeletalAnimationsGroup.ContentColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.SkeletalAnimationsGroup.Dock = System.Windows.Forms.DockStyle.Top;
            this.SkeletalAnimationsGroup.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SkeletalAnimationsGroup.Location = new System.Drawing.Point(0, 0);
            this.SkeletalAnimationsGroup.Name = "SkeletalAnimationsGroup";
            this.SkeletalAnimationsGroup.Size = new System.Drawing.Size(256, 256);
            this.SkeletalAnimationsGroup.TabIndex = 5;
            this.SkeletalAnimationsGroup.Title = "Skeletal Animations";
            // 
            // SkeletalAnimationControl
            // 
            this.SkeletalAnimationControl.BackColor = System.Drawing.Color.Transparent;
            this.SkeletalAnimationControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SkeletalAnimationControl.ForeColor = System.Drawing.Color.White;
            this.SkeletalAnimationControl.Location = new System.Drawing.Point(0, 0);
            this.SkeletalAnimationControl.Name = "SkeletalAnimationControl";
            this.SkeletalAnimationControl.Size = new System.Drawing.Size(254, 239);
            this.SkeletalAnimationControl.TabIndex = 0;
            // 
            // OAnimationsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Content);
            this.Name = "OAnimationsWindow";
            this.Size = new System.Drawing.Size(256, 800);
            this.Controls.SetChildIndex(this.Content, 0);
            this.Content.ContentArea.ResumeLayout(false);
            this.Content.ResumeLayout(false);
            this.Content.PerformLayout();
            this.MaterialAnimationsGroup.ContentArea.ResumeLayout(false);
            this.MaterialAnimationsGroup.ResumeLayout(false);
            this.MaterialAnimationsGroup.PerformLayout();
            this.SkeletalAnimationsGroup.ContentArea.ResumeLayout(false);
            this.SkeletalAnimationsGroup.ResumeLayout(false);
            this.SkeletalAnimationsGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private OScrollablePanel Content;
        private OGroupBox SkeletalAnimationsGroup;
        private OGroupBox MaterialAnimationsGroup;
        private OGenericAnimationControls MaterialAnimationControl;
        private OGenericAnimationControls SkeletalAnimationControl;

    }
}
