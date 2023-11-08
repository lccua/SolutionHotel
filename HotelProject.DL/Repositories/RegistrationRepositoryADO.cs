using HotelProject.BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.DL.Repositories
{
    public class RegistrationRepositoryADO : IRegistrationRepository
    {
        private string connectionString;

        public RegistrationRepositoryADO(string connectionString)
        {
            this.connectionString = connectionString;
        }
    }
}
