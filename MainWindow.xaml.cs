using CotTools.ViewModels;

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
                lblFile.Content = files[0];

                int dateColumnIndex = 0;
                int columnIndex = 0;
                bool invertResults = false; // TODO

                // TODO
                txtResult.Text = ExcelProcessor.ProcessForexNet(files[0], 0, 0, invertResults);
            }
        }

    }
}
