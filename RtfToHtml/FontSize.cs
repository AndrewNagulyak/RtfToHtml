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
            return null;
        }

        public static string getFontSizeReferenceInPx(double valueInPixel)
        {
            return FONT_SIZE_RTF_REFERENCE + Math.Truncate((double)(valueInPixel) * ONE_PIXEL_IN_POINT);
        }
    }
}
