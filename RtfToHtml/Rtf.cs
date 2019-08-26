using HtmlAgilityPack;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RtfToHtml
{
    class Rtf
    {
        string rtfHeaderOpening;
        string rtfHeaderContent;
        string rtfClosing;
        List<Reference> rtfContentReferences = new List<Reference>();
        public Rtf()
        {
            this.rtfHeaderOpening = "{\\rtf1\\ansi\\deff0{\\fonttbl {\\f0\\fnil\\fcharset0 Calibri;}{\\f1\\fnil\\fcharset2 Symbol;}}";
            this.rtfHeaderContent = "";
            this.rtfClosing = "}";
        }
        public string convertHtmlToRtf(string html)
        {
            string htmlWithoutStrangerTags = this.swapHtmlStrangerTags(html);
            Console.WriteLine(htmlWithoutStrangerTags);
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(htmlWithoutStrangerTags);
            
            var htmlBody = htmlDoc.DocumentNode.SelectSingleNode("//html");
            HtmlNodeCollection treeofTags = htmlBody.ChildNodes;

            foreach (var node in treeofTags)
            {
                readAllChildsInTag(node);
            }
            return this.buildRtf();
        }
        void readAllChildsInTag(HtmlNode fatherTag)
        {
            //Console.WriteLine(fatherTag.OriginalName);
            if (fatherTag.ChildNodes != null)
            {
                Console.WriteLine(fatherTag.Name);

                this.addOpeningTagInRtfCode(fatherTag.Name);
                // this.ifExistsAttributesAddAllReferencesInRtfCode(fatherTag.Attributes);

                //if (fatherTag.Name.toLowerCase() == 'table')
                //    this.Table.setAmountOfColumns(this.getAmountOfColumnThroughOfFirstChildOfTbodyTag(fatherTag.children));

                //if (fatherTag.Name.toLowerCase() == 'tr')
                //    this.addReferenceTagInRtfCode(this.Table.buildCellsLengthOfEachColumn());

                //if (fatherTag.Name.toLowerCase() == 'mark')
                //    this.setHighlightInRtf();
                foreach (HtmlNode node in fatherTag.ChildNodes)
                {
                    if (node.OriginalName != "#text")
                    {

                        this.readAllChildsInTag(node);
                    }
                    else
                    {
                        this.addContentOfTagInRtfCode(node.InnerText);
                        //Console.WriteLine(node.InnerText);
                    }
                }

            }
            //Console.WriteLine(fatherTag.OriginalName);

            this.addClosingFatherTagInRtfCode(fatherTag.OriginalName);

        }
        void addClosingFatherTagInRtfCode(string closingFatherTag)
        {
            Console.WriteLine(closingFatherTag);
            this.addReferenceTagInRtfCode(AllowedHtmlTags.getRtfReferenceTag($"/{closingFatherTag}"));
        }
        void addContentOfTagInRtfCode(string contentOfTag)
        {
            contentOfTag = MyString.removeCharacterOfEscapeInAllString(contentOfTag, "\n\t");

            if (contentOfTag != null && !MyString.hasOnlyWhiteSpace(contentOfTag))
                this.rtfContentReferences.Add(new Reference() { content = this.addSpaceAroundString(contentOfTag.Trim()), tag = false });
        }
        string addSpaceAroundString(string contentOfTag)
        {
            return $" {contentOfTag} ";
        }
        void addOpeningTagInRtfCode(string tag)
        {
            this.addReferenceTagInRtfCode(AllowedHtmlTags.getRtfReferenceTag(tag));
        }

        void addReferenceTagInRtfCode(string referenceTag)
        {
            if (referenceTag != null)
            {
                this.rtfContentReferences.Add(new Reference { content = referenceTag, tag = true });
            }
        }

        string buildRtf()
        {
            //this.rtfHeaderContent += Style.getRtfColorTable();
            string content = (this.rtfHeaderOpening + this.rtfHeaderContent
                + this.getRtfContentReferences() 
                + this.rtfClosing);
            this.clearCacheContent();
            return content;
        }
        void clearCacheContent()
        {
            this.rtfHeaderContent = "";
            this.rtfContentReferences = new List<Reference>();
        }
        string getRtfContentReferences()
        {
            string rtfReference = "";
            foreach (Reference value in this.rtfContentReferences)
            {
                
                rtfReference += value.content;
            }
            return rtfReference;
        }
        string swapHtmlStrangerTags(string html)
        {
            MatchEvaluator evaluator = new MatchEvaluator(WordReplace);
            
            
            string replace = Regex.Replace(html, @"<(\/?[a-z-]+)( *[^>]*)?>", evaluator, RegexOptions.IgnoreCase);
           
            return replace;
        }
        public static string WordReplace(Match match)
        {
            MatchEvaluator evaluator1 = new MatchEvaluator(CheckTag);

          
            string tag = Regex.Replace(match.Value, @"<(\/?[a-z-]+[0-9]?)", evaluator1, RegexOptions.IgnoreCase);
            

            return tag;
        }
        public static string CheckTag(Match match)
        {
            string tag = match.Value;
            if (match.Value.Contains('/'))
            {
                tag = tag.Remove(0, 1);
              

                if (!AllowedHtmlTags.isKnowedTag(tag))
                {

                    return "</p";
                }
                else return match.Value;
            }
            else
            {
                tag = tag.Remove(0, 1);
             
                if (!AllowedHtmlTags.isKnowedTag(tag))
                {
                   
                    return "<p";
                }
                else return match.Value;
            }

        }
    }
}
