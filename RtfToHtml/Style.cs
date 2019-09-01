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
        public static string getRtfFontSizeReference(string value)
        {
            return FontSize.getRtfFontSizeReference(value);
        }
        public static string getRtfReferencesInStyleProperty(string styleValue)
        {
            string value = "";
            string propertyName = "";
            string listOfRtfReferences = "";
            styleValue = styleValue.Replace(" ", "");

            foreach (var entries in styleValue.Split(';'))
            {
                string[] values = entries.Split(':');
                if (values.Length == 2)
                {
                    propertyName = values[0];
                    value = values[1];
                    switch (propertyName)
                    {

                        case "color":
                            { listOfRtfReferences += Style.getRtfReferenceColor(value); break; }
                        case "background-color":
                            { listOfRtfReferences += Style.getRtfReferenceBackGroundColor(value); break; }
                        case "font-size":
                            { listOfRtfReferences += Style.getRtfFontSizeReference(value); break; }
                        case "text-align":
                            { listOfRtfReferences += Style.getRtfAlignmentReference(value); break; }
                    }
                }
            
            }



            //if (listOfRtfReferences == '')
            //    return undefined;

            return listOfRtfReferences;
        }
    }
}
