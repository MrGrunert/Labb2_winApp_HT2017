using System;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using FriendOrganizer2.Model;

namespace FriendOrganizer2.UI.Wrapper
{
    public class ModelWrapper<T> :NotifyDataErrorInfoBase
    {
        public T Model { get; }

        public ModelWrapper(T model)
        {
            Model = model;
        }

        protected virtual TValue GetValue<TValue>([CallerMemberName]string propertyName = null)
        {
            return (TValue)typeof(T).GetProperty(propertyName).GetValue(Model);

        }

    }

    public class FriendWrapper : ModelWrapper<Friend>
    {
        
        public int Id { get { return Model.Id; } }

        public string FirstName
        {
            get { return GetValue<string>(); }
            set
            {
                Model.FirstName = value;
                OnPropertyChanged();
                ValidateProperty(nameof(FirstName));
            }
        }

       

        public string LastName
        {
            get { return GetValue<string>(); }
            set
            {
                Model.LastName = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get { return GetValue<string>(); }
            set
            {
                Model.Email = value;
                OnPropertyChanged();
            }
        }

      

        private void ValidateProperty(string propertyName)
        {
            ClearErrors(propertyName);
            switch (propertyName)
            {
                case nameof(FirstName):
                    if (string.Equals(FirstName, "Monkies", StringComparison.OrdinalIgnoreCase))
                    {
                        AddError(propertyName, "Monkies are not valid friends");
                    }
                    break;
            }

        }

        public FriendWrapper(Friend model) : base(model)
        {
        }
    }
}
