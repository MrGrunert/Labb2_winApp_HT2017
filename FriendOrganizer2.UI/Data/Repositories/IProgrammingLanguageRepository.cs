using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FriendOrganizer2.Model;

namespace FriendOrganizer2.UI.Data.Repositories
{
    public interface IProgrammingLanguageRepository
        : IGenericRepository<ProgrammingLanguage>
    {
        Task<bool> IsReferencedByFriendAsync(int programmingLanguageId);
    }
}
