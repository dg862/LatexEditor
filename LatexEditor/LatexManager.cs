using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using PapyrusDictionary;
using REditorLib;
using LatexHelpers;

namespace LatexEditor
{
	class LatexManager
	{
		#region Fields

		private DiskHandler			diskHandler;
		private LatexCompiler		compiler;
		private PapyrusRichTextBox	sourceTextBox;
		private RControl			latexControl;
		private WebBrowser			browser;
		private string				compilationID;
        private string              previewCodeID;
        private OptionManager       optionsManager;
		private int					previewCnt = 0;

		#endregion

		#region Properties

		public OptionManager OptionManager
		{
			get { return optionsManager; }
		}

		#endregion

		#region Constructors

		public LatexManager()
		{
			sourceTextBox	=	( PapyrusRichTextBox ) App.Instance.Editor.SourceTextBox;
			latexControl	=	( RControl ) App.Instance.Editor.LatexControl;
			//pdfPreview		=	( PdfViewer ) App.Instance.Editor.PagePreview;
			browser			=	(WebBrowser) App.Instance.Editor.Browser;
            optionsManager  =    new OptionManager(App.Instance.OptionForm);

			compiler		=	new LatexCompiler();
			diskHandler		=	new DiskHandler( 50 );

			latexControl.LatexCompiler = compiler;
			latexControl.DiskHandler = diskHandler;

			//diskHandler.diskOperationDone += new DiskEventHandler( diskHandler_diskOperationDone );
			diskHandler.diskOperationDone += new DiskEventHandler( diskHandler_diskOperationDone );
			sourceTextBox.recompilationEvent += new NeedsRecompilation( sourceTextBox_recompilationEvent );
			compiler.compilationDone += new CompilerEventHandler( compiler_compilationDone );
            optionsManager.readRequest += optionsManager_readRequest;

            optionsManager.Load();
            LoadCommands();
			DeletePreviousPreviews();

			sourceTextBox.Snippets = optionsManager.SnippetDict;
		}

		#endregion

		#region Methods

		public void DeletePreviousPreviews()
		{
			DirectoryInfo di = new DirectoryInfo(Constants.scratchPadPath);

			foreach (var item in di.EnumerateFiles())
			{
				if (item.Name.Contains(".tex"))
				{
					item.Delete();
				}
			}

			di = new DirectoryInfo(Directory.GetCurrentDirectory());

			foreach (var item in di.EnumerateFiles())
			{
				if (item.Name.Contains(".png") || item.Name.Contains(".pdf") || item.Name.Contains(".log") || item.Name.Contains(".aux"))
				{
					item.Delete();
				}
			}
		}

        public void Preview()
        {
			RBase preamble = latexControl.LatexContainer.Document.Preamble;

			foreach (var item in latexControl.LatexContainer.Document.Children)
			{
				if (!item.IsPreambleTag)
				{
					CreatePreviewElement(item, preamble);
				}
			}
        }

		public void CreatePreviewElement(RBase item, RBase preamble)
		{
			if (item.PreviewOutOfDate)
			{
				previewCnt++;
				FileIODescriptor fiod = new FileIODescriptor();
				fiod.ID = item.ID;
				//previewCodeID = fiod.ID;
				fiod.FileName = Constants.scratchPadPath + item.ID + ".tex";
				fiod.Write = true;
				//fiod.Output = latexControl.LatexContainer.GetCompleteCodeWithChildren();
				//fiod.Output = item.LatexDocumentPreviewCode;
				fiod.Output = item.GetPreviewCodeWithPreamble(preamble);

				diskHandler.AddFileToAccessQueue(fiod);
			}	
		}

        public void Search(string what)
        {
            sourceTextBox.SearchAndScroll(what);
        }

        public void LoadCommands()
        {
            try
            {
                if (File.Exists(Constants.commandFile))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    FileStream fs = File.OpenRead(Constants.commandFile);

                    sourceTextBox.CommandList = (List<string>)bf.Deserialize(fs);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: failed to deserialize command file. Error message: " + ex.Message);
            }            
        }

        public void InsertPi()
        {
            InsertDirectly("\\pi");
        }

        public void SaveOptions()
        {
            optionsManager.SaveOptions();
        }

        public void ScanForLatexDistros()
        {
            optionsManager.ScanForLatexDistros();
        }

