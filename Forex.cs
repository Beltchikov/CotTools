using System;
using System.Collections.Generic;
using System.Text;

namespace CotTools
{
    public class Forex
    {
        private static IList<string> _pairs = new List<string> { "EURUSD", "AUDUSD", "USDCAD", "USDCHF", "GBPUSD", "USDJPY",
            "EURAUD", "EURCAD", "EURCHF", "EURGBP", "EURJPY",
            "AUDCAD", "AUDCHF", "GBPAUD", "AUDJPY",
            "CADCHF", "GBPCAD", "CADJPY",
            "GBPCHF", "CHFJPY",
            "GBPJPY"};

        public static string EUR => "EUR";
        public static string AUD => "AUD";
        public static string CAD => "CAD";
        public static string CHF => "CHF";
        public static string GBP => "GBP";
        public static string JPY => "JPY";
        public static string USD => "USD";

        public static IList<string> Pairs => _pairs;

    }
}
