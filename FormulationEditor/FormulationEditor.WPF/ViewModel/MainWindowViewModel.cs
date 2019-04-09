using System.Threading.Tasks;

namespace FormulationEditor.WPF.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private FormulationEditViewModel _formulationEditViewModel;

        public MainWindowViewModel()
        {

        }

        public async Task LoadAsync()
        {
            var formulationEditViewModel = new FormulationEditViewModel();
            formulationEditViewModel.Load();

            FormulationEditViewModel = formulationEditViewModel;
        }

        public FormulationEditViewModel FormulationEditViewModel
        {
            get { return _formulationEditViewModel; }
            set
            {
                if (_formulationEditViewModel != value)
                {
                    _formulationEditViewModel = value;
                    NotifyPropertyChanged();
                }
            }
        }
    }
}
