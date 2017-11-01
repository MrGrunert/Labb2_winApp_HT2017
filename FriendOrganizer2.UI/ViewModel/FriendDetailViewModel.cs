using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FriendOrganizer2.Model;
using FriendOrganizer2.UI.Data;
using FriendOrganizer2.UI.Data.Repositories;
using FriendOrganizer2.UI.Event;
using FriendOrganizer2.UI.Wrapper;
using Prism.Commands;
using Prism.Events;

namespace FriendOrganizer2.UI.ViewModel
{
   public  class FriendDetailViewModel : ViewModelBase, IFriendDetailViewModel
    {
        private bool _hasChanges;
        private FriendWrapper _friend;
        private IFriendRepository _friendRepository;
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

        public bool HasChanges
        {
            get { return _hasChanges; }
            set
            {
                if (_hasChanges != value)
                {
                    _hasChanges = value;
                    OnPropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
               
            }
        }

        public async Task LoadAsync(int friendId)
        {
            var friend = await _friendRepository.GetBYIdAsync(friendId);
            Friend = new FriendWrapper(friend);
            Friend.PropertyChanged += (s, e) =>
            {
                if (!HasChanges)
                {
                    HasChanges = _friendRepository.HasChanges();
                }
                if (e.PropertyName == nameof(Friend.HasErrors))
                {
                    ((DelegateCommand) SaveCommand).RaiseCanExecuteChanged();
                }
            };
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        public FriendDetailViewModel(IFriendRepository dataService,
            IEventAggregator eventAggregator)
        {
            _friendRepository = dataService;
            _eventAggregator = eventAggregator;
            

            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
        }

        private async void OnSaveExecute()
        {
           await _friendRepository.SaveAsync();
            HasChanges = _friendRepository.HasChanges();
            _eventAggregator.GetEvent<AfterFriendSaveEvent>()
                .Publish(new AfterFriendSavedEventArgs
                {
                    Id = Friend.Id,
                    DisplayMember = $"{Friend.FirstName} {Friend.LastName}"
                });
        }

        private bool OnSaveCanExecute()
        {
            return Friend != null && !Friend.HasErrors && HasChanges;
        }
    }
}
