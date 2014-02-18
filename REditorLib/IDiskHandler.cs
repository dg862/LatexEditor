using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using LatexHelpers;

namespace LatexHelpers
{
	public delegate void DiskEventHandler( FileIODescriptor fiod );

	public interface IDiskHandler
	{
		#region Events
		event DiskEventHandler	diskOperationDone;
		#endregion

		#region Methods

		void AddFileToAccessQueue( FileIODescriptor fileIODesc );

		#endregion

	}
}
