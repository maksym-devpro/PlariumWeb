using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace PlariumGui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                string fileLogs = File.ReadAllText(openFileDialog.FileName);
                txtEditor.Text = "All logs were loaded. Thank you.";
            }
        }
    }
}
