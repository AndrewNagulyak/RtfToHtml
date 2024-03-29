using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RtfToHtml
{
    static class ColorTable
    {
        public static int amount = -1;
        public static List<string[]> colors = new List<string[]>();
    }
    static class Color
    {

        static IDictionary<string, string> baseColors = new Dictionary<string, string>()
                                            {
                                                {"black","rgb(0,0,0)"},
                                                {"white","rgb(255,255,255)"},
                                                {"red","rgb(255,0,0)"},
                                                {"lime","rgb(0,255,0)"},
                                                {"yellow","rgb(255,255,0)"},
                                                {"cyan","rgb(0,255,255)"},
                                                {"magenta","rgb(255,0,255)"},
                                                {"silver","rgb(192,192,192)"},
                                                {"gray","rgb(128,128,128)"},
                                                {"maroon","rgb(128,0,0)"},
                                                {"olive","rgb(128,128,0)"},
                                                {"green","rgb(0,128,0)"},
                                                {"purple","rgb(128,0,128)"},
                                                {"teal","rgb(0,128,128)"},
                                                {"blue","rgb(0,0,255)"},

                                            };
        const string RTF_COLOR_TABLE_OPENING = "{\\colortbl";
        const string RTF_COLOR_TABLE_CLOSING = "}";
        public static string getRtfColorTable()
        {
            return RTF_COLOR_TABLE_OPENING + getAllColorsDeclaredInColorTable() + RTF_COLOR_TABLE_CLOSING;
        }

        public static string getRtfReferenceColor(string color)
        {
            foreach (KeyValuePair<string, string> entry in baseColors)
            {
                if (entry.Key == color.ToLower())
                    color = entry.Value;
            }
            if (color.Contains("rgb"))
                return getColorInColorTable(getRgbValues(color));

            if (color.Contains("#"))
                return getColorInColorTable(convertColorInHexToRgb(color));

            return null;
        }
        public static string getRtfReferenceBackgroundColor(string color)
        {
            foreach (KeyValuePair<string, string> entry in baseColors)
            {
                if (entry.Key == color.ToLower())
                    color = entry.Value;
            }
            if (color.Contains("rgb"))
                return getBackColorInColorTable(getRgbValues(color));

            if (color.Contains("#"))
                return getBackColorInColorTable(convertColorInHexToRgb(color));

            return null;
        }

        public static double[] getRgbValues(string color)
        {
            color = Regex.Replace(color, "[\\])}[{(rgb:; ]", "");
            return Array.ConvertAll(color.Split(','), Double.Parse);
        }

        public static double[] convertColorInHexToRgb(string hexColor)
        {
            Console.WriteLine("-----");
            Console.WriteLine(hexColor);
            double[] rgb = new double[3];
            hexColor = Regex.Replace(hexColor, "[#; ]", "");
            Console.WriteLine(hexColor);
            hexColor = (hexColor.Length == 3) ? hexColor[0] + "" + hexColor[0] + "" + hexColor[1] + "" + hexColor[1] + "" + hexColor[2] + "" + hexColor[2] : hexColor;
            Console.WriteLine(hexColor);

         
            rgb[2] = Convert.ToInt32((hexColor[4].ToString()+hexColor[5].ToString()).ToString(), 16);
            rgb[1] = Convert.ToInt32((hexColor[2].ToString()+hexColor[3].ToString()).ToString(), 16);
            rgb[0] = Convert.ToInt32((hexColor[0].ToString()+hexColor[1].ToString()).ToString(), 16);

            Console.WriteLine(rgb[0]+"-r"+rgb[1]+"-g"+rgb[2]);
            Console.WriteLine("-----");

            return rgb;
        }

        public static string getColorInColorTable(double[] rgb)
        {
            if (verifyIfColorExistsInColorTable(rgb, "fore"))
                return getRtfReferenceColorInColorTable(rgb, "fore");
            else
            {
                addColorInColorTable(rgb, "fore");
                return getRtfReferenceColorInColorTable(rgb, "fore");
            }
        }
        public static string getBackColorInColorTable(double[] rgb)
        {
            if (verifyIfColorExistsInColorTable(rgb, "back"))
                return getRtfReferenceColorInColorTable(rgb, "back");
            else
            {
                addColorInColorTable(rgb, "back");
                return getRtfReferenceColorInColorTable(rgb, "back");
            }
        }

        public static bool verifyIfColorExistsInColorTable(double[] rgb, string type)
        {
            bool hasThisColor = false; int colorsPosition = 1;
            string style = "";
            if (type == "fore")
            {
                style = "cf";
            }
            else if (type == "back")
            {
                style = "highlight";
            }
            foreach (string[] value in ColorTable.colors)
            {
                if (value[0] == rgb[0].ToString() && value[1] == rgb[1].ToString() && value[2] == rgb[2].ToString())
                    hasThisColor = true;
            }
            return hasThisColor;
        }

        public static void addColorInColorTable(double[] rgb, string type)
        {
            string rtfReferenceColor = "";
            int amountColorPosition = 0, colorsPosition = 1;
            ColorTable.amount++;
            Console.WriteLine(rgb[0] + rgb[1] + rgb[2] + ColorTable.amount);
                rtfReferenceColor = "\\cf" + ColorTable.amount;
            ColorTable.colors.Add(new string[4] { rgb[0].ToString(), rgb[1].ToString(), rgb[2].ToString(), rtfReferenceColor });
        }

        public static string getRtfReferenceColorInColorTable(double[] rgb, string type)
        {
            string rtfReferenceColor = "";
            string style = "";
           
            foreach (string[] value in ColorTable.colors)
            {
                if (value[0] == rgb[0].ToString() && value[1] == rgb[1].ToString() && value[2] == rgb[2].ToString())
                    rtfReferenceColor = value[3];
            }
            if (type == "fore")
            {
                rtfReferenceColor.Replace("highlight","cf");
            }
            else if (type == "back")
            {
                Console.WriteLine(rtfReferenceColor);
                rtfReferenceColor =  rtfReferenceColor.Replace("cf", "highlight");

                Console.WriteLine("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
                Console.WriteLine(rtfReferenceColor);

            }
            return rtfReferenceColor;
        }

        public static string getAllColorsDeclaredInColorTable()
        {
            string colorTableContent = "";
            foreach (string[] value in ColorTable.colors)
            {
                colorTableContent += "\\red" + value[0] + "\\green" + value[1] + "\\blue" + value[2] + " ;";
            }
            return colorTableContent;
        }

    }
}