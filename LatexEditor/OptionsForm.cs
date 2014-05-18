using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LatexEditor
{
    public partial class OptionsForm : Form
    {
        #region Fields

        private OptionManager opManager;

        #endregion

        #region Properties

		public OptionManager OpManager
		{
			set
			{
				if (value != null)
				{
					opManager = value;

					snippetTreeView.Nodes.Clear();

					foreach (var item in opManager.SnippetDict)
					{
						snippetTreeView.Nodes.Add(item.Key);
					}

					//foreach (var item in opManager.SnippetDict)
					//{
					//	snippetTreeView.Nodes.Add(
					//}
				}
			}
		}

        public ComboBox DistroCombo
        {
            get { return this.distroCb; }
        }

        public TextBox DistPathTb
        {
            get { return this.distPathTb; }
        }

        public TextBox CompilerPathTb
        {
            get { return this.compilerPathTb; }
        }

        public TextBox CompilerArgsTb
        {
            get { return this.compilerArgsTb; }
        }

        public TextBox IMagickPathTb
        {
            get { return this.imagickPathTb; }
        }

        public TextBox TempFilesTb
        {
            get { return this.tempFilesTb; }
        }

        public TextBox PreviewArgsTb
        {
            get { return this.previewArgsTb; }
        }

        public TextBox PreviewCodeTb
        {
            get { return this.previewCodeTb; }
        }

		public TextBox TexifyPathTb
		{
			get { return this.texifyPathTb; }
		}

        //public OptionManager OpManager
        //{
        //    set
        //    {
        //        if (value != null)
        //        {
        //            opManager = value;
        //        }
        //    }
        //}

        #endregion

        #region Constructors

        public OptionsForm()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        public void CheckOptions()
        {
            //Check if we have selected a distribution to use in the options file.
            if (!string.IsNullOrEmpty(Constants.latexDistribution))
            {
                this.distroCb.Items.Add(Constants.latexDistribution);
                this.distroCb.Text = Constants.latexDistribution;
                this.distPathTb.Text = Constants.distributionPath;
				this.texifyPathTb.Text = Constants.texifyPath;
                this.compilerPathTb.Text = Constants.compilerName;
                this.compilerArgsTb.Text = Constants.compilerArgs;
                this.imagickPathTb.Text = Constants.imageMagickPath;
                this.tempFilesTb.Text = Constants.scratchPadPath;
                this.previewCodeTb.Text = Constants.previewCode;
                this.previewArgsTb.Text = Constants.previewArgs;
            }
        }

        #endregion

        #region Event handlers

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void scanBtn_Click(object sender, EventArgs e)
        {
            App.Instance.ScanForLatexDistros();
        }

        private void browseBtn_Click(object sender, EventArgs e)
        {
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Multiselect = false;
			ofd.Filter = "Executables (*.exe)|*.exe|All files (*.*)|*.*";

			if (ofd.ShowDialog() == DialogResult.OK)
			{
				this.distPathTb.Text = ofd.FileName;
			}
        }

        private void compPathBrowseBtn_Click(object sender, EventArgs e)
        {
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Multiselect = false;
			ofd.Filter = "Executables (*.exe)|*.exe|All files (*.*)|*.*";

			if (ofd.ShowDialog() == DialogResult.OK)
			{
				this.compilerPathTb.Text = ofd.FileName;
			}
        }

        private void imagickPathBtn_Click(object sender, EventArgs e)
        {
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Multiselect = false;
			ofd.Filter = "Executables (*.exe)|*.exe|All files (*.*)|*.*";

			if (ofd.ShowDialog() == DialogResult.OK)
			{
				this.imagickPathTb.Text = ofd.FileName;
			}
        }

        private void tempFilesBtn_Click(object sender, EventArgs e)
        {
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Multiselect = false;
			ofd.Filter = "Executables (*.exe)|*.exe|All files (*.*)|*.*";

			if (ofd.ShowDialog() == DialogResult.OK)
			{
				this.tempFilesTb.Text = ofd.FileName;
			}
        }

		private void texifyPathBrowseBtn_Click(object sender, EventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Multiselect = false;
			ofd.Filter = "Executables (*.exe)|*.exe|All files (*.*)|*.*";

			if (ofd.ShowDialog() == DialogResult.OK)
			{
				this.texifyPathTb.Text = ofd.FileName;
			}
		}
		
		private void snippetTreeView_Click(object sender, EventArgs e)
		{
			//snippetRtb.Text = opManager.SnippetDict[snippetTreeView.SelectedNode.Text];
		}

		private void addNewNodeBtn_Click(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(newSnippetTb.Text))
			{
				snippetTreeView.Nodes.Add(newSnippetTb.Text);
			}

			opManager.SnippetDict[newSnippetTb.Text] = snippetRtb.Text;

			snippetRtb.Text = string.Empty;
			newSnippetTb.Text = string.Empty;
		}

		private void saveSnipBtn_Click(object sender, EventArgs e)
		{
			snippetTreeView.Nodes.Add(newSnippetTb.Text);
			opManager.SnippetDict[newSnippetTb.Text] = snippetRtb.Text;

			newSnippetTb.Text = string.Empty;

			//if (snippetTreeView.SelectedNode != null)
			//{
			//	opManager.SnippetDict[snippetTreeView.SelectedNode.Text] = snippetRtb.Text;
			//}

			//else
			//{
			//}
		}

		private void snippetTreeView_DoubleClick(object sender, EventArgs e)
		{
			try
			{
				snippetRtb.Text = opManager.SnippetDict[snippetTreeView.SelectedNode.Text];
			}
			catch (Exception)
			{

			}
		}

        #endregion
    }
}