		public void CompileSourceCode()
		{
			browser.Stop();
			browser.Navigate( "about:blank" );
			//( (WebBrowser) ( (EditorForm) editorForm ).OutputViewer ).Stop();

			if ( File.Exists( Constants.defaultFilename ) )
			{
				File.Delete( Constants.defaultFilename );
			}

			//if ( File.Exists( Constants.defaultOutputFile ) )
			//{
			//    File.Delete( Constants.defaultOutputFile );
			//}

			compilationID = Guid.NewGuid().ToString();
			FileIODescriptor fiod = new FileIODescriptor( compilationID, sourceTextBox.PapyrusText );
			fiod.Write = true;
			fiod.FileName = Constants.defaultFilename;

			diskHandler.AddFileToAccessQueue( fiod );


			//using ( StreamWriter wr = new StreamWriter( Constants.defaultFilename ) )
			//{
			//    string source = App.Instance.Editor.SourceTextBox.Text;
			//    wr.Write( source );
			//}

			//compiler.Compile();

			//browser.Navigate( Application.StartupPath + "\\" + Constants.defaultOutputFile );

			//pdfPreview.LoadDocument( Constants.defaultOutputFile );
		}

		public void CompileSelection()
		{
			browser.Stop();
			browser.Navigate( "about:blank" );

			if ( File.Exists( Constants.defaultFilename ) )
			{
				File.Delete( Constants.defaultFilename );
			}

			compilationID = Guid.NewGuid().ToString();

			string toCompile = sourceTextBox.SelectedText;

			if ( !toCompile.Contains( @"\begin{document}" ) )
			{
				toCompile = toCompile.Insert( 0, @"\begin{document}" );
			}

			if ( !toCompile.Contains( @"\documentclass" ) )
			{
				toCompile = toCompile.Insert( 0, @"\documentclass{article}" );
			}

			if ( !toCompile.Contains( @"\end{document}" ) )
			{
				toCompile = toCompile.Insert( toCompile.Length - 1, @"\end{document}" );
			}


			FileIODescriptor fiod = new FileIODescriptor( compilationID, toCompile );
			fiod.Write = true;
			fiod.FileName = Constants.defaultFilename;

			diskHandler.AddFileToAccessQueue( fiod );			
		}

        public void InsertContainingBraces(string what)
        {
            string code = sourceTextBox.PapyrusText;
            int caret = sourceTextBox.CaretPosition;

            int selStart = sourceTextBox.SelectionStart;
            int selLength = sourceTextBox.SelectionLength;

            int lines = Helper.CountLines(code, selStart);

            if (selLength > 0)
            {
                code = code.Insert(selStart + selLength + lines, what.Substring(what.IndexOf('}')));
                string temp = code.Substring(selStart + lines, selLength);
                string temp2 = sourceTextBox.SelectedText;
                code = code.Insert(selStart + lines, what.Substring(0, what.IndexOf('}')));
            }
            else
            {
                code = code.Insert(caret + lines, what);
                sourceTextBox.CaretPosition = caret + what.Length;
                sourceTextBox.Select(sourceTextBox.CaretPosition, 0);
            }


            latexControl.LatexContainer = new RBase("\\documentclass{article}\n\n\\end{document}", "documentclass");

            LatexInterpreter.Interpret(code, ((RControl)App.Instance.Editor.LatexControl).LatexContainer);
            sourceTextBox.LatexContainer = ((RControl)App.Instance.Editor.LatexControl).LatexContainer;           
        }

        public void InsertDirectly(string what)
        {
            string code = sourceTextBox.PapyrusText;
            int caret = sourceTextBox.CaretPosition;

            int selStart = sourceTextBox.SelectionStart;
            int selLength = sourceTextBox.SelectionLength;

            int lines = Helper.CountLines(code, selStart);

            code = code.Insert(caret + lines, what);
            sourceTextBox.CaretPosition = caret + what.Length;
            sourceTextBox.Select(sourceTextBox.CaretPosition, 0);


            latexControl.LatexContainer = new RBase("\\documentclass{article}\n\n\\end{document}", "documentclass");

            LatexInterpreter.Interpret(code, ((RControl)App.Instance.Editor.LatexControl).LatexContainer);
            sourceTextBox.LatexContainer = ((RControl)App.Instance.Editor.LatexControl).LatexContainer;           
        }

