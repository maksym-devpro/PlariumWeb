using System;
using Plarium.Abstraction.Repository.CustomRepo;
using Plarium.Interfaces.UnitOfWork;
using Plarium.Repositories.CustomRepo;

namespace Plarium.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Private
        private bool _disposed = false;
        private ILogMessageRepository _logMessageRepository;
        private readonly ApplicationDbContext _appDbContext;
        #endregion

        #region Constructor
        public UnitOfWork( ApplicationDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }
        #endregion
        
        #region Public repositories
           
        public ILogMessageRepository LogMessage
        {
            get
            {
                if (this._logMessageRepository == null)
                {
                    this._logMessageRepository = new LogMessageRepository(_appDbContext);
                }
                return this._logMessageRepository;
            }
        }

        public void SaveChanges()
        { 
           this._appDbContext.SaveChanges();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    this._appDbContext.Dispose();
                }
                this._disposed = true;
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }


}