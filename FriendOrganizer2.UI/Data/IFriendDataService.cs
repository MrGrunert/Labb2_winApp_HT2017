using System.Collections.Generic;
using FriendOrganizer2.Model;

namespace FriendOrganizer2.UI.Data
{
    public interface IFriendDataService
    {
        IEnumerable<Friend> Getall();
    }
}