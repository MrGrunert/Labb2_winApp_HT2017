using System.Threading.Tasks;

namespace FriendOrganizer2.UI.ViewModel
{
    public interface IDetailViewModel
    {
        Task LoadAsync(int id);
        bool HasChanges { get; }
        int Id { get; }
    }
}
