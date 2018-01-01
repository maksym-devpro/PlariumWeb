using System;
using System.Windows;
using System.Windows.Controls;

namespace ParsingService.Abstraction
{
    public interface ILogService
    {
        int UploadLogsInDb(string[] fileContent, 
            UpdateProgressBarDelegate progressDelegate, 
            ProgressBar pg, 
            ProgressBarDelegate pg2,
            Progress<int> progress);
    }
}
