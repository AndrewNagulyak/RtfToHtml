using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RtfToHtml
{
   public static class MyString
    {
       public static string removeCharacterOfEscapeInAllString(string stringValue, string stringOfEscape)
        {
            string listOfEscape = removeCharacterOfEscapeNotAllowed(stringOfEscape);
            string newstringValue = "";

            if (listOfEscape == String.Empty)
                return stringValue;
            foreach(var element in stringValue)
            { 
                if (!listOfEscape.Contains(element))
                    newstringValue += element;
            }
            return newstringValue;
        }

       public static string convertOneCharInHexToDec(string value)
        {

            if (value.Length != 1)
                return null;
             if (value[0] >= '0' && value[0] <= '9')
                return value;

            else if (Char.ToUpper(value[0]) >= 'A' && Char.ToUpper(value[0]) <= 'F')
            {
                value = Char.ToUpper(value[0]).ToString();
                string number = "";
                switch (value)
                {
                    case "A": number = "10"; break;
                    case "B": number = "11"; break;
                    case "C": number = "12"; break;
                    case "D": number = "13"; break;
                    case "E": number = "14"; break;
                    case "F": number = "15"; break;
                }
                return number;
            }
            else
                return null;
        }

        static string removeCharacterOfEscapeNotAllowed(string stringOfEscape)
        {
            string[] listOfCharacterOfEscape = new string[] { "\n", "\r", "\t", "\f" };
            string newStringOfEscape = "";
            foreach(string s in listOfCharacterOfEscape)
            { 
                if (stringOfEscape.Contains(s))
                    newStringOfEscape+=s;
            }
            return newStringOfEscape.Length > 0 ? newStringOfEscape : null;
        }

        public static bool hasOnlyWhiteSpace(string content)
        {
            return String.IsNullOrWhiteSpace(content);
        }
    }
}
