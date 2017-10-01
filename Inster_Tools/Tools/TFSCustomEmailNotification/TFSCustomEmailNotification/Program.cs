using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.TeamFoundation.Client;
using System.Collections.ObjectModel;
using Microsoft.TeamFoundation.Framework.Client;
using Microsoft.TeamFoundation.Framework.Common;
using Microsoft.TeamFoundation.Build.Client;
using Microsoft.TeamFoundation.VersionControl.Client;
using Microsoft.TeamFoundation.TestManagement.Client;
using System.Net.Mail;
using System.Configuration;
using System.Xml.Linq;
using System.IO;
using System.Xml;

namespace TFSCustomEmailNotification
{
    class Program
    {
        static bool isSendEmail = false;
        static bool isCoverageAboveTheBar = true;
        static bool isTestPassRateAboveTheBar = true;
        static bool isFxcopSuccess = true;

        static string teamProjcet;
        static string buildDefinition;
        static string toEmail;
        static int requiredCoverage;
        static int requiredTestPassrate;
        static string fxcopViolationFile;
        static string stylecopFilePath = string.Empty;

        static void Main(String[] args)
        {
            try
            {
                AssignArgs(args);

            }
            catch (ArgumentException ex)
            {
                return;
            }

            Uri tfsUri = new Uri(ConfigurationManager.AppSettings["Uri"]);
            TfsTeamProjectCollection server;
            IBuildServer buildServer;

            server = new TfsTeamProjectCollection(tfsUri);
            server.EnsureAuthenticated();
            buildServer = (IBuildServer)server.GetService(typeof(IBuildServer));

            IBuildDetail[] nightlyBuilds = buildServer.QueryBuilds(teamProjcet, buildDefinition);
            IBuildDetail mostRecent = nightlyBuilds[nightlyBuilds.GetUpperBound(0)];
            TestManagementService tcm;
            string bdy;
            GetTestResults(server, mostRecent, out tcm, out bdy);
            bdy = GetCoverage(mostRecent, tcm, bdy);
            bdy = DoFxcopValidation(fxcopViolationFile, bdy);
            DoStyleCopValidation(mostRecent, server);
            isSendEmail = bool.Parse(ConfigurationManager.AppSettings["SendNotification"]);
            if (isSendEmail)
            {                
                SendEmail(mostRecent, bdy);
                Console.WriteLine("Mail sent");
            }
            if (!isCoverageAboveTheBar)
            {
                Environment.ExitCode = 1;
            }
            if (!isTestPassRateAboveTheBar)
            {
                Environment.ExitCode = 1;
            }

            if (!isFxcopSuccess)
            {
                Environment.ExitCode = 1;
            }
            Console.WriteLine("Above required Coverage :" + isCoverageAboveTheBar);

        }

        private static void AssignArgs(String[] args)
        {
            if (args.Length < 6)
            {
                System.Console.WriteLine("Please pass the arguments for Team Project, Build Definition Name, EmailTo List, Required Coverage Required Test pass rate,  and FxCopViolationFilePath.");
                throw new ArgumentException();
            }

            teamProjcet = args[0];
            buildDefinition = args[1];
            toEmail = args[2];
            requiredCoverage = Convert.ToInt16(args[3]);
            requiredTestPassrate = Convert.ToInt16(args[4]);
            fxcopViolationFile = args[5];
            if (args.Length == 7)
            {
                stylecopFilePath = args[6];
            }
        }

        #region Test Result extraction

        private static void GetTestResults(TfsTeamProjectCollection server, IBuildDetail mostRecent, out TestManagementService tcm, out string bdy)
        {


            tcm = (TestManagementService)server.GetService(typeof(TestManagementService));
            IEnumerable<ITestRun> allTestRuns =
    (IEnumerable<ITestRun>)(tcm.GetTeamProject(teamProjcet).TestRuns.ByBuild(mostRecent.Uri));

            if (allTestRuns.Count() > 0)
            {
                isSendEmail = true;
            }

            bdy = "<b><u>Test result</u></b><br>";

            int totalTest = 0;
            int TestPassed = 0;

            foreach (ITestRun testRun in allTestRuns)
            {

                ITestCaseResultCollection testResults = testRun.QueryResults();
                bdy += "<br><table border=1><font face='Garamond' color=blue><tr><th>TestCase</th><th>Result</th><th>Error Description</th></tr>";
                foreach (ITestCaseResult testResult in testResults)
                {
                    totalTest++;
                    if (testResult.Outcome != TestOutcome.Passed)
                    {
                        bdy += "<tr  bgcolor=red><td>";
                    }
                    else
                    {
                        TestPassed++;
                        bdy += "<tr><td>";
                    }

                    bdy += testResult.TestCaseTitle + "</td><td>" + testResult.Outcome + "</td><td>" + testResult.ErrorMessage + "</td></tr>";
                }


                double actualPassrate;
                actualPassrate = (double)TestPassed / totalTest;
                if (requiredTestPassrate > actualPassrate * 100)
                {
                    isTestPassRateAboveTheBar = false;
                }
                bdy += "</font></table>";
            }
        }

