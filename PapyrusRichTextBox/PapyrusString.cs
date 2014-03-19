using System;
using System.Collections.Generic;
using System.Text;

namespace PapyrusDictionary
{
	public class PapyrusString
	{
		#region Fields

		private string			str;
		private int				numberOfLines;
		private int				linesPerPage = 100;
		private List<int>		lineIndices;
		private bool			spacing;

		#endregion

		#region Properties

		public string Str
		{
			get { return str; }
			set
			{
				str = value;
				CountLines();
			}
		}

		public int NumberOfLines
		{
			get { return numberOfLines; }
		}

		public List<int> LineIndices
		{
			get { return lineIndices; }
		}

		public int LinesPerPage
		{
			get { return linesPerPage; }
			set { linesPerPage = value > 0 ? value : linesPerPage; }		//at least one line has to be visible
		}

		public bool Spacing
		{
			get { return spacing; }
			set { spacing = value; }
		}

		#endregion

		#region Contructors

		public PapyrusString( string s, int linesperpage )
		{
			str = s;
			lineIndices = new List<int>( 10000 );
			linesPerPage = linesperpage;
			CountLines();
		}

		public PapyrusString( string s )
		{
			str = s;
			lineIndices = new List<int>( 10000 );
			CountLines();
		}

		#endregion

		#region Methods

        public int Search(string what)
        {
            int line = 0;

            while (!this[line].Contains(what) && line < numberOfLines)
            {
                line++;
            }

            return line;
        }

        public int Search(string what, int start)
        {
            int line = start;

            while (!this[line].Contains(what) && line < numberOfLines)
            {
                line++;
            }

            return line;
        }

		private int CountLines()
		{
			if ( string.IsNullOrEmpty( str ) )
			{
				return 0;
			}
			lineIndices.Clear();
			//lineIndices.Insert( 0, 0 );
			lineIndices.Add( 0 );
			numberOfLines = 1;
			int n = 0;

			while ( ( n = str.IndexOf( '\n', n ) ) != -1 )
			{
				//lineIndices[numberOfLines] = n;
				lineIndices.Add( n + 1 );
				n++;
				numberOfLines++;
			}

			//lineIndices[numberOfLines] = str.Length;
			lineIndices.Add( str.Length );

			return numberOfLines;
		}

		#endregion

		#region Operators

		public static PapyrusString operator +( PapyrusString a, string b )
		{
			return new PapyrusString( a.Str + b, a.LinesPerPage );
		}

		public static PapyrusString operator +( PapyrusString a, PapyrusString b )
		{
			return new PapyrusString( a.Str + b.Str, a.LinesPerPage );
		}

		public string this[int index]
		{
			//	get { return str.Substring( lineIndices[index], lineIndices[index + linesPerPage] ); }
			get
			{
				if ( !string.IsNullOrEmpty( str ) )
				{
					int start = index >= 0 ? index : 0;
					start = start < numberOfLines ? start : numberOfLines - 1;
					start = start >= 0 ? start : 0;
					int stop = (int) ( ( (float) index + 1.2f * (float) linesPerPage ) < (float) numberOfLines ? ( (float) index + 1.2f * (float) linesPerPage ) : (float) numberOfLines );
					stop = (int) ( (float) stop >= (float) 0 ? (float) stop : 1.2f * (float) linesPerPage );

					string s = str.Substring( lineIndices[start], lineIndices[stop] - lineIndices[start] );

					if ( spacing )
					{
						return s.Replace( Environment.NewLine, Environment.NewLine + Environment.NewLine );
					}

					return s;
				}

				return string.Empty;
			}
		}

		#endregion

		#region Event handlers
		#endregion
	}
}
