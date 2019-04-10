using Autofac;
using FormulationEditor.Model;
using FormulationEditor.WPF.Data.Repositories;
using FormulationEditor.WPF.Startup;
using FormulationEditor.WPF.View;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.Generic;

namespace FormulationEditor.WPF
{
    public partial class App : Application
    {
        private IContainer _container;

        private async void Application_Startup(object sender, StartupEventArgs e)
        {
            var bootStrapper = new BootStrapper();
            _container = bootStrapper.BootStrap();

            await InitializeApplicationForUseCase1Async();

            var mainWindow = _container.Resolve<MainWindowView>();
            mainWindow.Show();
        }

        private async Task InitializeApplicationForUseCase1Async()
        {
            var ingredientRepository = _container.Resolve<IIngredientRepository>();

            ingredientRepository.Add(new Ingredient { Name = "Corn" });
            ingredientRepository.Add(new Ingredient { Name = "Soybeans" });
            ingredientRepository.Add(new Ingredient { Name = "Wheat" });
            ingredientRepository.Add(new Ingredient { Name = "Hay" });
            ingredientRepository.Add(new Ingredient { Name = "Straw" });

            var client = new HttpClient();

            client.BaseAddress = new Uri("http://localhost:64370/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("api/IngredientPrice");
            if (response.IsSuccessStatusCode)
            {
                var ingredientPriceRepository = _container.Resolve<IIngredientPriceRepository>();

                var prices = await response.Content.ReadAsAsync<IEnumerable<IngredientPrice>>();

                foreach(var price in prices)
                {
                    ingredientPriceRepository.Add(price);
                }
            }
        }
    }
}
