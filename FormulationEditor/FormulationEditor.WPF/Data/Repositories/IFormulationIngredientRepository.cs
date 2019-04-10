using FormulationEditor.Model;
using System.Collections.Generic;

namespace FormulationEditor.WPF.Data.Repositories
{
    public interface IFormulationIngredientRepository : IGenericRepository<FormulationIngredient>
    {
        FormulationIngredient GetById(int formulationIngredientId);
        IEnumerable<FormulationIngredient> GetByFormulationId(int id);
    }
}