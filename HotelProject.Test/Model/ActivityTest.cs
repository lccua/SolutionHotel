using HotelProject.BL.Exceptions.Model;
using HotelProject.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.Test.Model
{
    public class ActivityTest
    {
        #region HAPPY PATH TESTS

        [Fact]
        public void SetValidId_ShouldNotThrowException()
        {
            // Arrange
            var activity = new Activity();
            int validId = 1;

            // Act
            activity.Id = validId;

            // Assert
            Assert.Equal(validId, activity.Id);
        }

        [Fact]
        public void SetValidName_ShouldNotThrowException()
        {
            // Arrange
            var activity = new Activity();
            string validName = "Kayaking Adventure";

            // Act
            activity.Name = validName;

            // Assert
            Assert.Equal(validName, activity.Name);
        }

        [Fact]
        public void SetValidActivityInfo_ShouldNotThrowException()
        {
            // Arrange
            var activity = new Activity();
            var validAddress = new Address("MunicipalityName", "ZipCode", "HouseNumber", "StreetName");
            var validActivityInfo = new ActivityInfo("Valid Description", validAddress, 60);

            // Act
            activity.ActivityInfo = validActivityInfo;

            // Assert
            Assert.Equal(validActivityInfo, activity.ActivityInfo);
        }

        [Fact]
        public void SetValidScheduledDate_ShouldNotThrowException()
        {
            // Arrange
            var activity = new Activity();
            string validScheduledDate = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"); // Future date

            // Act
            activity.ScheduledDate = validScheduledDate;

            // Assert
            Assert.Equal(validScheduledDate, activity.ScheduledDate);
        }

        // ... Continue with other happy path tests for AvailableSpots, AdultPrice, ChildPrice, and Discount

        #endregion HAPPY PATH TESTS

        #region UNHAPPY PATH TESTS

        [Fact]
        public void SetInvalidId_ShouldThrowActivityException()
        {
            // Arrange
            var activity = new Activity();
            int invalidId = -1;

            // Act & Assert
            Assert.Throws<ActivityException>(() => activity.Id = invalidId);
        }

        [Fact]
        public void SetEmptyName_ShouldThrowActivityException()
        {
            // Arrange
            var activity = new Activity();
            string invalidName = "";

            // Act & Assert
            Assert.Throws<ActivityException>(() => activity.Name = invalidName);
        }

        [Fact]
        public void SetNullActivityInfo_ShouldThrowActivityException()
        {
            // Arrange
            var activity = new Activity();
            ActivityInfo invalidActivityInfo = null;

            // Act & Assert
            Assert.Throws<ActivityException>(() => activity.ActivityInfo = invalidActivityInfo);
        }

        [Theory]
        [InlineData("invalid-date")]
        [InlineData("1990-01-01")] // Past date
        public void SetInvalidScheduledDate_ShouldThrowActivityException(string invalidDate)
        {
            // Arrange
            var activity = new Activity();

            // Act & Assert
            Assert.Throws<ActivityException>(() => activity.ScheduledDate = invalidDate);
        }

        [Fact]
        public void SetNegativeAvailableSpots_ShouldThrowActivityException()
        {
            // Arrange
            var activity = new Activity();
            int invalidAvailableSpots = -1;

            // Act & Assert
            Assert.Throws<ActivityException>(() => activity.AvailableSpots = invalidAvailableSpots);
        }

        [Fact]
        public void SetNegativeAdultPrice_ShouldThrowActivityException()
        {
            // Arrange
            var activity = new Activity();
            decimal invalidAdultPrice = -1m;

            // Act & Assert
            Assert.Throws<ActivityException>(() => activity.AdultPrice = invalidAdultPrice);
        }

        [Fact]
        public void SetNegativeChildPrice_ShouldThrowActivityException()
        {
            // Arrange
            var activity = new Activity();
            decimal invalidChildPrice = -1m;

            // Act & Assert
            Assert.Throws<ActivityException>(() => activity.ChildPrice = invalidChildPrice);
        }

        [Theory]
        [InlineData(-1)]  // Less than minimum
        [InlineData(101)] // More than maximum
        public void SetInvalidDiscount_ShouldThrowActivityException(int invalidDiscount)
        {
            // Arrange
            var activity = new Activity();

            // Act & Assert
            Assert.Throws<ActivityException>(() => activity.Discount = invalidDiscount);
        }

        #endregion UNHAPPY PATH TESTS
    }
}
