using System.Threading.Tasks;

namespace FriendOrganizer2.UI.ViewModel
{
   public interface IFriendDetailViewModel
    {
        Task LoadAsync(int friendId);
    }
}