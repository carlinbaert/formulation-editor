using FormulationEditor.Model;

namespace FormulationEditor.WPF.BusinessModel
{
    public class FormulationBusinessModel : BusinessModelBase<Formulation>
    {
        public FormulationBusinessModel(Formulation model) : base(model)
        {
        }

        public int Id { get { return Model.Id; } }

        public string Name
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public decimal TotalPrice
        {
            get { return GetValue<decimal>(); }
            set { SetValue(value); }
        }
    }
}
