using System.Collections.Generic;
using FormulationEditor.Model;

namespace FormulationEditor.WPF.Data.Lookups
{
    public interface IIngredientLookupDataService
    {
        IEnumerable<LookupItem> GetAllIngredients();
    }
}