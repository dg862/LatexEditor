using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;


namespace PapyrusDictionary
{
	public delegate void NeedsRecompilation( string toCompile );

	public partial class PapyrusRichTextBox : System.Windows.Forms.RichTextBox
	{
		#region Fields
		private IContainerAccess		container;
		private PapyrusString			papyrusText = new PapyrusString( string.Empty );
		private int						currentLine = 0;
		private VScrollBar				scrollBar;
		private PapyrusTooltip			toolTip;
		private bool					toolTipVisible;
		private Point					tooltipLocation;
		private uint					maxToolTipDistance = 50;
		public event NeedsRecompilation	recompilationEvent;
		private string					currentTranslation = string.Empty;
		private string					currentID;
		private Size					maxTooltipSize;
		private Size					minTooltipSize;
		private int						tooltipOffset;
		private bool					hasTooltip;
		private bool					needsRecompilation;
		private int						caretPosition = 0;
		private string					temporaryModifiedContent;
		private bool					isMouseDragging = false;
		private bool					leftMouseClick = false;
		//private TooltipBox		toolTip;

		#endregion

		#region Properties

		public int CaretPosition
		{
			get { return caretPosition; }
			set
			{
				if ( value > -1 && value < base.Text.Length )
				{
					caretPosition = value;
				}
			}
		}

		public bool NeedsRecompilation
		{
			get { return needsRecompilation; }
			set { needsRecompilation = value; }
		}

		public IContainerAccess LatexContainer
		{
			set
			{
				if ( value != null )
				{
					container = value;

					if ( container.Count != 0 )
					{
						PapyrusString toDisplay = new PapyrusString(string.Empty);
						//string toDisplay = string.Empty;
						for ( int i = 0; i < container.Count; i++ )
						{
							toDisplay += container[i] + "\r\n";

							if ( toDisplay.NumberOfLines >= CalculateLinesPerPage(false))
							{
								break;
							}
						}

						this.PapyrusText = toDisplay.Str;
					}
				}
			}
		}

		public Size MinTooltipSize
		{
			get { return minTooltipSize; }
			set { minTooltipSize = value; }
		}

		public Size MaxTooltipSize
		{
			get { return maxTooltipSize; }
			set { maxTooltipSize = value; }
		}

		public string PapyrusText
		{
			get { return papyrusText.Str; }
			set
			{
				if ( !string.IsNullOrEmpty(value) )
				{
					papyrusText.Str = value;
					scrollBar.Maximum = papyrusText.NumberOfLines;
					scrollBar.Minimum = 0;
					if ( !string.IsNullOrEmpty( value ) )
					{
						scrollBar.Visible = true;
						//base.Clear();
						Redraw();
						//GC.Collect();
						//base.Text = papyrusText.Str;
					}
					//base.ScrollBars
					//base.Handle
				}
			}
		}

		public PapyrusString PapyrusStr
		{
			get { return papyrusText; }
		}

		public int CurrentLine
		{
			get { return currentLine; }
			set
			{
				if ( value >= 0 )
				{
					currentLine = value;
				}
			}
		}

		public int LinesPerPage
		{
			get { return papyrusText.LinesPerPage; }
		}

		public bool Spacing
		{
			get { return papyrusText.Spacing; }
			set
			{
				papyrusText.Spacing = value;
				Redraw();
			}
		}

		public uint MaxToolTipDistance
		{
			get { return maxToolTipDistance; }
			set { maxToolTipDistance = value; }
		}

		public string CurrentTranslation
		{
			get { return currentTranslation; }
			set
			{
				if ( !string.IsNullOrEmpty( value ) )
				{
					currentTranslation = value;
					currentTranslation = currentTranslation.Replace( "\\par", " " );
					DisplayTooltip( tooltipLocation );
				}
				//try
				//{
				//	if ( !string.IsNullOrEmpty( value ) )
				//	{
				//		currentTranslation = value;
				//		currentTranslation = currentTranslation.Replace( "\\par", " " );
				//		DisplayTooltip( tooltipLocation );
				//	}
				//}
				//catch ( Exception e )
				//{
				//	MessageBox.Show( e.Message );
				//}
			}
		}

		public string CurrentID
		{
			get { return currentID; }
			set
			{
				if ( !string.IsNullOrEmpty(value) )
				{
					currentID = value;
				}
			}
		}

		public int TooltipOffset
		{
			get { return tooltipOffset; }
			set { tooltipOffset = value; }
		}

		public VScrollBar ScrollBar
		{
			get { return this.scrollBar; }
			set
			{
				if ( value != null )
				{
					this.scrollBar = value;
					scrollBar.Visible = false;
					scrollBar.Parent = this;
					scrollBar.SmallChange = 5;
					scrollBar.LargeChange = 50;
					scrollBar.Scroll += new ScrollEventHandler( scrollBar_Scroll );
					//scrollBar.ValueChanged += new EventHandler( scrollBar_ValueChanged );

					this.Controls.Add( scrollBar );
				}
			}
		}

