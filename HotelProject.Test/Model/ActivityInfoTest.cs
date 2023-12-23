using HotelProject.BL.Exceptions.Model;
using HotelProject.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.Test.Model
{
    public class ActivityInfoTest
    {
        #region HAPPY PATH TESTS

        [Fact]
        public void SetValidDescription_ShouldNotThrowException()
        {
            // Arrange
            var activityInfo = new ActivityInfo();
            var validDescription = "Sightseeing Tour";

            // Act
            var exception = Record.Exception(() => activityInfo.Description = validDescription);

            // Assert
            Assert.Null(exception);
        }

        [Fact]
        public void SetValidAddress_ShouldNotThrowException()
        {
            // Arrange
            var activityInfo = new ActivityInfo();
            var validAddress = new Address("Municipality", "ZipCode", "HouseNumber", "Street");

            // Act
            var exception = Record.Exception(() => activityInfo.Address = validAddress);

            // Assert
            Assert.Null(exception);
        }

        [Fact]
        public void SetValidDuration_ShouldNotThrowException()
        {
            // Arrange
            var activityInfo = new ActivityInfo();
            var validDuration = 60;

            // Act
            var exception = Record.Exception(() => activityInfo.Duration = validDuration);

            // Assert
            Assert.Null(exception);
        }

        #endregion HAPPY PATH TESTS

        #region UNHAPPY PATH TESTS

        [Fact]
        public void SetEmptyDescription_ShouldThrowActivityInfoException()
        {
            // Arrange
            var activityInfo = new ActivityInfo();
            var invalidDescription = "";

            // Act & Assert
            Assert.Throws<ActivityInfoException>(() => activityInfo.Description = invalidDescription);
        }

        [Fact]
        public void SetNullAddress_ShouldThrowActivityInfoException()
        {
            // Arrange
            var activityInfo = new ActivityInfo();

            // Act & Assert
            Assert.Throws<ActivityInfoException>(() => activityInfo.Address = null);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void SetInvalidDuration_ShouldThrowActivityInfoException(int invalidDuration)
        {
            // Arrange
            var activityInfo = new ActivityInfo();

            // Act & Assert
            Assert.Throws<ActivityInfoException>(() => activityInfo.Duration = invalidDuration);
        }

        #endregion UNHAPPY PATH TESTS

    }
}
