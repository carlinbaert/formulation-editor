using System.Collections.Generic;
using FormulationEditor.Model;
using FormulationEditor.WPF.Data.Lookups;
using FormulationEditor.WPF.Data.Repositories;
using FormulationEditor.WPF.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FormulationEditor.WPF.Tests
{
    [TestClass]
    public class FormulationEditViewModelTests
    {
        private Mock<IIngredientLookupDataService> ingredientLookupDataService = new Mock<IIngredientLookupDataService>(MockBehavior.Strict);
        private Mock<IFormulationIngredientRepository> formulationIngredientRepository = new Mock<IFormulationIngredientRepository>(MockBehavior.Strict);
        private Mock<IFormulationRepository> formulationRepository = new Mock<IFormulationRepository>(MockBehavior.Strict);
        private Mock<IIngredientRepository> ingredientRepository = new Mock<IIngredientRepository>(MockBehavior.Strict);

        [TestMethod]
        public void Load_NoExceptions()
        {
            ingredientLookupDataService.Setup(i => i.GetAllIngredients()).Returns(() => new List<LookupItem> { new LookupItem { Id = 1, DisplayMember = "Corn" } });

            formulationIngredientRepository.Setup(f => f.GetByFormulationId(It.IsAny<int>())).Returns(new List<FormulationIngredient>());

            formulationRepository.Setup(f => f.GetAll()).Returns(new List<Formulation>());

            ingredientRepository.Setup(f => f.GetAll()).Returns(() => new List<Ingredient> { new Ingredient { Id = 1, Name = "Corn" } });
            ingredientRepository.Setup(f => f.GetById(It.IsAny<int>())).Returns(() => new Ingredient { Id = 1, Name = "Corn" });

            var viewModel = new FormulationEditViewModel(ingredientLookupDataService.Object, formulationIngredientRepository.Object, formulationRepository.Object, ingredientRepository.Object);

            viewModel.Load(0);

            Assert.IsNotNull(viewModel.AvailableIngredients);
            Assert.IsTrue(viewModel.AvailableIngredients.Count == 1);
            Assert.IsNotNull(viewModel.AssignedIngredientBusinessModels);
            Assert.IsTrue(viewModel.AssignedIngredientBusinessModels.Count == 0);

            ingredientLookupDataService.VerifyAll();
            formulationIngredientRepository.VerifyAll();
        }

        [TestMethod]
        public void Load_ExistingFormula_AvailableAssignedListsCorrectCount()
        {
            ingredientLookupDataService.Setup(i => i.GetAllIngredients()).Returns(() => new List<LookupItem>
            {
                  new LookupItem { Id = 1, DisplayMember = "Corn" }
                , new LookupItem { Id = 2, DisplayMember = "Hay" }
                , new LookupItem { Id = 3, DisplayMember = "Straw" }
                , new LookupItem { Id = 4, DisplayMember = "Wheat" }
            });

            var ingredient1 = new Ingredient { Id = 1, Name = "Corn" };
            var ingredient2 = new Ingredient { Id = 2, Name = "Hay" };
            var ingredient3 = new Ingredient { Id = 3, Name = "Straw" };
            var ingredient4 = new Ingredient { Id = 4, Name = "Wheat" };

            ingredientRepository.Setup(f => f.GetAll()).Returns(() => new List<Ingredient>
            { ingredient1, ingredient2, ingredient3, ingredient4 });

            ingredientRepository.Setup(f => f.GetById(ingredient1.Id)).Returns(ingredient1);
            ingredientRepository.Setup(f => f.GetById(ingredient2.Id)).Returns(ingredient2);
            ingredientRepository.Setup(f => f.GetById(ingredient3.Id)).Returns(ingredient3);
            ingredientRepository.Setup(f => f.GetById(ingredient4.Id)).Returns(ingredient4);

            formulationRepository.Setup(f => f.GetById(It.IsAny<int>())).Returns(new Formulation { Id = 1, Name = "Formulation1"});
            
            formulationIngredientRepository.Setup(f => f.GetByFormulationId(It.IsAny<int>())).Returns(new List<FormulationIngredient>()
            {
                  new FormulationIngredient { Id = 1, FormulationId = 1, IngredientId = 1 }
                , new FormulationIngredient { Id = 2, FormulationId = 1, IngredientId = 2 }
            });            

            var viewModel = new FormulationEditViewModel(ingredientLookupDataService.Object, formulationIngredientRepository.Object, formulationRepository.Object, ingredientRepository.Object);

            viewModel.Load(1);

            Assert.IsNotNull(viewModel.AvailableIngredients);
            Assert.IsTrue(viewModel.AvailableIngredients.Count == 2);
            Assert.IsNotNull(viewModel.AssignedIngredientBusinessModels);
            Assert.IsTrue(viewModel.AssignedIngredientBusinessModels.Count == 2);

            ingredientLookupDataService.VerifyAll();
            formulationRepository.VerifyAll();
            formulationIngredientRepository.VerifyAll();
        }
    }
}
