using System;
using System.Collections.Generic;
using System.Text;

namespace PapyrusDictionary
{
	public interface IContainerAccess
	{
		string this[int index]
		{
			get;
			set;
		}

		int Count
		{
			get;
		}
	}
}
