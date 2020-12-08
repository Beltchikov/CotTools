using CotTools.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CotTools.ViewModels
{
    /// <summary>
    /// MainWindowViewModel
    /// </summary>
    internal class MainWindowViewModel
    {
        private string selectedAssetGroupName;
        private AssetGroups assetGroups;

        /// <summary>
        /// MainWindowViewModel
        /// </summary>
        public MainWindowViewModel()
        {
            assetGroups = new AssetGroups();

            AssetGroups = new ObservableCollection<string>(assetGroups.Names);
            SelectedAssetGroup = assetGroups.Group("Financials").Name;
            Scenarios = new ObservableCollection<string>(assetGroups.Group("Financials").Scenarios);
            SelectedScenario = Scenarios[0];
        }
        
        public ObservableCollection<string> AssetGroups { get; set; }

        public string SelectedAssetGroup 
        {
            get {
                return selectedAssetGroupName;
            }
            set {
                selectedAssetGroupName = value;
                var selectedAssetGroup = assetGroups.Group(selectedAssetGroupName);
                Scenarios?.Clear();
                selectedAssetGroup.Scenarios.ForEach(s => Scenarios?.Add(s));
                //Scenarios = new ObservableCollection<string>(selectedAssetGroup.Scenarios);
                
            } 
        }

        public ObservableCollection<string> Scenarios { get; set; }

        public string SelectedScenario{ get; set; }




    }
}
