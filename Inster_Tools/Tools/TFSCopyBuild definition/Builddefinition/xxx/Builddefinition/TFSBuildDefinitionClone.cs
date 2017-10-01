using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


//Add this NameSpace to work on TFS api

using Microsoft.TeamFoundation.Build.Client;
using Microsoft.TeamFoundation.Client;
using System.Collections.Specialized;
using System.Configuration;
using System.Collections;

//Add this NameSpace to work on TFS api

using System.Reflection;

namespace Builddefinition
{
    public partial class TFSBuildDefinitionClone : Form
    {
        public string project;
        private string Toproject;
        private string buildname;
        private string agentpath;
        private string sourcecontrolpath;
        private string process;
        private string Old_Branch;
        private string New_Branch;
        private string Old_RouteTag;
        private string New_dRouteTag;
        private string Old_buidoutpath;
        private string New_buidoutpath;
        private string BuildDefName;
        private string Main;
        private string Droplocation;
        private string Buildcontroller;
        private string BuildNoFormatFrom;
        private string BuildNoFormatTo;

        private string Main_Old_RouteTag;
        private string Main_New_dRouteTag;
        IBuildDefinition buildDefinition;



        public TFSBuildDefinitionClone()
        {
            InitializeComponent();

        }

        private void TFSBuildDefinitionClone_Load(object sender, EventArgs e)
        {
            //this.BackColor = Color.FromName("green");

        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {


                // if (!checkBox1.Checked)
                //  {

                var server = TfsTeamProjectCollectionFactory.GetTeamProjectCollection(new Uri("http://tfsapp.dotcom.tesco.org/tfs/Grocery"));
                //IBuildServer buildServer = server.GetService<IBuildServer>();
                //Get all Build from ALM project
                // var buildDetails = buildServer.QueryBuildDefinitions("TescoAppStore");
                IBuildServer buildServer = server.GetService<IBuildServer>();

                //clone the parent branch build definition  

                project = comboBox1.SelectedItem.ToString();
                Old_Branch = comboBox2.SelectedItem.ToString();
                New_Branch = comboBox3.SelectedItem.ToString();
                Old_RouteTag = comboBox4.SelectedItem.ToString();
                New_dRouteTag = comboBox5.SelectedItem.ToString();
                BuildDefName = textBox1.Text;
                BuildNoFormatFrom = textBox3.Text;
                BuildNoFormatTo = textBox4.Text;

                var buildDetails = buildServer.QueryBuildDefinitions(project);
                Hashtable appSettings = (System.Configuration.ConfigurationManager.GetSection(project) as Hashtable);

                foreach (var build in buildDetails)
                {

                    if (appSettings.ContainsValue(build.Name))
                    {

                        //IBuildDefinition buildDefinition = buildServer.GetBuildDefinition("TescoAppStore", "R8.0_AppStore.Authentication.Library");

                        IBuildDefinition buildDefinition = buildServer.GetBuildDefinition(project, build.Name);

                        //buildDefinition.BuildServer.GetBuildAgent(new Uri("http://tfsapp.dotcom.tesco.org/tfs/Grocery"));
                        // buildDefinition.BuildServer.QueryBuildDefinitions("TescoAppStore");
                        var buildDefinitionClone = buildDefinition.BuildServer.CreateBuildDefinition(buildDefinition.TeamProject);

                        buildDefinitionClone.BuildController = buildDefinition.BuildController;
                        buildDefinitionClone.ContinuousIntegrationType = buildDefinition.ContinuousIntegrationType;
                        buildDefinitionClone.ContinuousIntegrationQuietPeriod = buildDefinition.ContinuousIntegrationQuietPeriod;
                        buildDefinitionClone.DefaultDropLocation = buildDefinition.DefaultDropLocation;
                        buildDefinitionClone.Description = buildDefinition.Description;
                        buildDefinitionClone.Enabled = buildDefinition.Enabled;
                        buildDefinitionClone.Name = String.Format("Copy of{0}", buildDefinition.Name);
                        buildDefinitionClone.Name = buildDefinitionClone.Name.Replace("Copy of", BuildDefName);
                        buildDefinitionClone.Process = buildDefinition.Process;


                        IProcessTemplate template = buildServer.QueryProcessTemplates(buildDefinition.TeamProject).FirstOrDefault(i => i.ServerPath == buildDefinitionClone.Process.ServerPath.Replace(Old_Branch, New_Branch));
                        if (template == null)
                        {
                            template = buildServer.CreateProcessTemplate(buildDefinition.Process.TeamProject, buildDefinitionClone.Process.ServerPath.Replace(Old_Branch, New_Branch));
                            template.SupportedReasons = buildDefinition.Process.SupportedReasons;
                            template.Description = buildDefinition.Process.Description;
                            template.TemplateType = buildDefinition.Process.TemplateType;
                            template.Save();
                            object object1 = Activator.CreateInstance(template.GetType(), true);
                            FieldInfo fieldInfo = object1.GetType().GetField("m_serverPath", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetField | BindingFlags.DeclaredOnly | BindingFlags.Default | BindingFlags.ExactBinding | BindingFlags.FlattenHierarchy | BindingFlags.GetField | BindingFlags.GetProperty | BindingFlags.IgnoreCase |
                                  BindingFlags.IgnoreReturn | BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.OptionalParamBinding |
                                    BindingFlags.Public | BindingFlags.PutDispProperty | BindingFlags.PutRefDispProperty | BindingFlags.SetField | BindingFlags.SetProperty | BindingFlags.Static | BindingFlags.SuppressChangeType);
                            fieldInfo.SetValue(buildDefinitionClone.Process, buildDefinitionClone.Process.ServerPath.Replace(Old_Branch, New_Branch));
                        }

                        buildDefinitionClone.Process = template;
                        buildDefinitionClone.ProcessParameters = buildDefinition.ProcessParameters;
                        //buildDefinitionClone.ProcessParameters = buildDefinitionClone.Process.ServerPath.Replace("Release8.0", "Release9.0");
                        buildDefinitionClone.ProcessParameters = buildDefinition.ProcessParameters;
                        //buildDefinitionClone.ProcessParameters = buildDefinitionClone.ProcessParameters.Replace("Release8.0", "Release9.0");
                        buildDefinitionClone.ProcessParameters = buildDefinitionClone.ProcessParameters.Replace(Old_Branch, New_Branch);
                        buildDefinitionClone.ProcessParameters = buildDefinitionClone.ProcessParameters.Replace(Old_RouteTag, New_dRouteTag);

                        if (project!= "TescoAppstore")
                        {
                            if (BuildNoFormatFrom != null && BuildNoFormatTo != null)
                            {
                                buildDefinitionClone.ProcessParameters = buildDefinitionClone.ProcessParameters.Replace(BuildNoFormatFrom, BuildNoFormatTo);
                            }
                        }
                        foreach (var schedule in buildDefinition.Schedules)
                        {
                            var newSchedule = buildDefinitionClone.AddSchedule();
                            newSchedule.DaysToBuild = schedule.DaysToBuild;
                            newSchedule.StartTime = schedule.StartTime;
                            newSchedule.TimeZone = schedule.TimeZone;
                        }

                        foreach (var mapping in buildDefinition.Workspace.Mappings)
                        {
                            if (mapping.LocalItem != null)
                            {
                                agentpath = mapping.LocalItem.ToString();
                                //agentpath = agentpath.Replace("Release8.0", "Release9.0");

                                agentpath = agentpath.Replace(Old_Branch, New_Branch);
                            }
                            sourcecontrolpath = mapping.ServerItem.ToString();
                            //sourcecontrolpath = sourcecontrolpath.Replace("Release8.0", "Release9.0");
                            sourcecontrolpath = sourcecontrolpath.Replace(Old_Branch, New_Branch);
                            //buildDefinitionClone.Workspace.AddMapping(mapping.ServerItem, mapping.LocalItem, mapping.MappingType, mapping.Depth);
                            buildDefinitionClone.Workspace.AddMapping(sourcecontrolpath, agentpath, mapping.MappingType, mapping.Depth);
                        }

                        buildDefinitionClone.RetentionPolicyList.Clear();

                        foreach (var policy in buildDefinition.RetentionPolicyList)
                        {
                            buildDefinitionClone.AddRetentionPolicy(policy.BuildReason, policy.BuildStatus, policy.NumberToKeep, policy.DeleteOptions);
                        }

                        // buildDefinitionClone.Process.Save();
                        buildDefinitionClone.Save();


                        label2.Text = "Suceesully Build Definiton Created";
                        label2.ForeColor = Color.Green;
                        label2.Font = new Font(label2.Font, FontStyle.Bold);

                    } //end if loop

                } //end for each loop
                //} //check box if condition
                // else
                //  { 

                // }
            } //try loop

            catch (Exception exception)
            {
                label2.Text = exception.Message.ToString();
                label2.ForeColor = Color.Red;
                label2.Font = new Font(label2.Font, FontStyle.Bold);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                BuildNoFormatFrom = textBox5.Text;
                BuildNoFormatTo = textBox6.Text;
                Droplocation = @"\\uktee01-clusdb\IGHSBuildOutput";
                // if (!checkBox1.Checked)
                //  {

                var server = TfsTeamProjectCollectionFactory.GetTeamProjectCollection(new Uri("http://tfsapp.dotcom.tesco.org/tfs/Grocery"));
                //IBuildServer buildServer = server.GetService<IBuildServer>();
                //Get all Build from ALM project
                // var buildDetails = buildServer.QueryBuildDefinitions("TescoAppStore");
                IBuildServer buildServer = server.GetService<IBuildServer>();

                //clone the parent branch build definition  

                project = comboBox8.SelectedItem.ToString();
                Main_Old_RouteTag = comboBox6.SelectedItem.ToString();
                Main_New_dRouteTag = comboBox7.SelectedItem.ToString();
                New_buidoutpath = @"\\uktee01-clusdb\IGHSBuildOutput\IGHS_Manifest";
                Old_buidoutpath = @"\\uktee01-clusdb\BuildOutput\IGHS_Manifest";
                var buildDetails = buildServer.QueryBuildDefinitions(project);
                Hashtable appSettings = (System.Configuration.ConfigurationManager.GetSection(project) as Hashtable);
                

                foreach (var build in buildDetails)
                {

                    if (appSettings.ContainsValue(build.Name))
                    {


                        IBuildDefinition buildDefinition = buildServer.GetBuildDefinition(project, build.Name);
                        var G = buildDefinition.DefaultDropLocation;

                        buildDefinition.DefaultDropLocation = G.Replace(G, @Droplocation);
                        buildDefinition.Process = buildDefinition.Process;

                        if (project != "TescoAppstore" && project != "InternationalAdministration")
                        {
                            if (BuildNoFormatFrom != null && BuildNoFormatTo != null)
                            {
                                buildDefinition.ProcessParameters = buildDefinition.ProcessParameters.Replace(BuildNoFormatFrom, BuildNoFormatTo);
                            }
                        }

                        buildDefinition.ProcessParameters = buildDefinition.ProcessParameters.Replace(Main_Old_RouteTag, Main_New_dRouteTag);
                        buildDefinition.ProcessParameters = buildDefinition.ProcessParameters.Replace(Old_buidoutpath, New_buidoutpath);


                        buildDefinition.Save();

                        label11.Text = "Suceesully updated Route to live property";
                        label11.ForeColor = Color.Green;
                        label11.Font = new Font(label11.Font, FontStyle.Bold);


                    } //end if loop

                } //end for each loop
                //} //check box if condition
                // else
                //  { 

                // }
            } //try loop

            catch (Exception exception)
            {
                label11.Text = exception.Message.ToString();
                label11.ForeColor = Color.Red;
                label11.Font = new Font(label11.Font, FontStyle.Bold);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            try
            {
                //Connect to your TFS
                var server = TfsTeamProjectCollectionFactory.GetTeamProjectCollection(new Uri("http://tfsapp.dotcom.tesco.org/tfs/Grocery"));
                IBuildServer buildServer = server.GetService<IBuildServer>();

                project = comboBox9.SelectedItem.ToString();
                Toproject = comboBox12.SelectedItem.ToString();
                Old_Branch = comboBox10.SelectedItem.ToString();
                New_Branch = comboBox11.SelectedItem.ToString();
                Old_RouteTag = comboBox13.SelectedItem.ToString();
                New_dRouteTag = comboBox14.SelectedItem.ToString();
                BuildDefName = textBox2.Text;
                Droplocation = comboBox15.SelectedItem.ToString();
                Buildcontroller = comboBox16.SelectedItem.ToString();

                var buildDetails = buildServer.QueryBuildDefinitions(project);

                Hashtable appSettings = (System.Configuration.ConfigurationManager.GetSection(project) as Hashtable);

                foreach (var build in buildDetails)
                {

                    if (appSettings.ContainsValue(build.Name))
                    {
                        IBuildDefinition buildDefinition = buildServer.GetBuildDefinition(project, build.Name);

                        var buildDefinitionClone = buildServer.CreateBuildDefinition(Toproject);
                        buildDefinitionClone.Name = "Copy of " + build.Name;
                        buildDefinitionClone.Name = buildDefinitionClone.Name.Replace("Copy", BuildDefName);

                        
                        for (int i = 0; i < buildServer.QueryBuildControllers().Length; i++)
                        {
                            var a = buildServer.QueryBuildControllers()[i].Name;
                            if (Buildcontroller == a)
                            {
                                buildDefinitionClone.BuildController = buildServer.QueryBuildControllers()[i];
                            }
                        }

                        // buildDefinitionClone.BuildController = buildServer.QueryBuildControllers()[25];
                        //buildDefinitionClone.BuildController = build.BuildController;
                        buildDefinitionClone.Process = buildServer.QueryProcessTemplates(Toproject)[3];
                        buildDefinitionClone.ContinuousIntegrationType = buildDefinition.ContinuousIntegrationType;
                        buildDefinitionClone.ContinuousIntegrationQuietPeriod = buildDefinition.ContinuousIntegrationQuietPeriod;
                        var b = buildDefinition.DefaultDropLocation;
                        
                        buildDefinitionClone.DefaultDropLocation = b.Replace(b, @Droplocation);
                        buildDefinitionClone.Description = buildDefinition.Description;
                        buildDefinitionClone.Enabled = buildDefinition.Enabled;
                        buildDefinitionClone.Process = buildServer.QueryProcessTemplates(Toproject)[3];
                        buildDefinitionClone.ProcessParameters = buildDefinition.ProcessParameters;
                        buildDefinitionClone.ProcessParameters = buildDefinition.ProcessParameters;
                        buildDefinitionClone.ProcessParameters = buildDefinitionClone.ProcessParameters.Replace(Old_Branch, New_Branch);
                        buildDefinitionClone.ProcessParameters = buildDefinitionClone.ProcessParameters.Replace(Old_RouteTag, New_dRouteTag);
                        //buildDefinitionClone.ProcessParameters = buildDefinitionClone.ProcessParameters.Replace("11.0", "11.0");
                        buildDefinitionClone.ProcessParameters = buildDefinitionClone.ProcessParameters.Replace(project, Toproject);
                        //var c = @"\\UKDEV71Macnf01v\BuildOutput\Orderfulfilment";
                        //var d = @"\\UKTEE01-CLUSDB\BuildOutput\IGHS_Manifest\ComponentDelivery";
                        //buildDefinitionClone.ProcessParameters = buildDefinitionClone.ProcessParameters.Replace(@d, @c);
                        //buildDefinitionClone.ProcessParameters = buildDefinitionClone.ProcessParameters.Replace("172.25.59.66", "ukdev71macnf01v");

                        foreach (var schedule in buildDefinition.Schedules)
                        {
                            var newSchedule = buildDefinitionClone.AddSchedule();
                            newSchedule.DaysToBuild = schedule.DaysToBuild;
                            newSchedule.StartTime = schedule.StartTime;
                            newSchedule.TimeZone = schedule.TimeZone;
                        }

                        foreach (var mapping in buildDefinition.Workspace.Mappings)
                        {
                            agentpath = mapping.LocalItem.ToString();
                            agentpath = agentpath.Replace(Old_Branch, New_Branch);
                            sourcecontrolpath = mapping.ServerItem.ToString();
                            sourcecontrolpath = sourcecontrolpath.Replace(Old_Branch, New_Branch);
                            sourcecontrolpath = sourcecontrolpath.Replace(project, Toproject);
                            buildDefinitionClone.Workspace.AddMapping(sourcecontrolpath, agentpath, mapping.MappingType, mapping.Depth);
                        }
                        buildDefinitionClone.RetentionPolicyList.Clear();

                        foreach (var policy in buildDefinition.RetentionPolicyList)
                        {
                            buildDefinitionClone.AddRetentionPolicy(policy.BuildReason, policy.BuildStatus, policy.NumberToKeep, policy.DeleteOptions);
                        }

                        buildDefinitionClone.Save();

                        label19.Text = "Suceesully updated Route to live property";
                        label19.ForeColor = Color.Green;
                        label19.Font = new Font(label11.Font, FontStyle.Bold);
                    }//end of if block
                }//end of for each loop

            }//end of try
            catch (Exception exception)
            {
                label19.Text = exception.Message.ToString();
                label19.ForeColor = Color.Red;
                label19.Font = new Font(label11.Font, FontStyle.Bold);

            }//end of catc
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBox1.SelectedItem.ToString().Trim() == "TescoAppstore")
            {
                textBox3.Enabled = false;
                textBox4.Enabled = false;
                

            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {
            //TabPage page = tabPage1.TabPages[e.Index];

            //e.Graphics.FillRectangle(new SolidBrush(page.BackColor), e.Bounds);

            //Rectangle paddedBounds = e.Bounds;
            //int yOffset = (e.State == DrawItemState.Selected) ? -2 : 1;
            //paddedBounds.Offset(1, yOffset);
            //TextRenderer.DrawText(e.Graphics, page.Text, this.Font, paddedBounds, page.ForeColor);
        }

        private void comboBox8_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox8.SelectedItem.ToString().Trim() == "TescoAppstore") 
            {
                textBox5.Enabled = false;
                textBox6.Enabled = false;

            }
            if (comboBox8.SelectedItem.ToString().Trim() == "InternationalAdministration")
            {
                textBox5.Enabled = false;
                textBox6.Enabled = false;

            }
        }


    }
}
