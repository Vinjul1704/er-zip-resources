namespace ZipHelper
{
    partial class ZipHelper
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ZipHelper));
            this.labelBpm = new System.Windows.Forms.Label();
            this.trackBarBpm = new System.Windows.Forms.TrackBar();
            this.labelGuardButton = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.textBoxOffset = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxGuardButton = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBpm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // labelBpm
            // 
            this.labelBpm.AutoSize = true;
            this.labelBpm.Location = new System.Drawing.Point(12, 37);
            this.labelBpm.Name = "labelBpm";
            this.labelBpm.Size = new System.Drawing.Size(56, 15);
            this.labelBpm.TabIndex = 0;
            this.labelBpm.Text = "BPM: 100";
            // 
            // trackBarBpm
            // 
            this.trackBarBpm.CausesValidation = false;
            this.trackBarBpm.LargeChange = 1;
            this.trackBarBpm.Location = new System.Drawing.Point(12, 55);
            this.trackBarBpm.Maximum = 40;
            this.trackBarBpm.Name = "trackBarBpm";
            this.trackBarBpm.Size = new System.Drawing.Size(420, 45);
            this.trackBarBpm.TabIndex = 1;
            this.trackBarBpm.ValueChanged += new System.EventHandler(this.trackBarBpm_ValueChanged);
            // 
            // labelGuardButton
            // 
            this.labelGuardButton.AutoSize = true;
            this.labelGuardButton.Location = new System.Drawing.Point(12, 9);
            this.labelGuardButton.Name = "labelGuardButton";
            this.labelGuardButton.Size = new System.Drawing.Size(81, 15);
            this.labelGuardButton.TabIndex = 2;
            this.labelGuardButton.Text = "Guard Button:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Made by: HYP3RSOMNIAC";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(160, 98);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(18, 15);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pictureBox1.MouseEnter += new System.EventHandler(this.pictureBox1_MouseEnter);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(177, 97);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(24, 19);
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            this.pictureBox2.MouseEnter += new System.EventHandler(this.pictureBox2_MouseEnter);
            // 
            // textBoxOffset
            // 
            this.textBoxOffset.Location = new System.Drawing.Point(335, 6);
            this.textBoxOffset.Name = "textBoxOffset";
            this.textBoxOffset.Size = new System.Drawing.Size(70, 23);
            this.textBoxOffset.TabIndex = 6;
            this.textBoxOffset.Text = "0";
            this.textBoxOffset.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxOffset.TextChanged += new System.EventHandler(this.textBoxOffset_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(209, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 15);
            this.label2.TabIndex = 7;
            this.label2.Text = "Reduce inital beat by:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(411, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 15);
            this.label3.TabIndex = 8;
            this.label3.Text = "ms";
            // 
            // textBoxGuardButton
            // 
            this.textBoxGuardButton.Location = new System.Drawing.Point(99, 6);
            this.textBoxGuardButton.Name = "textBoxGuardButton";
            this.textBoxGuardButton.Size = new System.Drawing.Size(70, 23);
            this.textBoxGuardButton.TabIndex = 9;
            this.textBoxGuardButton.Text = "RButton";
            this.textBoxGuardButton.TextChanged += new System.EventHandler(this.textBoxGuardButton_TextChanged);
            // 
            // ZipHelper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 131);
            this.Controls.Add(this.textBoxGuardButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxOffset);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelGuardButton);
            this.Controls.Add(this.trackBarBpm);
            this.Controls.Add(this.labelBpm);
            this.Name = "ZipHelper";
            this.Text = "ZipHelper";
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBpm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label labelBpm;
        private TrackBar trackBarBpm;
        private Label labelGuardButton;
        private Label label1;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private TextBox textBoxOffset;
        private Label label2;
        private Label label3;
        private TextBox textBoxGuardButton;
    }
}