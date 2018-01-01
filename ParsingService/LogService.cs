using ParsingService.Abstraction;
using Plarium.Interfaces.UnitOfWork;
using System;
using System.Windows;
using System.Windows.Controls;

namespace ParsingService
{
    public delegate void UpdateProgressBarDelegate(DependencyProperty dp, Object value);
    public delegate void ProgressBarDelegate(double processingPercentage);



    public class LogService : ILogService
    {
        #region private fields
        private readonly IParserLog _parserLog;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public LogService(IParserLog parserLog, IUnitOfWork unitOfWork)
        {
            this._parserLog = parserLog;
            this._unitOfWork = unitOfWork;
        }
        #endregion

        #region Actions       
        public int UploadLogsInDb(string[] fileContent, Progress<int> progress)
        {
            var logMessages = this._parserLog.ParseLogFile(fileContent, progress);

          //  var result = this._unitOfWork.LogMessage.Add(logMessages);

            return logMessages.Count;
        }
        #endregion
    }
}
