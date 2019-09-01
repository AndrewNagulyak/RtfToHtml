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
              opening = "p",
              openingRtf= "{",
              closing= "/p",
              closingRtf= "\\par}"
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
              openingRtf="\\par",
              closing="br/",
              closingRtf="\\par"
           },
           new Tag()
           {
              opening="center",
              openingRtf="\\line{\\pard\\qc",
              closing="/center",
              closingRtf="\\par}"
           },
           new Tag()
           {
              opening="div",
              openingRtf="{",
              closing="/div",
              closingRtf= "\\par}"

           },
       
           new Tag()
           {
              opening="a",
              openingRtf="",
              closing="/a",
              closingRtf= "}}}"

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
              openingRtf="{\\fs43\\f2 {\\ltrch",
              closing="/h1",
              closingRtf="}\\li0\\ri0\\sa0\\sb0\\fi0\\ql\\par}"
           },
            new Tag()
           {
              opening="h2",
              openingRtf="{\\fs39\\f2 {\\ltrch",
              closing="/h2",
              closingRtf="}\\li0\\ri0\\sa0\\sb0\\fi0\\ql\\par}"
           },
            new Tag()
           {
              opening="h3",
              openingRtf="{\\fs36\\f2 {\\ltrch",
              closing="/h3",
              closingRtf="}\\li0\\ri0\\sa0\\sb0\\fi0\\ql\\par}"
           },
            new Tag()
           {
              opening="h4",
              openingRtf="{\\fs32\\f2 {\\ltrch",
              closing="/h4",
              closingRtf="}\\li0\\ri0\\sa0\\sb0\\fi0\\ql\\par}"
           },
           new Tag()
           {
              opening="h5",
              openingRtf="{\\fs24\\f2 {\\ltrch",
              closing="/h5",
              closingRtf="}\\li0\\ri0\\sa0\\sb0\\fi0\\ql\\par}"
           },
           new Tag()
           {
              opening="h6",
              openingRtf="{\\fs19\\f2 {\\ltrch",
              closing="/h6",
              closingRtf="}\\li0\\ri0\\sa0\\sb0\\fi0\\ql\\par}"
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
                    new Tag()

           {
              opening= "ol",
              openingRtf= "{{\\*\\pn\\pnlvlbody\\pnf0\\pnindent0\\pnstart1\\pndec{\\pntxta.}}\\fi-360\\li720\\sa200\\sl276\\slmult1",
              closing= "/ol",
              closingRtf= "}"
           },
             new Tag()

           {
              opening= "ul",
              openingRtf= "{{\\*\\pn\\pnlvlblt\\pnf1\\pnindent0{\\pntxtb\\\'B7}}\\fi-360\\li720\\sa200\\sl276\\slmult1\\lang22\\f0\\fs22",
              closing= "/ul",
              closingRtf= "}"
           },
                    new Tag()

           {
              opening= "s",
              openingRtf= "{\\strike",
              closing= "/s",
              closingRtf= "}"
           },
                    new Tag()

           {
              opening= "span",
              openingRtf= "{",
              closing= "/span",
              closingRtf= "}"
           },
                    new Tag()

           {
              opening= "sub",
              openingRtf= "{\\sub",
              closing= "/sub",
              closingRtf= "}"
           },
                    new Tag()

           {
              opening= "sup",
              openingRtf= "{\\super",
              closing= "/sup",
              closingRtf= "}"
           },
                    new Tag()

           {
              opening= "strong",
              openingRtf= "{\\b",
              closing= "/strong",
              closingRtf= "}"
           },
                    new Tag()

           {
              opening= "table",
              openingRtf= "\\par{",
              closing= "/table",
              closingRtf= "}"
           },
                    new Tag()

           {
              opening= "tbody",
              openingRtf= "",
              closing= "/tbody",
              closingRtf= ""
           },
                    new Tag()

           {
              opening= "thead",
              openingRtf= "",
              closing= "/thead",
              closingRtf= ""
           },
                    new Tag()

           {
              opening= "td",
              openingRtf= "{\\pard\\intbl\\qc",
              closing= "/td",
              closingRtf= "\\cell}"
           },
                    new Tag()

           {
              opening= "th",
              openingRtf= "{\\pard\\intbl\\qc",
              closing= "/th",
              closingRtf= "\\cell}"
           },
                    new Tag()

           {
              opening= "tr",
              openingRtf= "{\\trowd\\trgaph10",
              closing= "/tr",
              closingRtf= "\\row}"
           },
                    new Tag()

           {
              opening= "u",
              openingRtf= "{\\ul",
              closing= "/u",
              closingRtf= "}"
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
              opening= "style",
              openingRtf= "",
              closing= "/style",
              closingRtf= ""
           },
                    new Tag()

           {
              opening= "meta",
              openingRtf= "",
              closing= "meta",
              closingRtf= ""
           },
                    new Tag()

           {
              opening= "title",
              openingRtf= "",
              closing= "/title",
              closingRtf= ""
           }
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
