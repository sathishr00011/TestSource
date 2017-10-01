using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;
using System.Xml.Xsl;
using System.Net.Mail;

namespace UpdateRootManifest
{
    public static class Util
    {
        public static string Component { get; set; }
        public static string JenkinsURL { get; set; }
        public static string CompnentManifet { get; set; }

        public static string UpdateComponentManifest(string componentRMFile, string Component)
        {

            string altername = componentRMFile.Substring(0, componentRMFile.LastIndexOf("\\") + 1);
             altername += ConfigurationManager.AppSettings[Component + "Pattern"] + ConfigurationManager.AppSettings[Component] + ".xml";
            File.Copy(componentRMFile, altername);
            return altername.Substring(altername.LastIndexOf("\\") + 1);
        }

        public static void SendEmail(string fileName, string bdy, string type)
        {
            string subjectstatus;
            string body = string.Empty;
            if (type == "before")
            {
                body = ConfigurationManager.AppSettings["Body"];
                string bodyaddition = "<br><b>Deployment Details: <b><br> <span style=font-weight:normal>";
                body = body += bodyaddition;
                subjectstatus = "Started"; 
            }
            else
            {

                body = "Team,  <br><br>Deployment Status: Completed <br> <br><b>Deployment Details: <b><br> <span style=font-weight:normal>";
                subjectstatus = "Completed";
            }

            string subjectFileName = fileName.Substring(fileName.LastIndexOf('\\') + 1);
            subjectFileName = subjectFileName.Substring(0, subjectFileName.LastIndexOf('.'));
            string[] subjectFileNameFilter = subjectFileName.Split('_');
            //subjectFileName = subjectFileNameFilter[2].ToString();
            subjectFileName=subjectFileNameFilter[subjectFileNameFilter.Length - 1].ToString();
              DateTime dateConvert = DateTime.ParseExact(subjectFileName, "yyyy-MM-dd-HH-mm-ss", System.Globalization.CultureInfo.InvariantCulture);
            var istdate = TimeZoneInfo.ConvertTimeFromUtc(dateConvert,TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

            SmtpClient client = new SmtpClient(ConfigurationManager.AppSettings["SMTPServer"]);
            MailMessage message = new MailMessage(ConfigurationManager.AppSettings["FromAddress"], ConfigurationManager.AppSettings["ToAddress"], ConfigurationManager.AppSettings["MailSubject"] + ": " + subjectstatus + " " + istdate + " IST", body);

            message.Body += bdy;

            message.IsBodyHtml = true;
            if (File.Exists(fileName))
                //message.Attachments.Add(new Attachment(fileName));
            client.Send(message);
        }

        public static string WriteConsole(string rootFile)
        {

            XslCompiledTransform xslt = new XslCompiledTransform();
            xslt.Load("ReleaseManifestStyle.xslt");

            // Execute the transform and output the results to a file.
            xslt.Transform(rootFile, "Output.html");

            StringBuilder sb = new StringBuilder();
            using (StreamReader sr = new StreamReader("Output.html"))
            {
                String line;
                string versionName = rootFile.Substring(rootFile.LastIndexOf("\\") + 1);
                versionName = versionName.Substring(0, versionName.LastIndexOf('.'));
               // sb.Append("<h3><u>" + versionName + "</u></h3>");
                while ((line = sr.ReadLine()) != null)
                {
                    sb.AppendLine(line);
                }
            }
            sb.AppendLine("<br>Regards,<br>");
            sb.AppendLine("Team Jaguars");
            string allines = sb.ToString();
            return allines;
        }

        public static void NotifyDeployment()
        {
            string filename = ConfigurationManager.AppSettings["ManifestFile"];
            string body = WriteConsole(filename);
            SendEmail(filename, body, "after");

        }
    }
}