		public bool HasTooltip
		{
			get { return hasTooltip; }
			set
			{
				hasTooltip = value;
				if ( hasTooltip )
				{
					toolTip = new PapyrusTooltip();
					toolTip.ScrollBar = new VScrollBar();
					toolTip.Visible = false;
					this.Controls.Add( toolTip );
				}
				else
				{
					toolTip = null;
					//toolTip.ScrollBar = new VScrollBar();
					//toolTip.Visible = false;
					//this.Controls.Add( toolTip );
				}
			}
		}

		//public System.Drawing.Font PapyrusFont
		//{
		//    get { return this.Font; }
		//    set
		//    {
		//        if ( value != null )
		//        {
		//            this.Font = value;
		//            CalculateLinesPerPage();
		//        }
		//    }
		//}

		#endregion

		#region Constructors

		public PapyrusRichTextBox()
			: base()
		{
			base.FontChanged += new EventHandler( PapyrusRichTextBox_FontChanged );
			this.MouseClick += new MouseEventHandler( PapyrusRichTextBox_MouseClick );
			this.MouseUp += new MouseEventHandler( PapyrusRichTextBox_MouseUp );
			this.MouseDown += new MouseEventHandler( PapyrusRichTextBox_MouseDown );
			this.MouseWheel += new MouseEventHandler( PapyrusRichTextBox_MouseWheel );
			this.KeyDown += new KeyEventHandler( PapyrusRichTextBox_KeyDown );
			this.MouseMove += new MouseEventHandler( PapyrusRichTextBox_MouseMove );
			this.KeyUp += new KeyEventHandler( PapyrusRichTextBox_KeyUp );

			//papyrusText = new PapyrusString( string.Empty, papyrusText.LinesPerPage );
			papyrusText = new PapyrusString( string.Empty );
			//toolTip.Visible = false;
			//this.Controls.Add( toolTip );

			toolTip = new PapyrusTooltip();
		}

		public PapyrusRichTextBox( VScrollBar vscroll )
			: base()
		{
			//	InitializeComponent();
			base.FontChanged += new EventHandler( PapyrusRichTextBox_FontChanged );
			this.MouseClick += new MouseEventHandler( PapyrusRichTextBox_MouseClick );
			this.MouseUp += new MouseEventHandler( PapyrusRichTextBox_MouseUp );
			this.MouseDown += new MouseEventHandler( PapyrusRichTextBox_MouseDown );
			this.MouseWheel += new MouseEventHandler( PapyrusRichTextBox_MouseWheel );
			this.KeyDown += new KeyEventHandler( PapyrusRichTextBox_KeyDown );
			this.MouseMove += new MouseEventHandler( PapyrusRichTextBox_MouseMove );
			this.KeyUp += new KeyEventHandler( PapyrusRichTextBox_KeyUp );

			//papyrusText = new PapyrusString( string.Empty, papyrusText.LinesPerPage );
			papyrusText = new PapyrusString( string.Empty );

			scrollBar = vscroll;
			//	scrollBar.CreateControl();
			scrollBar.Visible = false;
			scrollBar.Parent = this;
			scrollBar.SmallChange = 5;
			scrollBar.LargeChange = 50;
			scrollBar.Scroll += new ScrollEventHandler( scrollBar_Scroll );
			//scrollBar.ValueChanged += new EventHandler( scrollBar_ValueChanged );

			this.Controls.Add( scrollBar );
			//toolTip.Visible = false;
			//this.Controls.Add( toolTip );

			toolTip = new PapyrusTooltip();
		}

		#endregion

		#region Methods

		public void Redraw()
		{
			this.SuspendLayout();
			base.SuspendLayout();
			//this.HideSelection = true;

			//Control dummy = new Control();
			//dummy.Focus();

			base.Text = papyrusText[currentLine];

			SyntaxHighlight();

			//this.Focus();

			//this.HideSelection = false;

			base.ResumeLayout();
			this.ResumeLayout();
		}

