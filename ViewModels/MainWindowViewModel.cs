using CotTools.Commands;
using CotTools.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace CotTools.ViewModels
{
    /// <summary>
    /// MainWindowViewModel
    /// </summary>
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<string> assets;
        private string selectedAssetGroupName;
        private string selectedScenario;
        private AssetGroups assetGroups;

        /// <summary>
        /// MainWindowViewModel
        /// </summary>
        public MainWindowViewModel()
        {
            Assets = new ObservableCollection<string>();

            assetGroups = new AssetGroups();

            AssetGroups = new ObservableCollection<string>(assetGroups.Names);
            SelectedAssetGroup = assetGroups.Group("Financials").Name;
            Scenarios = new ObservableCollection<string>(assetGroups.Group("Financials").Scenarios);
            SelectedScenario = Scenarios[0];

            CommandRequestNavigate = new CommandRequestNavigate();
        }

        public ObservableCollection<string> Assets
        {
            get
            {
                return assets;
            }
            set
            {
                assets = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// AssetGroups
        /// </summary>
        public ObservableCollection<string> AssetGroups { get; set; }

        /// <summary>
        /// SelectedAssetGroup
        /// </summary>
        public string SelectedAssetGroup
        {
            get
            {
                return selectedAssetGroupName;
            }
            set
            {
                selectedAssetGroupName = value;
                var selectedAssetGroup = assetGroups.Group(selectedAssetGroupName);
                Scenarios?.Clear();
                selectedAssetGroup.Scenarios.ForEach(s => Scenarios?.Add(s));
                SelectedScenario = Scenarios?[0];
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// ObservableCollection
        /// </summary>
        public ObservableCollection<string> Scenarios { get; set; }

        /// <summary>
        /// SelectedScenario
        /// </summary>
        public string SelectedScenario
        {
            get
            {
                return selectedScenario;
            }
            set
            {
                selectedScenario = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// PropertyChangedEventHandler
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// NotifyPropertyChanged
        /// </summary>
        /// <param name="propertyName"></param>
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// CommandRequestNavigate
        /// </summary>
        public ICommand CommandRequestNavigate { get; set; }

    }
}
