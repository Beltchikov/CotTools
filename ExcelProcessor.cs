using Aspose.Cells;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace CotTools
{
    public class Excel
    {
        public static void ProcessForex(string fileWithPath)
        {
            Workbook wb = new Workbook(fileWithPath);

            Worksheet worksheetEur = wb.Worksheets[Forex.EUR];
            if (worksheetEur == null)
            {
                MessageBox.Show($"Worksheet {Forex.EUR} missing");
                return;
            }
            Worksheet worksheetAud = wb.Worksheets[Forex.AUD];
            if (worksheetAud == null)
            {
                MessageBox.Show($"Worksheet {Forex.AUD} missing");
                return;
            }
            Worksheet worksheetCad = wb.Worksheets[Forex.CAD];
            if (worksheetCad == null)
            {
                MessageBox.Show($"Worksheet {Forex.CAD} missing");
                return;
            }
            Worksheet worksheetChf = wb.Worksheets[Forex.CHF];
            if (worksheetChf == null)
            {
                MessageBox.Show($"Worksheet {Forex.CHF} missing");
                return;
            }
            Worksheet worksheetGbp = wb.Worksheets[Forex.GBP];
            if (worksheetGbp == null)
            {
                MessageBox.Show($"Worksheet {Forex.GBP} missing");
                return;
            }
            Worksheet worksheetJpy = wb.Worksheets[Forex.JPY];
            if (worksheetJpy == null)
            {
                MessageBox.Show($"Worksheet {Forex.GBP} missing");
                return;
            }

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
