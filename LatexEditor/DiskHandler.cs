using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Threading;
using System.IO;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using LatexHelpers;

namespace LatexEditor
{
	//delegate void DiskEventHandler( RunWorkerCompletedEventArgs args );
	//delegate void DiskEventProgressHandler( ProgressChangedEventArgs args );

	public class DiskHandler : IDiskHandler
	{
		#region Fields

		private int								maxDiskAccessThreads;
		private int								currentDiskAccessThreads;
		private ArrayList						filesToRead;
		private BackgroundWorker[]				diskAccessThreads;
		public event DiskEventHandler			diskOperationDone;
		//public event DiskEventProgressHandler	diskOperationProgress;

		#endregion

		#region Properties

		public int MaxDiskAccessCount
		{
			get { return maxDiskAccessThreads; }
			set
			{
				if ( value > 0 )
				{
					maxDiskAccessThreads = value;
				}
			}
		}

		#endregion

		#region Constructors

		public DiskHandler()
		{
			filesToRead = new ArrayList();
			maxDiskAccessThreads = 1;
			currentDiskAccessThreads = 0;
			//diskAccessThreads = new Thread[maxDiskAccessThreads];
			//filesToReadSemaphore		= new Semaphore( 1, 1 );
			diskAccessThreads = new BackgroundWorker[maxDiskAccessThreads];
			diskAccessThreads[0].DoWork += new DoWorkEventHandler( DiskHandler_DoWork );
			diskAccessThreads[0].RunWorkerCompleted += new RunWorkerCompletedEventHandler( DiskHandler_RunWorkerCompleted );
			//diskAccessThreads[0].ProgressChanged += new ProgressChangedEventHandler( DiskHandler_ProgressChanged );
			diskAccessThreads[0].WorkerReportsProgress = true;
		}

		public DiskHandler( int maxThreadCount )
		{
			filesToRead = new ArrayList();
			maxDiskAccessThreads = maxThreadCount;
			currentDiskAccessThreads = 0;
			//diskAccessThreads = new Thread[maxDiskAccessThreads];
			//filesToReadSemaphore		= new Semaphore( 1, 1 );
			diskAccessThreads = new BackgroundWorker[maxDiskAccessThreads];
			for ( int i = 0; i < maxDiskAccessThreads; i++ )
			{
				diskAccessThreads[i] = new BackgroundWorker();
				diskAccessThreads[i].DoWork += new DoWorkEventHandler( DiskHandler_DoWork );
				diskAccessThreads[i].RunWorkerCompleted += new RunWorkerCompletedEventHandler( DiskHandler_RunWorkerCompleted );
				//diskAccessThreads[i].ProgressChanged += new ProgressChangedEventHandler( DiskHandler_ProgressChanged );
				diskAccessThreads[i].WorkerReportsProgress = true;
			}
			
		}

		#endregion

		#region Methods

		public void AddFileToAccessQueue( FileIODescriptor fileIODesc )
		{
			//add file to disk access queue
			filesToRead.Add( fileIODesc );

			//first, check if there are any free worker threads we can use
			for ( int i = 0; i < currentDiskAccessThreads; i++ )
			{
				if ( !diskAccessThreads[i].IsBusy )
				{
					diskAccessThreads[i].RunWorkerAsync();
					return;
				}
			}
			//check if there is a free thread slot, if there are fewer threads than max, create a new one
			//TODO: write else branch
			if ( currentDiskAccessThreads < maxDiskAccessThreads )
			{
				//diskAccessThreads[currentDiskAccessThreads].RunWorkerAsync( fileIODesc );
				diskAccessThreads[currentDiskAccessThreads].RunWorkerAsync();
				currentDiskAccessThreads++;
				return;
			}

			//otherwise, wait for a bit for a free slot
			for ( int j = 0; j < 5; j++ )
			{
				for ( int i = 0; i < currentDiskAccessThreads; i++ )
				{
					System.Threading.Thread.Sleep( 50 );
					if ( !diskAccessThreads[i].IsBusy )
					{
						diskAccessThreads[i].RunWorkerAsync();
						return;
					}
				}
			}
		}

		public void ClearFileArray()
		{

		}

		private void PopAccessQueueHead( int index )
		{
			filesToRead.RemoveAt( index );
		}

		private void ReadFileInOneStep( StreamReader reader, out string output )
		{
			output = reader.ReadToEnd();
		}

		private void ReadNumberOfCharsInBlocks( StreamReader reader, int blockSize, int numberOfChars, out string output )
		{
			char []temp = new char[blockSize + 1];
			int index = numberOfChars;
			output = string.Empty;

			while ( index != 0 )
			{
				index -= reader.Read( temp, 0, index < blockSize ? index : blockSize );
				output += new string( temp );
			}

			//output = reader.ReadToEnd();
		}

