using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.BL.Exceptions.Model
{
    public class AddressException : Exception
    {
        public AddressException(string? message) : base(message)
        {
        }

        public AddressException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
