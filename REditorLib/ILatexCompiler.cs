using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using LatexHelpers;

namespace LatexHelpers
{
	public delegate void CompilerEventHandler( LatexCompilationArgs args );

	public interface ILatexCompiler
	{
		#region Events

		event CompilerEventHandler	compilationDone;

		#endregion

		#region Methods

		void AddToCompilationQueue( LatexCompilationArgs args );

		#endregion
	}
}
