using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using System.IO;
using System.Configuration;

namespace ReleaseManifests
{
    public partial class ComponentManifest : Form
    {
        private const string VersionConfigFileName = "VersionConfig.xml";
        private const string outputPathUserName = "service_dbt66";
        private const string outputPathDomainName = "DOTCOM";
        private const string outputPathPassword = "XSN4Ke2#Z&q!4Tz";
        private string componentRegionName = string.Empty;
        private bool tibcoComponentManifestGenerated = false;
        private List<string> missingComponent = null;
        private bool totallingManifestGenerated = false;
        public ComponentManifest()
        {
            InitializeComponent();
        }

        #region Events
        private void ComponentManifest_Load(object sender, EventArgs e)
        {
            ddlReleaseVerison.Items.AddRange(ConfigurationManager.AppSettings["CurrentReleaseVersion"].ToString().Split(',').OrderByDescending(r => Convert.ToDecimal(r.Substring(1))).ToArray());
            ddlReleaseVerison.SelectedIndex = 0;
            ManifestTabContainer_SelectedIndexChanged(sender, e);
        }

        private void ManifestTabContainer_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;
            switch (ManifestTabContainer.SelectedTab.Name)
            {
                case "Grocery":
                    LoadComponentsList(chkListGrocery, "Grocery", "UK");
                    SetTabPageSize();
                    break;
                case "AppStore":
                    LoadComponentsList(chkListAppStore, "AppStore", "UK");
                    SetTabPageSize();
                    break;
                case "OMS":
                    LoadComponentsList(chkListOms, "OMS", "UK");
                    SetTabPageSize();
                    break;
                case "Tibco":
                    this.Size = new Size(1101, 590);
                    ManifestTabContainer.Size = new Size(1072, 476);
                    this.Location = new Point(this.MdiParent.Size.Width / 2 - this.Location.X - 40, this.Location.Y);
                    break;
                case "Other":
                    lblMessage.Text = string.Empty;
                    chkListOther.Items.Clear();
                    chkListOther.Items.AddRange(ConfigurationManager.AppSettings["Other_Components"].Split(','));
                    SetTabPageSize();
                    break;
                case "Tot_QP_Chk":
                    var componentNames = ConfigurationManager.AppSettings["Tot_QP_Chk"].ToString().Split(',');
                    ddlComponents.DataSource = componentNames;
                    //ddlComponents.SelectedIndex = 0;
                    SetTabPageSize();
                    break;
                case "BIReporting":
                    LoadBIReportingTab();
                    SetTabPageSize();
                    break;
            }
        }

        private void btnGenerateComponentManifest_Click(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;
            SetProgress(true);
            var buttonName = ((Button)sender).Name;
            switch (buttonName)
            {
                case "btnGrocery":
                    GenerateComponentManifest("Grocery", "Grocery_VersionNumber", chkListGrocery, "UK");
                    break;
                case "btnOms":
                    GenerateComponentManifest("OMS", "OMS_VersionNumber", chkListOms, "UK");
                    break;
                case "btnAppStore":
                    GenerateComponentManifest("AppStore", "AppStore_VersionNumber", chkListAppStore, "UK");
                    break;
                case "btnOther":
                    GenerateOtherComponentManifest("Other", chkListOther, "UK");
                    break;
                case "btnTotQpChk":
                    try
                    {
                        if (chkListRegions.CheckedItems.Count > 0)
                        {
                            missingComponent = new List<string>();
                            var newVersion = TfsCheckoutCheckin.CheckOutFromTFS("TotQpChk_VersionConfig.xml", ddlComponents.SelectedItem.ToString() + "_VersionNumber", ddlReleaseVerison.SelectedItem.ToString());
                            foreach (var item in chkListRegions.CheckedItems)
                            {
                                GenerateTotQpChkManifest(ddlComponents.SelectedItem.ToString(), newVersion, item.ToString());
                            }
                            if (missingComponent.Any())
                            {
                                var message = "Manifest not generated for regions - " + Environment.NewLine + string.Join(Environment.NewLine, missingComponent.ToArray());
                                MessageBox.Show(message);
                            }
                            if (totallingManifestGenerated)
                            {
                                TfsCheckoutCheckin.CheckInToTFS("TotQpChk_VersionConfig.xml", ddlReleaseVerison.SelectedItem.ToString());
                                lblMessage.Text = "Manifest Generated Successfully " + newVersion;
                                lblMessage.ForeColor = Color.Green;
                            }
                            else
                            {
                                TfsCheckoutCheckin.UndoChangesToTFS("TotQpChk_VersionConfig.xml", ddlReleaseVerison.SelectedItem.ToString());
                            }
                        }
                        else
                        {
                            lblMessage.Text = "Please select atleast one region";
                            lblMessage.ForeColor = Color.Red;
                        }
                    }
                    catch (Exception ex)
                    {
                        lblMessage.ForeColor = Color.Red;
                        lblMessage.Text = ex.Message;
                        TfsCheckoutCheckin.UndoChangesToTFS("TotQpChk_VersionConfig.xml", ddlReleaseVerison.SelectedItem.ToString());
                    }
                    break;
                case "btnBIReporting":
                    GenerateComponentManifest("BIReporting", "BIReporting_VersionNumber", chkListBIReporting, "UK");
                    break;
            }
            SetProgress(false);
        }