		private void ReadWholeFileInBlocks( StreamReader reader, int blockSize, ref string output )
		{
			char []temp = new char[blockSize + 1];
			//output = string.Empty;

			int ret = 0;

			//output = reader.ReadToEnd();

			while ( ( ret = reader.Read( temp, 0, blockSize ) ) == blockSize )
			{
				output += new string( temp, 0, blockSize );
			}

			output += new string( temp, 0, ret );
		}

		private void GetDelimitedSubstrings( string wholeString, string delim, ref List<string> list )
		{
			int count = 0;
			int delimIndex = wholeString.IndexOf( delim, 0 );

			//count the number of output entries, so we can create the list with a known capacity
			while ( delimIndex != -1 )
			{
				count++;
				delimIndex = wholeString.IndexOf( delim, delimIndex + delim.Length );
			}

			//create the list with the capacity we got
			//list = new List<string>( count );
			list.Capacity = count;

			string toAdd = string.Empty;
			int i = 0;
			while ( i < wholeString.Length )
			{
				int j = 0;
				while ( wholeString[i] == delim[j] )
				{
					i++;
					j++;

					if ( i >= wholeString.Length )
					{
						goto Finish;
					}

					if ( j >= delim.Length )
					{
						break;
					}
				}

				while ( wholeString[i] != delim[0] )
				{
					toAdd += wholeString[i];
					i++;
					if ( i >= wholeString.Length )
					{
						goto AddAndFinish;
					}
				}

				list.Add( toAdd );
				toAdd = string.Empty;
			}

		Finish:
			return;
		AddAndFinish:
			list.Add( toAdd );
			return;
		}

		#endregion

		#region Event handlers

		void DiskHandler_DoWork( object sender, DoWorkEventArgs e )
		{
			FileIODescriptor fiod;
			StreamReader reader;
			//string tempOutput = string.Empty;
			List<string> list = null;
			string tempOutput = null;
			Monitor.Enter( filesToRead );
			try
			{
				fiod = ( (FileIODescriptor) filesToRead[0] );
				filesToRead.RemoveAt( 0 );
				e.Result = fiod;													//set the result to the io descriptor so that the manager can know the ID
			}
			finally
			{
				Monitor.Exit( filesToRead );
			}

			try
			{
				if ( fiod.Write )
				{
					using ( FileStream stream = new FileStream( fiod.FileName, FileMode.Create, FileAccess.Write ) )
					using ( StreamWriter writer = new StreamWriter( stream ) )
					{
						writer.Write( fiod.Output );
					}
				}

				else if (fiod.IsImage)
				{
					if (File.Exists(fiod.FileName))
					{
						fiod.OutputImage = Image.FromFile(fiod.FileName);
					}
				}

				else
				{
					using ( FileStream stream = new FileStream( fiod.FileName, FileMode.Open, FileAccess.Read, fiod.ShareType, fiod.BlockSize, fiod.IsAsync ) )
					using ( reader = new StreamReader( stream, fiod.Encoding, false ) )
					{
						int index = 0;
						int offset = fiod.StartOffset;
						int len = fiod.BlockSize;
						list = fiod.OutputList;

						//seek the starting position
						if ( reader.BaseStream.CanSeek )
						{
							reader.BaseStream.Seek( fiod.StartOffset, SeekOrigin.Begin );
						}

						if ( len == -1 )															//read the whole file in one step
						{
							ReadFileInOneStep( reader, out tempOutput );
						}
						else
						{
							if ( fiod.CharsToRead == -1 )											//read the whole file in blocks
							{
								ReadWholeFileInBlocks( reader, fiod.BlockSize, ref tempOutput );
							}
							else																	//otherwise just read the given number of chars (but also in blocks)
							{
								ReadNumberOfCharsInBlocks( reader, fiod.BlockSize, fiod.CharsToRead, out tempOutput );
							}

						}
					}
				}
			}
			catch ( Exception ex )
			{
				MessageBox.Show( ex.Message );
			}

			if ( !fiod.Write && !fiod.IsImage )
			{
				if ( !string.IsNullOrEmpty( fiod.OutputDelimiter ) )
				{
					//List<string> list;
					GetDelimitedSubstrings( tempOutput, fiod.OutputDelimiter, ref list );
					//	fiod.OutputList = list;
				}
				else
				{
					fiod.Output = tempOutput;
				}
			}
		}

		void DiskHandler_RunWorkerCompleted( object sender, RunWorkerCompletedEventArgs e )
		{
			if ( diskOperationDone != null )
				diskOperationDone( (FileIODescriptor)e.Result );
		}

		#endregion		
	}
}
