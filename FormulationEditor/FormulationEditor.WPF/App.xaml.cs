using Autofac;
using FormulationEditor.Model;
using FormulationEditor.WPF.Data.Repositories;
using FormulationEditor.WPF.Startup;
using FormulationEditor.WPF.View;
using System;
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

            InitializeApplicationForUseCase1();

            var mainWindow = _container.Resolve<MainWindowView>();
            mainWindow.Show();
        }

        private void InitializeApplicationForUseCase1()
        {
            var ingredientRepository = _container.Resolve<IIngredientRepository>();

            ingredientRepository.Add(new Ingredient { Id = 1, Name = "Corn" });
            ingredientRepository.Add(new Ingredient { Id = 1, Name = "Soybeans" });
            ingredientRepository.Add(new Ingredient { Id = 1, Name = "Wheat" });
            ingredientRepository.Add(new Ingredient { Id = 1, Name = "Hay" });
            ingredientRepository.Add(new Ingredient { Id = 1, Name = "Straw" });
        }
    }
}
