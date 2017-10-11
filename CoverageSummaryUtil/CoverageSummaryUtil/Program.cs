using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.TeamFoundation.Client;
using System.Collections.ObjectModel;
using Microsoft.TeamFoundation.Framework.Client;
using Microsoft.TeamFoundation.Framework.Common;
using Microsoft.TeamFoundation.Build.Client;
using Microsoft.TeamFoundation.TestManagement.Client;
using System.Net.Mail;
using System.Configuration;
using System.Xml.Linq;
using System.IO;
using System.Collections;
using System.Diagnostics;

namespace CoverageSummaryUtil
{
    class Program
    {
        static string teamProjcet;
        static string buildDefinition;
        static bool isSendEmail = true;
        static string toEmail = "sathishkumar.x.ramaraj@in.tesco.com";

        static void Main(string[] args)
        {
            Uri tfsUri = new Uri(ConfigurationManager.AppSettings["Uri"]);
            toEmail = ConfigurationManager.AppSettings["EmailTo"];
            teamProjcet = ConfigurationManager.AppSettings["Project"];
            TfsTeamProjectCollection server;
            IBuildServer buildServer;

            server = new TfsTeamProjectCollection(tfsUri);
            TestManagementService tcm;
            tcm = (TestManagementService)server.GetService(typeof(TestManagementService));


            server.EnsureAuthenticated();
            buildServer = (IBuildServer)server.GetService(typeof(IBuildServer));

            string bdy = "<p><b><u>Coverage Report</u></b></p>";

            bdy = SectionWiseCoverage(buildServer, tcm, bdy, "PaymentService");
            //bdy += SectionWiseCoverage(buildServer, tcm, bdy, "RBC");
            //bdy += SectionWiseCoverage(buildServer, tcm, bdy, "TAPI");
            //bdy += SectionWiseCoverage(buildServer, tcm, bdy, "RAPI");
            //bdy += SectionWiseCoverage(buildServer, tcm, bdy, "WindowsServices");


            if (isSendEmail)
            {
                SendEmail(bdy);
            }




        }

        private static string SectionWiseCoverage(IBuildServer buildServer, TestManagementService tcm, string bdy, string _section)
        {
            var section = (ConfigurationManager.GetSection("BuildDefs/" + _section) as System.Collections.Hashtable)
                 .Cast<System.Collections.DictionaryEntry>()
                 .ToDictionary(n => n.Key.ToString(), n => n.Value.ToString());

            bdy = "<p style='text-align:center;font:tahoma;font-size:15px;'><b><u>" + _section + "</u></b></p>";
            foreach (KeyValuePair<string, string> entry in section)
            {

                IBuildDetail[] nightlyBuilds = buildServer.QueryBuilds(teamProjcet, entry.Value);

                if (nightlyBuilds.Length != 0)
                {
                    IBuildDetail mostRecent = nightlyBuilds[nightlyBuilds.GetUpperBound(0)];
                    bdy += GetCoverage(mostRecent, tcm, entry.Key);
                }


            }
            return bdy;
        }

        private static string GetCoverage(IBuildDetail mostRecent, TestManagementService tcm, string component)
        {
            string msg = string.Empty;
            var coverageAnalysisManager = tcm.GetTeamProject(teamProjcet).CoverageAnalysisManager;

            var queryBuildCoverage = coverageAnalysisManager.QueryBuildCoverage(mostRecent.Uri.ToString(), CoverageQueryFlags.Modules);


            foreach (var coverage in queryBuildCoverage)
            {

                msg += "<table width='80%' align='center' style='border:1px solid #fff;border-collapse:collapse;font-size=14px;padding:10px;font:Tahoma;margin:auto 0;'><tr bgcolor='#66a9bd' style='padding:10px;color:#fff;'><th  width='40%' style='padding:5px;text-align:center;'>" + component + "</th><th style='padding:5px;text-align:center;' >Blocks Covered</th><th style='padding:5px;text-align:center;'>Blocks Not Covered</th><th style='padding:5px;text-align:center;'>Covered Percentage</th></tr>";
                foreach (var moduleInfo in coverage.Modules)
                {
                    var a1 = moduleInfo.Name;
                    var a2 = moduleInfo.Statistics.BlocksCovered;
                    var a3 = moduleInfo.Statistics.BlocksNotCovered;
                    var a4 = Convert.ToDecimal(moduleInfo.Statistics.BlocksCovered) + Convert.ToDecimal(moduleInfo.Statistics.BlocksNotCovered);
                    a4 = Convert.ToDecimal(moduleInfo.Statistics.BlocksNotCovered) / Convert.ToDecimal(a4);
                    a4 = 100 - (a4 * 100);
                    a4 = decimal.Round(a4, 2);
                    string clor;
                    if (a4 > 80)
                    {
                        clor = "91c5d4";
                    }
                    else if (a4 > 60)
                    {
                        clor = "b0cc7fx";
                    }
                    else
                    {
                        clor = "d43031";
                    }
                    msg += "<tr bgcolor='d5eaf0' style='padding:10px;font-size=12px;'><td style='padding:5px;border:1px solid #fff'>" + a1 + "</td><td style='padding:5px;text-align:center;border:1px solid #fff'>" + a2 + "</td><td style='padding:5px;text-align:center;border:1px solid #fff'>" + a3 + "</td><td style='padding:5px;text-align:center;border:1px solid #fff' bgcolor='" + clor + "'>" + a4 + "</td></tr>";

                }
                msg += "</font></table>";
            }
            return msg;
        }

        private static void SendEmail(string bdy)
        {
            CaptureSonar();
            SmtpClient client = new SmtpClient(ConfigurationManager.AppSettings["SMTPServer"]);
            MailMessage message = new MailMessage(ConfigurationManager.AppSettings["FromAddress"], toEmail, ConfigurationManager.AppSettings["MailSubject"], bdy);
            message.IsBodyHtml = true;
            if (File.Exists("snrreport.png"))
                message.Attachments.Add(new Attachment("d:\\temp\\snrreport.png"));
            client.Send(message);

        }

        private static void CaptureSonar()
        {
            Process capt = new Process();
            System.Threading.Thread.Sleep(3000);
            capt.StartInfo.FileName = "d:\\IECapt.exe";
            capt.StartInfo.Arguments = " --url=http://ukdbt11build02v:9000/ --out=d:\\temp\\snrreport.png";
            capt.Start();
            while (capt.HasExited == false)
            {
                System.Threading.Thread.Sleep(250);
            }

        }
    }
}
