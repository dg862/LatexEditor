using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.ComponentModel;
using LatexHelpers;

namespace LatexEditor
{
	public class LatexCompiler : ILatexCompiler
	{
		#region Fields

		public event CompilerEventHandler		compilationDone;
		private int								maxWorkerThreads;
		private int								currentWorkerThreads;
		private List<LatexCompilationArgs>		toCompile;
		private BackgroundWorker[]				workerThreads;

		#endregion

		#region Properties

		public int MaxWorkerThreads
		{
			get { return maxWorkerThreads; }
			set
			{
				if ( value > 0 )
				{
					maxWorkerThreads = value;
				}
			}
		}

		#endregion

		#region Constructors

		public LatexCompiler()
		{
			maxWorkerThreads = 25;
			currentWorkerThreads = 0;
			toCompile = new List<LatexCompilationArgs>(100);
			workerThreads = new BackgroundWorker[maxWorkerThreads];

			for ( int i = 0; i < maxWorkerThreads; i++ )
			{
				workerThreads[i] = new BackgroundWorker();
				workerThreads[i].DoWork += new DoWorkEventHandler( LatexCompiler_DoWork );
				workerThreads[i].RunWorkerCompleted += new RunWorkerCompletedEventHandler( LatexCompiler_RunWorkerCompleted );
				workerThreads[i].WorkerReportsProgress = true;
			}
		}

		public LatexCompiler(int maxThreads)
		{
			maxWorkerThreads = maxThreads;
			currentWorkerThreads = 0;
			toCompile = new List<LatexCompilationArgs>( 100 );
			workerThreads = new BackgroundWorker[maxWorkerThreads];

			for ( int i = 0; i < maxWorkerThreads; i++ )
			{
				workerThreads[i] = new BackgroundWorker();
				workerThreads[i].DoWork += new DoWorkEventHandler( LatexCompiler_DoWork );
				workerThreads[i].RunWorkerCompleted += new RunWorkerCompletedEventHandler( LatexCompiler_RunWorkerCompleted );
				workerThreads[i].WorkerReportsProgress = true;
			}
		}

		#endregion

		#region Methods

		public void AddToCompilationQueue( LatexCompilationArgs args )
		{
			//add file to disk access queue
			toCompile.Add( args );

			//first, check if there are any free worker threads we can use
			for ( int i = 0; i < currentWorkerThreads; i++ )
			{
				if ( !workerThreads[i].IsBusy )
				{
					workerThreads[i].RunWorkerAsync();
					return;
				}
			}
			//check if there is a free thread slot, if there are fewer threads than max, create a new one
			//TODO: write else branch
			if ( currentWorkerThreads < maxWorkerThreads )
			{
				workerThreads[currentWorkerThreads].RunWorkerAsync();
				currentWorkerThreads++;
				return;
			}

			//otherwise, wait for a bit for a free slot
			for ( int j = 0; j < 5; j++ )
			{
				for ( int i = 0; i < currentWorkerThreads; i++ )
				{
					System.Threading.Thread.Sleep( 50 );
					if ( !workerThreads[i].IsBusy )
					{
						workerThreads[i].RunWorkerAsync();
						return;
					}
				}
			}			
		}

		public void Compile()
		{
			RunProcess( Constants.texifyPath, Constants.defaultFilename + " --pdf --clean" );
		}

		private string RunProcess( string cmd, string args )
		{
			System.Diagnostics.Process p;
			p = new System.Diagnostics.Process();

			p.StartInfo.FileName = cmd;
			p.StartInfo.Arguments = args;
			p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
			p.StartInfo.CreateNoWindow = true;
			p.StartInfo.RedirectStandardOutput = true;
			p.StartInfo.UseShellExecute = false;

			p.Start();

			string output= p.StandardOutput.ReadToEnd();
			p.WaitForExit();
			return output;
		}

		#endregion

		#region Event handlers

		void LatexCompiler_RunWorkerCompleted( object sender, RunWorkerCompletedEventArgs e )
		{
			if ( compilationDone != null )
			{
				compilationDone( (LatexCompilationArgs) e.Result );
			}
		}

		void LatexCompiler_DoWork( object sender, DoWorkEventArgs e )
		{
			LatexCompilationArgs args;

			Monitor.Enter( toCompile );
			try
			{
				args = ( (LatexCompilationArgs) toCompile[0] );
				toCompile.RemoveAt( 0 );
				e.Result = args;													//set the result to the io descriptor so that the manager can know the ID
			}
			finally
			{
				Monitor.Exit( toCompile );
			}

			if ( args.Preview )
			{
                RunProcess(args.CompilerPath, args.LatexCode);

				//string testCode = @"\documentclass[border=0pt,convert={density=300,outext=.png}]{standalone}\usepackage{varwidth}\begin{document}\begin{varwidth}{\linewidth}
//\section{Introduction}Here is the text of your introduction.\end{varwidth}\end{document}";
//				string path = "\"E:\\Google Drive\\munka\\diplomaterv\\diplomaterv 1\\test1.tex\"";
				//RunProcess( args.CompilerPath, args.CompilerArgs + " " + args.LatexCode );
				//RunProcess( args.PreviewCommandPath, args.PreviewCommandArgs + " " + args.PDFFile + " " + args.PngFile );
				//RunProcess( args.CompilerPath, args.CompilerArgs + " " + args.LatexCode );
		//		RunProcess( args.CompilerPath, args.CompilerArgs + " " + path );
				//RunProcess( args.CompilerPath, args.CompilerArgs + " " + @"E:\test1.tex");		//works
				//RunProcess( args.CompilerPath, args.CompilerArgs + " " + testCode);
			}

			else
			{
			//	RunProcess( args.CompilerPath, args.CompilerArgs + " " + args.TexFile );
			}
		}

		#endregion
	}
}
