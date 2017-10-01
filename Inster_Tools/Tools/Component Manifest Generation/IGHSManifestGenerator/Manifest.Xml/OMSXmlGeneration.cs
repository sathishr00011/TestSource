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
    /// Class for OMS specific manifest generation which includes OMS etc based on supplied template
    /// </summary>
    public class OMSXmlGeneration : BaseXml
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OMSXmlGeneration"/> class.
        /// </summary>
        /// <param name="templateCategory">The template category.</param>
        /// <param name="regions">The regions.</param>
        /// <param name="version">The version.</param>
        /// <param name="outputManifestPath">The output manifest path.</param>
        /// <param name="tag">The tag.</param>
        public OMSXmlGeneration(string templateCategory, string regions, string version, string outputManifestPath, string tag, string searchDirectoryPath) :
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

                IEnumerable<XAttribute> allComponentAttr = template.XPathSelectElements(@"//Component").Select(x => x.Attribute("Name")).Distinct();

                foreach (XAttribute e in allComponentAttr)
                {
                    string searchPattern = "*_" + e.Value + "_*.xml";
                    XDocument partialManifest = LoadPartialManifestFromUNCPath(searchPattern);

                    if (partialManifest == null)
                        ExecuteRules(e.Value, newManifestSkeleton);
                    else
                        InMemoryNewManifestProcessing(e.Value, e.Value, newManifestSkeleton, partialManifest);
                }

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
                    IEnumerable<XElement> allPartialComponents = partialManifest.Element("PartialManifest").Elements("Package");
                    foreach (XElement partialComponent in allPartialComponents)
                    {
                        if (partialComponent != null && partialComponent.HasAttributes)
                        {
                            if (partialComponent.Attribute("ComponentName").Value.Equals(srcComponentName, StringComparison.InvariantCultureIgnoreCase))
                            {
                                string version = partialComponent.Attribute("Version").Value;
                                string path = partialComponent.Attribute("Path").Value;

                                //Add version, path to new manifest under component nodes
                                IEnumerable<XElement> matchedComponents = newManifestSkeleton.XPathSelectElements(@"//Component[@Name='" + destComponentName + "']");

                                if (matchedComponents.Any())
                                {
                                    matchedComponents.ToList().ForEach(x =>
                                    {
                                        x.SetAttributeValue("Version", version);
                                        x.SetAttributeValue("SourcePath", path);
                                    });
                                }

                                this.isNewManifestModified = true;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("Error processing the partial manifest for component -> {0}", srcComponentName), e);
            }
        }

        #region private methods        
        /// <summary>
        /// Executes the rules.
        /// </summary>
        /// <param name="componentName">Name of the component.</param>
        private void ExecuteRules(string componentName, XDocument newManifestSkeleton)
        {
            XElement templates = appSettingsDoc.Element("configuration").Element("Templates").Elements("Template").First(x => x.Attribute("name").Value.Equals(templateCategory, StringComparison.InvariantCultureIgnoreCase));

            IEnumerable<XElement> compRef = templates.Elements("artifacts").Elements("rules").Elements("componentref");
            if (compRef != null && compRef.Any())
            {
                foreach (XElement e in compRef)
                {
                    string destination = e.Element("component").Attribute("destination").Value;
                    string source = e.Element("component").Attribute("source").Value;                   

                    string searchPattern = "*_" + source + "_*.xml";
                    XDocument partialManifest = LoadPartialManifestFromUNCPath(searchPattern);

                    InMemoryNewManifestProcessing(source, destination, newManifestSkeleton, partialManifest);                   
                }
            }
        }
        #endregion
    }
}
