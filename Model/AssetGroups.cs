using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CotTools.Model
{
    /// <summary>
    /// AssetGroups
    /// </summary>
    public class AssetGroups
    {
        private static List<AssetGroup> assetGroups;

        /// <summary>
        /// AssetGroups
        /// </summary>
        public AssetGroups()
        {
            assetGroups = new List<AssetGroup>
            {
                new AssetGroup{
                    Name = AssetGroup.FINANCIALS,
                    Scenarios = new List<string>
                    {
                        Scenario.DEALERINVERTED,
                        Scenario.DEALERCHANGEINVERTED,
                        Scenario.ASSETMANAGER,
                        Scenario.ASSETMANAGERCHANGE,
                        Scenario.LEVERAGED,
                        Scenario.LEVERAGEDCHANGED,
                    }
                }, new AssetGroup{
                    Name = AssetGroup.DISAGGREGATED,
                    Scenarios = new List<string>
                    {
                        "TODO"
                    }
                }
            };

        }

        /// <summary>
        /// Names
        /// </summary>
        public List<string> Names
        {
            get
            {
                return assetGroups.Select(g => g.Name).ToList();
            }
        }

        /// <summary>
        /// Group
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public AssetGroup Group(string name)
        {
            return assetGroups.FirstOrDefault(g => g.Name == name);
        }


    }
}
