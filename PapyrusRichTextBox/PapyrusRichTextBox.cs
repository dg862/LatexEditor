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
		private string					currentID;
		private Size					maxTooltipSize;
		private Size					minTooltipSize;
		private int						tooltipOffset;
		private bool					hasTooltip;
		private bool					needsRecompilation;
		//private int						caretPosition = 0;
		private string					temporaryModifiedContent;
		private bool					isMouseDragging = false;
		private bool					leftMouseClick = false;

        private List<string>            fullCommandList;
        private List<string>            shortCommandList;
        private string                  command;
        private string                  currentSearch;
        private int                     currentSearchIndex;
        private int                     searchResults = 0;
		private bool					tabPressed = false;
		private Dictionary<string, string> snippets;
        //private string                  commandID;
		//private TooltipBox		toolTip;

		#endregion

		#region Properties

        //public string CommandID
        //{
        //    get { return commandID; }
        //    set
        //    {
        //        if (!string.IsNullOrEmpty(value))
        //        {
        //            commandID = value;
        //        }
        //    }
        //}

        public List<string> CommandList
        {
            get { return fullCommandList; }
            set
            {
                if (value != null)
                {
                    fullCommandList = value;
                }
            }
        }

		public int CaretPosition
		{
			get { return base.SelectionStart; }
			set
			{
				if (value != null)
				{
					if (value < 0)
					{
						base.SelectionStart = 0;
					}

					else if (value > base.Text.Length)
					{
						base.SelectionStart = base.Text.Length;
					}

					else
					{
						base.SelectionStart = value;
					}
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

					//caretPosition = 0;
					base.SelectionStart = 0;
					base.SelectionLength = 0;
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

		public Dictionary<string, string> Snippets
		{
			set
			{
				if (value != null)
				{
					snippets = value;
				}
			}
		}

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
            this.KeyPress += PapyrusRichTextBox_KeyPress;
			this.MouseMove += new MouseEventHandler( PapyrusRichTextBox_MouseMove );
			this.KeyUp += new KeyEventHandler( PapyrusRichTextBox_KeyUp );
			this.TextChanged += PapyrusRichTextBox_TextChanged;

			//papyrusText = new PapyrusString( string.Empty, papyrusText.LinesPerPage );
			papyrusText = new PapyrusString( string.Empty );
			//toolTip.Visible = false;
			//this.Controls.Add( toolTip );

			toolTip = new PapyrusTooltip();

			this.ForeColor = Color.FromArgb(0, 131, 148, 151);
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
			this.TextChanged += PapyrusRichTextBox_TextChanged;

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

			this.ForeColor = Color.FromArgb(0, 131, 148, 151);
		}

		#endregion

		#region Methods

        public void SearchAndScroll(string what)
        {
            searchResults = 0;
            currentSearch = what;
            currentSearchIndex = 0;
            int where = papyrusText.Search(what);

            PapyrusVScroll(where - currentLine);

            int index = 0;

            this.SelectionBackColor = Color.White;
            SyntaxHighlight();

            while (index != -1)
            {
                index = this.Text.IndexOf(what, index + 1);

                if (index != -1)
                {
                    this.Select(index, what.Length);
                    this.SelectionBackColor = Color.Teal;

                    searchResults++;
                }
            }

            
        }

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

			while ( slashIndex != -1 || braceIndex != -1 || bracketIndex != -1 )
			{
				if ( slashIndex != -1 )
				{
					delim = Helper.FindNextDelimiter(text, slashIndex);

					if (delim != -1)
					{
						if (text[delim] == '{')
						{
							rtb.Select(slashIndex, delim - slashIndex);
							rtb.SelectionColor = Constants.commandColor;

							rtb.Select(delim, 1);
							rtb.SelectionColor = Constants.bracketColors[colorIndex % Constants.bracketColors.Count];

							int match = Helper.FindMatchingBracket(text, braceIndex + 1, Constants.BracketType.Braces);

							if (match != -1)
							{
								rtb.Select(match, 1);
								rtb.SelectionColor = Constants.bracketColors[colorIndex % Constants.bracketColors.Count];
							}
						}

						else if (text[delim] == '[')
						{
							rtb.Select(slashIndex, delim - slashIndex);
							rtb.SelectionColor = Constants.commandColor;

							rtb.Select(delim, 1);
							rtb.SelectionColor = Constants.bracketColors[colorIndex % Constants.bracketColors.Count];

							int match = Helper.FindMatchingBracket(text, braceIndex + 1, Constants.BracketType.Brackets);

							if (match != -1)
							{
								rtb.Select(match, 1);
								rtb.SelectionColor = Constants.bracketColors[colorIndex % Constants.bracketColors.Count];
							}
						}

						else if (text[delim] == '\n')
						{
							rtb.Select(slashIndex, delim - slashIndex);
							rtb.SelectionColor = Constants.commandColor;
						}
					}
				}

				slashIndex = text.IndexOf( '\\', slashIndex + 1 );
				newlineIndex = text.IndexOf( "\n", newlineIndex + 1 );
				braceIndex = text.IndexOf( '{', braceIndex + 1 );
				bracketIndex = text.IndexOf( '[', bracketIndex + 1 );
			}

			int tempCaret = base.SelectionStart;
            base.Rtf = rtb.Rtf;

			base.SelectionStart = tempCaret;

			//base.Select(base.SelectionStart, 0);
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
			}

			toolTip.Cursor = System.Windows.Forms.Cursors.Hand;
			toolTip.Dock = System.Windows.Forms.DockStyle.None;
			toolTip.Name = "TooltipBox";
			toolTip.ReadOnly = true;
			toolTip.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			toolTip.TabIndex = 0;

			try
			{
                shortCommandList = fullCommandList;
                UpdateTooltip();
                toolTip.SelectedLine = 0;
			}
			catch ( Exception )
			{
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
			toolTipVisible = false;
		}

        private void UpdateTooltip()
        {
            shortCommandList = shortCommandList.FindAll(x => x.Contains(command));

			if (shortCommandList.Count <= 0 || command.Length <= 0)
			{
				HideTooltip();
				return;
			}

            toolTip.Text = string.Empty;

            foreach (var item in shortCommandList)
            {
                toolTip.Text += item + '\n';
            }

			//tooltipLocation = this.GetPositionFromCharIndex(this.CaretPosition);
			//toolTip.Location = CalcTooltipLocation();

            toolTip.SelectedLine = toolTip.SelectedLine;
            
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
			if (tooltipLocation.X + tooltipOffset + toolTip.Size.Width > this.ClientSize.Width)
			{
				tooltipLocation.X = this.ClientSize.Width - toolTip.Width - tooltipOffset;
			}
			else
			{
				tooltipLocation.X = tooltipLocation.X + tooltipOffset;
			}

			if (tooltipLocation.Y + toolTip.Size.Height > this.ClientSize.Height)
			{
				tooltipLocation.Y = this.ClientSize.Height - toolTip.Height;
			}

			//tooltipLocation.X = this.ClientRectangle.Width - 50;
			//tooltipLocation.Y = 120;

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

		//public int MoveCaret(bool up)
		//{
		//	PapyrusString ptxt = this.Text;

		//	int lineCnt = ptxt.NumberOfLines;
		//	int caretLine = 0;
		//	int dir = up ? -1 : 1;

		//	if (caretLine + 1 < ptxt.LineIndices.Count)
		//	{

		//		while (	base.SelectionStart > ptxt.LineIndices[caretLine + 1])
		//		{
		//			base.SelectionStart++;
		//		}

		//		int positionInLine = base.SelectionStart - ptxt.LineIndices[caretLine];

		//		if (caretLine + dir - 1 < 0)
		//		{
		//			base.SelectionStart = positionInLine;
		//		}

		//		else
		//		{
		//			base.SelectionStart = positionInLine + ptxt.LineIndices[caretLine + dir - 1];
		//		}
		//	}

		//	return base.SelectionStart;
		//}

        public void InsertTooltipCommand()
        {
            string toInsert = toolTip.SelectedText;

			InsertCommand(toInsert);

            HideTooltip();          
        }

		public void InsertCommand(string what)
		{
			string toInsert = what;
			int i = 0;
			int tempCaret = base.SelectionStart;
			while (toInsert[i] != '\\' && i < toInsert.Length)
			{
				i++;
			}
			toInsert = toInsert.Substring(i);

			this.Text = this.Text.Insert(base.SelectionStart, toInsert.Substring(command.Length, toInsert.Length - command.Length));
			tempCaret += toInsert.Length - command.Length;
			base.SelectionStart = tempCaret;
		}

		public string ReverseString(string s)
		{
			char[] arr = s.ToCharArray();
			Array.Reverse(arr);
			return new string(arr);
		}

		//public void MoveCaretHorizontal(bool left, int num)
		//{

		//	int i = 0;
		//	if (left)
		//	{
		//		while (i < num)
		//		{
		//			if (base.SelectionStart - 1 > -1)
		//			{
		//				if (base.Text.Length > 0)
		//				{
		//					if (base.Text[base.SelectionStart - 1] == '\n')
		//					{
		//						base.SelectionStart--;
		//						if (base.SelectionStart - 1 > -1)
		//						{
		//							if (base.Text[base.SelectionStart - 1] == '\r')
		//							{
		//								base.SelectionStart--;
		//							}
		//						}
		//					}
		//				}

		//				base.SelectionStart--;
		//			}

		//			i++;
		//		}
		//	}

		//	else
		//	{
		//		while (i < num)
		//		{
		//			if (base.SelectionStart + 1 < base.Text.Length)
		//			{
		//				base.SelectionStart++;
		//			}

		//			if (base.SelectionStart + 1 < base.Text.Length)
		//			{
		//				if (base.Text[base.SelectionStart + 1] == '\r')
		//				{
		//					base.SelectionStart++;
		//					if (base.SelectionStart + 1 < base.Text.Length)
		//					{
		//						if (base.Text[base.SelectionStart + 1] == '\n')
		//						{
		//							base.SelectionStart++;
		//						}
		//					}
		//				}

		//				base.SelectionStart--;
		//			}

		//			base.SelectionStart++;

		//			i++;

		//		}
		//	}

		//}

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
					base.SelectionStart = temp;
					base.Select(base.SelectionStart, 0);
				}
			}

			if (toolTip.Visible && !PointInsideRectangle(e.Location, toolTip.ClientRectangle))
			{
				HideTooltip();
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

			//if ( e.Delta > 0 )
			//{
			//	//PapyrusVScroll( -1 );

			//	//SyntaxHighlight();
			//}
			//else
			//{
			//	//PapyrusVScroll( 1 );

			//	//SyntaxHighlight();
			//}

			if ( needsRecompilation )
			{
				PapyrusText = base.Text;
				needsRecompilation = true;
				//PapyrusText = temporaryModifiedContent;
				//SignalForRecompilation( papyrusText.Str );
			}
		}

		void PapyrusRichTextBox_KeyDown( object sender, KeyEventArgs e )
		{
			if ( e.KeyCode == Keys.Up )
			{
				if (toolTip.Visible)
				{
					e.SuppressKeyPress = true;
				}

			}

			else if ( e.KeyCode == Keys.Down )
			{
				if (toolTip.Visible)
				{
					e.SuppressKeyPress = true;
				}
			}

			else if (e.KeyCode == Keys.Left)
			{
				if (toolTip.Visible)
				{
					e.SuppressKeyPress = true;
				}
			}

			else if (e.KeyCode == Keys.Right)
			{
				if (toolTip.Visible)
				{
					e.SuppressKeyPress = true;
				}

				if (base.SelectionStart + 1 < base.Text.Length)
				{
					if (base.Text[base.SelectionStart + 1] == '}' || base.Text[base.SelectionStart + 1] == ']' || base.Text[base.SelectionStart + 1] == '>')
					{
						HighlightBracket(base.SelectionStart);
					}
				}
			}

			else if (e.KeyCode == Keys.Space && (ModifierKeys & Keys.Control) == Keys.Control)
			{
				int i = 1;
				string snip = string.Empty;
				while (base.SelectionStart - i > -1 && (this.Text[base.SelectionStart - i] != '\n' && this.Text[base.SelectionStart - i] != ' '))
				{
					snip += this.Text[base.SelectionStart - i];
					i++;
				}

				i--;

				snip = ReverseString(snip);

				if (snippets.ContainsKey(snip))
				{
					int tempCaret = base.SelectionStart;
					this.Text = this.Text.Insert(base.SelectionStart, snippets[snip]);

					//this.Text = this.Text.Remove(base.SelectionStart - i + 1, snip.Length);
					this.Text = this.Text.Remove(tempCaret - snip.Length, snip.Length);

					//base.SelectionStart = tempCaret;

					//base.SelectionStart += snippets[snip].Length;
					//base.SelectionStart -= snip.Length;

					tempCaret += snippets[snip].Length;
					tempCaret -= snip.Length;

					base.SelectionStart = tempCaret;

					//base.Select(base.SelectionStart, 0);

					//recompilationEvent(this.Text);

					SyntaxHighlight();
				}

				e.SuppressKeyPress = true;
			}

			else if (e.KeyCode == Keys.Back)
			{
				//caretPosition = caretPosition - 1 > -1 ? caretPosition-- : caretPosition;
				base.SelectionStart = base.SelectionStart;
				if (toolTip.Visible)
				{
					command = command.Substring(0, command.Length - 1);
					shortCommandList = fullCommandList;
					UpdateTooltip();
				}
			}

			else if (e.KeyCode == Keys.Escape)
			{
				if (toolTip.Visible)
				{
					HideTooltip();
				}
			}
		}

		void PapyrusRichTextBox_KeyUp( object sender, KeyEventArgs e )
		{
            if (e.KeyCode == Keys.Space)
            {
				tabPressed = false;
                if (toolTipVisible)
                {
                    InsertTooltipCommand();
					e.SuppressKeyPress = true;
                }
            }
            
			if ( e.KeyCode == Keys.Up )
			{
				tabPressed = false;
                if (toolTipVisible)
                {
                    toolTip.SelectedLine--;
					e.SuppressKeyPress = true;
                }
                else
                {
                    //MoveCaret(true);
                }
			}

			else if ( e.KeyCode == Keys.Down )
			{
				tabPressed = false;
                if (toolTipVisible)
                {
                    toolTip.SelectedLine++;
					e.SuppressKeyPress = true;
                }
                else
                {
                   // MoveCaret(false);
                }
			}

			else if ( e.KeyCode == Keys.Left )
			{
				//MoveCaretHorizontal(true, 1);
			}

			else if ( e.KeyCode == Keys.Right )
			{
				tabPressed = false;
				if (toolTipVisible)
				{
					InsertTooltipCommand();
					e.SuppressKeyPress = true;
				}

				else
				{
					//MoveCaretHorizontal(false, 1);
				}
			}

			else if (e.KeyCode == Keys.Enter)
			{
				//int caretBackup = caretPosition;
				//MoveCaret(false);
				//caretPosition++;
				//caretPosition = base.SelectionStart;
				SyntaxHighlight();
				//base.Select(caretPosition, 0);
			}

			//if ( e.KeyCode == Keys.PageUp )
			//{
			//	tabPressed = false;
			//	PapyrusVScroll( -papyrusText.LinesPerPage );
			//	e.SuppressKeyPress = true;
			//}

			//else if ( e.KeyCode == Keys.PageDown )
			//{
			//	tabPressed = false;
			//	PapyrusVScroll( papyrusText.LinesPerPage );
			//	e.SuppressKeyPress = true;
			//}

			else
			{
				tabPressed = false;
				temporaryModifiedContent = base.Text;

				needsRecompilation = true;
			}			


		}

        void PapyrusRichTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\\')
            {
                command = "\\";
                tooltipLocation = this.GetPositionFromCharIndex(base.SelectionStart + 50);
                DisplayTooltip(tooltipLocation);
               // base.SelectionStart++;
            }

            else if (((int)e.KeyChar > 64 && (int)e.KeyChar < 91) || ((int)e.KeyChar > 96 && (int)e.KeyChar < 123))
            {
                //base.SelectionStart++;
				if (toolTip.Visible)
				{
					command += e.KeyChar;
					UpdateTooltip();
				}
            }
        }

		void PapyrusRichTextBox_MouseMove( object sender, MouseEventArgs e )
		{
            //if ( toolTipVisible )
            //{
            //    if ( CalcToolTipDistance( e.Location ) > maxToolTipDistance )
            //    {
            //        HideTooltip();
            //    }
            //}

			if ( leftMouseClick )
			{
				isMouseDragging = true;
			}
		}

		void PapyrusRichTextBox_MouseDown( object sender, MouseEventArgs e )
		{
			leftMouseClick = true;
		}

		void PapyrusRichTextBox_TextChanged(object sender, EventArgs e)
		{
			this.papyrusText = this.Text;
		}


		#endregion

	}
}
