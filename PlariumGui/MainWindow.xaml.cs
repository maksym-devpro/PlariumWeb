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
        private readonly ILogService _logService;

        public MainWindow(ILogService logService)
        {
            this._logService = logService;

            InitializeComponent();
        }


        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                string fileLogs = File.ReadAllText(openFileDialog.FileName);

                this._logService.UploadLogsInDb(fileLogs);

                txtEditor.Text = "All logs were loaded. Thank you.";
            }
        }
    }
}
