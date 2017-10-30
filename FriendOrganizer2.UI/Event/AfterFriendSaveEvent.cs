

using Prism.Events;

namespace FriendOrganizer2.UI.Event
{
    public class AfterFriendSaveEvent : PubSubEvent<AfterFriendSavedEventArgs>
    {
    }

    public class AfterFriendSavedEventArgs
    {
        public int Id { get; set; }
        public string DisplayMember { get; set; }

    }
}
