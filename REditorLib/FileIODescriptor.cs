using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;

namespace LatexHelpers
{
	public class FileIODescriptor
	{
		#region Fields

		private string			fileName;
		private bool			write;
		private Encoding		encoding;
		private FileShare		shareType = FileShare.None;
		private bool			isAsync = false;
		private bool			isImage;
		private Image			outputImage;
		private string			output;
		private string			id;
		private int				startOffset = 0;
		private int				charsToRead = -1;
		private int				blockSize = 50000;
		private bool			aggregateBlocks;			//deprecated
		private string			outputDelimiter;			//Optional - if a delimiter is given, the results will be placed in the outputList
		private List<string>	outputList;					//Optional, if arrayDelimiter is not given, otherwise the results are placed here

		#endregion

		#region Properties

		public List<string> OutputList
		{
			get { return outputList; }
			set { outputList = value; }
		}

		public string OutputDelimiter
		{
			get { return outputDelimiter; }
			set { outputDelimiter = value; }
		}

		public string Output
		{
			get { return output; }
			set { output = value; }
		}

		public Encoding Encoding
		{
			get { return encoding; }
			set { encoding = value; }
		}

		public string FileName
		{
			get { return fileName; }
			set { fileName = value; }
		}

		public string ID
		{
			get { return id; }
			set { id = value; }
		}

		public int StartOffset
		{
			get { return startOffset; }
			set 
			{
				if ( value >= 0 )
					startOffset = value;
				else
					startOffset = 0;
			}
		}

		public int BlockSize
		{
			get { return blockSize; }
			set { blockSize = value; }
		}

		public bool IsAggregating
		{
			get { return aggregateBlocks; }
			set { aggregateBlocks = value; }
		}

		public FileShare ShareType
		{
			get { return shareType; }
			set { shareType = value; }
		}

		public int CharsToRead
		{
			get { return charsToRead; }
			set { charsToRead = value; }
		}

		public bool IsAsync
		{
			get { return isAsync; }
			set { isAsync = value; }
		}

		public bool IsImage { get { return isImage; } set { isImage = value; } }

		public Image OutputImage
		{
			get { return outputImage; }
			set { outputImage = value; }
		}

		public bool Write
		{
			get { return write; }
			set { write = value; }
		}
		
		#endregion

		#region Constructors

		//public FileIODescriptor( string fileName, Encoding encoding, int id, string output, bool aggregate )
		public FileIODescriptor( string fileName, Encoding encoding, string id, string output )
		{
			this.fileName			= fileName;
			this.encoding			= encoding;
			this.output				= output;
			this.id					= id;
			//this.aggregateBlocks	= aggregate;
		}

		public FileIODescriptor( string fileName, Encoding encoding, string id, string output, FileShare share, int length, int offset )
		{
			this.fileName = fileName;
			this.encoding = encoding;
			this.output = output;
			this.id = id;
			this.shareType = share;
			this.charsToRead = length;
			this.startOffset = offset;
		}

		public FileIODescriptor( string id, string output )
		{
			this.output = output;
			this.id = id;
		}

		public FileIODescriptor(string id, string fileName, bool isImage )
		{
			this.id = id;
			this.fileName = fileName;
			this.isImage = isImage;
		}

		public FileIODescriptor(){}

		#endregion

		#region Methods
		#endregion
	}
}
