using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Xml;

namespace UpdateRootManifest
{
    public class ScheduleDeploy
    {
        

        public void UpdateConfigForSchedule()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings[Util.Component].Value = (Convert.ToInt32(config.AppSettings.Settings[Util.Component].Value) + 1).ToString(); 
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        public  void UpdateRootManifestSchedule(string rootRMFile, string componentRMFile, string altertedFile, string DeployFalg)
        {

            string releaseManifestMasterNode = "ReleaseManifest";
           // string releaseManifestChildNode = "Applications/Product/Services/Service/Component";
            string releaseManifestChildNode = "Applications";
            string fileName = string.Empty; ;
            string releaseManifestChildNodeX = "Product/Services/Service/Component";

            XmlDocument xmlReleaseManifest = new XmlDocument();
            XmlDocument xmlReleaseManifest1 = new XmlDocument();

            xmlReleaseManifest.Load(componentRMFile);
            XmlNodeList nodes = xmlReleaseManifest.SelectNodes(releaseManifestMasterNode + "/" + releaseManifestChildNode);

            #region To update the Root file with Latest version

            if (nodes != null)
            {
                foreach (XmlNode item in nodes)
                {
                   // string trackName = item.Attributes["TrackName"].Value;
                   // string trackName = item.Attributes["Name"].Value;
                    string trackName = DeployFalg;
                    //string msiVersion = item.Attributes["Path"].Value;
                    



                    xmlReleaseManifest1.Load(rootRMFile);
                    XmlNode node = xmlReleaseManifest1.SelectSingleNode(releaseManifestMasterNode);

                    if (node != null)
                    {
                        //XmlNode removeNode = node.SelectSingleNode(releaseManifestChildNode + "[@Name ='Martini.IISWebsite.Setup']/@Path");
                        //XmlNodeList matchingBuildOutputPaths = node.SelectNodes(releaseManifestChildNode + "[@TrackName ='" + trackName + "']/@Path");

                        XmlNodeList matchingBuildOutputPaths = node.SelectNodes(releaseManifestChildNodeX + "[@Name ='" + trackName + "']/@State");
                        var matchingBuildOutputPathsList = new List<XmlNode>(matchingBuildOutputPaths.Cast<XmlNode>());

                        if (matchingBuildOutputPathsList.Count > 0)
                        {
                            foreach (var path in matchingBuildOutputPathsList)
                            {
                               // path.Value = msiVersion;
                               // string pathToChange = path.Value;
                            }
                        }
                        else
                        {
                            // else put in to the unknow (default section) TODO
                            Console.WriteLine("ReleaseManifestGen: The MSI component name could not be found.\n");

                        }

                      //  matchingBuildOutputPaths = node.SelectNodes(releaseManifestChildNodeX + "[@Name ='" + trackName + "']/@Version");
                     //   matchingBuildOutputPathsList = new List<XmlNode>(matchingBuildOutputPaths.Cast<XmlNode>());

                     //   if (matchingBuildOutputPathsList.Count > 0)
                     //  {
                    //       foreach (var version in matchingBuildOutputPathsList)
                     //     {
                   //          string componentName = componentRMFile.Substring(componentRMFile.LastIndexOf(("\\")) + 1);
                    //         version.Value = componentName;
                     //      }
                   //  }
                  //   else
                  //    {
                            // else put in to the unknow (default section) TODO
                  //    Console.WriteLine("ReleaseManifestGen: The MSI component name could not be found.\n");

                    //  }

                        matchingBuildOutputPaths = node.SelectNodes(releaseManifestChildNodeX + "[@Name ='" + trackName + "']/@ComponentManifest");
                        matchingBuildOutputPathsList = new List<XmlNode>(matchingBuildOutputPaths.Cast<XmlNode>());

                        if (matchingBuildOutputPathsList.Count > 0)
                       {
                            foreach (var ComponentManifest in matchingBuildOutputPathsList)
                           {
                                ComponentManifest.Value = altertedFile;
                           }
                        }
                       else
                       {
                            // else put in to the unknow (default section) TODO
                           Console.WriteLine("ReleaseManifestGen: The MSI component name could not be found.\n");

                       }

                        matchingBuildOutputPaths = node.SelectNodes(releaseManifestChildNodeX + "[@Name ='" + trackName + "']/@State");
                             matchingBuildOutputPathsList = new List<XmlNode>(matchingBuildOutputPaths.Cast<XmlNode>());

                        if (matchingBuildOutputPathsList.Count > 0)
                        {
                            foreach (var state in matchingBuildOutputPathsList)
                           {
                                state.Value = "ToBeInstalled";
                           }
                        }
                        else
                       {
                            // else put in to the unknow (default section) TODO
                           Console.WriteLine("ReleaseManifestGen: The MSI component name could not be found.\n");

                       }

                        xmlReleaseManifest1.Save(rootRMFile);
                        Console.WriteLine("Custom Task - Updated Release Manifest");
                    }

                }
            }
            #endregion



            //string body = WriteConsole(fileName);
            //SendEmail(fileName, body, "before");
            this.UpdateConfigForSchedule();
        }
    }
}
