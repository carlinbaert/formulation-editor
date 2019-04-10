using FormulationEditor.Model;
using System.Linq;

namespace FormulationEditor.WPF.Data.Repositories
{
    public class FormulationIngredientRepository : GenericRepository<FormulationIngredient>, IFormulationIngredientRepository
    {
        public FormulationIngredient GetById(int formulationIngredientId)
        {
            return AllItems.FirstOrDefault(f => f.Id == formulationIngredientId);
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
