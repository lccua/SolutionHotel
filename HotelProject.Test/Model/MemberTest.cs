using HotelProject.BL.Exceptions.Model;
using HotelProject.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.Test.Model
{
    public class MemberTest
    {
        #region HAPPY PATH TESTS

        [Fact]
        public void ConstructWithValidParameters_ShouldCreateMember()
        {
            // Arrange
            string validName = "John Doe";
            DateOnly validBirthDay = DateOnly.FromDateTime(DateTime.Now.AddYears(-20));

            // Act
            var member = new Member(validName, validBirthDay);

            // Assert
            Assert.NotNull(member);
            Assert.Equal(validName, member.Name);
            Assert.Equal(validBirthDay, member.BirthDay);
        }

        [Fact]
        public void SetValidId_ShouldNotThrowException()
        {
            // Arrange
            var member = new Member();
            int validId = 1;

            // Act & Assert
            member.Id = validId;
        }

        [Fact]
        public void SetValidName_ShouldNotThrowException()
        {
            // Arrange
            var member = new Member();
            string validName = "Jane Doe";

            // Act & Assert
            member.Name = validName;
        }

        [Fact]
        public void SetValidBirthDay_ShouldNotThrowException()
        {
            // Arrange
            var member = new Member();
            DateOnly validBirthDay = DateOnly.FromDateTime(DateTime.Now.AddYears(-20));

            // Act & Assert
            member.BirthDay = validBirthDay;
        }

        #endregion HAPPY PATH TESTS

        #region UNHAPPY PATH TESTS

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void SetInvalidId_ShouldThrowException(int invalidId)
        {
            // Arrange
            var member = new Member();

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => member.Id = invalidId);
            Assert.Equal("Invalid ID", exception.Message);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void SetInvalidName_ShouldThrowOrganizerException(string invalidName)
        {
            // Arrange
            var member = new Member();

            // Act & Assert
            Assert.Throws<OrganizerException>(() => member.Name = invalidName);
        }

        [Fact]
        public void SetFutureBirthDay_ShouldThrowMemberException()
        {
            // Arrange
            var member = new Member();
            DateOnly futureBirthDay = DateOnly.FromDateTime(DateTime.Now.AddDays(1));

            // Act & Assert
            Assert.Throws<MemberException>(() => member.BirthDay = futureBirthDay);
        }

        #endregion UNHAPPY PATH TESTS
    }
}
