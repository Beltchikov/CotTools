using Aspose.Cells;
using System;
using System.Collections.Generic;
using System.Text;

namespace CotTools
{
    /// <summary>
    /// CftcFinancialsWorkbook
    /// </summary>
    public class CftcFinancialsWorkbook
    {
        private Workbook _workbook;

        private const string COLUMN_DATE = "Report_Date_as_MM_DD_YYYY";
        private const string COLUMN_DEALER_LONG = "Dealer_Positions_Long_All";
        private const string COLUMN_DEALER_SHORT = "Dealer_Positions_Short_All";
        private const string COLUMN_ASSETMANAGER_LONG = "Asset_Mgr_Positions_Long_All";
        private const string COLUMN_ASSETMANAGER_SHORT = "Asset_Mgr_Positions_Short_All";
        private const string LEVERAGED_LONG = "Lev_Money_Positions_Long_All";
        private const string LEVERAGED_SHORT = "Lev_Money_Positions_Short_All";

        /// <summary>
        /// CftcFinancialsWorkbook
        /// </summary>
        /// <param name="fileWithPath"></param>
        public CftcFinancialsWorkbook(string fileWithPath)
        {
            _workbook = new Workbook(fileWithPath);
        }

        public Worksheet FirstWorksheet => _workbook.Worksheets[0];
        public int IndexOfDate => IndexOfColumn(COLUMN_DATE);
        public int IndexOfDealerLong => IndexOfColumn(COLUMN_DEALER_LONG);
        public int IndexOfDealerShort => IndexOfColumn(COLUMN_DEALER_SHORT);
        public int IndexOfAssetManagerLong => IndexOfColumn(COLUMN_ASSETMANAGER_LONG);
        public int IndexOfAssetManagerShort => IndexOfColumn(COLUMN_ASSETMANAGER_SHORT);
        public int IndexOfLeveragedLong => IndexOfColumn(LEVERAGED_LONG);
        public int IndexOfLeveragedShort => IndexOfColumn(LEVERAGED_SHORT);

        /// <summary>
        /// IndexOfColumn
        /// </summary>
        /// <param name="columnString"></param>
        /// <returns>Returns -1 if column header not found.</returns>
        private int IndexOfColumn(string columnString)
        {
            Cell cell = FirstWorksheet.Cells.Find(columnString, null);
            if (cell == null)
            {
                return -1;
            }

            return cell.Column;
        }
    }
}

