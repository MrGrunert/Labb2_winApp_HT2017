using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FriendOrganizer2.Model;

namespace FriendOrganizer2.UI.Wrapper
{
    public class FriendWrapper : ModelWrapper<Friend>
    {

        public int Id { get { return Model.Id; } }

        public FriendWrapper(Friend model) : base(model)
        {
        }

        public string FirstName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }


        public string LastName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string Email
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }


        protected override IEnumerable<string> ValidateProperty(string propertyName)
        {
            switch (propertyName)
            {
                case nameof(FirstName):
                    if (string.Equals(FirstName, "Monkies", StringComparison.OrdinalIgnoreCase))
                    {
                        yield return "Monkies are not valid friends";
                    }
                    break;
            }
        }
    }
}
