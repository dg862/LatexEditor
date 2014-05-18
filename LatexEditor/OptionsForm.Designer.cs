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
			this.previewCodeTb = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.previewArgsTb = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.tempFilesBtn = new System.Windows.Forms.Button();
			this.tempFilesTb = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.imagickPathBtn = new System.Windows.Forms.Button();
			this.imagickPathTb = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.compilerArgsTb = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.compPathBrowseBtn = new System.Windows.Forms.Button();
			this.compilerPathTb = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.browseBtn = new System.Windows.Forms.Button();
			this.distPathTb = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.distroCb = new System.Windows.Forms.ComboBox();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.saveSnipBtn = new System.Windows.Forms.Button();
			this.newSnippetTb = new System.Windows.Forms.TextBox();
			this.addNewNodeBtn = new System.Windows.Forms.Button();
			this.snippetRtb = new System.Windows.Forms.RichTextBox();
			this.snippetTreeView = new System.Windows.Forms.TreeView();
			this.label9 = new System.Windows.Forms.Label();
			this.texifyPathTb = new System.Windows.Forms.TextBox();
			this.texifyPathBrowseBtn = new System.Windows.Forms.Button();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.SuspendLayout();
			// 
			// cancelBtn
			// 
			this.cancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelBtn.Location = new System.Drawing.Point(610, 576);
			this.cancelBtn.Name = "cancelBtn";
			this.cancelBtn.Size = new System.Drawing.Size(75, 23);
			this.cancelBtn.TabIndex = 0;
			this.cancelBtn.Text = "Cancel";
			this.cancelBtn.UseVisualStyleBackColor = true;
			this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
			// 
			// okBtn
			// 
			this.okBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okBtn.Location = new System.Drawing.Point(529, 576);
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
			this.tabControl1.Size = new System.Drawing.Size(673, 558);
			this.tabControl1.TabIndex = 3;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.texifyPathBrowseBtn);
			this.tabPage1.Controls.Add(this.texifyPathTb);
			this.tabPage1.Controls.Add(this.label9);
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
			this.tabPage1.Size = new System.Drawing.Size(665, 532);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Paths";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// previewCodeTb
			// 
			this.previewCodeTb.Location = new System.Drawing.Point(134, 311);
			this.previewCodeTb.Multiline = true;
			this.previewCodeTb.Name = "previewCodeTb";
			this.previewCodeTb.Size = new System.Drawing.Size(518, 199);
			this.previewCodeTb.TabIndex = 22;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(6, 314);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(75, 13);
			this.label8.TabIndex = 21;
			this.label8.Text = "Preview code:";
			// 
			// previewArgsTb
			// 
			this.previewArgsTb.Location = new System.Drawing.Point(134, 272);
			this.previewArgsTb.Name = "previewArgsTb";
			this.previewArgsTb.Size = new System.Drawing.Size(518, 20);
			this.previewArgsTb.TabIndex = 20;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(6, 275);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(100, 13);
			this.label7.TabIndex = 19;
			this.label7.Text = "Preview arguments:";
			// 
			// tempFilesBtn
			// 
			this.tempFilesBtn.Location = new System.Drawing.Point(577, 229);
			this.tempFilesBtn.Name = "tempFilesBtn";
			this.tempFilesBtn.Size = new System.Drawing.Size(75, 23);
			this.tempFilesBtn.TabIndex = 18;
			this.tempFilesBtn.Text = "Browse...";
			this.tempFilesBtn.UseVisualStyleBackColor = true;
			this.tempFilesBtn.Click += new System.EventHandler(this.tempFilesBtn_Click);
			// 
			// tempFilesTb
			// 
			this.tempFilesTb.Location = new System.Drawing.Point(134, 231);
			this.tempFilesTb.Name = "tempFilesTb";
			this.tempFilesTb.Size = new System.Drawing.Size(437, 20);
			this.tempFilesTb.TabIndex = 17;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(6, 234);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(81, 13);
			this.label6.TabIndex = 16;
			this.label6.Text = "Temporary files:";
			// 
			// imagickPathBtn
			// 
			this.imagickPathBtn.Location = new System.Drawing.Point(577, 190);
			this.imagickPathBtn.Name = "imagickPathBtn";
			this.imagickPathBtn.Size = new System.Drawing.Size(75, 23);
			this.imagickPathBtn.TabIndex = 15;
			this.imagickPathBtn.Text = "Browse...";
			this.imagickPathBtn.UseVisualStyleBackColor = true;
			this.imagickPathBtn.Click += new System.EventHandler(this.imagickPathBtn_Click);
			// 
			// imagickPathTb
			// 
			this.imagickPathTb.Location = new System.Drawing.Point(134, 192);
			this.imagickPathTb.Name = "imagickPathTb";
			this.imagickPathTb.Size = new System.Drawing.Size(437, 20);
			this.imagickPathTb.TabIndex = 14;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(6, 195);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(98, 13);
			this.label5.TabIndex = 13;
			this.label5.Text = "ImageMagick path:";
			// 
			// compilerArgsTb
			// 
			this.compilerArgsTb.Location = new System.Drawing.Point(134, 152);
			this.compilerArgsTb.Name = "compilerArgsTb";
			this.compilerArgsTb.Size = new System.Drawing.Size(518, 20);
			this.compilerArgsTb.TabIndex = 12;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(6, 155);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(102, 13);
			this.label4.TabIndex = 11;
			this.label4.Text = "Compiler arguments:";
			// 
			// compPathBrowseBtn
			// 
			this.compPathBrowseBtn.Location = new System.Drawing.Point(577, 113);
			this.compPathBrowseBtn.Name = "compPathBrowseBtn";
			this.compPathBrowseBtn.Size = new System.Drawing.Size(75, 23);
			this.compPathBrowseBtn.TabIndex = 10;
			this.compPathBrowseBtn.Text = "Browse...";
			this.compPathBrowseBtn.UseVisualStyleBackColor = true;
			this.compPathBrowseBtn.Click += new System.EventHandler(this.compPathBrowseBtn_Click);
			// 
			// compilerPathTb
			// 
			this.compilerPathTb.Location = new System.Drawing.Point(134, 115);
			this.compilerPathTb.Name = "compilerPathTb";
			this.compilerPathTb.Size = new System.Drawing.Size(437, 20);
			this.compilerPathTb.TabIndex = 9;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(6, 118);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(74, 13);
			this.label3.TabIndex = 8;
			this.label3.Text = "Compiler path:";
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
			// distPathTb
			// 
			this.distPathTb.Location = new System.Drawing.Point(293, 35);
			this.distPathTb.Name = "distPathTb";
			this.distPathTb.Size = new System.Drawing.Size(278, 20);
			this.distPathTb.TabIndex = 6;
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
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(131, 6);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(94, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "Distribution to use:";
			// 
			// distroCb
			// 
			this.distroCb.FormattingEnabled = true;
			this.distroCb.Location = new System.Drawing.Point(134, 35);
			this.distroCb.Name = "distroCb";
			this.distroCb.Size = new System.Drawing.Size(121, 21);
			this.distroCb.TabIndex = 3;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.saveSnipBtn);
			this.tabPage2.Controls.Add(this.newSnippetTb);
			this.tabPage2.Controls.Add(this.addNewNodeBtn);
			this.tabPage2.Controls.Add(this.snippetRtb);
			this.tabPage2.Controls.Add(this.snippetTreeView);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(665, 491);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Snippets";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// saveSnipBtn
			// 
			this.saveSnipBtn.Location = new System.Drawing.Point(343, 463);
			this.saveSnipBtn.Name = "saveSnipBtn";
			this.saveSnipBtn.Size = new System.Drawing.Size(87, 23);
			this.saveSnipBtn.TabIndex = 4;
			this.saveSnipBtn.Text = "Save changes";
			this.saveSnipBtn.UseVisualStyleBackColor = true;
			this.saveSnipBtn.Click += new System.EventHandler(this.saveSnipBtn_Click);
			// 
			// newSnippetTb
			// 
			this.newSnippetTb.Location = new System.Drawing.Point(134, 465);
			this.newSnippetTb.Name = "newSnippetTb";
			this.newSnippetTb.Size = new System.Drawing.Size(100, 20);
			this.newSnippetTb.TabIndex = 3;
			// 
			// addNewNodeBtn
			// 
			this.addNewNodeBtn.Location = new System.Drawing.Point(240, 463);
			this.addNewNodeBtn.Name = "addNewNodeBtn";
			this.addNewNodeBtn.Size = new System.Drawing.Size(97, 23);
			this.addNewNodeBtn.TabIndex = 2;
			this.addNewNodeBtn.Text = "Add new snippet";
			this.addNewNodeBtn.UseVisualStyleBackColor = true;
			this.addNewNodeBtn.Click += new System.EventHandler(this.addNewNodeBtn_Click);
			// 
			// snippetRtb
			// 
			this.snippetRtb.Location = new System.Drawing.Point(134, 7);
			this.snippetRtb.Name = "snippetRtb";
			this.snippetRtb.Size = new System.Drawing.Size(525, 452);
			this.snippetRtb.TabIndex = 1;
			this.snippetRtb.Text = "";
			// 
			// snippetTreeView
			// 
			this.snippetTreeView.Location = new System.Drawing.Point(6, 6);
			this.snippetTreeView.Name = "snippetTreeView";
			this.snippetTreeView.Size = new System.Drawing.Size(121, 482);
			this.snippetTreeView.TabIndex = 0;
			this.snippetTreeView.Click += new System.EventHandler(this.snippetTreeView_Click);
			this.snippetTreeView.DoubleClick += new System.EventHandler(this.snippetTreeView_DoubleClick);
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(6, 81);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(62, 13);
			this.label9.TabIndex = 23;
			this.label9.Text = "Texify path:";
			// 
			// texifyPathTb
			// 
			this.texifyPathTb.Location = new System.Drawing.Point(134, 78);
			this.texifyPathTb.Name = "texifyPathTb";
			this.texifyPathTb.Size = new System.Drawing.Size(437, 20);
			this.texifyPathTb.TabIndex = 24;
			// 
			// texifyPathBrowseBtn
			// 
			this.texifyPathBrowseBtn.Location = new System.Drawing.Point(577, 76);
			this.texifyPathBrowseBtn.Name = "texifyPathBrowseBtn";
			this.texifyPathBrowseBtn.Size = new System.Drawing.Size(75, 23);
			this.texifyPathBrowseBtn.TabIndex = 25;
			this.texifyPathBrowseBtn.Text = "Browse...";
			this.texifyPathBrowseBtn.UseVisualStyleBackColor = true;
			this.texifyPathBrowseBtn.Click += new System.EventHandler(this.texifyPathBrowseBtn_Click);
			// 
			// OptionsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(697, 611);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.okBtn);
			this.Controls.Add(this.cancelBtn);
			this.Name = "OptionsForm";
			this.Text = "OptionsForm";
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.tabPage2.ResumeLayout(false);
			this.tabPage2.PerformLayout();
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
		private System.Windows.Forms.RichTextBox snippetRtb;
		private System.Windows.Forms.TreeView snippetTreeView;
		private System.Windows.Forms.Button addNewNodeBtn;
		private System.Windows.Forms.TextBox newSnippetTb;
		private System.Windows.Forms.Button saveSnipBtn;
		private System.Windows.Forms.Button texifyPathBrowseBtn;
		private System.Windows.Forms.TextBox texifyPathTb;
		private System.Windows.Forms.Label label9;
    }
}