using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using PapyrusDictionary;
//using System

namespace REditorLib
{
	public class RContainer : IContainerAccess
	{
		#region Fields

		private LinkedList<RBase>		nodes;

		#endregion

		#region Properties

		public LinkedList<RBase> Nodes
		{
			get { return nodes; }
		}

		public RBase Document
		{
			get
			{
				foreach (var item in nodes)
				{
					if (item.Tag.Contains("begin{document}") && !string.IsNullOrEmpty(item.Tag))
					{
						return item;
					}
				}

				return null;
			}
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


		public int Count
		{
			get { return this.nodes.Count; }
		}
	}
}
