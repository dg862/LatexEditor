using System;
using System.Collections.Generic;
using System.Text;
using PapyrusDictionary;

namespace REditorLib
{
	public static class LatexInterpreter
	{
		#region Fields



		#endregion

		#region Properties



		#endregion

		#region Constructors



		#endregion

		#region Methods

		public static void Interpret( string latexSourceCode, RBase parent )
		{
			string[] tags = latexSourceCode.Split('\\');

			RecursivelyCreateObjects( tags, parent );
		}

		private static void RecursivelyCreateObjects( string[] tags, RBase parent )
		{
			int index;
			int[] bracketIndex = new int[2];
			int i = 0;
			int start = 0;

			if ( tags != null && tags.Length != 0)
			{
				while ( i < tags.Length && tags[i] != null )
				{
					start = i;
					//Check for ignored tags.
					for ( int j = 0; j < Constants.ignoredTags.Count; j++ )
					{
						if ( tags[i].Contains( Constants.ignoredTags[j] ) )
						{
							i++;
							break;
						}
					}


					if ( i >= tags.Length )
					{
						break;
					}

					//Check for preamble tags.
					for ( int j = 0; j < Constants.preambleTags.Count; j++ )
					{
						if ( tags[i].Contains( Constants.preambleTags[j] ) )
						{
							bracketIndex = new int[2];
							//Check for pairless opening brackets.
							if ( ( index = tags[i].IndexOf( '{' ) ) != -1 )
							{
								bracketIndex = Helper.FindMatchingBracketSingleTag( tags, new int[2] { i, index }, PapyrusDictionary.Constants.BracketType.Braces );

								//This means that this tag has childs tags.
								if ( bracketIndex[0] == -1 || bracketIndex[1] == -1 )
								{
									//string code = "\\" + tags[i].Substring( 0, tags[i].IndexOf( '{' ) + 1 ) + Constants.insertSign;
									//string code = "\\" + tags[i] + Constants.insertSign;
									string code = "\\" + tags[i];
									int[] endIndex = new int[2] { -1, -1 };

									//Find the matching bracket. bracketIndex[0] gives where it is in tags[].
									bracketIndex = Helper.FindMatchingBracket( tags, new int[2] { i, index }, PapyrusDictionary.Constants.BracketType.Braces );

									//Go through all tags until the matching bracket. Copy the contents to the code string of the new RBase.
									for ( int k = i + 1; k < bracketIndex[0] + 1; k++ )
									{
										//If there is an opening brace in the tag - find its pair and continue from there.
										if ( tags[k].IndexOf( '{' ) != -1 )
										{
											endIndex = Helper.FindMatchingBracketSingleTag( tags, new int[2] { k, tags[k].IndexOf( '{' ) }, PapyrusDictionary.Constants.BracketType.Braces );
										}

										//code += Constants.insertSign + tags[k].Substring( endIndex[1] + 1 );
///////////////////////										code += tags[k].Substring( endIndex[1] + 1 );
										code += Constants.insertSign;
										code += tags[k].Substring( endIndex[1] + 1 );

										//Only keep the part of the tag that we hadn't added yet.
										tags[k] = tags[k].Substring( 0, endIndex[1] + 1 );
										//code += tags[k];
									}

									string[] newTags = new string[endIndex[0] - i];
									for ( int k = 0; k < endIndex[0] - i; k++ )
									{
										newTags[k] = tags[i + k + 1];
									}

									RBase toAdd = new RBase( code, tags[i].Substring( 0, tags[i].IndexOf( '{' ) - 1 ) );

									RecursivelyCreateObjects( newTags, toAdd );

									parent.AddToContainer( toAdd );

									i = endIndex[0] + 1;
								}

								//This means that this is just a single tag with no children.
								else
								{
									RBase toAdd = new RBase( '\\' + tags[i], tags[i].Substring( 0, tags[i].IndexOf( '{' ) ) );

									parent.AddToContainer( toAdd );
									i++;
								}
							}

							break;
						}
					}

					if ( i >= tags.Length )
					{
						break;
					}


					//Check for beginner tags.
					for ( int j = 0; j < Constants.beginnerTags.Count; j++ )
					{
						if ( tags[i].Contains( Constants.beginnerTags[j] ) )
						{
							int endIndex = Helper.FindEndTag( tags, i );

							RBase toAdd = new RBase( '\\' + tags[i] + Constants.insertSign + '\\' + tags[endIndex], tags[i] );
							//RBase toAdd = new RBase( '\\' + tags[i] + tags[endIndex], tags[i] );

							string[] newTags = new string[endIndex - i];
							for ( int k = 0; k < endIndex - i - 1; k++ )
							{
								newTags[k] = tags[i + k + 1];
							}

							RecursivelyCreateObjects( newTags, toAdd );

							parent.AddToContainer( toAdd );

							i = endIndex + 1;
							break;
						}
					}

					if ( i >= tags.Length )
					{
						break;
					}


					//Check for pairless opening brackets.
					if ( ( index = tags[i].IndexOf( '{' ) ) != -1 )
					{
						bracketIndex = Helper.FindMatchingBracketSingleTag( tags, new int[2] { i, index }, PapyrusDictionary.Constants.BracketType.Braces );

						//This means that this tag has childs tags.
						if ( bracketIndex[0] == -1 || bracketIndex[1] == -1 )
						{
							string code = "\\" + tags[i].Substring( 0, tags[i].IndexOf( '{' ) + 1 ) + Constants.insertSign;
							int[] endIndex = new int[2] { -1, -1 };

							//Find the matching bracket. bracketIndex[0] gives where it is in tags[].
							bracketIndex = Helper.FindMatchingBracket( tags, bracketIndex, PapyrusDictionary.Constants.BracketType.Braces );

							//Go through all tags until the matching bracket. Copy the contents to the code string of the new RBase.
							for ( int j = i + 1; j < bracketIndex[0]; j++ )
							{
								//If there is an opening brace in the tag - find its pair and continue from there.
								if ( tags[j].IndexOf( '{' ) != -1 )
								{
									endIndex = Helper.FindMatchingBracketSingleTag( tags, new int[2] { j, 0 }, PapyrusDictionary.Constants.BracketType.Braces );
								}

								//code += Constants.insertSign + tags[j].Substring( endIndex[1], tags[j].Length );
								code += tags[j].Substring( endIndex[1], tags[j].Length );
							}

							string[] newTags = new string[endIndex[0] - i];
							for ( int k = 0; k < endIndex[0] - i - 1; k++ )
							{
								newTags[k] = tags[i + k + 1];
							}

							RBase toAdd = new RBase( code, tags[i].Substring( 0, tags[i].IndexOf( '{' ) ) );

							RecursivelyCreateObjects( newTags, toAdd );

							parent.AddToContainer( toAdd );

							i = endIndex[0] + 1;
						}

						//This means that this is just a single tag with no children.
						else
						{
							RBase toAdd = new RBase( '\\' + tags[i], tags[i].Substring( 0, tags[i].IndexOf( '{' ) ) );

							parent.AddToContainer( toAdd );

							i++;
						}
					}

					//Just add the tag as it is.
					else if ( !string.IsNullOrEmpty( tags[i] ) )
					{
						RBase toAdd = new RBase( '\\' + tags[i], tags[i] );

						parent.AddToContainer( toAdd );

						i++;
					}

					if ( start == i )
					{
						i++;
					}
				}
			}
		}

