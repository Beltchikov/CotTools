using System;
using System.Collections.Generic;
using System.Text;

namespace CotTools.Model
{
    /// <summary>
    /// AssetGroup
    /// </summary>
    public class AssetGroup
    {
        /// <summary>
        /// FINANCIALS
        /// </summary>
        public const string FINANCIALS = "Financials";

        /// <summary>
        /// DISAGGREGATED
        /// </summary>
        public const string DISAGGREGATED = "Disaggregated";

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Scenarios
        /// </summary>
        public List<string> Scenarios { get; set; }

       

    }
}
