using ParsingService.Abstraction;
using Plarium.Interfaces.UnitOfWork;

namespace ParsingService
{
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
        public bool UploadLogsInDb(string[] fileContent)
        {
            var logMessages = this._parserLog.ParseLogFile(fileContent);

            var result = this._unitOfWork.LogMessage.Add(logMessages);

            return false;
        }
        #endregion
    }
}
