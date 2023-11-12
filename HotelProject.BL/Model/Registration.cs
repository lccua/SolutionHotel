using HotelProject.BL.Exceptions.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.BL.Model
{
    public class Registration
    {
        public Registration(Customer customer, Activity activity, List<Member> members, decimal totalPrice)
        {
            _customer = customer;
            _activity = activity;
            _members = members;
            _totalPrice = totalPrice;
        }

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

        private Customer _customer;
        public Customer Customer
        {
            get { return _customer; }
            set { ValidateCustomer(value); }
        }

        private void ValidateCustomer(Customer value)
        {
            if (value == null)
            {
                throw new Exception("Customer is null");
            }
            _customer = value;
        }

        //------------------------------------------------------------------

        private Activity _activity;
        public Activity Activity
        {
            get { return _activity; }
            set { ValidateActivity(value); }
        }

        private void ValidateActivity(Activity value)
        {
            if (value == null)
            {
                throw new Exception("Activity is null");
            }
            _activity = value;
        }

        //------------------------------------------------------------------

        private List<Member> _members;
        public List<Member> Members
        {
            get { return _members; }
            set { ValidateMembers(value); }
        }

        public void ValidateMembers(List<Member> value)
        {
            if (value == null || value.Count == 0)
            {
                throw new Exception("Members list is empty or null.");
            }
            _members = value;
        }

        //------------------------------------------------------------------

        private decimal _totalPrice;
        public decimal TotalPrice
        {
            get { return _totalPrice; }
            set { ValidateTotalPrice(value); }
        }

        private void ValidateTotalPrice(decimal value)
        {
            if (value <= 0)
            {
                throw new Exception("Total price is 0");
            }
            _totalPrice = value;
        }

        //------------------------------------------------------------------

    }
}
