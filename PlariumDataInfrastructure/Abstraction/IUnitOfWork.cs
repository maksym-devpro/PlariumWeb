using System;
using Plarium.Abstraction.Repository.CustomRepo;

namespace Plarium.Interfaces.UnitOfWork
{
    public interface IUnitOfWork: IDisposable
    {
        ILogMessageRepository LogMessage { get; }
        void SaveChanges();
        void Dispose(bool disposing);
    }
}
