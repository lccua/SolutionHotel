using HotelProject.BL.Exceptions.Model;
using HotelProject.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.Test.Model
{
    public class ContactInfoTest
    {
        #region HAPPY PATH TESTS

        [Fact]
        public void ConstructWithValidParameters_ShouldCreateContactInfo()
        {
            // Arrange
            string validEmail = "test@example.com";
            string validPhone = "1234567890";
            var validAddress = new Address("Municipality", "ZipCode", "HouseNumber", "Street");

            // Act
            var contactInfo = new ContactInfo(validEmail, validPhone, validAddress);

            // Assert
            Assert.NotNull(contactInfo);
            Assert.Equal(validEmail, contactInfo.Email);
            Assert.Equal(validPhone, contactInfo.Phone);
            Assert.Equal(validAddress, contactInfo.Address);
        }

        [Theory]
        [InlineData("user@example.com")]
        [InlineData("contact@domain.co.uk")]
        public void SetValidEmail_ShouldNotThrowException(string validEmail)
        {
            // Arrange
            var contactInfo = new ContactInfo("placeholder@example.com", "1234567890", new Address("Municipality", "ZipCode", "HouseNumber", "Street"));

            // Act
            var exception = Record.Exception(() => contactInfo.Email = validEmail);

            // Assert
            Assert.Null(exception);
        }

        [Fact]
        public void SetValidPhone_ShouldNotThrowException()
        {
            // Arrange
            var contactInfo = new ContactInfo("test@example.com", "1234567890", new Address("Municipality", "ZipCode", "HouseNumber", "Street"));
            string validPhone = "0987654321";

            // Act
            var exception = Record.Exception(() => contactInfo.Phone = validPhone);

            // Assert
            Assert.Null(exception);
        }


        #endregion HAPPY PATH TESTS

        #region UNHAPPY PATH TESTS

        [Theory]
        [InlineData("")]
        [InlineData("invalidemail")]
        [InlineData("missingatmark.com")]
        public void SetInvalidEmail_ShouldThrowContactInfoException(string invalidEmail)
        {
            // Arrange
            var contactInfo = new ContactInfo("placeholder@example.com", "1234567890", new Address("Municipality", "ZipCode", "HouseNumber", "Street"));

            // Act & Assert
            Assert.Throws<ContactInfoException>(() => contactInfo.Email = invalidEmail);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void SetInvalidPhone_ShouldThrowContactInfoException(string invalidPhone)
        {
            // Arrange
            var contactInfo = new ContactInfo("test@example.com", "1234567890", new Address("Municipality", "ZipCode", "HouseNumber", "Street"));

            // Act & Assert
            Assert.Throws<ContactInfoException>(() => contactInfo.Phone = invalidPhone);
        }

        [Fact]
        public void SetNullAddress_ShouldThrowContactInfoException()
        {
            // Arrange
            var contactInfo = new ContactInfo("test@example.com", "1234567890", new Address("Municipality", "ZipCode", "HouseNumber", "Street"));

            // Act & Assert
            Assert.Throws<ContactInfoException>(() => contactInfo.Address = null);
        }

        #endregion UNHAPPY PATH TESTS
    }
}

