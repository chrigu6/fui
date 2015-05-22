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
            ((System.ComponentModel.ISupportInitialize)(this.axAcroPDF1)).BeginInit();
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
            this.axAcroPDF1.Location = new System.Drawing.Point(461, 39);
            this.axAcroPDF1.Name = "axAcroPDF1";
            this.axAcroPDF1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axAcroPDF1.OcxState")));
            this.axAcroPDF1.Size = new System.Drawing.Size(795, 1147);
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1268, 1198);
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
    }
}

