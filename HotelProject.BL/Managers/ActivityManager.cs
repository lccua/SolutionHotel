using HotelProject.BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.BL.Managers
{
    public class ActivityManager
    {

        private IActivityRepository _activityRepository;

        public ActivityManager(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }
    }
}
