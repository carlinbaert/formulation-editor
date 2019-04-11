using FormulationEditor.Model;

namespace FormulationEditor.WPF.BusinessModel
{
    public class FormulationIngredientBusinessModel : BusinessModelBase<FormulationIngredient>
    {
        private Ingredient _ingredient;

        public FormulationIngredientBusinessModel(FormulationIngredient model, Ingredient ingredient) : base(model)
        {
            _ingredient = ingredient;
        }

        public int Id { get { return Model.Id; } }            

        public int Quantity
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public string Name
        {
            get { return _ingredient.Name; }
        }

        public int IngredientId
        {
            get { return _ingredient.Id; }
        }

        public decimal Price
        {
            get { return _ingredient.Price; }
        }

        public decimal TotalPrice
        {
            get { return Model.TotalPrice; }
            private set
            {
                if (Model.TotalPrice != value)
                {
                    Model.TotalPrice = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public void CalculateTotalPrice()
        {
            TotalPrice = Price * Quantity;
        }
    }
}
