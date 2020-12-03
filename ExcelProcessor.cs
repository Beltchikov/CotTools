using Aspose.Cells;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace CotTools
{
    public class Excel
    {
        public static void ProcessForexNet(string fileWithPath, int columnIndex)
        {
            ForexWorkbook forexWorkbook = new ForexWorkbook(fileWithPath);
            StringBuilder stringBuilder = new StringBuilder();

            // Validate
            string message;
            if ((message = forexWorkbook.Validate()) != string.Empty)
            {
                MessageBox.Show(message);
            }

            //
            Cells cellsEur = forexWorkbook.WorksheetEur.Cells;
            int rowCountEur = cellsEur.MaxDataRow;
            for (int row = 1; row <= rowCountEur; row++)
            {
                Dictionary<string, int> dictCurrencyValue = new Dictionary<string, int>();

                dictCurrencyValue[Forex.EUR] = cellsEur[row, columnIndex].Value.ToNetValue();
                dictCurrencyValue[Forex.AUD] = forexWorkbook.WorksheetAud.Cells[row, columnIndex].Value.ToNetValue();
                dictCurrencyValue[Forex.CAD] = forexWorkbook.WorksheetCad.Cells[row, columnIndex].Value.ToNetValue();
                dictCurrencyValue[Forex.CHF] = forexWorkbook.WorksheetChf.Cells[row, columnIndex].Value.ToNetValue();
                dictCurrencyValue[Forex.GBP] = forexWorkbook.WorksheetGbp.Cells[row, columnIndex].Value.ToNetValue();
                dictCurrencyValue[Forex.JPY] = forexWorkbook.WorksheetJpy.Cells[row, columnIndex].Value.ToNetValue();

                var max = GetCurrencyOfMaxValue(dictCurrencyValue);



                // TODO Best & worst. Inversion vor Dealer

            }

        }

        private static KeyValuePair<string, int> GetCurrencyOfMaxValue(Dictionary<string, int> dictCurrencyValue)
        {
           KeyValuePair<string, int> kvpMax = new KeyValuePair<string, int>(string.Empty, Int32.MinValue);
       
            foreach (var key in dictCurrencyValue.Keys)
            {
                if(dictCurrencyValue[key] > kvpMax.Value)
                {
                    kvpMax = new KeyValuePair<string, int> (key, dictCurrencyValue[key]);
                }
            }

            return kvpMax;
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
