using FormulationEditor.WPF.Data.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FormulationEditor.WPF.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private IFormulationEditViewModel _formulationEditViewModel;
        private IFormulationRepository _formulationRepository;
        private Func<IFormulationEditViewModel> _formulationEditViewModelCreator;

        public MainWindowViewModel(IFormulationRepository formulationRepository
            , Func<IFormulationEditViewModel> formulationEditViewModelCreator)
        {
            _formulationRepository = formulationRepository;
            _formulationEditViewModelCreator = formulationEditViewModelCreator;
        }

        public void Load()
        {
            if (!_formulationRepository.GetAll().Any())
            {
                var formulationEditViewModel = _formulationEditViewModelCreator();
                formulationEditViewModel.Load(0);

                FormulationEditViewModel = formulationEditViewModel;
            }
        }

        public IFormulationEditViewModel FormulationEditViewModel
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
