using System;

using Manifest.Contracts;

namespace GenerateManifest
{
    public class Program
    {
        /// <summary>
        /// Mains the specified args.
        /// </summary>
        /// <param name="args">The args.</param>
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                args = new string[6];
                args[0] = @"\Version:""4.0.0.TEST""";
                args[1] = @"\regions:""Hawk""";
                args[2] = @"\template=""" + ManifestType.Tibco.ToString() + @"""";
                args[3] = @"\output:""\\UKTEE01-CLUSDB\BuildOutput\IGHS_Manifest\ManifestAutomation""";
                args[4] = @"\tag:""R8.0""";
                args[5] = @"searchDirectoryPath:""\\UKTEE01-CLUSDB\BuildOutput\IGHS_Manifest\ManifestAutomation\TibcoErrorHandling""";
            }  

            InvokeManifestWorkflow iwf = new InvokeManifestWorkflow(args);           
        }
    }
}
