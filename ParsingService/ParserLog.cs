using ParsingService.Abstraction;
using System;
using System.Collections.Generic;
using Plarium.Domain.Entities;
using System.Threading.Tasks;
using System.Threading;
using System.ComponentModel;

namespace ParsingService
{
    public class ParserLog : IParserLog
    {
        private const string DATE_FORMAT_TEMPLATE = "dd/MMM/yyyy':'HH':'mm':'ss zzz";
        private LogMessage[] LogMessages;
        private Thread[] Threads;

        public ParserLog()
        {
            Threads = new Thread[7];
        }

        public LogMessage[] ParseLogFile(string[] fileContent)
        {
            int countLogs = fileContent.Length;
            LogMessages = new LogMessage[countLogs];
            int counterSuccsess = 0;

            for(int i = 0; i< countLogs; i++)
            {
                try
                {
                    LogMessages[i] = new LogMessage { Id = Guid.NewGuid() };

                    //Thread newThread = new Thread(new ThreadStart(ThreadMethod));
                    //newThread.SetApartmentState(ApartmentState.MTA);
                    //newThread.Start();

                    Thread t = new Thread(new ParameterizedThreadStart(AddLogToArray));
                    t.SetApartmentState(ApartmentState.MTA);
                    t.Start(new Param2(fileContent[i], i));


                    //  AddLogToArray(fileContent[i],i);
                    // logs.Add(this.ConvertToLogMessage(log));
                    counterSuccsess++;
                }
                catch (Exception ex)
                {

                }
            }
            return LogMessages;

            // List<LogMessage> logs = new List<LogMessage>();
            //foreach (string log in fileContent)
            //{
            //    try
            //    {
            //        logs.Add(this.ConvertToLogMessage(log));
            //    }
            //    catch (Exception ex)
            //    {

            //    }

            //}
            //   return logs;
        }

        private void AddLogToArray(object parametrs)
        {
            var param = (Param2)parametrs;
            string log = param.Log;
            int indexInArray = param.LogMessagesIndex;

            System.Threading.Thread.CurrentThread.SetApartmentState(ApartmentState.MTA);//.GetApartmentState().ToString();

            //Thread thread = new Thread(() => {
            //    LogMessages[indexInArray].IpAddress = GetIpAddresAsync(log); });
            //thread.Start();
            var events = new List<ManualResetEvent>();
            var ipAddresEvent = new ManualResetEvent(false);
            ThreadPool.QueueUserWorkItem(GetIpAddresOperation, new Parametr(indexInArray,log, ipAddresEvent) );
            events.Add(ipAddresEvent);

            var dateTimeEvent = new ManualResetEvent(false);
            ThreadPool.QueueUserWorkItem(GetDateTimeOperation, new Parametr(indexInArray, log, dateTimeEvent));
            events.Add(dateTimeEvent);

            var routeEvent = new ManualResetEvent(false);
            ThreadPool.QueueUserWorkItem(GetRouteOperation, new Parametr(indexInArray, log, routeEvent));
            events.Add(routeEvent);

            var requestResultEvent = new ManualResetEvent(false);
            ThreadPool.QueueUserWorkItem(GetRequestResultOperation, new Parametr(indexInArray, log, requestResultEvent));
            events.Add(requestResultEvent);

            var requestSizeEvent = new ManualResetEvent(false);
            ThreadPool.QueueUserWorkItem(GetRequestSizeOperation, new Parametr(indexInArray, log, requestSizeEvent));
            events.Add(requestSizeEvent);
             
            WaitHandle.WaitAll(events.ToArray());
        }


