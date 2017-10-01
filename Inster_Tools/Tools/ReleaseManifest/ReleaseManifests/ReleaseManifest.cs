using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;
using System.Xml.Linq;
using System.Windows.Forms;
using System.Reflection;

namespace ReleaseManifests
{
    class ReleaseManifest
    {
        public static string _applicationName;

        private static string ApplicationName
        {
            get { return _applicationName; }
            set { _applicationName = value; }
        }

        public string GenerateManifest(string applicationName, string regionName, string referenceFilePath, ListView componentList)
        {
            try
            {
                string objComponents = string.Empty;
                XDocument regionTempPath = null;
                var partialManifestdirectory = new DirectoryInfo(ConfigurationManager.AppSettings["AppStore_PartialManifestPath"].ToString());
                XDocument referenceDoc = XDocument.Load(referenceFilePath);
                var referenceComponets = referenceDoc.Descendants("Component").Select(s => new Component(s));                
                ApplicationName = applicationName;
                regionTempPath = GetRegionTemplate(ApplicationName, regionName);
                var currentVersion = referenceDoc.Root.Attribute("lastManifestVersion").Value;
                var newVersion = GetVersionNumber(currentVersion);
                var components = regionTempPath.Descendants("Product");
                List<string> selectedComponents = new List<string>();
                foreach (ListViewItem item in componentList.CheckedItems)
                {
                    selectedComponents.Add(item.Text);
                }
                XDocument xmlDocument = new XDocument();
                var rootElement = new XElement("ReleaseManifest", new object[] { new XAttribute("type", "Component"), new XAttribute("version", newVersion) });

                List<XElement> finalComponentElements = new List<XElement>();
                List<XElement> changedComponents = new List<XElement>();
                foreach (var component in components)
                {
                    var componentName = component.Attribute("Name").Value;
                    if (selectedComponents.Contains(componentName))
                    {
                        var productElement = new XElement("Product", new XAttribute("Name", componentName));
                        var packages = component.Descendants("Package");
                        foreach (var package in packages)
                        {
                            var pName = package.Attribute("Name").Value;
                            var partialManifestFile = partialManifestdirectory.GetDirectories(string.Format("*{0}*", pName), SearchOption.AllDirectories).
                                                            SelectMany(x => x.GetFiles("*.xml")).OrderByDescending(f => f.LastWriteTimeUtc).First();
                            var partialManifest = XDocument.Load(partialManifestFile.FullName).Descendants("Package").ToList();

                            var partialItem = partialManifest.FirstOrDefault(p => p.Attributes("ComponentName").First().Value == pName);
                            if (partialItem != null)
                            {
                                var cComp = new XElement("Package", new object[]{ new XAttribute("Name", partialItem.Attribute("ComponentName").Value)
                                            ,new XAttribute("Version",  partialItem.Attribute("Version").Value), 
                                            new XAttribute("Path",  partialItem.Attribute("Path").Value)});
                                changedComponents.Add(cComp);
                                productElement.Add(cComp);
                            }
                        }
                        finalComponentElements.Add(productElement);
                        rootElement.Add(productElement);
                    }
                }
                xmlDocument.Add(rootElement);
                string manifestOutputPath = ConfigurationManager.AppSettings["FinalReleaseManifest"];
                string finalManifestname = "ComponentManifest_IGHS_" + ApplicationName + "_AppStore_" + newVersion + ".xml";
                var finalFilePath = Path.Combine(manifestOutputPath, finalManifestname);
                if (File.Exists(finalFilePath))
                    File.Delete(finalFilePath);
                xmlDocument.Save(finalFilePath);

                UpDateReferenceManifest(changedComponents, newVersion, referenceFilePath);
                return "Success";
            }
            catch (Exception ex) { return ex.Message; }
        }

        private void UpDateReferenceManifest(List<XElement> changedComponents, string newVersion, string referenceFilePath)
        {
            XDocument referenceDoc = XDocument.Load(referenceFilePath);
            referenceDoc.Root.Attribute("lastManifestVersion").Value = newVersion;
            foreach (var cComp in changedComponents)
            {
                referenceDoc.Descendants("Component").FirstOrDefault(c => c.Attribute("Name").Value.Equals(cComp.Attribute("Name").Value))
                    .Attribute("Version").Value = cComp.Attribute("Version").Value;
            }
            referenceDoc.Save(referenceFilePath);
        }

