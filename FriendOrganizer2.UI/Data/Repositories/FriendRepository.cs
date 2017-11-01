using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Documents;
using FriendOrganizer2.DataAccess;
using FriendOrganizer2.Model;

namespace FriendOrganizer2.UI.Data.Repositories
{
    public class FriendRepository : IFriendRepository
    {

        private FriendOrganizerDbContext _context;

        public FriendRepository(FriendOrganizerDbContext context)
        {
            _context = context;
        }

        public async Task<Friend> GetBYIdAsync(int friendId)
        {
            return await _context.Friends.SingleAsync(f => f.Id == friendId);
        }

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
