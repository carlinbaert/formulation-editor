using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using FormulationEditor.Model;
using FormulationEditor.WPF.BusinessModel;
using FormulationEditor.WPF.Data.Lookups;
using FormulationEditor.WPF.Data.Repositories;
using Prism.Commands;

namespace FormulationEditor.WPF.ViewModel
{
    public class FormulationEditViewModel : ViewModelBase, IFormulationEditViewModel
    {
        private readonly IIngredientLookupDataService _ingredientLookupDataService;
        private readonly IFormulationIngredientRepository _formulationIngredientRepository;
        private readonly IFormulationRepository _formulationRepository;
        private readonly IIngredientRepository _ingredientRepository;
        private FormulationBusinessModel _formulationBusinessModel;
        private IEnumerable<LookupItem> _allIngredients;
        private ObservableCollection<FormulationIngredientBusinessModel> _assignedIngredientBusinessModels;
        private ObservableCollection<LookupItem> _availableIngredients;
        private LookupItem _selectedIngredient;
        private bool _ingredientIsSelected;
        private FormulationIngredientBusinessModel _formulationIngredientBusinessModel;

        public FormulationEditViewModel(IIngredientLookupDataService ingredientLookupDataService
            , IFormulationIngredientRepository formulationIngredientRepository
            , IFormulationRepository formulationRepository
            , IIngredientRepository ingredientRepository)
        {
            _ingredientLookupDataService = ingredientLookupDataService;
            _formulationIngredientRepository = formulationIngredientRepository;
            _formulationRepository = formulationRepository;
            _ingredientRepository = ingredientRepository;

            AddSelectedIngredientCommand = new DelegateCommand(OnAddSelectedIngredient, OnAddSelectedIngredientCanExecute);
            RemoveFormulationIngredientCommand = new DelegateCommand<object>(OnRemoveFormulationIngredient);

            PropertyChanged += FormulationEditViewModel_PropertyChanged;
        }

        private void OnRemoveFormulationIngredient(object id)
        {
            if (id.GetType() != typeof(int))
                return;

            var itemToRemove = AssignedIngredientBusinessModels.FirstOrDefault(i => i.Id == (int)id);

            if (itemToRemove == null)
                return;

            itemToRemove.PropertyChanged -= FormulationIngredientBusinessModel_PropertyChanged;

            AssignedIngredientBusinessModels.Remove(itemToRemove);

            CalculateTotalFormulationPrice();

            UpdateAvailableIngredients();
        }

