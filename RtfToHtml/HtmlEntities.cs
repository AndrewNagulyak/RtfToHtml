using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtfToHtml
{
    static class HtmlEntities
    {
        /// <summary>
        /// Lista de reemplazos caracter-entidad
        /// </summary>
        private static Dictionary<string, string> reemplazos;

        /// <summary>
        /// Codifica un texto reemplazando todos los caracteres especiales por su entidad HTML correspondiente.
        /// </summary>
        /// <param name="texto">Texto a codificar.</param>
        /// <returns>Texto codificado.</returns>
        public static string Encode(string texto)
        {
            string res = texto;

            foreach (KeyValuePair<string, string> reemplazo in reemplazos)
            {
                res = res.Replace(reemplazo.Key, reemplazo.Value);
            }

            return res;
        }

        /// <summary>
        /// Decodifica un texto reemplazando todas las entidades HTML por su caracter especial correspondiente.
        /// </summary>
        /// <param name="texto">Texto a decodificar.</param>
        /// <returns>Texto decodificado.</returns>
        public static string Decode(string texto)
        {
            string res = texto;

            foreach (KeyValuePair<string, string> reemplazo in reemplazos)
            {
                res = res.Replace(reemplazo.Value, reemplazo.Key);
            }

            return res;
        }

        /// <summary>
        /// Constructor de la clase HtmlEntities
        /// </summary>
        static HtmlEntities()
        {
            reemplazos = new Dictionary<string, string>();

            //reemplazos.Add("\"", "&quot;");
            //reemplazos.Add("<", "&lt;");
            //reemplazos.Add(">", "&gt;");
            reemplazos.Add("¡", "&iexcl;");
            reemplazos.Add("¢", "&cent;");
            reemplazos.Add("£", "&pound;");
            reemplazos.Add("¤", "&curren;");
            reemplazos.Add("¥", "&yen;");
            reemplazos.Add("¦", "&brvbar;");
            reemplazos.Add("§", "&sect;");
            reemplazos.Add("¨", "&uml;");
            reemplazos.Add("©", "&copy;");
            reemplazos.Add("ª", "&ordf;");
            reemplazos.Add("«", "&laquo;");
            reemplazos.Add("¬", "&not;");
            reemplazos.Add("­", "&shy;");
            reemplazos.Add("®", "&reg;");
            reemplazos.Add("¯", "&macr;");
            reemplazos.Add("°", "&deg;");
            reemplazos.Add("±", "&plusmn;");
            reemplazos.Add("²", "&sup2;");
            reemplazos.Add("³", "&sup3;");
            reemplazos.Add("´", "&acute;");
            reemplazos.Add("µ", "&micro;");
            reemplazos.Add("¶", "&para;");
            reemplazos.Add("·", "&middot;");
            reemplazos.Add("¸", "&cedil;");
            reemplazos.Add("¹", "&sup1;");
            reemplazos.Add("º", "&ordm;");
            reemplazos.Add("»", "&raquo;");
            reemplazos.Add("¼", "&frac14;");
            reemplazos.Add("½", "&frac12;");
            reemplazos.Add("¾", "&frac34;");
            reemplazos.Add("¿", "&iquest;");
            reemplazos.Add("À", "&Agrave;");
            reemplazos.Add("Á", "&Aacute;");
            reemplazos.Add("Â", "&Acirc;");
            reemplazos.Add("Ã", "&Atilde;");
            reemplazos.Add("Ä", "&Auml;");
            reemplazos.Add("Å", "&Aring;");
            reemplazos.Add("Æ", "&AElig;");
            reemplazos.Add("Ç", "&Ccedil;");
            reemplazos.Add("È", "&Egrave;");
            reemplazos.Add("É", "&Eacute;");
            reemplazos.Add("Ê", "&Ecirc;");
            reemplazos.Add("Ë", "&Euml;");
            reemplazos.Add("Ì", "&Igrave;");
            reemplazos.Add("Í", "&Iacute;");
            reemplazos.Add("Î", "&Icirc;");
            reemplazos.Add("Ï", "&Iuml;");
            reemplazos.Add("Ð", "&ETH;");
            reemplazos.Add("Ñ", "&Ntilde;");
            reemplazos.Add("Ò", "&Ograve;");
            reemplazos.Add("Ó", "&Oacute;");
            reemplazos.Add("Ô", "&Ocirc;");
            reemplazos.Add("Õ", "&Otilde;");
            reemplazos.Add("Ö", "&Ouml;");
            reemplazos.Add("×", "&times;");
            reemplazos.Add("Ø", "&Oslash;");
            reemplazos.Add("Ù", "&Ugrave;");
            reemplazos.Add("Ú", "&Uacute;");
            reemplazos.Add("Û", "&Ucirc;");
            reemplazos.Add("Ü", "&Uuml;");
            reemplazos.Add("Ý", "&Yacute;");
            reemplazos.Add("Þ", "&THORN;");
            reemplazos.Add("ß", "&szlig;");
            reemplazos.Add("à", "&agrave;");
            reemplazos.Add("á", "&aacute;");
            reemplazos.Add("â", "&acirc;");
            reemplazos.Add("ã", "&atilde;");
            reemplazos.Add("ä", "&auml;");
            reemplazos.Add("å", "&aring;");
            reemplazos.Add("æ", "&aelig;");
            reemplazos.Add("ç", "&ccedil;");
            reemplazos.Add("è", "&egrave;");
            reemplazos.Add("é", "&eacute;");
            reemplazos.Add("ê", "&ecirc;");
            reemplazos.Add("ë", "&euml;");
            reemplazos.Add("ì", "&igrave;");
            reemplazos.Add("í", "&iacute;");
            reemplazos.Add("î", "&icirc;");
            reemplazos.Add("ï", "&iuml;");
            reemplazos.Add("ð", "&eth;");
            reemplazos.Add("ñ", "&ntilde;");
            reemplazos.Add("ò", "&ograve;");
            reemplazos.Add("ó", "&oacute;");
            reemplazos.Add("ô", "&ocirc;");
            reemplazos.Add("õ", "&otilde;");
            reemplazos.Add("ö", "&ouml;");
            reemplazos.Add("÷", "&divide;");
            reemplazos.Add("ø", "&oslash;");
            reemplazos.Add("ù", "&ugrave;");
            reemplazos.Add("ú", "&uacute;");
            reemplazos.Add("û", "&ucirc;");
            reemplazos.Add("ü", "&uuml;");
            reemplazos.Add("ý", "&yacute;");
            reemplazos.Add("þ", "&thorn;");
            reemplazos.Add("ÿ", "&yuml;");
            reemplazos.Add("ƒ", "&fnof;");
            reemplazos.Add("Α", "&Alpha;");
            reemplazos.Add("Β", "&Beta;");
            reemplazos.Add("Γ", "&Gamma;");
            reemplazos.Add("Δ", "&Delta;");
            reemplazos.Add("Ε", "&Epsilon;");
            reemplazos.Add("Ζ", "&Zeta;");
            reemplazos.Add("Η", "&Eta;");
            reemplazos.Add("Θ", "&Theta;");
            reemplazos.Add("Ι", "&Iota;");
            reemplazos.Add("Κ", "&Kappa;");
            reemplazos.Add("Λ", "&Lambda;");
            reemplazos.Add("Μ", "&Mu;");
            reemplazos.Add("Ν", "&Nu;");
            reemplazos.Add("Ξ", "&Xi;");
            reemplazos.Add("Ο", "&Omicron;");
            reemplazos.Add("Π", "&Pi;");
            reemplazos.Add("Ρ", "&Rho;");
            reemplazos.Add("Σ", "&Sigma;");
            reemplazos.Add("Τ", "&Tau;");
            reemplazos.Add("Υ", "&Upsilon;");
            reemplazos.Add("Φ", "&Phi;");
            reemplazos.Add("Χ", "&Chi;");
            reemplazos.Add("Ψ", "&Psi;");
            reemplazos.Add("Ω", "&Omega;");
            reemplazos.Add("α", "&alpha;");
            reemplazos.Add("β", "&beta;");
            reemplazos.Add("γ", "&gamma;");
            reemplazos.Add("δ", "&delta;");
            reemplazos.Add("ε", "&epsilon;");
            reemplazos.Add("ζ", "&zeta;");
            reemplazos.Add("η", "&eta;");
            reemplazos.Add("θ", "&theta;");
            reemplazos.Add("ι", "&iota;");
            reemplazos.Add("κ", "&kappa;");
            reemplazos.Add("λ", "&lambda;");
            reemplazos.Add("μ", "&mu;");
            reemplazos.Add("ν", "&nu;");
            reemplazos.Add("ξ", "&xi;");
            reemplazos.Add("ο", "&omicron;");
            reemplazos.Add("π", "&pi;");
            reemplazos.Add("ρ", "&rho;");
            reemplazos.Add("ς", "&sigmaf;");
            reemplazos.Add("σ", "&sigma;");
            reemplazos.Add("τ", "&tau;");
            reemplazos.Add("υ", "&upsilon;");
            reemplazos.Add("φ", "&phi;");
            reemplazos.Add("χ", "&chi;");
            reemplazos.Add("ψ", "&psi;");
            reemplazos.Add("ω", "&omega;");
            reemplazos.Add("ϑ", "&thetasym;");
            reemplazos.Add("ϒ", "&upsih;");
            reemplazos.Add("ϖ", "&piv;");
            reemplazos.Add("•", "&bull;");
            reemplazos.Add("…", "&hellip;");
            reemplazos.Add("′", "&prime;");
            reemplazos.Add("″", "&Prime;");
            reemplazos.Add("‾", "&oline;");
            reemplazos.Add("⁄", "&frasl;");
            reemplazos.Add("℘", "&weierp;");
            reemplazos.Add("ℑ", "&image;");
            reemplazos.Add("ℜ", "&real;");
            reemplazos.Add("™", "&trade;");
            reemplazos.Add("ℵ", "&alefsym;");
            reemplazos.Add("←", "&larr;");
            reemplazos.Add("↑", "&uarr;");
            reemplazos.Add("→", "&rarr;");
            reemplazos.Add("↓", "&darr;");
            reemplazos.Add("↔", "&harr;");
            reemplazos.Add("↵", "&crarr;");
            reemplazos.Add("⇐", "&lArr;");
            reemplazos.Add("⇑", "&uArr;");
            reemplazos.Add("⇒", "&rArr;");
            reemplazos.Add("⇓", "&dArr;");
            reemplazos.Add("⇔", "&hArr;");
            reemplazos.Add("∀", "&forall;");
            reemplazos.Add("∂", "&part;");
            reemplazos.Add("∃", "&exist;");
            reemplazos.Add("∅", "&empty;");
            reemplazos.Add("∇", "&nabla;");
            reemplazos.Add("∈", "&isin;");
            reemplazos.Add("∉", "&notin;");
            reemplazos.Add("∋", "&ni;");
            reemplazos.Add("∏", "&prod;");
            reemplazos.Add("∑", "&sum;");
            reemplazos.Add("−", "&minus;");
            reemplazos.Add("∗", "&lowast;");
            reemplazos.Add("√", "&radic;");
            reemplazos.Add("∝", "&prop;");
            reemplazos.Add("∞", "&infin;");
            reemplazos.Add("∠", "&ang;");
            reemplazos.Add("∧", "&and;");
            reemplazos.Add("∨", "&or;");
            reemplazos.Add("∩", "&cap;");
            reemplazos.Add("∪", "&cup;");
            reemplazos.Add("∫", "&int;");
            reemplazos.Add("∴", "&there4;");
            reemplazos.Add("∼", "&sim;");
            reemplazos.Add("≅", "&cong;");
            reemplazos.Add("≈", "&asymp;");
            reemplazos.Add("≠", "&ne;");
            reemplazos.Add("≡", "&equiv;");
            reemplazos.Add("≤", "&le;");
            reemplazos.Add("≥", "&ge;");
            reemplazos.Add("⊂", "&sub;");
            reemplazos.Add("⊃", "&sup;");
            reemplazos.Add("⊄", "&nsub;");
            reemplazos.Add("⊆", "&sube;");
            reemplazos.Add("⊇", "&supe;");
            reemplazos.Add("⊕", "&oplus;");
            reemplazos.Add("⊗", "&otimes;");
            reemplazos.Add("⊥", "&perp;");
            reemplazos.Add("⋅", "&sdot;");
            reemplazos.Add("⌈", "&lceil;");
            reemplazos.Add("⌉", "&rceil;");
            reemplazos.Add("⌊", "&lfloor;");
            reemplazos.Add("⌋", "&rfloor;");
            reemplazos.Add("〈", "&lang;");
            reemplazos.Add("〉", "&rang;");
            reemplazos.Add("♠", "&spades;");
            reemplazos.Add("♣", "&clubs;");
            reemplazos.Add("♥", "&hearts;");
            reemplazos.Add("♦", "&diams;");
            reemplazos.Add("Œ", "&OElig;");
            reemplazos.Add("œ", "&oelig;");
            reemplazos.Add("Š", "&Scaron;");
            reemplazos.Add("š", "&scaron;");
            reemplazos.Add("Ÿ", "&Yuml;");
            reemplazos.Add("ˆ", "&circ;");
            reemplazos.Add("˜", "&tilde;");
            reemplazos.Add("‌", "&zwnj;");
            reemplazos.Add("‍", "&zwj;");
            reemplazos.Add("‎", "&lrm;");
            reemplazos.Add("‏", "&rlm;");
            reemplazos.Add("–", "&ndash;");
            reemplazos.Add("—", "&mdash;");
            reemplazos.Add("‘", "&lsquo;");
            reemplazos.Add("’", "&rsquo;");
            reemplazos.Add("‚", "&sbquo;");
            reemplazos.Add("“", "&ldquo;");
            reemplazos.Add("”", "&rdquo;");
            reemplazos.Add("„", "&bdquo;");
            reemplazos.Add("†", "&dagger;");
            reemplazos.Add("‡", "&Dagger;");
            reemplazos.Add("‰", "&permil;");
            reemplazos.Add("‹", "&lsaquo;");
            reemplazos.Add("›", "&rsaquo;");
            reemplazos.Add("€", "&euro;");
        }
    }
}
