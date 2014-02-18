using System;
using System.Collections.Generic;
using System.Text;
//using System

namespace REditorLib
{
	public class RContainer
	{
		#region Fields

		private LinkedList<RBase>		nodes;

		#endregion

		#region Properties

		public LinkedList<RBase> Nodes
		{
			get { return nodes; }
		}

		#endregion

		#region Constructors

		public RContainer()
		{
			nodes = new LinkedList<RBase>();
		}

		#endregion

		#region Methods

		public void PushFront( RBase node )
		{
			nodes.AddFirst( node );
		}

		public void PopFront()
		{
			nodes.RemoveFirst();
		}

		public void PushBack( RBase node )
		{
			nodes.AddLast( node );
		}

		public void PopBack()
		{
			nodes.RemoveLast();
		}

		public void Insert()
		{ 
			
		}

		#endregion

		#region Operators

		public string this[int index]
		{
			get
			{
				LinkedList<RBase>.Enumerator en = nodes.GetEnumerator();
				if ( nodes.Count != 0 )
				{
					en.MoveNext();
					for ( int i = 0; i < index; i++ )
					{
						string code = en.Current.GetCompleteCodeWithChildren();

						en.MoveNext();
					}


					return en.Current.GetCompleteCodeWithChildren();
				}

				return string.Empty;
			}

			set
			{
				
			}
		}

		#endregion

		#region Event handlers

		#endregion
	}
}
