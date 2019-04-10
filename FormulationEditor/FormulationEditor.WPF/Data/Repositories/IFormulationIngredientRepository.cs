using FormulationEditor.Model;

namespace FormulationEditor.WPF.Data.Repositories
{
    public interface IFormulationIngredientRepository
    {
        FormulationIngredient GetById(int formulationIngredientId);
    }
}