using FormulationEditor.Model;

namespace FormulationEditor.WPF.Data.Repositories
{
    public interface IIngredientRepository : IGenericRepository<Ingredient>
    {
        Ingredient GetById(int ingredientId);
    }
}