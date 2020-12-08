using CotTools.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;

namespace CotTools.ViewModels
{
    /// <summary>
    /// MainWindowViewModel
    /// </summary>
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private string selectedAssetGroupName;
        private string selectedScenario;
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

        public ObservableCollection<string> Scenarios { get; set; }

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

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        //public string SelectedScenario
        //{
        //    get { return (string)GetValue(SelectedScenarioProperty); }
        //    set { SetValue(SelectedScenarioProperty, value); }
        //}

        //public static readonly DependencyProperty SelectedScenarioProperty =
        //    DependencyProperty.Register("SelectedScenario", typeof(string), typeof(MainWindowViewModel), new PropertyMetadata(0));

    }
}