		public void SyntaxHighlight()
		{
            base.SelectionColor = Constants.editorForegroundColor;
            base.BackColor = Constants.editorBackgroundColor;

            RichTextBox rtb = new RichTextBox();
            rtb.Visible = false;
            rtb.Text = base.Text;

            rtb.Select(0, rtb.Text.Length);
            rtb.SelectionColor = Constants.editorForegroundColor;
            rtb.BackColor = Constants.editorBackgroundColor;

			string text;
            text = rtb.Text;

			int index = 0;
			int slashIndex = text.IndexOf( '\\' );
			int newlineIndex = text.IndexOf( "\n" );
			int braceIndex = text.IndexOf( '{' );
			int bracketIndex = text.IndexOf( '[' );
			int colorIndex = 0;
			int delim = 0;
			//int parenIndex = text.IndexOf( '(' );

			while ( slashIndex != -1 || braceIndex != -1 || bracketIndex != -1 )
			{
				if ( slashIndex != -1 )
				{
					delim = Helper.FindNextDelimiter(text, slashIndex);

					if ( text[delim] == '{' )
					{
                        rtb.Select(slashIndex, delim - slashIndex);
                        rtb.SelectionColor = Constants.commandColor;

                        rtb.Select(delim, 1);
                        rtb.SelectionColor = Constants.bracketColors[colorIndex % Constants.bracketColors.Count];

						int match = Helper.FindMatchingBracket( text, braceIndex + 1, Constants.BracketType.Braces );

						if ( match != -1 )
						{
                            rtb.Select(match, 1);
                            rtb.SelectionColor = Constants.bracketColors[colorIndex % Constants.bracketColors.Count];
						}

						//colorIndex++;
					}

					else if ( text[delim] == '[' )
					{
                        rtb.Select(slashIndex, delim - slashIndex);
                        rtb.SelectionColor = Constants.commandColor;

                        rtb.Select(delim, 1);
                        rtb.SelectionColor = Constants.bracketColors[colorIndex % Constants.bracketColors.Count];

						int match = Helper.FindMatchingBracket( text, braceIndex + 1, Constants.BracketType.Brackets );

						if ( match != -1 )
						{
                            rtb.Select(match, 1);
                            rtb.SelectionColor = Constants.bracketColors[colorIndex % Constants.bracketColors.Count];
						}

						//colorIndex++;
					}

					else if ( text[delim] == '\n' )
					{
                        rtb.Select(slashIndex, delim - slashIndex);
                        rtb.SelectionColor = Constants.commandColor;
					}
				}



				//if ( newlineIndex != -1 && bracketIndex != -1 && braceIndex != -1 )
				//{
				//    //This means that a newline character comes first.
				//    if ( newlineIndex < braceIndex && braceIndex < bracketIndex && slashIndex < newlineIndex )
				//    {
				//        base.Select( slashIndex, newlineIndex - slashIndex );
				//        base.SelectionColor = Constants.commandColor;
				//    }

				//    else if ( braceIndex < newlineIndex && newlineIndex < bracketIndex && slashIndex < braceIndex )
				//    {
				//        base.Select( slashIndex, braceIndex - slashIndex );
				//        base.SelectionColor = Constants.commandColor;
				//    }

				//    else if ( bracketIndex < newlineIndex && newlineIndex < braceIndex && slashIndex < bracketIndex )
				//    {
				//        base.Select( slashIndex, bracketIndex - slashIndex );
				//        base.SelectionColor = Constants.commandColor;
				//    }
				//}

				//base.Select(braceIndex, 1);
				//base.SelectionColor = Constants.bracketColors[colorIndex % Constants.bracketColors.Count];

				//int match = Helper.FindMatchingBracket( text, braceIndex, Constants.BracketType.Braces );

				//base.Select( match, 1 );
				//base.SelectionColor = Constants.bracketColors[colorIndex % Constants.bracketColors.Count];

				//colorIndex++;

				//base.Select( bracketIndex, 1 );
				//base.SelectionColor = Constants.bracketColors[colorIndex % Constants.bracketColors.Count];

				//match = Helper.FindMatchingBracket( text, bracketIndex, Constants.BracketType.Brackets );

				//base.Select( match, 1 );
				//base.SelectionColor = Constants.bracketColors[colorIndex % Constants.bracketColors.Count];

				//colorIndex++;

				slashIndex = text.IndexOf( '\\', slashIndex + 1 );
				newlineIndex = text.IndexOf( "\n", newlineIndex + 1 );
				braceIndex = text.IndexOf( '{', braceIndex + 1 );
				bracketIndex = text.IndexOf( '[', bracketIndex + 1 );
			}

            base.Rtf = rtb.Rtf;
		}

		public void PapyrusVScroll( int lines )
		{
			base.Update();

			base.Text = papyrusText[currentLine + lines];

			if ( ( currentLine + lines >= 0 ) && ( currentLine + lines < papyrusText.NumberOfLines ) )
			{
				currentLine += lines;
			}

			scrollBar.Value = currentLine;

			//SyntaxHighlight();
		}

