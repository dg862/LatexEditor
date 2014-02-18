using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LatexEditor
{
	public partial class EditorForm : Form
	{
		#region Fields

		#endregion

		#region Properties

		public Control SourceTextBox
		{
			get { return this.rControl.SourceTexBox; }
		}

		public Control LatexControl
		{
			get { return this.rControl; }
		}

		//public Control PagePreview
		//{
		//    get { return this.pagePreview; }
		//}

		public Control Browser
		{
			get { return this.rControl.OutputViewer; }
		}

		#endregion

		#region Constructors
		
		public EditorForm()
		{
			InitializeComponent();

			App.Instance.Initialize( this );
			//AppManager.Instance.Initialize( this );
		}

		#endregion

		#region Methods

		#endregion

		#region Event handlers

		private void compileBtn_Click( object sender, EventArgs e )
		{
			App.Instance.Compile();
			//AppManager.Instance.Compile();
		}

		private void openToolStripButton_Click( object sender, EventArgs e )
		{
			App.Instance.Open();
			//AppManager.Instance.Open();
		}

		private void saveToolStripButton_Click( object sender, EventArgs e )
		{
			App.Instance.Save();
			//AppManager.Instance.Save();
		}

		#endregion		

		private void compileSelectedBtn_Click( object sender, EventArgs e )
		{
			App.Instance.CompileSelection();
		}

		private void boldBtn_Click( object sender, EventArgs e )
		{
			App.Instance.Bold();
		}

		private void underlinedBtn_Click( object sender, EventArgs e )
		{
			App.Instance.Underlined();
		}

		private void italicsBtn_Click( object sender, EventArgs e )
		{
			App.Instance.Italics();
		}

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            App.Instance.ShowOptions();
        }

        private void piToolStripMenuItem_Click(object sender, EventArgs e)
        {
            App.Instance.InsertPi();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            App.Instance.Open();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            App.Instance.NewDocument();
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            App.Instance.NewDocument();
        }
	}
}
