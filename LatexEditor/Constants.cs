using System;
using System.Collections.Generic;
using System.Text;

namespace LatexEditor
{
	public class Constants
	{
		#region Fields

		public const string texifyPath = @"C:\Program Files (x86)\MiKTeX 2.9\miktex\bin\texify.exe";
		public const string defaultFilename = "temp.tex";
		public const string defaultOutputFile = "temp.pdf";

        public const string compilerName = "pdflatex.exe";
        public const string optionsFile = "options.ini";
        public const string commandFile = "commands.bin";

		#endregion
	}
}
