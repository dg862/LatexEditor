using System;
using System.Collections.Generic;
using System.Text;

namespace REditorLib
{
	public class Constants
	{
		#region Fields

		//public const string				compilerPath = @"C:\Program Files (x86)\MiKTeX 2.9\miktex\bin\pdflatex.exe";
		//public const string				imageMagickPath = @"C:\Program Files (x86)\ImageMagick-6.8.7-Q16\imgconvert.exe";
        public static string                latexDistribution;
        public static string                distributionPath;
		public static string				compilerPath = @"scratch\pdflatex.exe";
		public static string				imageMagickPath = @"scratch\imgconvert.exe";
		public static string				defaultCompilerArgs = "-shell-escape -max-print-line=120 -interaction=nonstopmode";
		public static string				defaultPreviewArgs = "-density 300 -quality 90";
		public static string				defaultPreviewCode = @"	\documentclass[border=0pt,convert={density=300,outext=.png}]{standalone}\usepackage{varwidth}
																\begin{document}
																\begin{varwidth}{\linewidth}
																%
																\end{varwidth}
																\end{document}
																";
		public static string				scratchPadPath = @"scratch\";
		public static List<string>		preambleTags = new List<string>{ "title", "author" };
		public static List<string>		beginnerTags = new List<string> { "begin" };
		public static List<string>		enderTags = new List<string> { "end" };
		//public static List<string>		ignoredTags = new List<string> { "documentclass" };
		public static List<string>		ignoredTags = new List<string> {  };
		public static List<char>		openingBrackets = new List<char> { '(', '[', '{', '<' };
		public static List<char>		closingBrackets = new List<char> { ')', ']', '}', '>' };
		public const string				insertSign = "@&@&";

		public enum BracketType { Parentheses = 0, Brackets, Braces, Chevrons };
		public enum BeginnerType { Begin = 0 };

		#endregion
	}
}