		//selects the word nearest in the textbox to point e and returns both the selected word AND the word immediately before it
		public string SelectNearestWord( Point e )
		{
			Point p = new Point();
			p.X = e.X;
			p.Y = e.Y;

			string currentPage = this.Text;

			if ( !string.IsNullOrEmpty( currentPage ) )
			{
				int index = GetCharIndexFromPosition( p );
				int wordStart = index, cnt;


				//reach the start of a word
				int dist = 0;
				while ( currentPage[wordStart] == ' ' || currentPage[wordStart] == '\n' || currentPage[wordStart] == '.'
					|| currentPage[wordStart] == ':' || currentPage[wordStart] == ';' || currentPage[wordStart] == '?'
					|| currentPage[wordStart] == '!' || currentPage[wordStart] == '/' || currentPage[wordStart] == '\\'
					|| currentPage[wordStart] == 8212 || currentPage[wordStart] == 171 )
				{
					//don't enter a new line
					if ( currentPage[wordStart] == '\n' )
					{
						return string.Empty;
					}
					wordStart++;
					dist++;
				}

				//also let's not go too far
				if ( dist > 2 )
				{
					return string.Empty;
				}

				//if we clicked to the middle of a word, walk back to its first letter - 171 is '<<', 8212 is '--'
				while ( currentPage[wordStart] != ' ' && currentPage[wordStart] != '\n' && currentPage[wordStart] != 8212 && currentPage[wordStart] != 171 )
				{
					wordStart--;
					if ( wordStart < 0 || wordStart >= currentPage.Length )
						break;
				}
				wordStart++;
				wordStart = wordStart < 0 ? 0 : wordStart;
				wordStart = wordStart >= currentPage.Length ? currentPage.Length - 1 : wordStart;

				cnt = 0;

				while ( currentPage[wordStart + cnt] != ' ' && currentPage[wordStart + cnt] != '\n' && currentPage[wordStart + cnt] != '.'
					&& currentPage[wordStart + cnt] != ':' && currentPage[wordStart + cnt] != ';' && currentPage[wordStart + cnt] != '?'
					&& currentPage[wordStart + cnt] != '!' && currentPage[wordStart + cnt] != '/' && currentPage[wordStart + cnt] != '\\'
					&& currentPage[wordStart + cnt] != ',' && currentPage[wordStart + cnt] != 171 && currentPage[wordStart + cnt] != 8212 )
				{
					cnt++;
					if ( wordStart + cnt >= currentPage.Length )
						break;
				}

				//int previousWordStart = SelectPreviousWord( wordStart );
				string previousWord = SelectPreviousWord( wordStart );
				Select( wordStart, cnt );
				//Select( previousWordStart, cnt + (wordStart - previousWordStart) );

				//string ret = this.Text.Substring( previousWordStart, cnt + ( wordStart - previousWordStart ) );
				string ret = previousWord + ' ' + this.SelectedText;
				return ret;

			}
			else
				return string.Empty;
		}

		public string SelectPreviousWord( int start )
		{
			string ret = string.Empty;
			string currentPage = this.Text;

			//first, search for the beginning of the current word
			//note, that here, we do not stop if we hit a '\n'
			//TODO: problem - what if the previous word is on a different page?
			while ( currentPage[start] != ' ' )
			{
				//this means that we hit a word, where we can be sure we do not need the previous for the translation (e.g. "--Dijo...", "<<Preguntó...", etc.)
				if ( currentPage[start] == 8212 || currentPage[start] == 171 || currentPage[start] == ',' )
				{
					return ret;
				}
				start--;

				if ( start < 0 || start >= currentPage.Length )
					return ret;
			}

			start--;
			while ( currentPage[start] != ' ' && currentPage[start] != 8212 && currentPage[start] != 171 )
			{
				ret = ret.Insert( 0, currentPage.Substring( start, 1 ) );

				start--;

				if ( start < 0 || start >= currentPage.Length )
					break;
			}

			return ret;
			//return ++ret;
		}

		public int CalculateLinesPerPage(bool set)
		{
			int size = base.Font.Height;
			Size clientSize = base.ClientSize;

			if ( set )
			{
				papyrusText.LinesPerPage = clientSize.Height / size;
			}

			return papyrusText.LinesPerPage;
		}

		public void CalculateCharsPerLine()
		{

		}

		public Size CalculateTooltipSize()
		{
			Size ret = new Size();
			Size s = TextRenderer.MeasureText( toolTip.Text, toolTip.Font );

			ret.Width = s.Width > maxTooltipSize.Width ? maxTooltipSize.Width : s.Width;
			if ( s.Height > maxTooltipSize.Height )
			{
				ret.Height = maxTooltipSize.Height;
			}
			else
			{
				int temp = s.Width / maxTooltipSize.Width - 1;
				ret.Height = temp * s.Height > maxTooltipSize.Height ? maxTooltipSize.Height : temp * s.Height;
			}

			ret.Width = ret.Width < minTooltipSize.Width ? minTooltipSize.Width : ret.Width;
			ret.Height = ret.Height < minTooltipSize.Height ? minTooltipSize.Height : ret.Height;

			return ret;
		}

