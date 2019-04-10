using FormulationEditor.Model;

namespace FormulationEditor.WPF.BusinessModel
{
    public class FormulationIngredientBusinessModel : BusinessModelBase<FormulationIngredient>
    {
        public FormulationIngredientBusinessModel(FormulationIngredient model) : base(model)
        {
        }

        public int Id { get { return Model.Id; } }            

        public int Quantity
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }
    }
}
