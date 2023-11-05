using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.BL.Model
{
    public class ActivityInfo
    {
        public string Desciption { get; set; }
        public Address Address { get; set; }
        public TimeSpan Duration { get; set; } // Duration of the activity.

    }
}
