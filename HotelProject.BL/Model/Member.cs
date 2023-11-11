using HotelProject.BL.Exceptions.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.BL.Model
{
    public class Member
    {
        public Member(string name, DateOnly birthDay)
        {
            _name = name;
            _birthDay = birthDay;
        }

        public Member(int id, string name, DateOnly birthDay)
        {
            _id = id;
            _name = name;
            _birthDay = birthDay;
        }

        public Member() {}

        //------------------------------------------------------------------

        private int _id;
        public int Id
        {
            get { return _id; }
            set { ValidateId(value); }
        }

        private void ValidateId(int value)
        {
            if (value <= 0)
            {
                throw new Exception("Invalid ID");
            }
            _id = value;
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

        private DateOnly _birthDay;
        public DateOnly BirthDay 
        { 
            get { return _birthDay; } 
            set 
            {
                DateTime currentDate = DateTime.Now;
                DateOnly current = new DateOnly(currentDate.Year, currentDate.Month, currentDate.Day);

                if (value < current) throw new MemberException("birthday invalid"); 
                _birthDay = value; 
            } 
        }

        //------------------------------------------------------------------

        /* public override bool Equals(object? obj)
         {
             return obj is Member member &&
                    _name == member._name &&
                    _birthDay.Equals(member._birthDay);
         }

         public override int GetHashCode()
         {
             return HashCode.Combine(_name, _birthDay);
         }*/
    }
}
