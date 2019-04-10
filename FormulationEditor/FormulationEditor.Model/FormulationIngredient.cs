namespace FormulationEditor.Model
{
    public class FormulationIngredient
    {
        public int Id { get; set; }

        public int FormulationId { get; set; }

        public int IngredientId { get; set; }

        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
