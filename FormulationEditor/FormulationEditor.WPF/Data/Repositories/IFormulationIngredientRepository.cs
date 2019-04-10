using FormulationEditor.Model;
using System.Collections.Generic;

namespace FormulationEditor.WPF.Data.Repositories
{
    public interface IFormulationIngredientRepository
    {
        FormulationIngredient GetById(int formulationIngredientId);
        IEnumerable<FormulationIngredient> GetByFormulationId(int id);
    }
}