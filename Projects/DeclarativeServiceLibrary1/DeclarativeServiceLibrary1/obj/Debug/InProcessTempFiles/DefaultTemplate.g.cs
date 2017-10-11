//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.269
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace TfsBuild {
    
    
    [System.Runtime.InteropServices.ComVisible(false)]
    public partial class Process : System.Activities.Activity, System.ComponentModel.ISupportInitialize {
        
        private bool _contentLoaded;
        
        private System.Activities.InArgument<Microsoft.TeamFoundation.Build.Workflow.Activities.BuildSettings> _BuildSettings;
        
        private System.Activities.InArgument<Microsoft.TeamFoundation.Build.Workflow.Activities.TestSpecList> _TestSpecs;
        
        private System.Activities.InArgument<string> _BuildNumberFormat;
        
        private System.Activities.InArgument<Microsoft.TeamFoundation.Build.Workflow.Activities.CleanWorkspaceOption> _CleanWorkspace;
        
        private System.Activities.InArgument<Microsoft.TeamFoundation.Build.Workflow.Activities.CodeAnalysisOption> _RunCodeAnalysis;
        
        private System.Activities.InArgument<Microsoft.TeamFoundation.Build.Workflow.Activities.SourceAndSymbolServerSettings> _SourceAndSymbolServerSettings;
        
        private System.Activities.InArgument<Microsoft.TeamFoundation.Build.Workflow.Activities.AgentSettings> _AgentSettings;
        
        private System.Activities.InArgument<bool> _AssociateChangesetsAndWorkItems;
        
        private System.Activities.InArgument<bool> _CreateWorkItem;
        
        private System.Activities.InArgument<bool> _DropBuild;
        
        private System.Activities.InArgument<string> _MSBuildArguments;
        
        private System.Activities.InArgument<Microsoft.TeamFoundation.Build.Workflow.Activities.ToolPlatform> _MSBuildPlatform;
        
        private System.Activities.InArgument<bool> _PerformTestImpactAnalysis;
        
        private System.Activities.InArgument<bool> _CreateLabel;
        
        private System.Activities.InArgument<bool> _DisableTests;
        
        private System.Activities.InArgument<string> _GetVersion;
        
        private System.Activities.InArgument<string> _PrivateDropLocation;
        
        private System.Activities.InArgument<Microsoft.TeamFoundation.Build.Workflow.BuildVerbosity> _Verbosity;
        
        private Microsoft.TeamFoundation.Build.Workflow.ProcessParameterMetadataCollection _Metadata;
        
        private Microsoft.TeamFoundation.Build.Client.BuildReason _SupportedReasons;
        
        public Process() {
            this.InitializeComponent();
        }
        
        public System.Activities.InArgument<Microsoft.TeamFoundation.Build.Workflow.Activities.BuildSettings> BuildSettings {
            get {
                return this._BuildSettings;
            }
            set {
                this._BuildSettings = value;
            }
        }
        
        public System.Activities.InArgument<Microsoft.TeamFoundation.Build.Workflow.Activities.TestSpecList> TestSpecs {
            get {
                return this._TestSpecs;
            }
            set {
                this._TestSpecs = value;
            }
        }
        
        public System.Activities.InArgument<string> BuildNumberFormat {
            get {
                return this._BuildNumberFormat;
            }
            set {
                this._BuildNumberFormat = value;
            }
        }
        
        public System.Activities.InArgument<Microsoft.TeamFoundation.Build.Workflow.Activities.CleanWorkspaceOption> CleanWorkspace {
            get {
                return this._CleanWorkspace;
            }
            set {
                this._CleanWorkspace = value;
            }
        }
        
        public System.Activities.InArgument<Microsoft.TeamFoundation.Build.Workflow.Activities.CodeAnalysisOption> RunCodeAnalysis {
            get {
                return this._RunCodeAnalysis;
            }
            set {
                this._RunCodeAnalysis = value;
            }
        }
        
        public System.Activities.InArgument<Microsoft.TeamFoundation.Build.Workflow.Activities.SourceAndSymbolServerSettings> SourceAndSymbolServerSettings {
            get {
                return this._SourceAndSymbolServerSettings;
            }
            set {
                this._SourceAndSymbolServerSettings = value;
            }
        }
        
        public System.Activities.InArgument<Microsoft.TeamFoundation.Build.Workflow.Activities.AgentSettings> AgentSettings {
            get {
                return this._AgentSettings;
            }
            set {
                this._AgentSettings = value;
            }
        }
        
        public System.Activities.InArgument<bool> AssociateChangesetsAndWorkItems {
            get {
                return this._AssociateChangesetsAndWorkItems;
            }
            set {
                this._AssociateChangesetsAndWorkItems = value;
            }
        }
        
        public System.Activities.InArgument<bool> CreateWorkItem {
            get {
                return this._CreateWorkItem;
            }
            set {
                this._CreateWorkItem = value;
            }
        }
        
        public System.Activities.InArgument<bool> DropBuild {
            get {
                return this._DropBuild;
            }
            set {
                this._DropBuild = value;
            }
        }
        
        public System.Activities.InArgument<string> MSBuildArguments {
            get {
                return this._MSBuildArguments;
            }
            set {
                this._MSBuildArguments = value;
            }
        }
        
        public System.Activities.InArgument<Microsoft.TeamFoundation.Build.Workflow.Activities.ToolPlatform> MSBuildPlatform {
            get {
                return this._MSBuildPlatform;
            }
            set {
                this._MSBuildPlatform = value;
            }
        }
        
        public System.Activities.InArgument<bool> PerformTestImpactAnalysis {
            get {
                return this._PerformTestImpactAnalysis;
            }
            set {
                this._PerformTestImpactAnalysis = value;
            }
        }
        
        public System.Activities.InArgument<bool> CreateLabel {
            get {
                return this._CreateLabel;
            }
            set {
                this._CreateLabel = value;
            }
        }
        
        public System.Activities.InArgument<bool> DisableTests {
            get {
                return this._DisableTests;
            }
            set {
                this._DisableTests = value;
            }
        }
        
        public System.Activities.InArgument<string> GetVersion {
            get {
                return this._GetVersion;
            }
            set {
                this._GetVersion = value;
            }
        }
        
        public System.Activities.InArgument<string> PrivateDropLocation {
            get {
                return this._PrivateDropLocation;
            }
            set {
                this._PrivateDropLocation = value;
            }
        }
        
        public System.Activities.InArgument<Microsoft.TeamFoundation.Build.Workflow.BuildVerbosity> Verbosity {
            get {
                return this._Verbosity;
            }
            set {
                this._Verbosity = value;
            }
        }
        
        public Microsoft.TeamFoundation.Build.Workflow.ProcessParameterMetadataCollection Metadata {
            get {
                return this._Metadata;
            }
            set {
                this._Metadata = value;
            }
        }
        
        public Microsoft.TeamFoundation.Build.Client.BuildReason SupportedReasons {
            get {
                return this._SupportedReasons;
            }
            set {
                this._SupportedReasons = value;
            }
        }
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if ((this._contentLoaded == true)) {
                return;
            }
            this._contentLoaded = true;
            string resourceName = this.FindResource();
            System.IO.Stream initializeXaml = typeof(Process).Assembly.GetManifestResourceStream(resourceName);
            System.Xml.XmlReader xmlReader = null;
            System.Xaml.XamlReader reader = null;
            System.Xaml.XamlObjectWriter objectWriter = null;
            try {
                System.Xaml.XamlSchemaContext schemaContext = XamlStaticHelperNamespace._XamlStaticHelper.SchemaContext;
                xmlReader = System.Xml.XmlReader.Create(initializeXaml);
                System.Xaml.XamlXmlReaderSettings readerSettings = new System.Xaml.XamlXmlReaderSettings();
                readerSettings.LocalAssembly = System.Reflection.Assembly.GetExecutingAssembly();
                readerSettings.AllowProtectedMembersOnRoot = true;
                reader = new System.Xaml.XamlXmlReader(xmlReader, schemaContext, readerSettings);
                System.Xaml.XamlObjectWriterSettings writerSettings = new System.Xaml.XamlObjectWriterSettings();
                writerSettings.RootObjectInstance = this;
                writerSettings.AccessLevel = System.Xaml.Permissions.XamlAccessLevel.PrivateAccessTo(typeof(Process));
                objectWriter = new System.Xaml.XamlObjectWriter(schemaContext, writerSettings);
                System.Xaml.XamlServices.Transform(reader, objectWriter);
            }
            finally {
                if ((xmlReader != null)) {
                    ((System.IDisposable)(xmlReader)).Dispose();
                }
                if ((reader != null)) {
                    ((System.IDisposable)(reader)).Dispose();
                }
                if ((objectWriter != null)) {
                    ((System.IDisposable)(objectWriter)).Dispose();
                }
            }
        }
        
        private string FindResource() {
            string[] resources = typeof(Process).Assembly.GetManifestResourceNames();
            for (int i = 0; (i < resources.Length); i = (i + 1)) {
                string resource = resources[i];
                if ((resource.Contains(".DefaultTemplate.g.xaml") || resource.Equals("DefaultTemplate.g.xaml"))) {
                    return resource;
                }
            }
            throw new System.InvalidOperationException("Resource not found.");
        }
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1033")]
        void System.ComponentModel.ISupportInitialize.BeginInit() {
        }
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1033")]
        void System.ComponentModel.ISupportInitialize.EndInit() {
            this.InitializeComponent();
        }
    }
}
