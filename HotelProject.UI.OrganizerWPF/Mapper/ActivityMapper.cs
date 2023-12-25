using HotelProject.BL.Model;
using HotelProject.UI.OrganizerWPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.UI.OrganizerWPF.Mapper
{
    public static class ActivityMapper
    {
        // Maps from Activity (BL model) to ActivityUI (UI model)
        public static ActivityUI MapToUI(Activity activity)
        {
            return new ActivityUI(
                                   id: activity.Id,
                                   name: activity.Name,
                                   sheduledDate: activity.ScheduledDate,
                                   availableSpots: activity.AvailableSpots,
                                   adultPrice: activity.AdultPrice,
                                   childPrice: activity.ChildPrice,
                                   discount: activity.Discount,
                                   description: activity.ActivityInfo.Description,  
                                   duration: activity.ActivityInfo.Duration,       
                                   address: activity.ActivityInfo.Address.ToString()      
                                 );
        }

        // Maps from ActivityUI (UI model) to Activity (BL model)
        public static Activity MapToDomain(ActivityUI activityUI)
        {

            var address = new Address(activityUI.Address);
            var activityInfo = new ActivityInfo
            {
                // Assign properties to activityInfo here, assuming it has settable properties
                Description = activityUI.Description,
                Duration = activityUI.Duration,
                Address = address
            };

            return new Activity
            {
                Id = activityUI.Id,
                Name = activityUI.Name,
                ActivityInfo = activityInfo,
                ScheduledDate = activityUI.SheduledDate,
                AvailableSpots = activityUI.AvailableSpots,
                AdultPrice = activityUI.AdultPrice,
                ChildPrice = activityUI.ChildPrice,
                Discount = activityUI.Discount
            };
        }
    }

}
