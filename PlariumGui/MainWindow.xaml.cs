using Microsoft.Win32;
using ParsingService.Abstraction;
using System.IO;
using System.Windows;

namespace PlariumGui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string  END_MESSAGE = "All logs were loaded. Thank you.";
        private readonly ILogService _logService;

        #region Constructor       
        public MainWindow(ILogService logService)
        {
            this._logService = logService;
             
            InitializeComponent();
        }
        #endregion

        #region Private methods       
        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                string[] fileLogs = File.ReadAllLines(openFileDialog.FileName);
           
                var result = _logService.UploadLogsInDb(fileLogs);

                txtEditor.Text = END_MESSAGE;
            }
        }
        #endregion
    }
}
