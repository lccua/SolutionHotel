using HotelProject.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.BL.Interfaces
{
    public interface IActivityRepository
    {
        List<Activity> GetActivities(string filter);
        int AddActivity(Activity activity);
        int GetLastActivityId();

    }
}
