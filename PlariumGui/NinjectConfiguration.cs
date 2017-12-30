using Ninject;
using Ninject.Modules;
using ParsingService;
using ParsingService.Abstraction;
using Plarium.Data.UnitOfWork;
using Plarium.Interfaces.UnitOfWork;
using PlariumGui;

namespace DependencyInjectionModule
{
    public class NinjectConfiguration
    { 
        public StandardKernel GetKernel()
        {
            var container = new StandardKernel();
            container.Bind<IParserLog>().To<ParserLog>();
            container.Bind<ILogService>().To<LogService>();
            container.Bind<IUnitOfWork>().To<UnitOfWork>();

            return container;


        }
 
    }
}
