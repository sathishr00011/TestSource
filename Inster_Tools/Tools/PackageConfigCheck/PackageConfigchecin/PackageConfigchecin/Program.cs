using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PackageConfigchecin
{
    class Program
    {
        static void Main(string[] args)
        {

            if (args[0] == "Packageconfig")
            {
                if (args.Length != 3)
                {
                    Console.WriteLine("Pakcage confige file version available");
                    return;

                }
                 PackageUtility sPackage = new PackageUtility();
                 sPackage.UpdateTemplatefile(args[1], args[2]);
                //Util.Component = args[3];
                //Util.JenkinsURL = ConfigurationManager.AppSettings["JenkinsURL"];
                //ContinuousDeploy cdploy = new ContinuousDeploy();
                //string ComponentManifestFileName = Util.UpdateComponentManifest(args[2], Util.Component);
               // cdploy.UpdateRootManifest(args[1], args[2], ComponentManifestFileName);

            }
        }
    }
}
