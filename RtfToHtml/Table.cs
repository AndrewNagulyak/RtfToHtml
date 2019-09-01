using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtfToHtml
{
    public class Table
    {
        public string rtfReferenceRow;
        public int amountOfColumns;
        public int defaultLengthOfPageInTwips;
        public Table()
        {
            this.rtfReferenceRow = "\\clbrdrt\\brdrw15\\brdrs\\clbrdrl\\brdrw15\\brdrs\\clbrdrb\\brdrw15\\brdrs\\clbrdrr\\brdrw15\\brdrs\\cellx";
            this.amountOfColumns = 0;
            this.defaultLengthOfPageInTwips = 8503;
        }

        public void setAmountOfColumns(int amountOfColumns)
        {
            this.amountOfColumns = amountOfColumns;
        }

        public int getAmountOfColumns()
        {
            return this.amountOfColumns;
        }

        public double getCellLength()
        {
            return Math.Floor((double)(this.defaultLengthOfPageInTwips / this.amountOfColumns));
        }

        public string getRtfReferenceRow()
        {
            return this.rtfReferenceRow;
        }

        public string buildCellsLengthOfEachColumn()
        {
            string cellGroup = "";
            for (int columnNumber = 0; columnNumber < this.amountOfColumns; columnNumber++)
            {

                cellGroup += this.rtfReferenceRow + (this.getCellLength() * columnNumber + this.getCellLength());

            }
            return cellGroup;
        }
    }
}