        private void FormulationEditViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.PropertyName))
                return;

            switch (e.PropertyName)
            {
                case "SelectedIngredient":
                    OnSelectedIngredientChanged();
                    break;
            }
        }

        private void OnSelectedIngredientChanged()
        {
            IngredientIsSelected = _selectedIngredient != null;

            if (IngredientIsSelected)
            {
                IngredientAddText = $"How much {SelectedIngredient.DisplayMember} in Tons?";
                MarketPriceLabelText = $"Price for 1 Ton of {SelectedIngredient.DisplayMember}";
                InitializeIngredientFormulationBusinessModel();
            }

            ((DelegateCommand)AddSelectedIngredientCommand).RaiseCanExecuteChanged();
        }

        public void Load(int formulationId)
        {
            Formulation formulation;

            if (formulationId == 0)
                formulation = new Formulation();
            else
                formulation = _formulationRepository.GetById(formulationId);                

            FormulationBusinessModel = new FormulationBusinessModel(formulation);

            AssignedIngredientBusinessModels = new ObservableCollection<FormulationIngredientBusinessModel>();

            AvailableIngredients = new ObservableCollection<LookupItem>();

            LoadAssignedIngredients();

            LoadAllIngredients();

            UpdateAvailableIngredients();
        }

        private void UpdateAvailableIngredients()
        {
            var assignedIngredients = AssignedIngredientBusinessModels.Select(i => i.IngredientId).ToList();

            var availableIngredients = _allIngredients.Where(i => !assignedIngredients.Contains(i.Id));

            AvailableIngredients.Clear();

            foreach (var ingredient in availableIngredients)
                AvailableIngredients.Add(ingredient);
        }

        private void LoadAllIngredients()
        {
            _allIngredients = _ingredientLookupDataService.GetAllIngredients();
        }

        private void LoadAssignedIngredients()
        {
            var assignedIngredients = _formulationIngredientRepository.GetByFormulationId(_formulationBusinessModel.Id);

            AssignedIngredientBusinessModels.Clear();

            foreach (var ingredient in assignedIngredients)
            {
                var ingredientModel = _ingredientRepository.GetById(ingredient.IngredientId);
                AssignedIngredientBusinessModels.Add(new FormulationIngredientBusinessModel(ingredient, ingredientModel));
            }                
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

        public ObservableCollection<FormulationIngredientBusinessModel> AssignedIngredientBusinessModels
        {
            get { return _assignedIngredientBusinessModels; }
            set
            {
                if (_assignedIngredientBusinessModels != value)
                {
                    _assignedIngredientBusinessModels = value;
                    NotifyPropertyChanged();
                }                
            }
        }

        public ObservableCollection<LookupItem> AvailableIngredients
        {
            get { return _availableIngredients; }
            set
            {
                if (_availableIngredients != value)
                {
                    _availableIngredients = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public LookupItem SelectedIngredient
        {
            get { return _selectedIngredient; }
            set
            {
                if (_selectedIngredient != value)
                {
                    _selectedIngredient = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool IngredientIsSelected
        {
            get { return _ingredientIsSelected; }
            private set
            {
                if (_ingredientIsSelected != value)
                {
                    _ingredientIsSelected = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _ingredientAddText;

        public string IngredientAddText
        {
            get { return _ingredientAddText; }
            set
            {
                if (_ingredientAddText != value)
                {
                    _ingredientAddText = value;
                    NotifyPropertyChanged();
                }                
            }
        }

        public ICommand AddSelectedIngredientCommand { get; }

        public ICommand RemoveFormulationIngredientCommand { get; }

        private string _marketPriceLabelText;

        public string MarketPriceLabelText
        {
            get { return _marketPriceLabelText; }
            set
            {
                if (_marketPriceLabelText != value)
                {
                    _marketPriceLabelText = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private void OnAddSelectedIngredient()
        {
            _formulationIngredientRepository.Add(FormulationIngredientBusinessModel.Model);

            AssignedIngredientBusinessModels.Add(FormulationIngredientBusinessModel);

            UpdateAvailableIngredients();

            CalculateTotalFormulationPrice();

            FormulationIngredientBusinessModel = null;

            SelectedIngredient = null;
        }

        private bool OnAddSelectedIngredientCanExecute()
        {
            return _ingredientIsSelected && FormulationIngredientBusinessModel != null && FormulationIngredientBusinessModel.Quantity > 0;
        }

        public FormulationIngredientBusinessModel FormulationIngredientBusinessModel
        {
            get { return _formulationIngredientBusinessModel; }
            set
            {
                if (_formulationIngredientBusinessModel != value)
                {
                    _formulationIngredientBusinessModel = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private void InitializeIngredientFormulationBusinessModel()
        {
            var ingredientModel = _ingredientRepository.GetById(SelectedIngredient.Id);

            var formulationIngredient = new FormulationIngredient { IngredientId = SelectedIngredient.Id, FormulationId = FormulationBusinessModel.Id };

            FormulationIngredientBusinessModel = new FormulationIngredientBusinessModel(formulationIngredient, ingredientModel);

            FormulationIngredientBusinessModel.PropertyChanged += FormulationIngredientBusinessModel_PropertyChanged;
        }

        private void FormulationIngredientBusinessModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.PropertyName))
                return;

            if (sender.GetType() != typeof(FormulationIngredientBusinessModel))
                return;

            var formulationIngredient = (FormulationIngredientBusinessModel)sender;

            if (e.PropertyName == "Quantity")
            {
                formulationIngredient.CalculateTotalPrice();
                ((DelegateCommand)AddSelectedIngredientCommand).RaiseCanExecuteChanged();

                if (formulationIngredient != FormulationIngredientBusinessModel)
                    CalculateTotalFormulationPrice();
            }                
        }

        private void CalculateTotalFormulationPrice()
        {
            FormulationBusinessModel.TotalPrice = 0;

            foreach (var ingredient in AssignedIngredientBusinessModels)
                FormulationBusinessModel.TotalPrice += ingredient.TotalPrice;
        }
    }
}
