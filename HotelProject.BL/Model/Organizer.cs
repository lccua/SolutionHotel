using HotelProject.BL.Exceptions.Model;

namespace HotelProject.BL.Model
{
    public class Organizer
    {

        public Organizer(int organizerId, string name, ContactInfo contactInfo)
        {
            _organizerId = organizerId;
            _name = name;
            _contactInfo = contactInfo;
        }

        public Organizer(string name, ContactInfo contactInfo)
        {
            _name = name;
            _contactInfo = contactInfo;
        }

        //------------------------------------------------------------------

        private int _organizerId;
        public int OrganizerId
        {
            get { return _organizerId; }
            set { ValidateOrganizerId(value); _organizerId = value; }
        }

        private void ValidateOrganizerId(int value)
        {
            if (value <= 0)
            {
                throw new OrganizerException("Invalid OrganizerId");
            }
        }

        //------------------------------------------------------------------

        private string _name;
        public string Name
        {
            get { return _name; }
            set { ValidateName(value); }
        }

        private void ValidateName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new OrganizerException("Name cannot be null or empty");
            }
            _name = value;
        }

        //------------------------------------------------------------------


        private ContactInfo _contactInfo;
        public ContactInfo ContactInfo
        {
            get { return _contactInfo; }
            set { ValidateContactInfo(value); }
        }

        private void ValidateContactInfo(ContactInfo value)
        {
            if (value == null)
            {
                throw new OrganizerException("ContactInfo is null");
            }
            _contactInfo = value;
        }

        //------------------------------------------------------------------

        private string _username;
        public string Username
        {
            get { return _username; }
            set { ValidateUsername(value); }
        }

        private void ValidateUsername(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new OrganizerException("Username cannot be null or empty");
            }
            _username = value;
        }

        //------------------------------------------------------------------

        private string _hashedPassword;
        public string HashedPassword
        {
            get { return _hashedPassword; }
            set { ValidateHashedPassword(value); }
        }

        private void ValidateHashedPassword(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new OrganizerException("HashedPassword cannot be null or empty");
            }
            _hashedPassword = value;
        }

        //------------------------------------------------------------------

    }
}
