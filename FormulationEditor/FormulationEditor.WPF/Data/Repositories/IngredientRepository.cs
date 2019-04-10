using FormulationEditor.Model;
using System.Linq;

namespace FormulationEditor.WPF.Data.Repositories
{
    public class IngredientRepository : GenericRepository<Ingredient>, IIngredientRepository
    {
        public Ingredient GetById(int ingredientId)
        {
            return AllItems.FirstOrDefault(i => i.Id == ingredientId);
        }

        protected override int GetNextId()
        {
            if (AllItems.Count == 0)
                return 1;

            var maxId = AllItems.Select(m => m.Id).Max();

            return maxId + 1;
        }
    }
}
