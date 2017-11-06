using System.Data.Entity;
using System.Threading.Tasks;
using FriendOrganizer2.DataAccess;
using FriendOrganizer2.Model;

namespace FriendOrganizer2.UI.Data.Repositories
{
    public class ProgrammingLanguageRepository
        : GenericRepository<ProgrammingLanguage, FriendOrganizerDbContext>,
            IProgrammingLanguageRepository
    {
        public ProgrammingLanguageRepository(FriendOrganizerDbContext context)
            : base(context)
        {
        }

        public async Task<bool> IsReferencedByFriendAsync(int programmingLanguageId)
        {
            return await Context.Friends.AsNoTracking()
                .AnyAsync(f => f.FavoriteLanguageId == programmingLanguageId);
        }
    }
}