		public void DisplayTooltip( Point p )
		{
			if ( toolTipVisible )
			{
				HideTooltip();
				//toolTip.Dispose();
			}

			toolTip.Cursor = System.Windows.Forms.Cursors.Hand;
			toolTip.Dock = System.Windows.Forms.DockStyle.None;
			toolTip.Name = "TooltipBox";
			toolTip.ReadOnly = true;
			toolTip.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			toolTip.TabIndex = 0;

			try
			{
				//toolTip.Text += "testestsetstsetsetse";
				toolTip.Rtf = currentTranslation;
				//toolTip.Text += "testestsetstsetsetse";
				//toolTip.Select( 5, 10 );
				//toolTip.Rtf += toolTip.SelectedRtf;
			}
			catch ( Exception )
			{
				toolTip.Text = currentTranslation;
			}

			toolTip.Size = CalculateTooltipSize();
			toolTip.Location = CalcTooltipLocation();

			toolTip.BringToFront();
			toolTip.Visible = true;
			toolTipVisible = true;
		}

		public void HideTooltip()
		{
			toolTip.Visible = false;
			//toolTip.Hide();
			toolTipVisible = false;
			//toolTip.Dispose();
		}

		public bool PointInsideRectangle( Point P, Rectangle R )
		{
			if ( P.X < R.Right && P.X > R.Left )
			{
				if ( P.Y < R.Bottom && P.Y > R.Top )
				{
					return true;
				}
			}

			return false;
		}

		public uint CalcToolTipDistance( Point p )
		{
			//double dist = Math.Sqrt( ( p.X - toolTip.Location.X ) * ( p.X - toolTip.Location.X ) + ( p.Y - toolTip.Location.Y ) * ( p.Y - toolTip.Location.Y ) );

			//return (uint)( Math.Round( dist, 0, MidpointRounding.AwayFromZero ) );

			//if ( toolTip.PointToClient( p ).X < toolTip.ClientSize.Width && toolTip.PointToClient( p ).X > -1 &&
			//    toolTip.PointToClient( p ).Y < toolTip.ClientSize.Height && toolTip.PointToClient( p ).Y > -1 )
			//{
			//    return 0;
			//}

			if ( PointInsideRectangle( p,
				new Rectangle( (int) ( toolTip.Location.X - maxToolTipDistance ), (int) ( toolTip.Location.Y - maxToolTipDistance ),
					(int) ( toolTip.ClientSize.Width + 2 * maxToolTipDistance ), (int) (toolTip.ClientSize.Height + 2 * maxToolTipDistance ) ) ) )
			{ 
				return 0;
			}

			return (uint) Math.Max( Math.Abs( p.X - toolTip.Location.X ), p.Y - toolTip.Location.Y );
		}

		public Point CalcTooltipLocation()
		{
			//if ( tooltipLocation.X + tooltipOffset + toolTip.Size.Width > App.Instance.Manager.BookTextBox.ClientSize.Width )
			//{
			//    tooltipLocation.X = App.Instance.Manager.BookTextBox.ClientSize.Width - toolTip.Width - tooltipOffset;
			//}
			//else
			//{
			//    tooltipLocation.X = tooltipLocation.X + tooltipOffset;
			//}

			//if ( tooltipLocation.Y + toolTip.Size.Height > App.Instance.Manager.BookTextBox.ClientSize.Height )
			//{
			//    tooltipLocation.Y = App.Instance.Manager.BookTextBox.ClientSize.Height - toolTip.Height;
			//}

			if ( tooltipLocation.X + tooltipOffset + toolTip.Size.Width > this.ClientSize.Width )
			{
				tooltipLocation.X = this.ClientSize.Width - toolTip.Width - tooltipOffset;
			}
			else
			{
				tooltipLocation.X = tooltipLocation.X + tooltipOffset;
			}

			if ( tooltipLocation.Y + toolTip.Size.Height > this.ClientSize.Height )
			{
				tooltipLocation.Y = this.ClientSize.Height - toolTip.Height;
			}

			return tooltipLocation;
		}

		public void SignalForRecompilation( string toCompile)
		{
			if ( recompilationEvent != null )
			{
				//translationEvent( toTranslate.ToLower() );
				recompilationEvent( toCompile );
			}
		}

		public void DisplayText(string text)
		{
			this.PapyrusText = text;
		}

