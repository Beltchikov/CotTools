using CotTools.Model;
using CotTools.ViewModels;
using System;
using System.Collections.ObjectModel;
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
                txtDropHint.Text = files[0];

                //string assetGroup = cmbAssetGroups.Text;
                //string scenario = cmbScenario.Text;

                (DataContext as MainWindowViewModel).Assets = new ObservableCollection<string>(ExcelProcessor.GetAssets(files[0]));


                //string processResult;
                //switch (assetGroup)
                //{
                //    case AssetGroup.FINANCIALS:
                //        processResult = ProcessFinancials(files[0], scenario);
                //        break;
                //    case AssetGroup.DISAGGREGATED:
                //        throw new NotImplementedException();
                //    default:
                //        throw new NotImplementedException();
                //}


                // TODO
                //bool invertResults = false; 
                //txtResult.Text = ExcelProcessor.Financials.ProcessForexNet(files[0], 0, 0, invertResults);

                //txtResult.Text = string.Empty;
                //txtResult.Text = processResult;

            }
        }

        private string ProcessFinancials(string fileName, string scenario)
        {


            switch (scenario)
            {
                case Scenario.DEALERINVERTED:
                    return ExcelProcessor.Financials.ProcessDealerInverted(fileName);
                case Scenario.DEALERCHANGEINVERTED:
                    return ExcelProcessor.Financials.ProcessDealerChangeInverted(fileName);
                default:
                    throw new NotImplementedException();
            }
        }

    }
}
