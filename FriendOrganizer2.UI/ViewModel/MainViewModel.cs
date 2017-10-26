using System.Collections.ObjectModel;
using FriendOrganizer2.Model;
using FriendOrganizer2.UI.Data;

namespace FriendOrganizer2.UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private Friend _selectedFriend;
        private IFriendDataService _friendDataService;


        public ObservableCollection<Friend> Friends { get; set; }

        public Friend SelectedFriend
        {
            get { return _selectedFriend; }
            set
            {
                _selectedFriend = value;
                OnPropertyChanged();
            }

        }

        public MainViewModel(IFriendDataService friendDataService)
        {
            Friends = new ObservableCollection<Friend>();
            _friendDataService = friendDataService;
        }

        public void Load()
        {
            var friends = _friendDataService.Getall();
            Friends.Clear();
            foreach (var friend in friends)
            {
                Friends.Add(friend);
            }
        }
    }
}