        private LogMessage ConvertToLogMessage(string log)
        { 

            Task<string> ipAddress = Task<string>.Factory.StartNew(() => GetIpAddresAsync(log));
            Task<string> route = Task<string>.Factory.StartNew(() => GetRouteAsync(log));
            Task<int> requestResult = Task<int>.Factory.StartNew(() => GetRequestResultAsync(log));
            Task<DateTime> requestTime = Task<DateTime>.Factory.StartNew(() => GetDateTimeAsync(log));
            Task<int> requestSize = Task<int>.Factory.StartNew(() => GetRequestSizeAsync(log));


            return new LogMessage
            {
                Id = Guid.NewGuid(),
                IpAddress = ipAddress.Result,
                RequestTime = requestTime.Result,
                Route = route.Result,
                RequestResult = requestResult.Result,
                RequestSize = requestSize.Result
                // UrlQueryParameters = GetUrlQueryParameters(route),

            };
        }

        private string GetIpAddresAsync(string log)
        {
            string[] separating = { " - -" };
            var ip = log.Split(separating, StringSplitOptions.RemoveEmptyEntries);
            return ip[0];
        }

        private DateTime GetDateTimeAsync(string log)
        {
            var startIndex = log.IndexOf("[");
            var endIndex = log.LastIndexOf("] ");
            int length = endIndex - startIndex - 1;
            System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-US", true);
            var timeStr = log.Substring(++startIndex, length);
            DateTime dt = DateTime.ParseExact(timeStr, DATE_FORMAT_TEMPLATE, ci);
            return dt;
        }

        private string GetRouteAsync(string log)
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

        private int GetRequestResultAsync(string log)
        {
            var startIndex = log.LastIndexOf("\" ");
            startIndex += 2;
            var logEnd = log.Remove(0, startIndex);

            var resultAndByteSize = logEnd.Split(' ');
            var requestResult = Convert.ToInt32(resultAndByteSize[0]);
            return requestResult;
        }

        private int GetRequestSizeAsync(string log)
        {
            var startIndex = log.IndexOf("\" ");
            startIndex += 2;
            var logEnd = log.Remove(0, startIndex);
            var resultAndByteSize = logEnd.Split(' ');
            int result = 0;
            int.TryParse(resultAndByteSize[1], out result);

            return result;
        }

        public void GetIpAddresOperation(object logObject)
        {
            var parametr = (Parametr)logObject;
            LogMessages[parametr.LogMessagesIndex].IpAddress = GetIpAddresAsync(parametr.Log);
            parametr.ManualResetEvent.Set();
        }

        public void GetDateTimeOperation(object logObject)
        {
            var parametr = (Parametr)logObject;
            LogMessages[parametr.LogMessagesIndex].RequestTime = GetDateTimeAsync(parametr.Log);
            parametr.ManualResetEvent.Set();
        }

        public void GetRouteOperation(object logObject)
        {
            var parametr = (Parametr)logObject;
            LogMessages[parametr.LogMessagesIndex].Route = GetRouteAsync(parametr.Log);
            parametr.ManualResetEvent.Set();
        }
        public void GetRequestResultOperation(object logObject)
        {
            var parametr = (Parametr)logObject;
            LogMessages[parametr.LogMessagesIndex].RequestResult = GetRequestResultAsync(parametr.Log);
            parametr.ManualResetEvent.Set();
        }

        public void GetRequestSizeOperation(object logObject)
        {
            var parametr = (Parametr)logObject;
            LogMessages[parametr.LogMessagesIndex].RequestSize = GetRequestSizeAsync(parametr.Log);
            parametr.ManualResetEvent.Set();
        }

    }

    public class Parametr
    {
        public Parametr(int index, string log, ManualResetEvent resetEvent)
        {
            this.LogMessagesIndex = index;
            this.Log = log;
            this.ManualResetEvent = resetEvent;
        }

        public int LogMessagesIndex { get; set; }
        public string Log { get; set; }
        public ManualResetEvent ManualResetEvent { get; set; }
    }

    public class Param2
    {
        public Param2( string log, int index)
        {
            this.LogMessagesIndex = index;
            this.Log = log;
            
        }

        public int LogMessagesIndex { get; set; }
        public string Log { get; set; }
    }
}
