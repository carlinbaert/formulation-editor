using Autofac;
using FormulationEditor.WPF.View;
using FormulationEditor.WPF.ViewModel;

namespace FormulationEditor.WPF.Startup
{
    public class BootStrapper
    {
        public IContainer BootStrap()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<MainWindowView>().AsSelf();
            builder.RegisterType<MainWindowViewModel>().AsSelf();

            return builder.Build();
        }
    }
}
