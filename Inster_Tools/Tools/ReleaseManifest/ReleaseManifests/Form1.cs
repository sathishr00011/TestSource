using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Xml.Linq;
using System.Configuration;

namespace ReleaseManifests
{
    public partial class Form1 : Form
    {

        //********** THINGS TO DO**************
        //1.Option to select AppStore,Tibco,Instore,OMS
        //2.Dev,STG,Production specific manifest generation
        //3.Loading region and checkboxlist based on application(Refered to point 1)
        //4.Generate reference file which contains all latest version and msi path
        //Test with all application type and templates
        //*************************************
        public Form1()
        {
            InitializeComponent();
        }

        public static string _applicationName;

        private void Form1_Load(object sender, EventArgs e)
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
                string objComponents = string.Empty;
                XDocument regionTempPath = null;
                if (txtReference.Text.Contains("AppStore"))
                {
                    ApplicationName = "AppStore";
                    objComponents = ConfigurationManager.AppSettings["Grocery"].ToString();
                    regionTempPath = GetRegionTemplate();
                }

                foreach (var item in objComponents.Split(','))
                {
                    listView1.Items.Add(new ListViewItem((new CheckBox()).Text = item));
                }

                var referenceManifest = XDocument.Load(openReferenceFileDialog.FileName).Descendants("Component").ToList();

                var tempComponents = new List<XElement>();
                tempComponents.AddRange(regionTempPath.Descendants("Service").ToList());
                tempComponents.AddRange(regionTempPath.Descendants("Website").ToList());
                tempComponents.AddRange(regionTempPath.Descendants("WebSite").ToList());

                var partialManifestdirectory = new DirectoryInfo(ConfigurationManager.AppSettings["AppStore_PartialManifestPath"].ToString());
                bool isModified;
                foreach (ListViewItem item in listView1.Items)
                {
                    isModified = false;
                    var tempComponent = tempComponents.Where(t => t.Attribute("Name").Value == item.Text);
                    foreach (var component in tempComponent.Descendants("Component").ToList())
                    {
                        if (partialManifestdirectory.GetDirectories(component.Attribute("Name").Value).Any())
                        {
                            var partialManifestFile = partialManifestdirectory.GetDirectories(component.Attribute("Name").Value).First().GetFiles()
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
                        if (isModified)
                            break;
                    }
                }

            }
            Console.WriteLine(result);
        }

        //Generate manifest
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var regionTempPath = GetRegionTemplate();

                var tempComponents = new List<XElement>();
                tempComponents.AddRange(regionTempPath.Descendants("Service").ToList());
                tempComponents.AddRange(regionTempPath.Descendants("Website").ToList());
                tempComponents.AddRange(regionTempPath.Descendants("WebSite").ToList());

                var partialManifestdirectory = new DirectoryInfo(ConfigurationManager.AppSettings["AppStore_PartialManifestPath"].ToString());

                var productNames = new List<string>();
                var componentList = new List<XElement>();
                var preRequisitesList = new List<XElement>();

                var compVersionNumber = GetVersionNumber();
                XDocument xmlDocument = new XDocument();                
                var rootElement = new XElement("ReleaseManifest", new object[] { new XAttribute("type", "Component"), new XAttribute("version", compVersionNumber) });
               
                foreach (ListViewItem item in listView1.CheckedItems)
                {
                    var tempComponent = tempComponents.Where(t => t.Attribute("Name").Value == item.Text).FirstOrDefault();

                    var productElement = new XElement("Product", new XAttribute("Name", item.Text));
                    foreach (var component in tempComponent.Descendants("Component").ToList())
                    {
                        if (partialManifestdirectory.GetDirectories(component.Attribute("Name").Value).Any())
                        {
                            var partialManifestFile = partialManifestdirectory.GetDirectories(component.Attribute("Name").Value).First().GetFiles()
                                             .OrderByDescending(f => f.LastWriteTime)
                                             .First();
                            var partialManifest = XDocument.Load(partialManifestFile.FullName).Descendants("Package").ToList();

                            var partialItem = partialManifest.Where(p => p.Attributes("ComponentName").First().Value == component.Attribute("Name").Value).FirstOrDefault();
                            if (partialItem != null)
                                productElement.Add(new XElement("Component", new object[]{ new XAttribute("Name", partialItem.Attribute("ComponentName").Value)
                                            ,new XAttribute("Version",  partialItem.Attribute("Version").Value), 
                                            new XAttribute("Path",  partialItem.Attribute("Path").Value)}));
                        }                        
                    }
                    rootElement.Add(productElement);

                    var subRootElement = new XElement("ReleaseManifest", new object[] { new XAttribute("type", "Component"), new XAttribute("version", compVersionNumber) });
                    var subComponentDocument = new XDocument();
                    subRootElement.Add(productElement);
                    subComponentDocument.Add(subRootElement);
                    subComponentDocument.Save(ConfigurationManager.AppSettings["ComponentManifest"].ToString() +
                "ReleaseManifest_IGHS_" + ddlRegion.SelectedItem.ToString() + "_AppStore_" + item.Text + "_" + compVersionNumber + ".xml");
                }
                xmlDocument.Add(rootElement);
                xmlDocument.Save(ConfigurationManager.AppSettings["FinalReleaseManifest"].ToString() +
                    "ReleaseManifest_IGHS_" + ddlRegion.SelectedItem.ToString() + "_AppStore_" + compVersionNumber + ".xml");
                lblError.Text = "Manifest Generated Successfully";
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }

        private static string ApplicationName
        {
            get { return _applicationName; }
            set { _applicationName = value; }
        }

        private XDocument GetRegionTemplate()
        {
            return XDocument.Load(ConfigurationManager.AppSettings["TemplateRootPath"].ToString() + ConfigurationManager.AppSettings[ApplicationName + "_" + ddlRegion.SelectedItem.ToString()].ToString());
        }

        private string GetVersionNumber()
        {
            var directory = new DirectoryInfo(ConfigurationManager.AppSettings["FinalReleaseManifest"].ToString());
            var fileName = directory.GetFiles().Any() ? directory.GetFiles().OrderByDescending(f => f.LastWriteTime).First().Name : string.Empty;

            if (!string.IsNullOrEmpty(fileName))
            {
                fileName = fileName.Substring(0, fileName.LastIndexOf('.'));

            }
            return "8.0.0." + (string.IsNullOrEmpty(fileName) ? "1" : (Convert.ToInt16(fileName.Substring(fileName.LastIndexOf('.') + 1)) + 1).ToString()); ;
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
    }



}

