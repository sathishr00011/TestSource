using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Coverage.Analysis;

namespace CoverageReportxml
{
    class Program
    {
        static void Main(string[] args)
        {

            if (args == null || args.Length != 3 || args[0] == null || args[1] == null || args[2] == null ||
                string.IsNullOrEmpty(args[0]) || string.IsNullOrEmpty(args[1]) || string.IsNullOrEmpty(args[2]))
            {
                help();
                return;
            }
            string coverageFileName = args[0];
            string dllFileName = args[1];
            string coverageXmlFileName = args[2];
            Console.WriteLine("args[0] = " + coverageFileName);
            Console.WriteLine("args[1] = " + dllFileName);
            Console.WriteLine("args[2] = " + coverageXmlFileName);

            using (CoverageInfo info = CoverageInfo.CreateFromFile(coverageFileName, new string[] { dllFileName }, new string[] { }))
            {
                CoverageDS data = info.BuildDataSet();

                data.WriteXml(coverageXmlFileName);
            }
        }
        static private void help()
        {
            Console.WriteLine("Help!");
            Console.Write(@"
                Arguments
                    Args[0]: data coverage file name (data.coverage)
                    Args[1]: DLL to use (myProject.dll)
                    Args[2]: coveragexml file name to be generated (converted.coveragexml)
                 ");

        }
    }


}
