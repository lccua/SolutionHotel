using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.BL.Exceptions.Model
{
    public class ActivityInfoException : Exception
    {
        public ActivityInfoException(string? message) : base(message)
        {
        }

        public ActivityInfoException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
