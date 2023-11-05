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
        void AddCustomer(Customer customer);
        int GetLastCustomerId();
        void DeleteCustomer (int customerId);
        void UpdateCustomer(Customer customer, int id); 
    }
}
