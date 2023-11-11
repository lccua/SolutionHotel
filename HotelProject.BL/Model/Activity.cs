using HotelProject.BL.Exceptions.Model;  // Assuming ActivityException is in the same namespace
using System.Security.Cryptography;

namespace HotelProject.BL.Model
{
    public class Activity
    {
        public Activity(int id, string name, ActivityInfo activityInfo, string scheduledDate, int availableSpots, decimal adultPrice, decimal childPrice, int discount)
        {
            _id = id;
            _name = name;
            _activityInfo = activityInfo;
            _scheduledDate = scheduledDate;
            _availableSpots = availableSpots;
            _adultPrice = adultPrice;
            _childPrice = childPrice;
            _discount = discount;
        }

        public Activity(string name, ActivityInfo activityInfo, string scheduledDate, int availableSpots, decimal adultPrice, decimal childPrice, int discount)
        {
            _name = name;
            _activityInfo = activityInfo;
            _scheduledDate = scheduledDate;
            _availableSpots = availableSpots;
            _adultPrice = adultPrice;
            _childPrice = childPrice;
            _discount = discount;
        }

        public Activity() { }


        //------------------------------------------------------------------

        // ID property
        private int _id;
        public int Id
        {
            get { return _id; }
            set { ValidateId(value); _id = value; }
        }

        // Validation for the ID property
        private void ValidateId(int value)
        {
            if (value <= 0)
            {
                throw new ActivityException("Invalid ID");
            }
        }


        //------------------------------------------------------------------

        // Name property
        private string _name;
        public string Name
        {
            get { return _name; }
            set { ValidateName(value); _name = value; }
        }

        // Validation for the Name property
        private void ValidateName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ActivityException("Name is empty");
            }
        }

        //------------------------------------------------------------------

        // ActivityInfo property
        private ActivityInfo _activityInfo;
        public ActivityInfo ActivityInfo
        {
            get { return _activityInfo; }
            set { ValidateActivityInfo(value); _activityInfo = value; }
        }

        // Validation for the ActivityInfo property
        private void ValidateActivityInfo(ActivityInfo value)
        {
            if (value == null)
            {
                throw new ActivityException("ActivityInfo is null");
            }
        }

        //------------------------------------------------------------------

        // ScheduledDate property
        private string _scheduledDate;
        public string ScheduledDate
        {
            get { return _scheduledDate; }
            set { ValidateScheduledDate(value); _scheduledDate = value; }
        }

        // Validation for the ScheduledDate property
        private void ValidateScheduledDate(string value)
        {
            // Assuming you want to check if the date is in a valid format
            if (!DateTime.TryParse(value, out DateTime scheduledDate))
            {
                throw new ActivityException("Invalid date format for ScheduledDate");
            }

            // Check if the date is not in the past
            if (scheduledDate < DateTime.Now)
            {
                throw new ActivityException("ScheduledDate cannot be in the past");
            }
        }


        //------------------------------------------------------------------

        // AvailableSpots property
        private int _availableSpots;
        public int AvailableSpots
        {
            get { return _availableSpots; }
            set { ValidateAvailableSpots(value); _availableSpots = value; }
        }

        // Validation for the AvailableSpots property
        private void ValidateAvailableSpots(int value)
        {
            if (value < 0)
            {
                throw new ActivityException("AvailableSpots cannot be negative");
            }
        }

        //------------------------------------------------------------------

        // AdultPrice property
        private decimal _adultPrice;
        public decimal AdultPrice
        {
            get { return _adultPrice; }
            set { ValidateAdultPrice(value); _adultPrice = value; }
        }

        // Validation for the AdultPrice property
        private void ValidateAdultPrice(decimal value)
        {
            if (value < 0)
            {
                throw new ActivityException("AdultPrice cannot be negative");
            }
        }

        //------------------------------------------------------------------

        // ChildPrice property
        private decimal _childPrice;
        public decimal ChildPrice
        {
            get { return _childPrice; }
            set { ValidateChildPrice(value); _childPrice = value; }
        }

        // Validation for the ChildPrice property
        private void ValidateChildPrice(decimal value)
        {
            if (value < 0)
            {
                throw new ActivityException("ChildPrice cannot be negative");
            }
        }

        //------------------------------------------------------------------

        // Discount property
        private int _discount;
        public int Discount
        {
            get { return _discount; }
            set { ValidateDiscount(value); _discount = value; }
        }

        // Validation for the Discount property
        private void ValidateDiscount(int value)
        {
            const int minimumDiscount = 0;
            const int maximumDiscount = 100;  // Assuming the discount is a percentage

            if (value < minimumDiscount || value > maximumDiscount)
            {
                throw new ActivityException($"Discount must be between {minimumDiscount}% and {maximumDiscount}%");
            }
        }

        //------------------------------------------------------------------

    }
}
