﻿using HotelProject.BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelProject.BL.Model;
using HotelProject.BL.Exceptions.Manager;

namespace HotelProject.BL.Managers
{
    public class ActivityManager
    {

        private IActivityRepository _activityRepository;

        public ActivityManager(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }

        public List<Activity> GetActivities(string filter)
        {
            try
            {
                return _activityRepository.GetActivities(filter);
            }
            catch (Exception ex)
            {
                throw new ActivityManagerException("GetAllActivities", ex);
            }
        }
        public int AddActivity(Activity activity)
        {
            try
            {
                 return _activityRepository.AddActivity(activity);
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
