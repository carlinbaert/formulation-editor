using FormulationEditor.Model;
using System.Linq;

namespace FormulationEditor.WPF.Data.Repositories
{
    public class IngredientPriceRepository : GenericRepository<IngredientPrice>, IIngredientPriceRepository
    {
        protected override void SetNextId(IngredientPrice model)
        {
            if (AllItems.Count == 0)
                model.Id = 1;
            else
                model.Id = AllItems.Select(m => m.Id).Max() + 1;
        }
    }
}
