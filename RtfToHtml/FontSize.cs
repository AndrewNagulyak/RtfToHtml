using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtfToHtml
{
   public class FontSize
    {
        public const string FONT_SIZE_RTF_REFERENCE = "\\fs";
        public const double ONE_PIXEL_IN_POINT = 0.75;
        public static string getRtfFontSizeReference(string value)
        {
            if (value.Contains("px"))
            {
                value = value.Replace("px", "");
                double result = 0;
                Double.TryParse(value,out result);
                return getFontSizeReferenceInPx(result);
            }
            else
            {
                foreach (KeyValuePair<string, string> entry in baseSizes)
                {
                    if (entry.Key == value)
                        return getRtfFontSizeReference(entry.Value);
                }
            }
            return null;
        }
        static IDictionary<string, string> baseSizes = new Dictionary<string, string>()
                                            {
                                                {"x-small","10px"},
                                                {"small","13.333px"},
                                                {"medium","16px"},
                                                {"large","	18px"},
                                                {"x-large","24px"},
                                                {"xx-large","32px"},                                        
                                            };
        public static string getFontSizeReferenceInPx(double valueInPixel)
        {
            return FONT_SIZE_RTF_REFERENCE + Math.Truncate((double)(valueInPixel) * ONE_PIXEL_IN_POINT) *2;
        }
    }
}
