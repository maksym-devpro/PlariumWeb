using Microsoft.Win32;
using ParsingService;
using ParsingService.Abstraction;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PlariumGui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string  END_MESSAGE = "Total count {0}. Processed {1}. Total Time {2} .All logs were loaded. Thank you.";
        private readonly ILogService _logService;
        private BackgroundWorker worker;
        private UpdateProgressBarDelegate _updatePbDelegate;
        private ProgressBarDelegate _pg2;
        private Progress<int> _progress;
        #region Constructor       
        public MainWindow(ILogService logService)
        {
            this._logService = logService;             
            InitializeComponent();

            //worker = new BackgroundWorker();
            //worker.DoWork += new DoWorkEventHandler(worker_DoWork);

            this._progress = new Progress<int>(s => progresBar.Value = s);
    
            //progresBar.IsVisible = true;
            this._updatePbDelegate = new UpdateProgressBarDelegate(progresBar.SetValue);

           // this._pg2 = new ProgressBarDelegate(worker_DoWork);

            //double processingPercentage = 88;
            //progresBar.SetValue(ProgressBar.ValueProperty, processingPercentage);
            //ProgressBar d = progresBar;
            //Dispatcher.Invoke(_updatePbDelegate, new object[] { ProgressBar.ValueProperty, processingPercentage });

            //Dispatcher.Invoke(this._updatePbDelegate, new object[] { ProgressBar.ValueProperty, d });
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

                    result = _logService.UploadLogsInDb(fileLogs,
                        this._updatePbDelegate,
                        this.progresBar,
                         this._pg2,
                         this._progress);

                    stopWatch.Stop();
                    // Get the elapsed time as a TimeSpan value.
                    TimeSpan ts = stopWatch.Elapsed;
                    string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);

                    Dispatcher.BeginInvoke((Action)(() => txtEditor.Text = string.Format(END_MESSAGE, fileLogs.Length, result, elapsedTime)));
                });

        

               // txtEditor.Text = string.Format(END_MESSAGE, fileLogs.Length, result, elapsedTime);

            }
        }

        //void worker_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    string[] fileLogs = (string[])e.Argument;

        //    Stopwatch stopWatch = new Stopwatch();
        //    stopWatch.Start();

        //    var result = _logService.UploadLogsInDb(fileLogs,
        //        this._updatePbDelegate,
        //        this.progresBar,
        //         this._pg2);


        //    stopWatch.Stop();
        //    // Get the elapsed time as a TimeSpan value.
        //    TimeSpan ts = stopWatch.Elapsed;
        //    string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
        //ts.Hours, ts.Minutes, ts.Seconds,
        //ts.Milliseconds / 10);

        //    txtEditor.Text = string.Format(END_MESSAGE, fileLogs.Length, 0, elapsedTime);
        //}
        #endregion
    }
}
