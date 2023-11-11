using HotelProject.BL.Exceptions.Model;  // Assuming AddressException is in the same namespace

namespace HotelProject.BL.Model
{
    public class ActivityInfo
    {
        public ActivityInfo(string description, Address address, int duration)
        {
            _description = description;
            _address = address;
            _duration = duration;
        }

        //------------------------------------------------------------------

        // Description property
        private string _description;
        public string Description
        {
            get { return _description; }
            set { ValidateDescription(value); _description = value; }
        }

        // Validation for the Description property
        private void ValidateDescription(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ActivityInfoException("Description is empty");
            }
        }

        //------------------------------------------------------------------

        // Address property
        private Address _address;
        public Address Address
        {
            get { return _address; }
            set { ValidateAddress(value); _address = value; }
        }

        // Validation for the Address property
        private void ValidateAddress(Address value)
        {
            if (value == null)
            {
                throw new ActivityInfoException("Address is null");
            }
        }

        //------------------------------------------------------------------

        // Duration property
        private int _duration;
        public int Duration
        {
            get { return _duration; }
            set { ValidateDuration(value); _duration = value; }
        }

        // Validation for the Duration property
        private void ValidateDuration(int value)
        {
            if (value <= 0)
            {
                throw new ActivityInfoException("Invalid Duration");
            }
        }

        //------------------------------------------------------------------
    }
}
