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
            if (!string.IsNullOrEmpty(REditorLib.Constants.latexDistribution))
            {
                this.distroCb.Items.Add(REditorLib.Constants.latexDistribution);
                this.distroCb.Text = REditorLib.Constants.latexDistribution;
                this.distPathTb.Text = REditorLib.Constants.distributionPath;
                this.compilerPathTb.Text = REditorLib.Constants.compilerPath;
                this.compilerArgsTb.Text = REditorLib.Constants.defaultCompilerArgs;
                this.imagickPathTb.Text = REditorLib.Constants.imageMagickPath;
                this.tempFilesTb.Text = REditorLib.Constants.scratchPadPath;
                this.previewCodeTb.Text = REditorLib.Constants.defaultPreviewCode;
                this.previewArgsTb.Text = REditorLib.Constants.defaultPreviewArgs;
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

        }

        private void compPathBrowseBtn_Click(object sender, EventArgs e)
        {

        }

        private void imagickPathBtn_Click(object sender, EventArgs e)
        {

        }

        private void tempFilesBtn_Click(object sender, EventArgs e)
        {

        }

        #endregion
    }
}
