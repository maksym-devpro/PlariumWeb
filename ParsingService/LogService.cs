using ParsingService.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsingService
{
    public class LogService : ILogService
    {
        #region private fields
        private readonly IParserLog _parserLog;
        #endregion

        #region Constructor
        public LogService(IParserLog parserLog)
        {
            this._parserLog = parserLog;
        }
        #endregion


        #region Actions       
        public bool UploadLogsInDb(string fileContent)
        {
            var logMessages = this._parserLog.ParseLogFile(fileContent);

            return false;
        }
        #endregion
    }
}
