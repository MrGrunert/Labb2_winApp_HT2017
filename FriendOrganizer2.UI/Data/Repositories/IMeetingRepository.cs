using System.Collections.Generic;
using System.Threading.Tasks;
using FriendOrganizer2.Model;

namespace FriendOrganizer2.UI.Data.Repositories
{
    public interface IMeetingRepository : IGenericRepository<Meeting>
    {
        Task<List<Friend>> GetAllFriendsAsync();
        Task ReloadFriendAsync(int friendId);
    }
}