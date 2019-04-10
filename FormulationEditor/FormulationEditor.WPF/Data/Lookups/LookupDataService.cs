using FormulationEditor.Model;
using FormulationEditor.WPF.Data.Repositories;
using System.Collections.Generic;

namespace FormulationEditor.WPF.Data.Lookups
{
    public class LookupDataService : IIngredientLookupDataService
    {
        private IIngredientRepository _ingredientRepository;

        public LookupDataService(IIngredientRepository ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
        }

        public IEnumerable<LookupItem> GetAllIngredients()
        {
            var allIngredients = _ingredientRepository.GetAll();

            var ingredientLookups = new List<LookupItem>();

            foreach (var ingredient in allIngredients)
                ingredientLookups.Add(new LookupItem() { Id = ingredient.Id, DisplayMember = ingredient.Name });

            return ingredientLookups;
        }
    }
}
