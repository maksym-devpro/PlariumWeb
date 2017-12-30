using ParsingService.Abstraction;
using System;
using System.Collections.Generic; 
using Plarium.Domain.Entities;

namespace ParsingService
{
    public class ParserLog : IParserLog
    {
        private int count9 = 0;
        private int count8 = 0;
        private int count7 = 0;
        private int count6 = 0;
        private int count10 = 0;
        public List<LogMessage> ParseLogFile(string[] fileContent)
        {
            List<LogMessage> logs = new List<LogMessage>();

            foreach (string log in fileContent)
            {
                logs.Add( this.ConvertToLogMessage(log));
            }
            return logs;
        }

        private LogMessage ConvertToLogMessage(string log)
        {

          
            string[] separatingChars = { "HEAD", " - - [", "] ", "'", " ", "HTTP/1.0", "-0400]", "GET", "\"" };

            string[] logFilds = log.Split(separatingChars, System.StringSplitOptions.RemoveEmptyEntries);

            #region

            if (logFilds.Length == 9)
            {
                count9++;
                return null;
            }

            if (logFilds.Length == 8)
            {
                count8++;
                return null;
            }


            if (logFilds.Length == 7)
            {
                count7++;
                return null;
            }

            if (logFilds.Length == 6)
            {
                count6++;
                return null;
            }

            if (logFilds.Length > 10)
            {
                count10++;
                return null;
            }
            #endregion

            return new LogMessage
            {
                Id = Guid.NewGuid()
            };
        }

        private string GetIpAddres(string log)
        {
            
            return "";
        }
    }
}
