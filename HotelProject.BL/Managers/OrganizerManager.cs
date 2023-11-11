using HotelProject.BL.Interfaces;
using HotelProject.BL.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;

namespace HotelProject.BL.Managers
{
    public class OrganizerManager
    {
        private IOrganizerRepository _organizerRepository;

        public OrganizerManager(IOrganizerRepository organizerRepository)
        {
            _organizerRepository = organizerRepository;
        }

      


       


        public void SaveOrganizer(Organizer newOrganizer)
        {
            try
            {
                _organizerRepository.SaveOrganizer(newOrganizer);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string GetHashedPasswordByUsername(string username)
        {
            try
            {
               return _organizerRepository.GetHashedPasswordByUsername(username);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
