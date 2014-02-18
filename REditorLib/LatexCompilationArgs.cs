using System;
using System.Collections.Generic;
using System.Text;
using REditorLib;

namespace LatexHelpers
{
	public class LatexCompilationArgs
	{
		#region Fields

		private bool				preview = false;
		private string				convertCommandPath = Constants.imageMagickPath;
		private string				convertCommandArgs = Constants.defaultPreviewArgs;
		private string				compilerPath = Constants.compilerPath;
		private string				compilerArgs = Constants.defaultCompilerArgs;
		private string				pdfFile;
		private string				pngFile;
		private string				texFile;
		private string				latexCode;
		private string				id;

		#endregion

		#region Properties

		public string ConvertCommandPath
		{
			get { return convertCommandPath; }
			set
			{
				if ( !string.IsNullOrEmpty(value) )
				{
					convertCommandPath = value;
				}
			}
		}
		public string ConvertCommandArgs
		{
			get { return convertCommandArgs; }
			set
			{
				if ( !string.IsNullOrEmpty(value) )
				{
					convertCommandArgs = value;
				}
			}
		}
		public string CompilerPath
		{
			get { return compilerPath; }
			set
			{
				if ( !string.IsNullOrEmpty(value) )
				{
					compilerPath = value;
				}
			}
		}
		public string CompilerArgs
		{
			get { return compilerArgs; }
			set
			{
				if ( !string.IsNullOrEmpty(value) )
				{
					compilerArgs = value;
				}
			}
		}
		public bool Preview
		{
			get { return preview; }
			set { preview = value; }
		}
		public string PDFFile
		{
			get { return pdfFile; }
			set
			{
				if ( !string.IsNullOrEmpty(value) )
				{
					pdfFile = value;
				}
			}
		}
		public string PngFile
		{
			get { return pngFile; }
			set
			{
				if ( !string.IsNullOrEmpty(value) )
				{
					pngFile = value;
				}
			}
		}
		public string LatexCode
		{
			get { return latexCode; }
			set
			{
				if ( !string.IsNullOrEmpty(value) )
				{
					latexCode = value;
				}
			}
		}

		public string TexFile
		{
			get { return texFile; }
			set
			{
				if ( !string.IsNullOrEmpty( value ) )
				{
					texFile = value;
				}
			}
		}

		public string ID
		{
			get { return id; }
			set
			{
				if ( !string.IsNullOrEmpty(value) )
				{
					id = value;
				}
			}
		}

		#endregion

		#region Constructors

		public LatexCompilationArgs( string pdf, string png, string compId )
		{
			pdfFile = pdf;
			pngFile = png;
			id = compId;
		}

		public LatexCompilationArgs( string pdf, string png, string compId, string code )
		{
			id = compId;
			pdfFile = pdf;
			pngFile = png;
			latexCode = code;
		}

		public LatexCompilationArgs()
		{

		}

		#endregion

	}
}
