using HotelProject.BL.Exceptions.Model;

namespace HotelProject.BL.Model
{
    public class ContactInfo
    {
        public ContactInfo(string email, string phone, Address address)
        {
            Email = email;
            Phone = phone;
            Address = address;
        }

        //------------------------------------------------------------------

        private string _email;
        public string Email
        {
            get { return _email; }
            set { ValidateEmail(value); _email = value; }
        }

        private void ValidateEmail(string value)
        {
            if (string.IsNullOrEmpty(value) || !value.Contains('@'))
            {
                throw new ContactInfoException("Email is empty or invalid");
            }
        }

        //------------------------------------------------------------------

        private string _phone;
        public string Phone
        {
            get { return _phone; }
            set { ValidatePhone(value); _phone = value; }
        }

        private void ValidatePhone(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ContactInfoException("Phone is empty");
            }
        }

        //------------------------------------------------------------------

        private Address _address;
        public Address Address
        {
            get { return _address; }
            set { ValidateAddress(value); _address = value; }
        }

        private void ValidateAddress(Address value)
        {
            if (value == null)
            {
                throw new ContactInfoException("Address is null");
            }
        }

        //------------------------------------------------------------------
    }
}
