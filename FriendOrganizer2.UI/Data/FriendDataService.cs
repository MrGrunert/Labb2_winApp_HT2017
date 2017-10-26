using System.Collections.Generic;
using FriendOrganizer2.Model;

namespace FriendOrganizer2.UI.Data
{
   public class FriendDataService : IFriendDataService
    {
        public IEnumerable<Friend> Getall()
        {
            //TODO: Load data from DB
            yield return new Friend { FirstName = "Dean", LastName = "Winchester" };
            yield return new Friend { FirstName = "Sam", LastName = "Winchester" };
            yield return new Friend { FirstName = "Mr", LastName = "Crowley" };
            yield return new Friend { FirstName = "Castiel", LastName = "H" };
        }
    }
}
