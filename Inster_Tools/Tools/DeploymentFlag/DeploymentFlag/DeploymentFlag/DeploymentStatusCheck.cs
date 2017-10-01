using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Globalization;
using System.Configuration;
using System.IO;


namespace DeploymentFlag
{
    class DeploymentStatusCheck
    {
        public void GetTemplatefile(string Deploymentflag)
        {
            Deploymentflag = Deploymentflag.Replace("\"", "");

             
            string releaseManifestMasterNode = "Components";
            string releaseManifestChildNode = "Component";

            string fileName = string.Empty; ;

           // fileName = Deploymentflag;
            XmlDocument xmlflag = new XmlDocument();
            //XmlDocument xmlReleaseManifest1 = new XmlDocument();

            File.SetAttributes(Deploymentflag, FileAttributes.Normal);
            xmlflag.Load(Deploymentflag);
           // XmlNodeList nodes = xmlflag.SelectNodes(releaseManifestMasterNode + "/" + releaseManifestChildNode);
            //XmlNodeList nodes = xmlflag.SelectSingleNode(releaseManifestMasterNode);

            XmlNode nodes = xmlflag.SelectSingleNode(releaseManifestMasterNode);
            XmlNodeList node = xmlflag.SelectNodes(releaseManifestMasterNode + "/" + releaseManifestChildNode);


            //if (status != "false")
            //{
            //    NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

            //}

            //xmlReleaseManifest1.Load(fileName);
            //XmlNode node = xmlReleaseManifest1.SelectSingleNode(releaseManifestMasterNode);

           
          
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
                            state.Value = "InActive";
                         
                        }
                    }
                   
                    
                }


            }

             #endregion


            xmlflag.Save(Deploymentflag);
        }
        
        
    }
}
