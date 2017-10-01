using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace Deployment
{
   public class Util
    {

   
       public static string JenkinsURL { get; set; }
       public static void TriggerJenkinsJob(string jenken)
       {

           WebClient wc = new WebClient();
           
           wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";

           string HtmlResult = wc.UploadString(JenkinsURL, jenken);

       }

    }
}