		public void HighlightBracket( Point p )
		{
			string text = this.Text;
			int index = GetCharIndexFromPosition( p );
			int closeIndex = 0;

			if ( index < text.Length )
			{
				if ( text[index] == '{' )
				{
					Redraw();

					closeIndex = Helper.FindMatchingBracket( text, index + 1, Constants.BracketType.Braces );
					if ( closeIndex != -1 )
					{
						base.Select( index, 1 );
						base.SelectionColor = Constants.bracketHighlight;
						base.Select( closeIndex, 1 );
						base.SelectionColor = Constants.bracketHighlight;
					}
				}

				else if ( text[index] == '[' )
				{
					Redraw();

					closeIndex = Helper.FindMatchingBracket( text, index + 1, Constants.BracketType.Brackets );
					if ( closeIndex != -1 )
					{
						base.Select( index, 1 );
						base.SelectionColor = Constants.bracketHighlight;
						base.Select( closeIndex, 1 );
						base.SelectionColor = Constants.bracketHighlight;
					}
				}

				else if ( text[index] == '}' )
				{
					Redraw();

					closeIndex = Helper.FindMatchingBracket( text, index - 1, Constants.BracketType.Braces, false );
					if ( closeIndex != -1 )
					{
						base.Select( index, 1 );
						base.SelectionColor = Constants.bracketHighlight;
						base.Select( closeIndex, 1 );
						base.SelectionColor = Constants.bracketHighlight;
					}
				}

				else if ( text[index] == ']' )
				{
					Redraw();

					closeIndex = Helper.FindMatchingBracket( text, index - 1, Constants.BracketType.Brackets, false );
					if ( closeIndex != -1 )
					{
						base.Select( index, 1 );
						base.SelectionColor = Constants.bracketHighlight;
						base.Select( closeIndex, 1 );
						base.SelectionColor = Constants.bracketHighlight;
					}
				}			
			}
		}

		public void HighlightBracket( int index )
		{
			string text = this.Text;
			int closeIndex = 0;

			if ( index < text.Length )
			{
				if ( text[index] == '{' )
				{
					Redraw();

					closeIndex = Helper.FindMatchingBracket( text, index + 1, Constants.BracketType.Braces );
					if ( closeIndex != -1 )
					{
						base.Select( index, 1 );
						base.SelectionColor = Constants.bracketHighlight;
						base.Select( closeIndex, 1 );
						base.SelectionColor = Constants.bracketHighlight;
					}
				}

				else if ( text[index] == '[' )
				{
					Redraw();

					closeIndex = Helper.FindMatchingBracket( text, index + 1, Constants.BracketType.Brackets );
					if ( closeIndex != -1 )
					{
						base.Select( index, 1 );
						base.SelectionColor = Constants.bracketHighlight;
						base.Select( closeIndex, 1 );
						base.SelectionColor = Constants.bracketHighlight;
					}
				}

				else if ( text[index] == '}' )
				{
					Redraw();

					closeIndex = Helper.FindMatchingBracket( text, index - 1, Constants.BracketType.Braces, false );
					if ( closeIndex != -1 )
					{
						base.Select( index, 1 );
						base.SelectionColor = Constants.bracketHighlight;
						base.Select( closeIndex, 1 );
						base.SelectionColor = Constants.bracketHighlight;
					}
				}

				else if ( text[index] == ']' )
				{
					Redraw();

					closeIndex = Helper.FindMatchingBracket( text, index - 1, Constants.BracketType.Brackets, false );
					if ( closeIndex != -1 )
					{
						base.Select( index, 1 );
						base.SelectionColor = Constants.bracketHighlight;
						base.Select( closeIndex, 1 );
						base.SelectionColor = Constants.bracketHighlight;
					}
				}
			}
		}

		public int MoveCaret(bool up)
		{
			string text = this.Text;
			int firstLineLen = caretPosition;
			int secondLineLen = 0;
			int previousLineLen = firstLineLen;
			int dir = up ? -1 : 1;

			while ((up ? firstLineLen > -1 : firstLineLen < text.Length) && text[firstLineLen] != '\n')
			{
				firstLineLen += dir;
			}

			while ( ( up ? previousLineLen > -1 : previousLineLen < text.Length ) && text[previousLineLen] != '\n' )
			{
				previousLineLen -= dir;
			}

			secondLineLen = firstLineLen + dir;

			while ( ( up ? secondLineLen > -1 : secondLineLen < text.Length ) && text[secondLineLen] != '\n' )
			{
				secondLineLen += dir;
			}

			if ( Math.Abs( firstLineLen - secondLineLen ) < Math.Abs( caretPosition - firstLineLen ) )
			{
				if ( up )
				{
					if ( ( ( firstLineLen - caretPosition + secondLineLen ) > -1 ) && ( ( firstLineLen - caretPosition + secondLineLen ) < text.Length ) )
					{
						base.Select( firstLineLen - caretPosition + secondLineLen, 0 );
						caretPosition = firstLineLen - caretPosition + secondLineLen;
					}
					else
					{
						base.Select( caretPosition, 0 );
					}
				}

				else
				{
					if ( ( ( caretPosition - previousLineLen + firstLineLen ) > -1 ) && ( ( caretPosition - previousLineLen + firstLineLen ) < text.Length ) )
					{
						base.Select( caretPosition - previousLineLen + firstLineLen, 0 );
						caretPosition = caretPosition - previousLineLen + firstLineLen;
					}
					else
					{
						base.Select( caretPosition, 0 );
					}
				}

				//base.Select( secondLineLen, 0 );

				//if ( firstLineLen - 1 > -1 )
				//{
				//    base.Select( firstLineLen - 1, 0 );
				//    caretPosition = firstLineLen - 1;
				//}
				//else
				//{
				//    base.Select(0, 0);
				//    caretPosition = firstLineLen;
				//}
				return caretPosition;
			}

			else
			{
				if ( up )
				{
					if ( ( ( secondLineLen + caretPosition - firstLineLen ) > -1 ) && ( ( secondLineLen + caretPosition - firstLineLen ) < text.Length ) )
					{
						base.Select( secondLineLen + ( caretPosition - firstLineLen ), 0 );
						caretPosition = secondLineLen + ( caretPosition - firstLineLen );
					}
					else
					{
						base.Select( caretPosition, 0 );
					}
				}

				else
				{
					if ( ( ( caretPosition - previousLineLen + firstLineLen ) > -1 ) && ( ( caretPosition - previousLineLen + firstLineLen ) < text.Length ) )
					{
						base.Select( caretPosition - previousLineLen + firstLineLen, 0 );
						caretPosition = caretPosition - previousLineLen + firstLineLen;
					}
					else
					{
						base.Select( caretPosition, 0 );
					}
				}
				return caretPosition;
			}
		}

