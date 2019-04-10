using Autofac;
using FormulationEditor.WPF.Data.Lookups;
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
            builder.RegisterType<FormulationEditViewModel>().As<IFormulationEditViewModel>();

            builder.RegisterType<IngredientRepository>().As<IIngredientRepository>().SingleInstance();
            builder.RegisterType<FormulationRepository>().As<IFormulationRepository>().SingleInstance();
            builder.RegisterType<IngredientPriceRepository>().As<IIngredientPriceRepository>().SingleInstance();
            builder.RegisterType<FormulationIngredientRepository>().As<IFormulationIngredientRepository>().SingleInstance();

            builder.RegisterType<LookupDataService>().AsImplementedInterfaces().SingleInstance();

            return builder.Build();
        }
    }
}
