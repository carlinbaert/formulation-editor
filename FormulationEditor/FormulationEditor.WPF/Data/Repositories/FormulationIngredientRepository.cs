using FormulationEditor.Model;
using System.Collections.Generic;
using System.Linq;

namespace FormulationEditor.WPF.Data.Repositories
{
    public class FormulationIngredientRepository : GenericRepository<FormulationIngredient>, IFormulationIngredientRepository
    {
        public IEnumerable<FormulationIngredient> GetByFormulationId(int formulationId)
        {
            return AllItems.Where(m => m.FormulationId == formulationId);
        }

        public FormulationIngredient GetById(int formulationIngredientId)
        {
            return AllItems.FirstOrDefault(f => f.Id == formulationIngredientId);
        }

        protected override void SetNextId(FormulationIngredient model)
        {
            if (AllItems.Count == 0)
                model.Id = 1;
            else
                model.Id = AllItems.Select(m => m.Id).Max() + 1;
        }
    }
}
