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
using System.Collections;
using System.Configuration;
using System.Reflection;
using System.Xml.Linq;
using System.Xml.XPath;

namespace SummaryReport
{
    class Program
    {
        static void Main(string[] args)
        {
            string currentAssemblyDirectoryName;
            currentAssemblyDirectoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            XDocument appSettingsDoc;

            appSettingsDoc = XDocument.Load(currentAssemblyDirectoryName + @"\SummaryApp.config");
            var appSettings = appSettingsDoc.Element("configuration").Element("appSettings").Elements("add").Attributes("value");

            List<string> path = new List<string>();
            foreach (var item in appSettings)
            {
                //test.Add("*_" + item.Value + "_*" + "_" + X[3].ToString());

                //args[0] + @"\" + item + @"\TestResults";

                path.Add(args[0] + @"\" + item.Value.ToString() + @"\TestResults");
                                   

            }

            List<string> files = new List<string>(path);

                  
            try
            {
                using (CoverageInfo info = JoinCoverageFiles(files))
                {
                    // Dump out the info like you would if it weren't joined

                    //xml and hml start

                    DataSet data = info.BuildDataSet(null);


                    string xml = data.GetXml();
                    string locationpath;
                    locationpath = @"D:\MSBuildTask\CodeCoverageReport\SummaryReport\SummaryReport\SummaryReport\bin";
                    //path = args[0].Substring(0, args[0].LastIndexOf(("\\")));
                    XmlDocument xmlDocument = new XmlDocument();
                    xmlDocument.LoadXml(xml);
                    xmlDocument.Save(locationpath + @"\Report\coverage.xml");


                    XslTransform myXslTransform = new XslTransform();



                    XmlTextWriter writer = new XmlTextWriter(locationpath + @"\Report\coverage.html", null);






                    myXslTransform.Load(locationpath + @"\style.xslt");
                    myXslTransform.Transform(xmlDocument, null, writer);

                    writer.Flush();
                    writer.Close();



                    //xml and html end



                    //Dump
                    
                }
            }
            catch (CoverageAnalysisException ex)
            {
                Console.WriteLine(ex);
            }


           
        }

        //Coverage Summary Report Start

        static CoverageInfo JoinCoverageFiles(IEnumerable<string> files)
        {
            if (files == null)
                throw new ArgumentNullException("files");

            // This will represent the joined coverage files
            CoverageInfo result = null;

            try
            {
                foreach (string file in files)
                {
                    // Create from the current file
                   // CoverageInfo current = CoverageInfo.CreateFromFile(file);



                    string fullPath = file;
                    string projectName;
                    var dirInfo = new DirectoryInfo(fullPath).GetDirectories().OrderByDescending(d => d.LastWriteTimeUtc).First();
                    projectName = dirInfo.ToString();

                    string wspath = Environment.CurrentDirectory.ToString();

                    Regex isString = new Regex("(['\"]?)[a-zA-Z]*\\1$");
                    //CoverageInfo coverage = CoverageInfo.CreateFromFile(wspath + @"\" + args[0] + @"\in\" + Environment.MachineName + @"\" + "data.coverage");
                    CoverageInfo current = CoverageInfo.CreateFromFile(file + @"\" + projectName + @"\in\" + Environment.MachineName + @"\" + "data.coverage",
                                                                               new string[] { file + @"\" + projectName + @"\out\" },
                                                                               new string[] { file + @"\" + projectName + @"\out\" });






                    if (result == null)
                    {
                        // First time through, assign to result
                        result = current;
                        continue;
                    }

                    // Not the first time through, join the result with the current
                    CoverageInfo joined = null;
                    try
                    {
                        joined = CoverageInfo.Join(result, current);
                    }
                    finally
                    {
                        // Dispose current and result
                        current.Dispose();
                        current = null;
                        result.Dispose();
                        result = null;
                    }

                    result = joined;
                }
            }
            catch (Exception)
            {
                if (result != null)
                {
                    result.Dispose();
                }
                throw;
            }

            return result;
       }

        static void summaryreportfile(CoverageInfo result)
        {

           
        }
    }
   
}
