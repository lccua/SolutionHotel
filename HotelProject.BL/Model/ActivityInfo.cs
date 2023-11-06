using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.BL.Model
{
    public class ActivityInfo
    {
        public ActivityInfo(string desciption, Address address, int duration)
        {
            Desciption = desciption;
            Address = address;
            Duration = duration;
        }

        public string Desciption { get; set; }
        public Address Address { get; set; }
        public int Duration { get; set; } // Duration of the activity.

    }
}
