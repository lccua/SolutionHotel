﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.BL.Model
{
    public class Activity
    {
        public Activity(int id, string name, ActivityInfo activityInfo, string scheduledDate, int availableSpots, decimal adultPrice, decimal childPrice, int discount)
        {
            Id = id;
            Name = name;
            ActivityInfo = activityInfo;
            ScheduledDate = scheduledDate;
            AvailableSpots = availableSpots;
            AdultPrice = adultPrice;
            ChildPrice = childPrice;
            Discount = discount;
        }

        public Activity(string name, ActivityInfo activityInfo, string scheduledDate, int availableSpots, decimal adultPrice, decimal childPrice, int discount)
        {
            Name = name;
            ActivityInfo = activityInfo;
            ScheduledDate = scheduledDate;
            AvailableSpots = availableSpots;
            AdultPrice = adultPrice;
            ChildPrice = childPrice;
            Discount = discount;
        }

        public Activity()
        {
                
        }

        public int Id { get; set; }

        public string Name { get; set; }
        public ActivityInfo ActivityInfo { get; set; }

        public string ScheduledDate { get; set; }
        public int AvailableSpots { get; set; }


        public decimal AdultPrice { get; set; } // Cost for adults to participate in the activity.
        public decimal ChildPrice { get; set; } // Cost for children to participate in the activity.

        public int Discount { get; set; } // Discount applied to the activity.


    }
}
