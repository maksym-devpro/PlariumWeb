using Microsoft.Win32;
using ParsingService.Abstraction;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;

namespace PlariumGui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string END_MESSAGE = "Total count {0}. Processed {1}. Total Time {2} .All logs were loaded. Thank you.";
        private readonly ILogService _logService;
        private Progress<int> _progress;
        #region Constructor       
        public MainWindow(ILogService logService)
        {
            this._logService = logService;
            InitializeComponent();
            this._progress = new Progress<int>(s => progresBar.Value = s);

        }
        #endregion

        #region Private methods       
        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                string[] fileLogs = File.ReadAllLines(openFileDialog.FileName);

                Task.Run(() =>
                 {
                     Stopwatch stopWatch = new Stopwatch();
                     stopWatch.Start();
                     int result = 0;

                     result = _logService.UploadLogsInDb(fileLogs, this._progress);

                     stopWatch.Stop();
                    // Get the elapsed time as a TimeSpan value.
                    TimeSpan ts = stopWatch.Elapsed;
                     string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                 ts.Hours, ts.Minutes, ts.Seconds,
                 ts.Milliseconds / 10);

                     Dispatcher.BeginInvoke((Action)(() => txtEditor.Text = string.Format(END_MESSAGE, fileLogs.Length, result, elapsedTime)));
                 });

            }
        }

        #endregion
    }
}
