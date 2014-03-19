using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using PapyrusDictionary;

namespace REditorLib
{
	public class RBase : IContainerAccess
	{
		#region Fields

		protected Guid								id;
		protected string							latexDocumentPreviewCode;
		protected string							latexDocumentCode;
		protected RBase								preamble;
		protected string							tag;
		protected RContainer						container;
		protected RControl							parentControl;
		protected RBase								parentEditor;
		protected Image								visual;
		private PointF								positionInParent;
		protected bool								isChanged;
		private Color								defaultBackgroundColor = Color.FromArgb( 0, 43, 54 );
		private Color								defaultForegroundColor = Color.FromArgb( 131, 148, 150 );
		private Color								backColor;
		private Color								foreColor;
		private Size								clientSize;

		#endregion

		#region Properties

		public string Tag
		{
			get { return tag; }
		}

		public LinkedList<RBase> Children
		{
			get { return container.Nodes; }
		}

		public Image Visual
		{
			get { return visual; }
			set { visual = value; }
		}

		public PointF PositionInParent
		{
			get { return positionInParent; }
			set { positionInParent = value; }
		}

		public RBase ParentEditor
		{
			get { return parentEditor; }
			set { parentEditor = value; }
		}

		public bool IsChanged
		{
			get { return isChanged; }
			internal set { isChanged = value; }
		}

		public Color DefaultBackgroundColor
		{
			get { return defaultBackgroundColor; }
			set { defaultBackgroundColor = value; }
		}

		public Color DefaultForegroundColor
		{
			get { return defaultForegroundColor; }
			set { defaultForegroundColor = value; }
		}

		public Color BackColor
		{
			get { return backColor; }
			set { backColor = value; }
		}

		public Color ForeColor
		{
			get { return foreColor; }
			set { foreColor = value; }
		}

		public Size ClientSize
		{
			get { return clientSize; }
			set
			{
				if ( value != null )
				{
					clientSize = value;
				}
			}
		}

		public string ID
		{
			get { return id.ToString(); }
		}

		public string LatexDocumentPreviewCode
		{
			get
			{
				//if ( true )
				//{

				//}
				//return latexDocumentPreviewCode;
                return latexDocumentCode;
			}
		}

		public string LatexDocumentCode
		{
			get { return latexDocumentCode; }
		}

		public RBase Preamble
		{
			get { return preamble; }
			set { preamble = value;	}
		}

		#endregion

		#region Constructors

		public RBase()
		{
			this.BackColor = defaultBackgroundColor;
			this.ForeColor = defaultForegroundColor;

			id = Guid.NewGuid();

			latexDocumentCode = Constants.insertSign;

			container = new RContainer();

			//childObjects = new SortedList<PointF, REditorBase>();
			//childObjects = new LinkedList<REditorBase>();
		}

		public RBase(string code, string tag)
		{
			this.BackColor = defaultBackgroundColor;
			this.ForeColor = defaultForegroundColor;

			id = Guid.NewGuid();

			latexDocumentCode = code;
			int index = Constants.defaultPreviewCode.IndexOf('%');
			latexDocumentPreviewCode = Constants.defaultPreviewCode;
			latexDocumentPreviewCode = latexDocumentPreviewCode.Remove(index, 1);
			latexDocumentPreviewCode = latexDocumentPreviewCode.Insert( index, code );

			this.tag = tag;

			container = new RContainer();

		}

		public RBase( RBase p, PointF position )
		{
			parentEditor = p;
			positionInParent = position;

			this.BackColor = defaultBackgroundColor;
			this.ForeColor = defaultForegroundColor;

			id = Guid.NewGuid();

			container = new RContainer();

			//childObjects = new SortedList<PointF, REditorBase>();
			//childObjects = new LinkedList<REditorBase>();
		}

		#endregion

		#region Methods

		public string GetCompleteCodeWithChildren()
		{
			string ret = latexDocumentCode;
			int index = 0, prevIndex = 0;

			foreach ( var item in container.Nodes )
			{
				index = ret.IndexOf( Constants.insertSign, index );

				if ( index == -1 )
				{
					index = prevIndex;
				}
				else
				{
					ret = ret.Remove( index, Constants.insertSign.Length );
				}

				string toInsert = item.GetCompleteCodeWithChildren();
				ret = ret.Insert( index, toInsert );
				index += toInsert.Length;
				prevIndex = index;
			}


			//int i = 0;

			//while ( ret.Contains( Constants.insertSign ) )
			//{
			//    int index = ret.IndexOf( Constants.insertSign );
			//    ret = ret.Remove( index, Constants.insertSign.Length );

			//    string toInsert = container[i];
			//    i++;

			//    //foreach ( var item in container.Nodes )
			//    //{
			//    //    string toInsert = item.GetCompleteCodeWithChildren();
			//    //    ret = ret.Insert( index, toInsert );
			//    //    index += toInsert.Length;
			//    //}
			//}

			while ( ret.Contains( Constants.insertSign ) )
			{
				ret = ret.Remove( ret.IndexOf( Constants.insertSign ), Constants.insertSign.Length );
			}

			return ret;
		}

		public void AddToPreamble(RBase p)
		{
			this.preamble.container.PushBack( p );
		}

		public void AddToContainer(RBase b)
		{
			this.container.PushBack( b );
		}

		//Signals the control to draw itself and all its children. The results can then be queried with the Visual property. Only draw if there was a change in a control.
		protected virtual void Draw()
		{
			//if ( isChanged )
			//{
			//    Image toBeDrawn = new Bitmap( this.ClientSize.Width, this.ClientSize.Height );
			//    Graphics graphics = Graphics.FromImage( toBeDrawn );
			//    graphics.Clear( this.BackColor );

			//    foreach ( var child in childObjects )
			//    {
			//        //child.Value.Draw();
			//        child.Draw();

			//        //graphics.DrawImage( child.Value.Visual, child.Value.PositionInParent );
			//        graphics.DrawImage( child.Visual, child.PositionInParent );
			//    }

			//    visual = toBeDrawn;

			//    graphics.Dispose();
			//}
		}

		protected virtual void RemoveChild( RBase child )
		{
			////int index = childObjects.IndexOfValue( child );
			////childObjects.RemoveAt( index );
			//int index = childObjects.IndexOfValue( child );
			//childObjects.RemoveAt( index );

			//this.Draw();
		}

		protected virtual void RemoveChild( PointF position )
		{
			//REditorBase child = ChildFromPoint( position );

			//RemoveChild( child );
		}

		protected virtual void AddChild( PointF position, RBase child )
		{
			//childObjects.Add( position, child );

			//child.BackColor = 
		}

		//protected virtual RBase ChildFromPoint( PointF position )
		//{
		//    //return childObjects[position];
		//}

		#endregion

		#region Event handlers

		protected void OnKeyPress( KeyEventArgs e )
		{

		}

		#endregion


		public string this[int index]
		{
			get
			{
				return container[index];
			}

			set
			{

			}
		}

		public int Count
		{
			get { return container.Nodes.Count; }
		}
	}
}
