using System;
using System.Threading.Tasks;
using FriendOrganizer2.UI.Event;
using FriendOrganizer2.UI.View.Services;
using Prism.Events;

namespace FriendOrganizer2.UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private IFriendDetailViewModel _friendDetailViewModel;
        private IEventAggregator _eventAggregator;

        public INavigationViewModel NavigationViewModel { get; }

        private Func<IFriendDetailViewModel> _friendDetailViewModelCreator;
        private IMessageDialogService _messageDialogService;


        public IFriendDetailViewModel FriendDetailViewModel
        {
            get { return _friendDetailViewModel; }
           private set
            {
                _friendDetailViewModel = value; 
                OnPropertyChanged();
            }
        }



        public MainViewModel(INavigationViewModel navigationViewModel,
            Func<IFriendDetailViewModel> friendDetailViewModelCreator,
            IEventAggregator eventAggregator,
            IMessageDialogService MessageDialogService)
        {
            _eventAggregator = eventAggregator;
            _friendDetailViewModelCreator = friendDetailViewModelCreator;
            _messageDialogService = MessageDialogService;

            _eventAggregator.GetEvent<OpenFriendDetailViewEvent>()
                .Subscribe(OnOpenFriendDetailView);
            NavigationViewModel = navigationViewModel;
        }

        public async Task LoadAsync()
        {
            await NavigationViewModel.LoadAsync();
        }

        private async void OnOpenFriendDetailView(int friendId)
        {
            if (FriendDetailViewModel != null && FriendDetailViewModel.HasChanges)
            {
                var result =
                    _messageDialogService.ShowOkCancelDialog("You've made changes. Navigate away?", "Question");                  
                if (result == MessageDialogResult.Cancel)
                {
                    return;
                }
            }
           FriendDetailViewModel = _friendDetailViewModelCreator();
            await FriendDetailViewModel.LoadAsync(friendId);
        }
    }
}
