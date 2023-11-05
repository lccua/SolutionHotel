using HotelProject.BL.Exceptions;
using HotelProject.BL.Interfaces;
using HotelProject.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.BL.Managers
{
    public class CustomerManager
    {
        private ICustomerRepository _customerRepository;

        public CustomerManager(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

      
        public List<Customer> GetCustomers(string filter)
        {
            try
            {
                return _customerRepository.GetCustomers(filter);
            }
            catch(Exception ex)
            {
                throw new CustomerManagerException("GetAllCustomer",ex);
            }
        }

        public void AddCustomer(Customer customer)
        {
            try
            {
                _customerRepository.AddCustomer(customer);
            }
            catch (Exception ex)
            {
                throw new CustomerManagerException("AddCustomer", ex);
            }
        }

        public int GetLastCustomerId()
        {
            try
            {
                return _customerRepository.GetLastCustomerId();
            }
            catch (Exception ex)
            {
                throw new CustomerManagerException("GetLastCustomerId", ex);
            }
        }

        public void DeleteCustomer(int customerId)
        {
            try
            {
                _customerRepository.DeleteCustomer(customerId);
            }
            catch (Exception ex)
            {
                throw new CustomerManagerException("DeleteCustomer", ex);
            }
        }

        public void UpdateCustomer(Customer customer, int id) 
        {
            try
            {
                _customerRepository.UpdateCustomer(customer, id);
            }
            catch (Exception ex)
            {
                throw new CustomerManagerException("UpdateCustomer", ex);
            }

        }
    }
}
