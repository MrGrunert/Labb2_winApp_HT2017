using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FriendOrganizer2.Model;
using FriendOrganizer2.UI.Data;
using FriendOrganizer2.UI.Data.Lookups;
using FriendOrganizer2.UI.Data.Repositories;
using FriendOrganizer2.UI.Event;
using FriendOrganizer2.UI.View.Services;
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
        private IMessageDialogService _messageDialogService;
        private IProgrammingLanguageLookupDataService _programmingLanguageLookupDataService;

        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }

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

        public ObservableCollection<LookupItem> ProgrammingLanguages { get; }

      


        public FriendDetailViewModel(IFriendRepository dataService,
               IEventAggregator eventAggregator, 
               IMessageDialogService messageDialogService,
               IProgrammingLanguageLookupDataService programmingLanguageLookupDataService)
        {
            _friendRepository = dataService;
            _eventAggregator = eventAggregator;
            _messageDialogService = messageDialogService;
            _programmingLanguageLookupDataService = programmingLanguageLookupDataService;

            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
            DeleteCommand = new DelegateCommand(OnDeleteExecute);
            ProgrammingLanguages = new ObservableCollection<LookupItem>();
        }

        public async Task LoadAsync(int? friendId)
        {
            var friend = friendId.HasValue
                ? await _friendRepository.GetBYIdAsync(friendId.Value)
                : CreateNewFriend();

            InitializeFriend(friend);
            await LoadProgrammingLanguagesLookupAsync();
        }

        private void InitializeFriend(Friend friend)
        {
            Friend = new FriendWrapper(friend);
            Friend.PropertyChanged += (s, e) =>
            {
                if (!HasChanges)
                {
                    HasChanges = _friendRepository.HasChanges();
                }
                if (e.PropertyName == nameof(Friend.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            };
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            if (Friend.Id == 0)
            {
                //Trigger the validation...
                Friend.FirstName = "";
            }
        }

        private async Task LoadProgrammingLanguagesLookupAsync()
        {
            ProgrammingLanguages.Clear();
            ProgrammingLanguages.Add(new NullLokupItem { DisplayMember = " - " });
            var lookup = await _programmingLanguageLookupDataService.GetProgrammingLanguageLookupAsync();
            foreach (var lookupItem in lookup)
            {
                ProgrammingLanguages.Add(lookupItem);
            }
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

        private Friend CreateNewFriend()
        {
            var friend = new Friend();
            _friendRepository.Add(friend);
            return friend;
        }

        private async void OnDeleteExecute()
        {
            var result = _messageDialogService.ShowOkCancelDialog($"Do you want to delete Friend {Friend.FirstName} {Friend.LastName}", "Question");
            if (result == MessageDialogResult.OK)
            {
                _friendRepository.Remove(Friend.Model);
                await _friendRepository.SaveAsync();
                _eventAggregator.GetEvent<AfterFriendDeletedEvent>().Publish(Friend.Id);
            }
           
        }

    }
}
