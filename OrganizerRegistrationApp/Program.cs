﻿using System.Net;
using HotelProject.BL.Managers;
using HotelProject.BL.Model;
using HotelProject.BL.Security;
using HotelProject.Util;


namespace OrganizerRegistrationApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            OrganizerManager organizerManager;
            PasswordSecurity passwordSecurity;

            organizerManager = new OrganizerManager(RepositoryFactory.OrganizerRepository);
            passwordSecurity = new PasswordSecurity();

            string city = "Gent";
            string zipCode = "9000";
            string street = "Molenstraat";
            string houseNumber = "5a";
            Address newAddress = new Address(city, zipCode, houseNumber, street);

            string name = "luca";
            string email = "luca@gmail.com";
            string phone = "0498531583";
            ContactInfo newContactInfo = new ContactInfo(email, phone, newAddress);


            string userName = "luca";

            string password = "luca";

            string hashedPassword = passwordSecurity.HashPassword(password);
            Console.WriteLine("Password hashed");


            Organizer newOrganizer = new Organizer(name, newContactInfo);
            newOrganizer.Username = userName;
            newOrganizer.HashedPassword = hashedPassword;

            organizerManager.SaveOrganizer(newOrganizer);
            Console.WriteLine();
            Console.WriteLine("Organizer added to database");
        }
    }
}