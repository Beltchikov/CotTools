using CotTools.Commands;
using CotTools.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

        /// <summary>
        /// MainWindowViewModel
        /// </summary>
        static MainWindowViewModel()
        {
            AssetsProperty = DependencyProperty.Register("Assets", typeof(List<string>), typeof(MainWindowViewModel), new PropertyMetadata(null, AssetPropertyChanged));
            AssetFilterProperty = DependencyProperty.Register("AssetFilter", typeof(string), typeof(MainWindowViewModel), new PropertyMetadata(string.Empty, AssetFilterPropertyChanged));
            AssetsFilteredProperty = DependencyProperty.Register("AssetsFiltered", typeof(List<string>), typeof(MainWindowViewModel), new PropertyMetadata(null));
        }

        /// <summary>
        /// MainWindowViewModel
        /// </summary>
        public MainWindowViewModel()
        {
            CommandRequestNavigate = new CommandRequestNavigate();
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

        /// <summary>
        /// CommandRequestNavigate
        /// </summary>
        public ICommand CommandRequestNavigate { get; set; }

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
