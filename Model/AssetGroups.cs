using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CotTools.Model
{
    public class AssetGroups
    {
        private static List<AssetGroup> assetGroups;

        public AssetGroups()
        {
            assetGroups = new List<AssetGroup>
            {
                new AssetGroup{
                    Name = "Financials",
                    Scenarios = new List<string>
                    {
                        "DealerInverted",
                        "DealerChangeInverted",
                        "AssetManager",
                        "AssetManagerChange",
                        "Leveraged",
                        "LeveragedChanged"
                    }
                }, new AssetGroup{
                    Name = "Disaggregated",
                    Scenarios = new List<string>
                    {
                        "TODO"
                    }
                }
            };

        }

        public List<string> Names
        {
            get
            {
                return assetGroups.Select(g => g.Name).ToList();
            }
        }

        public AssetGroup Group(string name)
        {
            return assetGroups.FirstOrDefault(g => g.Name == name);
        }


    }
}