        #endregion

        #region Coverage Result extraction
        private static string GetCoverage(IBuildDetail mostRecent, TestManagementService tcm, string bdy)
        {
            var coverageAnalysisManager = tcm.GetTeamProject(teamProjcet).CoverageAnalysisManager;

            var queryBuildCoverage = coverageAnalysisManager.QueryBuildCoverage(mostRecent.Uri.ToString(), CoverageQueryFlags.Modules);

            foreach (var coverage in queryBuildCoverage)
            {
                bdy += "<p><b><u>Coverage Report</u></b></p>";
                bdy += "<table border=1 style='border:5px solid #000;'><font face='Garamond' color=blue><tr><th>Module</th><th>Blocks Covered</th><th>Blocks Not Covered</th><th>Covered Percentage</th></tr>";
                foreach (var moduleInfo in coverage.Modules)
                {
                    var a1 = moduleInfo.Name;
                    var a2 = moduleInfo.Statistics.BlocksCovered;
                    var a3 = moduleInfo.Statistics.BlocksNotCovered;
                    var a4 = Convert.ToDecimal(moduleInfo.Statistics.BlocksCovered) + Convert.ToDecimal(moduleInfo.Statistics.BlocksNotCovered);
                    a4 = Convert.ToDecimal(moduleInfo.Statistics.BlocksNotCovered) / Convert.ToDecimal(a4);
                    a4 = 100 - (a4 * 100);
                    a4 = decimal.Round(a4, 2);
                    bdy += "<tr><td>" + a1 + "</td><td>" + a2 + "</td><td>" + a3 + "</td><td>" + a4 + "</td></tr>";
                    if (a4 < requiredCoverage)
                    {
                        isCoverageAboveTheBar = false;
                    }
                }
                bdy += "</font></table>";
            }
            return bdy;
        }

        #endregion

        #region FxCop Validation

        private static string DoFxcopValidation(string fxcopOutputFile, string bdy)
        {

            if (!File.Exists(fxcopOutputFile))
            {
                return bdy;
            }

            XDocument xDoc = XDocument.Load(fxcopOutputFile);
            var nameAttributes2 = from el in xDoc.Descendants("Issue")
                                  join fc in ConfigurationManager.AppSettings["FxCopFailCondition"].Split(new char[] { ',' })
                                 on (string)el.Attribute("Level") equals (string)fc
                                  select el;
            isFxcopSuccess = (nameAttributes2.Count() == 0);

            bdy += "<p><b><u>FxCop Violation  Report</u></b></p>";
            bdy += "<table border=1 style='border:5px solid #000;'><font face='Garamond' color=blue><tr><th>Total Violations found" + nameAttributes2.Count() + " </th></tr></table>";
            return bdy;
        }

        #endregion

        #region StyleCop Validation

        private static void DoStyleCopValidation(IBuildDetail mostRecent, TfsTeamProjectCollection tpc)
        {

            if (!File.Exists(stylecopFilePath))
            {
                return;

            }



            {
                string _changeSetId = string.Empty;

                IBuildInformation buildInfo = mostRecent.Information;

                foreach (IBuildInformationNode nde in buildInfo.Nodes)
                {
                    if (nde.Type == "AssociatedChangeset")
                    {

                        Dictionary<string, string> dicBuildInfo = nde.Fields;
                        {
                            if (dicBuildInfo.ContainsKey("ChangesetId"))
                            {
                                //isSendEmail = true;
                                _changeSetId = dicBuildInfo["ChangesetId"];

                            }


                        }

                        VersionControlServer vcs = tpc.GetService<VersionControlServer>();
                        var changeset = vcs.GetChangeset(Convert.ToInt32(_changeSetId));

                        string dir = System.IO.Path.GetTempPath() + "Stylecop\\";
                        string stylecopReportFile = dir + "report.xml";
                        string stylecopReportViolationFile = dir + "report.violations.xml";
                        if (Directory.Exists(dir))
                            Array.ForEach(Directory.GetFiles(dir), File.Delete);

                        foreach (var change in changeset.Changes)
                        {
                            change.Item.DownloadFile(dir + change.Item.ServerItem.Split('/')[change.Item.ServerItem.Split('/').Length - 1]);
                        }

                        string stylecopArgs = string.Format("-d {0} -of {1}", dir, stylecopReportFile);
                        System.Diagnostics.Process stylecopProcess;
                        System.Threading.Thread.Sleep(3000);
                        stylecopProcess = System.Diagnostics.Process.Start(stylecopFilePath, stylecopArgs);
                        while (stylecopProcess.HasExited == false)
                        {
                            System.Threading.Thread.Sleep(250);
                        }

                        if (File.Exists(stylecopReportViolationFile))
                        {
                            Console.WriteLine(PrintXML(stylecopReportViolationFile));

                        }


                    }
                }



            }

            return;
        }
        #endregion

