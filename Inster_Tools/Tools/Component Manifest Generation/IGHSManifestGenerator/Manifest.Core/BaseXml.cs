using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Collections.Specialized;
using System.Configuration;


namespace Manifest.Core
{
    public abstract class BaseXml
    {
        protected XDocument appSettingsDoc;
        protected string[] manifestTemplates;
        protected string[] regions;
        protected string version;
        protected string templateCategory;
        protected string outputManifestPath;
        protected string currentAssemblyDirectoryName;
        protected List<XDocument> inmemXDocs;
        protected string manifestTName;
        protected string manifestTNameNew;
        protected string tag;
        protected bool isNewManifestModified;
        protected string searchDirectoryPath;
        private const string GROCERY = "IGHS_Grocery";
        private const string APPSTORE = "IGHS_AppStore";
        private const string IGHS = "IGHS";
        private const string BIReporting = "IGHS_BIReporting";


        private string ComName;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseXml"/> class.
        /// </summary>
        /// <param name="templateCategory">The template category.</param>
        /// <param name="regions">The regions.</param>
        /// <param name="version">The version.</param>
        /// <param name="outputManifestPath">The output manifest path.</param>
        /// <param name="tag">The tag.</param>
        public BaseXml(string templateCategory, string regions, string version, string outputManifestPath, string tag, string searchDirectoryPath)
        {
            try
            {
                this.regions = !string.IsNullOrWhiteSpace(regions) ? regions.Split(',') : null;
                this.version = version;
                this.outputManifestPath = outputManifestPath;
                this.templateCategory = templateCategory;
                this.tag = tag;
                this.isNewManifestModified = false;
                this.searchDirectoryPath = searchDirectoryPath;
                currentAssemblyDirectoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                appSettingsDoc = XDocument.Load(currentAssemblyDirectoryName + @"\App.config");
                XElement templates = appSettingsDoc.Element("configuration").Element("Templates").Elements("Template").First(x => x.Attribute("name").Value.Equals(templateCategory, StringComparison.InvariantCultureIgnoreCase));
                manifestTName = appSettingsDoc.Element("configuration").Element("Manifest").Attribute("templatename").Value;

                if (templateCategory == "Instore")
                {
                    manifestTNameNew = "ReleaseManifest";
                    manifestTName = manifestTName.Replace(manifestTName, manifestTNameNew);
                }

                if (this.regions == null || this.regions.Length == 0)
                {
                    manifestTemplates = templates.Elements("artifacts")
                        .Where(x => !string.IsNullOrWhiteSpace(x.Attribute("filename").Value))
                        .Select(x => x.Attribute("filename").Value).ToArray<string>();
                }
                else
                {
                    manifestTemplates = templates.Elements("artifacts")
                        .Where(x => !string.IsNullOrWhiteSpace(x.Attribute("region").Value) && this.regions.Contains(x.Attribute("region").Value, StringComparer.InvariantCultureIgnoreCase) && !string.IsNullOrWhiteSpace(x.Attribute("filename").Value))
                        .Select(x => x.Attribute("filename").Value).ToArray<string>();
                }

                LoadTemplates();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Loads the templates.
        /// </summary>
        protected virtual void LoadTemplates()
        {
            inmemXDocs = new List<XDocument>();
            var templatePath = @"\Templates\";
            var templatePathWithRelease = @"\Templates\" + this.tag + @"\";
            foreach (string template in this.manifestTemplates)
            {
                XDocument apptemplate;
                if (File.Exists(currentAssemblyDirectoryName + templatePathWithRelease + template))
                    apptemplate = XDocument.Load(currentAssemblyDirectoryName + templatePathWithRelease + template);
                else
                    apptemplate = XDocument.Load(currentAssemblyDirectoryName + templatePath + template);
                inmemXDocs.Add(apptemplate);
            }
        }

        /// <summary>
        /// Processes the manifest.
        /// </summary>
        protected abstract void ProcessManifest();

        /// <summary>
        /// Ins the memory new manifest processing.
        /// </summary>
        /// <param name="srcComponentName">Name of the SRC component.</param>
        /// <param name="destComponentName">Name of the dest component.</param>
        /// <param name="newManifestSkeleton">The new manifest skeleton.</param>
        /// <param name="partialManifest">The partial manifest.</param>
        protected abstract void InMemoryNewManifestProcessing(string srcComponentName, string destComponentName, XDocument newManifestSkeleton, XDocument partialManifest);

        /// <summary>
        /// Creates the new manifest skeleton from template.
        /// </summary>
        /// <param name="template">The template.</param>
        /// <returns></returns>
        protected virtual XDocument CreateNewManifestSkeletonFromTemplate(XDocument template)
        {
            TextReader tr = new StringReader(template.ToString());
            XDocument newManifest = XDocument.Load(tr);

            return newManifest;
        }

        /// <summary>
        /// Loads the partial manifest from UNC path.
        /// </summary>
        /// <param name="searchPattern">The search pattern.</param>
        /// <returns></returns>
        protected virtual XDocument LoadPartialManifestFromUNCPath(string searchPattern)
        {
            XDocument partialManifestDoc = null;
            try
            {
                StringBuilder partialManifestPath = new StringBuilder();
                partialManifestPath.Append(this.outputManifestPath);
                //partialManifestPath.Append(this.tag);



                var S = searchDirectoryPath.Split(',');

                if (S.Length == 3)
                {
                    //searchDirectoryPath = S[0].ToString();
                    //searchDirectoryPath = S[1].ToString();

                    foreach (var Serachnewpath in S)
                    {
                        string pathToSearch = string.IsNullOrWhiteSpace(Serachnewpath) ? outputManifestPath : Serachnewpath;

                        if (pathToSearch.Contains("SharedLibraries"))
                        {
                            DirectoryInfo di = new DirectoryInfo(pathToSearch);
                            FileInfo fiLatest;
                            // string searchversion = searchPattern.Replace("Library_*", "Library_SharedLibraries_*");

                            FileInfo[] matchingFiles = di.GetFiles(searchPattern, SearchOption.AllDirectories);
                            if (matchingFiles != null && matchingFiles.Any())
                            {
                                //fiLatest = matchingFiles.OrderByDescending(fi => fi.CreationTime).First(f => f.FullName.Contains(this.tag));
                                //string file = Path.GetFileName(matchingFiles.FirstOrDefault().FullName);

                                partialManifestDoc = XDocument.Load(matchingFiles.FirstOrDefault().FullName);
                            }
                        }
                        else
                        {
                            var X = searchPattern.Split('_');
                            List<string> test = new List<string>();
                            if (X.Length == 4)
                            {
                                //test.Add("*_Tesco.Com.Core_*" + "_" + X[3].ToString());
                                //test.Add("*_AppStore.Common.Library_*" + "_" + X[3].ToString());
                                //test.Add("*_Tesco.Com.Web.Mvc_*" + "_" + X[3].ToString());
                                //test.Add("*_Tesco.Com.Web.Common_*" + "_" + X[3].ToString());
                                //test.Add("*_AppStore.Content.Contracts_*" + "_" + X[3].ToString());
                                //test.Add("*_AppStore.Content.Library_*" + "_" + X[3].ToString());
                                //test.Add("*_AppStore.Notification.Contracts_*" + "_" + X[3].ToString());
                                //test.Add("*_AppStore.Notification.WcfClient_*" + "_" + X[3].ToString());
                                //test.Add("*_AppStore.DeviceIdentification.Contracts_*" + "_" + X[3].ToString());
                                //test.Add("*_AppStore.DeviceIdentification.WcfClient_*" + "_" + X[3].ToString());
                                //test.Add("*_Tesco.Com.Web.YUI_*" + "_" + X[3].ToString());

                                appSettingsDoc = XDocument.Load(currentAssemblyDirectoryName + @"\App.config");
                                var appSettings = appSettingsDoc.Element("configuration").Element("appSettings").Elements("add").Attributes("value");

                                foreach (var item in appSettings)
                                {
                                    test.Add("*_" + item.Value + "_*" + "_" + X[3].ToString());
                                }
                               

                            }

                            if (!test.Contains(searchPattern))
                            {
                                //    test.Add(X[0].ToString() + "_" + X[1].ToString() + "_" + X[2].ToString() + "_" + X[3].ToString());
                                //}

                                //if (!test.Contains(searchPattern))
                                //{
                                DirectoryInfo di = new DirectoryInfo(pathToSearch);
                                FileInfo fiLatest;
                                FileInfo[] matchingFiles = di.GetFiles(searchPattern, SearchOption.AllDirectories);

                                if (matchingFiles != null && matchingFiles.Any())
                                {
                                    //if (!matchingFiles.Any(f => f.FullName.Contains(this.tag)))
                                    //{
                                    //    fiLatest = matchingFiles.OrderByDescending(fi => fi.CreationTime).First();
                                    //}
                                    //else
                                    //{
                                    //    fiLatest = matchingFiles.OrderByDescending(fi => fi.CreationTime).First(f => f.FullName.Contains(this.tag));
                                    //}
                                    fiLatest = matchingFiles.OrderByDescending(fi => fi.CreationTime).First(f => f.FullName.Contains(this.tag));
                                    string file = Path.GetFileName(fiLatest.FullName);

                                    partialManifestDoc = XDocument.Load(fiLatest.FullName);
                                }
                            }
                        }
                    }
                }
                else
                {
                   
                        string pathToSearch = string.IsNullOrWhiteSpace(searchDirectoryPath) ? outputManifestPath : searchDirectoryPath;
                        
                        DirectoryInfo di = new DirectoryInfo(pathToSearch);

                        FileInfo fiLatest;
                        FileInfo[] matchingFiles = di.GetFiles(searchPattern, SearchOption.AllDirectories);
                        if (matchingFiles != null && matchingFiles.Any())
                        {
                            fiLatest = matchingFiles.OrderByDescending(fi => fi.CreationTime).First(f => f.FullName.Contains(this.tag));
                            string file = Path.GetFileName(fiLatest.FullName);

                            partialManifestDoc = XDocument.Load(fiLatest.FullName);
                        }
                    
                }
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("Error loading the partial manifest for component -> {0}", searchPattern), e);
            }

            return partialManifestDoc;
        }

        /// <summary>
        /// Saves the new manifest.
        /// </summary>
        /// <param name="templateUsed">The template used.</param>
        /// <param name="newManifestModifiedSkeleton">The new manifest modified skeleton.</param>
        protected void SaveNewManifest(XDocument templateUsed, XDocument newManifestModifiedSkeleton)
        {
            string region = templateUsed.Element("ReleaseManifest").Attribute("Region").Value;

            //Add release version to new manifest
            IEnumerable<XElement> matchedComponents = newManifestModifiedSkeleton.XPathSelectElements(@"//ReleaseManifest[@Region='" + region + "']");
            if (matchedComponents.Any())
            {
                matchedComponents.ToList().ForEach(x =>
                {
                    x.SetAttributeValue("ReleaseVersion", version);
                });
            }

            if (templateCategory == "MyAccount" || templateCategory == "Home" || templateCategory == "BasketBuilding" || templateCategory == "FindProducts"
                || templateCategory == "CustomerProfile" || templateCategory == "Delivery" || templateCategory == "OrderCheckout" || templateCategory == "GroceryHost" || templateCategory == "Home")
            {
                ComName = GROCERY;
            }

            if (templateCategory == "WindowsService" || templateCategory == "ConsoleApp" || templateCategory == "ReportingWebSite" || templateCategory == "ReportingManagementService")
            {
                ComName = BIReporting;
            }


            if (templateCategory == "MobileDispatcher")
            {
                ComName = IGHS;
            }

            if (templateCategory == "ProductIntegration")
            {
                ComName = IGHS;
            }

            if (templateCategory == "Appstore")
            {
                ComName = APPSTORE;
            }

            if (templateCategory == "CouponService" || templateCategory == "NotificationService" || templateCategory == "LocationService" || templateCategory == "CustomerService" || templateCategory == "PaymentService" || templateCategory == "OrderService" || templateCategory == "DeliveryService" || templateCategory == "ShoppingCartService" || templateCategory == "AuthenticationService" || templateCategory == "FavouriteService" || templateCategory == "LoyaltyService" || templateCategory == "DeviceIdentificationService" || templateCategory == "OrderBusinessService" || templateCategory == "StoreHouseService" || templateCategory == "ContentService" || templateCategory =="EntprseAuthenticationService")
            {
                ComName = APPSTORE;
            }

            if (templateCategory == "Instore")
            {
                ComName = IGHS;
            }

            if (templateCategory == "Login")
            {
                ComName = IGHS;
            }

            if (templateCategory == "LoginUIAssets")
            {
                ComName = IGHS;
            }
            if (templateCategory == "BackOffice")
            {
                ComName = IGHS;
            }

            if (templateCategory == "UIAssets")
            {
                ComName = IGHS;
            }

            if (templateCategory == "Login")
            {
                string manifestName = string.Format("{0}_{1}_{2}_{3}_{4}.xml", this.manifestTName, ComName, this.templateCategory, region, this.version);

                StringBuilder fullManifestName = new StringBuilder();
                fullManifestName.Append(this.outputManifestPath);
                fullManifestName.Append(@"\");
                fullManifestName.Append(manifestName);
                newManifestModifiedSkeleton.Save(fullManifestName.ToString());
            }
            else if (templateCategory == "LoginUIAssets")
            {
                string manifestName = string.Format("{0}_{1}_{2}_{3}_{4}.xml", this.manifestTName, ComName, this.templateCategory, region, this.version);

                StringBuilder fullManifestName = new StringBuilder();
                fullManifestName.Append(this.outputManifestPath);
                fullManifestName.Append(@"\");
                fullManifestName.Append(manifestName);
                newManifestModifiedSkeleton.Save(fullManifestName.ToString());
            }
            else if (templateCategory == "BackOffice")
            {
                string manifestName = string.Format("{0}_{1}_{2}_{3}_{4}.xml", this.manifestTName, ComName, this.templateCategory, region, this.version);

                StringBuilder fullManifestName = new StringBuilder();
                fullManifestName.Append(this.outputManifestPath);
                fullManifestName.Append(@"\");
                fullManifestName.Append(manifestName);
                newManifestModifiedSkeleton.Save(fullManifestName.ToString());
            }

            else if (templateCategory == "UIAssets")
            {
                string manifestName = string.Format("{0}_{1}_{2}_{3}_{4}.xml", this.manifestTName, ComName, this.templateCategory, region, this.version);

                StringBuilder fullManifestName = new StringBuilder();
                fullManifestName.Append(this.outputManifestPath);
                fullManifestName.Append(@"\");
                fullManifestName.Append(manifestName);
                newManifestModifiedSkeleton.Save(fullManifestName.ToString());
            }

            else
            {
                if (ComName == APPSTORE)
                {

                    this.templateCategory= this.templateCategory.Substring(0,this.templateCategory.Length - 7);
                    //this.templateCategory = this.templateCategory.Substring(1,6);
                  
                    
                }
             string manifestName = string.Format("{0}_{1}_{2}_{3}_{4}.xml", this.manifestTName, ComName, region, this.templateCategory, this.version);

                StringBuilder fullManifestName = new StringBuilder();
                fullManifestName.Append(this.outputManifestPath);
                fullManifestName.Append(@"\");
                fullManifestName.Append(manifestName);
                newManifestModifiedSkeleton.Save(fullManifestName.ToString());
            }
        }
    }
}
