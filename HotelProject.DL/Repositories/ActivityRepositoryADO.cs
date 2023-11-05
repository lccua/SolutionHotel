using HotelProject.BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.DL.Repositories
{
    public class ActivityRepositoryADO : IActivityRepository
    {
        private string connectionString;

        public ActivityRepositoryADO(string connectionString)
        {
            this.connectionString = connectionString;
        }

    }
}
