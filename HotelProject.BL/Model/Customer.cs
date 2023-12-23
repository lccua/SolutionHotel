using HotelProject.BL.Exceptions.Model;

namespace HotelProject.BL.Model
{
    public class Customer
    {
        public Customer() { }
      
        public Customer(string name, ContactInfo contactInfo)
        {
            _name = name;
            _contactInfo = contactInfo;
        }

        public Customer(string name, int id, ContactInfo contactInfo)
        {
            _name = name;
            _id = id;
            _contactInfo = contactInfo;
        }

        //------------------------------------------------------------------

        
        

        public void RemoveMember(Member member)
        {
            if (_members.Contains(member))
                _members.Remove(member);
            else
                throw new CustomerException("removemember");
        }

        public List<Member> GetMembers()
        {
            // Returning a copy of the internal list to prevent external modification
            return new List<Member>(_members);
        }


        //------------------------------------------------------------------

        private string _name;
        public string Name
        {
            get { return _name; }
            set { ValidateName(value); _name = value; }
        }

        private void ValidateName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new CustomerException("Name is empty");
            }
        }

        //------------------------------------------------------------------

        private int _id;
        public int Id
        {
            get { return _id; }
            set { ValidateId(value); _id = value; }
        }

        private void ValidateId(int value)
        {
            if (value <= 0)
            {
                throw new CustomerException("Invalid ID");
            }
        }

        //------------------------------------------------------------------

        private ContactInfo _contactInfo;
        public ContactInfo ContactInfo
        {
            get { return _contactInfo; }
            set { ValidateContactInfo(value); _contactInfo = value; }
        }

        private void ValidateContactInfo(ContactInfo value)
        {
            if (value == null)
            {
                throw new CustomerException("ContactInfo is null");
            }
        }

        //------------------------------------------------------------------

        private List<Member> _members = new List<Member>();
        public List<Member> Members
        {
            get { return _members; }
            set { ValidateMembers(value); _members = value; }
        }

        private void ValidateMembers(List<Member> value)
        {
            if (value == null)
            {
                throw new CustomerException("Members list is null");
            }
        }

        //------------------------------------------------------------------

    }
}