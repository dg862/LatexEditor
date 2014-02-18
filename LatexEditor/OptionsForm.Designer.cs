namespace LatexEditor
{
    partial class OptionsForm
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
            this.cancelBtn = new System.Windows.Forms.Button();
            this.okBtn = new System.Windows.Forms.Button();
            this.scanBtn = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.distroCb = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.distPathTb = new System.Windows.Forms.TextBox();
            this.browseBtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.compilerPathTb = new System.Windows.Forms.TextBox();
            this.compPathBrowseBtn = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.compilerArgsTb = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.imagickPathTb = new System.Windows.Forms.TextBox();
            this.imagickPathBtn = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.tempFilesTb = new System.Windows.Forms.TextBox();
            this.tempFilesBtn = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.previewArgsTb = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.previewCodeTb = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(610, 535);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelBtn.TabIndex = 0;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // okBtn
            // 
            this.okBtn.Location = new System.Drawing.Point(529, 535);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(75, 23);
            this.okBtn.TabIndex = 1;
            this.okBtn.Text = "OK";
            this.okBtn.UseVisualStyleBackColor = true;
            this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
            // 
            // scanBtn
            // 
            this.scanBtn.Location = new System.Drawing.Point(6, 6);
            this.scanBtn.Name = "scanBtn";
            this.scanBtn.Size = new System.Drawing.Size(101, 50);
            this.scanBtn.TabIndex = 2;
            this.scanBtn.Text = "Scan for LaTeX distributions";
            this.scanBtn.UseVisualStyleBackColor = true;
            this.scanBtn.Click += new System.EventHandler(this.scanBtn_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(673, 517);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.previewCodeTb);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.previewArgsTb);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.tempFilesBtn);
            this.tabPage1.Controls.Add(this.tempFilesTb);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.imagickPathBtn);
            this.tabPage1.Controls.Add(this.imagickPathTb);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.compilerArgsTb);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.compPathBrowseBtn);
            this.tabPage1.Controls.Add(this.compilerPathTb);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.browseBtn);
            this.tabPage1.Controls.Add(this.distPathTb);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.distroCb);
            this.tabPage1.Controls.Add(this.scanBtn);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(665, 491);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Paths";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(665, 491);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // distroCb
            // 
            this.distroCb.FormattingEnabled = true;
            this.distroCb.Location = new System.Drawing.Point(134, 35);
            this.distroCb.Name = "distroCb";
            this.distroCb.Size = new System.Drawing.Size(121, 21);
            this.distroCb.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(131, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Distribution to use:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(290, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Distribution path:";
            // 
            // distPathTb
            // 
            this.distPathTb.Location = new System.Drawing.Point(293, 35);
            this.distPathTb.Name = "distPathTb";
            this.distPathTb.Size = new System.Drawing.Size(278, 20);
            this.distPathTb.TabIndex = 6;
            // 
            // browseBtn
            // 
            this.browseBtn.Location = new System.Drawing.Point(577, 32);
            this.browseBtn.Name = "browseBtn";
            this.browseBtn.Size = new System.Drawing.Size(75, 23);
            this.browseBtn.TabIndex = 7;
            this.browseBtn.Text = "Browse...";
            this.browseBtn.UseVisualStyleBackColor = true;
            this.browseBtn.Click += new System.EventHandler(this.browseBtn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Compiler path:";
            // 
            // compilerPathTb
            // 
            this.compilerPathTb.Location = new System.Drawing.Point(134, 83);
            this.compilerPathTb.Name = "compilerPathTb";
            this.compilerPathTb.Size = new System.Drawing.Size(437, 20);
            this.compilerPathTb.TabIndex = 9;
            // 
            // compPathBrowseBtn
            // 
            this.compPathBrowseBtn.Location = new System.Drawing.Point(577, 81);
            this.compPathBrowseBtn.Name = "compPathBrowseBtn";
            this.compPathBrowseBtn.Size = new System.Drawing.Size(75, 23);
            this.compPathBrowseBtn.TabIndex = 10;
            this.compPathBrowseBtn.Text = "Browse...";
            this.compPathBrowseBtn.UseVisualStyleBackColor = true;
            this.compPathBrowseBtn.Click += new System.EventHandler(this.compPathBrowseBtn_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 123);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Compiler arguments:";
            // 
            // compilerArgsTb
            // 
            this.compilerArgsTb.Location = new System.Drawing.Point(134, 120);
            this.compilerArgsTb.Name = "compilerArgsTb";
            this.compilerArgsTb.Size = new System.Drawing.Size(518, 20);
            this.compilerArgsTb.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 163);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "ImageMagick path:";
            // 
            // imagickPathTb
            // 
            this.imagickPathTb.Location = new System.Drawing.Point(134, 160);
            this.imagickPathTb.Name = "imagickPathTb";
            this.imagickPathTb.Size = new System.Drawing.Size(437, 20);
            this.imagickPathTb.TabIndex = 14;
            // 
            // imagickPathBtn
            // 
            this.imagickPathBtn.Location = new System.Drawing.Point(577, 158);
            this.imagickPathBtn.Name = "imagickPathBtn";
            this.imagickPathBtn.Size = new System.Drawing.Size(75, 23);
            this.imagickPathBtn.TabIndex = 15;
            this.imagickPathBtn.Text = "Browse...";
            this.imagickPathBtn.UseVisualStyleBackColor = true;
            this.imagickPathBtn.Click += new System.EventHandler(this.imagickPathBtn_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 209);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Temporary files:";
            // 
            // tempFilesTb
            // 
            this.tempFilesTb.Location = new System.Drawing.Point(134, 206);
            this.tempFilesTb.Name = "tempFilesTb";
            this.tempFilesTb.Size = new System.Drawing.Size(437, 20);
            this.tempFilesTb.TabIndex = 17;
            // 
            // tempFilesBtn
            // 
            this.tempFilesBtn.Location = new System.Drawing.Point(577, 204);
            this.tempFilesBtn.Name = "tempFilesBtn";
            this.tempFilesBtn.Size = new System.Drawing.Size(75, 23);
            this.tempFilesBtn.TabIndex = 18;
            this.tempFilesBtn.Text = "Browse...";
            this.tempFilesBtn.UseVisualStyleBackColor = true;
            this.tempFilesBtn.Click += new System.EventHandler(this.tempFilesBtn_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 250);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 13);
            this.label7.TabIndex = 19;
            this.label7.Text = "Preview arguments:";
            // 
            // previewArgsTb
            // 
            this.previewArgsTb.Location = new System.Drawing.Point(134, 247);
            this.previewArgsTb.Name = "previewArgsTb";
            this.previewArgsTb.Size = new System.Drawing.Size(518, 20);
            this.previewArgsTb.TabIndex = 20;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 289);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(75, 13);
            this.label8.TabIndex = 21;
            this.label8.Text = "Preview code:";
            // 
            // previewCodeTb
            // 
            this.previewCodeTb.Location = new System.Drawing.Point(134, 286);
            this.previewCodeTb.Multiline = true;
            this.previewCodeTb.Name = "previewCodeTb";
            this.previewCodeTb.Size = new System.Drawing.Size(518, 199);
            this.previewCodeTb.TabIndex = 22;
            // 
            // OptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(697, 570);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.okBtn);
            this.Controls.Add(this.cancelBtn);
            this.Name = "OptionsForm";
            this.Text = "OptionsForm";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.Button okBtn;
        private System.Windows.Forms.Button scanBtn;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox distroCb;
        private System.Windows.Forms.Button browseBtn;
        private System.Windows.Forms.TextBox distPathTb;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button compPathBrowseBtn;
        private System.Windows.Forms.TextBox compilerPathTb;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox compilerArgsTb;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button imagickPathBtn;
        private System.Windows.Forms.TextBox imagickPathTb;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button tempFilesBtn;
        private System.Windows.Forms.TextBox tempFilesTb;
        private System.Windows.Forms.TextBox previewArgsTb;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox previewCodeTb;
        private System.Windows.Forms.Label label8;
    }
}