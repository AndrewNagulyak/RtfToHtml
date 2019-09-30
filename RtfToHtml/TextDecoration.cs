using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtfToHtml
{
   public static class TextDecoration
    {
        public static DecorationAttribute[] textDecorationReferenceList = new DecorationAttribute[]{
            new DecorationAttribute()
            { name= "underline",   reference="\\ul" },
            new DecorationAttribute()

            { name="overline",     reference="\\ol" },
            new DecorationAttribute()

             { name="line-through",    reference="\\strike" },
        };
        public static string getRtfTextDecorationReference(string propertyName)
        {
            string alignmentReference = "";
            foreach (DecorationAttribute element in textDecorationReferenceList)
            {
                if (element.name == propertyName.Trim())
                {
                    alignmentReference = element.reference;
                }
            }

            return alignmentReference;
        }
    }
}
