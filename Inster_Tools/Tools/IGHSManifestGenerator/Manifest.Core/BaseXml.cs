using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Manifest.Core
{
    public abstract class BaseXml
    {
        protected XDocument appSettingsDoc;
        protected string[] manifestTemplates;
        protected string[] regions;
        protected string version;
        protected string templateCategory;
        protected string outputManifestPath;
        protected string currentAssemblyDirectoryName;
        protected List<XDocument> inmemXDocs;
        protected string manifestTName;
        protected string tag;
        protected bool isNewManifestModified;
        protected string searchDirectoryPath;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseXml"/> class.
        /// </summary>
        /// <param name="templateCategory">The template category.</param>
        /// <param name="regions">The regions.</param>
        /// <param name="version">The version.</param>
        /// <param name="outputManifestPath">The output manifest path.</param>
        /// <param name="tag">The tag.</param>
        public BaseXml(string templateCategory, string regions, string version, string outputManifestPath, string tag, string searchDirectoryPath)
        {
            try
            {
                this.regions = !string.IsNullOrWhiteSpace(regions) ? regions.Split(',') : null;
                this.version = version;
                this.outputManifestPath = outputManifestPath;
                this.templateCategory = templateCategory;
                this.tag = tag;
                this.isNewManifestModified = false;
                this.searchDirectoryPath = searchDirectoryPath;
                currentAssemblyDirectoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                appSettingsDoc = XDocument.Load(currentAssemblyDirectoryName + @"\App.config");
                XElement templates = appSettingsDoc.Element("configuration").Element("Templates").Elements("Template").First(x => x.Attribute("name").Value.Equals(templateCategory, StringComparison.InvariantCultureIgnoreCase));
                manifestTName = appSettingsDoc.Element("configuration").Element("Manifest").Attribute("templatename").Value;

                if (this.regions == null || this.regions.Length == 0)
                {
                    manifestTemplates = templates.Elements("artifacts")
                        .Where(x => !string.IsNullOrWhiteSpace(x.Attribute("filename").Value))
                        .Select(x => x.Attribute("filename").Value).ToArray<string>();
                }
                else
                {
                    manifestTemplates = templates.Elements("artifacts")
                        .Where(x => !string.IsNullOrWhiteSpace(x.Attribute("region").Value) && this.regions.Contains(x.Attribute("region").Value, StringComparer.InvariantCultureIgnoreCase) && !string.IsNullOrWhiteSpace(x.Attribute("filename").Value))
                        .Select(x => x.Attribute("filename").Value).ToArray<string>();
                }

                LoadTemplates();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Loads the templates.
        /// </summary>
        protected virtual void LoadTemplates()
        {
            inmemXDocs = new List<XDocument>();
            var templatePath = @"\Templates\";
            var templatePathWithRelease = @"\Templates\" + this.tag + @"\";
            foreach (string template in this.manifestTemplates)
            {
                XDocument apptemplate;
                if (File.Exists(currentAssemblyDirectoryName + templatePathWithRelease + template))
                    apptemplate = XDocument.Load(currentAssemblyDirectoryName + templatePathWithRelease + template);
                else
                    apptemplate = XDocument.Load(currentAssemblyDirectoryName + templatePath + template);
                inmemXDocs.Add(apptemplate);
            }
        }

        /// <summary>
        /// Processes the manifest.
        /// </summary>
        protected abstract void ProcessManifest();

        /// <summary>
        /// Ins the memory new manifest processing.
        /// </summary>
        /// <param name="srcComponentName">Name of the SRC component.</param>
        /// <param name="destComponentName">Name of the dest component.</param>
        /// <param name="newManifestSkeleton">The new manifest skeleton.</param>
        /// <param name="partialManifest">The partial manifest.</param>
        protected abstract void InMemoryNewManifestProcessing(string srcComponentName, string destComponentName, XDocument newManifestSkeleton, XDocument partialManifest);

        /// <summary>
        /// Creates the new manifest skeleton from template.
        /// </summary>
        /// <param name="template">The template.</param>
        /// <returns></returns>
        protected virtual XDocument CreateNewManifestSkeletonFromTemplate(XDocument template)
        {
            TextReader tr = new StringReader(template.ToString());
            XDocument newManifest = XDocument.Load(tr);

            return newManifest;
        }

        /// <summary>
        /// Loads the partial manifest from UNC path.
        /// </summary>
        /// <param name="searchPattern">The search pattern.</param>
        /// <returns></returns>
        protected virtual XDocument LoadPartialManifestFromUNCPath(string searchPattern)
        {
            XDocument partialManifestDoc = null;
            try
            {
                StringBuilder partialManifestPath = new StringBuilder();
                partialManifestPath.Append(this.outputManifestPath);
                //partialManifestPath.Append(this.tag);
                string pathToSearch = string.IsNullOrWhiteSpace(searchDirectoryPath) ? outputManifestPath : searchDirectoryPath;
                DirectoryInfo di = new DirectoryInfo(pathToSearch);

                FileInfo fiLatest;
                FileInfo[] matchingFiles = di.GetFiles(searchPattern, SearchOption.AllDirectories);
                if (matchingFiles != null && matchingFiles.Any())
                {
                    fiLatest = matchingFiles.OrderByDescending(fi => fi.CreationTime).First(f => f.FullName.Contains(this.tag));
                    string file = Path.GetFileName(fiLatest.FullName);

                    partialManifestDoc = XDocument.Load(fiLatest.FullName);
                }
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("Error loading the partial manifest for component -> {0}", searchPattern), e);
            }

            return partialManifestDoc;
        }

        /// <summary>
        /// Saves the new manifest.
        /// </summary>
        /// <param name="templateUsed">The template used.</param>
        /// <param name="newManifestModifiedSkeleton">The new manifest modified skeleton.</param>
        protected void SaveNewManifest(XDocument templateUsed, XDocument newManifestModifiedSkeleton)
        {
            string region = templateUsed.Element("ReleaseManifest").Attribute("Region").Value;

            //Add release version to new manifest
            IEnumerable<XElement> matchedComponents = newManifestModifiedSkeleton.XPathSelectElements(@"//ReleaseManifest[@Region='" + region + "']");
            if (matchedComponents.Any())
            {
                matchedComponents.ToList().ForEach(x =>
                {
                    x.SetAttributeValue("ReleaseVersion", version);
                });
            }

            string manifestName = string.Format("{0}_{1}_{2}_{3}.xml", this.manifestTName, region, this.templateCategory, this.version);

            StringBuilder fullManifestName = new StringBuilder();
            fullManifestName.Append(this.outputManifestPath);
            fullManifestName.Append(@"\");
            fullManifestName.Append(manifestName);

            newManifestModifiedSkeleton.Save(fullManifestName.ToString());
        }
    }
}
