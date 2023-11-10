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

        public string GenerateHash(string password)
        {
            // Generate a salt and hash the password
            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);

            return hashedPassword;
        }


        public bool AuthenticateUser(string hashedPasswordFromDatabase, string enteredPassword)
        {
           
        
            // Check if the entered password matches the stored hashed password
            bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(enteredPassword, hashedPasswordFromDatabase);

            return isPasswordCorrect;
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

        public string GetHashedPasswordAndSaltByUsername(string username)
        {
            try
            {
               return _organizerRepository.GetHashedPasswordAndSaltByUsername(username);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
