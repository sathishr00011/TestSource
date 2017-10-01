using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Manifest.Contracts
{
    /// <summary>
    /// Interface for manifest
    /// </summary>
    public interface IManifest
    {
        /// <summary>
        /// Generates the manifest from template.
        /// </summary>
        /// <param name="templateCategory">The template category.</param>
        /// <param name="regions">The regions.</param>
        /// <param name="version">The version.</param>
        /// <param name="outputManifestPath">The output manifest path.</param>
        /// <param name="tag">The tag.</param>
        void GenerateManifestFromTemplate(string templateCategory, 
            string regions, string version, string outputManifestPath, string tag,
            string searchDirectoryPath);
    }
}
