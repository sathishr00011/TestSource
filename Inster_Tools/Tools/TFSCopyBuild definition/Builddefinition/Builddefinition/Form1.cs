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

namespace Builddefinition
{
    public partial class Form1 : Form
    {
        
       private string project;
       private string buildname;
       private string agentpath;
       private string sourcecontrolpath;
       private string process;
       private string Old_Branch;
       private string New_Branch;
       private string Old_RouteTag;
       private string New_dRouteTag;
       private string BuildDefName;
       private string Main;



       public Form1()
        {
            InitializeComponent();
           
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {


            //if (TescoAppStore.Checked)
            //{
            //    Project = TescoAppStore.Text;
            //    MessageBox.Show(Project);

            //}
            //else
            //{
            //    MessageBox.Show(TescoAppStore.Checked.ToString());
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    //Connect to your TFS
            //    var server = TfsTeamProjectCollectionFactory.GetTeamProjectCollection(new Uri("http://tfsapp.dotcom.tesco.org/tfs/Grocery"));
            //    IBuildServer buildServer = server.GetService<IBuildServer>();
            //    //Get all Build from ALM project
            //    //var buildDetails = buildServer.QueryBuildDefinitions(comboBox1.SelectedText);
            //    project = comboBox1.SelectedItem.ToString();
            //    if (!string.IsNullOrEmpty(project))
            //    {
            //        var buildDetails = buildServer.QueryBuildDefinitions(project);
            //        Hashtable appSettings = (System.Configuration.ConfigurationManager.GetSection(project) as Hashtable);
            //        foreach (var build in buildDetails)
            //        {

            //            if (appSettings.ContainsValue(build.Name))
            //            {
            //                var buildDefinition = buildServer.CreateBuildDefinition(project);



            //                buildDefinition.Name = "Copy of " + build.Name;
            //                buildDefinition.BuildController = build.BuildController;
            //                // This finds the template to use
            //                buildDefinition.Process = buildServer.QueryProcessTemplates(project)[0];
            //                buildDefinition.ProcessParameters = build.ProcessParameters;
            //                buildDefinition.Save();
            //                label2.Text = "Suceesully Build Definiton Created";
            //                label2.ForeColor = Color.Green;
            //                label2.Font = new Font(label2.Font, FontStyle.Bold);



            //            }//end of if block

            //            /*======================================================*/




            //            }//end of for each loop

            //    }
            //}//end of try
            //catch (Exception exception)
            //{
            //    label2.Text = exception.Message.ToString();
            //    label2.ForeColor = Color.Red;
            //    label2.Font = new Font(label2.Font, FontStyle.Bold);

            //}//end of catch

            // ==================================================================
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

                            buildDefinitionClone.ProcessParameters = buildDefinition.ProcessParameters;
                            //buildDefinitionClone.ProcessParameters = buildDefinitionClone.Process.ServerPath.Replace("Release8.0", "Release9.0");

                            //buildDefinitionClone.ProcessParameters = buildDefinitionClone.ProcessParameters.Replace("Release8.0", "Release9.0");
                            buildDefinitionClone.ProcessParameters = buildDefinitionClone.ProcessParameters.Replace(Old_Branch, New_Branch);
                            buildDefinitionClone.ProcessParameters = buildDefinitionClone.ProcessParameters.Replace(Old_RouteTag, New_dRouteTag);

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
                                //agentpath = agentpath.Replace("Release8.0", "Release9.0");
                                agentpath = agentpath.Replace(Old_Branch, New_Branch);
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

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
   
}
