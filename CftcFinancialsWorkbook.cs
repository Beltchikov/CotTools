using Aspose.Cells;
using System;
using System.Collections.Generic;
using System.Text;

namespace CotTools
{
    public class CftcFinancialsWorkbook
    {
        Workbook _workbook;
        public CftcFinancialsWorkbook(string fileWithPath)
        {
            _workbook = new Workbook(fileWithPath);
        }
        
        public Worksheet FirstWorksheet => _workbook.Worksheets[0];

        public const string COLUMN_DATE = "Report_Date_as_MM_DD_YYYY";
        public const string COLUMN_DEALER_LONG = "Dealer_Positions_Long_All";
        public const string COLUMN_DEALER_SHORT = "Dealer_Positions_Short_All";


    }
}

