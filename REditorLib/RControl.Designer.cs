namespace REditorLib
{
	partial class RControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose( bool disposing )
		{
			if ( disposing && ( components != null ) )
			{
				components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.editorSourceTabControl = new System.Windows.Forms.TabControl();
			this.sourcePage = new System.Windows.Forms.TabPage();
			this.sourceScrollbar = new System.Windows.Forms.VScrollBar();
			this.sourceTextBox = new PapyrusDictionary.PapyrusRichTextBox();
			this.editorPage = new System.Windows.Forms.TabPage();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.outputViewer = new System.Windows.Forms.WebBrowser();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.editorSourceTabControl.SuspendLayout();
			this.sourcePage.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.editorSourceTabControl);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
			this.splitContainer1.Size = new System.Drawing.Size(722, 396);
			this.splitContainer1.SplitterDistance = 283;
			this.splitContainer1.SplitterWidth = 8;
			this.splitContainer1.TabIndex = 0;
			// 
			// editorSourceTabControl
			// 
			this.editorSourceTabControl.Controls.Add(this.sourcePage);
			this.editorSourceTabControl.Controls.Add(this.editorPage);
			this.editorSourceTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.editorSourceTabControl.Location = new System.Drawing.Point(0, 0);
			this.editorSourceTabControl.Name = "editorSourceTabControl";
			this.editorSourceTabControl.SelectedIndex = 0;
			this.editorSourceTabControl.Size = new System.Drawing.Size(283, 396);
			this.editorSourceTabControl.TabIndex = 0;
			// 
			// sourcePage
			// 
			this.sourcePage.Controls.Add(this.sourceScrollbar);
			this.sourcePage.Controls.Add(this.sourceTextBox);
			this.sourcePage.Location = new System.Drawing.Point(4, 22);
			this.sourcePage.Name = "sourcePage";
			this.sourcePage.Padding = new System.Windows.Forms.Padding(3);
			this.sourcePage.Size = new System.Drawing.Size(275, 370);
			this.sourcePage.TabIndex = 0;
			this.sourcePage.Text = "Source";
			this.sourcePage.UseVisualStyleBackColor = true;
			// 
			// sourceScrollbar
			// 
			this.sourceScrollbar.Dock = System.Windows.Forms.DockStyle.Right;
			this.sourceScrollbar.Location = new System.Drawing.Point(255, 3);
			this.sourceScrollbar.Name = "sourceScrollbar";
			this.sourceScrollbar.Size = new System.Drawing.Size(17, 364);
			this.sourceScrollbar.TabIndex = 1;
			// 
			// sourceTextBox
			// 
			this.sourceTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.sourceTextBox.CurrentID = "0";
			this.sourceTextBox.CurrentLine = 0;
			this.sourceTextBox.CurrentTranslation = "";
			this.sourceTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.sourceTextBox.HasTooltip = false;
			this.sourceTextBox.Location = new System.Drawing.Point(3, 3);
			this.sourceTextBox.MaxToolTipDistance = ((uint)(50u));
			this.sourceTextBox.MaxTooltipSize = new System.Drawing.Size(0, 0);
			this.sourceTextBox.MinTooltipSize = new System.Drawing.Size(0, 0);
			this.sourceTextBox.Name = "sourceTextBox";
			this.sourceTextBox.NeedsRecompilation = false;
			this.sourceTextBox.PapyrusText = "";
			this.sourceTextBox.ScrollBar = null;
			this.sourceTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
			this.sourceTextBox.Size = new System.Drawing.Size(269, 364);
			this.sourceTextBox.Spacing = false;
			this.sourceTextBox.TabIndex = 0;
			this.sourceTextBox.Text = "";
			this.sourceTextBox.TooltipOffset = 0;
			// 
			// editorPage
			// 
			this.editorPage.Location = new System.Drawing.Point(4, 22);
			this.editorPage.Name = "editorPage";
			this.editorPage.Padding = new System.Windows.Forms.Padding(3);
			this.editorPage.Size = new System.Drawing.Size(308, 370);
			this.editorPage.TabIndex = 1;
			this.editorPage.Text = "Editor";
			this.editorPage.UseVisualStyleBackColor = true;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pictureBox1.Location = new System.Drawing.Point(3, 3);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(417, 364);
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(431, 396);
			this.tabControl1.TabIndex = 1;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.outputViewer);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(423, 370);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Output";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.pictureBox1);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(423, 370);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Preview";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// outputViewer
			// 
			this.outputViewer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.outputViewer.Location = new System.Drawing.Point(3, 3);
			this.outputViewer.MinimumSize = new System.Drawing.Size(20, 20);
			this.outputViewer.Name = "outputViewer";
			this.outputViewer.Size = new System.Drawing.Size(417, 364);
			this.outputViewer.TabIndex = 0;
			// 
			// RControl
			// 
			this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Controls.Add(this.splitContainer1);
			this.Name = "RControl";
			this.Size = new System.Drawing.Size(722, 396);
			this.Enter += new System.EventHandler(this.RControl_Enter);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RControl_KeyDown);
			this.Leave += new System.EventHandler(this.RControl_Leave);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.editorSourceTabControl.ResumeLayout(false);
			this.sourcePage.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.TabControl editorSourceTabControl;
		private System.Windows.Forms.TabPage sourcePage;
		private System.Windows.Forms.TabPage editorPage;
		private PapyrusDictionary.PapyrusRichTextBox sourceTextBox;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.VScrollBar sourceScrollbar;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.WebBrowser outputViewer;
	}
}
