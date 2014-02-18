using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using PapyrusDictionary;
using LatexHelpers;


namespace REditorLib
{
	public partial class RControl : UserControl
	{
		#region Fields

		private Timer					caretTimer;
		private int						caretBlinkPeriod = 500;
		private bool					isCaretToBeCleared = false;
		private RBase					editorWithFocus;
		private RBase					theContainer;
		private IDiskHandler			diskHandler;
		private ILatexCompiler			latexCompiler;
		private List<RBase>				waitingForCompilation;
		private List<RBase>				waitingForScratch;

		#endregion

		#region Properties

		public WebBrowser OutputViewer
		{
			get { return outputViewer; }
		}

		public IDiskHandler DiskHandler
		{
			get { return diskHandler; }
			set
			{
				if ( value != null )
				{
					diskHandler = value;
					diskHandler.diskOperationDone += new DiskEventHandler( diskHandler_diskOperationDone );
				}
			}
		}

		public ILatexCompiler LatexCompiler
		{
			get { return latexCompiler; }
			set
			{
				if ( value != null )
				{
					latexCompiler = value;
					latexCompiler.compilationDone += new CompilerEventHandler( latexCompiler_compilationDone );
				}
			}
		}

		public PapyrusRichTextBox SourceTexBox
		{
			get { return this.sourceTextBox; }
	
		}

		public RBase LatexContainer
		{
			get { return theContainer; }
			set
			{
				if ( value != null )
				{
					theContainer = value;
				}
			}
		}

		#endregion

		#region Constructors

		public RControl()
		{
			InitializeComponent();

			sourceTextBox.ScrollBar = sourceScrollbar;

			//theContainer = new RBase( "\\documentclass{article}", "documentclass");
            theContainer = new RBase("\\documentclass{article}\n\n\\end{document}", "documentclass");

			waitingForCompilation = new List<RBase>();
			waitingForScratch = new List<RBase>();
		}

		public RControl(IDiskHandler dh, ILatexCompiler lc)
		{
			InitializeComponent();

			diskHandler = dh;
			latexCompiler = lc;

			diskHandler.diskOperationDone += new DiskEventHandler( diskHandler_diskOperationDone );
			latexCompiler.compilationDone += new CompilerEventHandler( latexCompiler_compilationDone );

			sourceTextBox.ScrollBar = sourceScrollbar;

			waitingForCompilation = new List<RBase>();
			waitingForScratch = new List<RBase>();
		}

		#endregion

		#region Methods

		public void RecompileAll()
		{
			var temp = theContainer.Children.First;

			if ( temp.Value.Tag.Contains( "begin{document}" ) )
			{
				foreach ( var item in temp.Value.Children )
				{
					FileIODescriptor fiod = new FileIODescriptor();
					fiod.Output = item.LatexDocumentPreviewCode;
					fiod.FileName = Constants.scratchPadPath + item.ID + ".tex";
					fiod.Encoding = Encoding.UTF8;
					fiod.Write = true;
					diskHandler.AddFileToAccessQueue( fiod );

					waitingForScratch.Add( item );

					waitingForCompilation.Add( item );

					//LatexCompilationArgs args = new LatexCompilationArgs( Constants.scratchPadPath + item.ID + ".pdf", Constants.scratchPadPath + item.ID + ".png",
					//    item.ID, item.LatexDocumentPreviewCode );

					//args.Preview = true;

					//RequestCompilation( item, args );
				}
			}

			else
			{
				foreach ( var item in theContainer.Children )
				{
					LatexCompilationArgs args = new LatexCompilationArgs( Constants.scratchPadPath + item.ID + ".pdf", Constants.scratchPadPath + item.ID + ".png",
						item.ID, item.LatexDocumentPreviewCode );

					args.Preview = true;

					RequestCompilation( item, args );
				}
			}

		}

		public void RequestCompilation(RBase who, LatexCompilationArgs args)
		{
			//LatexCompilationArgs args = new LatexCompilationArgs( Constants.scratchPadPath + who.ID + ".pdf", Constants.scratchPadPath + who.ID + ".png",
			//    who.ID, who.LatexDocumentPreviewCode );

			waitingForCompilation.Add( who );

			latexCompiler.AddToCompilationQueue( args );
		}

		private void ResetCaretTimer()
		{
			isCaretToBeCleared = false;
			caretTimer = new Timer();
			caretTimer.Interval = caretBlinkPeriod;
			caretTimer.Tick += new EventHandler( caretTimer_Tick );
			caretTimer.Enabled = true;
		}

		//Draws the caret in the editor that currently has focus.
		private void DrawCaret( bool clear )
		{
			RWord textBoxWithFocus = editorWithFocus as RWord;

			if ( textBoxWithFocus != null )
			{
				textBoxWithFocus.DrawCaret( clear );
			}
		}

        public void ResetControl()
        {
            theContainer = new RBase("\\documentclass{article}\n\n\\end{document}", "documentclass");
        }

		#endregion

		#region Event handlers

		private void RControl_Enter( object sender, EventArgs e )
		{
			ResetCaretTimer();
			DrawCaret( false );
		}

		private void RControl_Leave( object sender, EventArgs e )
		{
			caretTimer.Enabled = false;
		}

		void caretTimer_Tick( object sender, EventArgs e )
		{
			isCaretToBeCleared = !isCaretToBeCleared;
		}

		private void RControl_KeyDown( object sender, KeyEventArgs e )
		{
			if ( editorWithFocus != null )
			{

			}
		}

		void latexCompiler_compilationDone( LatexCompilationArgs args )
		{
			if ( args.Preview )
			{
				FileIODescriptor fiod = new FileIODescriptor( args.ID, Constants.scratchPadPath + args.ID + ".png", true );

				diskHandler.AddFileToAccessQueue(fiod);
			}
		}

		void diskHandler_diskOperationDone( FileIODescriptor fiod )
		{
			if ( fiod.Write )
			{
				foreach ( var item in waitingForScratch )
				{
					if ( item.ID == fiod.ID )
					{
						if ( waitingForCompilation.Find( x => x.ID == fiod.ID ) != null )
						{
							LatexCompilationArgs args = new LatexCompilationArgs( Constants.scratchPadPath + item.ID + ".pdf", Constants.scratchPadPath + item.ID + ".png",
								item.ID );

							args.TexFile = "scratch\\" + item.ID + ".tex";

							args.Preview = true;

							waitingForScratch.Remove( waitingForScratch.Find( x => x.ID == fiod.ID ) );

							RequestCompilation( item, args );
						}
					}
				}
			}

			foreach ( var item in waitingForCompilation )
			{
				if ( item.ID == fiod.ID )
				{
					item.Visual = fiod.OutputImage;

					waitingForCompilation.Remove( waitingForCompilation.Find( x => x.ID == fiod.ID ) );
				}
			}
		}

		#endregion
	}
}
