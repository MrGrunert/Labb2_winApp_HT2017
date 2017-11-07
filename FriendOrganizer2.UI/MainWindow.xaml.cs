
using System.Windows;

using FriendOrganizer2.UI.ViewModel;
using MahApps.Metro.Controls;

namespace FriendOrganizer2.UI
{

    public partial class MainWindow : MetroWindow
    {
        private MainViewModel _viewModel;

        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
          await  _viewModel.LoadAsync();
        }
    }
}
