using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Configuration;
using System.IO;

namespace ReleaseManifests
{
    public partial class TabbedForm : Form
    {
        public static string _applicationName;
        public string ApplicationTemplatePath;
        public TabbedForm()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            DialogResult result = openReferenceFileDialog.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                listComponents.Items.Clear();
                txtReference.Text = openReferenceFileDialog.FileName;
            }
            Console.WriteLine(result);
        }
        private static string ApplicationName
        {
            get { return _applicationName; }
            set { _applicationName = value; }
        }

        private XDocument GetRegionTemplate(string applicationName)
        {
            return XDocument.Load(Path.Combine(ConfigurationManager.AppSettings["TemplateRootPath"],
                ConfigurationManager.AppSettings[applicationName + "_" + ddlRegion.SelectedItem.ToString()]));
        }

        private string GetVersionNumber(string currentVersion)
        {
            int li = currentVersion.LastIndexOf(".");
            return string.Concat(currentVersion.Substring(0, li + 1), int.Parse(currentVersion.Substring(li + 1)) + 1);
        }

        public string GetRegionTemplateName(string regionName, string applicationName)
        {
            return ConfigurationManager.AppSettings[applicationName + "_" + regionName].ToString();
        }

        public bool IsComponentModified(string[] partialVersion, string[] referenceVersion)
        {
            if (Convert.ToInt32(partialVersion[0]) > Convert.ToInt32(referenceVersion[0]) ||
                Convert.ToInt32(partialVersion[1]) > Convert.ToInt32(referenceVersion[1]) ||
                Convert.ToInt32(partialVersion[2]) > Convert.ToInt32(referenceVersion[2]) ||
                Convert.ToInt32(partialVersion[3]) > Convert.ToInt32(referenceVersion[3]))
                return true;

            return false;
        }

        private void TabbedForm_Load(object sender, EventArgs e)
        {
            ddlRegion.SelectedIndex = 0;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                string objComponents = string.Empty;
                XDocument regionTempPath = null;
                string partialManifestPaths = ConfigurationManager.AppSettings["AppStore_PartialManifestPaths"];
                List<DirectoryInfo> partialManifestDirectories = partialManifestPaths.Split(',').Select(c => new DirectoryInfo(c)).ToList();
                XDocument referenceDoc = XDocument.Load(txtReference.Text);
                
                var selectedButton = gbApplications.Controls.OfType<RadioButton>().FirstOrDefault(b => b.Checked == true);
                ApplicationName = selectedButton.Text;
                regionTempPath = GetRegionTemplate(ApplicationName);
                var currentVersion = referenceDoc.Root.Attribute("lastManifestVersion").Value;
                var newVersion = GetVersionNumber(currentVersion);
                var components = regionTempPath.Descendants("Product");
                List<string> selectedComponents = new List<string>();
                foreach (ListViewItem item in listComponents.CheckedItems)
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
                            var partialManifestFile = partialManifestDirectories.
                                                            SelectMany(m => m.GetDirectories(string.Format("*{0}*", pName), SearchOption.AllDirectories)).
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

                UpDateReferenceManifest(changedComponents, newVersion);
                //var tempComponents = new List<XElement>();
                //tempComponents.AddRange(regionTempPath.Descendants("Service").ToList());
                //tempComponents.AddRange(regionTempPath.Descendants("Website").ToList());
                //tempComponents.AddRange(regionTempPath.Descendants("WebSite").ToList());


                //var productNames = new List<string>();
                //var componentList = new List<XElement>();
                //var preRequisitesList = new List<XElement>();

                //var compVersionNumber = GetVersionNumber();
                //XDocument xmlDocument = new XDocument();

                //foreach (ListViewItem item in listView1.CheckedItems)
                //{
                //    var tempComponent = tempComponents.Where(t => t.Attribute("Name").Value == item.Text).FirstOrDefault();




                //    var subRootElement = new XElement("ReleaseManifest", new object[] { new XAttribute("type", "Component"), new XAttribute("version", compVersionNumber) });
                //    var subComponentDocument = new XDocument();
                //    subRootElement.Add(productElement);
                //    subComponentDocument.Add(subRootElement);
                //    subComponentDocument.Save(ConfigurationManager.AppSettings["ComponentManifest"].ToString() +
                //"ReleaseManifest_IGHS_" + ddlRegion.SelectedItem.ToString() + "_AppStore_" + item.Text + "_" + compVersionNumber + ".xml");
                //}
                //xmlDocument.Add(rootElement);

                lblMessage.Text = "Manifest Generated Successfully";
                lblMessage.ForeColor = Color.Green;
            }
            catch (Exception ex)
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = ex.Message;
            }
        }

        private void UpDateReferenceManifest(List<XElement> changedComponents, string newVersion)
        {
            XDocument referenceDoc = XDocument.Load(txtReference.Text);
            referenceDoc.Root.Attribute("lastManifestVersion").Value = newVersion;
            foreach (var cComp in changedComponents)
            {
               var component = referenceDoc.Descendants("Component").FirstOrDefault(c => c.Attribute("Name").Value.Equals(cComp.Attribute("Name").Value));
               if (component != null)
               {
                   component.Attribute("Version").Value = cComp.Attribute("Version").Value;
               }
               else
               {
                   throw new Exception(string.Format("Component {0} not found in reference doc", cComp.Attribute("Name").Value));
               }
            }
            referenceDoc.Save(txtReference.Text);
        }

        private void btnGenerateReference_Click(object sender, EventArgs e)
        {
            string referencePath;
            var result = new GetReferenceDialog().ShowDialog(out referencePath);
            if (result == DialogResult.OK)
            {
                var referenceDoc = XDocument.Load(referencePath);
                var distinctComps = referenceDoc.Descendants("Component").Select(s =>
                     new Component(s)
                     ).Distinct();
                var lastVersion = referenceDoc.Root.Attribute("ReleaseVersion").Value;
                XDocument xdoc = new XDocument(new XElement("ReferenceComponents", distinctComps.Select(s => s.ToXml()), new XAttribute("lastManifestVersion", lastVersion)));
                string outPath = ConfigurationManager.AppSettings["ReferencePath"].ToString();
                xdoc.Save(outPath);

                lblTabToolsResult.Text = string.Format("Reference File Created Successfully.Path:{0}", outPath);
                lblTabToolsResult.ForeColor = Color.Green;
            }
            else
            {
                lblTabToolsResult.Enabled = false;
            }
        }

        private void btnAnalyze_Click(object sender, EventArgs e)
        {
            listComponents.Items.Clear();
            lblMessage.Text = string.Empty;
            string objComponents = string.Empty;
            XDocument regionTempPath = null;

            string partialManifestPaths = ConfigurationManager.AppSettings["AppStore_PartialManifestPaths"];
            List<DirectoryInfo> partialManifestDirectories = partialManifestPaths.Split(',').Select(c => new DirectoryInfo(c)).ToList();
            XDocument referenceDoc = XDocument.Load(txtReference.Text);
            var referenceComponets = referenceDoc.Descendants("Component").Select(s => new Component(s)).Distinct();
            var selectedButton = gbApplications.Controls.OfType<RadioButton>().FirstOrDefault(b => b.Checked == true);
            ApplicationName = selectedButton.Text;

            regionTempPath = GetRegionTemplate(ApplicationName);
            var components = regionTempPath.Descendants("Product");

            foreach (var component in components)
            {
                var componentName = component.Attribute("Name").Value;
                var packages = component.Descendants("Package");
                bool isModified = false;
                foreach (var package in packages)
                {
                    var pName = package.Attribute("Name").Value;
                    var refComp = referenceComponets.FirstOrDefault(p => p.Name.Equals(pName));
                    if (refComp != null)
                    {
                        var refVersion = refComp.Version;
                        var partialManifestFile = partialManifestDirectories.
                                                            SelectMany(m => m.GetDirectories(string.Format("*{0}*", pName), SearchOption.AllDirectories)).
                                                            SelectMany(x => x.GetFiles("*.xml")).OrderByDescending(f => f.LastWriteTimeUtc).First(); ;
                        var latestVersion = XDocument.Load(partialManifestFile.FullName).Descendants("Package").
                            FirstOrDefault(p => p.Attribute("ComponentName").Value.Equals(pName)).Attribute("Version").Value;
                        if (!refVersion.Equals(latestVersion))
                        {
                            isModified = true; break;
                        }
                    }
                }
                var chkBox = new ListViewItem() { Text = componentName, BackColor = isModified ? Color.SkyBlue : Color.Transparent };

                listComponents.Items.Add(chkBox);
            }
        }


    }

    class Component
    {
        public Component(XElement element)
        {
            Name = element.Attribute("Name").Value;
            Version = element.Attribute("Version").Value;
        }
        public string Name { get; set; }
        public string Version { get; set; }

        public XElement ToXml()
        {
            return new XElement("Component", new XAttribute("Name", Name), new XAttribute("Version", Version));
        }

        public override bool Equals(object obj)
        {
            Component compareToObj = (Component)obj;
            return this.Name.Equals(compareToObj.Name) &&
                this.Version.Equals(compareToObj.Version);
        }
        public override int GetHashCode()
        {
            return this.Name.Length;
        }
    }

}