		#endregion

		#region Event handlers

		void PapyrusRichTextBox_FontChanged( object sender, EventArgs e )
		{
			CalculateLinesPerPage(true);
			CalculateCharsPerLine();
			if ( !string.IsNullOrEmpty( base.Text ) )
			{
				base.Text = papyrusText[currentLine];
			}
		}

		void scrollBar_Scroll( object sender, ScrollEventArgs e )
		{
			if ( e.Type == ScrollEventType.SmallDecrement )
			{
				PapyrusVScroll( e.NewValue - e.OldValue );
			}
			else if ( e.Type == ScrollEventType.SmallIncrement )
			{
				PapyrusVScroll( e.NewValue - e.OldValue );
			}

			else if ( e.Type == ScrollEventType.First )
			{
				PapyrusVScroll( -currentLine );
			}

			else if ( e.Type == ScrollEventType.Last )
			{
				PapyrusVScroll( papyrusText.NumberOfLines );
			}

			else if ( e.Type == ScrollEventType.LargeDecrement )
			{
				PapyrusVScroll( ( e.NewValue - e.OldValue ) * 5 );
			}

			else if ( e.Type == ScrollEventType.LargeIncrement )
			{
				PapyrusVScroll( ( e.NewValue - e.OldValue ) * 5 );
			}

			else if ( e.Type == ScrollEventType.ThumbPosition )
			{
				PapyrusVScroll( e.NewValue - e.OldValue );
			}

			else if ( e.Type == ScrollEventType.ThumbTrack )
			{
				PapyrusVScroll( e.NewValue - e.OldValue );
			}

			else if ( e.Type == ScrollEventType.EndScroll )
			{
				PapyrusVScroll( e.NewValue - e.OldValue );
			}
		}

		void PapyrusRichTextBox_MouseClick( object sender, MouseEventArgs e )
		{
			//HighlightBracket( e.Location );


			//int temp = GetCharIndexFromPosition( e.Location );
			//if ( temp < base.Text.Length )
			//{
			//    caretPosition = temp;
			//    base.Select( caretPosition, 0 );
			//}



			//if ( e.Button == System.Windows.Forms.MouseButtons.Left )
			//{
			//    SelectNearestWord( e.Location );

			//    if ( !string.IsNullOrEmpty( this.SelectedText ) )
			//    {
			//        tooltipLocation = e.Location;

			//        SignalForTranslation( this.SelectedText );
			//    }

			//}
			//else if ( e.Button == System.Windows.Forms.MouseButtons.Right )
			//{
			//    if ( toolTip.Visible )
			//    {
			//        HideTooltip();
			//    }
			//}
		}

		void PapyrusRichTextBox_MouseUp( object sender, MouseEventArgs e )
		{
			if ( isMouseDragging )
			{
				isMouseDragging = false;
			}
			else
			{
				HighlightBracket( e.Location );

				int temp = GetCharIndexFromPosition( e.Location );
				if ( temp < base.Text.Length )
				{
					caretPosition = temp;
					base.Select( caretPosition, 0 );
				}
			}

			leftMouseClick = false;

			//if ( e.Button == System.Windows.Forms.MouseButtons.Left )
			//{
			//    string selectedText = SelectNearestWord( e.Location );

			//    if ( !string.IsNullOrEmpty( selectedText ) )
			//    {
			//        tooltipLocation = e.Location;

			//        SignalForTranslation( selectedText );
			//    }

			//}
			//else if ( e.Button == System.Windows.Forms.MouseButtons.Right )
			//{
			//    if ( toolTip.Visible )
			//    {
			//        HideTooltip();
			//    }
			//}
		}


