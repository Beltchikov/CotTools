using CotTools.Model;
using CotTools.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace CotTools
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// MainWindow
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }

        /// <summary>
        /// panFile_Drop
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panFile_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                string fileName = files[0];
                txtDropHint.Text = fileName;

                (DataContext as MainWindowViewModel).FileName = fileName;
                (DataContext as MainWindowViewModel).Assets = new List<string>(ExcelProcessor.GetAssets(fileName));

                string processResult = ExcelProcessor.Financials.ProcessDealerChangeInverted(files[0]);
                txtDealerInverted.Text = processResult;
                txtDropHint.FontSize = 12;

                // Old version with Forex
                //bool invertResults = false; 
                //txtResult.Text = ExcelProcessor.Financials.ProcessForexNet(files[0], 0, 0, invertResults);

                //txtResult.Text = string.Empty;
                //txtResult.Text = processResult;
            }
        }
    }
}
