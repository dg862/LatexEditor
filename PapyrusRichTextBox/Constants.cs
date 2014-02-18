using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace PapyrusDictionary
{
	public class Constants
	{
		#region Fields

		//public static Color				commandColor = Color.FromArgb( 255, 0, 0, 204 );
		//public static Color				editorBackgroundColor = Color.FromArgb( 255, 255, 255, 255 );
		//public static Color				editorForegroundColor = Color.FromArgb( 255, 0, 0, 0 );

		public static Color				commandColor = Color.FromArgb( 255, 255, 98, 0 );
		public static Color				editorBackgroundColor = Color.FromArgb( 255, 0, 43, 54 );
		public static Color				editorForegroundColor = Color.FromArgb( 255, 131, 148, 150 );

		public static Color				bracketHighlight = Color.FromArgb( 255, 255, 0, 0 );
		//public static Color				braceColor1 = Color.FromKnownColor( KnownColor.DarkBlue );
		//public static Color				braceColor2 = Color.FromKnownColor( KnownColor.DarkCyan );
		//public static Color				braceColor3 = Color.FromKnownColor( KnownColor.DarkGreen );
		//public static Color				braceColor4 = Color.FromKnownColor( KnownColor.DarkCyan );
		public static List<Color>		bracketColors = new List<Color> { Color.FromKnownColor( KnownColor.Green ),  Color.FromKnownColor( KnownColor.DarkBlue ), Color.FromKnownColor( KnownColor.DarkCyan ),
			Color.FromKnownColor( KnownColor.DarkGreen ), Color.FromKnownColor( KnownColor.DarkCyan ), Color.FromKnownColor(KnownColor.DarkOrange) };
		

		public static List<string>		beginnerTags = new List<string> { "begin" };
		public static List<string>		enderTags = new List<string> { "end" };

		public static List<char>		openingBrackets = new List<char> { '(', '[', '{', '<' };
		public static List<char>		closingBrackets = new List<char> { ')', ']', '}', '>' };

		public enum BracketType { Parentheses = 0, Brackets, Braces, Chevrons };
		public enum BeginnerType { Begin = 0 };

		#endregion
	}
}
