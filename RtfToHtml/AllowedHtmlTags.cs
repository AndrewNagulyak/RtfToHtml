using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtfToHtml
{
    public static class AllowedHtmlTags
    {
        public static Tag[] HtmlTags = new Tag[]{
           new Tag()
           {
              opening = "b",
              openingRtf = "{\\b",
              closing = "/b",
              closingRtf =  "}"
           },
           new Tag()
           {
              opening = "h1",
              openingRtf = "{\\pard",
              closing = "/h1",
              closingRtf =  "\\sb70\\par}"
           },
           new Tag()
           {
              opening = "p",
              openingRtf= "{\\pard",
              closing= "/p",
              closingRtf= "\\sb70\\par}"
           },
           new Tag()
           {
              opening= "html",
              openingRtf= "",
              closing= "/html",
              closingRtf= ""
           },
           new Tag()
           {
              opening= "head",
              openingRtf= "",
              closing= "/head",
              closingRtf= ""
           },
           new Tag()
           {
              opening= "body",
              openingRtf= "",
              closing= "/body",
              closingRtf= ""
           },
        };
        public static bool isKnowedTag(string tag)
        {
            Tag isKnowedTag = getAllowedTag(tag);
            return isKnowedTag != null;
        }
        public static Tag getAllowedTag(string tag)
        {
            tag = tag.ToLower();
            foreach (Tag knowedTag in HtmlTags)
            {
                if (knowedTag.opening == tag || knowedTag.closing == tag)
                    return knowedTag;
            }
            return null;
        }
        public static string getRtfReferenceTag(string tagName)
        {
            Tag allowedTag;

            tagName = tagName.ToLower();
            allowedTag = getAllowedTag(tagName);

            if (allowedTag != null)
            {
                return tagName == allowedTag.opening ? allowedTag.openingRtf : allowedTag.closingRtf;
            }
            return null;
        }
    }

}
