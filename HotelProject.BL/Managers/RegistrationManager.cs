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
            catch (Exception)
            {

                throw;
            }
        
        }

        private int CalculateAge(DateOnly birthday)
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.Today);
            int age = today.Year - birthday.Year;

            if (birthday.DayOfYear > today.DayOfYear)
            {
                age--;
            }

            return age;
        }


        public decimal CalculateTotalPrice(List<Member> selectedMembers, Activity selectedActivity)
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
    }
}
