using HotelProject.BL.Model;
using HotelProject.UI.CustomerWPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.UI.CustomerWPF.Mapper
{
    public static class CustomerMapper
    {
        public static CustomerUI MapToUI(Customer customer)
        {
            return new CustomerUI
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.ContactInfo.Email,
                Phone = customer.ContactInfo.Phone,
                Address = customer.ContactInfo.Address.ToString(),
                NrOfMembers = customer.Members.Count,
                Members = customer.Members
            };
        }

        public static Customer MapToDomain(CustomerUI customerUI)
        {
            var address = new Address(customerUI.Address);
            var contactInfo = new ContactInfo(customerUI.Email, customerUI.Phone, address);

            var customer = new Customer(customerUI.Name, customerUI.Id, contactInfo);
            customer.Members = customerUI.Members;

            return customer;
        }
    }

}
