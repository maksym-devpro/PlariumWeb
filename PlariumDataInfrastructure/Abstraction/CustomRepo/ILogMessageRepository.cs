using Plarium.Domain.Entities; 
using System;
using System.Linq;

namespace Plarium.Abstraction.Repository.CustomRepo
{
    public interface ILogMessageRepository : IGenericRepository<LogMessage>
    { 
        IQueryable<string> GetTopIpAddressDesc(DateTime start, DateTime end, int count);

        IQueryable<string> GetTopRoutesDesc(DateTime start, DateTime end, int count);

        IQueryable<LogMessage> GetLogsAscByTime(DateTime start, DateTime end);
        
    }
}
