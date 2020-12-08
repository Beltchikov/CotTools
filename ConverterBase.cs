using Aspose.Cells;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace CotTools
{
    /// <summary>
    /// ConverterBase
    /// </summary>
    public class ConverterBase
    {
        protected static StringBuilder stringBuilder = new StringBuilder();
        protected const string SEPARATOR = ";";

        /// <summary>
        /// NormalizeCurrencyPair
        /// </summary>
        /// <param name="pair"></param>
        /// <returns></returns>
        protected static KeyValuePair<string, int> NormalizeCurrencyPair(string pair)
        {
            if (Forex.Pairs.Contains(pair.ToUpper()))
            {
                return new KeyValuePair<string, int>(pair.ToUpper(), 1);
            }
            else
            {
                string first3 = pair[0..3];
                string last3 = pair[3..^0];
                var pairReversed = last3 + first3;
                return new KeyValuePair<string, int>(pairReversed.ToUpper(), -1);
            }

        }

        /// <summary>
        /// GetDate - returns date of next friday.
        /// </summary>
        /// <param name="cell"></param>
        /// <returns>Return DateTime.MinValue in case of conversion error.</returns>
        protected static DateTime GetDate(Cell cell)
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

        /// <summary>
        /// GetCurrencyOfMaxMinValue
        /// </summary>
        /// <param name="dictCurrencyValue"></param>
        /// <param name="initValue"></param>
        /// <param name="compareFunction"></param>
        /// <returns></returns>
        protected static KeyValuePair<string, int> GetCurrencyOfMaxMinValue(Dictionary<string, int> dictCurrencyValue, int initValue, Func<int, int, bool> compareFunction)
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
    }
}
