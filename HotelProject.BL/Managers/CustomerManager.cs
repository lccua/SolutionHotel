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
            catch(CustomerManagerException ex)
            {
                throw new CustomerManagerException("CustomerManager: GetAllCustomer", ex);
            }
        }

        public int AddCustomer(Customer customer)
        {
            try
            {
                return _customerRepository.AddCustomer(customer);
            }
            catch (CustomerManagerException ex)
            {
                throw new CustomerManagerException("CustomerManager: AddCustomer", ex);
            }
        }

        public void DeleteCustomer(int customerId)
        {
            try
            {
                _customerRepository.DeleteCustomer(customerId);
            }
            catch (CustomerManagerException ex)
            {
                throw new CustomerManagerException("CustomerManager: DeleteCustomer", ex);
            }
        }

        public void DeleteMember(int memberId)
        {
            try
            {
                _customerRepository.DeleteMember(memberId);
            }
            catch (CustomerManagerException ex)
            {
                throw new CustomerManagerException("CustomerManager: DeleteMember", ex);
            }
        }

        public void UpdateCustomer(Customer customer, int id) 
        {
            try
            {
                _customerRepository.UpdateCustomer(customer, id);
            }
            catch (CustomerManagerException ex)
            {
                throw new CustomerManagerException("CustomerManager: UpdateCustomer", ex);
            }

        }
    }
}
