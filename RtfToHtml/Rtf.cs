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
        Table table = null;

        string rtfHeaderOpening;
        string rtfHeaderContent;
        string rtfClosing;
        HtmlNode prevTag = null;

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

            if (fatherTag.ChildNodes != null)
            {

                this.addOpeningTagInRtfCode(fatherTag.Name);
                this.ifExistsAttributesAddAllReferencesInRtfCode(fatherTag.Attributes);

                if (fatherTag.Name.ToLower() == "table")
                {
                    table = new Table();
                    table.setAmountOfColumns(this.getAmountOfColumnThroughOfFirstChildOfTbodyTag(fatherTag.ChildNodes));
                }

                if (fatherTag.Name.ToLower() == "tr")
                {
                    if (table != null)
                        this.addReferenceTagInRtfCode(table.buildCellsLengthOfEachColumn());
                }

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
                        if (node.NextSibling != null)
                            if ((node.NextSibling.Name.ToLower() == "p"
                               || node.NextSibling.Name.ToLower() == "h1"
                               || node.NextSibling.Name.ToLower() == "h2"
                               || node.NextSibling.Name.ToLower() == "h3"
                               || node.NextSibling.Name.ToLower() == "h4"
                               || node.NextSibling.Name.ToLower() == "h5"
                               || node.NextSibling.Name.ToLower() == "h6")&& !String.IsNullOrWhiteSpace(node.InnerText))
                            {
                                string text = node.InnerText;
                                text = MyString.removeCharacterOfEscapeInAllString(text, "\n\t");

                                if (text != null && !MyString.hasOnlyWhiteSpace(text))
                                    this.rtfContentReferences.Add(new Reference() { content = this.addSpaceAroundString(text.Trim())+"{\\par}", tag = false });

                                //this.addContentOfTagInRtfCode(text.Trim() + "{\\par}");

                            }
                            else
                            {
                                this.addContentOfTagInRtfCode(node.InnerText.Trim());

                            }
                        else
                        {
                            this.addContentOfTagInRtfCode(node.InnerText.Trim());

                        }

                        //Console.WriteLine(node.InnerText);
                    }
                }

            }
            //Console.WriteLine(fatherTag.OriginalName);
            if (fatherTag.OriginalName == "a" && !fatherTag.Attributes.Contains("href"))
            {
                this.addReferenceTagInRtfCode("}");
            }
            else
            {
                this.addClosingFatherTagInRtfCode(fatherTag.OriginalName);
            }
            prevTag = fatherTag;
        }
        int getAmountOfColumnThroughOfFirstChildOfTbodyTag(HtmlNodeCollection childs)
        {
            int count = 0;
            HtmlNode tbodyIndex;
            foreach (var element in childs)
            {
                if (element.Name == "tbody")
                {
                    tbodyIndex = element;
                    foreach (var node in element.ChildNodes)
                    {

                        if (node.OriginalName != "#text")
                        {
                            foreach (var tr in node.ChildNodes)
                            {
                                if (tr.OriginalName != "#text")
                                    count++;
                            }
                            break;
                        }
                    }
                    break;

                }

            }
            Console.WriteLine(count);
            return count;

        }
        void ifExistsAttributesAddAllReferencesInRtfCode(HtmlAttributeCollection attributes)
        {
            foreach (HtmlAttribute attribute in attributes)
            {
                if (attribute.OriginalName == "align" || attribute.OriginalName == "text-align")
                {
                    this.addReferenceTagInRtfCode(Style.getRtfAlignmentReference(attribute.Value));
                }
                if (attribute.OriginalName == "style")
                {
                    this.addReferenceTagInRtfCode(Style.getRtfReferencesInStyleProperty(attribute.Value));
                }
                if (attribute.OriginalName == "href")
                {
                    this.addReferenceTagInRtfCode("{\\field{\\*\\fldinst HYPERLINK " + @"""" + attribute.Value + @"""" + "}{\\fldrslt{\\ul\\cf3");
                }
            }
            //if (attributes.s != null)
            //    this.addReferenceTagInRtfCode(Style.getRtfReferencesInStyleProperty(attributes.style));

        }



        void addClosingFatherTagInRtfCode(string closingFatherTag)
        {
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
            Console.WriteLine(contentOfTag + "wrapped");
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
            this.rtfHeaderContent += Style.getRtfColorTable();
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

                    return "</html";
                }
                else return match.Value;
            }
            else
            {
                tag = tag.Remove(0, 1);

                if (!AllowedHtmlTags.isKnowedTag(tag))
                {

                    return "<html";
                }
                else return match.Value;
            }

        }
    }
}
