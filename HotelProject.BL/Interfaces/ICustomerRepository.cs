using HotelProject.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.BL.Interfaces
{
    public interface ICustomerRepository
    {
        List<Customer> GetCustomers(string filter);
        int AddCustomer(Customer customer);
        void DeleteCustomer (int customerId);
        void UpdateCustomer(Customer customer, int id);
        void DeleteMember(int memberId);
    }
}
