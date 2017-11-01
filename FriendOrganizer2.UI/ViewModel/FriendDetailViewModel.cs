using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FriendOrganizer2.Model;
using FriendOrganizer2.UI.Data;
using FriendOrganizer2.UI.Event;
using FriendOrganizer2.UI.Wrapper;
using Prism.Commands;
using Prism.Events;

namespace FriendOrganizer2.UI.ViewModel
{
   public  class FriendDetailViewModel : ViewModelBase, IFriendDetailViewModel
    {
        private FriendWrapper _friend;
        private IFriendDataService _dataService;
        private IEventAggregator _eventAggregator;

        public ICommand SaveCommand { get; }

        public FriendWrapper Friend
        {
            get
            {
                return _friend;
            }
            private set
            {
                _friend = value;
                OnPropertyChanged();
            }
        }

        public async Task LoadAsync(int friendId)
        {
            var friend = await _dataService.GetBYIdAsync(friendId);
            Friend = new FriendWrapper(friend);

        }

        public FriendDetailViewModel(IFriendDataService dataService,
            IEventAggregator eventAggregator)
        {
            _dataService = dataService;
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<OpenFriendDetailViewEvent>()
                .Subscribe(OnOpenFriendDetailView);

            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
        }

        private async void OnSaveExecute()
        {
           await _dataService.SaveAsync(Friend.Model);
            _eventAggregator.GetEvent<AfterFriendSaveEvent>()
                .Publish(new AfterFriendSavedEventArgs
                {
                    Id = Friend.Id,
                    DisplayMember = $"{Friend.FirstName} {Friend.LastName}"
                });
        }

        private bool OnSaveCanExecute()
        {
            //TODO: check if friend is valid...
            return true;
        }

        private async void OnOpenFriendDetailView(int friendId)
        {
            await LoadAsync(friendId);
        }
    }
}
