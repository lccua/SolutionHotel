﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.BL.Exceptions.Manager
{
    public class OrganizerManagerException : Exception
    {
        public OrganizerManagerException(string? message) : base(message)
        {
        }

        public OrganizerManagerException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
