using System.Collections.Generic;
using FriendOrganizer2.Model;

namespace FriendOrganizer2.UI.Data.Repositories
{
    public interface IFriendRepository : IGenericRepository<Friend>
    { 
       
        void RemovePhoneNumber(FriendPhoneNumber model);

    }
}