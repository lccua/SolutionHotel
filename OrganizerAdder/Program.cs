using HotelProject.BL.Model;
using HotelProject.BL.Managers;
using HotelProject.Util;
using HotelProject.Auth.Service;


namespace OrganizerRegistration
{
    public class Program
    {
        static void Main(string[] args)
        {
            OrganizerManager organizerManager;
            PasswordService passwordService;

            organizerManager = new OrganizerManager(RepositoryFactory.OrganizerRepository);
            passwordService = new PasswordService();

            string city = "Gent";
            string zipCode = "9000";
            string street = "Molenstraat";
            string houseNumber = "5a";
            Address newAddress = new Address(city, zipCode, houseNumber, street);

            string name = "luca";
            string email = "luca@gmail.com";
            string phone = "0498531583";
            ContactInfo newContactInfo = new ContactInfo(email, phone, newAddress);


            string userName = "organizer";

            string password = "organizer";

            string hashedPassword = passwordService.HashPassword(password);

            Organizer newOrganizer = new Organizer(name, newContactInfo);
            newOrganizer.Username = userName;
            newOrganizer.HashedPassword = hashedPassword;

            organizerManager.SaveOrganizer(newOrganizer);
        }
    }
}