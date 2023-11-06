using HotelProject.BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelProject.BL.Model;
using HotelProject.BL.Exceptions;

namespace HotelProject.BL.Managers
{
    public class ActivityManager
    {

        private IActivityRepository _activityRepository;

        public ActivityManager(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }

        public List<Activity> GetActivities()
        {
            try
            {
                return _activityRepository.GetActivities();
            }
            catch (Exception ex)
            {
                throw new ActivityManagerException("GetAllActivities", ex);
            }
        }
        public void AddActivity(Activity activity)
        {
            try
            {
                _activityRepository.AddActivity(activity);
            }
            catch (Exception ex)
            {
                throw new ActivityManagerException("AddActivity", ex);
            }
        }

        public int GetLastActivityId()
        {
            try
            {
                return _activityRepository.GetLastActivityId();
            }
            catch (Exception ex)
            {
                throw new ActivityManagerException("GetLastActivityId", ex);
            }
        }
    }
}
