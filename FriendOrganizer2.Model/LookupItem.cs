

namespace FriendOrganizer2.Model
{
    public class LookupItem
    {
        public int Id { get; set; }
        public string DisplayMember { get; set; }

    }

    public class NullLokupItem : LookupItem
    {
        public new int? Id { get { return null; }
        }

    }
}
