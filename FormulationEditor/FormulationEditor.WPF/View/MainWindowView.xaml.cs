﻿using FormulationEditor.WPF.ViewModel;
using System.Windows;

namespace FormulationEditor.WPF.View
{
    public partial class MainWindowView : Window
    {
        private MainWindowViewModel _mainWindowViewModel;

        public MainWindowView(MainWindowViewModel mainWindowViewModel)
        {
            InitializeComponent();
            _mainWindowViewModel = mainWindowViewModel;
            DataContext = _mainWindowViewModel;
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _mainWindowViewModel.Load();
        }
    }
}
