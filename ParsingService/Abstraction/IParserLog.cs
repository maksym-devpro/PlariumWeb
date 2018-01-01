using Plarium.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace ParsingService.Abstraction
{
    public interface IParserLog
    {
        List<LogMessage> ParseLogFile(string[] fileContent, 
            UpdateProgressBarDelegate progressDelegate, 
            ProgressBar pg,
            ProgressBarDelegate pg2,
             Progress<int> progress);//DependencyObject
    }
}
