namespace Virtual_Library
{
    partial class Form1
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.bookmarkbutton = new System.Windows.Forms.Button();
            this.showBookmarkButton = new System.Windows.Forms.Button();
            this.axAcroPDF1 = new AxAcroPDFLib.AxAcroPDF();
            this.deleteBookmarkButton = new System.Windows.Forms.Button();
            this.activeBookLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.leftHandBox = new System.Windows.Forms.TextBox();
            this.rightHandBox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.axAcroPDF1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(376, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Search";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(358, 20);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "Type query";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.textBox1.Enter += new System.EventHandler(this.textBox1_Enter);
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.Location = new System.Drawing.Point(12, 36);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(439, 23);
            this.textBox2.TabIndex = 3;
            this.textBox2.Text = "Say \"New search\" to start a new search.";
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged_1);
            // 
            // bookmarkbutton
            // 
            this.bookmarkbutton.Location = new System.Drawing.Point(461, 10);
            this.bookmarkbutton.Name = "bookmarkbutton";
            this.bookmarkbutton.Size = new System.Drawing.Size(75, 23);
            this.bookmarkbutton.TabIndex = 4;
            this.bookmarkbutton.Text = "Bookmark";
            this.bookmarkbutton.UseVisualStyleBackColor = true;
            this.bookmarkbutton.Click += new System.EventHandler(this.bookmarkbutton_Click);
            // 
            // showBookmarkButton
            // 
            this.showBookmarkButton.Location = new System.Drawing.Point(542, 10);
            this.showBookmarkButton.Name = "showBookmarkButton";
            this.showBookmarkButton.Size = new System.Drawing.Size(99, 23);
            this.showBookmarkButton.TabIndex = 6;
            this.showBookmarkButton.Text = "Show Bookmarks";
            this.showBookmarkButton.UseVisualStyleBackColor = true;
            this.showBookmarkButton.Click += new System.EventHandler(this.showBookmarkButton_Click);
            // 
            // axAcroPDF1
            // 
            this.axAcroPDF1.Enabled = true;
            this.axAcroPDF1.Location = new System.Drawing.Point(481, 48);
            this.axAcroPDF1.Name = "axAcroPDF1";
            this.axAcroPDF1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axAcroPDF1.OcxState")));
            this.axAcroPDF1.Size = new System.Drawing.Size(689, 578);
            this.axAcroPDF1.TabIndex = 7;
            // 
            // deleteBookmarkButton
            // 
            this.deleteBookmarkButton.Location = new System.Drawing.Point(647, 10);
            this.deleteBookmarkButton.Name = "deleteBookmarkButton";
            this.deleteBookmarkButton.Size = new System.Drawing.Size(97, 23);
            this.deleteBookmarkButton.TabIndex = 8;
            this.deleteBookmarkButton.Text = "Delete Bookmark";
            this.deleteBookmarkButton.UseVisualStyleBackColor = true;
            this.deleteBookmarkButton.Click += new System.EventHandler(this.deleteBookmarkButton_Click);
            // 
            // activeBookLabel
            // 
            this.activeBookLabel.AutoSize = true;
            this.activeBookLabel.Location = new System.Drawing.Point(750, 15);
            this.activeBookLabel.Name = "activeBookLabel";
            this.activeBookLabel.Size = new System.Drawing.Size(71, 13);
            this.activeBookLabel.TabIndex = 9;
            this.activeBookLabel.Text = "Active Book: ";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(120, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Right Hand:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Left Hand:";
            // 
            // leftHandBox
            // 
            this.leftHandBox.BackColor = System.Drawing.Color.Red;
            this.leftHandBox.Enabled = false;
            this.leftHandBox.Location = new System.Drawing.Point(9, 56);
            this.leftHandBox.Name = "leftHandBox";
            this.leftHandBox.Size = new System.Drawing.Size(34, 20);
            this.leftHandBox.TabIndex = 13;
            // 
            // rightHandBox
            // 
            this.rightHandBox.BackColor = System.Drawing.Color.Red;
            this.rightHandBox.Enabled = false;
            this.rightHandBox.Location = new System.Drawing.Point(135, 56);
            this.rightHandBox.Name = "rightHandBox";
            this.rightHandBox.Size = new System.Drawing.Size(36, 20);
            this.rightHandBox.TabIndex = 14;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rightHandBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.leftHandBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(12, 400);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 100);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tracking State";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1182, 638);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.activeBookLabel);
            this.Controls.Add(this.deleteBookmarkButton);
            this.Controls.Add(this.axAcroPDF1);
            this.Controls.Add(this.showBookmarkButton);
            this.Controls.Add(this.bookmarkbutton);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Virtual Library";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.axAcroPDF1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        public System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button bookmarkbutton;
        private System.Windows.Forms.Button showBookmarkButton;
        private AxAcroPDFLib.AxAcroPDF axAcroPDF1;
        private System.Windows.Forms.Button deleteBookmarkButton;
        private System.Windows.Forms.Label activeBookLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox leftHandBox;
        private System.Windows.Forms.TextBox rightHandBox;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}

