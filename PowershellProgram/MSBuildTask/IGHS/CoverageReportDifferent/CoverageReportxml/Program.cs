using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Coverage.Analysis;
using System.Xml;
using System.Xml.Xsl;
using System.Text.RegularExpressions;
using System.Data;
using System.IO;


namespace CoverageReportxml
{
    class Program
    {
        static void Main(string[] args)
        {

            {
                string fullPath = args[0];
                string projectName;
                var dirInfo = new DirectoryInfo(fullPath).GetDirectories().OrderByDescending(d => d.LastWriteTimeUtc).First();
                projectName = dirInfo.ToString();

                string wspath = Environment.CurrentDirectory.ToString();

                Regex isString = new Regex("(['\"]?)[a-zA-Z]*\\1$");
               // CoverageInfo coverage = CoverageInfo.CreateFromFile(wspath + @"\" + args[0] + @"\in\" + Environment.MachineName + @"\" + "data.coverage");
               // CoverageInfo coverage = CoverageInfo.CreateFromFile(args[0] + @"\" + projectName + @"\in\" + Environment.MachineName + @"\" + "data.coverage");
                CoverageInfo coverage = CoverageInfo.CreateFromFile(args[0] + @"\" + projectName + @"\in\" + Environment.MachineName + @"\" + "data.coverage",
                                                                           new string[] { args[0] + @"\" + projectName + @"\out\" },
                                                                           new string[] { args[0] + @"\" + projectName + @"\out\" }
                                                                           );
                                                                          
                DataSet data = coverage.BuildDataSet(null);


                string xml = data.GetXml();
                string path;
                path = args[0].Substring(0, args[0].LastIndexOf(("\\")));
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(xml);
                xmlDocument.Save(path + @"\Report\coverage.xml");


                XslTransform myXslTransform = new XslTransform();
                
                

                XmlTextWriter writer = new XmlTextWriter(path + @"\Report\coverage.html", null);


                                      

     

                myXslTransform.Load(path + @"\style.xslt");
                myXslTransform.Transform(xmlDocument, null, writer);

                writer.Flush();
                writer.Close();
            
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

        public static string wspath { get; set; }
    }



    class HtmlDocument
    {

       


    }
}