		public void Bold()
		{
            InsertContainingBraces("\\textbf{}");
            //string code = sourceTextBox.PapyrusText;
            //int caret = sourceTextBox.CaretPosition;

            //int selStart = sourceTextBox.SelectionStart;
            //int selLength = sourceTextBox.SelectionLength;

            //int lines = Helper.CountLines(code, selStart);

            //if ( selLength > 0 )
            //{
            //    code = code.Insert(selStart + selLength + lines, "}");
            //    string temp = code.Substring( selStart + lines, selLength );
            //    string temp2 = sourceTextBox.SelectedText;
            //    code = code.Insert(selStart + lines, "\\textbf{");
            //}
            //else
            //{
            //    code = code.Insert(caret + lines, "\\textbf{}");
            //    sourceTextBox.CaretPosition = caret + 9;
            //    sourceTextBox.Select( sourceTextBox.CaretPosition, 0 );
            //}


            //latexControl.LatexContainer = new RBase( "\\documentclass{article}", "documentclass" );

            //LatexInterpreter.Interpret( code, ( (RControl) App.Instance.Editor.LatexControl ).LatexContainer );
            //sourceTextBox.LatexContainer = ( (RControl) App.Instance.Editor.LatexControl ).LatexContainer;
		}

		public void Underlined()
		{
            InsertContainingBraces("\\underline{}");
            //string code = sourceTextBox.PapyrusText;
            //int caret = sourceTextBox.CaretPosition;

            //int selStart = sourceTextBox.SelectionStart;
            //int selLength = sourceTextBox.SelectionLength;

            //int lines = Helper.CountLines( code, selStart );

            //if ( selLength > 0 )
            //{
            //    code = code.Insert( selStart + selLength + lines, "}" );
            //    string temp = code.Substring( selStart + lines, selLength );
            //    string temp2 = sourceTextBox.SelectedText;
            //    code = code.Insert( selStart + lines, "\\underline{" );
            //}
            //else
            //{
            //    code = code.Insert( caret + lines, "\\underline{}" );
            //    sourceTextBox.CaretPosition = caret + 11;
            //    sourceTextBox.Select( sourceTextBox.CaretPosition, 0 );
            //}


            //latexControl.LatexContainer = new RBase( "\\documentclass{article}", "documentclass" );

            //LatexInterpreter.Interpret( code, ( (RControl) App.Instance.Editor.LatexControl ).LatexContainer );
            //sourceTextBox.LatexContainer = ( (RControl) App.Instance.Editor.LatexControl ).LatexContainer;
		}

		public void Italics()
		{
            InsertContainingBraces("\\textit{}");
            //string code = sourceTextBox.PapyrusText;
            //int caret = sourceTextBox.CaretPosition;

            //int selStart = sourceTextBox.SelectionStart;
            //int selLength = sourceTextBox.SelectionLength;

            //int lines = Helper.CountLines( code, selStart );

            //if ( selLength > 0 )
            //{
            //    code = code.Insert( selStart + selLength + lines, "}" );
            //    string temp = code.Substring( selStart + lines, selLength );
            //    string temp2 = sourceTextBox.SelectedText;
            //    code = code.Insert( selStart + lines, "\\textit{" );
            //}
            //else
            //{
            //    code = code.Insert( caret + lines, "\\textit{}" );
            //    sourceTextBox.CaretPosition = caret + 9;
            //    sourceTextBox.Select( sourceTextBox.CaretPosition, 0 );
            //}


            //latexControl.LatexContainer = new RBase( "\\documentclass{article}", "documentclass" );

            //LatexInterpreter.Interpret( code, ( (RControl) App.Instance.Editor.LatexControl ).LatexContainer );
            //sourceTextBox.LatexContainer = ( (RControl) App.Instance.Editor.LatexControl ).LatexContainer;
		}

		public void OpenFile( string fileName )
		{
			try
			{
				if ( !string.IsNullOrEmpty( fileName ) )
				{
//					sourceTextBox.CurrentID = App.RandomNumber;
					sourceTextBox.CurrentID = Guid.NewGuid().ToString();

					FileIODescriptor fiod = new FileIODescriptor( fileName, Encoding.UTF8,
						sourceTextBox.CurrentID,
						sourceTextBox.PapyrusText );
					//( (PapyrusString) ( (PapyrusRichTextBox) MainForm.SourceTextBox)).PapyrusStr );

					diskHandler.AddFileToAccessQueue( fiod );
				}
			}
			catch ( Exception ex )
			{
				MessageBox.Show( "Error: failed to read from file. Error message: " + ex.Message );
			}
		}

		public void SaveFile( string fileName )
		{
			try
			{
				FileIODescriptor fiod = new FileIODescriptor();
				fiod.Output = sourceTextBox.Text;
				fiod.FileName = fileName;
				fiod.Encoding = Encoding.UTF8;
				fiod.Write = true;
				diskHandler.AddFileToAccessQueue( fiod );
			}
			catch ( Exception ex )
			{
				MessageBox.Show( "Error: failed to write to file. Error message: " + ex.Message );
			}			
		}

