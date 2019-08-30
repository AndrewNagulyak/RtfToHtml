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
            string htmlofExample = "<html><head></head><body><div><p style = 'color:red; margin:5px;' align='center'>text of paragraph<b> text with bold<i>text with italic and bold</i></b><i>text with italic</i></p><p style = 'color:rgb(255,0,0);' align= 'right' > red paragraph => right with tag</p><p style = 'color:rgb(0,0,255); text-align:center;' > blue paragraph => center with style</p></div></body></html>";
            Console.WriteLine(htmlofExample);
            Rtf htmlToRtf = new Rtf();
           SaveToRtfFile(htmlToRtf.convertHtmlToRtf(htmlofExample));
        }
        static void SaveToRtfFile( string html)
        {
            Console.WriteLine(html);

            // Assume we already have a document 'dc'.
           File.WriteAllText(@"G:/projects/RtfToHtml/RtfToHtml/Rtf.rtf", html);
        }


    }
}
