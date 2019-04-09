using FormulationEditor.Model;
using FormulationEditor.WPF.BusinessModel;

namespace FormulationEditor.WPF.ViewModel
{
    public class FormulationEditViewModel : ViewModelBase
    {
        private FormulationBusinessModel _formulationBusinessModel;

        internal void Load()
        {
            FormulationBusinessModel = new FormulationBusinessModel(new Formulation());
        }

        public FormulationBusinessModel FormulationBusinessModel
        {
            get { return _formulationBusinessModel; }
            set
            {
                if (_formulationBusinessModel != value)
                {
                    _formulationBusinessModel = value;
                    NotifyPropertyChanged();
                }
            }
        }
    }
}
