using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace REditorLib
{
	class RWord : RBase
	{
		#region Fields

		private string				text;
		private int					previewInsertIndex = 101;
		//private string				colorFormatting;
		//private string				underlinedFormatting;
		//private string				italicsFormatting;
		//private string				boldFormatting;
		private bool				isCaretVisible;
		private int					caretPosition;
		//private int					lineSpacing;
		//private Font				currentFont;
		//private Pen					activePen;
		//private Brush				activeBrush;

		#endregion

		#region Properties

		public string Text
		{
			get { return text; }
			set
			{
				if ( !string.IsNullOrEmpty(value) )
				{
					text = value;

				}
			}
		}

		#endregion

		#region Constructors

		public RWord()
		{
			latexDocumentPreviewCode = @"\documentclass[border=0pt,convert={density=300,outext=.png}]{standalone}\begin{document}$\displaystyle $\end{document}";
			latexDocumentCode = string.Empty;
		}

		#endregion

		#region Methods

		//private int FindFormattingPositionFromCaret(int caretPos)
		//{
		//    //if ( caretPos < -1 )
		//    //{
		//    //    return -1;
		//    //}

		//    //string posString = "0";
		//    //string lastString = string.Empty;
		//    //int index;

		//    //while ( int.Parse( posString ) < caretPos )
		//    //{
		//    //    index = formatting.IndexOf( "#P" );

		//    //    lastString = posString;
		//    //    posString = formatting.Substring( index, formatting.IndexOf( "#", index + 1 ) );
		//    //}

		//    //return int.Parse( lastString );
		//}

		//public int ColorText(Color color)
		//{
		//    return ColorText( color, caretPosition );
		//}

		//public int ColorText( Color color, int start )
		//{
		//    int insertPos = FindFormattingPositionFromCaret( start );

		//    formatting = formatting.Insert( insertPos, "#P" + caretPosition.ToString() + "#" + "#C%R"
		//        + color.R.ToString() + "%G" + color.G.ToString() + "%B" + color.B.ToString() );

		//    return insertPos;
		//}

		//public int ColorText( Color color, int start, int stop )
		//{
		//    int index = ColorText( color, start );
		//    int stopFormat = FindFormattingPositionFromCaret( stop );

		//    for ( int i = index; i < stopFormat; i++ )
		//    {

		//    }

		//    int toDelete = formatting.IndexOf("#P");
			

		//    return index;
		//}

		//public void ClearFormatting()
		//{ 
			
		//}

		//public void Underline()
		//{

		//}

		//public void Bold()
		//{ 
			
		//}

		//public void Italics()
		//{ 
			
		//}

		private void CallRenderer()
		{
			//parentControl.RequestCompilation( this );
		}

		protected override void Draw()
		{
			base.Draw();
		}

		private void UpdateDocumentPreview( string str )
		{
			//int index = latexDocumentPreview.IndexOf("");
			latexDocumentPreviewCode = latexDocumentPreviewCode.Insert( previewInsertIndex, str );
		}

		public void DrawCaret( bool clear )
		{ 
			
		}

		#endregion

		#region Event handlers

		#endregion
	}
}
