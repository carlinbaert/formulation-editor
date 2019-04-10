using FormulationEditor.Model;
using System.Collections.Generic;
using System.Linq;

namespace FormulationEditor.WPF.Data.Repositories
{
    public class IngredientRepository : GenericRepository<Ingredient>, IIngredientRepository
    {
        public Ingredient GetById(int ingredientId)
        {
            return AllItems.FirstOrDefault(i => i.Id == ingredientId);
        }

        protected override void SetNextId(Ingredient model)
        {
            if (AllItems.Count == 0)
                model.Id = 1;
            else
                model.Id = AllItems.Select(m => m.Id).Max() + 1;
        }

        public override IEnumerable<Ingredient> GetAll()
        {
            return AllItems;
        }
    }
}
