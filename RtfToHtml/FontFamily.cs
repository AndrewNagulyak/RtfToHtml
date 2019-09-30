using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtfToHtml
{
    static class FontTable
    {
        public static int amount = 0;
        public static List<string> font = new List<string>();
        
    }
    static class FontFamily
    {

        const string RTF_FONT_TABLE_OPENING = "{\\fonttbl";
        const string RTF_FONT_TABLE_CLOSING = "}";

        public static string getRtfFontTable()
        {

            return RTF_FONT_TABLE_OPENING + getAllFontsDeclaredInFontTable() + RTF_FONT_TABLE_CLOSING;
        }

        public static string getRtfReferenceFont(string font)
        {
                return getFontInFontTable(font);
        }
        public static string getFontInFontTable(string font)
        {
            if (verifyIfFontExistsInFontTable(font))
                return getRtfReferenceFontInFontTable(font);
            else
            {
                addFontInFontTable(font);
                return getRtfReferenceFontInFontTable(font);
            }
        }
 
        public static bool verifyIfFontExistsInFontTable(string font)
        {
            bool hasThisFont = false;
            foreach (string value in FontTable.font)
            {
                if (value==font)
                    hasThisFont = true;
            }
            return hasThisFont;
        }

        public static void addFontInFontTable(string font)
        {
            if (FontTable.amount == 0)
            { FontTable.font.Add("TimesNewRoman");
                FontTable.amount++; }
            string rtfReferenceFont = "";
            int amountFontPosition = 0, fontsPosition = 1;
            FontTable.amount++;

            //rtfReferenceFont = "\\f" + ColorTable.amount + "\\fcharset0" + font;
            bool flag = true;
           foreach(string fontInTable in FontTable.font )
            {
                Console.WriteLine(fontInTable + "===" + font);

                if (fontInTable == font)
                {
                    flag = false;
                }
            }
            if (flag) { 
            FontTable.font.Add(font);
            }
        }

        public static string getRtfReferenceFontInFontTable(string font)
        {
            string rtfReferenceFont = "";
            
            foreach (string value in FontTable.font)
            {
                Console.WriteLine(value + "----" + font);
                if (value == font)
                    rtfReferenceFont = "\\f" + FontTable.font.IndexOf(value);
            }

            return rtfReferenceFont;
        }

        public static string getAllFontsDeclaredInFontTable()
        {

            string fontTableContent = "";
            foreach (string value in FontTable.font)
            {

                fontTableContent += "{\\f"+FontTable.font.IndexOf(value)+"\\fcharset0 "+value+";}";
            }
            return fontTableContent;
        }

    }
}
