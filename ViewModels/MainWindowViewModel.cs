using Aspose.Cells;
using CotTools.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace CotTools.ViewModels
{
    /// <summary>
    /// MainWindowViewModel
    /// </summary>
    public class MainWindowViewModel : DependencyObject
    {
        public static readonly DependencyProperty AssetsProperty;
        public static readonly DependencyProperty AssetFilterProperty;
        public static readonly DependencyProperty AssetsFilteredProperty;
        public static readonly DependencyProperty AssertSelectedProperty;
        public static readonly DependencyProperty FileNameProperty;
        public static readonly DependencyProperty ResultDealerInvertedProperty;
        public static readonly DependencyProperty ResultDealerInvertedChangeProperty;
        public static readonly DependencyProperty ResultAssetManagerProperty;
        public static readonly DependencyProperty ResultLeveragedProperty;

        public RelayCommand CommandRequestNavigate { get; set; }
        public RelayCommand CommandProcessAsset { get; set; }

        /// <summary>
        /// MainWindowViewModel
        /// </summary>
        static MainWindowViewModel()
        {
            AssetsProperty = DependencyProperty.Register("Assets", typeof(List<string>), typeof(MainWindowViewModel), new PropertyMetadata(null, AssetPropertyChanged));
            AssetFilterProperty = DependencyProperty.Register("AssetFilter", typeof(string), typeof(MainWindowViewModel), new PropertyMetadata(string.Empty, AssetFilterPropertyChanged));
            AssetsFilteredProperty = DependencyProperty.Register("AssetsFiltered", typeof(List<string>), typeof(MainWindowViewModel), new PropertyMetadata(null));
            AssertSelectedProperty = DependencyProperty.Register("AssertSelected", typeof(string), typeof(MainWindowViewModel), new PropertyMetadata(string.Empty));
            FileNameProperty = DependencyProperty.Register("FileName", typeof(string), typeof(MainWindowViewModel), new PropertyMetadata(string.Empty));
            ResultDealerInvertedProperty = DependencyProperty.Register("ResultDealerInverted", typeof(string), typeof(MainWindowViewModel), new PropertyMetadata(string.Empty));
            ResultDealerInvertedChangeProperty = DependencyProperty.Register("ResultDealerInvertedChange", typeof(string), typeof(MainWindowViewModel), new PropertyMetadata(string.Empty));
            ResultAssetManagerProperty = DependencyProperty.Register("ResultAssetManager", typeof(string), typeof(MainWindowViewModel), new PropertyMetadata(string.Empty));
            ResultLeveragedProperty = DependencyProperty.Register("ResultLeveraged", typeof(string), typeof(MainWindowViewModel), new PropertyMetadata(string.Empty));
        }

        /// <summary>
        /// MainWindowViewModel
        /// </summary>
        public MainWindowViewModel()
        {
            CommandRequestNavigate = new RelayCommand(p => { Process.Start(new ProcessStartInfo(((Uri)p).AbsoluteUri) { UseShellExecute = true }); });
            CommandProcessAsset = new RelayCommand(ProcessAsset);
        }

        /// <summary>
        /// ProcessAsset
        /// </summary>
        /// <param name="asset"></param>
        private void ProcessAsset(object asset)
        {
            ResultDealerInverted = ExcelProcessor.Financials.ProcessDealerInverted(FileName, asset);
            ResultDealerInvertedChange = ExcelProcessor.Financials.ProcessDealerInvertedChange(FileName, asset);
            ResultAssetManager = ExcelProcessor.Financials.ProcessAssetManager(FileName, asset);
            ResultLeveraged = ExcelProcessor.Financials.ProcessLeveraged(FileName, asset);
        }

        /// <summary>
        /// DEALERINVERTED
        /// </summary>
        public string DEALERINVERTED
        {
            get
            {
                return "Dealer Inverted";
            }
        }

        /// <summary>
        /// DEALERINVERTEDCHANGE
        /// </summary>
        public string DEALERINVERTEDCHANGE
        {
            get
            {
                return "Dealer Inverted Change";
            }
        }

        /// <summary>
        /// ASSETMANAGER
        /// </summary>
        public string ASSETMANAGER
        {
            get
            {
                return "Asset Manager";
            }
        }

        /// <summary>
        /// ASSETMANAGERCHANGE
        /// </summary>
        public string ASSETMANAGERCHANGE
        {
            get
            {
                return "Asset Manager Change";
            }
        }

        /// <summary>
        /// LEVERAGED
        /// </summary>
        public string LEVERAGED
        {
            get
            {
                return "Leveraged";
            }
        }

        /// <summary>
        /// LEVERAGEDCHANGED
        /// </summary>
        public string LEVERAGEDCHANGED
        {
            get
            {
                return "Leveraged Changed";
            }
        }

        /// <summary>
        /// WorkbookFinancials
        /// </summary>
        public Workbook WorkbookFinancials { get; set; }

        /// <summary>
        /// ResultDealerInverted
        /// </summary>
        public string ResultDealerInverted
        {
            get { return (string)GetValue(ResultDealerInvertedProperty); }
            set { SetValue(ResultDealerInvertedProperty, value); }
        }

        /// <summary>
        /// ResultDealerInvertedChange
        /// </summary>
        public string ResultDealerInvertedChange
        {
            get { return (string)GetValue(ResultDealerInvertedChangeProperty); }
            set { SetValue(ResultDealerInvertedChangeProperty, value); }
        }

        /// <summary>
        /// ResultAssetManager
        /// </summary>
        public string ResultAssetManager
        {
            get { return (string)GetValue(ResultAssetManagerProperty); }
            set { SetValue(ResultAssetManagerProperty, value); }
        }

        /// <summary>
        /// ResultLeveraged
        /// </summary>
        public string ResultLeveraged
        {
            get { return (string)GetValue(ResultLeveragedProperty); }
            set { SetValue(ResultLeveragedProperty, value); }
        }

        /// <summary>
        /// FileName
        /// </summary>
        public string FileName
        {
            get { return (string)GetValue(FileNameProperty); }
            set { SetValue(FileNameProperty, value); }
        }

        /// <summary>
        /// Assets
        /// </summary>
        public List<string> Assets
        {
            get { return (List<string>)GetValue(AssetsProperty); }
            set { SetValue(AssetsProperty, value); }
        }

        /// <summary>
        /// AssetFilter
        /// </summary>
        public string AssetFilter
        {
            get { return (string)GetValue(AssetFilterProperty); }
            set { SetValue(AssetFilterProperty, value); }
        }

        /// <summary>
        /// AssetsFiltered
        /// </summary>
        public List<string> AssetsFiltered
        {
            get { return (List<string>)GetValue(AssetsFilteredProperty); }
            set { SetValue(AssetsFilteredProperty, value); }
        }

        /// <summary>
        /// AssertSelected
        /// </summary>
        public string AssertSelected
        {
            get { return (string)GetValue(AssertSelectedProperty); }
            set { SetValue(AssertSelectedProperty, value); }
        }

        /// <summary>
        /// AssetPropertyChanged
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void AssetPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MainWindowViewModel instance = d as MainWindowViewModel;
            if (instance == null)
            {
                return;
            }

            instance.SetCurrentValue(AssetsFilteredProperty, instance.Assets);
        }

        /// <summary>
        /// AssetFilterPropertyChanged
        /// </summary>
        /// <param name="dependencyObject"></param>
        /// <param name="e"></param>
        public static void AssetFilterPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            MainWindowViewModel instance = dependencyObject as MainWindowViewModel;
            if (instance == null)
            {
                return;
            }

            if (String.IsNullOrWhiteSpace(instance.AssetFilter))
            {
                instance.SetCurrentValue(AssetsFilteredProperty, instance.Assets);
            }
            else
            {
                string filter1 = instance.AssetFilter.ToLower();
                string filter2 = instance.AssetFilter.ToUpper();

                var filteredAssets = instance.Assets.Where(a => a.Contains(filter1) || a.Contains(filter2));
                instance.SetCurrentValue(AssetsFilteredProperty, filteredAssets.ToList());
            }
        }


        // Variant with INotifyPropertyChanged

        ///// <summary>
        ///// PropertyChangedEventHandler
        ///// </summary>
        //public event PropertyChangedEventHandler PropertyChanged;

        ///// <summary>
        ///// NotifyPropertyChanged
        ///// </summary>
        ///// <param name="propertyName"></param>
        //private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}

    }
}
