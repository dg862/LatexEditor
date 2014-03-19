using System;
using System.Collections.Generic;
using System.Text;

namespace PapyrusDictionary
{
	public static class Helper
	{
		#region Methods

		public static int FindEndTag( string[] tags, int index )
		{
			int ret = -1;
			string inner = string.Empty;
			string tag = string.Empty;
			string endTag = string.Empty;

			try
			{
				inner = tags[index].Substring( tags[index].IndexOf( '{' ) + 1, tags[index].IndexOf( '}' ) - tags[index].IndexOf( '{' ) - 1 );
				tag = tags[index].Substring( 0, tags[index].IndexOf( '{' ) );
			}
			catch ( Exception )
			{
				return ret;
			}

			for ( int i = 0; i < Constants.beginnerTags.Count; i++ )
			{
				if ( tag.Contains( Constants.beginnerTags[i] ) )
				{
					endTag = Constants.enderTags[i];
					break;
				}
			}

			for ( int i = index + 1; i < tags.Length; i++ )
			{
				//If we find a matching end tag - check if its insides match.
				if ( tags[i].Contains( endTag ) )
				{
					string newInner = string.Empty;

					try
					{
						newInner = tags[i].Substring( tags[i].IndexOf( '{' ) + 1, tags[i].IndexOf( '}' ) - tags[i].IndexOf( '{' ) - 1 );
					}

					catch ( Exception )
					{

					}

					if ( inner == newInner )
					{
						ret = i;
						return ret;
					}
				}

				//Otherwise, if we found a new opening bracket - recursively find its pair.
				else if ( tags[i].Contains( tag ) )
				{
					i = FindEndTag( tags, i );
				}
			}

			return ret;
		}

		public static int[] FindMatchingBracket( string[] tags, int[] index, Constants.BracketType bracket )
		{
			int[] ret = new int[2] { -1, -1 };

			for ( int i = index[0]; i < tags.Length; i++ )
			{
				//Walk through each tag either from the starting point (the first brace) given in index[2], or from the start.
				for ( int j = ( i == index[0] ? index[1] + 1 : 0 ); j < tags[i].Length; j++ )
				{
					//If we find a closing bracket - we're done.
					if ( tags[i][j] == Constants.closingBrackets[(int) bracket] )
					{
						ret = new int[2] { i, j };
						return ret;
					}

					//Otherwise, if we found a new opening bracket - recursively find its pair.
					else if ( tags[i][j] == Constants.openingBrackets[(int) bracket] )
					{
						var temp = FindMatchingBracket( tags, new int[2] { i, j }, bracket );

						//Skip forward to the closing bracket.
						i = temp[0];
						j = temp[1];
					}
				}
			}

			return ret;
		}

		public static int[] FindMatchingBracketSingleTag( string[] tags, int[] index, Constants.BracketType bracket )
		{
			int[] ret = new int[2] { -1, -1 };

			//Walk through the given tag.
			for ( int j = index[1] + 1; j < tags[index[0]].Length; j++ )
			{
				//If we find a closing bracket - we're done.
				if ( tags[index[0]][j] == Constants.closingBrackets[(int) bracket] )
				{
					ret = new int[2] { index[0], j };
					return ret;
				}

				//Otherwise, if we found a new opening bracket - recursively find its pair.
				else if ( tags[index[0]][j] == Constants.openingBrackets[(int) bracket] )
				{
					var temp = FindMatchingBracketSingleTag( tags, new int[2] { index[0], j }, bracket );

					//Skip forward to the closing bracket.
					j = temp[1];
				}
			}

			return ret;
		}

		public static int FindMatchingBracket( string tags, int index, Constants.BracketType bracket )
		{
			int ret = -1;

			for ( int i = index; i < tags.Length; i++ )
			{
				//Walk through each tag either from the starting point (the first brace) given in index[2], or from the start.
				//If we find a closing bracket - we're done.
				if ( tags[i] == Constants.closingBrackets[(int) bracket] )
				{
					ret = i;
					return ret;
				}

				//Otherwise, if we found a new opening bracket - recursively find its pair.
				else if ( tags[i] == Constants.openingBrackets[(int) bracket] )
				{
					var temp = FindMatchingBracket( tags, i + 1, bracket );

					//Skip forward to the closing bracket.
					i = temp;
				}
			}

			return ret;
		}

		public static int FindMatchingBracket( string tags, int index, Constants.BracketType bracket, bool forward )
		{
			int ret = -1;
			int dir = forward == true ? 1 : -1;

			for ( int i = index; forward ? i < tags.Length : i > -1; i += dir )
			{
				//Walk through each tag either from the starting point (the first brace) given in index[2], or from the start.
				//If we find a closing bracket - we're done.
				if ( tags[i] == (forward ? Constants.closingBrackets[(int) bracket] : Constants.openingBrackets[(int) bracket]) )
				{
					ret = i;
					return ret;
				}

				//Otherwise, if we found a new opening bracket - recursively find its pair.
				else if ( tags[i] == ( forward ? Constants.closingBrackets[(int) bracket] : Constants.openingBrackets[(int) bracket] ) )
				{
					var temp = FindMatchingBracket( tags, forward ? i + 1 : i - 1, bracket, forward );

					//Skip forward to the closing bracket.
					i = temp;
				}
			}

			return ret;
		}

		public static int FindNextDelimiter( string str, int index) 
		{
			while ( str[index] != '\n' && str[index] != '{' && str[index] != '[' && index < str.Length )
			{
				index++;
			}

			return index;
		}

		public static int CountLines(string str, int until)
		{
			int sumLen = 0, i = 0, ret = 0;
			string[] sp = str.Split('\n');

			for ( i = 0; i < sp.Length; i++ )
			{
				if ( sumLen > until )
				{
					break;
				}

                if (sp[i].Length != 0)
                {
                    sumLen += sp[i].Length;

                    ret++;
                }
			}

			return ret - 1;
		}

		#endregion
	}
}
