using Autofac;
using FormulationEditor.WPF.Data.Repositories;
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
            builder.RegisterType<FormulationEditView>().AsSelf();
            builder.RegisterType<FormulationEditViewModel>().AsSelf();

            builder.RegisterType<IngredientRepository>().As<IIngredientRepository>().SingleInstance();
            builder.RegisterType<FormulationRepository>().As<IFormulationRepository>().SingleInstance();
            builder.RegisterType<FormulationIngredientRepository>().As<IFormulationIngredientRepository>().SingleInstance();

            return builder.Build();
        }
    }
}
