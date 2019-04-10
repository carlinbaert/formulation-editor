using FormulationEditor.WPF.Data.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace FormulationEditor.WPF.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private FormulationEditViewModel _formulationEditViewModel;
        private IFormulationRepository _formulationRepository;

        public MainWindowViewModel(IFormulationRepository formulationRepository)
        {
            _formulationRepository = formulationRepository;
        }

        public void Load()
        {
            if (!_formulationRepository.GetAll().Any())
            {
                var formulationEditViewModel = new FormulationEditViewModel();
                formulationEditViewModel.Load();

                FormulationEditViewModel = formulationEditViewModel;
            }
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
