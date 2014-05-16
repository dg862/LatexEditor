using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Xml;
//using REditorLib;
using LatexHelpers;

namespace LatexEditor
{
    public delegate void RequestRead(FileIODescriptor fiod);

    public class OptionManager
    {
        #region Fields

        private OptionsForm optionForm;
        private string rawXmlData;
        private string id;
        private string miktexPath;
        private string texlivePath;
        private string iMagickPath;
        private string distribution;
        public event RequestRead readRequest;
		private Dictionary<string, string> snippetDict;

        #endregion

        #region Properties

        public string RawXmlData
        {
            get { return this.rawXmlData; }
            set
            {
                if (value != null)
                {
                    this.rawXmlData = value;
                }
            }
        }

        public string ID
        {
            get { return this.id; }
        }

		public Dictionary<string, string> SnippetDict
		{
			get { return snippetDict; }
		}

        #endregion

        #region Constructors

        public OptionManager(OptionsForm form)
        {
            optionForm = form;
			snippetDict = new Dictionary<string, string>();
        }

        #endregion

        #region Methods

        public void ScanForLatexDistros()
        {
            string path = Environment.GetEnvironmentVariable("Path");
            string[] dirs = path.Split(Path.PathSeparator);

            foreach (string item in dirs)
            {
                if (item.ToLower().Contains("miktex"))
                {
                    miktexPath = item;
                }

                if (item.ToLower().Contains("texlive"))
                {
                    texlivePath = item;
                }

                if (item.ToLower().Contains("imagemagick"))
                {
                    iMagickPath = item;
                }
            }

            if (!string.IsNullOrEmpty(texlivePath))
            {
                optionForm.DistroCombo.Items.Clear();

                optionForm.DistroCombo.Items.Add("TeX Live");

                optionForm.DistroCombo.Text = "TeX Live";
            }

            if (!string.IsNullOrEmpty(miktexPath))
            {
                optionForm.DistroCombo.Items.Clear();

                optionForm.DistroCombo.Items.Add("MiKTeX");

                optionForm.DistroCombo.Text = "MiKTeX";
            }

            if (optionForm.DistroCombo.Items.Count != 0)
            {
                if (optionForm.DistroCombo.Items.IndexOf("MiKTeX") != -1)
                {
                    optionForm.DistPathTb.Text = miktexPath;
                    optionForm.CompilerPathTb.Text = miktexPath + Constants.compilerName;
                    optionForm.CompilerArgsTb.Text = REditorLib.Constants.defaultCompilerArgs;
                    optionForm.TempFilesTb.Text = REditorLib.Constants.scratchPadPath;
                    optionForm.PreviewArgsTb.Text = REditorLib.Constants.defaultPreviewArgs;
                    optionForm.PreviewCodeTb.Text = REditorLib.Constants.defaultPreviewCode;
                }

                else
                {
                    optionForm.DistPathTb.Text = texlivePath;
                    optionForm.CompilerPathTb.Text = texlivePath + Constants.compilerName;
                    optionForm.CompilerArgsTb.Text = REditorLib.Constants.defaultCompilerArgs;
                    optionForm.TempFilesTb.Text = REditorLib.Constants.scratchPadPath;
                    optionForm.PreviewArgsTb.Text = REditorLib.Constants.defaultPreviewArgs;
                    optionForm.PreviewCodeTb.Text = REditorLib.Constants.defaultPreviewCode;
                }
            }

            if (!string.IsNullOrEmpty(iMagickPath))
            {
                optionForm.IMagickPathTb.Text = iMagickPath;
            }

            //App.Instance.OptionForm.DistroCombo.Items;
        }

