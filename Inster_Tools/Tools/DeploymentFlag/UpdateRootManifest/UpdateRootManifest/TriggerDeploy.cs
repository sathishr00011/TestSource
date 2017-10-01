using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Net;
using System.Configuration;

namespace UpdateRootManifest
{
    public class TriggerDeploy
    {
        string fileName;
        bool validation=false;
        public TriggerDeploy(string rootRMFile, string componentManifestPath)
        {
            string datetimeNow = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss", CultureInfo.InvariantCulture);
            fileName = rootRMFile.Substring(0, rootRMFile.LastIndexOf('.'));
            fileName += "_" + datetimeNow + ".xml";
            File.Copy(rootRMFile, fileName);
            ResetRootManifest(rootRMFile);
            Roortvalidation(fileName);

            if (validation != false)
            {
                string body = Util.WriteConsole(fileName);
                Util.SendEmail(fileName, body, "before");
                UpdateConfig(fileName);
                TriggerNolio();
            }
            else
            {
                Console.WriteLine("could not be found ToBeInstalled State.\n");
            }


        }

        private void ResetRootManifest(string rmFile)
        {
            string releaseManifestMasterNode = "ReleaseManifest";
            string releaseManifestChildNode = "Product/Services/Service/Component";
            XmlDocument xmlReleaseManifest = new XmlDocument();
            xmlReleaseManifest.Load(rmFile);

            XmlNode node = xmlReleaseManifest.SelectSingleNode(releaseManifestMasterNode);

            if (node != null)
            {

                XmlNodeList matchingBuildOutputPaths = node.SelectNodes(releaseManifestChildNode + "/@State");
                var matchingBuildOutputPathsList = new List<XmlNode>(matchingBuildOutputPaths.Cast<XmlNode>());

                if (matchingBuildOutputPathsList.Count > 0)
                {
                    foreach (var state in matchingBuildOutputPathsList)
                    {
                        state.Value = "Installed";
                    }
                }
                else
                {
                    Console.WriteLine("ReleaseManifestGen: The MSI component name could not be found.\n");

                }
                xmlReleaseManifest.Save(rmFile);

            }
        }

        private void Roortvalidation(string newFile)
        {
            string releaseManifestMasterNode = "ReleaseManifest";
            string releaseManifestChildNode = "Product/Services/Service/Component";
            XmlDocument xmlReleaseManifest = new XmlDocument();
            xmlReleaseManifest.Load(newFile);

            XmlNode node = xmlReleaseManifest.SelectSingleNode(releaseManifestMasterNode);

            if (node != null)
            {

                XmlNodeList matchingBuildOutputPaths = node.SelectNodes(releaseManifestChildNode + "/@State");
                var matchingBuildOutputPathsList = new List<XmlNode>(matchingBuildOutputPaths.Cast<XmlNode>());

                if (matchingBuildOutputPathsList.Count > 0)
                {
                    //foreach (var state in matchingBuildOutputPathsList)
                    //{

                    //    if (state.Value.Contains("ToBeInstalled"))
                    //    {
                    //        validation = true;
                    //    }
                    //    else
                    //    {
                    //        validation = false;
                    //    }
                    //}

                    if (matchingBuildOutputPathsList.Find(item => item.Value.Contains("ToBeInstalled")) != null)
                    {
                        validation = true;
                    }
                }
                else
                {
                    Console.WriteLine("ReleaseManifestGen: The MSI component name could not be found.\n");

                }


            }
        }


        private void TriggerNolio()
        {

            XDocument doc = XDocument.Load(fileName);

            var elements =
      (from el in doc.Descendants("Component")
       where (string)el.Attribute("State").Value == "ToBeInstalled"
       select el);

            Dictionary<string, string> Components = new Dictionary<string, string>();
            foreach (var ele in elements)
            {
                if (!Components.ContainsKey(ele.Attribute("ComponentName").Value))
                {
                    string component=  ele.Attribute("ComponentName").Value;
                    string pattern =  ConfigurationManager.AppSettings[component + "NolioPattern"];
                    string onlyVersion = Path.GetFileNameWithoutExtension(ele.Attribute("ComponentManifest").Value);
                    onlyVersion = onlyVersion.Substring(onlyVersion.LastIndexOf("_") + 1);
                    Components.Add(component, pattern + onlyVersion);
                }

            }


            
            foreach (var pair in Components)
            {
                var key = pair.Key;
                var value = pair.Value;

                if (Components[key].Equals(Components.Last().Value))
                {
                    TriggerJenkinsJob(Util.JenkinsURL, key, value, true);
                }
                else

                {
                    TriggerJenkinsJob(Util.JenkinsURL, key, value, false);
                }

            }



        }

        private static void TriggerJenkinsJob(string Jenkinsurl, string component, string NolioArgument,bool isLastJob)
        {
            
            StringBuilder strComponent = new StringBuilder();
            StringBuilder strNolioArgument = new StringBuilder();
            StringBuilder strIsLastJob = new StringBuilder();
            strComponent.Append("&COMPONENT=" + component);
            strNolioArgument.Append("&NOLIOARGUMENT=" + NolioArgument);
            strIsLastJob.Append("&ISLASTJOB=" + isLastJob.ToString());

            string jobParameters = strComponent.ToString() + strNolioArgument.ToString() + strIsLastJob.ToString();
            using (WebClient wc = new WebClient())
            {
                wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                string HtmlResult = wc.UploadString(Jenkinsurl, jobParameters);
            }

        }

        public void UpdateConfig(string filename)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["ManifestFile"].Value = filename;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}
