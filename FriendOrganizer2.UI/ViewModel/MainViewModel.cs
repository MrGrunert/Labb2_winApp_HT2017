﻿using System;
using System.Threading.Tasks;
using System.Windows.Input;
using FriendOrganizer2.UI.Event;
using FriendOrganizer2.UI.View.Services;
using Prism.Commands;
using Prism.Events;

namespace FriendOrganizer2.UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private IDetailViewModel _detailViewModel;
        private IEventAggregator _eventAggregator;
        private Func<IFriendDetailViewModel> _friendDetailViewModelCreator;
        private IMessageDialogService _messageDialogService;

        public INavigationViewModel NavigationViewModel { get; }
        public ICommand CreateNewDetailCommand { get; }

        public IDetailViewModel DetailViewModel
            {
            get { return _detailViewModel; }
           private set
            {
                _detailViewModel = value; 
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
            _eventAggregator.GetEvent<OpenDetailViewEvent>().Subscribe(OnOpenDetailView);
            _eventAggregator.GetEvent<AfterDetailDeletedEvent>()
                .Subscribe(AfterDetailDeleted);

            CreateNewDetailCommand = new DelegateCommand<Type>(OnCreateNewDetailExecute);
            NavigationViewModel = navigationViewModel;
        }

    


        public async Task LoadAsync()
        {
            await NavigationViewModel.LoadAsync();
        }

        private async void OnOpenDetailView(OpenDetailViewEventArgs args)
        {
            if (DetailViewModel != null && DetailViewModel.HasChanges)
            {
                var result =
                    _messageDialogService.ShowOkCancelDialog("You've made changes. Navigate away?", "Question");                  
                if (result == MessageDialogResult.Cancel)
                {
                    return;
                }
            }
            switch (args.ViewModelName)
            {
                case nameof(FriendDetailViewModel):
                    DetailViewModel = _friendDetailViewModelCreator();
                    break;
            }
           
            await DetailViewModel.LoadAsync(args.Id);
        }

        private void OnCreateNewDetailExecute(Type viewModelType)
        {
           OnOpenDetailView(
               new OpenDetailViewEventArgs{ViewModelName = viewModelType.Name});
        }

        private void AfterDetailDeleted(AfterDetailDeletedEventArgs args)
        {
            DetailViewModel = null;
        }
    }
}