		void PapyrusRichTextBox_MouseWheel( object sender, MouseEventArgs e )
		{
			if ( toolTip != null )
			{
				if ( toolTip.Visible )
				{
					if ( e.Delta > 0 )
					{
						toolTip.PapyrusVScroll( -1 );
					}
					else
					{
						toolTip.PapyrusVScroll( 1 );
					}
				}
			}

			if ( e.Delta > 0 )
			{
				PapyrusVScroll( -1 );

				SyntaxHighlight();
			}
			else
			{
				PapyrusVScroll( 1 );

				SyntaxHighlight();
			}

			if ( needsRecompilation )
			{
				//PapyrusText = base.Text;
				PapyrusText = temporaryModifiedContent;
				SignalForRecompilation( papyrusText.Str );
			}
		}

		void PapyrusRichTextBox_KeyDown( object sender, KeyEventArgs e )
		{
			if ( e.KeyCode == Keys.Up )
			{
				//PapyrusVScroll( -1 );
				e.SuppressKeyPress = true;
				//System.Threading.Thread.Sleep(50);
			}

			else if ( e.KeyCode == Keys.Down )
			{
				//PapyrusVScroll( 1 );
				e.SuppressKeyPress = true;
				//System.Threading.Thread.Sleep( 50 );
			}

			//else if ( e.KeyCode == Keys.PageUp )
			//{
			//    PapyrusVScroll( -papyrusText.LinesPerPage );
			//    e.SuppressKeyPress = true;
			//    //System.Threading.Thread.Sleep( 50 );
			//}

			//else if ( e.KeyCode == Keys.PageDown )
			//{
			//    PapyrusVScroll( papyrusText.LinesPerPage );
			//    e.SuppressKeyPress = true;
			//    //System.Threading.Thread.Sleep( 50 );
			//}

			//else
			//{
			//    PapyrusText = this.Text;
			//    needsRecompilation = true;
			//}
		}

		void PapyrusRichTextBox_KeyUp( object sender, KeyEventArgs e )
		{
			if ( e.KeyCode == Keys.Up )
			{
				MoveCaret( true );
			//	PapyrusVScroll( -1 );
				e.SuppressKeyPress = true;

				//HighlightBracket( caretPosition );
				//System.Threading.Thread.Sleep(50);
			}

			else if ( e.KeyCode == Keys.Down )
			{
				MoveCaret( false );
//				PapyrusVScroll( 1 );
				e.SuppressKeyPress = true;
				//HighlightBracket( caretPosition );
				//System.Threading.Thread.Sleep( 50 );
			}

			else if ( e.KeyCode == Keys.Left )
			{
				//HighlightBracket( caretPosition );

				if ( caretPosition - 1 > -1 )
				{
					if ( base.Text[caretPosition - 1] == '\n' )
					{
						caretPosition--;
						if ( caretPosition - 1 > -1 )
						{
							if ( base.Text[caretPosition - 1] == '\r' )
							{
								caretPosition--;
							}							
						}
					}

					caretPosition--;
				}
			}

			else if ( e.KeyCode == Keys.Right )
			{
				//HighlightBracket( caretPosition );
				if ( caretPosition + 1 < base.Text.Length )
				{
					caretPosition++;
				}

				if ( caretPosition + 1 < base.Text.Length )
				{
					if ( base.Text[caretPosition + 1] == '\r' )
					{
						caretPosition++;
						if ( caretPosition + 1 < base.Text.Length )
						{
							if ( base.Text[caretPosition + 1] == '\n' )
							{
								caretPosition++;
							}
						}
					}

					caretPosition--;
				}

				caretPosition++;
			}



			//else if ( e.KeyCode == Keys.PageUp )
			if ( e.KeyCode == Keys.PageUp )
			{
				PapyrusVScroll( -papyrusText.LinesPerPage );
				e.SuppressKeyPress = true;

				//int index = GetCharIndexFromPosition( this.caretPosition );

				//System.Threading.Thread.Sleep( 50 );
			}

			else if ( e.KeyCode == Keys.PageDown )
			{
				PapyrusVScroll( papyrusText.LinesPerPage );
				e.SuppressKeyPress = true;
				//System.Threading.Thread.Sleep( 50 );
			}

			else
			{
				temporaryModifiedContent = base.Text;
				//this.sele
				//caretPosition =
				needsRecompilation = true;
			}			
		}

		void PapyrusRichTextBox_MouseMove( object sender, MouseEventArgs e )
		{
			if ( toolTipVisible )
			{
				if ( CalcToolTipDistance( e.Location ) > maxToolTipDistance )
				{
					HideTooltip();
				}
			}

			if ( leftMouseClick )
			{
				isMouseDragging = true;
			}
		}

		void PapyrusRichTextBox_MouseDown( object sender, MouseEventArgs e )
		{
			leftMouseClick = true;
		}


		#endregion

	}
}
