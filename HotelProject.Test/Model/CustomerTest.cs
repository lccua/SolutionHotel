using HotelProject.BL.Exceptions.Model;
using HotelProject.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.Test.Model
{
    public class CustomerTest
    {
        #region HAPPY PATH TESTS

        [Fact]
        public void ConstructWithValidParameters_ShouldCreateCustomer()
        {
            // Arrange
            string validName = "John Doe";
            var validContactInfo = new ContactInfo("email@example.com", "1234567890", new Address("Municipality", "ZipCode", "HouseNumber", "Street"));
            int validId = 1;

            // Act
            var customer = new Customer(validName, validId, validContactInfo);

            // Assert
            Assert.NotNull(customer);
            Assert.Equal(validName, customer.Name);
            Assert.Equal(validId, customer.Id);
            Assert.Equal(validContactInfo, customer.ContactInfo);
        }

        [Fact]
        public void AddingValidMember_ShouldNotThrowException()
        {
            // Arrange
            var customer = new Customer("John Doe", new ContactInfo("email@example.com", "1234567890", new Address("Municipality", "ZipCode", "HouseNumber", "Street")));
            var member = new Member(); // Assume Member is a valid class and has a default constructor.

            // Act
            customer.Members.Add(member);

            // Assert
            Assert.Contains(member, customer.Members);
        }

        [Fact]
        public void RemovingExistingMember_ShouldNotThrowException()
        {
            // Arrange
            var customer = new Customer("John Doe", new ContactInfo("email@example.com", "1234567890", new Address("Municipality", "ZipCode", "HouseNumber", "Street")));
            var member = new Member(); // Assume Member is a valid class and has a default constructor.
            customer.Members.Add(member);

            // Act
            customer.RemoveMember(member);

            // Assert
            Assert.DoesNotContain(member, customer.Members);
        }

        #endregion HAPPY PATH TESTS

        #region UNHAPPY PATH TESTS

        [Fact]
        public void RemovingNonExistentMember_ShouldThrowCustomerException()
        {
            // Arrange
            var customer = new Customer("John Doe", new ContactInfo("email@example.com", "1234567890", new Address("Municipality", "ZipCode", "HouseNumber", "Street")));
            var member = new Member(); // Assume Member is a valid class and has a default constructor.

            // Act & Assert
            Assert.Throws<CustomerException>(() => customer.RemoveMember(member));
        }

        [Fact]
        public void SetNullMembersList_ShouldThrowCustomerException()
        {
            // Arrange
            var customer = new Customer("John Doe", new ContactInfo("email@example.com", "1234567890", new Address("Municipality", "ZipCode", "HouseNumber", "Street")));

            // Act & Assert
            Assert.Throws<CustomerException>(() => customer.Members = null);
        }

        #endregion UNHAPPY PATH TESTS
    }
}