        private static void SendEmail(IBuildDetail mostRecent, string bdy)
        {
            SmtpClient client = new SmtpClient(ConfigurationManager.AppSettings["SMTPServer"]);
            MailMessage message = new MailMessage(ConfigurationManager.AppSettings["FromAddress"], toEmail, mostRecent.BuildNumber + " Test Results", bdy);
            message.IsBodyHtml = true;
            client.Send(message);

        }

        private static void GetChangeSetDetails(IBuildDetail mostRecent, TfsTeamProjectCollection tpc)
        {
            string _changeSetId = string.Empty;

            IBuildInformation buildInfo = mostRecent.Information;

            foreach (IBuildInformationNode nde in buildInfo.Nodes)
            {
                if (nde.Type == "AssociatedChangeset")
                {

                    Dictionary<string, string> dicBuildInfo = nde.Fields;
                    {
                        if (dicBuildInfo.ContainsKey("ChangesetId"))
                        {
                            isSendEmail = true;
                            _changeSetId = dicBuildInfo["ChangesetId"];

                        }


                    }

                    VersionControlServer vcs = tpc.GetService<VersionControlServer>();
                    var changeset = vcs.GetChangeset(Convert.ToInt32(_changeSetId));
                    var changedFiles = from change in changeset.Changes
                                       where
                                           ((change.ChangeType & ChangeType.Edit) == ChangeType.Edit
                                           || (change.ChangeType & ChangeType.Add) == ChangeType.Add
                                           || (change.ChangeType & ChangeType.Delete) == ChangeType.Delete)
                                       select change.Item.ServerItem;


                    foreach (var file in changedFiles)
                    {

                        #region Get Difference of Each file


                        //var histories = vcs.QueryHistory(file, VersionSpec.Latest, 0, RecursionType.OneLevel, null, null, null, Int32.MaxValue, true, false, true);
                        //List<int> items = new List<int>();
                        //foreach (Changeset item in histories)
                        //{
                        //    if (items.Count == 2)
                        //    {
                        //        string outpt = GetFileDifference(file, items[0], items[1]);                                
                        //        var repo = new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(outpt));
                        //        message.Attachments.Add(new Attachment(repo, file + ".txt"));
                        //        break;
                        //    }

                        //    foreach (Change change in item.Changes)
                        //    {
                        //        change.Item.DownloadFile(System.IO.Path.GetTempPath() + change.Item.ChangesetId +
                        //                                   change.Item.ServerItem.Split('/')[change.Item.ServerItem.Split('/').Length - 1]);
                        //    }

                        //    items.Add(item.ChangesetId);
                        //}


                        #endregion



                        //{

                        //}
                    }



                }
            }




        }

        public static String PrintXML(String XML)
        {
            String Result = "";

            MemoryStream mStream = new MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(mStream, Encoding.Unicode);
            XmlDocument document = new XmlDocument();

            try
            {
                //System.Threading.Thread.Sleep(5000);
                //5 Load the XmlDocument with the XML.
                document.Load(XML);

                writer.Formatting = Formatting.Indented;

                // Write the XML into a formatting XmlTextWriter
                document.WriteContentTo(writer);
                writer.Flush();
                mStream.Flush();

                // Have to rewind the MemoryStream in order to read
                // its contents.
                mStream.Position = 0;

                // Read MemoryStream contents into a StreamReader.
                StreamReader sReader = new StreamReader(mStream);

                // Extract the text from the StreamReader.
                String FormattedXML = sReader.ReadToEnd();

                Result = FormattedXML;
            }
            catch (XmlException ex)
             {
                Console.WriteLine(ex.Message);
            }

            mStream.Close();
            //writer.Close();

            return Result;
        }
    }


}
