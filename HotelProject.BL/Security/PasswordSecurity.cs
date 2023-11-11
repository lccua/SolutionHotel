using HotelProject.BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.BL.Security
{
    public class PasswordSecurity
    {

        public bool VerifyPassword(string hashedPasswordFromDatabase, string enteredPassword)
        {

            // Check if the entered password matches the stored hashed password
            bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(enteredPassword, hashedPasswordFromDatabase);

            return isPasswordCorrect;
        }

        public string HashPassword(string password)
        {
            // Generate a salt and hash the password
            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);

            return hashedPassword;
        }
    }
}
