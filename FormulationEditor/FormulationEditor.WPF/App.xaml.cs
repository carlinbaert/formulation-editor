using Autofac;
using FormulationEditor.WPF.Startup;
using FormulationEditor.WPF.View;
using System.Windows;

namespace FormulationEditor.WPF
{
    public partial class App : Application
    {
        private IContainer _container;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var bootStrapper = new BootStrapper();
            _container = bootStrapper.BootStrap();

            var mainWindow = _container.Resolve<MainWindowView>();
            mainWindow.Show();
        }
    }
}
