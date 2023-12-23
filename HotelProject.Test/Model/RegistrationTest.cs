using HotelProject.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.Test.Model
{
    public class RegistrationTest
    {
        private Registration CreateValidRegistration()
        {
            var validCustomer = new Customer("John Doe", new ContactInfo("email@example.com", "1234567890", new Address("Municipality", "ZipCode", "HouseNumber", "Street")));
            var validActivity = new Activity();
            var validMembers = new List<Member> { new Member("Jane Doe", DateOnly.FromDateTime(DateTime.Now.AddYears(-20))) };
            decimal validTotalPrice = 100m;
            return new Registration(validCustomer, validActivity, validMembers, validTotalPrice);
        }

        #region HAPPY PATH TESTS

        [Fact]
        public void ConstructWithValidParameters_ShouldCreateRegistration()
        {
            // Arrange
            var validCustomer = new Customer("John Doe", new ContactInfo("email@example.com", "1234567890", new Address("Municipality", "ZipCode", "HouseNumber", "Street")));
            var validActivity = new Activity();
            var validMembers = new List<Member> { new Member("Jane Doe", DateOnly.FromDateTime(DateTime.Now.AddYears(-20))) };
            decimal validTotalPrice = 100m;

            // Act
            var registration = new Registration(validCustomer, validActivity, validMembers, validTotalPrice);

            // Assert
            Assert.NotNull(registration);
            Assert.Equal(validCustomer, registration.Customer);
            Assert.Equal(validActivity, registration.Activity);
            Assert.Equal(validMembers, registration.Members);
            Assert.Equal(validTotalPrice, registration.TotalPrice);
        }

        [Fact]
        public void SetValidId_ShouldNotThrowException()
        {
            // Arrange
            var registration = CreateValidRegistration();
            int validId = 1;

            // Act & Assert
            registration.Id = validId;
        }

        #endregion HAPPY PATH TESTS

        #region UNHAPPY PATH TESTS

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void SetInvalidId_ShouldThrowException(int invalidId)
        {
            // Arrange
            var registration = CreateValidRegistration();

            // Act & Assert
            Assert.Throws<Exception>(() => registration.Id = invalidId);
        }

        [Fact]
        public void SetNullCustomer_ShouldThrowException()
        {
            // Arrange
            var registration = CreateValidRegistration();

            // Act & Assert
            Assert.Throws<Exception>(() => registration.Customer = null);
        }

        [Fact]
        public void SetNullActivity_ShouldThrowException()
        {
            // Arrange
            var registration = CreateValidRegistration();

            // Act & Assert
            Assert.Throws<Exception>(() => registration.Activity = null);
        }

        [Fact]
        public void SetEmptyMembersList_ShouldThrowException()
        {
            // Arrange
            var registration = CreateValidRegistration();

            // Act & Assert
            Assert.Throws<Exception>(() => registration.Members = new List<Member>());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void SetInvalidTotalPrice_ShouldThrowException(decimal invalidTotalPrice)
        {
            // Arrange
            var registration = CreateValidRegistration();

            // Act & Assert
            Assert.Throws<Exception>(() => registration.TotalPrice = invalidTotalPrice);
        }

        #endregion UNHAPPY PATH TESTS
    }
}
