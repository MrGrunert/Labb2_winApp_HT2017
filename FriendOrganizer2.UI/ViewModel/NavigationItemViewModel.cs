

namespace FriendOrganizer2.UI.ViewModel 
{
    public class NavigationItemViewModel :ViewModelBase
    {
        private string _displayMember;
        public int Id { get; }

        public string DisplayMember
        {
            get
            {
                return _displayMember;
            }
            set
            {
                _displayMember = value;
                OnPropertyChanged();
            }
        }


        public NavigationItemViewModel(int id, string displayMember)
        {
            Id = id;
            DisplayMember = displayMember;
        }
    }
}
