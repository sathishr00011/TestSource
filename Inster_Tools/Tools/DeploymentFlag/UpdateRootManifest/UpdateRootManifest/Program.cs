using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Globalization;
using System.IO;
using System.Net.Mail;
using System.Configuration;
using System.Xml.Xsl;
using System.Collections;
using System.Collections.Specialized;

namespace UpdateRootManifest
{
    class Program
    {
        
        static void Main(string[] args)
        {           

            if (args[0] == "UpdateManifest")
            {
                if (args.Length != 4)
                {
                    Console.WriteLine("Operation ('UpdateManifest / NotifyDeployment '),Root Manifest File path and Component Manifest File path needed as arguments");
                    return;

                }
                Util.Component = args[3];
                Util.JenkinsURL = ConfigurationManager.AppSettings["JenkinsURL"];
                ContinuousDeploy cdploy = new ContinuousDeploy();                
                string ComponentManifestFileName = Util.UpdateComponentManifest(args[2], Util.Component);
                cdploy.UpdateRootManifest(args[1], args[2], ComponentManifestFileName);

            }


            else if (args[0] == "NotifyDeployment")
            {
                Util.NotifyDeployment();
            }


            else if (args[0] == "ScheduleBuild")
            {
                if (args.Length != 4)
                {
                    Console.WriteLine("Operation ('UpdateManifest / NotifyDeployment '),Root Manifest File path and Component Manifest File path needed as arguments");
                    return;
                }

                //xml reading for deployment flag
                string Deploymentflag = args[3];
                
                Deploymentflag = Deploymentflag.Replace("\"", "");

                string releaseManifestMasterNode = "Components";
                string releaseManifestChildNode = "Component";

                XmlDocument xmlflag = new XmlDocument();

                xmlflag.Load(Deploymentflag);

                XmlNode nodes = xmlflag.SelectSingleNode(releaseManifestMasterNode);
                XmlNodeList node = xmlflag.SelectNodes(releaseManifestMasterNode + "/" + releaseManifestChildNode);


                #region To update the Template file with the key
                if (node != null)
                {
                    foreach (XmlNode item in node)
                    {
                        string trackName = item.Attributes["Name"].Value;

                        XmlNodeList matchingBuildOutputPaths = nodes.SelectNodes(releaseManifestChildNode + "[@Name ='" + trackName + "']/@InstallStatus");
                        var matchingBuildOutputPathsList = new List<XmlNode>(matchingBuildOutputPaths.Cast<XmlNode>());

                        if (matchingBuildOutputPathsList.Count > 0)
                        {
                            foreach (var state in matchingBuildOutputPathsList)
                            {

                                if (state.Value=="Active")
                                  {
                                    Util.Component = trackName;
                                    ScheduleDeploy sDeploy = new ScheduleDeploy();
                                    string ComponentManifestFileName = args[2];
                                    ComponentManifestFileName = ComponentManifestFileName.Substring(ComponentManifestFileName.LastIndexOf(("\\")) + 1);
                                    sDeploy.UpdateRootManifestSchedule(args[1], args[2], ComponentManifestFileName, trackName);
                                }
                            }
                        }


                    }


                }

                #endregion


              
                //xml reading for deployment flag

                
               // Util.CompnentManifet = args[2];
                
                
               // string ComponentManifestFileName = Util.UpdateComponentManifest(args[2], Util.Component);

                

               
            }

            else if (args[0] == "TriggerDeploy")
            {
                Util.JenkinsURL = ConfigurationManager.AppSettings["JenkinsURL"];
                TriggerDeploy triggerDeploy = new TriggerDeploy(args[1],args[2]);                
            }

        }      

    }
}