		//internal static int FindEndTag(string[] tags, int index)
		//{
		//    int ret = -1;
		//    string inner = string.Empty;
		//    string tag = string.Empty;
		//    string endTag = string.Empty;

		//    try
		//    {
		//        inner = tags[index].Substring( tags[index].IndexOf( '{' ) + 1, tags[index].IndexOf( '}' ) - tags[index].IndexOf( '{' ) - 1 );
		//        tag = tags[index].Substring( 0, tags[index].IndexOf( '{' ) );
		//    }
		//    catch ( Exception )
		//    {
		//        return ret;
		//    }

		//    for ( int i = 0; i < Constants.beginnerTags.Count; i++ )
		//    {
		//        if ( tag.Contains(Constants.beginnerTags[i]) )
		//        {
		//            endTag = Constants.enderTags[i];
		//            break;
		//        }
		//    }

		//    for ( int i = index + 1; i < tags.Length; i++ )
		//    {
		//        //If we find a matching end tag - check if its insides match.
		//        if ( tags[i].Contains( endTag ) )
		//        {
		//            string newInner = string.Empty;

		//            try
		//            {
		//                 newInner = tags[i].Substring( tags[i].IndexOf( '{' ) + 1, tags[i].IndexOf( '}' ) - tags[i].IndexOf( '{' ) - 1 );
		//            }

		//            catch ( Exception )
		//            {

		//            }

		//            if ( inner == newInner )
		//            {
		//                ret = i;
		//                return ret;						
		//            }
		//        }

		//        //Otherwise, if we found a new opening bracket - recursively find its pair.
		//        else if ( tags[i].Contains( tag ))
		//        {
		//            i = FindEndTag( tags, i );
		//        }
		//    }

		//    return ret;
		//}

		//internal static int[] FindMatchingBracket( string[] tags, int[] index, Constants.BracketType bracket )
		//{
		//    int[] ret = new int[2] { -1, -1 };

		//    for ( int i = index[0]; i < tags.Length; i++ )
		//    {
		//        //Walk through each tag either from the starting point (the first brace) given in index[2], or from the start.
		//        for ( int j = ( i == index[0] ? index[1] + 1 : 0 ); j < tags[i].Length; j++ )
		//        {
		//            //If we find a closing bracket - we're done.
		//            if ( tags[i][j] == Constants.closingBrackets[(int) bracket] )
		//            {
		//                ret = new int[2] { i, j };
		//                return ret;
		//            }

		//            //Otherwise, if we found a new opening bracket - recursively find its pair.
		//            else if ( tags[i][j] == Constants.openingBrackets[(int) bracket] )
		//            {
		//                var temp = FindMatchingBracket( tags, new int[2] { i, j }, bracket );

		//                //Skip forward to the closing bracket.
		//                i = temp[0];
		//                j = temp[1];
		//            }
		//        }
		//    }

		//    return ret;
		//}

		//internal static int[] FindMatchingBracketSingleTag(string[] tags, int[] index, Constants.BracketType bracket)
		//{
		//    int[] ret = new int[2] { -1, -1 };

		//    //Walk through the given tag.
		//    for ( int j = index[1] + 1; j < tags[index[0]].Length; j++ )
		//    {
		//        //If we find a closing bracket - we're done.
		//        if ( tags[index[0]][j] == Constants.closingBrackets[(int) bracket] )
		//        {
		//            ret = new int[2] { index[0], j };
		//            return ret;
		//        }

		//        //Otherwise, if we found a new opening bracket - recursively find its pair.
		//        else if ( tags[index[0]][j] == Constants.openingBrackets[(int) bracket] )
		//        {
		//            var temp = FindMatchingBracketSingleTag( tags, new int[2] { index[0], j }, bracket );

		//            //Skip forward to the closing bracket.
		//            j = temp[1];
		//        }
		//    }

		//    return ret;
		//}

		#endregion

		#region Event handlers



		#endregion

	}
}
