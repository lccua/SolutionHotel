using HotelProject.BL.Exceptions.Model;
using HotelProject.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.Test.Model
{
    public class AddressTest
    {
        #region HAPPY PATH TESTS

        [Fact]
        public void ConstructWithValidParameters_ShouldCreateAddress()
        {
            // Arrange
            string municipality = "Springfield";
            string zipCode = "12345";
            string houseNumber = "101";
            string street = "Main St";

            // Act
            var address = new Address(municipality, zipCode, houseNumber, street);

            // Assert
            Assert.NotNull(address);
            Assert.Equal(municipality, address.Municipality);
            Assert.Equal(zipCode, address.ZipCode);
            Assert.Equal(houseNumber, address.HouseNumber);
            Assert.Equal(street, address.Street);
        }

        [Fact]
        public void ConstructWithValidAddressLine_ShouldParseCorrectly()
        {
            // Arrange
            string addressLine = "Springfield|Main St|12345|101";

            // Act
            var address = new Address(addressLine);

            // Assert
            Assert.NotNull(address);
            Assert.Equal("Springfield", address.Municipality);
            Assert.Equal("12345", address.ZipCode);
            Assert.Equal("Main St", address.Street);
            Assert.Equal("101", address.HouseNumber);
        }

        [Fact]
        public void ToString_ReturnsFormattedString()
        {
            // Arrange
            var address = new Address("Springfield", "12345", "101", "Main St");

            // Act
            string result = address.ToString();

            // Assert
            Assert.Equal("(Springfield [12345] - Main St - 101)", result);
        }

        [Fact]
        public void ToAddressLine_ReturnsFormattedString()
        {
            // Arrange
            var address = new Address("Springfield", "12345", "101", "Main St");

            // Act
            string result = address.ToAddressLine();

            // Assert
            Assert.Equal("Springfield|12345|Main St|101", result);
        }

        #endregion HAPPY PATH TESTS

        #region UNHAPPY PATH TESTS

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void SetInvalidMunicipality_ShouldThrowAddressException(string invalidMunicipality)
        {
            // Arrange
            var address = new Address();

            // Act & Assert
            Assert.Throws<AddressException>(() => address.Municipality = invalidMunicipality);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void SetInvalidZipCode_ShouldThrowAddressException(string invalidZipCode)
        {
            // Arrange
            var address = new Address();

            // Act & Assert
            Assert.Throws<AddressException>(() => address.ZipCode = invalidZipCode);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void SetInvalidHouseNumber_ShouldThrowAddressException(string invalidHouseNumber)
        {
            // Arrange
            var address = new Address();

            // Act & Assert
            Assert.Throws<AddressException>(() => address.HouseNumber = invalidHouseNumber);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void SetInvalidStreet_ShouldThrowAddressException(string invalidStreet)
        {
            // Arrange
            var address = new Address();

            // Act & Assert
            Assert.Throws<AddressException>(() => address.Street = invalidStreet);
        }

        #endregion UNHAPPY PATH TESTS
    }
}