        public void SaveFile(string fileName, string data)
        {
            try
            {
                FileIODescriptor fiod = new FileIODescriptor();
                fiod.Output = data;
                fiod.FileName = fileName;
                fiod.Encoding = Encoding.UTF8;
                fiod.Write = true;
                diskHandler.AddFileToAccessQueue(fiod);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: failed to write to file. Error message: " + ex.Message);
            }
        }

        public void NewDocument()
        {
//            LatexInterpreter.Interpret("", ((RControl)App.Instance.Editor.LatexControl).LatexContainer);

            //sourceTextBox.LatexContainer = ;

            ((RControl)App.Instance.Editor.LatexControl).ResetControl();

            sourceTextBox.PapyrusText = ((RControl)App.Instance.Editor.LatexControl).LatexContainer.LatexDocumentCode;
        }

		#endregion

		#region Event handlers

		void diskHandler_diskOperationDone( LatexHelpers.FileIODescriptor fiod )
		{
			int index = 0;
			if ( sourceTextBox.CurrentID == ( fiod.ID ))
			{
				try
				{
					LatexInterpreter.Interpret( fiod.Output, ( (RControl) App.Instance.Editor.LatexControl ).LatexContainer );

					sourceTextBox.LatexContainer = ( (RControl) App.Instance.Editor.LatexControl ).LatexContainer;
					//sourceTextBox.PapyrusText = fiod.Output;
				}
				catch ( Exception ex )
				{
					MessageBox.Show( ex.Message );
					MessageBox.Show( ex.InnerException.Message );
				}

				//latexControl.RecompileAll();

			}

			else if( compilationID != null && compilationID.ToString() == fiod.ID )
			{
				LatexCompilationArgs args = new LatexCompilationArgs();
				args.ID = fiod.ID;
				args.TexFile = fiod.FileName;

				compiler.AddToCompilationQueue( args );
			}

            else if (optionsManager.ID == fiod.ID)
            {
                optionsManager.RawXmlData = fiod.Output;
                optionsManager.ProcessOptions();
            }

            //else if(previewCodeID == fiod.ID)
			else if ((index = latexControl.LatexContainer.Document.ContainsID(fiod.ID)) != -1)
            {
				if (fiod.IsImage)
				{
					latexControl.LatexContainer.Document.SetImage(index, fiod.OutputImage);
					previewCnt--;
					if (previewCnt == 0)
					{
						latexControl.DrawPreview();
					}
				}

				else
				{
					LatexCompilationArgs args = new LatexCompilationArgs();
					args.Preview = true;
					//args.LatexCode = latexControl.LatexContainer.LatexDocumentPreviewCode;
					args.LatexCode = fiod.Output;
					args.ID = fiod.ID;
					//compilationID = args.ID;
					args.CompilerPath = Constants.compilerName;
					args.CompilerArgs = Constants.defaultPreviewArgs + " " + fiod.FileName;
					//args.CompilerArgs = "-shell-escape -jobname " + Constants.scratchPadPath + fiod.ID + ".pdf " + fiod.FileName;
					//args.CompilerArgs = "-shell-escape " + fiod.FileName;
					args.PngFile = fiod.ID + ".png";

					compiler.AddToCompilationQueue(args);
				}
            }
		}

		void sourceTextBox_recompilationEvent( string toCompile )
		{
            latexControl.LatexContainer = new RBase("\\documentclass{article}\n\n\\end{document}", "documentclass");

			LatexInterpreter.Interpret( toCompile, ( (RControl) App.Instance.Editor.LatexControl ).LatexContainer );
			sourceTextBox.LatexContainer = ( (RControl) App.Instance.Editor.LatexControl ).LatexContainer;

			sourceTextBox.NeedsRecompilation = false;
		}

		void compiler_compilationDone( LatexCompilationArgs args )
		{
			if (!string.IsNullOrEmpty(compilationID))
			{
				if (compilationID.ToString() == args.ID)
				{
					if (args.Preview)
					{

					}

					else
					{
						browser.Navigate(Application.StartupPath + "\\" + Constants.defaultOutputFile);
					}
				}
			}

			else if (latexControl.LatexContainer.Document.ContainsID(args.ID) != -1)
			{
				FileIODescriptor fiod = new FileIODescriptor();
				fiod.ID = args.ID;
				fiod.Write = false;
				fiod.FileName = args.PngFile;
				fiod.IsImage = true;

				diskHandler.AddFileToAccessQueue(fiod);
			}
		}

        void optionsManager_readRequest(FileIODescriptor fiod)
        {
            diskHandler.AddFileToAccessQueue(fiod);
        }

		#endregion
	}
}
