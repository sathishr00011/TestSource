using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;

using Manifest.Contracts;

namespace ManifestActivityLibrary
{
    /// <summary>
    /// Code Activity that generate workflow based on template category
    /// </summary>
    public sealed class GenerateManifestCodeActivity : CodeActivity
    {
        public InArgument<string> version { get; set; }
        public InArgument<string> templateCategory { get; set; }
        public InArgument<string> regions { get; set; }
        public InArgument<string> outputManifestPath { get; set; }
        public InArgument<string> searchDirectoryPath { get; set; }
        public InArgument<string> tag { get; set; }

        /// <summary>
        /// Creates and validates a description of the activity’s arguments, variables, child activities, and activity delegates.
        /// </summary>
        /// <param name="metadata">The activity’s metadata that encapsulates the activity’s arguments, variables, child activities, and activity delegates.</param>
        protected override void CacheMetadata(CodeActivityMetadata metadata)
        {
            base.CacheMetadata(metadata);

            if (this.version == null)
                metadata.AddValidationError("version is required");

            if (this.templateCategory == null)
                metadata.AddValidationError("manifest template name is required");

            if (this.outputManifestPath == null)
                metadata.AddValidationError("manifest output path is required");

            if (this.tag == null)
                metadata.AddValidationError("tag is required");

            metadata.RequireExtension<IManifest>();
        }

        /// <summary>
        /// When implemented in a derived class, performs the execution of the activity.
        /// </summary>
        /// <param name="context">The execution context under which the activity executes.</param>
        protected override void Execute(CodeActivityContext context)
        {
            string templateCategory = context.GetValue(this.templateCategory);
            string regions = context.GetValue(this.regions);
            string version = context.GetValue(this.version);
            string outputManifestPath = context.GetValue(this.outputManifestPath);
            string tag = context.GetValue(this.tag);
            string searchDirectoryPath = context.GetValue(this.searchDirectoryPath);
            var extensions = context.GetExtension<IManifest>();
            extensions.GenerateManifestFromTemplate(templateCategory, regions, version, outputManifestPath, tag, searchDirectoryPath);
        }
    }
}
