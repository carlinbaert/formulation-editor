using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using FormulationEditor.Model;
using FormulationEditor.WPF.BusinessModel;
using FormulationEditor.WPF.Data.Lookups;
using FormulationEditor.WPF.Data.Repositories;

namespace FormulationEditor.WPF.ViewModel
{
    public class FormulationEditViewModel : ViewModelBase, IFormulationEditViewModel
    {
        private readonly IIngredientLookupDataService _ingredientLookupDataService;
        private readonly IFormulationIngredientRepository _formulationIngredientRepository;
        private readonly IFormulationRepository _formulationRepository;
        private FormulationBusinessModel _formulationBusinessModel;
        private IEnumerable<LookupItem> _allIngredients;
        private ObservableCollection<FormulationIngredientBusinessModel> _assignedIngredientBusinessModels;
        private ObservableCollection<LookupItem> _availableIngredients;
        private LookupItem _selectedIngredient;
        private bool _ingredientIsSelected;

        public FormulationEditViewModel(IIngredientLookupDataService ingredientLookupDataService
            , IFormulationIngredientRepository formulationIngredientRepository
            , IFormulationRepository formulationRepository)
        {
            _ingredientLookupDataService = ingredientLookupDataService;
            _formulationIngredientRepository = formulationIngredientRepository;
            _formulationRepository = formulationRepository;

            PropertyChanged += FormulationEditViewModel_PropertyChanged;
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

            if (!IngredientIsSelected)
                SetNoIngredientSelectedText();
            else
                IngredientAddText = $"Enter the amount of {SelectedIngredient.DisplayMember} in Tons";
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

            SetNoIngredientSelectedText();
        }

        private void SetNoIngredientSelectedText()
        {
            IngredientAddText = "Select an ingredient to add to the formulation.";
        }

        private void UpdateAvailableIngredients()
        {
            var assignedIngredients = AssignedIngredientBusinessModels.Select(i => i.Id).ToList();

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
                AssignedIngredientBusinessModels.Add(new FormulationIngredientBusinessModel(ingredient));
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
    }
}
