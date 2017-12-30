using Ninject.Modules;
using Plarium.Abstraction.Repository.CustomRepo;
using Plarium.Data.UnitOfWork;
using Plarium.Interfaces.UnitOfWork;
using Plarium.Repositories.CustomRepo;

namespace DependencyInjectionModule
{
    public class NinjectConfiguration : NinjectModule
    {
        public override void Load()
        {
            //Bind<ILogMessageRepository>().To<LogMessageRepository>();
            Bind<IUnitOfWork>().To<UnitOfWork>();
        }
 
    }
}