        public void SaveOptions()
        {
			XmlWriterSettings settings = new XmlWriterSettings() { Indent = true };

			using ( XmlWriter wr = XmlWriter.Create( Constants.optionsFile, settings ) )
			{

				wr.WriteStartElement( "LatexEditorOptions" );

                wr.WriteStartElement("Paths");

                wr.WriteElementString("Distribution", optionForm.DistroCombo.Text);
                wr.WriteElementString("DistributionPath", optionForm.DistPathTb.Text);

                wr.WriteElementString("CompilerPath", optionForm.CompilerPathTb.Text);
                wr.WriteElementString("CompilerArgs", optionForm.CompilerArgsTb.Text);

                wr.WriteElementString("ImageMagickPath", optionForm.IMagickPathTb.Text);

                wr.WriteElementString("TemporaryFilePath", optionForm.TempFilesTb.Text);
                wr.WriteElementString("PreviewArguments", optionForm.PreviewArgsTb.Text);
                wr.WriteElementString("PreviewCode", optionForm.PreviewCodeTb.Text);

                wr.WriteEndElement();

				wr.WriteStartElement("Snippets");

				foreach (var item in snippetDict)
				{
					wr.WriteElementString(item.Key, item.Value);
				}

				wr.WriteEndElement();

				wr.WriteEndElement();
			}			
        }

        public void Load()
        {
            if (File.Exists(Constants.optionsFile))
            {
                FileIODescriptor fiod = new FileIODescriptor();
                fiod.Encoding = Encoding.UTF8;
                fiod.FileName = Constants.optionsFile;
                fiod.ID = Guid.NewGuid().ToString();
                fiod.Output = rawXmlData;
                id = fiod.ID;
                readRequest(fiod);
            }
        }

        public void ProcessOptions()
        {
            using (StringReader sr = new StringReader(rawXmlData))
            using (XmlReader xr = XmlReader.Create(sr))
            {
                XmlDocument doc = new XmlDocument();

                try
                {
                    doc.Load(xr);
                }
                catch (Exception)
                {
                    //We did not find a root element. This could mean that the option file is corrupt or does not exist. Either way, we stop processing it.
                    return;
                }

                XmlElement docRoot = doc.DocumentElement;

                if (docRoot.Name != "LatexEditorOptions")
                {
                    throw new Exception("Wrong option file format.");
                }

                XmlElement elem = (XmlElement)docRoot.SelectSingleNode("Paths");

                XmlNode innerNode = elem.SelectSingleNode("Distribution");
                distribution = innerNode.InnerText;

                innerNode = elem.SelectSingleNode("DistributionPath");
                if (distribution == "MiKTeX")
                {
                    miktexPath = innerNode.InnerText;
                    REditorLib.Constants.distributionPath = miktexPath;
                }

                else if(distribution == "TeX Live")
                {
                    texlivePath = innerNode.InnerText;
                    REditorLib.Constants.distributionPath = texlivePath;
                }

                innerNode = elem.SelectSingleNode("CompilerPath");
                REditorLib.Constants.compilerPath = innerNode.InnerText;

                innerNode = elem.SelectSingleNode("CompilerArgs");
                REditorLib.Constants.defaultCompilerArgs = innerNode.InnerText;

                innerNode = elem.SelectSingleNode("ImageMagickPath");
                REditorLib.Constants.imageMagickPath = innerNode.InnerText;

                innerNode = elem.SelectSingleNode("TemporaryFilePath");
                REditorLib.Constants.scratchPadPath = innerNode.InnerText;

                innerNode = elem.SelectSingleNode("PreviewArguments");
                REditorLib.Constants.defaultPreviewArgs = innerNode.InnerText;

                innerNode = elem.SelectSingleNode("PreviewCode");
                REditorLib.Constants.defaultPreviewCode = innerNode.InnerText;

                REditorLib.Constants.latexDistribution = distribution;

				elem = (XmlElement)docRoot.SelectSingleNode("Snippets");
				if (elem != null)
				{
					innerNode = elem.SelectSingleNode("*");

					while (innerNode != null)
					{
						snippetDict.Add(innerNode.Name, innerNode.InnerText);
						innerNode = innerNode.NextSibling;
					}
				}
            }         
        }

        #endregion

        #region Event handlers



        #endregion
    }
}
