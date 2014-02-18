using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PapyrusDictionary;

namespace LatexEditor
{
	class App
	{
		#region Fields

		private static Random randomSupplier = new Random();
		private static object randomSupplierLockObject = new object();
		private static App theApp = new App();
		private EditorForm editorForm;
        private OptionsForm optionForm;
		private LatexManager manager;

		#endregion

		#region Properties

		public static App Instance
		{
			get { return theApp; }
		}

		public EditorForm Editor
		{
			get { return editorForm; }
		}

		public LatexManager Manager
		{
			get { return manager; }
		}

		public static int RandomNumber
		{
			get
			{
				lock ( randomSupplierLockObject )
				{
					return randomSupplier.Next();
				}
			}
		}

        public OptionsForm OptionForm
        {
            get { return optionForm; }
        }

		#endregion

		#region Constructors

		#endregion

		#region Methods

        public void ShowOptions()
        {
            //optionForm.OpManager = 

            optionForm.CheckOptions();

            if (optionForm.ShowDialog() == DialogResult.OK)
            {
                manager.SaveOptions();
            }
        }

        public void ScanForLatexDistros()
        {
            manager.ScanForLatexDistros();
        }

		public void Initialize( EditorForm form )
		{
			theApp.editorForm = form;
            theApp.optionForm = new OptionsForm();
			manager = new LatexManager();
		}

		public void Compile()
		{
			manager.CompileSourceCode();
		}

		public void CompileSelection()
		{
			manager.CompileSelection();
		}

		public void Open()
		{
			OpenFileDialog ofd	= new OpenFileDialog();
			ofd.Multiselect = false;
			ofd.Filter = "TeX Files (*.tex)|*.tex";

			if ( ofd.ShowDialog() == DialogResult.OK )
			{
				manager.OpenFile( ofd.FileName );
			}
		}

		public void Save()
		{
			SaveFileDialog sfd	= new SaveFileDialog();
			sfd.Filter = "TeX files (*.tex)|*.tex";

			if ( sfd.ShowDialog() == DialogResult.OK )
			{
				manager.SaveFile( sfd.FileName );
			}
		}

		public void Bold()
		{
			manager.Bold();
		}

		public void Underlined()
		{
			manager.Underlined();
		}

		public void Italics()
		{
			manager.Italics();
		}

        public void InsertPi()
        {
            manager.InsertPi();
        }

        public void NewDocument()
        {
            manager.NewDocument();
        }

		#endregion

		#region Event handlers

		#endregion
	}
}
