using HotelProject.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.BL.Interfaces
{
    public interface IOrganizerRepository
    {
        void SaveOrganizer(Organizer newOrganizer);
        string GetHashedPasswordByUsername(string username);
    }
}
