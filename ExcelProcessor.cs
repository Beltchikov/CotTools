using Aspose.Cells;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;

namespace CotTools
{
    /// <summary>
    /// ExcelProcessor
    /// </summary>
    public class ExcelProcessor
    {
        /// <summary>
        /// GetAssets
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        internal static List<string> GetAssets(string fileName)
        {
            List<string> resultList = new List<string>();

            Workbook workbook = new Workbook(fileName);
            Cells cells = workbook.Worksheets[0].Cells;
            int rowCount = cells.MaxDataRow;
            cells.RemoveDuplicates(1, 0, rowCount, 0);

            for (int row = 1; row <= rowCount; row++)
            {
                var cellValue = cells[row, 0].Value;
                if (cellValue == null)
                {
                    continue;
                }
                resultList.Add(cellValue.ToString());
            }

            return resultList;
        }

        /// <summary>
        /// Financials
        /// </summary>
        public partial class Financials : ConverterBase
        {
            internal delegate string Logic(Cells cells, int columnDate, int columnLong, int columnShort, string asset, bool invert);

            static Logic processingLogic = (cells, columnDate, columnLong, columnShort, asset, invert) =>
                {
                    // For each line in Excel
                    int rowCount = cells.MaxDataRow;
                    for (int row = 0; row <= rowCount; row++)
                    {
                        var asssetString = cells[row, 0].Value.ToString();
                        if (asssetString != asset)
                        {
                            continue;
                        }

                        var dateString = cells[row, columnDate].Value.ToString();
                        var date = DateTime.ParseExact(dateString, "dd.MM.yyyy HH:mm:ss", null);
                        var longValue = Convert.ToInt32(cells[row, columnLong].Value);
                        var shortValue = Convert.ToInt32(cells[row, columnShort].Value);
                        var netValue = invert  ? - 1 * (longValue - shortValue) : longValue - shortValue;

                        // Fill string builder
                        stringBuilder.Append($"{date.ToString("dd.MM.yyyy")}{SEPARATOR}{netValue}{Environment.NewLine}");
                    }

                    return stringBuilder.ToString();
                };

            /// <summary>
            /// ProcessDealerInverted
            /// </summary>
            /// <param name="fileName"></param>
            /// <param name="asset"></param>
            /// <returns></returns>
            internal static string ProcessDealerInverted(string fileName, object asset)
            {
                CftcFinancialsWorkbook workbook = new CftcFinancialsWorkbook(fileName);

                var colDate = workbook.IndexOfDate;
                var colLong = workbook.IndexOfDealerLong;
                var colShort = workbook.IndexOfDealerShort;
                stringBuilder.Clear();

                return processingLogic(workbook.FirstWorksheet.Cells, colDate, colLong, colShort, (string)asset, true);
            }

            /// <summary>
            /// ProcessDealerInvertedChange
            /// </summary>
            /// <param name="fileName"></param>
            /// <returns></returns>
            internal static string ProcessDealerInvertedChange(string fileName)
            {
                throw new NotImplementedException();

                //CftcFinancialsWorkbook workbook = new CftcFinancialsWorkbook(fileName);
                //var colDate = workbook.IndexOfDate;
                //var colLong = workbook.IndexOfDealerLong;
                //var colShort = workbook.IndexOfDealerShort;
                //stringBuilder.Clear();

                //Logic logic = (cells, columnDate, columnLong, columnShort) =>
                //{
                //    // For each line in Excel
                //    int rowCount = cells.MaxDataRow;
                //    int netValueBefore = 0, changeValue;
                //    for (int row = rowCount; row >= 1; row--)
                //    {
                //        var dateString = cells[row, columnDate].Value.ToString();
                //        var date = DateTime.ParseExact(dateString, "dd.MM.yyyy HH:mm:ss", null);
                //        var longValue = Convert.ToInt32(cells[row, columnLong].Value);
                //        var shortValue = Convert.ToInt32(cells[row, columnShort].Value);
                //        var netValue = -1 * (longValue - shortValue);

                //        if (row == rowCount)
                //        {
                //            netValueBefore = netValue;
                //            continue;
                //        }
                //        else
                //        {
                //            changeValue = netValue - netValueBefore;
                //        }

                //        // Fill string builder
                //        stringBuilder.Append($"{date.ToString("dd.MM.yyyy")}{SEPARATOR}{changeValue}{Environment.NewLine}");
                //    }
                //    return stringBuilder.ToString();
                //};

                //return logic(workbook.FirstWorksheet.Cells, colDate, colLong, colShort);
            }

            internal static string ProcessAssetManager(string fileName, object asset)
            {
                CftcFinancialsWorkbook workbook = new CftcFinancialsWorkbook(fileName);

                var colDate = workbook.IndexOfDate;
                var colLong = workbook.IndexOfAssetManagerLong;
                var colShort = workbook.IndexOfAssetManagerShort;
                stringBuilder.Clear();

                return processingLogic(workbook.FirstWorksheet.Cells, colDate, colLong, colShort, (string)asset, false);
            }