        //private XDocument GetRegionTemplate(string applicationName, string regionName)
        //{
        //    return XDocument.Load(Path.Combine(ConfigurationManager.AppSettings["TemplateRootPath"],
        //        ConfigurationManager.AppSettings[applicationName + "_" + regionName]));
        //}

        private string GetVersionNumber(string currentVersion)
        {
            int li = currentVersion.LastIndexOf(".");
            return string.Concat(currentVersion.Substring(0, li + 1), int.Parse(currentVersion.Substring(li + 1)) + 1);
        }

        #region Component and Master Manifest common methods
        public static XDocument GetRegionTemplate(string applicationName, string regionName)
        {
            var templatePath = Path.GetTempPath();//Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\Templates"
            var path = Path.Combine(templatePath, ConfigurationManager.AppSettings[applicationName + "_" + regionName]);
            TfsCheckoutCheckin.GetTemplateFromTfs(ConfigurationManager.AppSettings[applicationName + "_" + regionName]);
            return XDocument.Load(path); //For Tibco it will be different
        }

        //public static string SaveLatestVersion(string keyName)
        //{
        //    //TfsCheckoutCheckin.CheckOutFromTFS("VersionConfig.xml");
        //    XDocument xmlConfig = null;
        //    var newVersion = string.Empty;
        //    if (!File.Exists("VersionConfig.xml"))
        //    {
        //        xmlConfig = new XDocument();
        //        var appSettings = new XElement("appSettings");
        //        appSettings.Add(new XElement("add", new object[] { new XAttribute("key", "Grocery_VersionNumber"), new XAttribute("value", ConfigurationManager.AppSettings["Grocery_VersionNumber"].ToString()) }));
        //        appSettings.Add(new XElement("add", new object[] { new XAttribute("key", "AppStore_VersionNumber"), new XAttribute("value", ConfigurationManager.AppSettings["AppStore_VersionNumber"].ToString()) }));
        //        appSettings.Add(new XElement("add", new object[] { new XAttribute("key", "OMS_VersionNumber"), new XAttribute("value", ConfigurationManager.AppSettings["OMS_VersionNumber"].ToString()) }));
        //        appSettings.Add(new XElement("add", new object[] { new XAttribute("key", "TIBCO_VersionNumber"), new XAttribute("value", ConfigurationManager.AppSettings["TIBCO_VersionNumber"].ToString()) }));
        //        xmlConfig.Add(appSettings);
        //        xmlConfig.Save("VersionConfig.xml");
        //    }

        //    xmlConfig = XDocument.Load("VersionConfig.xml");
        //    var elementNodes = xmlConfig.Descendants("appSettings").FirstOrDefault().Descendants("add");

        //    foreach (XElement element in elementNodes)
        //    {
        //        if (element.Attribute("key").Value == keyName)
        //        {
        //            newVersion = IncreaseVersionNumber(element.Attribute("value").Value);
        //            element.Attribute("value").Value = newVersion;
        //            break;
        //        }
        //    }

        //    xmlConfig.Save("VersionConfig.xml");
        //    return newVersion;
        //}

        public static string IncreaseVersionNumber(string currentVersion)
        {
            int li = currentVersion.LastIndexOf(".");
            return string.Concat(currentVersion.Substring(0, li + 1), int.Parse(currentVersion.Substring(li + 1)) + 1);
        }

        public static string ReduceVersionNumber(string currentVersion)
        {
            int li = currentVersion.LastIndexOf(".");
            return string.Concat(currentVersion.Substring(0, li + 1), int.Parse(currentVersion.Substring(li + 1)) + 1);
        }

        public static void RevertSavedVersionNumber(string keyName)
        {
            XDocument xmlConfig = XDocument.Load("VersionConfig.xml");
            var elementNodes = xmlConfig.Descendants("appSettings").FirstOrDefault().Descendants("add");

            foreach (XElement element in elementNodes)
            {
                if (element.Attribute("key").Value == keyName)
                {
                    var oldVersion = ReduceVersionNumber(element.Attribute("value").Value);
                    element.Attribute("value").Value = oldVersion;
                    break;
                }
            }

            xmlConfig.Save("VersionConfig.xml");
        }
        #endregion
    }
}
