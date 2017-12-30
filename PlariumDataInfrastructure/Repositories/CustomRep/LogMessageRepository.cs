using System;
using System.Linq;
using Plarium.Domain.Entities;
using Plarium.Abstraction.Repository.CustomRepo;
using Plarium.Data.Repositories;
using Plarium.Data;

namespace Plarium.Repositories.CustomRepo
{
    public class LogMessageRepository : GenericRepository<LogMessage>, ILogMessageRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public LogMessageRepository(ApplicationDbContext dbContext):base(dbContext) 
        {
            this._dbContext = dbContext;
        }

        #region Public methods
              
        public IQueryable<string> GetTopIpAddressDesc(DateTime start, DateTime end, int count)
        {
            throw new NotImplementedException();
        }

        public IQueryable<string> GetTopRoutesDesc(DateTime start, DateTime end, int count)
        {
            throw new NotImplementedException();
        }

        public IQueryable<LogMessage> GetLogsAscByTime(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
