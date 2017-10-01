using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Manifest.Contracts;
using Manifest.Xml;

namespace Manifest.DefaultImpl
{
    /// <summary>
    /// Generates the manifest from template for Tibco
    /// </summary>
    public class TibcoManifestFromTemplate : IManifest
    {
        /// <summary>
        /// Generates the manifest from template.
        /// </summary>
        /// <param name="templateCategory">The template category.</param>
        /// <param name="regions">The regions.</param>
        /// <param name="version">The version.</param>
        /// <param name="outputManifestPath">The output manifest path.</param>
        /// <param name="tag">The tag.</param>
        public void GenerateManifestFromTemplate(string templateCategory, string regions, string version, string outputManifestPath, string tag, string searchDirectoryPath)
        {
            TibcoXmlGeneration xmlGen = new TibcoXmlGeneration(templateCategory, regions, version, outputManifestPath, tag, searchDirectoryPath);
        }
    }
}
