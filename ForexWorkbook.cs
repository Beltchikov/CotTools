using Aspose.Cells;
using System;
using System.Collections.Generic;
using System.Text;

namespace CotTools
{
    public class ForexWorkbook : Workbook
    {
        Workbook _workbook;
        public ForexWorkbook(string fileWithPath)
        {
            _workbook = new Workbook(fileWithPath);
        }

        /// <summary>
        /// Validate
        /// </summary>
        /// <returns Returns empty string if validation succesfull.</returns>
        public string Validate()
        {
            if (WorksheetEur == null)
            {
                return $"Worksheet {Forex.EUR} missing";
            }
            if (WorksheetAud == null)
            {
                return $"Worksheet {Forex.AUD} missing";
            }
            if (WorksheetCad == null)
            {
                return $"Worksheet {Forex.CAD} missing";
            }
            if (WorksheetChf == null)
            {
                return $"Worksheet {Forex.CHF} missing";
            }
            if (WorksheetGbp == null)
            {
                return $"Worksheet {Forex.GBP} missing";
            }
            if (WorksheetJpy == null)
            {
                return $"Worksheet {Forex.JPY} missing";
            }

            return string.Empty;
        }

        public Worksheet WorksheetEur => _workbook.Worksheets[Forex.EUR];
        public Worksheet WorksheetAud => _workbook.Worksheets[Forex.AUD];
        public Worksheet WorksheetCad => _workbook.Worksheets[Forex.CAD];
        public Worksheet WorksheetChf => _workbook.Worksheets[Forex.CHF];
        public Worksheet WorksheetGbp => _workbook.Worksheets[Forex.GBP];
        public Worksheet WorksheetJpy => _workbook.Worksheets[Forex.JPY];
    }
}
