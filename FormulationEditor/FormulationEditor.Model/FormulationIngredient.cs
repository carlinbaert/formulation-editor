namespace FormulationEditor.Model
{
    public class FormulationIngredient
    {
        public string Id { get; set; }

        public string FormulationId { get; set; }

        public string IngredientId { get; set; }

        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
