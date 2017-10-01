using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using htmlconverter;

namespace htmlconverter
{
    class SautinSoft
    {
         SautinSoft.HtmlToRtf h = new SautinSoft.HtmlToRtf();

            string[] htmlFiles = Directory.GetFiles(@"c:\htmls\", "*.htm*");
            string singleRtfPath = @"c:\Book.rtf";


            List<string> rtfDocs = new List<string>();

            // Convert each HTML file from "c:\htmls\" into RTF document
            foreach (string htmlFile in htmlFiles)
                rtfDocs.Add(h.ConvertFileToString(htmlFile));

            string singleRtfDoc = String.Empty;
            foreach (string rtfDoc in rtfDocs)
                singleRtfDoc = h.MergeRtfString(singleRtfDoc, rtfDoc);

            File.WriteAllText(singleRtfPath, singleRtfDoc);
    }
}
