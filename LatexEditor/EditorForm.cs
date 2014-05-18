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

		private void compileSelectedBtn_Click(object sender, EventArgs e)
		{
			App.Instance.CompileSelection();
		}

		private void boldBtn_Click(object sender, EventArgs e)
		{
			App.Instance.Bold();
		}

		private void underlinedBtn_Click(object sender, EventArgs e)
		{
			App.Instance.Underlined();
		}

		private void italicsBtn_Click(object sender, EventArgs e)
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

		private void searchBtn_Click(object sender, EventArgs e)
		{
			App.Instance.Search(searchCombo.Text);
		}

		private void searchBackBtn_Click(object sender, EventArgs e)
		{

		}

		private void alphaToolStripMenuItem_Click(object sender, EventArgs e)
		{
			App.Instance.Insert("\\alpha");
		}

		private void betaToolStripMenuItem_Click(object sender, EventArgs e)
		{
			App.Instance.Insert("\\beta");
		}

		private void gammaToolStripMenuItem_Click(object sender, EventArgs e)
		{
			App.Instance.Insert("\\gamma");
		}

		private void deltaToolStripMenuItem_Click(object sender, EventArgs e)
		{
			App.Instance.Insert("\\delta");
		}

		private void epsilonToolStripMenuItem_Click(object sender, EventArgs e)
		{
			App.Instance.Insert("\\epsilon");
		}

		private void zetaToolStripMenuItem_Click(object sender, EventArgs e)
		{
			App.Instance.Insert("\\zeta");
		}

		private void etaToolStripMenuItem_Click(object sender, EventArgs e)
		{
			App.Instance.Insert("\\eta");
		}

		private void thetaToolStripMenuItem_Click(object sender, EventArgs e)
		{
			App.Instance.Insert("\\theta");
		}

		private void iotaToolStripMenuItem_Click(object sender, EventArgs e)
		{
			App.Instance.Insert("\\iota");
		}

		private void kappaToolStripMenuItem_Click(object sender, EventArgs e)
		{
			App.Instance.Insert("\\kappa");
		}

		private void lamdaToolStripMenuItem_Click(object sender, EventArgs e)
		{
			App.Instance.Insert("\\lambda");
		}

		private void muToolStripMenuItem_Click(object sender, EventArgs e)
		{
			App.Instance.Insert("\\mu");
		}

		private void nuToolStripMenuItem_Click(object sender, EventArgs e)
		{
			App.Instance.Insert("\\nu");
		}

		private void xiToolStripMenuItem_Click(object sender, EventArgs e)
		{
			App.Instance.Insert("\\xi");
		}

		private void rhoToolStripMenuItem_Click(object sender, EventArgs e)
		{
			App.Instance.Insert("\\rho");
		}

		private void sigmaToolStripMenuItem_Click(object sender, EventArgs e)
		{
			App.Instance.Insert("\\sigma");
		}

		private void tauToolStripMenuItem_Click(object sender, EventArgs e)
		{
			App.Instance.Insert("\\tau");
		}

		private void upsilonToolStripMenuItem_Click(object sender, EventArgs e)
		{
			App.Instance.Insert("\\upsilon");
		}

		private void phiToolStripMenuItem_Click(object sender, EventArgs e)
		{
			App.Instance.Insert("\\phi");
		}

		private void chiToolStripMenuItem_Click(object sender, EventArgs e)
		{
			App.Instance.Insert("\\chi");
		}

		private void psiToolStripMenuItem_Click(object sender, EventArgs e)
		{
			App.Instance.Insert("\\psi");
		}

		private void omegaToolStripMenuItem_Click(object sender, EventArgs e)
		{
			App.Instance.Insert("\\omega");
		}

		private void arccosToolStripMenuItem_Click(object sender, EventArgs e)
		{
			App.Instance.Insert("\\arccos");
		}

		private void arcsinToolStripMenuItem_Click(object sender, EventArgs e)
		{
			App.Instance.Insert("\\arcsin");
		}

		private void arctanToolStripMenuItem_Click(object sender, EventArgs e)
		{
			App.Instance.Insert("\\arctan");
		}

		private void argToolStripMenuItem_Click(object sender, EventArgs e)
		{
			App.Instance.Insert("\\arg");
		}

		private void cosToolStripMenuItem_Click(object sender, EventArgs e)
		{
			App.Instance.Insert("\\cos");
		}

		private void coshToolStripMenuItem_Click(object sender, EventArgs e)
		{
			App.Instance.Insert("\\cosh");
		}

		private void cotToolStripMenuItem_Click(object sender, EventArgs e)
		{
			App.Instance.Insert("\\cot");
		}

		private void cothToolStripMenuItem_Click(object sender, EventArgs e)
		{
			App.Instance.Insert("\\coth");
		}

		private void cscToolStripMenuItem_Click(object sender, EventArgs e)
		{
			App.Instance.Insert("\\csc");
		}

		private void degToolStripMenuItem_Click(object sender, EventArgs e)
		{
			App.Instance.Insert("\\deg");
		}

		private void detToolStripMenuItem_Click(object sender, EventArgs e)
		{
			App.Instance.Insert("\\det");
		}

		private void dimToolStripMenuItem_Click(object sender, EventArgs e)
		{
			App.Instance.Insert("\\dim");
		}

		private void expToolStripMenuItem_Click(object sender, EventArgs e)
		{
			App.Instance.Insert("\\exp");
		}

		private void gcdToolStripMenuItem_Click(object sender, EventArgs e)
		{
			App.Instance.Insert("\\gcd");
		}

		private void homToolStripMenuItem_Click(object sender, EventArgs e)
		{
			App.Instance.Insert("\\hom");
		}

		private void infToolStripMenuItem_Click(object sender, EventArgs e)
		{
			App.Instance.Insert("\\inf");
		}

		private void kerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			App.Instance.Insert("\\ker");
		}

		private void lgToolStripMenuItem_Click(object sender, EventArgs e)
		{
			App.Instance.Insert("\\lg");
		}

		private void limToolStripMenuItem_Click(object sender, EventArgs e)
		{
			App.Instance.Insert("\\lim");
		}

		private void liminfToolStripMenuItem_Click(object sender, EventArgs e)
		{
			App.Instance.Insert("\\liminf");
		}

		private void limsupToolStripMenuItem_Click(object sender, EventArgs e)
		{
			App.Instance.Insert("\\limsup");
		}

		private void lnToolStripMenuItem_Click(object sender, EventArgs e)
		{
			App.Instance.Insert("\\ln");
		}

		private void logToolStripMenuItem_Click(object sender, EventArgs e)
		{
			App.Instance.Insert("\\log");
		}

		private void maxToolStripMenuItem_Click(object sender, EventArgs e)
		{
			App.Instance.Insert("\\max");
		}

		private void minToolStripMenuItem_Click(object sender, EventArgs e)
		{
			App.Instance.Insert("\\min");
		}

		private void prToolStripMenuItem_Click(object sender, EventArgs e)
		{
			App.Instance.Insert("\\Pr");
		}

		private void secToolStripMenuItem_Click(object sender, EventArgs e)
		{
			App.Instance.Insert("\\sec");
		}

		private void sinToolStripMenuItem_Click(object sender, EventArgs e)
		{
			App.Instance.Insert("\\sin");
		}

		private void sinhToolStripMenuItem_Click(object sender, EventArgs e)
		{
			App.Instance.Insert("\\sinh");
		}

		private void supToolStripMenuItem_Click(object sender, EventArgs e)
		{
			App.Instance.Insert("\\sup");
		}

		private void tanToolStripMenuItem_Click(object sender, EventArgs e)
		{
			App.Instance.Insert("\\tan");
		}

		private void tanhToolStripMenuItem_Click(object sender, EventArgs e)
		{
			App.Instance.Insert("\\tanh");
		}

		private void previewBtn_Click(object sender, EventArgs e)
		{
			App.Instance.Preview();
		}

		private void commentToggleBtn_Click(object sender, EventArgs e)
		{

		}


		#endregion		

	}
}
