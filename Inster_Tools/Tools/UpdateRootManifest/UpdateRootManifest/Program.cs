using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Globalization;
using System.IO;
using System.Net.Mail;
using System.Configuration;
using System.Xml.Xsl;

namespace UpdateRootManifest
{
    
    class Program
    {
        
        static void Main(string[] args)
        {           

            if (args[0] == "UpdateManifest")
            {
                if (args.Length != 4)
                {
                    Console.WriteLine("Operation ('UpdateManifest / NotifyDeployment '),Root Manifest File path and Component Manifest File path needed as arguments");
                    return;

                }
                Util.Component = args[3];
                Util.JenkinsURL = ConfigurationManager.AppSettings["JenkinsURL"];
                ContinuousDeploy cdploy = new ContinuousDeploy();                
                string ComponentManifestFileName = Util.UpdateComponentManifest(args[2], Util.Component);
                cdploy.UpdateRootManifest(args[1], args[2], ComponentManifestFileName);

            }


            else if (args[0] == "NotifyDeployment")
            {
                Util.NotifyDeployment();
            }


            else if (args[0] == "ScheduleBuild")
            {
                if (args.Length != 4)
                {
                    Console.WriteLine("Operation ('UpdateManifest / NotifyDeployment '),Root Manifest File path and Component Manifest File path needed as arguments");
                    return;
                }
                Util.Component = args[3];
               // Util.CompnentManifet = args[2];
                
                ScheduleDeploy sDeploy = new ScheduleDeploy();
               // string ComponentManifestFileName = Util.UpdateComponentManifest(args[2], Util.Component);

                string ComponentManifestFileName = args[2];
                ComponentManifestFileName = ComponentManifestFileName.Substring(ComponentManifestFileName.LastIndexOf(("\\"))+1);

                string ComponentNamestring=string.Empty;
                string[] strArray = ComponentManifestFileName.Split('_');
                
                foreach (string obj in strArray)
                {
                    if (obj == "AppStore")
                    {
                        
                    ComponentNamestring = obj;
                    }

                }


                if (ComponentNamestring.Contains("AppStore"))
                {
                    sDeploy.UpdateRootManifestSchedule(args[1], args[2], ComponentManifestFileName, args[3]);
                }
                else
                {
                    sDeploy.UpdateRootManifestSchedule(args[1], args[2], ComponentManifestFileName);
                }


              
            }

            else if (args[0] == "TriggerDeploy")
            {
                Util.JenkinsURL = ConfigurationManager.AppSettings["JenkinsURL"];
                TriggerDeploy triggerDeploy = new TriggerDeploy(args[1],args[2]);                
            }

        }      

    }
}
