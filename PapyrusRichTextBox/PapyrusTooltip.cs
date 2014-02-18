using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace PapyrusDictionary
{
	public class PapyrusTooltip : RichTextBox
	{
		#region Fields

		private PapyrusString			papyrusText;
		private VScrollBar				scrollBar;
		private int						currentLine = 0;

		#endregion

		#region Properties

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

		public string PapyrusText
		{
			get { return papyrusText.Str; }
			set
			{
				if ( !string.IsNullOrEmpty( value ) )
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

		#endregion

		#region Methods

		public void Redraw()
		{
			base.Text = papyrusText[currentLine];
		}

		public void PapyrusVScroll(int lines)
		{
			base.Update();

			base.Text = papyrusText[currentLine + lines];

			if ( ( currentLine + lines >= 0 ) && ( currentLine + lines < papyrusText.NumberOfLines ) )
			{
				currentLine += lines;
			}

			scrollBar.Value = currentLine;			
		}

		#endregion

		#region Event handlers

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

		#endregion
	}
}
