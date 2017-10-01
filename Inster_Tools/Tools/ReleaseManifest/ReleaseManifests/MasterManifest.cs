using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using System.IO;
using System.Configuration;
using System.Reflection;

namespace ReleaseManifests
{
    public partial class MasterManifest : Form
    {
        public MasterManifest()
        {
            InitializeComponent();
        }

        private const string VersionConfigFileName = "VersionConfig.xml";
        private const string outputPathUserName = "service_dbt66";
        private const string outputPathDomainName = "DOTCOM";
        private const string outputPathPassword = "XSN4Ke2#Z&q!4Tz";

        #region Events
        private void MasterManifest_Load(object sender, EventArgs e)
        {
            ddlReleaseVerison.Items.AddRange(ConfigurationManager.AppSettings["CurrentReleaseVersion"].ToString().Split(',').OrderByDescending(r => Convert.ToDecimal(r.Substring(1))).ToArray());
            ddlReleaseVerison.SelectedIndex = 0;
            ManifestTabContainer_SelectedIndexChanged(sender, e);
        }

        private void ManifestTabContainer_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ManifestTabContainer.SelectedTab.Name)
            {
                case "Grocery":
                    LoadComponentsList("Grocery", "UK", dgvGrocery);
                    break;
                case "AppStore":
                    LoadComponentsList("AppStore", "UK", dgvAppStore);
                    break;
                case "OMS":
                    LoadComponentsList("OMS", "UK", dgvOms);
                    break;
                case "Tibco":
                    var regionNames = ConfigurationManager.AppSettings["Tibco_Regions"].ToString().Split(',');
                    ddlTibcoRegions.DataSource = regionNames;
                    ddlTibcoRegions.SelectedIndex = 0;
                    LoadComponentsList("Tibco", ddlTibcoRegions.SelectedItem.ToString(), dgvTibco);
                    break;
            }
        }

        void dataGridView_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            var dgvObject = ((DataGridView)sender);
            if (dgvObject.IsCurrentCellDirty)
            {
                dgvObject.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var dgvObject = ((DataGridView)sender);
                switch (dgvObject.Name)
                {
                    case "dgvGrocery":
                        LoadVersionDropdown(dgvObject, e.RowIndex, "Grocery", "UK");
                        break;
                    case "dgvOms":
                        LoadVersionDropdown(dgvObject, e.RowIndex, "OMS", "UK");
                        break;
                    case "dgvAppStore":
                        LoadVersionDropdown(dgvObject, e.RowIndex, "AppStore", "UK");
                        break;
                    case "dgvTibco":
                        LoadVersionDropdown(dgvObject, e.RowIndex, "Tibco", ddlTibcoRegions.SelectedItem.ToString());
                        break;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private void btnGenerateComponentManifest_Click(object sender, EventArgs e)
        {
            var buttonName = ((Button)sender).Name;
            switch (buttonName)
            {
                case "btnGrocery":
                    GenerateMasterManifest("Grocery", "UK", dgvGrocery, "Grocery_VersionNumber");
                    break;
                case "btnOms":
                    GenerateMasterManifest("OMS", "UK", dgvOms, "OMS_VersionNumber");
                    break;
                case "btnAppStore":
                    GenerateMasterManifest("AppStore", "UK", dgvAppStore, "AppStore_VersionNumber");
                    break;
                case "btnTibco":
                    GenerateMasterManifest("Tibco", ddlTibcoRegions.SelectedItem.ToString(), dgvTibco, "TIBCO_VersionNumber");
                    break;
            }
        }

        private void dgvObject_DataError(object sender, EventArgs e)
        {
            //lblMessage.Text = "";
        }

        private void ddlTibcoRegions_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadComponentsList("Tibco", ddlTibcoRegions.SelectedItem.ToString(), dgvTibco);
        }

        private void ddlReleaseVerison_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;
            ManifestTabContainer_SelectedIndexChanged(sender, e);
        }
        #endregion

        #region Methods
        private void LoadComponentsList(string applicationName, string regionName, DataGridView dgvObject)
        {
            try
            {
                HideGridControls();
                lblMessage.Text = string.Empty;
                var descendantElementName = string.Empty;

                switch (applicationName)
                {
                    case "Grocery": descendantElementName = "Product"; break;
                    case "AppStore": descendantElementName = "Product"; break;
                    case "OMS": descendantElementName = "Product"; break;
                    case "Tibco": descendantElementName = "Service"; break;
                }

                //Based on Active tab template component names to be read
                XDocument regionTempPath = ReleaseManifest.GetRegionTemplate(applicationName, regionName);

                dgvObject.Visible = true;
                dgvObject.Rows.Clear();

                DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();
                cellStyle.SelectionBackColor = Color.Transparent;
                cellStyle.SelectionForeColor = dgvObject.DefaultCellStyle.ForeColor;
                dgvObject.DefaultCellStyle = cellStyle;
                dgvObject.DataError += new DataGridViewDataErrorEventHandler(dgvObject_DataError);
                foreach (var component in regionTempPath.Descendants(descendantElementName).Distinct())
                {
                    DataGridViewRow row = new DataGridViewRow();
                    var chkBoxCell = new MyDGVCheckBoxCell();
                    chkBoxCell.TrueValue = 1;
                    chkBoxCell.FalseValue = 0;
                    chkBoxCell.ReadOnly = false;                    
                    chkBoxCell.Tag = component.Attribute("Name").Value;
                    row.Cells.Add(chkBoxCell);
                    dgvObject.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = "Template Not Found";
            }
        }

        private void GenerateMasterManifest(string applicationName, string regionName, DataGridView dgvObject, string versionKeyName)
        {
            try
            {
                var newVersion = TfsCheckoutCheckin.CheckOutFromTFS(applicationName + "_" + VersionConfigFileName, versionKeyName,ddlReleaseVerison.SelectedItem.ToString());
                using (new Impersonator(outputPathUserName, outputPathDomainName, outputPathPassword))
                {
                    var xmlDocument = new XDocument();
                    var rootElement = new XElement("ReleaseManifest", new XAttribute("type", "MasterManifest"));
                    var componentManifestPath = ConfigurationManager.AppSettings["ComponentManifest"].ToString() + ddlReleaseVerison.SelectedItem.ToString() + @"\";
                    foreach (DataGridViewRow row in dgvObject.Rows)
                    {
                        var chkBoxCell = (MyDGVCheckBoxCell)row.Cells[0];
                        if (chkBoxCell.EditingCellFormattedValue.ToString() == "True")
                        {
                            DataGridViewComboBoxCell combobox = (DataGridViewComboBoxCell)row.Cells[1];
                            var selectedComboboxValue = ((DataGridViewComboBoxCell)row.Cells[1]).Value;
                            if (selectedComboboxValue != null)
                            {
                                rootElement.Add(new XElement("Component", new XAttribute("Path", componentManifestPath + selectedComboboxValue)));
                            }
                        }
                    }

                    xmlDocument.Add(rootElement);
                    string manifestOutputPath = ConfigurationManager.AppSettings["MasterManifest"].ToString() + ddlReleaseVerison.SelectedItem.ToString() + @"\";
                    string finalManifestname = "MasterManifest_IGHS_" + applicationName + "_" + regionName + "_" + newVersion + ".xml";
                    var finalFilePath = Path.Combine(manifestOutputPath, finalManifestname);
                    if (File.Exists(finalFilePath))
                        File.Delete(finalFilePath);
                    xmlDocument.Save(finalFilePath);
                }

                TfsCheckoutCheckin.CheckInToTFS(applicationName + "_" + VersionConfigFileName, ddlReleaseVerison.SelectedItem.ToString());
                lblMessage.Text = "Manifest Generated Successfully " + newVersion;
                lblMessage.ForeColor = Color.Green;
            }
            catch (Exception ex)
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = ex.Message;
                // ReleaseManifest.RevertSavedVersionNumber(versionKeyName);
                TfsCheckoutCheckin.UndoChangesToTFS(applicationName + "_" + VersionConfigFileName, ddlReleaseVerison.SelectedItem.ToString());
            }
        }

        private void LoadVersionDropdown(DataGridView dgvObject, int rowIndex, string applicationName, string regionName)
        {
            lblMessage.Text = string.Empty;
            if (dgvObject.CurrentCell.GetType().Name == "MyDGVCheckBoxCell")
            {
                var chkBoxCell = (MyDGVCheckBoxCell)dgvObject.CurrentCell;
                DataGridViewComboBoxCell combobox = (DataGridViewComboBoxCell)dgvObject.Rows[rowIndex].Cells[1];

                if (chkBoxCell.Value.Equals(1))
                {
                    using (new Impersonator(outputPathUserName, outputPathDomainName, outputPathPassword))
                    {
                        var componentManifestPath = ConfigurationManager.AppSettings["ComponentManifest"].ToString() + ddlReleaseVerison.SelectedItem.ToString() + @"\";
                        var files = new DirectoryInfo(componentManifestPath).GetFiles().Where(f => f.Name.Contains("ComponentManifest_IGHS_" + applicationName + "_" + regionName + "_" + chkBoxCell.Tag + "_"));
                        var versionNumber = new List<string>();
                        var dt = new DataTable("FileVersion");
                        dt.Columns.Add("Version");
                        dt.Columns.Add("FileName");
                        var selectedText = string.Empty;
                        int i = 0;
                        foreach (var file in files.Select(f => f.Name).OrderByDescending(f => f))
                        {
                            var fileVersion = file.ToString().Substring(file.LastIndexOf("_") + 1);
                            fileVersion = fileVersion.Substring(0, fileVersion.LastIndexOf("."));
                            DataRow row = dt.NewRow();
                            if (i == 0) selectedText = file;
                            row["Version"] = fileVersion;
                            row["FileName"] = file;
                            dt.Rows.Add(row);
                            i++;
                        }
                        if (dt.Rows.Count > 8)
                            combobox.MaxDropDownItems = dt.Rows.Count;
                                                
                        combobox.DataSource = dt;
                        combobox.DisplayMember = "Version";
                        combobox.ValueMember = "FileName";
                        combobox.Value = selectedText;                        
                    }
                }
                else
                {
                    combobox.DataSource = null;
                }
            }
        }

        private void HideGridControls()
        {
            dgvGrocery.Visible = false;
            dgvAppStore.Visible = false;
            dgvOms.Visible = false;
            dgvTibco.Visible = false;
        }
        #endregion       
    }

    public class MyDGVCheckBoxCell : DataGridViewCheckBoxCell
    {
        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates elementState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {

            // the base Paint implementation paints the check box

            base.Paint(graphics, clipBounds, cellBounds, rowIndex, elementState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);


            // now let's paint the text

            // Get the check box bounds: they are the content bounds

            Rectangle contentBounds = this.GetContentBounds(rowIndex);

            // Compute the location where we want to paint the string.

            Point stringLocation = new Point();

            // Compute the Y.

            // NOTE: the current logic does not take into account padding.

            stringLocation.Y = cellBounds.Y + 2;

            // Compute the X.

            // Content bounds are computed relative to the cell bounds

            // - not relative to the DataGridView control.

            stringLocation.X = cellBounds.X + contentBounds.Right + 2;

            // Paint the string.

            graphics.DrawString(this.Tag.ToString(), Control.DefaultFont, System.Drawing.Brushes.Black, stringLocation);

        }
    }
}
