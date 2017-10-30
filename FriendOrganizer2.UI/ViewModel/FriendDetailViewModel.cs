using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FriendOrganizer2.Model;
using FriendOrganizer2.UI.Data;

namespace FriendOrganizer2.UI.ViewModel
{
   public  class FriendDetailViewModel : ViewModelBase, IFriendDetailViewModel
    {
        private Friend _friend;
        private IFriendDataService _dataService;

        public Friend Friend
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

        public FriendDetailViewModel(IFriendDataService dataService)
        {
            _dataService = dataService;
        }

        public async Task LoadAsync(int friendId)
        {
            Friend = await _dataService.GetBYIdAsync(friendId);
        }
    }
}
