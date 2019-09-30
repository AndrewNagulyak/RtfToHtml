using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtfToHtml
{
    static class Style
    {
        public static string getRtfAlignmentReference(string value)
        {
            return Alignment.getRtfAlignmentReference(value);

        }
        public static string getRtfTextDecorationReference(string value)
        {
            return TextDecoration.getRtfTextDecorationReference(value);

        }
        
        public static string getRtfReferenceColor(string value)
        {
            return Color.getRtfReferenceColor(value);
        }
       
            public static string getRtfReferenceBackGroundColor(string value)
        {
            return Color.getRtfReferenceBackgroundColor(value);
        }
        public static string getRtfColorTable()
        {
            return Color.getRtfColorTable();
        }
        public static string getRtfFontTable()
        {
            return FontFamily.getRtfFontTable();
        }
        public static string getRtfFontSizeReference(string value)
        {
            return FontSize.getRtfFontSizeReference(value);
        }
        public static string getRtfFontReference(string value)
        {
            value = value.Split(',')[0];
            value = value.Replace(@"'", " ");
            Console.WriteLine(value);
            return FontFamily.getRtfReferenceFont(value);
        }
        public static string getRtfReferencesInStyleProperty(string styleValue)
        {
            string value = "";
            string propertyName = "";
            string listOfRtfReferences = "";

            foreach (var entries in styleValue.Split(';'))
            {
                string[] values = entries.Split(':');
                if (values.Length == 2)
                {

                    propertyName = values[0];
                    value = values[1];
                    propertyName = propertyName.Replace(" ", "");
                    value = value.Trim();

                    switch (propertyName)
                    {
                        case "font-family":

                            { listOfRtfReferences += Style.getRtfFontReference(value); break; }
                    
                        case "color":
                            { listOfRtfReferences += Style.getRtfReferenceColor(value); break; }
                        case "background-color":
                            { listOfRtfReferences += Style.getRtfReferenceBackGroundColor(value); break; }
                        case "font-size":
                            { listOfRtfReferences += Style.getRtfFontSizeReference(value); break; }
                        case "text-align":
                            { listOfRtfReferences += Style.getRtfAlignmentReference(value); break; }
                        case "text-decoration":
                            { listOfRtfReferences += Style.getRtfTextDecorationReference(value); break; }
                    }
                }
            
            }



            //if (listOfRtfReferences == '')
            //    return undefined;

            return listOfRtfReferences;
        }
    }
}
