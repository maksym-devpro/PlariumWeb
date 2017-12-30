using DependencyInjectionModule;
using Ninject;
using System.Windows;

namespace PlariumGui
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {       

        protected override void OnStartup(StartupEventArgs e)
        {
            var ninjectConfig = new NinjectConfiguration();
            IKernel kernel = ninjectConfig.GetKernel();            
            var mainWindow = kernel.Get<MainWindow>();           
            mainWindow.Show();
        }
    }
}
