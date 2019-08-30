using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtfToHtml
{
    static class Alignment
    {
        public static AlignmentAttribute[] alignmentReferenceList = new AlignmentAttribute[]{
            new AlignmentAttribute()
            { name= "center",   reference="\\qc" },
            new AlignmentAttribute()

            { name="left",     reference="\\ql" },
            new AlignmentAttribute()

             { name="right",    reference="\\qr" },
            new AlignmentAttribute()

            { name="justify",  reference="\\qj" }
        };
        public static string getRtfAlignmentReference(string propertyName)
        {
            string alignmentReference = "";
            foreach(AlignmentAttribute element in alignmentReferenceList)
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
