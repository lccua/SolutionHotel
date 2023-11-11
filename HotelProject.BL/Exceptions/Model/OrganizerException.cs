﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.BL.Exceptions.Model
{
    public class OrganizerException : Exception
    {
        public OrganizerException(string? message) : base(message)
        {
        }

        public OrganizerException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

    }
}
