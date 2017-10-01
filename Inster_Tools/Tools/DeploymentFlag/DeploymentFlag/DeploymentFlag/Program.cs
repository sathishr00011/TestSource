using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeploymentFlag
{
    class Program
    {
        static void Main(string[] args)
        {

            if (args[0] == "DeploymentFlag")
            {
                if (args.Length != 2)
                {
                    Console.WriteLine("DeploymentFlag file version available");
                    return;

                }
                DeploymentStatusCheck sdeployment  = new DeploymentStatusCheck();

                sdeployment.GetTemplatefile(args[1]);
                //Util.Component = args[3];
                //Util.JenkinsURL = ConfigurationManager.AppSettings["JenkinsURL"];
                //ContinuousDeploy cdploy = new ContinuousDeploy();
                //string ComponentManifestFileName = Util.UpdateComponentManifest(args[2], Util.Component);
               // cdploy.UpdateRootManifest(args[1], args[2], ComponentManifestFileName);

            }
        }
    }
}
