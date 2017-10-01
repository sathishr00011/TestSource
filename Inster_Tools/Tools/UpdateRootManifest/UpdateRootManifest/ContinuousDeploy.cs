using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Globalization;
using System.Configuration;
using System.IO;

namespace UpdateRootManifest
{
    public class ContinuousDeploy
    {

        public void UpdateRootManifest(string rootRMFile, string componentRMFile, string altertedFile)
        {

            string releaseManifestMasterNode = "ReleaseManifest";
            string releaseManifestChildNode = "Applications/Product/Services/Service/Component";
            string fileName = string.Empty; ;


            XmlDocument xmlReleaseManifest = new XmlDocument();
            XmlDocument xmlReleaseManifest1 = new XmlDocument();

            xmlReleaseManifest.Load(componentRMFile);
            XmlNodeList nodes = xmlReleaseManifest.SelectNodes(releaseManifestMasterNode + "/" + releaseManifestChildNode);

            #region To update the Root file with Latest version

            if (nodes != null)
            {
                foreach (XmlNode item in nodes)
                {
                    string trackName = item.Attributes["TrackName"].Value;
                    string msiVersion = item.Attributes["Path"].Value;



                    xmlReleaseManifest1.Load(rootRMFile);
                    XmlNode node = xmlReleaseManifest1.SelectSingleNode(releaseManifestMasterNode);

                    if (node != null)
                    {
                        //XmlNode removeNode = node.SelectSingleNode(releaseManifestChildNode + "[@Name ='Martini.IISWebsite.Setup']/@Path");
                        XmlNodeList matchingBuildOutputPaths = node.SelectNodes(releaseManifestChildNode + "[@TrackName ='" + trackName + "']/@Path");
                        var matchingBuildOutputPathsList = new List<XmlNode>(matchingBuildOutputPaths.Cast<XmlNode>());

                        if (matchingBuildOutputPathsList.Count > 0)
                        {
                            foreach (var path in matchingBuildOutputPathsList)
                            {
                                path.Value = msiVersion;
                                string pathToChange = path.Value;
                            }
                        }
                        else
                        {
                            // else put in to the unknow (default section) TODO
                            Console.WriteLine("ReleaseManifestGen: The MSI component name could not be found.\n");

                        }

                        matchingBuildOutputPaths = node.SelectNodes(releaseManifestChildNode + "[@TrackName ='" + trackName + "']/@Version");
                        matchingBuildOutputPathsList = new List<XmlNode>(matchingBuildOutputPaths.Cast<XmlNode>());

                        if (matchingBuildOutputPathsList.Count > 0)
                        {
                            foreach (var version in matchingBuildOutputPathsList)
                            {
                                version.Value = msiVersion.Substring(msiVersion.LastIndexOf("\\") + 1);
                            }
                        }
                        else
                        {
                            // else put in to the unknow (default section) TODO
                            Console.WriteLine("ReleaseManifestGen: The MSI component name could not be found.\n");

                        }

                        matchingBuildOutputPaths = node.SelectNodes(releaseManifestChildNode + "[@TrackName ='" + trackName + "']/@ComponentManifest");
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

                        xmlReleaseManifest1.Save(rootRMFile);
                        Console.WriteLine("Custom Task - Updated Release Manifest");
                    }

                }

                string datetimeNow = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss",CultureInfo.InvariantCulture);
                fileName = rootRMFile.Substring(0, rootRMFile.LastIndexOf('.'));
                fileName += "_" + datetimeNow + ".xml";
                File.Copy(rootRMFile, fileName);


            }
            #endregion

            #region To update the Genrated file
            if (nodes != null)
            {
                foreach (XmlNode item in nodes)
                {
                    string trackName = item.Attributes["TrackName"].Value;

                    xmlReleaseManifest1.Load(fileName);
                    XmlNode node = xmlReleaseManifest1.SelectSingleNode(releaseManifestMasterNode);

                    if (node != null)
                    {
                        //XmlNode removeNode = node.SelectSingleNode(releaseManifestChildNode + "[@Name ='Martini.IISWebsite.Setup']/@Path");
                        XmlNodeList matchingBuildOutputPaths = node.SelectNodes(releaseManifestChildNode + "[@TrackName ='" + trackName + "']/@State");
                        var matchingBuildOutputPathsList = new List<XmlNode>(matchingBuildOutputPaths.Cast<XmlNode>());

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


                        xmlReleaseManifest1.Save(fileName);
                        Console.WriteLine("Custom Task - Updated Release Manifest");
                    }

                }



            }
            #endregion

            string body = Util.WriteConsole(fileName);
            Util.SendEmail(fileName, body, "before");
            UpdateConfig(fileName);
        }



        public void UpdateConfig(string filename)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            //config.AppSettings.Settings["ManifestFile"].Value = filename;
            config.AppSettings.Settings[Util.Component].Value = (Convert.ToInt32(config.AppSettings.Settings[Util.Component].Value) + 1).ToString(); ;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

    }
}
