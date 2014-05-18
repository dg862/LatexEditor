using System;
using System.Collections.Generic;
using System.Text;

namespace LatexEditor
{
	public class Constants
	{
		#region Fields

		//public static string texifyPath = @"C:\Program Files (x86)\MiKTeX 2.9\miktex\bin\texify.exe";
		public static string texifyPath = @"scratch\texify.exe";
		public const string defaultFilename = @"scratch\temp.tex";
		public const string defaultOutputFile = @"pdf\temp.pdf";
		//public const string defaultPreviewArgs = "-shell-escape -density 300 -quality 90";
		public static string previewArgs = "-shell-escape";
		public static string imageMagickPath = @"scratch\imgconvert.exe";

        public static string scratchPadPath = @"scratch\";
		public static string pdfPath = @"pdf";
        public static string previewTempFile = "preview.tex";
        public static string compilerName = @"scratch\pdflatex.exe";
		//public const string compilerName = "pdflatex.exe";
        public const string optionsFile = "options.ini";
        public const string commandFile = "commands.bin";

		public static string compilerArgs = "";
		public static string previewCode = "";
		public static string latexDistribution;
		public static string distributionPath;


		#endregion
	}
}
