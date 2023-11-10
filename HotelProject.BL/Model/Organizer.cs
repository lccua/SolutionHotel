using HotelProject.BL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.BL.Model
{
    public class Organizer
    {
        public Organizer(int organizerId, string name, ContactInfo contactInfo)
        {
            OrganizerId = organizerId;
            SetName(name);
            ContactInfo = contactInfo;
        }

        public Organizer( string name, ContactInfo contactInfo)
        {

            SetName(name);
            ContactInfo = contactInfo;
        }

        public int OrganizerId { get; set; }    
        public string Name { get; set; }
        public string Username { get; set; }
        public string HashedPassword { get; set; }
        public ContactInfo ContactInfo { get; set; }


        private void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length > 500) throw new OrganizerException("SetName"); Name = name.Trim();
        }
    }
}
