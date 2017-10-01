using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Xml;
using System.Collections.Specialized;
using System.IO;


namespace PackageConfigchecin
{
    class PackageUtility
    {

        public void UpdateTemplatefile (string ManifestTemplatefile, string Packageconfigfile)
        {
            string PackageconfigMasterNode = "packages";
            // string releaseManifestChildNode = "Applications/Product/Services/Service/Component";
            string PackageconfigChildNode = "package";
            string fileName = string.Empty; ;
            //string releaseManifestChildNodeX = "Product/Services/Service/Component";

            XmlDocument xmlPackageconfig = new XmlDocument();
            XmlDocument xmlManifestTemaplateconfig = new XmlDocument();

            xmlPackageconfig.Load(Packageconfigfile);
            XmlNodeList nodes = xmlPackageconfig.SelectNodes(PackageconfigMasterNode + "/" + PackageconfigChildNode);
           

            File.SetAttributes(ManifestTemplatefile, FileAttributes.Normal);
            #region To update the Template file with Latest version
            if (nodes != null)
            {
                foreach (XmlNode item in nodes)
                {

                    string trackName = item.Attributes["id"].Value;

                    string ManifestTemplateMasterNode = "ReleaseManifest";
                    string ManifestTemplateChildNode = "Product/Package";

                    string[] strArray = ManifestTemplatefile.Split('_');

                    foreach (string obj in strArray)
                    {
                        if (obj == "AppStore")
                        {
                            ManifestTemplateChildNode = "Product/Service/Package";
                        }

                    }

                    xmlManifestTemaplateconfig.Load(ManifestTemplatefile);
                    XmlNode node = xmlManifestTemaplateconfig.SelectSingleNode(ManifestTemplateMasterNode);

                    NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
                    Dictionary<string, string> buildlist = new Dictionary<string, string>();

                    for (int i = 0; i < appSettings.Count; i++)
                    {
                        buildlist.Add(appSettings.GetKey(i), appSettings.Get(i));
                    

                    //if (trackName == "AppStore.Common.Library")
                    if (buildlist.ContainsValue(trackName))
                    {
                        string msiVersion = item.Attributes["version"].Value;
                        

                        if (node != null)
                        {
                            XmlNodeList matchingversion = node.SelectNodes(ManifestTemplateChildNode + "[@Name ='" + trackName + "']/@Version");
                            var matchingversionList = new List<XmlNode>(matchingversion.Cast<XmlNode>());

                            if (matchingversionList.Count > 0)
                            {
                                foreach (var path in matchingversionList)
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


                            xmlManifestTemaplateconfig.Save(ManifestTemplatefile);

                            Console.WriteLine("Custom Task - Updated Release Manifest");

                        }
                      
                    
                    }
                
                }

                    //if (trackName == "Tesco.Com.Core")
                    //{
                    //    string msiVersion = item.Attributes["version"].Value;


                    //    if (node != null)
                    //    {
                    //        XmlNodeList matchingversion = node.SelectNodes(ManifestTemplateChildNode + "[@Name ='" + trackName + "']/@Version");
                    //        var matchingversionList = new List<XmlNode>(matchingversion.Cast<XmlNode>());

                    //        if (matchingversionList.Count > 0)
                    //        {
                    //            foreach (var path in matchingversionList)
                    //            {
                    //                path.Value = msiVersion;
                    //                string pathToChange = path.Value;
                    //            }
                    //        }
                    //        else
                    //        {
                    //            // else put in to the unknow (default section) TODO
                    //            Console.WriteLine("ReleaseManifestGen: The MSI component name could not be found.\n");

                    //        }


                    //        xmlManifestTemaplateconfig.Save(ManifestTemplatefile);
                    //        Console.WriteLine("Custom Task - Updated Release Manifest");

                    //    }


                    //}


                    
                }
            
            }
            #endregion

        }
    }
}
