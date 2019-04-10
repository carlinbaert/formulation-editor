using FormulationEditor.Model;

namespace IngredientPricingAPI.Models
{
    public class IngredientPrice : IIngredientPrice
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
    }
}
