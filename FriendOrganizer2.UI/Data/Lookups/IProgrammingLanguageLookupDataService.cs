using System.Collections.Generic;
using System.Threading.Tasks;
using FriendOrganizer2.Model;

namespace FriendOrganizer2.UI.Data.Lookups
{
    public interface IProgrammingLanguageLookupDataService
    {
        Task<IEnumerable<LookupItem>> GetProgrammingLanguageLookupAsync();
    }
}