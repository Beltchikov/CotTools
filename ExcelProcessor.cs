using Aspose.Cells;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;

namespace CotTools
{
    public class Excel
    {
        public static void ProcessForexNet(string fileWithPath, int dateColumnIndex, int columnIndex, bool invertResults)
        {
            ForexWorkbook forexWorkbook = new ForexWorkbook(fileWithPath);
            StringBuilder stringBuilder = new StringBuilder();

            // Validate
            string message;
            if ((message = forexWorkbook.Validate()) != string.Empty)
            {
                MessageBox.Show(message);
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

                // Currencies mit max/min values
                var maxMin = invertResults
                   ? GetCurrencyOfMaxMinValue(dictCurrencyValue, int.MaxValue, (a, b) => a < b)
                   : GetCurrencyOfMaxMinValue(dictCurrencyValue, int.MinValue, (a, b) => a > b);

                // Get date
                DateTime date = GetDate(cellsEur[row, dateColumnIndex]);
                if (date == DateTime.MinValue)
                {
                    MessageBox.Show($"DateTime can not be parsed from string {cellsEur[row, dateColumnIndex].Value.ToString()}");
                    return;
                }




            }

        }

        /// <summary>
        /// GetDate - returns date of next friday.
        /// </summary>
        /// <param name="cell"></param>
        /// <returns>Return DateTime.MinValue in case of conversion error.</returns>
        private static DateTime GetDate(Cell cell)
        {
            DateTime date;
            var dateTimeString = cell.Value.ToString();
            if (!DateTime.TryParse(dateTimeString, new CultureInfo("DE-de"), DateTimeStyles.None, out date))
            {
                return DateTime.MinValue;
            }

            date = date.AddDays(DayOfWeek.Friday - date.DayOfWeek);
            return date;
        }

        private static KeyValuePair<string, int> GetCurrencyOfMaxMinValue(Dictionary<string, int> dictCurrencyValue, int initValue, Func<int, int, bool> compareFunction)
        {
            KeyValuePair<string, int> kvpMaxMin = new KeyValuePair<string, int>(string.Empty, initValue);

            foreach (var key in dictCurrencyValue.Keys)
            {
                if (compareFunction(dictCurrencyValue[key], kvpMaxMin.Value))
                {
                    kvpMaxMin = new KeyValuePair<string, int>(key, dictCurrencyValue[key]);
                }
            }

            return kvpMaxMin;
        }

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
