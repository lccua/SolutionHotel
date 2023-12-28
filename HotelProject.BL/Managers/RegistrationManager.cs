using HotelProject.BL.Exceptions.Manager;
using HotelProject.BL.Interfaces;
using HotelProject.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.BL.Managers
{
    public class RegistrationManager
    {
        private IRegistrationRepository _registrationRepository;

        public RegistrationManager(IRegistrationRepository registrationRepository)
        {
            _registrationRepository = registrationRepository;
        }

        public void SaveRegistration(Registration registration)
        {
            try
            {
                _registrationRepository.SaveRegistration(registration);
            }
            catch (RegistrationManagerException ex)
            {

                throw new RegistrationManagerException("RegistrationManager: SaveRegistration", ex);
            }

        }

        private int CalculateAge(DateOnly birthday)
        {
            try
            {
                DateOnly today = DateOnly.FromDateTime(DateTime.Today);
                int age = today.Year - birthday.Year;

                if (birthday.DayOfYear > today.DayOfYear)
                {
                    age--;
                }

                return age;
            }
            catch (RegistrationManagerException ex)
            {

                throw new RegistrationManagerException("RegistrationManager: CalculateAge", ex);
            }
          
        }


        public decimal CalculateTotalPrice(List<Member> selectedMembers, Activity selectedActivity)
        {
            try
            {
                // Calculate total price based on selected members and activity
                decimal totalPrice = 0;

                foreach (Member member in selectedMembers)
                {
                    // Calculate age based on member's birthday
                    int age = CalculateAge(member.BirthDay);

                    // Determine the price based on age
                    decimal memberPrice = age >= 18 ? selectedActivity.AdultPrice : selectedActivity.ChildPrice;

                    // Add member price to total
                    totalPrice += memberPrice;
                }

                return totalPrice;
            }
            catch (RegistrationManagerException ex)
            {

                throw new RegistrationManagerException("RegistrationManager: CalculateTotalPrice", ex);
            }


        }
    }
}
