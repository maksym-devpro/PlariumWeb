using Ninject;
using ParsingService;
using ParsingService.Abstraction;
using Plarium.Data.UnitOfWork;
using Plarium.Interfaces.UnitOfWork;

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
