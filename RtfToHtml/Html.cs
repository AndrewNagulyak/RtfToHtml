using Net.Sgoliver.NRtfTree.Core;
using Net.Sgoliver.NRtfTree.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RtfToHtml
{
    class Html
    {
        public string convertRtfToHtml(string rtf)
        {
            //string htmlWithoutStrangerTags = this.swapHtmlStrangerTags(html)
            return ConvertCode(rtf);

        }
        private StringBuilder _builder;
        private Format _htmlFormat;
        private List<Format> _formatList;
        private int spanCount = 0;
        private int divCount = 0;
        private bool hasHref = false;
        private RtfColorTable _colorTable;
        private RtfFontTable _fontTable;
        private bool _autoParagraph;
        private bool _ignoreFontNames;
        private bool _escapeHtmlEntities;
        private string _defaultFontName;
        private int _defaultFontSize;
        public Html()
        {
            AutoParagraph = false;
            IgnoreFontNames = false;
            DefaultFontSize = 10;
            //EscapeHtmlEntities = true;
            DefaultFontName = "Times New Roman";
        }
        public bool AutoParagraph
        {
            get
            {
                return _autoParagraph;
            }
            set
            {
                _autoParagraph = value;
            }
        }
        public bool IgnoreFontNames
        {
            get
            {
                return _ignoreFontNames;
            }
            set
            {
                _ignoreFontNames = value;
            }
        }
        public bool EscapeHtmlEntities
        {
            get
            {
                return _escapeHtmlEntities;
            }
            set
            {
                _escapeHtmlEntities = value;
            }
        }
        public string DefaultFontName
        {
            get
            {
                return _defaultFontName;
            }
            set
            {
                _defaultFontName = value;
            }
        }
        public int DefaultFontSize
        {
            get
            {
                return _defaultFontSize;
            }
            set
            {
                _defaultFontSize = value;
            }
        }


        /// <summary>
        /// Convierte una cadena de código RTF a formato HTML
        /// </summary>
        public static string ConvertCode(string rtf)
        {
            Html rtfToHtml = new Html();
            return rtfToHtml.Convert(rtf);
        }

        /// <summary>
        /// Convierte una cadena de código RTF a formato HTML
        /// </summary>
        public string Convert(string rtf)
        {
            //Console.WriteLine(rtf);
            //Generar arbol DOM
            RtfTree rtfTree = new RtfTree();
            rtfTree.LoadRtfText(rtf);
            Console.WriteLine(rtfTree.ToStringEx());
            //Inicializar variables empleadas
            _builder = new StringBuilder();
            _htmlFormat = new Format();
            this._builder.Append("<!DOCTYPE html><html><body> ");
            _formatList = new List<Format>();
            _formatList.Add(new Format());
            _fontTable = rtfTree.GetFontTable();
            _colorTable = rtfTree.GetColorTable();

            int inicio;
            for (inicio = 0; inicio < rtfTree.RootNode.FirstChild.ChildNodes.Count; inicio++)
            {

                if (rtfTree.RootNode.FirstChild.ChildNodes[inicio].NodeKey == "pard")
                {
                    break;
                }
            }
            //Procesar todos los nodos visibles
            TransformChildNodes(rtfTree.RootNode.FirstChild.ChildNodes, inicio);

            ProcessChildNodes(rtfTree.RootNode.FirstChild.ChildNodes, inicio);
            //Cerrar etiquetas pendientes
            _formatList.Last().Reset();
            WriteText(string.Empty);
            Regex repairList = new Regex("<span [^>]*>·</span><span style=\"([^\"]*)\">(.*?)<br\\s+/><" + "/span>",
                                         RegexOptions.IgnoreCase | RegexOptions.Singleline |
                                         RegexOptions.CultureInvariant);

            //foreach (Match match in repairList.Matches(_builder.ToString()))
            //{
            //    _builder.Replace(match.Value, string.Format("<li style=\"{0}\">{1}</li>", match.Groups[1].Value, match.Groups[2].Value));
            //}

            //Regex repairUl = new Regex("(?<!</li>)<li", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);

            //foreach (Match match in repairUl.Matches(_builder.ToString()))
            //{
            //    _builder.Insert(match.Index, "<ul>");
            //}

            //repairUl = new Regex("/li>(?!<li)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);

            //foreach (Match match in repairUl.Matches(_builder.ToString()))
            //{
            //    _builder.Insert(match.Index + match.Length, "</ul>");
            //}


            if (AutoParagraph)
            {
                string[] partes = _builder.ToString().Split(new[] { "<br /><br />" }, StringSplitOptions.RemoveEmptyEntries);
                _builder = new StringBuilder(_builder.Length + 7 * partes.Length);

                foreach (string parte in partes)
                {
                    _builder.Append("<p>");
                    _builder.Append(parte);
                    _builder.Append("</p>");
                }
            }
            this._builder.Append("</body></html>");

            //Console.WriteLine(_builder.ToString());
            return EscapeHtmlEntities ? HtmlEntities.Encode(_builder.ToString()) : _builder.ToString();

        }
        

        private void TransformChildNodes(RtfNodeCollection nodos, int inicio)
        {
            //for (int i =0;i<nodos.Count-1;i++)
            foreach(RtfTreeNode nod in nodos)
            {
                //if(nodo.)
                Console.WriteLine(nod.NodeKey);
                if (nod.NodeKey != "")
                {
                    switch (nod.NodeType)
                    {
                        case RtfNodeType.Group:
                            {
                                //Procesar nodos hijo, si los tiene
                                if (nod.HasChildNodes())
                                {

                                    TransformChildNodes(nod.ChildNodes, 0);


                                }
                                break;
                            }
                        case RtfNodeType.Keyword:
                            {
                                //Console.WriteLine(nod.NodeKey);
                                if(nod.NodeKey== "pnlvlblt")
                                {
                                    
                                    nod.ParentNode.ParentNode.NodeKey = "ul";
                                    foreach(RtfTreeNode node in nod.ParentNode.ParentNode.ChildNodes)
                                    {
                                     
                                        if(node.NodeType == RtfNodeType.Keyword)
                                        {
                                            
                                         //node.ParentNode.AppendChild(node);
                                        }
                                    }
                                    //Console.WriteLine(nod.ParentNode.NodeKey);
                                    nod.ParentNode.NodeKey = "";

                                }

                                if (nod.NodeKey == "pnlvlbody")
                                {

                                    nod.ParentNode.ParentNode.NodeKey = "ol";
                                    foreach (RtfTreeNode node in nod.ParentNode.ParentNode.ChildNodes)
                                    {

                                        if (node.NodeType == RtfNodeType.Keyword)
                                        {
                                             //node.ParentNode.ParentNode.AppendChild(node);
                                        }
                                    }
                                    nod.ParentNode.NodeKey = "";

                                }
                                if (nod.NodeKey=="pntext")
                                {
                                    nod.ParentNode.ParentNode.NodeKey = "li";
                                }
                                if (nod.NodeKey == "intbl")
                                {
                                    nod.ParentNode.NodeKey = "td";
                                    nod.ParentNode.ParentNode.NodeKey = "tr";
                                    nod.ParentNode.ParentNode.ParentNode.NodeKey = "table";

                                }
                                break;
                            }
                    }
                }
            }
        }


        private void ProcessChildNodes(RtfNodeCollection nodos, int inicio)
        {
            foreach (RtfTreeNode nodo in nodos)
            {
                //if(nodo.)
                // Console.WriteLine(nodo.NodeKey);
                if (nodo.NodeKey != "")
                {
                    switch (nodo.NodeType)
                    {

                        case RtfNodeType.Control:

                            if (nodo.NodeKey == "'")
                            {
                                Console.WriteLine(nodo.NodeKey);
                                WriteText(Encoding.Default.GetString(new[] { (byte)nodo.Parameter }));
                            }
                            break;

                        case RtfNodeType.Keyword:

                            switch (nodo.NodeKey)
                            {
                                case "pard":
                                    //Reinicio de formato
                                    //_formatList.Last().Reset();
                                    break;
                                case "pntext":
                                    {
                                        _formatList.Last().IsLi = true;
                                        break;
                                    }
                                case "f": //Tipo de fuente                                
                                    if (nodo.Parameter < _fontTable.Count)
                                        _formatList.Last().FontName = _fontTable[nodo.Parameter];
                                    break;

                                case "cf": //Color de fuente
                                    if (nodo.Parameter < _colorTable.Count )
                                        _formatList.Last().ForeColor = _colorTable[nodo.Parameter];
                                    break;

                                case "highlight": //Color de fondo
                                    if (nodo.Parameter < _colorTable.Count)
                                        _formatList.Last().BackColor = _colorTable[nodo.Parameter];
                                    break;

                                case "fs": //Tamaño de fuente
                                    _formatList.Last().FontSize = nodo.Parameter;
                                    break;

                                case "b": //Negrita
                                    _formatList.Last().Bold = !nodo.HasParameter || nodo.Parameter == 1;
                                    break;

                                case "i": //Cursiva
                                    _formatList.Last().Italic = !nodo.HasParameter || nodo.Parameter == 1;
                                    break;
                                case "em": //Cursiva
                                    _formatList.Last().Italic = !nodo.HasParameter || nodo.Parameter == 1;
                                    break;

                                case "ul": //Subrayado ON
                                    _formatList.Last().Underline = true;
                                    break;

                                case "ulnone": //Subrayado OFF
                                    _formatList.Last().Underline = false;
                                    break;

                                case "super": //Superscript
                                    _formatList.Last().Superscript = true;
                                    _formatList.Last().Subscript = false;
                                    break;
                                case "fonttbl":case "colortbl":
                                    {
                                        int i = nodo.ParentNode.ChildNodes.Count - 1;
                                        foreach (RtfTreeNode node in nodo.ParentNode.ChildNodes)
                                        {
                                            node.NodeKey = "";
                                            Console.WriteLine(node.NodeKey);
                                            i--;
                                        }
                                        break;
                                    }
                                case "sub": //Subindice
                                    _formatList.Last().Subscript = true;
                                    _formatList.Last().Superscript = false;
                                    break;

                                case "nosupersub":
                                    _formatList.Last().Superscript = _formatList.Last().Subscript = false;
                                    break;

                                case "qc": //Alineacion centrada
                                   if(nodo.ParentNode.NodeKey != "td")
                                    _formatList.Last().Alignment = HorizontalAlignment.Center;
                                    break;

                                case "qr": //Alineacion derecha
                                    if (nodo.ParentNode.NodeKey != "td")
                                        _formatList.Last().Alignment = HorizontalAlignment.Right;
                                    break;

                                case "li": //tabulacion
                                    _formatList.Last().Margin = nodo.Parameter;
                                    break;

                                case "line":
                                case "par": //Nueva línea
                                    _builder.Append("<br />");
                                    //_formatList.Last().Reset();

                                    break;
                                default:
                                    break;
                            }

                            break;

                        case RtfNodeType.Group:
                            {
                                
                                //Procesar nodos hijo, si los tiene
                                if (nodo.HasChildNodes())
                                {
                                    if (nodo.NodeKey == "ul")
                                    {
                                        _builder.Append("<ul>");
                                    }
                                    if (nodo.NodeKey == "ol")
                                    {
                                        _builder.Append("<ol>");
                                    }
                                    if (nodo.NodeKey == "td")
                                    {
                                        _builder.Append("<td>");
                                    }
                                    if (nodo.NodeKey == "table")
                                    {
                                        _builder.Append("<table>");
                                        _builder.Append("<tbody>");

                                    }
                                    if (nodo.NodeKey == "tr")
                                    {
                                        _builder.Append("<tr>");
                                    }
                                    if (nodo.NodeKey == "li")
                                    {
                                        _builder.Append("<li>");
                                    }
                                    else
                                    {
                                        if (_formatList.Last().IsOpen)
                                            WriteText("", false);
                                        else
                                            WriteText("", true);
                                    }
                                        _formatList.Add(new Format());
                                    
                                    ProcessChildNodes(nodo.ChildNodes, 0);

                                    if (nodo.NodeKey == "ul")
                                    {
                                        _builder.Append("</ul>");

                                    }
                                    if (nodo.NodeKey == "ol")
                                    {
                                        _builder.Append("</ol>");

                                    }
                                    if (nodo.NodeKey == "table")
                                    {
                                        _builder.Append("</tbody>");
                                        _builder.Append("</table>");

                                    }
                                    if (nodo.NodeKey == "td")
                                    {
                                        _builder.Append("</td>");

                                    }
                                    if (nodo.NodeKey=="li")
                                    {
                                        _builder.Append("</li>");
                                    }
                                    if (nodo.NodeKey == "tr")
                                    {
                                        _builder.Append("</tr>");
                                    }
                                    else
                                    {
                                        WriteText("close");
                                        hasHref = false;
                                        if (_formatList.Last().HasHref)
                                        {
                                            hasHref = true;
                                        }
                                    }
                                        _formatList.RemoveAt(_formatList.Count - 1);
                                        _htmlFormat.FontName = _formatList.Last().FontName;
                                        _htmlFormat.FontSize = _formatList.Last().FontSize;
                                        _htmlFormat.ForeColor = _formatList.Last().ForeColor;
                                        _htmlFormat.BackColor = _formatList.Last().BackColor;
                                        _htmlFormat.Margin = _formatList.Last().Margin;
                                        _htmlFormat.Alignment = _formatList.Last().Alignment;
                                    

                                }
                                break;
                            }

                        case RtfNodeType.Text:
                            {
                                string href = "";
                                if (nodo.NodeKey.Contains("HYPERLINK"))
                                {
                                    href = nodo.NodeKey.Replace("HYPERLINK", "<a href=").Replace("\\", "") + ">";
                                    _formatList.Last().HasHref = true;
                                    _builder.Append(href);
                                    _formatList.Last().IsOpen = true;
                                }
                                else
                                {
                                    Console.WriteLine(nodo.NodeKey);
                                    WriteText(nodo.NodeKey, false);
                                    _formatList.Last().IsOpen = true;
                                }
                            }
                            break;

                        default:

                            throw new ArgumentOutOfRangeException();
                    }
                }
            }
        }

        /// <summary>
        /// Función encargada de añadir texto con el formato actual al código html resultado
        /// </summary>
        private void WriteText(string text, bool update = true)
        {

            if (update)

            {
                if (_builder.Length > 0)
                {
                    //Cerrar etiquetas
                    if (_formatList.Last().Bold == true)
                    {
                        _builder.Append("</strong>");
                        _htmlFormat.Bold = false;
                    }
                    if (_formatList.Last().Italic == true)
                    {
                        _builder.Append("</em>");
                        _htmlFormat.Italic = false;
                    }
                    if (_formatList.Last().Underline == true)
                    {
                        _builder.Append("</u>");
                        _htmlFormat.Underline = false;
                    }
                    if (_formatList.Last().Subscript == true)
                    {
                        _builder.Append("</sub>");
                        _htmlFormat.Subscript = false;
                    }
                    if (_formatList.Last().Superscript == true)
                    {
                        _builder.Append("</sup>");
                        _htmlFormat.Superscript = false;
                    }
                    if (_formatList.Last().CompareFontFormat(_htmlFormat) == false || text.Contains("close") && _formatList.Last().SpanIsOpen) //El formato de fuente ha cambiado
                    {

                        if (spanCount > 0)
                        {
                            _builder.Append("</span>");

                            spanCount--;
                        }
                        if (divCount > 0)
                        {

                            _builder.Append("</div>");
                            divCount--;

                        }
                        //Reiniciar propiedades
                        _htmlFormat.Reset();
                    }
                }
            }
            if (!text.Contains("close"))
            {
                //Abrir etiquetas necesarias para representar el formato actual
                if (_formatList.Last().CompareFontFormat(_htmlFormat) == false) //El formato de fuente ha cambiado
                {
                    string estilo = string.Empty;

                    if (!IgnoreFontNames && !string.IsNullOrEmpty(_formatList.Last().FontName) &&
                        string.Compare(_formatList.Last().FontName, DefaultFontName, true) != 0)
                        estilo += string.Format("font-family:{0};", _formatList.Last().FontName);
                    if (_formatList.Last().FontSize > 0 && _formatList.Last().FontSize / 2 != DefaultFontSize)
                        estilo += string.Format("font-size:{0}pt;", _formatList.Last().FontSize / 2);
                    if (_formatList.Last().Margin != _htmlFormat.Margin)
                        estilo += string.Format("margin-left:{0}px;", _formatList.Last().Margin / 15);
                    if (_formatList.Last().Alignment != HorizontalAlignment.Left)
                        estilo += string.Format("text-align:{0};", _formatList.Last().Alignment.ToString().ToLower());
                    if (CompareColor(_formatList.Last().ForeColor, _htmlFormat.ForeColor) == false)
                        estilo += string.Format("color:{0};", ColorTranslator.ToHtml(_formatList.Last().ForeColor));
                    if (_formatList.Last().BackColor!=System.Drawing.Color.White)
                        estilo += string.Format("background-color:{0};", ColorTranslator.ToHtml(_formatList.Last().BackColor));

                    _htmlFormat.FontName = _formatList.Last().FontName;
                    _htmlFormat.FontSize = _formatList.Last().FontSize;
                    _htmlFormat.ForeColor = _formatList.Last().ForeColor;
                    _htmlFormat.BackColor = _formatList.Last().BackColor;
                    _htmlFormat.Margin = _formatList.Last().Margin;
                    _htmlFormat.Alignment = _formatList.Last().Alignment;

                    if (!string.IsNullOrEmpty(estilo))
                    {
                        if (estilo.Contains("text-align") && _formatList.Last().Alignment != HorizontalAlignment.Left)
                        {
                            Console.Write(estilo);
                            _formatList.Last().SpanIsOpen = true;
                            _builder.AppendFormat("<div style=\"{0}\">", estilo);
                            divCount++;
                        }
                        else
                        {
                            Console.Write(estilo);
                            _formatList.Last().SpanIsOpen = true;

                            _builder.AppendFormat("<span style=\"{0}\">", estilo);
                            spanCount++;
                        }
                    }
                }
                if (_formatList.Last().Superscript && _htmlFormat.Superscript == false)
                {
                    _builder.Append("<sup>");
                    _htmlFormat.Superscript = true;
                }
                if (_formatList.Last().Subscript && _htmlFormat.Subscript == false)
                {
                    _builder.Append("<sub>");
                    _htmlFormat.Subscript = true;
                }
                if (_formatList.Last().Underline && _htmlFormat.Underline == false)
                {
                    _builder.Append("<u>");
                    _htmlFormat.Underline = true;
                }
                if (_formatList.Last().Italic && _htmlFormat.Italic == false)
                {
                    _builder.Append("<em>");
                    _htmlFormat.Italic = true;
                }
                if (_formatList.Last().Bold && _htmlFormat.Bold == false)
                {
                    _builder.Append("<strong>");
                    _htmlFormat.Bold = true;
                }

            }
            if (text == "close")
            {
                text = "";
                if (hasHref)
                {
                    _builder.Append("</a>");
                }
            }
            _builder.Append(text.Replace("\"", "&quot;").Replace("<", "&lt;").Replace(">", "&gt;"));
            //_htmlFormat.Reset();

        }

        private static bool CompareColor(System.Drawing.Color a, System.Drawing.Color b)
        {
            return a.R == b.R && a.G == b.G && a.B == b.B;
        }


        private class Format
        {
            public bool Italic;
            public bool Bold;
            public bool Subscript;
            public bool Underline;
            public bool IsLi = false;
            public bool Superscript;
            public bool HasHref = false;
            public string FontName;
            public int FontSize;
            public System.Drawing.Color ForeColor;
            public System.Drawing.Color BackColor;
            public int Margin;
            public bool SpanIsOpen;
            public bool IsOpen;
            public HorizontalAlignment Alignment;

            public Format()
            {
                Reset();
            }

            /// <summary>
            /// Compara las propiedades FontName, FontSize, Margin, ForeColor, BackColor y Alignment con otro
            /// objeto de esta clase
            /// </summary>
            public bool CompareFontFormat(Format format)
            {
                return string.Compare(FontName, format.FontName, true) == 0 &&
                       FontSize == format.FontSize &&
                       ForeColor == format.ForeColor &&
                       BackColor == format.BackColor &&
                       Margin == format.Margin &&
                       Alignment == format.Alignment;
            }

            public void Reset()
            {
                FontName = string.Empty;
                FontSize = 0;
                Bold = false;
                Subscript = false;
                Italic = false;
                ForeColor = System.Drawing.Color.Black;
                BackColor = System.Drawing.Color.White;
                Underline = false;
                Margin = 0;
                SpanIsOpen = false;
                Alignment = HorizontalAlignment.Left;
                IsOpen = false;
            }
        }

        private enum HorizontalAlignment
        {
            Left,
            Right,
            Center
        }

    }
}
