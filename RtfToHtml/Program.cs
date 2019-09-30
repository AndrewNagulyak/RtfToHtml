using System;

using System.Collections.Generic;

using System.Text;


using System.Text.RegularExpressions;

using System.IO;

namespace RtfToHtml
{
    class Program
    {
        [STAThread()]

        static void Main(string[] args)

        {
            string htmlofExample = File.ReadAllText("../../html.html");
            string rtfofExample = File.ReadAllText("../../rtf.rtf");

           // Console.WriteLine(htmlofExample);
            Rtf htmlToRtf = new Rtf();
            SaveToRtfFile(htmlToRtf.convertHtmlToRtf(htmlofExample));
            Html rtfToHtml = new Html();
            SaveToHtmlFile(rtfToHtml.convertRtfToHtml(rtfofExample));
        }
        static void SaveToRtfFile( string html)
        {
            //Console.WriteLine(html);

            // Assume we already have a document 'dc'.
           File.WriteAllText(@"../../Rtf.rtf", html);
        }
        static void SaveToHtmlFile(string rtf)
        {
            // Assume we already have a document 'dc'.
            File.WriteAllText(@"../../html1.html", rtf);
        }


    }
}
