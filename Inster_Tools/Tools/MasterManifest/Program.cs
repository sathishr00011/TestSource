using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.Build.Client;
using System.Xml;

namespace MasterManifest
{
    class Program
    {
        static void Main(string[] args)
        {
            /// Uncomment the following line to debug
            //System.Diagnostics.Debug.Assert(false);
            TeamFoundationServer tfs = new TeamFoundationServer("http://tfsapp.dotcom.tesco.org/tfs/Grocery");
            IBuildServer buildServer = (IBuildServer)tfs.GetService(typeof(IBuildServer));
            //"tag:R5.0 components:AppStore,Instore,Tibco,OMS regions:UK,CE,CZ,PL,SK mastermanifest:0.0.0"
            Dictionary<string, string> argDict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            args.ToList().ForEach(s => argDict.Add(s.Split(':')[0], s.Split(':')[1]));
            var buildQueue = buildServer.QueryBuilds("InternationalDeployment").OrderByDescending(b=>b.StartTime).ToList();
            string tag = argDict["tag"];
            string superManifestVersion = argDict["mastermanifest"];
            string[] components = argDict["components"].Split(',');
            string[] regions = argDict["regions"].Split(',');
            string manifestFileNameFormat = "ReleaseManifest_IGHS_{0}_{1}_{2}.xml"; // region, component, version
            string releaseFileName = String.Format("Release_{0}.xml", superManifestVersion);
            string rootFolder = @"\\UKTEE01-CLUSDB\BuildOutput\IGHS_Manifest\ManifestAutomation";
            Dictionary<string, string> ManifestVersionTable = new Dictionary<string, string>();

            foreach (var component in components)
            {
                foreach (var buildDetail in buildQueue)
                {
                    if (buildDetail.BuildNumber.StartsWith(String.Format("{0}_ManifestGeneration_{1}", tag, component)) && buildDetail.Status == BuildStatus.Succeeded && buildDetail.Quality == "Ready for Deployment")
                    {
                        ManifestVersionTable[component] = buildDetail.BuildNumber.Substring(buildDetail.BuildNumber.LastIndexOf("_") + 1);
                        break;
                    }
                }
            }

            

            var XmlDocument = new XmlDocument();
            var Declaration = XmlDocument.CreateXmlDeclaration("1.0", "utf-8", String.Empty);
            XmlDocument.AppendChild(Declaration);

            var masterManifestNode = XmlDocument.CreateElement("EnterpriseManifest");
            masterManifestNode.SetAttribute("Version", superManifestVersion);
            masterManifestNode.SetAttribute("DateCreatedUTC", DateTime.UtcNow.ToString("yyyyMMddTHHmmss"));
            var manifestsNode = XmlDocument.CreateElement("Manifests");
            foreach (var region in regions)
            {
                foreach (var component in ManifestVersionTable)
                {
                    string filename = Path.Combine(rootFolder, String.Format(manifestFileNameFormat, region, component.Key, component.Value));
                    if (File.Exists(filename))
                    {
                        var manifestNode = XmlDocument.CreateElement("Manifest");
                        manifestNode.SetAttribute("Region", region);
                        manifestNode.SetAttribute("ComponentGroup", component.Key);
                        manifestNode.SetAttribute("Version", component.Value);
                        manifestNode.SetAttribute("Path", filename);
                        manifestsNode.AppendChild(manifestNode);
                    }
                }
                masterManifestNode.AppendChild(manifestsNode);
                XmlDocument.AppendChild(masterManifestNode);
            }
            XmlDocument.Save(Path.Combine(rootFolder, releaseFileName));
        }
    }
}