            internal static string ProcessAssetManagerChange(string fileName)
            {
                throw new NotImplementedException();
            }

            internal static string ProcessLeveraged(string fileName)
            {
                throw new NotImplementedException();
            }

            internal static string ProcessLeveragedChange(string fileName)
            {
                throw new NotImplementedException();
            }

            /// <summary>
            /// ProcessForexNet
            /// </summary>
            /// <param name="fileWithPath"></param>
            /// <param name="dateColumnIndex"></param>
            /// <param name="columnIndex"></param>
            /// <param name="invertResults"></param>
            /// <returns></returns>
            public static string ProcessForexNet(string fileWithPath, int dateColumnIndex, int columnIndex, bool invertResults)
            {
                ForexWorkbook forexWorkbook = new ForexWorkbook(fileWithPath);

                // Validate
                string message;
                if ((message = forexWorkbook.Validate()) != string.Empty)
                {
                    return message;
                }

                // For each line in Excel
                Cells cellsEur = forexWorkbook.WorksheetEur.Cells;
                int rowCountEur = cellsEur.MaxDataRow;
                for (int row = 1; row <= rowCountEur; row++)
                {
                    // Data from different worksheets in dictionary
                    Dictionary<string, int> dictCurrencyValue = new Dictionary<string, int>();
                    dictCurrencyValue[Forex.EUR] = cellsEur[row, columnIndex].Value.ToNetValue();
                    dictCurrencyValue[Forex.AUD] = forexWorkbook.WorksheetAud.Cells[row, columnIndex].Value.ToNetValue();
                    dictCurrencyValue[Forex.CAD] = forexWorkbook.WorksheetCad.Cells[row, columnIndex].Value.ToNetValue();
                    dictCurrencyValue[Forex.CHF] = forexWorkbook.WorksheetChf.Cells[row, columnIndex].Value.ToNetValue();
                    dictCurrencyValue[Forex.GBP] = forexWorkbook.WorksheetGbp.Cells[row, columnIndex].Value.ToNetValue();
                    dictCurrencyValue[Forex.JPY] = forexWorkbook.WorksheetJpy.Cells[row, columnIndex].Value.ToNetValue();
                    dictCurrencyValue[Forex.USD] = (int)Math.Round((double)(dictCurrencyValue[Forex.EUR] + dictCurrencyValue[Forex.AUD] + dictCurrencyValue[Forex.CAD] + dictCurrencyValue[Forex.CHF]
                        + dictCurrencyValue[Forex.GBP] + dictCurrencyValue[Forex.JPY]) / -6);

                    // Currencies mit max/min values
                    var max = invertResults
                       ? GetCurrencyOfMaxMinValue(dictCurrencyValue, int.MaxValue, (a, b) => a < b)
                       : GetCurrencyOfMaxMinValue(dictCurrencyValue, int.MinValue, (a, b) => a > b);
                    var min = invertResults
                       ? GetCurrencyOfMaxMinValue(dictCurrencyValue, int.MinValue, (a, b) => a > b)
                       : GetCurrencyOfMaxMinValue(dictCurrencyValue, int.MaxValue, (a, b) => a < b);
                    var bestPairUnnormilized = max.Key + min.Key;
                    KeyValuePair<string, int> bestPairWithDirection = NormalizeCurrencyPair(bestPairUnnormilized);

                    // Get date
                    DateTime date = GetDate(cellsEur[row, dateColumnIndex]);
                    if (date == DateTime.MinValue)
                    {
                        message = $"DateTime can not be parsed from string {cellsEur[row, dateColumnIndex].Value.ToString()}";
                        //MessageBox.Show();
                        return message;
                    }

                    // Fill string builder
                    stringBuilder.Append($"{date.ToString("dd.MM.yyyy")}{SEPARATOR}{bestPairWithDirection.Key}{SEPARATOR}{bestPairWithDirection.Value}{Environment.NewLine}");
                }

                return stringBuilder.ToString();
            }

            /// <summary>
            /// Example
            /// </summary>
            /// <param name="fileWithPath"></param>
            public static void Example(string fileWithPath)
            {
                //Open your template file.
                Workbook wb = new Workbook(fileWithPath);

                //Get the first worksheet.
                Worksheet worksheet = wb.Worksheets[0];

                //Get cells
                Cells cells = worksheet.Cells;

                // Get row and column count
                int rowCount = cells.MaxDataRow;
                int columnCount = cells.MaxDataColumn;

                // Current cell value
                string strCell = "";

                Console.WriteLine(String.Format("rowCount={0}, columnCount={1}", rowCount, columnCount));

                for (int row = 0; row <= rowCount; row++) // Numeration starts from 0 to MaxDataRow
                {
                    for (int column = 0; column <= columnCount; column++)  // Numeration starts from 0 to MaxDataColumn
                    {
                        strCell = "";
                        strCell = Convert.ToString(cells[row, column].Value);
                        if (String.IsNullOrEmpty(strCell))
                        {
                            continue;
                        }
                        else
                        {
                            // Do your staff here
                            Console.WriteLine(strCell);
                        }
                    }
                }
            }

        }
    }
}
