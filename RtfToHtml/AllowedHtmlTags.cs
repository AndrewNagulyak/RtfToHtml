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
           new Tag()
            {
              opening="br",
              openingRtf="\\line",
              closing="br/",
              closingRtf="\\line"
           },
           new Tag()
           {
              opening="center",
              openingRtf="{\\pard\\qr",
              closing="/center",
              closingRtf="\\par}"
           },
           new Tag()
           {
              opening="div",
              openingRtf="{\\pard",
              closing="/div",
              closingRtf="\\sb70\\par}"
           },
           new Tag()
           {
              opening="em",
              openingRtf="{\\b",
              closing="/em",
              closingRtf="}"
           },
           new Tag()
           {
              opening="font",
              openingRtf="{",
              closing="/font",
              closingRtf="}"
           },
            new Tag()
           {
              opening="h1",
              openingRtf="{\\pard",
              closing="/h1",
              closingRtf="\\sb70\\par}"
           },
            new Tag()
           {
              opening="h2",
              openingRtf="{\\pard",
              closing="/h2",
              closingRtf="\\sb70\\par}"
           },
            new Tag()
           {
              opening="h3",
              openingRtf="{\\pard",
              closing="/h3",
              closingRtf="\\sb70\\par}"
           },
            new Tag()
           {
              opening="h4",
              openingRtf="{\\pard",
              closing="/h4",
              closingRtf="\\sb70\\par}"
           },
           new Tag()
           {
              opening="h5",
              openingRtf="{\\pard",
              closing="/h5",
              closingRtf="\\sb70\\par}"
           },
           new Tag()
           {
              opening="h6",
              openingRtf="{\\pard",
              closing="/h6",
              closingRtf="\\sb70\\par}"
           },
             new Tag()
           {
              opening="i",
              openingRtf="{\\i",
              closing="/i",
              closingRtf="}"
           },
             new Tag()
           {
              opening="li",
              openingRtf="{\\pntext\\tab}",
              closing="/li",
              closingRtf="\\par"
           },
            new Tag()
           {
              opening="mark",
              openingRtf="{",
              closing="/mark",
              closingRtf="}"
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
