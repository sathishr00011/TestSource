using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Manifest.Xml
{
    using Manifest.Core;

    /// <summary>
    /// Class for appstore specific manifest generation which includes AppStore, Enterprise, Websites etc based on supplied template
    /// </summary>
    public class WindowsServiceXmlGeneration : BaseXml

    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MobileDispatcherXmlGeneration"/> class.
        /// </summary>
        /// <param name="templateCategory">The template category.</param>
        /// <param name="regions">The regions.</param>
        /// <param name="version">The version.</param>
        /// <param name="outputManifestPath">The output manifest path.</param>
        /// <param name="tag">The tag.</param>
        public WindowsServiceXmlGeneration(string templateCategory, string regions, string version, string outputManifestPath, string tag, string searchDirectoryPath) :
            base(templateCategory, regions, version, outputManifestPath, tag, searchDirectoryPath)
        {
            try
            {    
                ProcessManifest();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Processes the manifest.
        /// </summary>
        protected override void ProcessManifest()
        {
            foreach (XDocument template in this.inmemXDocs)
            {
                this.isNewManifestModified = false;
                XDocument newManifestSkeleton = CreateNewManifestSkeletonFromTemplate(template);

                //IEnumerable<XAttribute> allComponentAttr = template.XPathSelectElements(@"//Component").Select(x => x.Attribute("Name")).Distinct();
                //IEnumerable<XAttribute> allComponentAttr = template.XPathSelectElements(@"//Package").Select(x => (x.Attribute("Name")) && (x.Attribute("Version"))).Distinct();
                IEnumerable<XElement> packages = (from xml2 in template.Descendants("Package")
                                                  select xml2);

                foreach (XElement element in packages)
                {
                    string name = element.Attribute("Name").Value;
                    string version = string.Empty;
                    if (element.Attribute("Version") != null)
                        version = element.Attribute("Version").Value;

                    string searchPattern = "*_" + name;

                    if (!string.IsNullOrWhiteSpace(version))
                    {
                        searchPattern += "_*_" + version;
                    }
                    else
                        searchPattern += "_*";

                    searchPattern += ".xml";

                    XDocument partialManifest = LoadPartialManifestFromUNCPath(searchPattern);
                    InMemoryNewManifestProcessing(name, name, newManifestSkeleton, partialManifest);
                }

                //foreach (XAttribute e in allComponentAttr)
                //{
                //    string searchPattern = "*_" + e.Value + "_*.xml";
                //    XDocument partialManifest = LoadPartialManifestFromUNCPath(searchPattern);
                //    InMemoryNewManifestProcessing(e.Value, e.Value, newManifestSkeleton, partialManifest);
                //}

                if (this.isNewManifestModified)
                    SaveNewManifest(template, newManifestSkeleton);
            }
        }

        /// <summary>
        /// Ins the memory new manifest processing.
        /// </summary>
        /// <param name="srcComponentName">Name of the SRC component.</param>
        /// <param name="destComponentName">Name of the dest component.</param>
        /// <param name="newManifestSkeleton">The new manifest skeleton.</param>
        /// <param name="partialManifest">The partial manifest.</param>
        protected override void InMemoryNewManifestProcessing(string srcComponentName, string destComponentName, XDocument newManifestSkeleton, XDocument partialManifest)
        {
            try
            {
                if (partialManifest != null)
                {
                    XElement firstPartialComponent = partialManifest.Element("PartialManifest").Elements("Package").FirstOrDefault();
                    if (firstPartialComponent != null && firstPartialComponent.HasAttributes)
                    {
                        if (firstPartialComponent.Attribute("ComponentName").Value.Equals(srcComponentName, StringComparison.InvariantCultureIgnoreCase))
                        {
                            string version = firstPartialComponent.Attribute("Version").Value;
                            string path = firstPartialComponent.Attribute("Path").Value;

                            if (File.Exists(path))
                            {
                                // path is a file.
                            }
                            else if (Directory.Exists(path))
                            {
                                // path is a directory.
                            }
                            else
                            {
                                // path doesn't exist.
                                throw new Exception(string.Format("File or Directory {0} doesn't exists while generating release manifest", path)); 
                            }


                            //Add version, path to new manifest under component nodes
                           // IEnumerable<XElement> matchedComponents = newManifestSkeleton.XPathSelectElements(@"//Component[@Name='" + destComponentName + "']");
                            IEnumerable<XElement> matchedComponents = newManifestSkeleton.XPathSelectElements(@"//Package[@Name='" + destComponentName + "']");

                            if (matchedComponents.Any())
                            {
                                matchedComponents.ToList().ForEach(x =>
                                    {
                                        x.SetAttributeValue("Version", version);
                                        x.SetAttributeValue("Path", path);
                                    });
                            }

                            //Add version to new manifest under package nodes
                            IEnumerable<XElement> matchedPackages = newManifestSkeleton.XPathSelectElements(@"//Package[@Name='" + destComponentName + "']");

                            if (matchedPackages.Any())
                            {
                                matchedPackages.ToList().ForEach(x =>
                                {
                                    x.SetAttributeValue("Version", version);
                                });
                            }

                            this.isNewManifestModified = true;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("Error processing the partial manifest for component -> {0} \r\n", srcComponentName);
                sb.Append("Message : " + e.Message);

                throw new Exception(sb.ToString(), e);
            }
        }
    }
}
