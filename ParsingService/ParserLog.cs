using ParsingService.Abstraction;
using System;
using System.Collections.Generic;
using Plarium.Domain.Entities;

namespace ParsingService
{
    public class ParserLog : IParserLog
    {
        private const string DATE_FORMAT_TEMPLATE = "dd/MMM/yyyy':'HH':'mm':'ss zzz";

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
                try
                {
                    logs.Add(this.ConvertToLogMessage(log));
                }
                catch (Exception ex)
                {

                }

            }
            return logs;
        }

        private LogMessage ConvertToLogMessage(string log)
        {
            #region

            //if (logFilds.Length == 9)
            //{
            //    count9++;
            //    return null;
            //}

            //if (logFilds.Length == 8)
            //{
            //    count8++;
            //    return null;
            //}


            //if (logFilds.Length == 7)
            //{
            //    count7++;
            //    return null;
            //}

            //if (logFilds.Length == 6)
            //{
            //    count6++;
            //    return null;
            //}

            //if (logFilds.Length > 10)
            //{
            //    count10++;
            //    return null;
            //}
            #endregion

            var route = GetRoute(log);
            return new LogMessage
            {
                Id = Guid.NewGuid(),
                IpAddress = GetIpAddres(log),
                RequestTime = GetDateTime(log),
                Route = route,
                UrlQueryParameters = GetUrlQueryParameters(route),
                RequestResult = GetRequestResult(log),
                RequestSize = GetRequestSize(log)
            };
        }

        private string GetIpAddres(string log)
        {
            //string[] separatingChars = { " - - [", "] ", "'", " ", "HTTP/1.0", "-0400]", "GET", "\"" };
            //string[] logFilds = log.Split(separatingChars, System.StringSplitOptions.RemoveEmptyEntries);
            //var ip = log.TrimEnd('-');
            //int indexOfChar = log.IndexOf(" - -");
            string[] separating = { " - -" };
            var ip = log.Split(separating, StringSplitOptions.RemoveEmptyEntries);
            return ip[0];
        }

        private DateTime GetDateTime(string log)
        {
            var startIndex = log.IndexOf("[");
            var endIndex = log.LastIndexOf("] ");
            int length = endIndex - startIndex - 1;
            System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-US", true);
            var timeStr = log.Substring(++startIndex, length);
            DateTime dt = DateTime.ParseExact(timeStr, DATE_FORMAT_TEMPLATE, ci);
            return dt;
        }

        private string GetRoute(string log)
        {
            var startIndex = log.IndexOf(" \"");
            var endIndex = log.LastIndexOf("\" ");
            int length = endIndex - startIndex - 1;
            var httpStartLine = log.Substring(++startIndex, length);
            var httpLineItems = httpStartLine.Split(' ');
            return httpLineItems[1];
        }

        private string GetUrlQueryParameters(string route)
        {
            var routeItem = route.Split('?');
            return routeItem.Length == 2 ? routeItem[1] : string.Empty;
        }

        private int GetRequestResult(string log)
        {
            var startIndex = log.LastIndexOf("\" ");
            startIndex += 2;
            var logEnd = log.Remove(0, startIndex);

            var resultAndByteSize = logEnd.Split(' ');
            var requestResult = Convert.ToInt32(resultAndByteSize[0]);
            return requestResult;
        }

        private int GetRequestSize(string log)
        {
            var startIndex = log.IndexOf("\" ");
            startIndex += 2;
            var logEnd = log.Remove(0, startIndex);
            var resultAndByteSize = logEnd.Split(' ');
            int result = 0;
            int.TryParse(resultAndByteSize[1], out result);

            return result;
        }
    }
}