        /// <summary>
        /// For Tibco regions checkbox check changed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chk_CheckedChanged(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;
            var applicationName = "Tibco";
            CheckBox checkBox = (CheckBox)sender;
            var regionName = checkBox.Text;
            switch (regionName)
            {
                case "UK":
                    if (checkBox.Checked)
                        LoadComponentsList(chkListUk, applicationName, regionName);
                    else chkListUk.Items.Clear();
                    break;
                case "CZ":
                    if (checkBox.Checked)
                        LoadComponentsList(chkListCz, applicationName, regionName);
                    else chkListCz.Items.Clear();
                    break;
                case "PL":
                    if (checkBox.Checked)
                        LoadComponentsList(chkListPl, applicationName, regionName);
                    else chkListPl.Items.Clear();
                    break;
                case "CE":
                    if (checkBox.Checked)
                        LoadComponentsList(chkListCe, applicationName, regionName);
                    else chkListCe.Items.Clear();
                    break;
                case "SK":
                    if (checkBox.Checked)
                        LoadComponentsList(chkListSk, applicationName, regionName);
                    else chkListSk.Items.Clear();
                    break;
                case "TH":
                    if (checkBox.Checked)
                        LoadComponentsList(chkListTh, applicationName, regionName);
                    else chkListTh.Items.Clear();
                    break;
                case "MY":
                    if (checkBox.Checked)
                        LoadComponentsList(chkListMy, applicationName, regionName);
                    else chkListMy.Items.Clear();
                    break;
                case "APUK":
                    if (checkBox.Checked)
                        LoadComponentsList(chkListApuk, applicationName, regionName);
                    else chkListApuk.Items.Clear();
                    break;
                case "AP":
                    if (checkBox.Checked)
                        LoadComponentsList(chkListAp, applicationName, regionName);
                    else chkListAp.Items.Clear();
                    break;
                case "HU":
                    if (checkBox.Checked)
                        LoadComponentsList(chkListHu, applicationName, regionName);
                    else chkListHu.Items.Clear();
                    break;
                case "CH":
                    if (checkBox.Checked)
                        LoadComponentsList(chkListCh, applicationName, regionName);
                    else chkListCh.Items.Clear();
                    break;
                case "CS":
                    if (checkBox.Checked)
                        LoadComponentsList(chkListCs, applicationName, regionName);
                    else chkListCs.Items.Clear();
                    break;
                case "TR":
                    if (checkBox.Checked)
                        LoadComponentsList(chkListTr, applicationName, regionName);
                    else chkListTr.Items.Clear();
                    break;
                case "Hwak":
                    //if (checkBox.Checked)
                    //    LoadComponentsList(chkListHwak, applicationName, regionName);
                    //else chkListHwak.Items.Clear();
                    break;
                case "ErrorHandling_AP":
                    //if (checkBox.Checked)
                    //    LoadComponentsList(chkListErrorHandlingAp, applicationName, regionName);
                    //else chkListErrorHandlingAp.Items.Clear();
                    break;
                case "ErrorHandling_CE":
                    //if (checkBox.Checked)
                    //    LoadComponentsList(chkListErrorHandlingCe, applicationName, regionName);
                    //else chkListErrorHandlingCe.Items.Clear();
                    break;
                case "ErrorHandling_CH":
                    //if (checkBox.Checked)
                    //    LoadComponentsList(chkListErrorHandlingCh, applicationName, regionName);
                    //else chkListErrorHandlingCh.Items.Clear();
                    break;
            }
        }
        //Code for Tibco click 

        private void btnTibco_Click(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;
            SetProgress(true);
            var checkBoxControlsList = Tibco.Controls.OfType<CheckBox>();
            var applicationName = "Tibco";
            try
            {
                var newVersion = TfsCheckoutCheckin.CheckOutFromTFS(applicationName + "_" + VersionConfigFileName, "TIBCO_VersionNumber", ddlReleaseVerison.SelectedItem.ToString());
                foreach (CheckBox chkBoxItem in checkBoxControlsList)
                {
                    if (chkBoxItem.Checked)
                    {
                        var regionName = chkBoxItem.Text;
                        switch (regionName)
                        {
                            case "UK":
                                GenerateTibcoComponentManifest(applicationName, newVersion, chkListUk, regionName);
                                break;
                            case "CZ":
                                GenerateTibcoComponentManifest(applicationName, newVersion, chkListCz, regionName);
                                break;
                            case "PL":
                                GenerateTibcoComponentManifest(applicationName, newVersion, chkListPl, regionName);
                                break;
                            case "CE":
                                GenerateTibcoComponentManifest(applicationName, newVersion, chkListCe, regionName);
                                break;
                            case "SK":
                                GenerateTibcoComponentManifest(applicationName, newVersion, chkListSk, regionName);
                                break;
                            case "TH":
                                GenerateTibcoComponentManifest(applicationName, newVersion, chkListTh, regionName);
                                break;
                            case "MY":
                                GenerateTibcoComponentManifest(applicationName, newVersion, chkListMy, regionName);
                                break;
                            case "APUK":
                                GenerateTibcoComponentManifest(applicationName, newVersion, chkListApuk, regionName);
                                break;
                            case "AP":
                                GenerateTibcoComponentManifest(applicationName, newVersion, chkListAp, regionName);
                                break;
                            case "HU":
                                GenerateTibcoComponentManifest(applicationName, newVersion, chkListHu, regionName);
                                break;
                            case "CH":
                                GenerateTibcoComponentManifest(applicationName, newVersion, chkListCh, regionName);
                                break;
                            case "CS":
                                GenerateTibcoComponentManifest(applicationName, newVersion, chkListCs, regionName);
                                break;
                            case "TR":
                                GenerateTibcoComponentManifest(applicationName, newVersion, chkListTr, regionName);
                                break;
                            case "Hwak":
                                //    GenerateTibcoComponentManifest(applicationName, newVersion, chkListHwak, regionName);
                                break;
                            case "ErrorHandling_AP":
                                //  GenerateTibcoComponentManifest(applicationName, newVersion, chkListErrorHandlingAp, regionName);
                                break;
                            case "ErrorHandling_CE":
                                //  GenerateTibcoComponentManifest(applicationName, newVersion, chkListErrorHandlingCe, regionName);
                                break;
                            case "ErrorHandling_CH":
                                //  GenerateTibcoComponentManifest(applicationName, newVersion, chkListErrorHandlingCh, regionName);
                                break;
                        }

                    }
                }

                if (tibcoComponentManifestGenerated)
                {
                    TfsCheckoutCheckin.CheckInToTFS(applicationName + "_" + VersionConfigFileName, ddlReleaseVerison.SelectedItem.ToString());
                    lblMessage.Text = "Manifest Generated Successfully " + newVersion;
                    lblMessage.ForeColor = Color.Green;
                    tibcoComponentManifestGenerated = false;
                }

                if (!string.IsNullOrEmpty(componentRegionName))//Display select component message specific to region
                {
                    if (lblMessage.Text != string.Empty)
                    {
                        lblMessage.Text = lblMessage.Text + Environment.NewLine + "Please select atleast one component for region " + componentRegionName.TrimStart(',');
                    }
                    else
                        lblMessage.Text = "Please select atleast one component for region " + componentRegionName.TrimStart(',');
                    lblMessage.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = ex.Message;
                TfsCheckoutCheckin.UndoChangesToTFS(applicationName + "_" + VersionConfigFileName, ddlReleaseVerison.SelectedItem.ToString());
            }
            finally
            {
                SetProgress(false);
            }
        }

        private void ddlComponents_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;
            try
            {
                chkListRegions.Items.Clear();
                chkListRegions.Items.AddRange(ConfigurationManager.AppSettings[ddlComponents.SelectedItem.ToString()].ToString().Split(','));
            }
            catch (Exception ex)
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = ex.Message;
            }
        }

        private void ddlReleaseVerison_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;
            if (ManifestTabContainer.SelectedTab.Name == "BIReporting")
            {
                LoadBIReportingTab();
            }
        }
        #endregion

        #region Methods
        private void LoadComponentsList(CheckedListBox chkList, string applicationName, string regionName)
        {
            try
            {
                chkList.Items.Clear();
                var descendantElementName = string.Empty;

                switch (applicationName)
                {
                    case "Grocery": descendantElementName = "Product"; break;
                    case "AppStore": descendantElementName = "Product"; break;
                    case "OMS": descendantElementName = "Product"; break;
                    case "Tibco": descendantElementName = "Service"; break;
                    case "BIReporting": descendantElementName = "Product"; break;
                }

                //Based on Active tab template component names to be read
                XDocument regionTempPath = ReleaseManifest.GetRegionTemplate(applicationName, regionName);
                foreach (var component in regionTempPath.Descendants(descendantElementName))
                {
                    chkList.Items.Add(component.Attribute("Name").Value);
                }
            }
            catch (Exception ex)
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = "Template Not Found";
            }
        }

        private void GenerateComponentManifest(string applicationName, string versionKeyName, CheckedListBox chkList, string regionName)
        {
            try
            {
                if (chkList.CheckedItems.Count > 0)
                {
                    missingComponent = new List<string>();
                    var newVersion = TfsCheckoutCheckin.CheckOutFromTFS(applicationName + "_" + VersionConfigFileName, versionKeyName, ddlReleaseVerison.SelectedItem.ToString());
                    var components = ReleaseManifest.GetRegionTemplate(applicationName, regionName).Descendants("Product");
                    // List<string> selectedComponents = new List<string>();
                    var isManifestGenerated = false;
                    //foreach (var item in chkList.CheckedItems)
                    //{
                    //    selectedComponents.Add(item.ToString());
                    //}

                    using (new Impersonator(outputPathUserName, outputPathDomainName, outputPathPassword))
                    {
                        List<DirectoryInfo> partialManifestDirectories =
                            ConfigurationManager.AppSettings["PartialManifestPaths"].ToString().Split(',').Select(c => new DirectoryInfo(c)).ToList();

                        // foreach (var component in components)
                        foreach (var item in chkList.CheckedItems)
                        {
                            var component = components.Where(c => c.Attribute("Name").Value == item.ToString()).FirstOrDefault();
                            var componentName = component.Attribute("Name").Value;
                            //if (selectedComponents.Contains(componentName))
                            //{
                            var productElement = new XElement("Product", new object[] { new XAttribute("Name", componentName), new XAttribute("InstallType", component.Attribute("InstallType").Value) });
                            XElement serviceElement = null;
                            if (component.Element("Service") != null)
                            {
                                serviceElement = new XElement("Service", component.Element("Service").Attributes());
                            }

                            var packages = component.Descendants("Package");
                            foreach (var package in packages)
                            {
                                var pName = package.Attribute("Name").Value;
                                try
                                {
                                    var partialManifestFile = partialManifestDirectories.
                                                                    SelectMany(s => s.GetDirectories(ddlReleaseVerison.SelectedItem.ToString(), SearchOption.AllDirectories)).
                                                                    SelectMany(m => m.GetDirectories(string.Format("*{0}", pName), SearchOption.AllDirectories)).
                                                                    SelectMany(x => x.GetFiles("*.xml")).OrderByDescending(f => f.LastWriteTimeUtc).First();
                                   
                                    var partialManifest = XDocument.Load(partialManifestFile.FullName).Descendants("Package").ToList();

                                    var partialItem = partialManifest.FirstOrDefault(p => p.Attributes("ComponentName").First().Value == pName);
                                    if (partialItem != null)
                                    {
                                        var cComp = new XElement("Package", new object[]{ new XAttribute("Name", partialItem.Attribute("ComponentName").Value)
                                            ,new XAttribute("InstallType", package.Attribute("InstallType").Value)
                                            ,new XAttribute("Version",  partialItem.Attribute("Version").Value), 
                                            new XAttribute("Path",  partialItem.Attribute("Path").Value)});

                                        if (component.Element("Service") != null)
                                        {
                                            serviceElement.Add(cComp);
                                        }
                                        else
                                        {
                                            productElement.Add(cComp);
                                        }
                                    }
                                    else
                                        missingComponent.Add(componentName + " [" + pName + "]");
                                }
                                catch
                                {
                                    missingComponent.Add(componentName + " [" + pName + "]");
                                }
                            }

                            if (component.Element("Service") != null)
                            {
                                productElement.Add(serviceElement);
                            }

                            var packagesCount = component.Element("Service") != null ? serviceElement.Elements().Count() : productElement.Elements().Count();
                            if (packages.Count() == packagesCount)
                            {
                                isManifestGenerated = true;
                                XDocument xmlDocument = new XDocument();
                                var rootElement = new XElement("ReleaseManifest", new object[] { new XAttribute("type", "Component"), new XAttribute("version", newVersion) });
                                rootElement.Add(productElement);

                                xmlDocument.Add(rootElement);
                                string manifestOutputPath = ConfigurationManager.AppSettings["ComponentManifest"].ToString() + ddlReleaseVerison.SelectedItem.ToString() + @"\";
                                string finalManifestname = "ComponentManifest_IGHS_" + applicationName + "_" + regionName + "_" + componentName + "_" + newVersion + ".xml";
                                var finalFilePath = Path.Combine(manifestOutputPath, finalManifestname);
                                if (File.Exists(finalFilePath))
                                    File.Delete(finalFilePath);
                                xmlDocument.Save(finalFilePath);
                            }
                            // }
                        }
                    }

                    if (missingComponent != null && missingComponent.Any())
                    {
                        if (!isManifestGenerated)
                            TfsCheckoutCheckin.UndoChangesToTFS(applicationName + "_" + VersionConfigFileName, ddlReleaseVerison.SelectedItem.ToString());
                        var message = "Manifest not generated for components - " + Environment.NewLine + string.Join(Environment.NewLine, missingComponent.ToArray());
                        MessageBox.Show(message);
                    }

                    if (isManifestGenerated)
                    {
                        TfsCheckoutCheckin.CheckInToTFS(applicationName + "_" + VersionConfigFileName, ddlReleaseVerison.SelectedItem.ToString());
                        lblMessage.Text = "Manifest Generated Successfully " + newVersion;
                        lblMessage.ForeColor = Color.Green;
                    }
                }
                else
                {
                    lblMessage.Text = "Please select atleast one component";
                    lblMessage.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                TfsCheckoutCheckin.UndoChangesToTFS(applicationName + "_" + VersionConfigFileName, ddlReleaseVerison.SelectedItem.ToString());
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = ex.Message;
            }
        }

        private void GenerateTibcoComponentManifest(string applicationName, string newVersion, CheckedListBox chkList, string regionName)
        {
            if (chkList.CheckedItems.Count > 0)
            {
                tibcoComponentManifestGenerated = true;
                XDocument regionTempPath = ReleaseManifest.GetRegionTemplate(applicationName, regionName);
                using (new Impersonator(outputPathUserName, outputPathDomainName, outputPathPassword))
                {
                    List<DirectoryInfo> partialManifestDirectories =
                        ConfigurationManager.AppSettings["PartialManifestPaths"].ToString().Split(',').Select(c => new DirectoryInfo(c)).ToList();

                    //List<string> selectedComponents = new List<string>();
                    //foreach (var item in chkList.CheckedItems)
                    //{
                    //    selectedComponents.Add(item.ToString());
                    //}

                    var components = regionTempPath.Descendants("Service");
                    //foreach (var component in components)
                    foreach (var item in chkList.CheckedItems)
                    {
                        var component = components.Where(c => c.Attribute("Name").Value == item.ToString()).FirstOrDefault();
                        var componentName = component.Attribute("Name").Value;
                        //if (selectedComponents.Contains(componentName))
                        //{
                        var partialManifestFile = partialManifestDirectories.
                                                        SelectMany(s => s.GetDirectories(ddlReleaseVerison.SelectedItem.ToString(), SearchOption.AllDirectories)).
                                                        SelectMany(m => m.GetDirectories(string.Format("*{0}*", applicationName + "_" + componentName), SearchOption.AllDirectories)).
                                                        SelectMany(x => x.GetFiles("*.xml")).OrderByDescending(f => f.LastWriteTimeUtc).First();
                        var partialManifest = XDocument.Load(partialManifestFile.FullName).Descendants("Package").ToList();

                        var partialItem = partialManifest.FirstOrDefault(p => p.Attributes("ComponentName").First().Value == componentName);
                        if (partialItem != null)
                        {
                            var productElement = new XElement("Service", new object[] { 
                                    new XAttribute("Name", componentName), 
                                    new XAttribute("InstallType", component.Attribute("InstallType").Value),
                                    new XAttribute("Version",  partialItem.Attribute("Version").Value),
                                    new XAttribute("Path",  partialItem.Attribute("Path").Value)
                                });

                            var componentElements = component.Descendants("Component");
                            foreach (XElement element in componentElements)
                            {
                                productElement.Add(element);
                            }
                            XDocument xmlDocument = new XDocument();
                            var rootElement = new XElement("ReleaseManifest", new object[] { new XAttribute("type", "Component"), new XAttribute("version", newVersion) });
                            rootElement.Add(productElement);

                            xmlDocument.Add(rootElement);
                            string manifestOutputPath = ConfigurationManager.AppSettings["ComponentManifest"].ToString() + ddlReleaseVerison.SelectedItem.ToString() + @"\";
                            string finalManifestname = "ComponentManifest_IGHS_" + applicationName + "_" + regionName + "_" + componentName + "_" + newVersion + ".xml";
                            var finalFilePath = Path.Combine(manifestOutputPath, finalManifestname);
                            if (File.Exists(finalFilePath))
                                File.Delete(finalFilePath);
                            xmlDocument.Save(finalFilePath);
                        }
                        // }
                    }

                }
            }
            else
            {
                componentRegionName = componentRegionName + "," + regionName;
            }
        }

        private void GenerateOtherComponentManifest(string applicationName, CheckedListBox chkList, string regionName)
        {
            if (chkList.SelectedItems.Count > 0)
            {
                try
                {
                    missingComponent = new List<string>();
                    var versionNumber = "_VersionNumber";
                    var newComponentVersions = string.Empty;

                    var manifestGenerated = false;
                    List<DirectoryInfo> partialManifestDirectories =
                        ConfigurationManager.AppSettings["PartialManifestPaths"].ToString().Split(',').Select(c => new DirectoryInfo(c)).ToList();
                    foreach (var item in chkList.CheckedItems)
                    {
                        var isManifestGenerated = false;
                        var newVersion = TfsCheckoutCheckin.CheckOutFromTFS(applicationName + "_" + VersionConfigFileName, item + versionNumber, ddlReleaseVerison.SelectedItem.ToString());
                        var components = ReleaseManifest.GetRegionTemplate(item.ToString(), regionName).Descendants("Product");
                        using (new Impersonator(outputPathUserName, outputPathDomainName, outputPathPassword))
                        {
                            //foreach (var component in components)
                            //{
                            var component = components.Where(c => c.Attribute("Name").Value == item.ToString()).FirstOrDefault();
                            var componentName = component.Attribute("Name").Value;
                            var productElement = new XElement("Product", new object[] { new XAttribute("Name", componentName), new XAttribute("InstallType", component.Attribute("InstallType").Value) });
                            XElement serviceElement = null;
                            if (component.Element("Service") != null)
                            {
                                serviceElement = new XElement("Service", component.Element("Service").Attributes());
                            }

                            var packages = component.Descendants("Package");
                            foreach (var package in packages)
                            {
                                var pName = package.Attribute("Name").Value;
                                try
                                {
                                    var partialManifestFile = partialManifestDirectories.
                                                                    SelectMany(s => s.GetDirectories(ddlReleaseVerison.SelectedItem.ToString(), SearchOption.AllDirectories)).
                                                                    SelectMany(m => m.GetDirectories(string.Format("*{0}", pName), SearchOption.AllDirectories)).
                                                                    SelectMany(x => x.GetFiles("*.xml")).OrderByDescending(f => f.LastWriteTimeUtc).First();
                                    var partialManifest = XDocument.Load(partialManifestFile.FullName).Descendants("Package").ToList();

                                    var partialItem = partialManifest.FirstOrDefault(p => p.Attributes("ComponentName").First().Value == pName);
                                    if (partialItem != null)
                                    {
                                        var cComp = new XElement("Package", new object[]{ new XAttribute("Name", partialItem.Attribute("ComponentName").Value)
                                            ,new XAttribute("InstallType", package.Attribute("InstallType").Value)
                                            ,new XAttribute("Version",  partialItem.Attribute("Version").Value), 
                                            new XAttribute("Path",  partialItem.Attribute("Path").Value)});

                                        if (component.Element("Service") != null)
                                        {
                                            serviceElement.Add(cComp);
                                        }
                                        else
                                        {
                                            productElement.Add(cComp);
                                        }
                                    }
                                    else
                                        missingComponent.Add(item.ToString() + " [" + pName + "]");
                                }
                                catch
                                {
                                    missingComponent.Add(item.ToString() + " [" + pName + "]");
                                }
                            }

                            if (component.Element("Service") != null)
                            {
                                productElement.Add(serviceElement);
                            }

                            var packagesCount = component.Element("Service") != null ? serviceElement.Elements().Count() : productElement.Elements().Count();
                            if (packages.Count() == packagesCount)
                            {
                                isManifestGenerated = true;
                                manifestGenerated = true;
                                XDocument xmlDocument = new XDocument();
                                var rootElement = new XElement("ReleaseManifest", new object[] { new XAttribute("type", "Component"), new XAttribute("version", newVersion) });
                                rootElement.Add(productElement);

                                xmlDocument.Add(rootElement);
                                string manifestOutputPath = ConfigurationManager.AppSettings["ComponentManifest"].ToString() + ddlReleaseVerison.SelectedItem.ToString() + @"\";
                                string finalManifestname = "ComponentManifest_IGHS_" + item + "_" + regionName + "_" + newVersion + ".xml"; //"_" + componentName +
                                var finalFilePath = Path.Combine(manifestOutputPath, finalManifestname);
                                if (File.Exists(finalFilePath))
                                    File.Delete(finalFilePath);
                                xmlDocument.Save(finalFilePath);
                                newComponentVersions = newComponentVersions + ", " + newVersion;
                            }
                            // }
                        }
                        if (isManifestGenerated)
                            TfsCheckoutCheckin.CheckInToTFS(applicationName + "_" + VersionConfigFileName, ddlReleaseVerison.SelectedItem.ToString());
                        else
                            TfsCheckoutCheckin.UndoChangesToTFS(applicationName + "_" + VersionConfigFileName, ddlReleaseVerison.SelectedItem.ToString());
                    }
                    if (missingComponent != null && missingComponent.Any())
                    {
                        var message = "Manifest not generated for components - " + Environment.NewLine + string.Join(Environment.NewLine, missingComponent.ToArray());
                        MessageBox.Show(message);
                    }
                    if (manifestGenerated)
                    {
                        lblMessage.Text = "Manifest Generated Successfully " + newComponentVersions.TrimStart(',');
                        lblMessage.ForeColor = Color.Green;
                    }
                }
                catch (Exception ex)
                {
                    TfsCheckoutCheckin.UndoChangesToTFS(applicationName + "_" + VersionConfigFileName, ddlReleaseVerison.SelectedItem.ToString());
                    lblMessage.Text = ex.Message;
                    lblMessage.ForeColor = Color.Red;
                }
            }
            else
            {
                lblMessage.Text = "Please select atleast one component";
                lblMessage.ForeColor = Color.Red;
            }
        }

        private void GenerateTotQpChkManifest(string applicationName, string newVersion, string regionName)
        {
            XDocument regionTempPath = ReleaseManifest.GetRegionTemplate(applicationName, regionName);

            using (new Impersonator(outputPathUserName, outputPathDomainName, outputPathPassword))
            {
                List<DirectoryInfo> partialManifestDirectories =
                    ConfigurationManager.AppSettings["PartialManifestPaths"].ToString().Split(',').Select(c => new DirectoryInfo(c)).ToList();

                var components = regionTempPath.Descendants("Service");
                foreach (var component in components)
                {
                    var componentName = component.Attribute("Name").Value;
                    var productElement = new XElement("Product", new object[] { new XAttribute("Name", componentName), new XAttribute("InstallType", component.Attribute("InstallType").Value) });
                    var serviceElement = new XElement("Service", component.Attributes());

                    var packages = component.Descendants("Package");
                    foreach (var package in packages)
                    {
                        var pName = package.Attribute("Name").Value;
                        try
                        {
                            var partialManifestFile = partialManifestDirectories.
                                                            SelectMany(s => s.GetDirectories(ddlReleaseVerison.SelectedItem.ToString(), SearchOption.AllDirectories)).
                                                            SelectMany(m => m.GetDirectories(string.Format("*{0}", pName), SearchOption.AllDirectories)).
                                                            SelectMany(x => x.GetFiles("*.xml")).OrderByDescending(f => f.LastWriteTimeUtc).First();
                            var partialManifest = XDocument.Load(partialManifestFile.FullName).Descendants("Package").ToList();

                            var partialItem = partialManifest.FirstOrDefault(p => p.Attributes("ComponentName").First().Value == pName);
                            if (partialItem != null)
                            {
                                var cComp = new XElement("Package", new object[]{ new XAttribute("Name", partialItem.Attribute("ComponentName").Value)
                                            ,new XAttribute("InstallType", package.Attribute("InstallType").Value)
                                            ,new XAttribute("Version",  partialItem.Attribute("Version").Value), 
                                            new XAttribute("Path",  partialItem.Attribute("Path").Value)});

                                serviceElement.Add(cComp);
                            }
                            else
                                missingComponent.Add(regionName + " [" + pName + "]");
                        }
                        catch
                        {
                            missingComponent.Add(regionName + " [" + pName + "]");
                        }
                    }
                    productElement.Add(serviceElement);
                    var packagesCount = serviceElement.Elements().Count();
                    if (packages.Count() == packagesCount)
                    {
                        totallingManifestGenerated = true;
                        XDocument xmlDocument = new XDocument();
                        var rootElement = new XElement("ReleaseManifest", new object[] { new XAttribute("type", "Component"), new XAttribute("version", newVersion) });
                        rootElement.Add(productElement);

                        xmlDocument.Add(rootElement);
                        string manifestOutputPath = ConfigurationManager.AppSettings["ComponentManifest"].ToString() + ddlReleaseVerison.SelectedItem.ToString() + @"\";
                        string finalManifestname = "ComponentManifest_IGHS_" + applicationName + "_" + regionName + "_" + newVersion + ".xml"; // "_" + componentName + 
                        var finalFilePath = Path.Combine(manifestOutputPath, finalManifestname);
                        if (File.Exists(finalFilePath))
                            File.Delete(finalFilePath);
                        xmlDocument.Save(finalFilePath);
                    }
                }
            }
        }

        private void SetTabPageSize()
        {
            this.Location = new Point(this.MdiParent.Size.Width / 2 - 160, this.Location.Y);
            this.Size = new Size(420, 590);
            ManifestTabContainer.Size = new Size(385, 476);
        }

        private void SetProgress(bool visibility)
        {
            //ManifestParent md1 = (ManifestParent)this.MdiParent;

            //ToolStripProgressBar tsp1 = (ToolStripProgressBar)md1.toolStripProgressBar1;
            //tsp1.Style = ProgressBarStyle.Marquee;
            //tsp1.Visible = visibility;
            //md1.Update();
            if (visibility)
            {
                ProgressIndicator progressIndicator = new ProgressIndicator();
                progressIndicator.MdiParent = this.MdiParent;
                progressIndicator.Show();
            }
            else
            {
                var childform = this.MdiParent.ActiveMdiChild;
                if (childform.Name == "ProgressIndicator")
                    childform.Close();
            }
        }

        private void LoadBIReportingTab()
        {
            var releaseVersion = ddlReleaseVerison.SelectedItem.ToString();
            if (!string.IsNullOrWhiteSpace(releaseVersion))
            {
                releaseVersion = releaseVersion.Substring(1, releaseVersion.Length - 1);
                if (Convert.ToInt16(releaseVersion.Substring(0, releaseVersion.LastIndexOf("."))) > 9)
                {
                    btnBIReporting.Enabled = true;
                    LoadComponentsList(chkListBIReporting, "BIReporting", "UK");
                }
                else
                {
                    btnBIReporting.Enabled = false;
                    chkListBIReporting.Items.Clear();
                }
            }
            else
            {
                btnBIReporting.Enabled = false;
                chkListBIReporting.Items.Clear();
            }
        }
        #endregion

        private void chkList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            lblMessage.Text = string.Empty;
        }
    }

}
