using HotelProject.BL.Exceptions.Model;
using HotelProject.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.Test.Model
{
    public class OrganizerTest
    {
        #region HAPPY PATH TESTS

        [Fact]
        public void ConstructWithValidParameters_ShouldCreateOrganizer()
        {
            // Arrange
            int validOrganizerId = 1;
            string validName = "John Doe";
            var validContactInfo = new ContactInfo("email@example.com", "1234567890", new Address("Municipality", "ZipCode", "HouseNumber", "Street"));
            string validUsername = "johndoe";
            string validHashedPassword = "hashedpassword";

            // Act
            var organizer = new Organizer(validOrganizerId, validName, validContactInfo)
            {
                Username = validUsername,
                HashedPassword = validHashedPassword
            };

            // Assert
            Assert.NotNull(organizer);
            Assert.Equal(validOrganizerId, organizer.OrganizerId);
            Assert.Equal(validName, organizer.Name);
            Assert.Equal(validContactInfo, organizer.ContactInfo);
            Assert.Equal(validUsername, organizer.Username);
            Assert.Equal(validHashedPassword, organizer.HashedPassword);
        }


        #endregion HAPPY PATH TESTS

        #region UNHAPPY PATH TESTS

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void SetInvalidOrganizerId_ShouldThrowOrganizerException(int invalidOrganizerId)
        {
            // Arrange
            var organizer = new Organizer();

            // Act & Assert
            Assert.Throws<OrganizerException>(() => organizer.OrganizerId = invalidOrganizerId);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void SetInvalidName_ShouldThrowOrganizerException(string invalidName)
        {
            // Arrange
            var organizer = new Organizer();

            // Act & Assert
            Assert.Throws<OrganizerException>(() => organizer.Name = invalidName);
        }

        [Fact]
        public void SetNullContactInfo_ShouldThrowOrganizerException()
        {
            // Arrange
            var organizer = new Organizer();

            // Act & Assert
            Assert.Throws<OrganizerException>(() => organizer.ContactInfo = null);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void SetInvalidUsername_ShouldThrowOrganizerException(string invalidUsername)
        {
            // Arrange
            var organizer = new Organizer();

            // Act & Assert
            Assert.Throws<OrganizerException>(() => organizer.Username = invalidUsername);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void SetInvalidHashedPassword_ShouldThrowOrganizerException(string invalidHashedPassword)
        {
            // Arrange
            var organizer = new Organizer();

            // Act & Assert
            Assert.Throws<OrganizerException>(() => organizer.HashedPassword = invalidHashedPassword);
        }


        #endregion UNHAPPY PATH TESTS
    }
}
