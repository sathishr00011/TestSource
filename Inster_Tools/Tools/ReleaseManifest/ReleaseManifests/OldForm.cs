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
    public partial class OldForm : Form
    {
        public OldForm()
        {
            InitializeComponent();
        }

        private void OldForm_Load(object sender, EventArgs e)
        {
            ddlRegion.SelectedIndex = 0;
        }

        //Upload Reference File and identify modified component
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = openReferenceFileDialog.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                listView1.Items.Clear();
                lblError.Text = string.Empty;

                txtReference.Text = openReferenceFileDialog.FileName;

                if (txtReference.Text.Contains("AppStore"))
                {
                    var objComponents = System.Configuration.ConfigurationSettings.AppSettings["Grocery"].ToString();

                    foreach (var item in objComponents.Split(','))
                    {
                        listView1.Items.Add(new ListViewItem((new CheckBox()).Text = item));
                    }

                    var referenceManifest = XDocument.Load(openReferenceFileDialog.FileName).Descendants("Component").ToList();
                    var regionTempPath = XDocument.Load(ConfigurationSettings.AppSettings["TemplateRootPath"].ToString() + GetRegionTemplateName(ddlRegion.SelectedItem.ToString(), "AppStore"));

                    var tempComponents = new List<XElement>();
                    tempComponents.AddRange(regionTempPath.Descendants("Service").ToList());
                    tempComponents.AddRange(regionTempPath.Descendants("Website").ToList());
                    tempComponents.AddRange(regionTempPath.Descendants("WebSite").ToList());

                    var partialManifestdirectory = new DirectoryInfo(ConfigurationSettings.AppSettings["PartialManifestPath"].ToString());
                    bool isModified;
                    foreach (ListViewItem item in listView1.Items)
                    {
                        isModified = false;
                        foreach (var tempComponent in tempComponents)
                        {
                            if (item.Text == tempComponent.Attribute("Name").Value)
                            {
                                if (partialManifestdirectory.GetDirectories(tempComponent.Attribute("Name").Value, SearchOption.AllDirectories).Any())
                                {
                                    var partialManifestFile = partialManifestdirectory.GetDirectories(tempComponent.Attribute("Name").Value).First().GetFiles()
                                                 .OrderByDescending(f => f.LastWriteTime)
                                                 .First();
                                    var partialManifest = XDocument.Load(partialManifestFile.FullName).Descendants("Package").First();
                                    var partialComponentName = partialManifest.Attribute("ComponentName").Value;
                                    var partialVersion = partialManifest.Attribute("Version").Value.Split('.');

                                    foreach (var refComponentElement in referenceManifest)
                                    {
                                        if (partialComponentName == refComponentElement.Attribute("Name").Value && IsComponentModified(partialVersion, refComponentElement.Attribute("Version").Value.Split('.')))
                                        {
                                            item.BackColor = Color.SkyBlue;
                                            isModified = true;
                                            break;
                                        }
                                    }
                                }
                            }
                            if (isModified)
                                break;
                        }
                    }
                }

                //Read Tempalate Specific to region and then partial file

            }
            Console.WriteLine(result);
        }

        //Generate manifest
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var regionTempPath = XDocument.Load(System.Configuration.ConfigurationSettings.AppSettings["TemplateRootPath"].ToString() + GetRegionTemplateName(ddlRegion.SelectedItem.ToString(), "AppStore"));

                var tempComponents = new List<XElement>();
                tempComponents.AddRange(regionTempPath.Descendants("Service").ToList());
                tempComponents.AddRange(regionTempPath.Descendants("Website").ToList());
                tempComponents.AddRange(regionTempPath.Descendants("WebSite").ToList());

                var partialManifestdirectory = new DirectoryInfo(@"D:\PartialManifest\");

                var productNames = new List<string>();
                var componentList = new List<XElement>();
                var preRequisitesList = new List<XElement>();

                var compVersionNumber = GetVersionNumber();
                XDocument xmlDocument = new XDocument();
                var rootElement = new XElement("ReleaseManifest", new object[] { new XAttribute("type", "Component"), new XAttribute("version", compVersionNumber) });

                //XDocument xmlDocument = new XDocument(
                //  new XElement("ReleaseManifest", new object[] { new XAttribute("type", "Component"), new XAttribute("version", compVersionNumber) },
                //  productNames.Select(p => new XElement("Product", new object[]{ new XAttribute("Name", p), 
                //                componentList.Select(c => c),
                //                new XElement("Pre-Requisties", preRequisitesList.Select(pr => pr))}))

                //));

                foreach (ListViewItem item in listView1.CheckedItems)
                {
                    var tempComponent = tempComponents.Where(t => t.Attribute("Name").Value == item.Text).FirstOrDefault();

                    var productElement = new XElement("Product", new XAttribute("Name", item.Text));
                    if (partialManifestdirectory.GetDirectories(tempComponent.Attribute("Name").Value).Any())
                    {
                        var partialManifestFile = partialManifestdirectory.GetDirectories(tempComponent.Attribute("Name").Value).First().GetFiles()
                                         .OrderByDescending(f => f.LastWriteTime)
                                         .First();
                        var partialManifest = XDocument.Load(partialManifestFile.FullName).Descendants("Package").ToList();
                        foreach (var tempItems in tempComponent.Descendants("Component").ToList())
                        {
                            var partialItem = partialManifest.Where(p => p.Attributes("ComponentName").First().Value == tempItems.Attribute("Name").Value).FirstOrDefault();
                            if (partialItem != null)
                                productElement.Add(new XElement("Component", new object[]{ new XAttribute("Name", partialItem.Attribute("ComponentName").Value)
                                            ,new XAttribute("Version",  partialItem.Attribute("Version").Value), 
                                            new XAttribute("Path",  partialItem.Attribute("Path").Value)}));
                        }

                        foreach (var pckItem in tempComponent.Descendants("Package").ToList())
                        {
                            var partialItem = partialManifest.Where(p => p.Attributes("ComponentName").First().Value == pckItem.Attribute("Name").Value).FirstOrDefault();
                            if (partialItem != null)
                                productElement.Add(new XElement("Package", new object[]{ new XAttribute("Name", partialItem.Attribute("ComponentName").Value)
                                            ,new XAttribute("Version",  partialItem.Attribute("Version").Value), 
                                            new XAttribute("Path",  partialItem.Attribute("Path").Value)}));
                        }

                        rootElement.Add(productElement);
                    }
                }
                xmlDocument.Add(rootElement);
                xmlDocument.Save(ConfigurationSettings.AppSettings["FinalReleaseManifest"].ToString() +
                    "ReleaseManifest_IGHS_" + ddlRegion.SelectedItem.ToString() + "_AppStore_" + compVersionNumber + ".xml");
                lblError.Text = "Manifest Generated Successfully";
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }

        private string GetVersionNumber()
        {
            var directory = new DirectoryInfo(ConfigurationSettings.AppSettings["FinalReleaseManifest"].ToString());
            var fileName = directory.GetFiles().Any() ? directory.GetFiles().OrderByDescending(f => f.LastWriteTime).First().Name : string.Empty;

            if (!string.IsNullOrEmpty(fileName))
            {
                fileName = fileName.Substring(0, fileName.LastIndexOf('.'));

            }
            return "8.0.0." + (string.IsNullOrEmpty(fileName) ? "1" : (Convert.ToInt16(fileName.Substring(fileName.LastIndexOf('.') + 1)) + 1).ToString()); ;
        }

        public string GetRegionTemplateName(string regionName, string applicationName)
        {
            return ConfigurationSettings.AppSettings[applicationName + "_" + regionName].ToString();
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
    }
}
